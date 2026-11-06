# 06 — Git colaborativo

**Estado: PUBLICADO en GitHub (`dfa4f18`).**

## Estado comprobado — 2026-10-26

- El estudiante autorizó comparar `master` con `origin/master`, conservar `src/SistemaTelefonia/`, crear commits locales y luego publicar.
- El push inicial se completó en `4cfaefa`; el commit `dfa4f18` publicó la limpieza de los 61 campos no usados. Se usó la cuenta `alexestuardodonismorales26-ctrl`. `master` y `origin/master` quedaron en el mismo commit.
- La revisión de rutas registró 146 diferencias antes de la integración: 118 renombres (principalmente el traslado de los proyectos a `src/SistemaTelefonia/`), 22 adiciones, 2 modificaciones y 4 eliminaciones.
- El merge tuvo conflictos de ubicación/ruta y contenido en `packages.config`, Excel, lector de códigos de barras, Empleados, Facturación, Pagos, Inventario, DetalleCompra, DetalleVenta y el `.csproj` de Presentación. Se conservaron las FK como ComboBox usando `ComboFK`, el árbol de proyectos bajo `src/` y las funciones remotas integradas. No quedan conflictos sin resolver.
- La revisión detectó y corrigió en `ArchivosExcel.CrearHoja` que las filas del Dashboard podían escribirse en la misma fila. La solución compiló con código 0; la exportación Excel no se probó en ejecución.
- Se retiraron solo las declaraciones huérfanas de `DataGridViewTextBoxColumn` en siete `.Designer.cs`; `dotnet msbuild /t:Rebuild` terminó con 0 errores y 0 advertencias C#. Se conservaron inicialización, layout, eventos y `.resx`. No se reabrieron los formularios para validación visual posterior.
- La captura posterior de Visual Studio muestra 21 errores de `Compilación + IntelliSense`, mayormente por referencias/tipos base en `FrmCardex`; la reconstrucción completa con MSBuild no reproduce los errores. El diagnóstico de Visual Studio sigue pendiente. Los cambios recientes locales de `FrmCardex.cs`/Designer no forman parte del commit publicado.
- `.env` y el respaldo `.bak` siguen fuera de Git.

## Reglas para la revisión/publicación

- En Windows, preferir CMD (`cmd.exe`) si está disponible.
- Revisar qué contiene el índice y el working tree por separado antes de preparar un commit; no usar `reset`, restauraciones o comandos de descarte para resolver divergencias sin autorización específica.
- Mantener `.env`, contraseñas y respaldos `.bak` fuera de Git.
- No hacer commit ni push hasta que el estudiante revise el resultado y lo autorice explícitamente.
