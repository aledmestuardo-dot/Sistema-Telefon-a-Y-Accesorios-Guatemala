# 04 — CRUD relacional

**Estado: GUÍA 03 COMPLETADA por autorización explícita del estudiante (2026-10-26).** El estudiante confirmó compilación y ejecución tras la revisión y los cambios de baja lógica.

## Ajuste visual posterior — 2026-10-30

- FrmMarcas ahora usa un layout desplazable, ajusta la grilla para diez filas por página y habilita la barra vertical derecha del contenido. Se conserva la paginación y la lógica de las relaciones.
- La solución compiló correctamente en una salida temporal aislada. El estudiante todavía no ha confirmado la validación visual de este ajuste.

## Alcance

Revisar las tablas que tienen FK y que no forman parte de los flujos encabezado-detalle de la Guía 04:

| Módulo | Tabla | Referencias verificadas |
|---|---|---|
| Cardex | `dbo.Tbl_Cardex` | Inventario; Compras (nullable); Ventas (nullable) |
| Empleados | `dbo.Tbl_Empleados` | Sucursales; Usuarios |
| Equipos | `dbo.Tbl_Equipos` | Marcas |
| Inventario | `dbo.Tbl_Inventario` | Equipos; Sucursales |
| Líneas | `dbo.Tbl_Lineas` | Clientes; Planes |
| Marcas | `dbo.Tbl_Marcas` | Proveedores |
| Pagos | `dbo.Tbl_Pagos` | Facturación |
| Reclamos | `dbo.Tbl_Reclamos` | Clientes; Líneas; DetalleVenta |

Estas relaciones provienen del catálogo SQL documentado en `database/CONEXION_DATABASE.md`. Compras/DetalleCompra, Ventas/DetalleVenta y Facturación/DetalleFactura se revisan como maestro-detalle en Skill 05.

## Reglas de implementación

- Para adaptar o crear la interfaz de un formulario relacional, seguir `docs/MODELO_VISUAL_CRUD_RELACIONAL.md`, basado en el piloto Facturación / DetalleFactura. Primero limpiar la jerarquía visual existente antes de aplicar el modelo; conservar FK como listas y la lógica maestro-detalle.
- Los errores de consulta, guardado, carga FK e importación deben usar `MensajesError.Resumir` para mostrar una causa breve y un paso útil, sin exponer directamente mensajes largos de SQL Server, TCP o PostgREST.

- Conservar las cuatro capas: Entidad, Datos, Negocio y Presentación. Presentación no debe conectarse directamente a SQL Server.
- Cargar cada FK en ComboBox con `ValueMember` como clave y `DisplayMember` como descripción. Usar campos y controles ya existentes; no inventar columnas ni modificar el diseñador.
- Usar consultas parametrizadas, `try-catch` comprensible, paginación de 10 filas solo en Presentación y conservar las etiquetas de totales.
- Eliminar significa inactivar: `UPDATE ... SET Estado = @Estado` con `false`, nunca borrar físicamente una fila persistida. Mantenerla consultable con su estado inactivo.
- Actualizar los documentos solo con hechos comprobados. Atribuir al estudiante las confirmaciones de pruebas que no se observaron directamente.

## Estado comprobado en la revisión del 2026-10-26

- El estudiante confirmó que Marcas funciona completamente y que todos los CRUD ya habían sido supervisados.
- La inspección de código encontró eliminación física en el método relacional genérico y en rutas de Compras/detalles. Se cambió el CRUD relacional genérico para inactivar por `Estado`, y los detalles retirados durante una edición ahora se conservan inactivos.
- Los flujos de Compras, Ventas y Facturación inactivan encabezado y detalles dentro de una transacción. Las llamadas de Presentación fueron conectadas al flujo transaccional correspondiente y los mensajes indican inactivación.
- El examen textual de los archivos C# bajo `src/SistemaTelefonia` no encontró `DELETE FROM` después de los cambios. No se modificó el esquema ni se ejecutaron cambios de datos en SQL Server.
- Las fuentes de las capas compilaron con Roslyn y referencias de .NET Framework; Presentación produjo 61 advertencias CS0169 por campos de diseñador no usados. No equivale a ejecutar la solución desde Visual Studio ni a probar formularios.
- El cierre y la confirmación de compilación/ejecución son declaraciones explícitas del estudiante. No se registraron en esta conversación resultados SQL individualizados que demuestren cada fila con `Estado = 0`; no presentarlos como observación independiente.
