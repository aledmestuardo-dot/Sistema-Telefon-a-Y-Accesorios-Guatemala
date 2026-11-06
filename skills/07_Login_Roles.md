# 07 — Login y roles

**Estado: IMPLEMENTADO; autenticación y cierre de sesión verificados localmente. La administración de usuarios Supabase sigue pendiente de validación desde WinForms.** Trabajo autorizado por el estudiante.

## Implementación comprobada

- `Program.cs` inicia `FrmLogin`; la compilación local de la solución con MSBuild terminó correctamente después de restaurar los paquetes.
- La autenticación se delega desde Presentación a Negocio y Datos. `UsuariosDatos` consulta usuarios activos mediante parámetros; `UsuariosNegocio` valida hashes PBKDF2 con sal y actualiza el último acceso.
- `FrmLogin` ya no compara usuario/contraseña fijos dentro del formulario. La navegación usa la instancia inicial de Login para regresar al cerrar sesión.
- En la aplicación compilada se autenticaron por separado Gabriel, Alex y Homar; cada acceso abrió `FrmMenuPrincipal`. Tras cerrar la sesión de prueba, la aplicación volvió a `Acceso al sistema`.
- El estudiante confirmó que las credenciales incorrectas se rechazan para las tres cuentas.
- Después de cerrar sesión, la versión recompilada se ejecutó y se observó que ambos campos quedan vacíos y que el foco vuelve al campo Usuario.

## Datos de usuarios

- Se inactivaron las cuentas que estaban activas, sin borrar filas, para conservar la FK `Tbl_Empleados.CodigoUsuario`.
- Gabriel, Alex y Homar quedaron activos con tipo `Administrador` y los correos locales acordados. Las contraseñas se almacenan como hashes PBKDF2 con sal; no registrar contraseñas en documentos compartidos.
- El resultado SQL reportado para la transacción fue 14 filas totales, 3 activas y 11 inactivas; se conservaron 10 asociaciones de empleados.

## Pendientes

- Precisar y validar el alcance de autorización por roles. Las tres cuentas tienen acceso tipo Administrador, según la instrucción del estudiante.
- Probar desde FrmUsuarios las altas, ediciones e inactivaciones/reactivaciones administrativas contra Supabase, incluida la protección de la cuenta dueña. La función usuarios-admin está desplegada y sus permisos mínimos fueron confirmados por el estudiante; esto no sustituye las pruebas desde la interfaz.
- Completar la aceptación de Docker desde una base limpia y comprobar Login con los usuarios iniciales del paquete.
- Verificar módulo por módulo las rutas Supabase aún no ejecutadas. Consultar el inventario vigente en ESTADO_DOCUMENTOS.md.
