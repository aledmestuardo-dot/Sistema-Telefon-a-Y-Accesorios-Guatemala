# Sistema de Telefonía y Accesorios

Proyecto académico de Programación II 2026. La solución actual está en `src/SistemaTelefonia/` y usa C#, Windows Forms, .NET Framework 4.8 y arquitectura por capas.

## Estado vigente — 2026-11-03

Esta actualización documental no ejecutó compilaciones ni pruebas funcionales.

- La aplicación actual es de escritorio (`WinExe`). Setup Project o ClickOnce pueden empaquetarla para instalación en Windows; no la convierten a una aplicación de navegador.
- La entrega acordada conserva SQL Server dentro de Docker y Supabase/PostgreSQL alojado. El Login valida contra el origen seleccionado.
- `usuarios-admin` está desplegada en Supabase y el estudiante verificó sus permisos mínimos; sus flujos desde WinForms todavía requieren pruebas. Tampoco se han verificado todos los módulos CRUD Supabase ni los guardados maestro-detalle.
- Antes de declarar completa la entrega siguen pendientes la inicialización desde una base Docker vacía, la aceptación del paquete en un equipo limpio, pruebas CRUD en ambos orígenes y la brecha del módulo Garantías requerido por el documento académico.

## Configurar acceso a Supabase

El archivo `.env.example` contiene la URL del proyecto Supabase y la clave `publishable`, que está diseñada para usarse en aplicaciones cliente. Para preparar la configuración local desde CMD, copia el archivo como `.env`:

```cmd
if not exist .env copy .env.example .env
```

`.env` está excluido de Git. El programa usa Supabase Auth y las políticas RLS para autorizar las operaciones; no agregues claves `secret`/`service_role`, contraseñas de usuarios ni credenciales SQL a este repositorio. La URL y la clave `publishable` permiten conectar con el proyecto, pero cada persona necesita las credenciales de una cuenta Auth activa, entregadas por un medio privado. Al abrir el programa, selecciona Supabase como origen de datos.

## Documentación del proyecto

- [`docs/`](docs/): procedimientos, mapeos y guías de implementación.
- [`skills/`](skills/): guías del curso y tareas.
- [`database/`](database/): scripts de esquema y operación.
- [`supabase/`](supabase/): configuración y funciones del backend remoto.

No incluir `.env`, respaldos `.bak`, contraseñas ni claves Supabase secret/service-role en Git o documentación compartida.
