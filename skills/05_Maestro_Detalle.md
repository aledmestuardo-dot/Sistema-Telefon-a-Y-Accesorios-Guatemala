# 05 — Maestro y detalle

Para el diseño visual de las dos pantallas del flujo relacional (consulta y captura), seguir también `docs/MODELO_VISUAL_CRUD_RELACIONAL.md`. La ventana de captura reserva diez filas de detalle por página, sin filtros, y conserva las acciones transaccionales existentes. Los errores de carga o guardado usan `MensajesError.Resumir` y no muestran respuestas técnicas completas.

**Estado: GUÍA 04 COMPLETADA por autorización explícita del estudiante (2026-10-26).** El estudiante confirmó compilación y ejecución tras los ajustes maestro-detalle.

## Relaciones del sistema

- Compras: `dbo.Tbl_Compras` → `dbo.Tbl_DetalleCompra`.
- Ventas: `dbo.Tbl_Ventas` → `dbo.Tbl_DetalleVenta`.
- Facturación: `dbo.Tbl_Facturacion` → `dbo.Tbl_DetalleFactura`.

Los formularios y controles existentes deben conservarse. Un formulario WinForms puede definir controles en `.Designer.cs` o construirlos desde código; no crear un diseñador nuevo si el formulario actual funciona y el diseño está aprobado.

## Reglas de baja y edición

- Alta de encabezado y detalles se realiza como una unidad mediante transacción; si falla una parte, usar `ROLLBACK`.
- Eliminar un encabezado significa marcarlo inactivo y marcar también sus detalles inactivos dentro de la misma transacción. No borrar filas ni modificar el esquema.
- Al editar y retirar una línea ya persistida, cambiar su `Estado` a `false` en vez de ejecutar `DELETE`. Las líneas nuevas retiradas antes de guardar no generan filas.
- Mantener integridad de FK, SQL parametrizado, cuatro capas y controles existentes.

## Estado de la revisión — 2026-10-26

- Se cambiaron los métodos compartidos para inactivar encabezado/detalles con una transacción y conservar detalles retirados durante la edición.
- Compras también usa su operación transaccional para inactivar `Tbl_Compras` y `Tbl_DetalleCompra`.
- Ventas y Facturación fueron conectadas al método transaccional genérico con sus relaciones verificadas.
- Las fuentes de C# compilaron por capa con Roslyn y referencias de .NET Framework; esto no demuestra ejecución desde Visual Studio ni verificación en SQL Server.
- El cierre y la confirmación de compilación/ejecución son declaraciones explícitas del estudiante. No se recibieron resultados detallados de cada caso maestro-detalle ni consultas SQL de comprobación; no atribuir esas verificaciones específicas como observadas independientemente.
