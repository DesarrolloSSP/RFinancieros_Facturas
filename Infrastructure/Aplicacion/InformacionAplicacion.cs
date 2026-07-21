using System.Reflection;

namespace RFinancieros_Facturas.Infrastructure.Aplicacion
{
    public static class InformacionAplicacion
    {
        public static string Nombre =>
            Assembly.GetExecutingAssembly().GetName().Name;

        public static string Version =>
            Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string Framework =>
            System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
    }
}