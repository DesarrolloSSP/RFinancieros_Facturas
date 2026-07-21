using System;

namespace RFinancieros_Facturas.Errores
{
    public partial class _500 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CargaDatos.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}