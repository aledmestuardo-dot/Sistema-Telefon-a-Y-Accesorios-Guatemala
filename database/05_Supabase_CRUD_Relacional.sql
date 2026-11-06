-- Función transaccional para los tres flujos maestro-detalle existentes.
-- No crea ni modifica tablas. Ejecutar una sola vez en Supabase SQL Editor.
-- La función se ejecuta como invoker: conserva el JWT del usuario y aplica RLS.

BEGIN;

CREATE OR REPLACE FUNCTION public."GuardarMaestroDetalle"(
    p_tabla_maestro text,
    p_clave_maestro text,
    p_tabla_detalle text,
    p_clave_detalle text,
    p_clave_foranea text,
    p_codigo_maestro integer,
    p_maestro jsonb,
    p_detalles jsonb,
    p_accion text
)
RETURNS integer
LANGUAGE plpgsql
SET search_path = pg_catalog, public, auth
AS $$
DECLARE
    v_codigo_maestro integer := p_codigo_maestro;
    v_detalle jsonb;
    v_codigo_detalle integer;
    v_codigos_conservados integer[] := ARRAY[]::integer[];
    v_columnas text;
    v_valores text;
    v_asignaciones text;
    v_filas integer;
BEGIN
    -- Solo se permiten las relaciones maestro-detalle usadas por los formularios.
    IF NOT (
        (p_tabla_maestro = 'Tbl_Compras' AND p_clave_maestro = 'CodigoCompra'
            AND p_tabla_detalle = 'Tbl_DetalleCompra' AND p_clave_detalle = 'CodigoDetalleCompra'
            AND p_clave_foranea = 'CodigoCompra')
        OR
        (p_tabla_maestro = 'Tbl_Ventas' AND p_clave_maestro = 'CodigoVenta'
            AND p_tabla_detalle = 'Tbl_DetalleVenta' AND p_clave_detalle = 'CodigoDetalleVenta'
            AND p_clave_foranea = 'CodigoVenta')
        OR
        (p_tabla_maestro = 'Tbl_Facturacion' AND p_clave_maestro = 'CodigoFactura'
            AND p_tabla_detalle = 'Tbl_DetalleFactura' AND p_clave_detalle = 'CodigoDetalleFactura'
            AND p_clave_foranea = 'CodigoFactura')
    ) THEN
        RAISE EXCEPTION 'La relación maestro-detalle indicada no está permitida.';
    END IF;

    IF p_accion = 'inactivar' THEN
        IF p_codigo_maestro IS NULL OR p_codigo_maestro <= 0 THEN
            RAISE EXCEPTION 'Selecciona un registro maestro válido para inactivar.';
        END IF;

        EXECUTE format('UPDATE public.%I SET "Estado" = false WHERE %I = $1',
            p_tabla_detalle, p_clave_foranea)
            USING p_codigo_maestro;
        EXECUTE format('UPDATE public.%I SET "Estado" = false WHERE %I = $1',
            p_tabla_maestro, p_clave_maestro)
            USING p_codigo_maestro;
        GET DIAGNOSTICS v_filas = ROW_COUNT;
        IF v_filas = 0 THEN
            RAISE EXCEPTION 'No existe el registro maestro seleccionado.';
        END IF;
        RETURN p_codigo_maestro;
    END IF;

    IF p_accion NOT IN ('agregar', 'editar') THEN
        RAISE EXCEPTION 'La operación maestro-detalle no es válida.';
    END IF;
    IF p_maestro IS NULL OR jsonb_typeof(p_maestro) <> 'object' THEN
        RAISE EXCEPTION 'Los datos del encabezado están vacíos o no tienen el formato esperado.';
    END IF;
    IF p_detalles IS NULL OR jsonb_typeof(p_detalles) <> 'array'
        OR jsonb_array_length(p_detalles) = 0 THEN
        RAISE EXCEPTION 'Agrega al menos una línea de detalle.';
    END IF;

    SELECT string_agg(format('%I', column_name), ', ' ORDER BY ordinal_position),
           string_agg(format('r.%I', column_name), ', ' ORDER BY ordinal_position),
           string_agg(format('%I = r.%I', column_name, column_name), ', ' ORDER BY ordinal_position)
      INTO v_columnas, v_valores, v_asignaciones
      FROM information_schema.columns
     WHERE table_schema = 'public'
       AND table_name = p_tabla_maestro
       AND column_name <> p_clave_maestro;

    IF p_accion = 'agregar' THEN
        EXECUTE format(
            'INSERT INTO public.%I (%s) SELECT %s FROM jsonb_populate_record(NULL::public.%I, $1) AS r RETURNING %I',
            p_tabla_maestro, v_columnas, v_valores, p_tabla_maestro, p_clave_maestro)
            USING p_maestro - p_clave_maestro
            INTO v_codigo_maestro;
    ELSE
        IF p_codigo_maestro IS NULL OR p_codigo_maestro <= 0 THEN
            RAISE EXCEPTION 'Selecciona un registro maestro válido para editar.';
        END IF;
        EXECUTE format(
            'UPDATE public.%I AS t SET %s FROM jsonb_populate_record(NULL::public.%I, $1) AS r WHERE t.%I = $2 RETURNING t.%I',
            p_tabla_maestro, v_asignaciones, p_tabla_maestro, p_clave_maestro, p_clave_maestro)
            USING p_maestro, p_codigo_maestro
            INTO v_codigo_maestro;
        IF v_codigo_maestro IS NULL THEN
            RAISE EXCEPTION 'No existe el registro maestro seleccionado.';
        END IF;
    END IF;

    SELECT string_agg(format('%I', column_name), ', ' ORDER BY ordinal_position),
           string_agg(format('r.%I', column_name), ', ' ORDER BY ordinal_position),
           string_agg(format('%I = r.%I', column_name, column_name), ', ' ORDER BY ordinal_position)
      INTO v_columnas, v_valores, v_asignaciones
      FROM information_schema.columns
     WHERE table_schema = 'public'
       AND table_name = p_tabla_detalle
       AND column_name <> p_clave_detalle;

    FOR v_detalle IN SELECT value FROM jsonb_array_elements(p_detalles)
    LOOP
        v_detalle := jsonb_set(v_detalle, ARRAY[p_clave_foranea], to_jsonb(v_codigo_maestro), true);
        v_codigo_detalle := COALESCE(NULLIF(v_detalle ->> p_clave_detalle, '')::integer, 0);

        IF p_accion = 'editar' AND v_codigo_detalle > 0 THEN
            EXECUTE format(
                'UPDATE public.%I AS t SET %s FROM jsonb_populate_record(NULL::public.%I, $1) AS r WHERE t.%I = $2 AND t.%I = $3 RETURNING t.%I',
                p_tabla_detalle, v_asignaciones, p_tabla_detalle,
                p_clave_detalle, p_clave_foranea, p_clave_detalle)
                USING v_detalle, v_codigo_detalle, v_codigo_maestro
                INTO v_codigo_detalle;
            IF v_codigo_detalle IS NULL THEN
                RAISE EXCEPTION 'Una línea de detalle no pertenece a este encabezado.';
            END IF;
        ELSE
            EXECUTE format(
                'INSERT INTO public.%I (%s) SELECT %s FROM jsonb_populate_record(NULL::public.%I, $1) AS r RETURNING %I',
                p_tabla_detalle, v_columnas, v_valores, p_tabla_detalle, p_clave_detalle)
                USING v_detalle - p_clave_detalle
                INTO v_codigo_detalle;
        END IF;
        v_codigos_conservados := array_append(v_codigos_conservados, v_codigo_detalle);
    END LOOP;

    IF p_accion = 'editar' THEN
        -- Las líneas retiradas durante la edición quedan inactivas, nunca se borran.
        EXECUTE format(
            'UPDATE public.%I SET "Estado" = false WHERE %I = $1 AND NOT (%I = ANY($2))',
            p_tabla_detalle, p_clave_foranea, p_clave_detalle)
            USING v_codigo_maestro, v_codigos_conservados;
    END IF;

    RETURN v_codigo_maestro;
END;
$$;

REVOKE ALL ON FUNCTION public."GuardarMaestroDetalle"(
    text, text, text, text, text, integer, jsonb, jsonb, text
) FROM PUBLIC, anon, authenticated;
GRANT EXECUTE ON FUNCTION public."GuardarMaestroDetalle"(
    text, text, text, text, text, integer, jsonb, jsonb, text
) TO authenticated;

-- Hace visible la nueva función RPC para PostgREST tras confirmar la transacción.
NOTIFY pgrst, 'reload schema';

COMMIT;
