using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas.Reportes
{
    public partial class ReporteEgresoFechas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("Administrador"))
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect(Request.RawUrl, false);
                Response.Redirect("~Inicio/Login.aspx");
            }
            egreso.ProcessingMode = ProcessingMode.Remote;
            //Le indicamos la URL donde se encuentra hospedado Reporting Services
            egreso.ServerReport.ReportServerUrl = new Uri("http://10.8.3.199/reportserver");
            //Le indicamos la carpeta y el Reporte que deseamos Ver
            egreso.ServerReport.ReportPath = "/financieros/ListadoDeEgresoFechas";
            //Definimos los parámetros

            //int folioint = (int)Session["folio"];

            //ReportParameter parametro = new ReportParameter();
            //parametro.Name = "egreso";
            //parametro.Values.Add(folioint.ToString());


            //Crearemos un arreglo de parámetros
            //ReportParameter[] rp = { parametro };
            //Ahora agregamos el parámetro en al reporte
            ///egreso.ServerReport.SetParameters(rp);
            //egreso.ServerReport.Refresh();
        }
    }
}