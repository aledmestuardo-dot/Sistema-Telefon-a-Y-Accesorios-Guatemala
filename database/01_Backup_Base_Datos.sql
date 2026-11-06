-- Respaldo completo de la base verificada.
-- Ejecutar desde el contenedor SQL Server con permisos BACKUP DATABASE.
-- El destino se crea dentro de /var/opt/mssql/data, una ruta validada para este contenedor.
-- Se aborta si el archivo de salida ya existe; no se sobrescribe un respaldo anterior.
SET NOCOUNT ON;

DECLARE @NombreBase sysname = N'BD_TelefoniaAccesoriosSA';
DECLARE @MarcaTiempo varchar(24) =
    CONVERT(char(8), GETDATE(), 112) + N'_' +
    REPLACE(CONVERT(varchar(12), SYSDATETIME(), 114), N':', N'');
DECLARE @RutaRespaldo nvarchar(4000) =
    N'/var/opt/mssql/data/' + @NombreBase + N'_' + @MarcaTiempo + N'.bak';
DECLARE @ArchivoExiste int;

EXEC master.dbo.xp_fileexist @RutaRespaldo, @ArchivoExiste OUTPUT;

IF @ArchivoExiste = 1
BEGIN
    THROW 50001, 'El archivo de respaldo ya existe. Ejecute nuevamente para generar otro nombre.', 1;
END;

BACKUP DATABASE [BD_TelefoniaAccesoriosSA]
TO DISK = @RutaRespaldo
WITH COPY_ONLY, CHECKSUM, INIT, STATS = 10;

RESTORE VERIFYONLY
FROM DISK = @RutaRespaldo
WITH CHECKSUM;

SELECT @RutaRespaldo AS RutaRespaldoVerificado;
