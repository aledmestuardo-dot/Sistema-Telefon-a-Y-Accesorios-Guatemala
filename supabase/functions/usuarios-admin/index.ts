import { createClient } from "npm:@supabase/supabase-js@2";

const url = Deno.env.get("SUPABASE_URL")!;
const publicKeys = JSON.parse(Deno.env.get("SUPABASE_PUBLISHABLE_KEYS") || "{}");
const secretKeys = JSON.parse(Deno.env.get("SUPABASE_SECRET_KEYS") || "{}");
const anonKey = Deno.env.get("SUPABASE_ANON_KEY") || publicKeys.default;
const serviceRoleKey = Deno.env.get("SUPABASE_SERVICE_ROLE_KEY") || secretKeys.default;
const camposPerfil = "CodigoUsuario,TipoUsuario,Nombre,CorreoElectronico,FechaCreacion,FechaUltimoAcceso,Estado,EsDueno,AuthUserId";

function respuesta(datos: unknown, estado = 200): Response {
  return new Response(JSON.stringify(datos), {
    status: estado,
    headers: { "Content-Type": "application/json; charset=utf-8" },
  });
}

Deno.serve(async (solicitud: Request) => {
  if (solicitud.method !== "POST") return respuesta({ error: "Método no permitido." }, 405);

  const autorizacion = solicitud.headers.get("Authorization");
  if (!autorizacion?.startsWith("Bearer ")) return respuesta({ error: "Inicia sesión para administrar usuarios." }, 401);

  try {
    const clienteSesion = createClient(url, anonKey, {
      global: { headers: { Authorization: autorizacion } },
      auth: { persistSession: false, autoRefreshToken: false },
    });
    const clienteAdmin = createClient(url, serviceRoleKey, {
      auth: { persistSession: false, autoRefreshToken: false },
    });

    const { data: autenticacion, error: errorAuth } = await clienteSesion.auth.getUser();
    if (errorAuth || !autenticacion.user) return respuesta({ error: "La sesión no es válida o expiró." }, 401);

    const { data: actor, error: errorActor } = await clienteAdmin
      .from("Tbl_Usuarios").select("CodigoUsuario,TipoUsuario,Estado,EsDueno,AuthUserId")
      .eq("AuthUserId", autenticacion.user.id).maybeSingle();

    // No mezclar un fallo de consulta con un rechazo de permisos: el primero
    // es un error del servidor y debe quedar visible en los logs de la función.
    if (errorActor) {
      console.error("No se pudo consultar el perfil que solicita administrar usuarios.", {
        code: errorActor.code,
        message: errorActor.message,
      });
      return respuesta({ error: "No se pudo verificar el perfil. Revisa los logs de usuarios-admin en Supabase." }, 500);
    }
    if (!actor) {
      return respuesta({ error: "La cuenta con sesión iniciada no tiene un perfil enlazado en Tbl_Usuarios." }, 403);
    }
    if (actor.Estado !== true) {
      return respuesta({ error: "El perfil de esta cuenta está inactivo." }, 403);
    }
    if (actor.EsDueno !== true && actor.TipoUsuario !== "Administrador") {
      return respuesta({ error: "Tu cuenta no tiene permiso para administrar usuarios." }, 403);
    }

    const entrada = await solicitud.json();
    const accion = String(entrada.accion || "").toLowerCase();

    if (accion === "listar") {
      const { data, error } = await clienteAdmin.from("Tbl_Usuarios")
        .select(camposPerfil).order("CodigoUsuario", { ascending: true });
      if (error) throw error;
      return respuesta({ usuarios: data || [] });
    }

    if (accion === "agregar") {
      const nombre = validarTexto(entrada.nombre, "Nombre", 100);
      const correo = validarCorreo(entrada.correo);
      const contrasenia = validarContrasenia(entrada.contrasenia);
      const { data: creada, error: errorCrear } = await clienteAdmin.auth.admin.createUser({
        email: correo,
        password: contrasenia,
        email_confirm: true,
        user_metadata: { alias: nombre },
      });
      if (errorCrear || !creada.user) throw errorCrear || new Error("Supabase no devolvió el usuario creado.");

      const { data: perfil, error: errorPerfil } = await clienteAdmin.from("Tbl_Usuarios").insert({
        TipoUsuario: "Administrador",
        Nombre: nombre,
        CorreoElectronico: correo,
        FechaCreacion: new Date().toISOString(),
        FechaUltimoAcceso: null,
        Estado: entrada.estado !== false,
        AuthUserId: creada.user.id,
        EsDueno: false,
      }).select(camposPerfil).single();

      if (errorPerfil) {
        await clienteAdmin.auth.admin.deleteUser(creada.user.id);
        throw errorPerfil;
      }
      return respuesta({ usuario: perfil });
    }

    const codigo = Number(entrada.codigoUsuario);
    if (!Number.isInteger(codigo) || codigo <= 0) return respuesta({ error: "Selecciona un usuario válido." }, 400);
    const { data: objetivo, error: errorObjetivo } = await clienteAdmin.from("Tbl_Usuarios")
      .select(camposPerfil).eq("CodigoUsuario", codigo).maybeSingle();
    if (errorObjetivo) throw errorObjetivo;
    if (!objetivo) return respuesta({ error: "No se encontró el usuario seleccionado." }, 404);
    if (objetivo.EsDueno === true) {
      if (accion === "inactivar" || entrada.estado === false)
        return respuesta({ error: "La cuenta propietaria no se puede inactivar." }, 403);
      if (entrada.TipoUsuario && entrada.TipoUsuario !== "Administrador")
        return respuesta({ error: "No se puede cambiar el acceso de la cuenta propietaria." }, 403);
    }

    if (accion === "inactivar") {
      if (objetivo.AuthUserId === autenticacion.user.id)
        return respuesta({ error: "No puedes inactivar la cuenta que tiene la sesión abierta." }, 403);
      const { data, error } = await clienteAdmin.from("Tbl_Usuarios").update({ Estado: false })
        .eq("CodigoUsuario", codigo).select(camposPerfil).single();
      if (error) throw error;
      return respuesta({ usuario: data });
    }

    if (accion === "editar") {
      const nombre = validarTexto(entrada.nombre, "Nombre", 100);
      const correo = validarCorreo(entrada.correo);
      const estado = entrada.estado !== false;
      const contraseniaNueva = String(entrada.contrasenia || "");
      if (!estado && objetivo.AuthUserId === autenticacion.user.id)
        return respuesta({ error: "No puedes inactivar la cuenta que tiene la sesión abierta." }, 403);

      const correoAnterior = objetivo.CorreoElectronico;
      const cambiaCorreo = correo.toLowerCase() !== String(correoAnterior).toLowerCase();
      if (objetivo.EsDueno === true && actor.EsDueno !== true
        && (cambiaCorreo || contraseniaNueva.length > 0))
        return respuesta({ error: "Solo la cuenta propietaria puede cambiar su correo o contraseña." }, 403);

      const cambiosAuth: { email?: string; email_confirm?: boolean; user_metadata: { alias: string }; password?: string } = {
        user_metadata: { alias: nombre },
      };
      if (cambiaCorreo) {
        cambiosAuth.email = correo;
        cambiosAuth.email_confirm = true;
      }
      if (contraseniaNueva.length > 0) cambiosAuth.password = validarContrasenia(contraseniaNueva);
      const { data, error } = await clienteAdmin.from("Tbl_Usuarios").update({
        TipoUsuario: "Administrador",
        Nombre: nombre,
        CorreoElectronico: correo,
        Estado: estado,
      }).eq("CodigoUsuario", codigo).select(camposPerfil).single();
      if (error) throw error;

      const { error: errorAuthActualizar } = await clienteAdmin.auth.admin.updateUserById(objetivo.AuthUserId, cambiosAuth);
      if (errorAuthActualizar) {
        await clienteAdmin.from("Tbl_Usuarios").update({
          TipoUsuario: objetivo.TipoUsuario,
          Nombre: objetivo.Nombre,
          CorreoElectronico: correoAnterior,
          Estado: objetivo.Estado,
        }).eq("CodigoUsuario", codigo);
        throw errorAuthActualizar;
      }
      return respuesta({ usuario: data });
    }

    return respuesta({ error: "La operación solicitada no está permitida." }, 400);
  } catch (error) {
    // No registrar cuerpos de solicitudes: pueden incluir contraseñas iniciales.
    return respuesta({ error: error instanceof Error ? error.message : "No fue posible administrar el usuario." }, 400);
  }
});

function validarTexto(valor: unknown, campo: string, maximo: number): string {
  const texto = String(valor || "").trim();
  if (!texto || texto.length > maximo) throw new Error(campo + " es obligatorio y admite hasta " + maximo + " caracteres.");
  return texto;
}

function validarCorreo(valor: unknown): string {
  const correo = validarTexto(valor, "El correo electrónico", 100);
  if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(correo)) throw new Error("Escribe un correo electrónico válido.");
  return correo;
}

function validarContrasenia(valor: unknown): string {
  const contrasenia = String(valor || "");
  if (contrasenia.length < 8 || contrasenia.length > 100)
    throw new Error("La contraseña inicial debe tener entre 8 y 100 caracteres.");
  return contrasenia;
}
