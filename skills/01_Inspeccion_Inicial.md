# 01 — Inspección inicial

**Estado:** COMPLETADO (inspección documental inicial).  
**Proyecto:** Programación II 2026.  
**Regla de entorno:** preferir CMD (`cmd.exe`) en Windows cuando esté disponible.

## Objetivo

Reconocer el contenido del repositorio antes de programar. No crear CRUD ni cambiar formularios durante la inspección. Anotar por separado los hechos observados y las preguntas pendientes.

## Procedimiento en CMD

Desde la carpeta del proyecto:

```bat
cd
dir /a
tree /f /a
git status --short --branch
```

`dir /a` muestra archivos ocultos y normales; `tree /f /a` permite revisar la estructura; `git status` distingue archivos rastreados modificados/eliminados de archivos nuevos. No usar comandos de limpieza ni restauración para “arreglar” el estado sin autorización.

## Estructura esperada para la documentación

```text
skills/       archivos .md numerados, sin subcarpetas
database/     archivos de base de datos, sin subcarpetas
docs/         documentación, sin subcarpetas
src/
  SistemaTelefonia/   sistema existente
```

No crear solución o formularios durante esta etapa. Conservar el sistema y los diseños existentes.

## Observación inicial comprobada (2026-10-25)

- La raíz contiene `.gitattributes`, `.gitignore`, `AGENTS.md`, `MEMORY.md`, `ESTADO_DOCUMENTOS.md` y las carpetas `database`, `docs`, `skills`, `src`.
- `src/SistemaTelefonia/` contiene una solución, proyectos de Entidad, Datos, Negocio y Presentación, formularios y recursos existentes.
- `database/`, `docs/` y `skills/` estaban vacías durante la inspección.
- Git reportó rama `master`, `ahead 3` respecto de `origin/master` y una lista de eliminaciones y elementos nuevos. Consultar `ESTADO_DOCUMENTOS.md`; no asumir que los borrados son accidentales.
- Había un archivo raíz vacío `cd`; se quitó durante la limpieza autorizada.

## Confirmación del árbol y avance a Skill 02

El estudiante confirmó el árbol real mostrado por el Explorador el 2026-10-25. Esa confirmación permitió continuar con la inspección y conexión documentadas en Skill 02. No volver a tratar este prerrequisito como pendiente.

## Resultado

La inspección inicial está documentada y el árbol real fue confirmado por el estudiante. La conexión y las siguientes etapas se ejecutaron posteriormente bajo sus autorizaciones, según el estado vigente de `ESTADO_DOCUMENTOS.md`.
