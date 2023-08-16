using OfficeOpenXml;
using RFinancieros_Facturas.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas
{
    public partial class ExportarDatos : System.Web.UI.Page
    {
        dbFacturasFinancierosEntities ctx = new dbFacturasFinancierosEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("Administrador"))
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect(Request.RawUrl, false);
                Response.Redirect("~Inicio/Login.aspx");
            }
            if (!IsPostBack)
            {
                DateTime dt = DateTime.Now;
                tbfechaini.Text = String.Format("{0:yyyy-MM-dd}", dt);
                tbfechafin.Text = String.Format("{0:yyyy-MM-dd}", dt);
            }
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excel = new ExcelPackage(); ;
                string fi = string.Format("{0:d}", tbfechaini.Text);
                string ff = string.Format("{0:d}", tbfechafin.Text);

                var informe = ctx.sel_facturas_fecha(Convert.ToDateTime(fi), Convert.ToDateTime(ff)).ToList();

                var workSheet = excel.Workbook.Worksheets.Add("Facturas");
                workSheet.Cells[1, 1].LoadFromCollection(informe, true);

                using (var memoryStream = new MemoryStream())
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=Total_Activides.xlsx");
                    excel.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }
    }
}