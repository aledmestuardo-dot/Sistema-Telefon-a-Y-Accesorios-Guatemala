# 03 — CRUD simple

**Estado: GUÍA 02 COMPLETADA por instrucción explícita del estudiante (2026-10-25).** Esta guía corresponde a CRUD simple y está registrada en este archivo numerado como Skill 03.

## Ajuste visual posterior — 2026-10-30

- FrmProveedores reserva espacio para las diez filas de la página y habilita desplazamiento vertical del panel de contenido por la derecha cuando el catálogo excede el área visible. Se conserva la paginación.
- La solución compiló correctamente en una salida temporal aislada para evitar los binarios bloqueados por Visual Studio. El estudiante todavía no ha confirmado la validación visual de este ajuste.

El estudiante indicó dar por realizadas las pruebas de todos los módulos candidatos y solicitó cerrar la guía. Esta es una declaración del estudiante; no equivale a verificación técnica independiente de cada módulo.

## Alcance inicial autorizado

- Módulo: Proveedores.
- Tabla: `dbo.Tbl_Proveedores`.
- Formulario existente: `FrmProveedores`.
- Revisar y validar consulta, alta, edición, baja lógica y limpieza del formulario.
- Mantener las cuatro capas existentes y consultas parametrizadas. En Presentación, resumir los errores con `MensajesError.Resumir`; no mostrar directamente `ex.Message` ni detalles extensos del proveedor.
- No cambiar el diseñador ni el esquema de SQL Server sin autorización específica.
- No afirmar que una operación fue probada en ejecución hasta realizarla y comprobar el resultado.

## Estado comprobado al habilitar

- El código existente contiene consulta/listado y búsqueda, alta, edición, baja lógica y limpieza del formulario.
- La baja cambia `Estado` a inactivo; no elimina físicamente el registro.
- La solución compiló con 0 errores y 61 advertencias CS0169 por campos no usados.
- No se ejecutaron altas, ediciones ni bajas de datos reales en esta verificación.
- `FrmProveedores` llama a `AjustesDisenoCrud.ConfigurarTabla(dgvProveedores)`. El helper compartido crea una barra de paginación en Presentación con `RegistrosPorPagina = 10`; pagina el origen enlazado al `DataGridView` y conserva el texto de `lblRegistros`.
- Por inspección de código se comprobó la paginación; el estudiante confirmó después que consulta y paginación funcionan en ejecución.
- El estudiante confirmó en ejecución: consulta, búsqueda, paginación, limpieza; agregó el registro identificado como `2006`, modificó y eliminó lógicamente el `1006`, que permanece registrado como inactivo.
- La confirmación funcional anterior proviene del estudiante; no se observó de forma independiente en esta sesión. La compilación comprobada fue 0 errores y 61 advertencias CS0169.

## Inventario de módulos elegibles revisado — 2026-10-25

Se toma “sin FK” como “la tabla no tiene columnas FK propias”. La lista de restricciones del esquema muestra que estas tablas sí reciben referencias desde otras tablas; por eso una eliminación física puede quedar bloqueada y los formularios usan baja lógica.

| Módulo | Tabla SQL Server | Formulario | Verificación del código existente |
|---|---|---|---|
| Clientes | `dbo.Tbl_Clientes` | `FrmClientes` | Presentación y clases Datos/Negocio contienen consulta/búsqueda, alta, edición, baja lógica y limpieza. |
| Planes | `dbo.Tbl_Planes` | `FrmPlanes` | Presentación y clases Datos/Negocio contienen consulta/búsqueda, alta, edición, baja lógica y limpieza. |
| Proveedores | `dbo.Tbl_Proveedores` | `FrmProveedores` | Presentación y clases Datos/Negocio contienen consulta/búsqueda, alta, edición, baja lógica y limpieza. |
| Sucursales | `dbo.Tbl_Sucursales` | `FrmSucursales` | Presentación y clases Datos/Negocio contienen consulta/búsqueda, alta, edición, baja lógica y limpieza. |
| Usuarios | `dbo.Tbl_Usuarios` | `FrmUsuarios` | Presentación y clases Datos/Negocio contienen consulta/búsqueda, alta, edición, baja lógica y limpieza; reservar para la etapa de Login/Roles. |

Las cinco tablas tienen PK y ninguna FK de salida según el inventario verificado en `database/CONEXION_DATABASE.md`. Referencias entrantes: Clientes ← Líneas/Reclamos/Ventas; Planes ← Líneas/DetalleVenta; Proveedores ← Compras/Marcas; Sucursales ← Empleados/Inventario/Ventas; Usuarios ← Empleados. La comprobación de los módulos en esta sección es por inspección del código y compilación de solución, no una prueba funcional de cada formulario ni una operación contra SQL Server. La asignación académica concreta de cada módulo no está documentada aquí y debe confirmarse con el estudiante.

## Cierre de Guía 02

- Módulos incluidos por instrucción del estudiante: Clientes, Planes, Proveedores, Sucursales y Usuarios.
- El estudiante indicó que las pruebas se realizaron para todos y pidió cerrar la guía. No se recibieron resultados detallados módulo por módulo; por ello el cierre queda atribuido a su declaración, sin afirmar observación independiente.
- Para Proveedores sí se recibió confirmación detallada de consulta, búsqueda, paginación, limpieza, alta del registro identificado como 2006, edición y baja lógica del 1006; el 1006 permanece inactivo y conservado.
- Esta guía quedó cerrada el 2026-10-25. En ese momento aún no se había iniciado el CRUD relacional. Después, el estudiante autorizó esa etapa; su estado vigente aparece en `skills/04_CRUD_Relacional.md` y `ESTADO_DOCUMENTOS.md`. Login, roles y Git/GitHub continúan sujetos a sus etapas y autorizaciones.
