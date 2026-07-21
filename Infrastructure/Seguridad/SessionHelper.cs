using System.Web;

namespace RFinancieros_Facturas.Infrastructure.Seguridad
{
    public static class SessionHelper
    {
        public static bool HaySesion =>
            HttpContext.Current?.Session != null;

        public static T Obtener<T>(string llave)
        {
            object valor = HttpContext.Current?.Session?[llave];

            if (valor == null)
                return default(T);

            return (T)valor;
        }

        public static void Guardar(string llave, object valor)
        {
            HttpContext.Current.Session[llave] = valor;
        }

        public static void Eliminar(string llave)
        {
            HttpContext.Current.Session.Remove(llave);
        }

        public static void Limpiar()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void Abandonar()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}