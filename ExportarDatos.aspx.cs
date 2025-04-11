using ClosedXML.Excel;
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
using WebGrease.Activities;

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

        //protected void BtnExportar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //        ExcelPackage excel = new ExcelPackage(); ;
        //        string fi = string.Format("{0:d}", tbfechaini.Text);
        //        string ff = string.Format("{0:d}", tbfechafin.Text);

        //        var informe = ctx.sel_facturas_fecha(Convert.ToDateTime(fi), Convert.ToDateTime(ff)).ToList();

        //        var workSheet = excel.Workbook.Worksheets.Add("Facturas");
        //        workSheet.Cells[1, 1].LoadFromCollection(informe, true);

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            Response.AddHeader("content-disposition", "attachment;  filename=Total_Activides.xlsx");
        //            excel.SaveAs(memoryStream);
        //            memoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //            Response.Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _ = ex.Message;
        //    }
        //}




        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo libro de trabajo
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Facturas");

                    // Cargar datos desde el procedimiento almacenado

                    var informe = ctx.sel_facturas_fecha(Convert.ToDateTime(tbfechaini.Text), Convert.ToDateTime(tbfechafin.Text)).ToList();
                    //sel_facturas_fecha
                    if (informe.Any())
                    {
                        // Obtener las propiedades del primer objeto (supone que todos los objetos tienen las mismas propiedades)
                        var properties = informe.First().GetType().GetProperties();

                        // Crear encabezados en la primera fila con los nombres de las propiedades
                        for (int i = 0; i < properties.Length; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = properties[i].Name; // Coloca el nombre de la propiedad como encabezado
                        }

                        // Agregar los valores en las filas siguientes
                        int row = 2;
                        foreach (var item in informe)
                        {
                            for (int col = 0; col < properties.Length; col++)
                            {
                                try
                                {
                                    var value = properties[col].GetValue(item);

                                    // Manejar diferentes tipos de datos
                                    if (value == null)
                                    {
                                        worksheet.Cell(row, col + 1).Value = "";  // Si es nulo, coloca una cadena vacía
                                    }
                                    else if (value is DateTime)
                                    {
                                        worksheet.Cell(row, col + 1).Value = ((DateTime)value).ToString("yyyy-MM-dd");  // Asegura que las fechas tengan el formato correcto
                                    }
                                    else if (value is decimal || value is double || value is float)
                                    {
                                        worksheet.Cell(row, col + 1).Value = Convert.ToDouble(value);  // Asegura que los valores numéricos se conviertan correctamente
                                    }
                                    else
                                    {
                                        worksheet.Cell(row, col + 1).Value = value.ToString();  // Convierte cualquier otro tipo de dato a cadena
                                    }
                                }
                                catch (Exception ex)
                                {
                                    worksheet.Cell(row, col + 1).Value = "Error";  // Si ocurre un error en la conversión, colocar "Error" en la celda
                                }
                            }
                            row++;
                        }
                    }

                    // Ajustar el ancho de todas las columnas automáticamente
                    worksheet.Columns().AdjustToContents();

                    // Guardar el archivo Excel en un MemoryStream
                    using (var memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);

                        // Exportar a Excel
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=Facturas.xlsx");
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                //lblError.Text = "Error al exportar: " + ex.Message;
            }

        }

    }
}