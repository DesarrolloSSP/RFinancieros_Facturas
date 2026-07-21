using RFinancieros_Facturas.Infrastructure.Logging;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace RFinancieros_Facturas
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                RegistrarRutas();
                RegistrarBundles();
                InicializarSqlServerTypes();

                AppLogger.Info("Aplicación iniciada correctamente.");
            }
            catch (Exception ex)
            {
                AppLogger.Fatal("Error durante Application_Start.", ex);
                throw;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            AppLogger.Error(Server.GetLastError());
            
        }

        #region Inicialización

        private void RegistrarRutas()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void RegistrarBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void InicializarSqlServerTypes()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
        }

        #endregion
        
    }
}