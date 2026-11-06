# 02 — Conexión a SQL Server

**Estado:** COMPLETADA. La conexión y las consultas de metadatos se verificaron el 2026-10-25; la prueba inicial de conexión desde Visual Studio también consta en el registro del proyecto.  
**Prerrequisito satisfecho:** el estudiante confirmó el árbol del proyecto y proporcionó la consulta de verificación.

## Propósito

Explicar y registrar la preparación de una conexión desde C# .NET Framework con ADO.NET. Los resultados históricos se documentan en esta guía y en `ESTADO_DOCUMENTOS.md`.

## Prerrequisitos aplicados

1. El estudiante confirmó el árbol real del proyecto.
2. Se inspeccionaron el proyecto de Datos, el .NET Framework, las referencias y la configuración existente, conservando los diseños.
3. El estudiante proporcionó la configuración de SQL Server; sus secretos se mantuvieron fuera de la documentación versionada.
4. Se verificó la conexión y se consultaron metadatos de solo lectura. La prueba inicial también se ejecutó desde Visual Studio, según el registro de `ESTADO_DOCUMENTOS.md`.
5. El respaldo fue autorizado, ejecutado y verificado en una etapa posterior. Su resultado está documentado en `database/CONEXION_DATABASE.md`; la instrucción inicial de mantener el script sin ejecutar ya no describe el estado actual.

## Capas previstas

- **Entidad:** clases sencillas para transportar los datos.
- **Datos:** apertura y cierre de `SqlConnection`, comandos SQL y parámetros.
- **Negocio:** reglas y validaciones de la aplicación.
- **Presentación:** interfaz Windows Forms; no debe contener credenciales ni SQL de acceso directo.

## Patrón de conexión (referencia didáctica, no ejecutada)

Usar la configuración aprobada del proyecto y el proveedor compatible con el .NET Framework detectado. No copiar credenciales reales a código fuente ni al repositorio. El patrón general es:

```csharp
using (SqlConnection conexion = new SqlConnection(cadenaConexion))
{
    try
    {
        conexion.Open();
        // Ejecutar aquí solo una operación autorizada y parametrizada.
    }
    catch (SqlException ex)
    {
        // Registrar o comunicar el error sin exponer secretos.
        throw;
    }
}
```

`using` libera la conexión incluso cuando ocurre un error. Las consultas deben parametrizar valores; no concatenar entradas del usuario. Manejar las excepciones en la capa apropiada y comunicar mensajes comprensibles sin ocultar el error técnico necesario para diagnosticarlo.

## No registrar como hecho hasta verificar

No afirmar que hay conexión, que existe una base de datos, que una consulta funcionó o que se hizo un respaldo sin una ejecución autorizada y evidencia observable. Al avanzar, anotar fecha, instancia/base aprobadas sin secretos, operación realizada y resultado comprobado.

## Verificación realizada

El 2026-10-25, desde el contenedor local de SQL Server, se ejecutaron las consultas de solo lectura `SELECT DB_NAME()` y el listado `TOP (5)` de tablas base. La base actual devuelta fue `BD_TelefoniaAccesoriosSA`. Las cinco tablas devueltas, ordenadas por nombre, fueron `dbo.Tbl_Cardex`, `dbo.Tbl_Clientes`, `dbo.Tbl_Compras`, `dbo.Tbl_DetalleCompra` y `dbo.Tbl_DetalleFactura`. No se modificaron datos ni esquema. Este resultado confirma que la conexión usada para esa consulta funcionó; no prueba permisos para otras operaciones.
