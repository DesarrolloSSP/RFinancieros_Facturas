using System.Configuration;

namespace RFinancieros_Facturas.Infrastructure.Configuracion
{
    public static class ConfiguracionAplicacion
    {
        public static string Servidor =>
            ConfigurationManager.AppSettings["servidor"];

        public static string BaseDatos =>
            ConfigurationManager.AppSettings["bd"];

        public static string Usuario =>
            ConfigurationManager.AppSettings["usuario"];

        public static string Clave =>
            ConfigurationManager.AppSettings["clave"];

        public static string Obtener(string llave)
        {
            return ConfigurationManager.AppSettings[llave];
        }
    }
}