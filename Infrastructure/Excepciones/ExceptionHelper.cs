using System;

namespace RFinancieros_Facturas.Infrastructure.Excepciones
{
    public static class ExceptionHelper
    {
        public static Exception ObtenerExcepcionBase(Exception ex)
        {
            if (ex == null)
                return null;

            while (ex.InnerException != null)
                ex = ex.InnerException;

            return ex;
        }

        public static string ObtenerMensajeCompleto(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            string mensaje = ex.Message;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                mensaje += Environment.NewLine + ex.Message;
            }

            return mensaje;
        }

        public static string ObtenerStackTraceCompleto(Exception ex)
        {
            return ex?.ToString() ?? string.Empty;
        }
    }
}