using RFinancieros_Facturas.Datos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RFinancieros_Facturas
{
    public partial class CargarOp : System.Web.UI.Page
    {
        dbFacturasFinancierosEntities ctx = new dbFacturasFinancierosEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuArchivo.HasFile)
                {
                    sppiner.Visible = true;
                    string FileName = Path.GetFileName(fuArchivo.PostedFile.FileName);
                    string Extension = Path.GetExtension(fuArchivo.PostedFile.FileName);
                    string FolderPath = System.Configuration.ConfigurationManager.AppSettings["Ejecutivo"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fuArchivo.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension, "Sí");
                    gvDatosExcel.AllowPaging = false;
                    gvDatosExcel.DataBind();
                    InsertaDatos();
                    sppiner.Visible = false;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString().Trim();
                connExcel.Close();
                connExcel.Open();
                cmdExcel.CommandText = "SELECT Nop, Revisor From [" + SheetName.Trim() + "] where Nop <> ''";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                gvDatosExcel.Caption = Path.GetFileName(FilePath);
                gvDatosExcel.DataSource = dt;
                gvDatosExcel.DataBind();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private void InsertaDatos()
        {
            try
            {
                string registros = gvDatosExcel.Rows.Count.ToString();
                foreach (GridViewRow rw in gvDatosExcel.Rows)
                {
                    string op = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[0].Text.ToString().Trim()));
                    string rev = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[1].Text.Trim()));
                    tdoprev ops = new tdoprev()
                    {
                        NoOdp = op, 
                        Revisor = rev
                    };
                    ctx.tdoprev.Add(ops); 
                    ctx.SaveChanges();
                }               
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('La información ha sido cargada correctamente, se cargaron " + registros + " registros','info')", true);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }
    }
}