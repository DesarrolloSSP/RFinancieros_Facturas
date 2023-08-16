using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RFinancieros_Facturas.Datos;

namespace RFinancieros_Facturas
{
    public partial class CargaDatos : System.Web.UI.Page
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
                    string FolderPath = ConfigurationManager.AppSettings["Ejecutivo"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fuArchivo.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension, "Sí");
                    gvDatosExcel.AllowPaging = false;
                    gvDatosExcel.DataBind();
                    InsertaDatos();
                    sppiner.Visible = false;
                }
            }
            //catch (Exception ex)
            //{
            //    _ = ex.Message;
            //}
            catch (DbEntityValidationException ee)
            {
                foreach (var eve in ee.EntityValidationErrors)
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
                cmdExcel.CommandText = "SELECT id as Id, folio as Folio, Estado,  Total as Importe, rfce as RFC, rsemisor as Razon_Social, fechaEmision as Fecha_Emisión, fechaCertificacion as Fecha_Certificación, PACcertifico From [" + SheetName.Trim() + "] where folio <> ''";
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
                string registros = gvDatosExcel.Rows.Count.ToString() ;
                foreach (GridViewRow rw in gvDatosExcel.Rows)
                {
                    string id = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[0].Text.ToString().Trim()));
                    string folio = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[1].Text.Trim()));
                    string estado = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[2].Text.Trim()));
                    string importe = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[3].Text.Trim()));
                    string rfce = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[4].Text.ToString().Trim()));
                    string rsemisor = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[5].Text.ToString().Trim()));
                    string fechaEmision = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[6].Text.Trim()));
                    string fechaCertificacion = HttpUtility.HtmlDecode(Convert.ToString((rw.Cells[7].Text.Trim())));
                    string pac = HttpUtility.HtmlDecode(Convert.ToString(rw.Cells[8].Text.Trim()));

                    tdfacturas lista1 = new tdfacturas()
                    {
                        Folio = folio,
                        Estado = estado,
                        Importe = Convert.ToDecimal(importe),
                        Rfce = rfce,
                        Rsemisor = rsemisor,
                        FechaEmision = Convert.ToDateTime(fechaEmision),
                        FechaCertifica = Convert.ToDateTime(fechaCertificacion),
                        PacCertifico = pac,
                        Idarea = 0,
                        IdPago = 0,
                        Idconcepto = 0,
                        NoOdp = "",
                        FolioInternoFactura = "",
                        ConceptoDevol = "",
                        FechaRevision = null,
                        FechaDevol = null,
                        FechaCaptura = null,
                        FechaCarga = DateTime.Now,
                        Estreg = "P",
                        FechaReingreso = null,
                        Revisor = "---",
                        Capturista = "---",
                        Egreso = null
                    };
                    ctx.tdfacturas.Add(lista1);
                }
                ctx.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "VariableRegisteration", "alertame('La información ha sido cargada correctamente, se cargaron " + registros + " registros','info')", true);
            }
            //catch (Exception ex)
            //{
            //    _ = ex.Message;
            //}
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
    }
}