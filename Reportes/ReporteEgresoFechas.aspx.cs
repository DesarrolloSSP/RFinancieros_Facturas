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
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {

        //        if (!User.IsInRole("Administrador"))
        //        {
        //            FormsAuthentication.SignOut();
        //            Session.Abandon();
        //            Response.Redirect(Request.RawUrl, false);
        //            Response.Redirect("~Inicio/Login.aspx");
        //        }
        //        egreso.ProcessingMode = ProcessingMode.Remote;
        //        //Le indicamos la URL donde se encuentra hospedado Reporting Services
        //        egreso.ServerReport.ReportServerUrl = new Uri("http://10.8.3.199/reportserver");
        //        //Le indicamos la carpeta y el Reporte que deseamos Ver
        //        egreso.ServerReport.ReportPath = "/financieros/ListadoDeEgresoFechas";

        //    }

        //}


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                egreso.ProcessingMode = ProcessingMode.Remote;
                //Le indicamos la URL donde se encuentra hospedado Reporting Services
                egreso.ServerReport.ReportServerUrl = new Uri("http://10.8.3.199/reportserver");
                //Le indicamos la carpeta y el Reporte que deseamos Ver
                egreso.ServerReport.ReportPath = "/financieros/ListadoDeEgresoFechas";

            }

        }

        //protected void btnVerReporte_Click(object sender, EventArgs e)
        //{

        //    string rfc = txtRfc.Text.Trim();
        //    string fechai = txtFechaInicio.Text.Trim();
        //    string fechaf = txtFechaFin.Text.Trim();
        //    string idEstatus = ddlEstatus.SelectedItem.Text.Trim();


        //    ReportParameter[] parameters = new ReportParameter[4];
        //    parameters[0] = new ReportParameter("rfce", rfc);
        //    parameters[1] = new ReportParameter("fechai", fechai);
        //    parameters[2] = new ReportParameter("fechaf", fechaf);
        //    parameters[3] = new ReportParameter("estatus", idEstatus);

        //    // Configuramos el ReportViewer
        //    egreso.ProcessingMode = ProcessingMode.Remote;
        //    egreso.ServerReport.ReportServerUrl = new Uri("http://10.8.3.199/reportserver");  // URL de Reporting Services
        //    egreso.ServerReport.ReportPath = "/financieros/ListadoDeEgresoFechas";  // Ruta del reporte en el servidor

        //    // Asignamos los parámetros al reporte
        //    egreso.ServerReport.SetParameters(parameters);

        //    // Cargamos el reporte
        //    egreso.ServerReport.Refresh();
        //}

        protected void btnVerReporte_Click(object sender, EventArgs e)
        {
            // Limpiar mensaje de error
            lblError.Text = "";

            // 1. Validar RFC
            string rfc = txtRfc.Text.Trim();
            if (string.IsNullOrWhiteSpace(rfc))
            {
                lblError.Text = "Debe ingresar un RFC.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 2. Validar fechas
            string fechaiStr = txtFechaInicio.Text.Trim();
            string fechafStr = txtFechaFin.Text.Trim();
            DateTime fechai, fechaf;

            if (!DateTime.TryParse(fechaiStr, out fechai))
            {
                lblError.Text = "La fecha de inicio no es válida.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!DateTime.TryParse(fechafStr, out fechaf))
            {
                lblError.Text = "La fecha de fin no es válida.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (fechai > fechaf)
            {
                lblError.Text = "La fecha de inicio no puede ser mayor que la fecha de fin.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 3. Validar estatus
            string idEstatus = ddlEstatus.SelectedItem?.Text?.Trim();
            if (string.IsNullOrEmpty(idEstatus) || ddlEstatus.SelectedIndex == 0)
            {
                lblError.Text = "Debe seleccionar un estatus.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 4. Configurar parámetros del reporte
            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("rfce", rfc);
            parameters[1] = new ReportParameter("fechai", fechai.ToString("yyyy-MM-dd"));
            parameters[2] = new ReportParameter("fechaf", fechaf.ToString("yyyy-MM-dd"));
            parameters[3] = new ReportParameter("estatus", idEstatus);

            // 5. Configurar ReportViewer
            egreso.ProcessingMode = ProcessingMode.Remote;
            egreso.ServerReport.ReportServerUrl = new Uri("http://10.8.3.199/reportserver");
            egreso.ServerReport.ReportPath = "/financieros/ListadoDeEgresoFechas";

            // 6. Asignar parámetros y cargar reporte
            egreso.ServerReport.SetParameters(parameters);
            egreso.ServerReport.Refresh();
        }

    }
}