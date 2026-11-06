# Administración de cuentas Supabase

## Qué hace la aplicación

`FrmUsuarios` consulta y pagina los perfiles en páginas de 20 registros, permite buscar por código, alias, correo y tipo, crear cuentas, editar alias/correo/estado, cambiar la contraseña si se escribe una nueva e inactivar de forma lógica. El campo de contraseña queda vacío al consultar; ni el hash de SQL Server ni contraseñas Auth se muestran en la tabla.

Las cuentas creadas desde la aplicación usan Supabase Auth y quedan confirmadas. La contraseña inicial se envía solo a la función protegida por HTTPS; nunca se guarda en `public.Tbl_Usuarios`. El inicio de sesión de cuentas nuevas se hace con el correo y su contraseña. Los alias `alex`, `gabriel` y `homar` siguen disponibles para las tres cuentas iniciales.

Docker conserva el acceso CRUD de `dbo.Tbl_Usuarios`; sus contraseñas se almacenan con PBKDF2. Si el campo contraseña queda vacío al editar, se conserva la credencial actual.

## Protecciones

- Las solicitudes administrativas requieren una sesión Auth válida y un perfil activo con acceso de administrador.
- Alex conserva la marca `EsDueno`. La función rechaza su inactivación y no permite que otro administrador cambie su correo o contraseña.
- Ninguna cuenta puede inactivar la cuenta que mantiene la sesión actual.
- Los usuarios se inactivan cambiando `Estado` a `false`; no se borran las filas ni las cuentas de Auth. El acceso a la aplicación queda bloqueado por el control de perfil/RLS.
- `TipoUsuario` queda en `Administrador` para mantener el acceso total acordado. La pantalla no permite cambiar ese nivel en Supabase.
- La aplicación llama a `supabase/functions/usuarios-admin/` con el JWT del usuario. La función valida al operador y realiza los cambios Auth/perfil usando credenciales disponibles únicamente en el entorno de Edge Functions. No poner claves `secret` o `service_role` en C#, `.env`, Markdown ni Git.
- El cliente administrativo de la función requiere privilegios SQL explícitos además de omitir RLS. El log del proyecto reportó `42501 permission denied for table Tbl_Usuarios`; el ajuste mínimo preparado está en `database/06_Supabase_Permisos_Usuarios_Admin.sql`. Este concede `SELECT`, `INSERT` y `UPDATE` al rol `service_role` únicamente sobre `Tbl_Usuarios`, más uso de su secuencia; no concede `DELETE` ni cambia los permisos de `anon` o `authenticated`.
- La lectura de perfiles para la FK de Empleados sigue siendo una consulta PostgREST de solo lectura y no necesita permisos de administración.

## Publicar la función en el proyecto

La publicación de la función `usuarios-admin` fue confirmada por el estudiante el 2026-10-26. Al desplegar, la CLI avisó que Docker no estaba corriendo, pero continuó por API y reportó la publicación. Para futuras actualizaciones, desde CMD en la raíz:

```cmd
supabase login
supabase link --project-ref ID_DEL_PROYECTO
supabase functions deploy usuarios-admin --project-ref ID_DEL_PROYECTO
```

El ID se obtiene en Supabase Dashboard, en la configuración general del proyecto. La función necesita los secretos estándar inyectados por Supabase: URL del proyecto, clave publicable y clave secreta. Verifica que estén disponibles en **Edge Functions > Secrets**; no copies ni compartas los valores de la clave secreta. La configuración deja activa la verificación JWT (`verify_jwt = true`).

El despliegue requiere la Supabase CLI y una sesión con acceso al proyecto. Si Docker no está disponible, `--use-api` permite desplegar sin depender de Docker. El despliegue no modifica tablas ni datos.

## Verificación que falta

La solución compiló localmente con MSBuild. El estudiante compartió un log de Edge Function con SQLSTATE `42501` (`permission denied for table Tbl_Usuarios`). Después ejecutó `database/06_Supabase_Permisos_Usuarios_Admin.sql` en el proyecto `zlvjumrwymvoyivhvufy`; la verificación devolvió `true` para consultar, insertar, editar y usar la secuencia, y `false` para eliminar. En la prueba posterior, una captura de WinForms mostró los tres perfiles activos y la paginación en página 1 de 1. Aún faltan las pruebas de búsqueda, alta, edición, inactivación/reactivación y restricciones de propietario/sesión. No declarar esos flujos como verificados hasta recibir sus resultados.

## Referencias oficiales

- [Supabase: proteger Edge Functions](https://supabase.com/docs/guides/functions/auth)
- [Supabase: secretos en Edge Functions](https://supabase.com/docs/guides/functions/secrets)
- [Supabase: desplegar Edge Functions](https://supabase.com/docs/guides/functions/deploy)
