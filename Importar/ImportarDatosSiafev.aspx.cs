using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using RFinancieros_Facturas.Datos.Entities;
using RFinancieros_Facturas.Datos.Models;
using RFinancieros_Facturas.Services;

namespace RFinancieros_Facturas.Importar
{
    public partial class ImportarDatosSiafev : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }

        }

        protected void btnLeer_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (!fuExcel.HasFile)
                {
                    lblMensaje.Text = "Seleccione un archivo.";
                    return;
                }

                // Solo aceptar archivos .xlsx
                string extension = Path.GetExtension(fuExcel.FileName).ToLower();

                if (extension != ".xlsx")
                {
                    lblMensaje.Text = "Solo se permiten archivos Excel (.xlsx).";
                    return;
                }
                else
                {
                    gvDatos.DataSource = null;
                    gvDatos.DataBind();
                }


                string carpeta = Server.MapPath("~/Temp");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);


                string nombreArchivo = Guid.NewGuid().ToString() + extension;

                string rutaArchivo = Path.Combine(carpeta, nombreArchivo);


                fuExcel.SaveAs(rutaArchivo);


                ExcelService servicio = new ExcelService();

                List<PagoExcelVM> lista = servicio.LeerExcel(rutaArchivo);


                ViewState["RutaExcel"] = rutaArchivo;

                Session["PagosSiafec"] = lista;


                gvDatos.DataSource = lista;
                gvDatos.DataBind();
                btnGuardar.Enabled = true;


                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = $"Se encontraron {lista.Count} registros.";

                //btnGuardar.Enabled = lista.Count > 0;
                //btnLeer.Visible = false;
                btnGuardar.Visible = lista.Count > 0;
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = ex.Message;
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                List<PagoExcelVM> lista =
                    Session["PagosSiafec"] as List<PagoExcelVM>;

                if (lista == null)
                {
                    lblMensaje.Text = "Primero debe leer un archivo Excel.";
                    return;
                }

                PagoSiafecService servicio = new PagoSiafecService();

                ResultadoImportacion resultado = servicio.Guardar(lista);

                //lblMensaje.Text = resultado.Mensaje;
                lblMensaje.Text =
$@"Importación terminada correctamente.<br/><br/>
Leídos: {lista.Count}<br/>
Insertados: {resultado.Insertados}<br/>
Duplicados: {resultado.Duplicados}<br/>
Errores: {resultado.Errores}";

                gvDatos.DataSource = null;
                gvDatos.DataBind();

                Session.Remove("PagosSiafec");

                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }

}