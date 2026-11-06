/*
    Permisos minimos para que la Edge Function usuarios-admin consulte y
    administre perfiles en public.Tbl_Usuarios.

    El log verificado de la funcion devolvio SQLSTATE 42501:
    permission denied for table Tbl_Usuarios.

    Este script NO otorga permisos de escritura a anon ni authenticated,
    NO habilita DELETE y NO modifica filas, politicas RLS ni el esquema.
    Ejecutar una sola vez en el SQL Editor del proyecto Supabase enlazado
    a la aplicacion.
*/

BEGIN;

GRANT USAGE ON SCHEMA public TO service_role;
GRANT SELECT, INSERT, UPDATE ON TABLE public."Tbl_Usuarios" TO service_role;

DO $$
DECLARE
    secuencia text;
BEGIN
    secuencia := pg_get_serial_sequence('public."Tbl_Usuarios"', 'CodigoUsuario');
    IF secuencia IS NULL THEN
        RAISE EXCEPTION 'No se encontro la secuencia de CodigoUsuario en Tbl_Usuarios.';
    END IF;

    EXECUTE format(
        'GRANT USAGE, SELECT ON SEQUENCE %s TO service_role',
        secuencia::regclass
    );
END;
$$;

COMMIT;

-- Verificacion: SELECT, INSERT y UPDATE deben devolver true.
-- DELETE debe devolver false, y anon/authenticated no reciben cambios.
SELECT
    has_table_privilege('service_role', 'public."Tbl_Usuarios"', 'SELECT') AS puede_consultar,
    has_table_privilege('service_role', 'public."Tbl_Usuarios"', 'INSERT') AS puede_insertar,
    has_table_privilege('service_role', 'public."Tbl_Usuarios"', 'UPDATE') AS puede_editar,
    has_table_privilege('service_role', 'public."Tbl_Usuarios"', 'DELETE') AS puede_eliminar,
    has_sequence_privilege(
        'service_role',
        pg_get_serial_sequence('public."Tbl_Usuarios"', 'CodigoUsuario'),
        'USAGE'
    ) AS puede_generar_codigo;
