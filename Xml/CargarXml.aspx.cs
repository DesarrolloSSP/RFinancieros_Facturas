using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using RFinancieros_Facturas.Datos;


namespace RFinancieros_Facturas.Xml
{
    public partial class CargarXml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarDatos();

            }
        }

        protected void cargarDatos()
        {
            using (dbFacturasFinancierosEntities ctx = new dbFacturasFinancierosEntities())
            {

                var query = ctx.FacturasXml.ToList();
                gvDatos.DataSource = query;
                gvDatos.DataBind();
                lblTotal.Text = "Registros encontrados:" + " " + query.Count().ToString();

            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFiles)
            {
                string uploadPath = Server.MapPath("~/Uploads/");

                // Verificar si el directorio existe, si no, crearlo
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                using (var db = new dbFacturasFinancierosEntities())
                {
                    foreach (var uploadedFile in FileUploadControl.PostedFiles)
                    {
                        string fileName = Path.GetFileName(uploadedFile.FileName);
                        string filePath = Path.Combine(uploadPath, fileName);
                        uploadedFile.SaveAs(filePath);

                        // Para depuración
                        //Console.WriteLine($"Archivo guardado en: {filePath}");
                        //StatusLabel.Text = $"Archivo guardado en: {filePath}";

                        // Procesar el archivo XML
                        XmlDocument doc = new XmlDocument();
                        doc.Load(filePath);

                        // Manejar espacios de nombres
                        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                        nsmgr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/4"); // Ajusta esta URI según la definición del espacio de nombres en tu XML

                        // Extraer Folio y verificar si ya existe
                        string folio = ExtractNodeAttribute(doc, "//cfdi:Comprobante", "Folio", nsmgr);
                        var existingFactura = db.FacturasXml.SingleOrDefault(f => f.Folio == folio);

                        if (existingFactura != null)
                        {                            
                            string mensaje = $"El archivo con Folio {folio} ya ha sido ingresado.";
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", $"alert('{mensaje}')", true);
                            continue;
                        }

                        
                        string subtotalStr = ExtractNodeAttribute(doc, "//cfdi:Comprobante", "SubTotal", nsmgr);

                        
                        string emisorNombre = ExtractNodeAttribute(doc, "//cfdi:Emisor", "Nombre", nsmgr);
                        string receptorNombre = ExtractNodeAttribute(doc, "//cfdi:Receptor", "Nombre", nsmgr);
                        string cantidadStr = ExtractNodeAttribute(doc, "//cfdi:Concepto", "Cantidad", nsmgr);
                        string importeStr = ExtractNodeAttribute(doc, "//cfdi:Concepto", "Importe", nsmgr);
                        string descripcion = ExtractNodeAttribute(doc, "//cfdi:Concepto", "Descripcion", nsmgr);

                        
                        decimal cantidad = decimal.Parse(cantidadStr);
                        decimal importe = decimal.Parse(importeStr);
                        decimal subtotal = decimal.Parse(subtotalStr);

                        
                        var factura = new FacturasXml
                        {
                            EmisorNombre = emisorNombre,
                            ReceptorNombre = receptorNombre,
                            Cantidad = cantidad,
                            Importe = importe,
                            Descripcion = descripcion,
                            SubTotal = subtotal,
                            Folio = folio
                        };

                        db.FacturasXml.Add(factura);
                        db.SaveChanges();
                    }
                }



                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Archivos cargados exitosamente')", true);
                cargarDatos();
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('Por favor, selecciona archivos para subir')", true);
            }
        }

        private string ExtractNodeAttribute(XmlDocument doc, string xPath, string attributeName, XmlNamespaceManager nsmgr)
        {
            XmlNode node = doc.SelectSingleNode(xPath, nsmgr);
            if (node != null && node.Attributes[attributeName] != null)
            {
                return node.Attributes[attributeName].Value;
            }
            return "Atributo no encontrado";
        }

    }
}