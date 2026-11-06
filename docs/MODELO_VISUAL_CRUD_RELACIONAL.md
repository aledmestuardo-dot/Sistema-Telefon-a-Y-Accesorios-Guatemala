# Modelo visual para CRUD relacional

**Estado:** COMPLETADO (100% según confirmación del estudiante, 2026-10-29).

**Última actualización:** 2026-10-29

**Uso:** regla de referencia para CRUD con claves foráneas y formularios relacionales nuevos o rediseñados. En un maestro-detalle, usarlo junto con `skills/05_Maestro_Detalle.md` y conservar sus operaciones transaccionales.

## 1. Preparar y ordenar el formulario

Antes de aplicar este modelo, limpiar primero el formulario visual existente: identificar sus bloques, retirar del árbol visual los controles/paneles que serán reemplazados y después construir la nueva jerarquía. No borrar campos de diseñador, nombres, eventos, consultas ni lógica de negocio. Conservar los controles y sus enlaces funcionales; reubicarlos o envolverlos en nuevos contenedores. Esto evita que controles antiguos se dupliquen, queden superpuestos o conserven posiciones incompatibles con el nuevo layout.

La pantalla de consulta sigue la secuencia: encabezado, dashboard, separación de 10 px, formulario/captura relacional, acciones y catálogo. Si el módulo usa una ventana de captura maestro-detalle separada, mantiene su propia secuencia de encabezado, secciones maestro y detalle, grilla de detalle e instrumentos de acción.

## 2. Encabezado, dashboard y distribución

- El encabezado usa dos filas independientes: título de 21 pt negrita y subtítulo de 10 pt, sin superposición ni recorte.
- El dashboard toma la composición y paleta de Usuarios: título y Actualizar; botones contextuales; cuatro tarjetas iguales; gráfica en aproximadamente 52% del ancho y dos resúmenes apilados en 48%.
- Dashboard de referencia: fondo `#08243A`, superficies `#0B3558`, texto blanco, texto secundario `#A9BAC8`, acento de gráfica `#00D9FF`; panel exterior radio 12 px, tarjetas y subpaneles radio 10 px.
- Usar métricas y resúmenes reales del módulo. Facturación muestra cantidad total/activa/inactiva, monto activo, facturas por mes, distribución por estado y facturas recientes. DetalleFactura muestra conceptos total/activos/inactivos, subtotal activo, conceptos por periodo, conceptos por factura y detalles recientes. No introducir datos ficticios.
- Mantener el dashboard legible y separado de la grilla. En ventanas pequeñas, permitir desplazamiento vertical del contenido antes de comprimir o recortar controles. El piloto reserva mínimo 380 px para el dashboard, 10 px de separación y 320 px para el catálogo; Facturación crece desde el área disponible y DetalleFactura calcula también las alturas del formulario y la barra de acciones.
- Las acciones contextuales de Facturación «Agregar nueva factura» y «Ver / editar seleccionada» se muestran completas, en una fila alineada a la derecha; cada botón mide 232 × 36 px, usa Segoe UI 9 pt negrita, texto blanco, sin elipsis y radio 8 px. La segunda acción permanece deshabilitada hasta seleccionar una factura.
- Importar Excel, Exportar Excel e Imprimir se ubican junto a «Agregar nueva factura». Conservan el manejo común de Usuarios, la paleta aprobada y botones de 158 × 48 px, en Segoe UI 9 pt negrita, con radio 8 px. Exportar e Imprimir operan sobre la grilla actual. Importar abre una vista previa y registra cada factura con sus líneas dentro de una operación maestro-detalle.
- El libro de importación de Facturación usa una fila por línea, con columnas `NumeroFactura`, `CodigoVenta`, `FechaEmision`, `NombreCliente`, `Descripcion`, `Cantidad` y `PrecioUnitario`; `Nit` y `Descuento` son opcionales. Repetir los valores de encabezado en cada línea de la misma factura. El guardado exige una venta disponible y no permite encabezados sin detalles; subtotal, IVA (12%) y total se calculan al importar. La importación no crea facturas aisladas.

## 3. Barra lateral

Reutilizar `AjustesDisenoCrud.ConstruirBarraLateralModelo(Form)` para evitar implementaciones distintas por formulario.

- Acoplada a la izquierda, ancho `min(195, max(148, ancho cliente × 0.18))`.
- Orden: identidad de Sistema de Telefonía y Accesorios (142 px), rol/usuario (48 px), Volver al menú principal, 16 módulos y Cerrar sesión al final.
- Identidad con el icono MobileAlt cian y la marca de Usuarios; bloque de rol en dos filas centradas.
- Dieciocho filas de navegación de la misma altura: iconos turquesa a la izquierda, texto blanco y fondo oscuro. El módulo actual usa fondo `#0B3558` y texto/icono cian. Volver usa icono de inicio y superficie `#08243A`; cerrar sesión usa icono de salida y fondo rojo `#A83F48`.
- Mantener las rutas ya declaradas en los controles del formulario y excluir entradas que no son módulos de navegación, como DetalleFactura.

## 4. Relaciones y campos

- Toda FK editable se elige mediante una lista `ComboBox`, con la opción inicial `- Seleccione -`, el código al inicio del texto y el nombre/descripción después cuando exista. Mantener la clave numérica como `ValueMember`; guardar la clave, nunca la cadena mostrada. Usar `ComboFK.Cargar` o helper equivalente.
- La lista debe presentar registros vigentes y pertinentes al flujo. En Facturación, Código de venta enumera ventas activas sin factura y muestra código junto con número de venta/cliente. Si seleccionar la FK rellena datos derivados (como el total), conservar el comportamiento existente.
- En la factura, el cliente se identifica desde la venta seleccionada. El nombre y NIT deben provenir del cliente asociado, para evitar que se escriban datos que no correspondan a esa venta. Si el formulario permite corregir datos de facturación guardados como copia en el encabezado, conservar la distinción entre esa copia y el registro maestro de Clientes.
- FK opcionales convierten `- Seleccione -` a `NULL` al guardar. FK obligatorias deben validarse antes de la operación. En DetalleFactura, el Código de factura se conserva como vínculo de solo lectura cuando lo asigna el maestro.
- Los campos mantienen resaltado, navegación por Enter, validaciones, valores, eventos y estados actuales del módulo. No sustituir controles funcionales por capturas visuales ni cambiar reglas del negocio al aplicar el estilo.
- Los controles de texto, número, fecha y listas usan esquinas redondeadas con radio 7 px; mantener el indicador visual de control activo/editable que ya usa el CRUD simple.

### Ventana separada de captura maestro-detalle

- La ventana de captura se considera la segunda pantalla del CRUD relacional. Mantiene el mismo encabezado, colores y campos claros; las etiquetas se dimensionan con el rótulo más ancho de cada bloque y las cuatro columnas de captura usan anchos compartidos para que los campos queden uniformes. No truncar rótulos como «Código detalle factura».
- Orden de foco y resaltado: recorrer campos habilitados del encabezado por filas, izquierda a derecha y arriba abajo; después recorrer detalle con el mismo orden. PK, campos calculados y FK de detalle asignada por el encabezado quedan fuera del recorrido. Enter acepta una selección si la lista desplegable está abierta; cuando se cierra, avanza al siguiente campo; nunca ejecuta Guardar ni agrega el detalle por sí solo.
- El detalle usa una grilla sin filtros, reserva encabezado y diez filas de 29 px (326 px en total) en todas las páginas y pone el conteo a la izquierda con Anterior, número de página y Siguiente a la derecha. Mantener diez registros por página.
- Los cinco botones CRUD se centran en la ventana y usan 158 × 48 px, el orden y colores normales del modelo simple; al habilitar/deshabilitar, actualizar también el color y cursor correspondientes.

## 5. Acciones y botones

- Conservar orden, paleta y comportamiento compartidos del [Modelo visual CRUD simple](MODELO_VISUAL_CRUD_SIMPLE.md): Nuevo verde `#208E60`, Guardar azul `#0070D2`, Editar teal `#0082AA`, Eliminar rojo `#BE3A42`, Limpiar gris azulado `#5B6C7E`, Importar morado `#7053AE`, Exportar verde azulado `#008480`, Imprimir azul grisáceo `#2C4E71`.
- En la barra principal, distribuir los ocho botones en partes iguales dentro de un máximo de 1264 px; en ventanas más estrechas usar el ancho disponible. Segoe UI 9 pt negrita, texto blanco, hover/presión por acción, estados deshabilitados según el modelo simple y radio 8 px.
- Botones de dashboard, contextual, actualización, búsqueda y paginación deben mantener texto legible, tamaño suficiente para su rótulo, estados habilitado/deshabilitado correctos y esquinas redondeadas. No añadir indicadores ni flechas decorativas que no existan en el patrón aprobado.

## 6. Catálogo, grilla y paginación

- Conservar el catálogo CRUD simple: búsqueda con lista Todos/Activos/Inactivos a la izquierda, cuadro de búsqueda y Buscar a la derecha; opciones de columnas sin barra vertical ni cuadro de esquina; «Activar todos los filtros» a la izquierda y «Seleccionar registro» a la derecha; conteos y paginación bajo la grilla.
- La grilla relacional reserva encabezado y diez filas por página incluso si está vacía, presenta PK/FK con sus rótulos y datos correspondientes y conserva selección, paginación y estado actual.
- Grilla con esquinas redondeadas de radio 10 px. No alterar sus columnas funcionales, enlaces ni formato de valores para implementar redondeo.

## 7. Esquinas, color y tipografía

- Paneles de dashboard, tarjetas, resúmenes, formulario, acciones y catálogo con radios de 10–12 px. Botones principales/contextuales/laterales con radio 6–8 px. Campos con radio 7 px y grilla con radio 10 px.
- Los paneles que sirven solo como separadores, el fondo general y el marco exterior de navegación pueden conservar sus bordes rectos. Aplicar `CampoRedondeado.Aplicar` después de construir o reconstruir controles, incluidas las cargas que creen botones de paginación.
- Familia Segoe UI; título de módulo 21 pt, título de bloque/dashboard 14 pt negrita, subtítulo 10 pt, etiquetas 9 pt negrita, captura/celdas alrededor de 9.5 pt, rótulos contextuales 9 pt negrita.

## 8. Alcance del piloto y evidencia

Implementación piloto en `FrmFacturacion`, `FrmDetalleFactura`, `FrmMaestroDetalle` y el helper compartido `AjustesDisenoCrud`. MSBuild compiló los cuatro proyectos el 2026-10-29 sin errores ni advertencias visibles. El estudiante compartió una captura de «Factura 1» con el encabezado, cliente, grilla de detalle y pie de paginación, y confirmó que el modelo relacional está al 100%. Esa confirmación y captura son la evidencia de aceptación visual; no equivalen a una prueba independiente de cada operación ni de todos los casos de paginación.

La apariencia no debe considerarse cerrada por el solo hecho de compilar. Después de reutilizar el modelo en otro módulo, revisar la pantalla a tamaño normal y reducido; revisar también lista FK abierta, catálogo vacío/con registros y selección de detalle, sin modificar sus operaciones para acomodar una captura.

### Ajuste de catálogo de Facturación — 2026-10-29

- El selector Todos/Activos/Inactivos va a la izquierda de la búsqueda. «Activar todos los filtros» va al extremo izquierdo de la fila de opciones y marca/desmarca todos los checks de columnas; «Seleccionar registro» queda anclado al extremo derecho. Los checks de columnas ocupan la franja central en una sola línea con desplazamiento horizontal cuando hace falta.
- «Dashboard de facturación», «Facturas registradas» y «Opciones de visualización» usan Segoe UI negrita de 14 pt.
- El pie usa el helper compartido de CRUD para mostrar Resultados, Activos e Inactivos a la izquierda y paginación a la derecha, como Usuarios. No reemplazar ese texto por un total aislado desde los eventos de Facturación.
- La grilla fija su espacio en encabezado más diez filas de 29 px (326 px), aunque la página final tenga menos filas o no haya datos. El catálogo y dashboard se dimensionan considerando también búsqueda, opciones y pie.

## 9. Mensajes de error en formularios relacionales

Aplicar la regla de errores del [Modelo visual CRUD simple](MODELO_VISUAL_CRUD_SIMPLE.md#11-mensajes-de-error) también a la pantalla de consulta y a la ventana de captura maestro-detalle. Los errores de guardado, carga de listas FK, búsqueda, importación y transacciones deben usar `MensajesError.Resumir`; no mostrar respuestas completas de SQL Server, TCP o PostgREST. Conservar como título la acción que falló y dejar en el cuerpo una causa breve y un paso concreto, como revisar Docker, la sesión, los permisos, los datos obligatorios o la relación seleccionada.
