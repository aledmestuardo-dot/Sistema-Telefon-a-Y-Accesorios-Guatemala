param(
    [string]$EnvFile = (Join-Path (Split-Path -Parent $PSScriptRoot) '.env'),
    [string]$OutputFile = (Join-Path (Split-Path -Parent $PSScriptRoot) '.local-delivery\08_SQLServer_Usuarios_Iniciales.sql')
)

$ErrorActionPreference = 'Stop'

function Leer-ConfiguracionEnv {
    param([string]$Ruta)

    if (-not (Test-Path -LiteralPath $Ruta -PathType Leaf)) {
        throw "No se encontró el archivo de configuración local: $Ruta"
    }

    $valores = @{}
    foreach ($lineaOriginal in Get-Content -LiteralPath $Ruta) {
        $linea = $lineaOriginal.Trim()
        if (-not $linea -or $linea.StartsWith('#')) { continue }
        $separador = $linea.IndexOf('=')
        if ($separador -le 0) { continue }
        $valores[$linea.Substring(0, $separador).Trim()] = $linea.Substring($separador + 1).Trim()
    }
    return $valores
}

function Convertir-LiteralSql {
    param([AllowNull()][string]$Valor)
    if ($null -eq $Valor) { return 'NULL' }
    return "N'" + $Valor.Replace("'", "''") + "'"
}

function Convertir-FechaSql {
    param([AllowNull()][object]$Valor)
    if ($null -eq $Valor -or $Valor -is [DBNull]) { return 'NULL' }
    $fecha = [DateTime]$Valor
    return "CONVERT(datetime, '" + $fecha.ToString('yyyy-MM-ddTHH:mm:ss.fff', [Globalization.CultureInfo]::InvariantCulture) + "', 126)"
}

$config = Leer-ConfiguracionEnv -Ruta $EnvFile
foreach ($requerida in @('DB_SERVER', 'DB_NAME', 'DB_USER', 'DB_PASSWORD')) {
    if (-not $config.ContainsKey($requerida) -or [string]::IsNullOrWhiteSpace($config[$requerida])) {
        throw "Falta la variable $requerida en el archivo .env local."
    }
}

$servidor = $config['DB_SERVER']
if ($config['DB_PORT'] -and -not $servidor.Contains(',')) {
    $servidor += ',' + $config['DB_PORT']
}

$builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
$builder['Data Source'] = $servidor
$builder['Initial Catalog'] = $config['DB_NAME']
$builder['User ID'] = $config['DB_USER']
$builder['Password'] = $config['DB_PASSWORD']
$builder['Encrypt'] = $true
$builder['TrustServerCertificate'] = $true

$conexion = New-Object System.Data.SqlClient.SqlConnection $builder.ConnectionString
$comando = $conexion.CreateCommand()
$comando.CommandText = @'
SELECT TipoUsuario, Nombre, Contrasenia, CorreoElectronico, FechaCreacion, FechaUltimoAcceso, Estado
FROM dbo.Tbl_Usuarios
WHERE Estado = 1
ORDER BY CodigoUsuario;
'@

$filas = New-Object System.Collections.Generic.List[object]
try {
    $conexion.Open()
    $lector = $comando.ExecuteReader()
    try {
        while ($lector.Read()) {
            $hash = [string]$lector['Contrasenia']
            if (-not $hash.StartsWith('PBKDF2$', [StringComparison]::Ordinal)) {
                throw 'Se encontró una credencial activa que no usa el formato PBKDF2 esperado; no se generó el archivo.'
            }

            $filas.Add([pscustomobject]@{
                TipoUsuario = [string]$lector['TipoUsuario']
                Nombre = [string]$lector['Nombre']
                Hash = $hash
                Correo = [string]$lector['CorreoElectronico']
                FechaCreacion = $lector['FechaCreacion']
                FechaUltimoAcceso = $lector['FechaUltimoAcceso']
                Estado = [bool]$lector['Estado']
            })
        }
    }
    finally {
        $lector.Dispose()
    }
}
finally {
    $comando.Dispose()
    $conexion.Dispose()
    $builder.Clear()
    $config['DB_PASSWORD'] = $null
}

if ($filas.Count -eq 0) {
    throw 'No hay usuarios activos que exportar. No se generó el archivo.'
}

$duplicados = $filas | Group-Object -Property Nombre | Where-Object { $_.Count -gt 1 }
if ($duplicados) {
    throw 'Hay nombres de usuario activos duplicados. No se generó el archivo.'
}

$directorioSalida = Split-Path -Parent $OutputFile
if (-not (Test-Path -LiteralPath $directorioSalida -PathType Container)) {
    New-Item -ItemType Directory -Path $directorioSalida -Force | Out-Null
}
$rutaSalidaCompleta = [System.IO.Path]::GetFullPath($OutputFile)
if (Test-Path -LiteralPath $rutaSalidaCompleta) {
    throw "El archivo de salida ya existe; elige otra ruta para no sobrescribirlo: $rutaSalidaCompleta"
}

$sql = New-Object System.Collections.Generic.List[string]
$sql.Add('-- Usuarios activos de la aplicación para inicializar una base Docker limpia.')
$sql.Add('-- Contiene hashes PBKDF2, no contraseñas en texto plano; mantener el archivo fuera de Git.')
$sql.Add('USE [BD_TelefoniaAccesoriosSA];')
$sql.Add('GO')
$sql.Add('SET XACT_ABORT ON;')
$sql.Add('BEGIN TRY')
$sql.Add('    BEGIN TRANSACTION;')
foreach ($fila in $filas) {
    $nombre = Convertir-LiteralSql $fila.Nombre
    $correo = Convertir-LiteralSql $fila.Correo
    $tipo = Convertir-LiteralSql $fila.TipoUsuario
    $hash = Convertir-LiteralSql $fila.Hash
    $creada = Convertir-FechaSql $fila.FechaCreacion
    $ultimoAcceso = Convertir-FechaSql $fila.FechaUltimoAcceso
    $estado = if ($fila.Estado) { '1' } else { '0' }

    $sql.Add("    IF NOT EXISTS (SELECT 1 FROM dbo.Tbl_Usuarios WHERE Nombre = $nombre)")
    $sql.Add('    BEGIN')
    $sql.Add('        INSERT INTO dbo.Tbl_Usuarios (TipoUsuario, Nombre, Contrasenia, CorreoElectronico, FechaCreacion, FechaUltimoAcceso, Estado)')
    $sql.Add("        VALUES ($tipo, $nombre, $hash, $correo, $creada, $ultimoAcceso, $estado);")
    $sql.Add('    END;')
}
$sql.Add('    COMMIT TRANSACTION;')
$sql.Add('END TRY')
$sql.Add('BEGIN CATCH')
$sql.Add('    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;')
$sql.Add('    THROW;')
$sql.Add('END CATCH;')
$sql.Add('GO')

[System.IO.File]::WriteAllLines($rutaSalidaCompleta, $sql, [System.Text.UTF8Encoding]::new($false))
Write-Output "Se generó el seed de $($filas.Count) usuarios activos en $rutaSalidaCompleta. Incluye hashes; no subirlo a Git."
