# Modelo visual para formularios CRUD simples

**Estado:** modelo de referencia tomado de `FrmUsuarios`; aplicado a `FrmSucursales` y `FrmPlanes`

**Última actualización:** 2026-10-29

**Uso:** regla obligatoria para formularios CRUD simples nuevos o que se rediseñen.

Este documento registra el diseño que debe conservarse al adaptar un formulario. Las dimensiones que dependen del ancho de ventana, del número de filas o del tamaño del área disponible se calculan en tiempo de ejecución; las cifras indicadas son píxeles lógicos de Windows Forms.

## 1. Orden vertical de la pantalla

De arriba hacia abajo, dentro del área principal:

1. Encabezado del módulo (por ejemplo, «Gestión de usuarios»), acoplado arriba del contenido.
2. Dashboard del sistema.
3. Separación vacía de **10 px**, implementada como una fila de diseño sin control visible.
4. Información del registro (formulario).
5. Barra horizontal de acciones: Nuevo, Guardar, Editar, Eliminar, Limpiar, Importar Excel, Exportar Excel e Imprimir.
6. Registros (búsqueda, opciones de visualización, grilla y paginación).

El contenido principal usa un `TableLayoutPanel` vertical para fijar este orden. La separación entre Dashboard e Información del registro es una fila de 10 px; no contiene un panel decorativo ni cambia el fondo del diseño.

## 2. Dimensiones y distribución

| Elemento | Medida / regla | Implementación actual |
|---|---:|---|
| Encabezado de módulo | Alto 108 | `pnlHeader`; título a 21 pt negrita, subtítulo a 10 pt; título alto 48 y subtítulo alto 42. Distribuirlos en dos filas independientes (por ejemplo, un `TableLayoutPanel`) para evitar que el título se monte sobre el subtítulo; no superponer controles con Dock |
| Área principal | Relleno 14 por lado | `pnlContenido.Padding = 14`; desplazamiento vertical habilitado |
| Dashboard | Mínimo 360 de alto; crece para ocupar el espacio sobrante | Fórmula: alto visible del contenido menos relleno, formulario, acciones, catálogo y separación de 10; aplica mínimo 360 |
| Dashboard → formulario | 10 | Fila vacía en `layoutBloquesUsuarios` |
| Información del registro | Alto 226; relleno interno 12 | Panel redondeado; encabezado de sección alto 40 |
| Acciones | Franja de 58; tabla de botones hasta 54 de alto | Botones centrados en una sola fila |
| Ancho máximo de acciones | 1264 | En ventanas más angostas, ocupa el ancho disponible |
| Columnas de acciones | 8 iguales | Cinco acciones CRUD y tres acciones de Excel/impresión; cada botón comparte la misma celda proporcional |
| Separación de botón | Margen 4 horizontal y 3 vertical | La altura útil queda en 48 cuando la franja tiene 54 |
| Búsqueda | Fila de 54 de alto | Lista Todos/Activos/Inactivos a la izquierda (160 px), búsqueda ocupa el espacio sobrante y Buscar usa el ancho de los botones CRUD; separación horizontal 8 |
| Opciones de visualización | Título alto 40 y fila de filtros alto 36 | Altura total calculada de 76 |
| Grilla | Encabezado 40; filas 32 | Reserva 10 filas por página, aunque la última página tenga menos registros |
| Dashboard: encabezado | Fila 32 | Título 14 pt negrita y botón Actualizar |
| Dashboard: indicadores | Fila 88 | Cuatro tarjetas del mismo ancho |
| Dashboard: detalle | Resto de la altura | En Usuarios, gráfica 52%; paneles de detalle 48% |
| Barra lateral | Ancho entre 148 y 195; fórmula `min(195, max(148, ancho cliente × 0.18))` | Identidad 142 de alto; rol 48; 18 filas iguales que ocupan el alto restante |

### Botones de acciones: orden y paleta compartida

Los ocho botones ocupan columnas iguales, centradas en una fila, hasta 1264 px de ancho. Orden y color habilitado:

| Orden | Botón | Fondo (RGB / HEX) |
|---:|---|---|
| 1 | Nuevo | `32, 142, 96` / `#208E60` |
| 2 | Guardar | `0, 112, 210` / `#0070D2` |
| 3 | Editar | `0, 130, 170` / `#0082AA` |
| 4 | Eliminar | `190, 58, 66` / `#BE3A42` |
| 5 | Limpiar | `91, 108, 126` / `#5B6C7E` |
| 6 | Importar Excel | `112, 83, 174` / `#7053AE` |
| 7 | Exportar Excel | `0, 132, 128` / `#008480` |
| 8 | Imprimir | `44, 78, 113` / `#2C4E71` |

Los habilitados usan texto blanco, Segoe UI negrita de 9 pt y cursor de mano. Los deshabilitados usan fondo `#55616C`, texto `#BEC6CD` y cursor normal. Mantener hover/pressed de la acción, bordes sin trazo y radio 8 px. El margen por botón es 4 px horizontal y 3 px vertical; la franja tiene 58 px y la tabla de botones hasta 54 px de alto. En ventanas menores de 950 px la fuente baja a 8 pt.

La aplicación del tema no debe depender de una lista de nombres de formulario. Después de crear los controles de un formulario nuevo, llamar `TemaVisualCrud.Aplicar(this)`; el tema aplica la paleta común y los botones reconocidos por nombre. Llamar `TemaVisualCrud.ActualizarBotonesModeloCrud(this)` cada vez que cambie el estado habilitado/deshabilitado. Los formularios nuevos deben conservar los nombres `btnNuevo`, `btnGuardar`, `btnEditar`, `btnEliminar`, `btnLimpiar`, `btnImportarExcel`, `btnExportarExcel`, `btnImprimir`, `btnBuscar`, `btnPaginaAnterior` y `btnPaginaSiguiente` para recibir automáticamente el estilo.

En la pantalla de Planes el ancho de barra se calcula con la misma fórmula que Usuarios. El bloque superior muestra marca, rol y descripción del usuario; las 18 filas iguales ordenan Volver al menú, los módulos y Cerrar sesión al pie.

El alto del catálogo se calcula como `Padding.Vertical + título + búsqueda + opciones + altoGrilla + altoPaginacion`. `altoPaginacion` es el alto de `pnlPaginacion` completa (incluye conteo, anterior, número de página y siguiente); no se suma `lblRegistros` por separado porque el helper lo reubica dentro de ese pie. La grilla reserva diez filas por página aunque el resultado tenga menos registros. Revisar tanto la última página parcial como una lista de varias páginas: el conteo y los cuatro elementos de paginación deben quedar visibles sin recortarse.

### Acciones

El diseñador declara inicialmente los cinco botones CRUD a 174 × 48. En ejecución, el helper compartido distribuye los ocho botones de manera proporcional en una fila, hasta 1264 px de ancho. La fuente es Segoe UI negrita de 9 pt; baja a 8 pt en ventanas cuyo contenido tenga menos de 950 px de ancho.

### Botones de acciones

- Nuevo, Guardar, Editar, Eliminar, Limpiar, Importar Excel, Exportar Excel, Imprimir, Buscar, Anterior y Siguiente usan esquinas redondeadas con radio 8 px.
- Mantener los colores normales específicos de cada acción, el texto blanco, la tipografía Segoe UI negrita de 9 pt y el cursor de mano cuando estén habilitados.
- Hover y presión usan sus colores de realce; los botones deshabilitados usan fondo `#55616C`, texto `#BEC6CD` y cursor normal.
- Buscar y los botones de paginación usan los mismos estados y tratamiento visual que las acciones principales; conservan las dimensiones definidas por su contenedor.

## 3. Paleta y texto

### Paleta general oscura

| Uso | Color |
|---|---|
| Fondo de ventana | `#03162B` |
| Navegación lateral | `#02192F` |
| Superficies de panel | `#072342` |
| Franja/encabezado | `#032B53` |
| Texto principal | `#EBF2F9` |
| Texto secundario | `#AABED2` |
| Fondo de campos | `#E1ECF6` |
| Texto de campos | `#072342` |
| Acento principal | `#0070D2` |
| Borde de tabla | `#14466E` |
| Fila de grilla | `#072342` |
| Fila alterna | `#0A2C4E` |
| Selección de grilla | `#114B77` |

### Dashboard

| Uso | Color |
|---|---|
| Fondo del Dashboard | `#08243A` |
| Tarjetas y paneles interiores | `#0B3558` |
| Acento de gráfica (serie principal) | `#00D9FF` |
| Texto secundario | `#A9BAC8` |
| Serie secundaria, cuando el indicador la requiere | `#FFB547` |
| Botón Actualizar | `#0B6E91` |
| Texto | `#FFFFFF` |

El contenedor del Dashboard tiene esquinas de radio 12. La grilla usa radio 10. Los paneles de encabezado, información del registro y registros usan radio 12.

### Tipografía

- Familia principal: Segoe UI.
- Título de ventana: 21 pt, negrita.
- Títulos de Información, Registros y Opciones de visualización: 14 pt, negrita.
- Subtítulo: 10 pt.
- Etiquetas de campos: 9 pt, negrita.
- Campos y celdas: 9.5 pt aproximadamente.
- Filtros y casillas: 9–9.5 pt.
- Encabezado de grilla: 9.5 pt, negrita.
- Texto de filas: 9.5 pt.
- Dashboard: título 14 pt; valores de indicadores 18 pt; encabezados interiores 9 pt.

## 4. Formulario de registro

- Mantener un panel de información con su título a la izquierda y campos en dos columnas cuando la cantidad de campos lo permita.
- Usar etiquetas blancas, legibles y en negrita; campos claros con texto oscuro.
- Conservar la lógica de habilitación de campos por los modos Nuevo, Editar y lectura.
- Los controles de captura —incluidos PK de solo lectura y campos FK como listas— conservan sus nombres y lógica, usan fondo claro y esquinas redondeadas de radio 7 px. No habilitar una PK solo para mostrar su estilo.
- Toda lista de clave foránea muestra el código y un nombre o descripción reconocible del registro relacionado, además de la opción inicial «- Seleccione -». Usar `ComboFK.Cargar` o una lista equivalente con esos mismos tres elementos; conservar `ValueMember` como la clave numérica y no guardar el texto de presentación.
- Al presionar Enter, validar el campo actual cuando corresponda y mover el foco al siguiente campo visible y habilitado siguiendo el orden definido por ese formulario. En ComboBox con la lista desplegada, Enter acepta la selección y no avanza todavía. Enter no ejecuta Guardar; el último campo conserva el foco. El orden y las validaciones se determinan módulo por módulo según sus campos.
- Usar cian `#00D9FF` solo en controles que estén resaltados como activos/editables; restaurar el color al cambiar de modo.
- Los estados Activo e Inactivo mantienen texto blanco también cuando están deshabilitados.
- No cambiar validaciones, eventos, consultas, nombres públicos de controles ni operaciones de negocio al aplicar el estilo.

## 5. Búsqueda, filtros y grilla

- La búsqueda y el filtro de estado van en una sola línea, en este orden de izquierda a derecha: lista Todos/Activos/Inactivos, caja de búsqueda y botón Buscar alineado verticalmente. Nunca colocar la lista al extremo derecho.
- «Activar todos los filtros» queda al extremo izquierdo de la fila de opciones.
- Las casillas de columnas permanecen en una sola línea (`WrapContents = false`). La altura debe admitir la fila completa y sus márgenes. No mostrar barra vertical, flechas de desplazamiento ni el cuadro de esquina de una barra junto a «Seleccionar registro». Si las opciones exceden el ancho, permitir únicamente desplazamiento horizontal o recalcular el espacio; no activar el desplazamiento automático bidireccional de `FlowLayoutPanel` que agrega controles verticales.
- «Seleccionar registro» conserva su alineación al extremo derecho.
- «Activar todos los filtros» y «Seleccionar registro» reservan el ancho según su texto medido con la fuente actual y sus márgenes; no se asignan anchos fijos que puedan recortarlos.
- La selección de columnas conserva la visibilidad individual que usa el formulario.
- Grilla redondeada, encabezado azul de acento con texto blanco, filas oscuras alternadas y separadores discretos.
- Paginación: 10 registros por página; la grilla conserva la altura visual para 10 filas en todas las páginas.
- Reservar altura para el encabezado y las diez filas de la grilla, incluso en un catálogo recién creado o con pocos resultados. Calcular la altura del catálogo con el pie de paginación completo para que el `DataGridView` no colapse ni quede cubierto por otros bloques.
- Los conteos de resultados/activos/inactivos y los botones Anterior/Siguiente se conservan bajo la grilla.
- Los botones Anterior/Siguiente usan el mismo color, texto, tipografía, cursor y estado habilitado/deshabilitado que las acciones. Cada uno mide el ancho de los botones principales, tiene su alto, y queda separado 8 px; Siguiente se alinea a la derecha, el número de página va antes de él y Anterior queda a la izquierda del número. El pie mide el alto del botón más 16 px.
- El conteo queda alineado a la izquierda en Segoe UI 9.5 pt negrita y blanco; el texto de página va centrado en Segoe UI 9.5 pt blanco.
- Los controles Activo/Inactivo usan `RadioButtonTextoBlanco`: texto blanco a 9.5 pt y círculo cian de 14 px; el radio conserva lectura con el formulario deshabilitado.
- La ecuación del alto del catálogo cuenta el panel de paginación entero, incluso cuando el helper mueve dentro de él `lblRegistros`; no contar dos veces el control ni omitir el panel.

## 6. Barra lateral

- Fondo `#061D30`; texto blanco; acentos e iconos cian `#00D9FF`.
- Ancho adaptable: `min(195, max(148, ancho cliente × 0.18))` px.
- Orden: bloque Sistema de Telefonía y Accesorios, bloque de rol, Volver al menú principal, 16 módulos de navegación y Cerrar sesión al final.
- Bloque de identidad: ancho completo de la barra y 142 px de alto; icono de marca FontAwesome `IconChar.MobileAlt`, cian `#00CBFF`, de 42 × 42 px con margen superior de 8 px; texto de marca dentro de 6 px laterales, desde y=52, con 82 px de alto. Usar el mismo glifo de Usuarios para mantener la identidad consistente.
- Bloque de rol: 48 px de alto, dividido en dos etiquetas centradas de 24 px: rol y descripción.
- Las 18 filas (Volver, 16 módulos y Cerrar sesión) ocupan el ancho interno completo y dividen en partes iguales el alto restante después de identidad y rol.
- Implementar las opciones como `FontAwesome.Sharp.IconButton`, no como una etiqueta con el icono en otra posición. Usar `ImageBeforeText`, icono a la izquierda, texto en la misma línea y alineado a la izquierda, fuente Segoe UI 9.5 pt, relleno izquierdo de 12 px, margen cero.
- Volver al menú y Cerrar sesión conservan el mismo alto de fila; el primero usa icono de inicio de 20 px, fondo `#08243A` y borde `#526779`; Cerrar sesión usa icono de salida de 20 px, fondo `#A83F48` y borde `#D56A70`.
- Estado normal: fondo `#061D30`, texto blanco, icono cian. Hover/seleccionado: fondo `#0B3558`, texto e icono cian.
- `TemaVisualCrud.Aplicar` excluye controles laterales por ascendencia bajo `pnlMenu`, no comparando el nombre del padre inmediato con un nombre fijo de `TableLayoutPanel`. Aplicar después el estado específico de activo, Volver y Cerrar sesión. Confirmar que el tema genérico no cambie sus fondos.
- Borde exterior de la barra: 1 px `#34495E`; sin esquinas redondeadas en los botones laterales.
- Conservar los identificadores `btnVolverMenu`, `btnUsuarios`, `btnSucursales`, `btnEmpleados`, `btnClientes`, `btnPlanes`, `btnLineas`, `btnProveedores`, `btnMarcas`, `btnEquipos`, `btnCompras`, `btnInventario`, `btnVentas`, `btnFacturacion`, `btnPagos`, `btnCardex`, `btnReclamos` y `btnCerrarSesion`, así como `lblRol` y `lblDescripcionRol`.

Las filas en porcentaje del área disponible son la regla vigente, aplicada también a Planes para conservar la misma distribución y posición de las 18 opciones.

## 7. Dashboard y datos

La composición es reutilizable, pero los indicadores y el contenido deben pertenecer al módulo. El Dashboard actual de Usuarios muestra usuarios registrados, cuentas activas, cuentas inactivas y administradores; incluye nuevos usuarios por mes, tipos de cuenta y accesos recientes. No debe conservar los indicadores antiguos de ventas/compras en módulos ajenos.

Al reutilizar la presentación en otro CRUD, conservar las cuatro tarjetas, la gráfica y los dos paneles en su lugar. Adaptar etiquetas, métricas, agrupaciones y consultas a propiedades reales de la entidad; no introducir valores de muestra ni conectar Presentación directamente a SQL Server. Conservar el espaciado de Usuarios: dashboard con padding 12; tarjetas con margen superior/inferior 5 y separación horizontal 8; detalle con margen superior 4, gráfica/detalle a 52/48, espacio lateral 8 y separación vertical de 5 entre los paneles derechos; panel de gráfica con padding 7/5 y paneles de contenido con 9/6. No comprimir las regiones ni poner tarjetas/paneles pegados.

## 8. Adaptación a otro CRUD simple

1. **Limpiar primero la superficie visual del formulario.** Inspeccionar y registrar sus controles, campos y eventos; quitar del diseñador la distribución anterior, paneles y controles visuales que formen parte del diseño que se reemplaza. Dejar el formulario sin la interfaz anterior y aplicar después el modelo completo, sin montar el nuevo diseño encima del viejo.
2. Al limpiar, preservar las capas Entidad, Datos, Negocio y Presentación, las reglas de negocio y validación, consultas, eventos y comportamiento CRUD. Mantener los nombres de controles usados por el código o actualizar todas sus referencias antes de compilar.
3. Mapear los campos y columnas del módulo destino sin cambiar el orden general ni las medidas compartidas.
4. Conservar búsqueda, alta, edición, limpieza, paginación e inactivación lógica existentes.
5. Respetar columnas requeridas, PK y relaciones propias del módulo; los campos no aplicables pueden variar sin alterar el tema visual.
6. Recalcular la altura de la ficha y el catálogo cuando el formulario tenga otra cantidad de campos o columnas.
7. Usar la paleta y botones compartidos mediante los nombres estándar. No añadir excepciones por nombre de formulario que dejen fuera una pantalla nueva.
8. Compilar en la misma carpeta de salida/configuración que ejecuta el usuario; una salida aislada de verificación no actualiza el ejecutable que él ve.
9. Abrir ese ejecutable, entrar al formulario y comparar en ejecución con Usuarios: dashboard y sus separaciones, pie completo del catálogo, acciones habilitadas/deshabilitadas y barra lateral antes de declarar aplicada la plantilla. Confirmar el pie en una lista corta y en varias páginas.

El Dashboard de Usuarios es la implementación de referencia para la composición visual. Sus cifras y consultas pertenecen al dominio del sistema; no deben copiarse literalmente en otro módulo sin adaptar su origen de datos.

## 10. Regla transversal para campos de clave foránea

Antes de modificar o crear un formulario, contrastar sus campos con el esquema de SQL Server y Supabase. Toda FK de negocio que aparezca en una ficha se presenta como lista desplegable, nunca como `NumericUpDown` o texto editable. La lista inicia en `- Seleccione -`, presenta el código a la izquierda seguido del nombre o una descripción útil cuando exista, y conserva la clave numérica como `ValueMember`. Los formularios usan `ComboFK.Cargar` o un enlace equivalente que mantenga esas mismas reglas.

Las FK opcionales conservan la opción inicial como valor nulo al guardar. Las FK obligatorias se validan y enfocan si continúan en `- Seleccione -`. Cuando la relación se asigna automáticamente por ser una línea hija de su encabezado, se presenta también como lista de solo lectura: muestra el código y la descripción del encabezado cuando ya existe; antes de guardar permanece en `- Seleccione -` y el guardado asigna la clave del encabezado. Nunca habilitar esa FK para que se cambie por otra relación.

Inventario de FK de negocio para auditar formularios (26 relaciones en 14 tablas):

| Tabla | Campo FK | Referencia |
|---|---|---|
| `Tbl_Empleados` | `CodigoSucursal` | `Tbl_Sucursales.CodigoSucursal` |
| `Tbl_Empleados` | `CodigoUsuario` | `Tbl_Usuarios.CodigoUsuario` |
| `Tbl_Lineas` | `CodigoCliente` | `Tbl_Clientes.CodigoCliente` |
| `Tbl_Lineas` | `CodigoPlan` | `Tbl_Planes.CodigoPlan` |
| `Tbl_Compras` | `CodigoProveedor` | `Tbl_Proveedores.CodigoProveedor` |
| `Tbl_Equipos` | `CodigoMarca` | `Tbl_Marcas.CodigoMarca` |
| `Tbl_Marcas` | `CodigoProveedor` | `Tbl_Proveedores.CodigoProveedor` |
| `Tbl_DetalleCompra` | `CodigoCompra` | `Tbl_Compras.CodigoCompra` |
| `Tbl_DetalleCompra` | `CodigoEquipo` | `Tbl_Equipos.CodigoEquipo` |
| `Tbl_Inventario` | `CodigoEquipo` | `Tbl_Equipos.CodigoEquipo` |
| `Tbl_Inventario` | `CodigoSucursal` | `Tbl_Sucursales.CodigoSucursal` |
| `Tbl_Ventas` | `CodigoSucursal` | `Tbl_Sucursales.CodigoSucursal` |
| `Tbl_Ventas` | `CodigoCliente` | `Tbl_Clientes.CodigoCliente` |
| `Tbl_Ventas` | `CodigoEmpleado` | `Tbl_Empleados.CodigoEmpleado` |
| `Tbl_DetalleVenta` | `CodigoVenta` | `Tbl_Ventas.CodigoVenta` |
| `Tbl_DetalleVenta` | `CodigoEquipo` | `Tbl_Equipos.CodigoEquipo` |
| `Tbl_DetalleVenta` | `CodigoPlan` *(opcional)* | `Tbl_Planes.CodigoPlan` |
| `Tbl_Facturacion` | `CodigoVenta` | `Tbl_Ventas.CodigoVenta` |
| `Tbl_DetalleFactura` | `CodigoFactura` | `Tbl_Facturacion.CodigoFactura` |
| `Tbl_Pagos` | `CodigoFactura` | `Tbl_Facturacion.CodigoFactura` |
| `Tbl_Cardex` | `CodigoInventario` | `Tbl_Inventario.CodigoInventario` |
| `Tbl_Cardex` | `CodigoCompra` *(opcional)* | `Tbl_Compras.CodigoCompra` |
| `Tbl_Cardex` | `CodigoVenta` *(opcional)* | `Tbl_Ventas.CodigoVenta` |
| `Tbl_Reclamos` | `CodigoCliente` | `Tbl_Clientes.CodigoCliente` |
| `Tbl_Reclamos` | `CodigoLinea` | `Tbl_Lineas.CodigoLinea` |
| `Tbl_Reclamos` | `CodigoDetalleVenta` | `Tbl_DetalleVenta.CodigoDetalleVenta` |

La relación Supabase `Tbl_Usuarios.AuthUserId -> auth.users.id` pertenece al sistema de autenticación y no es un selector de catálogo de negocio.

En la revisión del 2026-10-29 se comprobó que los formularios CRUD relacionales ya cargan listas FK con código, descripción y marcador; `FrmCompraCompleta` usa una lista equivalente para Proveedor y Equipo. Se cambió su `CodigoCompra` de detalle, que antes era una caja de texto con asignación automática, por una lista de solo lectura para mostrar la compra relacionada sin permitir alterarla. `FrmMaestroDetalle` también representa las FK de líneas hijas mediante listas de solo lectura y añade `CodigoVenta` al catálogo genérico usado por Facturación. La compilación de la solución y la comprobación visual en ejecución deben quedar registradas por separado; esta auditoría estática no confirma la presentación en ejecución ni el comportamiento de guardado de cada módulo.

## 9. Aplicación a Sucursales y estado de Planes

El 2026-10-28 se adaptó `FrmSucursales` a esta plantilla. Se mantuvieron sus operaciones y validaciones existentes; el Dashboard presenta conteos de sucursales, aperturas recientes, aperturas por mes y alertas por datos de contacto faltantes. El formulario compiló con MSBuild en una carpeta de salida aislada. La revisión visual en ejecución aún no se ha realizado.

El 2026-10-29 se reconstruyó `FrmPlanes` desde el lienzo temporalmente vacío siguiendo la estructura del modelo. Se conservaron Código, Nombre, Descripción, Costo mensual, Datos (MB), Minutos incluidos, Vigencia (meses) y Estado, junto con las reglas de validación y las operaciones existentes. El Dashboard usa conteos de planes activos/inactivos, costo promedio, costo mensual por plan, distribución por vigencia y beneficios incluidos, consultados mediante `PlanesNegocio`. El catálogo reserva diez filas y usa búsqueda, filtro común y paginación. La solución compiló con MSBuild; no se confirmó la apariencia en ejecución.

La corrección visual posterior aplica a Planes los ocho botones de igual ancho, colores por acción, estados, tipografía y espaciado de Usuarios. La barra lateral usa los mismos `IconButton`, relación icono-texto, alineación izquierda, colores de estado, ancho adaptable y distribución de 18 filas. Los botones de paginación y los conteos usan la fuente, colores y estados de Usuarios; Estado utiliza `RadioButtonTextoBlanco`. Las casillas «Activar todos los filtros» y «Seleccionar registro» toman su ancho del texto medido y la fuente actual. El estilo de acciones se generalizó a `ActualizarBotonesModeloCrud`, sin una lista de formularios permitidos.

El 2026-10-29 se adaptó `FrmLineas` al orden visual del modelo: dashboard, separación, ficha, ocho acciones y catálogo. Se reconstruyó la navegación lateral con filas de iconos y distribución adaptable. Los selectores de Cliente y Plan continúan siendo listas FK cargadas desde sus catálogos; Tipo de línea también conserva su lista. El dashboard usa los registros de líneas para sus conteos y agrupaciones. La revisión posterior corrigió la prioridad de la paleta de navegación después de aplicar el tema común e incorporó resaltado y avance por Enter en el orden de captura, con validación para los campos obligatorios. Una revisión de las capturas posteriores identificó dos regresiones de composición: el alto del catálogo no reservaba `pnlPaginacion` completo y las tarjetas/paneles del dashboard no respetaban los márgenes de Usuarios. Se corrigieron ambas medidas; la exclusión de navegación del tema genérico ahora se basa en su ascendencia bajo `pnlMenu`. Una compilación aislada no actualizaba el ejecutable normal; la validación final debe abrir la salida estándar configurada y confirmar la pantalla en ejecución. La autenticación requerida impidió completar esa revisión visual automatizada en esta sesión.

El 2026-10-29 se actualizaron `FrmPagos` y `FrmReclamos` con reglas observadas al comparar las capturas con Usuarios: título y subtítulo en filas independientes; barra de marca con el icono FontAwesome MobileAlt, tamaño y color de Usuarios; «Opciones de visualización» con la misma tipografía y tamaño que el título del catálogo; casilla «Seleccionar registro» alineada a la derecha sin controles de desplazamiento visibles al lado; y fila de búsqueda en orden lista de estado a la izquierda, caja y Buscar. En Reclamos se conservan los combos de campos FK, el resaltado de captura y el avance validado con Enter; el contador muestra resultados, activos e inactivos. Ambos formularios compilaron con MSBuild después de los cambios. La inspección visual en ejecución de estos cambios queda pendiente; la captura del estudiante motivó el último ajuste de búsqueda y flechas en Reclamos.

El 2026-10-29 se corrigió `FrmEmpleados`: la carga posterior del catálogo reemplazaba las listas FK con enlaces directos que mostraban solo el nombre y no incluían el marcador; ahora todas usan código, descripción y «- Seleccione -», conservando el valor numérico. El catálogo se ordenó en filas de diseño y reserva el espacio de encabezado más diez filas para evitar que la grilla colapse cuando hay pocos registros. La solución compiló con MSBuild. Falta revisar en ejecución la lista y la fila recién agregada con el origen elegido por el estudiante.

## 11. Mensajes de error

- Los mensajes de error visibles deben indicar en una o dos frases qué ocurrió y el siguiente paso útil. No presentar al usuario cadenas extensas del proveedor SQL, TCP, HTTP ni trazas internas.
- En Presentación, usar `MensajesError.Resumir(Exception)` o `MensajesError.Resumir(string)` en capturas y avisos de error. La utilidad traduce errores frecuentes de conexión, credenciales, sesión/permisos, duplicados, relaciones inválidas, campos requeridos, archivos y tiempos de espera; títulos de los diálogos conservan la acción que falló.
- Conservar las validaciones cortas que ya nombran el campo y la corrección requerida. No reemplazarlas por un mensaje genérico. Mantener la excepción original y su cadena interna para que la capa superior pueda clasificarla; no concatenar `ex.Message` sin resumir en ventanas o etiquetas de estado.
- Si se agregan nuevas causas comunes, ampliar el mapeo compartido en `MensajesError`; no implementar resúmenes distintos por formulario.
- El 2026-10-29 se conectaron 131 puntos de captura de excepción con el resumidor y se acortaron los mensajes de prueba de conexión de SQL Server y Supabase. Presentación compiló en Debug y Release. La compilación no ejercitó cada mensaje en ejecución.
