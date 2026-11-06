/*
    Asociacion inicial de las tres cuentas Auth con perfiles de la aplicacion.

    REQUISITOS:
    1. Confirmar que el esquema de tablas ya existe en el proyecto Supabase.
    2. Confirmar que public."Tbl_Usuarios" esta vacia.
    3. Verificar que los tres correos de abajo correspondan a cuentas Auth
       confirmadas. El script se detiene si falta una cuenta o no esta confirmada.
    4. Ejecutar este archivo una sola vez. No contiene ni consulta contrasenas.

    No crea registros de negocio. El resto de las tablas queda vacio.
*/

BEGIN;

-- Correos reales confirmados por el estudiante para el alta inicial.
CREATE TEMP TABLE perfiles_iniciales (
    nombre text PRIMARY KEY,
    correo text NOT NULL,
    es_dueno boolean NOT NULL
) ON COMMIT DROP;

INSERT INTO perfiles_iniciales (nombre, correo, es_dueno) VALUES
    ('Alex', 'alexestuardodonismorales26@gmail.com', TRUE),
    ('Gabriel', 'galvarezo3@miumg.edu.gt', FALSE),
    ('Homar', 'lhernandezl38@miumg.edu.gt', FALSE);

DO $$
DECLARE
    correos_faltantes text;
BEGIN
    IF EXISTS (SELECT 1 FROM public."Tbl_Usuarios") THEN
        RAISE EXCEPTION 'Tbl_Usuarios debe estar vacia antes de asociar las cuentas iniciales.';
    END IF;

    SELECT string_agg(c.correo, ', ')
    INTO correos_faltantes
    FROM perfiles_iniciales AS c
    WHERE NOT EXISTS (
        SELECT 1 FROM auth.users AS a
        WHERE lower(a.email) = lower(c.correo)
          AND a.email_confirmed_at IS NOT NULL
    );

    IF correos_faltantes IS NOT NULL THEN
        RAISE EXCEPTION 'Faltan cuentas confirmadas en Supabase Auth (o los correos no coinciden): %', correos_faltantes;
    END IF;

    INSERT INTO public."Tbl_Usuarios" (
        "TipoUsuario", "Nombre", "CorreoElectronico", "FechaCreacion",
        "FechaUltimoAcceso", "Estado", "AuthUserId", "EsDueno"
    )
    SELECT
        'Administrador',
        p.nombre,
        lower(a.email),
        CURRENT_TIMESTAMP,
        NULL,
        TRUE,
        a.id,
        p.es_dueno
    FROM perfiles_iniciales AS p
    INNER JOIN auth.users AS a ON lower(a.email) = lower(p.correo)
    WHERE a.email_confirmed_at IS NOT NULL;

    IF (SELECT count(*) FROM public."Tbl_Usuarios") <> 3 THEN
        RAISE EXCEPTION 'Se esperaban exactamente tres perfiles iniciales.';
    END IF;

    IF (SELECT count(*) FROM public."Tbl_Usuarios" WHERE "EsDueno" IS TRUE) <> 1 THEN
        RAISE EXCEPTION 'Debe quedar exactamente un perfil protegido como dueño.';
    END IF;
END;
$$;

COMMIT;
