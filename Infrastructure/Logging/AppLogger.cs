using System;
using System.Diagnostics;

namespace RFinancieros_Facturas.Infrastructure.Logging
{
    /// <summary>
    /// Logger centralizado de la aplicación.
    /// En una primera etapa utiliza System.Diagnostics.Trace.
    /// Posteriormente podrá escribir en archivo, BD,
    /// Event Viewer o proveedores como Serilog/NLog.
    /// </summary>
    public static class AppLogger
    {
        public static void Info(string mensaje)
        {
            Trace.TraceInformation(FormatearMensaje("INFO", mensaje));
        }

        public static void Warning(string mensaje)
        {
            Trace.TraceWarning(FormatearMensaje("WARN", mensaje));
        }

        public static void Error(string mensaje)
        {
            Trace.TraceError(FormatearMensaje("ERROR", mensaje));
        }

        public static void Error(Exception ex)
        {
            Error("Excepción no controlada.", ex);
        }

        public static void Error(string mensaje, Exception ex)
        {
            Trace.TraceError(FormatearExcepcion("ERROR", mensaje, ex));
        }

        public static void Fatal(string mensaje, Exception ex)
        {
            Trace.TraceError(FormatearExcepcion("FATAL", mensaje, ex));
        }

        private static string FormatearMensaje(string nivel, string mensaje)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}] {mensaje}";
        }

        private static string FormatearExcepcion(string nivel, string mensaje, Exception ex)
        {
            return
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}] {mensaje}{Environment.NewLine}" +
                $"Tipo........: {ex.GetType().FullName}{Environment.NewLine}" +
                $"Mensaje.....: {ex.Message}{Environment.NewLine}" +
                $"Origen......: {ex.Source}{Environment.NewLine}" +
                $"Método......: {ex.TargetSite}{Environment.NewLine}" +
                $"StackTrace..:{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}" +
                $"Inner........{Environment.NewLine}{ObtenerInnerException(ex.InnerException)}";
        }

        private static string ObtenerInnerException(Exception ex)
        {
            if (ex == null)
                return "Sin InnerException.";

            return
                $"Tipo.....: {ex.GetType().FullName}{Environment.NewLine}" +
                $"Mensaje..: {ex.Message}{Environment.NewLine}" +
                $"Origen...: {ex.Source}{Environment.NewLine}" +
                $"Stack....:{Environment.NewLine}{ex.StackTrace}";
        }
    }
}