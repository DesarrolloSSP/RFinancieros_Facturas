using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.Word;
using Microsoft.Reporting.WebForms;
using RFinancieros_Facturas.Datos;
using RFinancieros_Facturas.Funciones;


namespace RFinancieros_Facturas.Reportes
{
    public partial class ReporteAuditoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }



        public List<AuditoriaResult> ObtenerAuditoria(
     DateTime fechaInicio,
     DateTime fechaFin,
     string rfc,
     string estatus)
        {
          


            var resultados = new List<AuditoriaResult>();
            string connString = ConfigurationManager
                .ConnectionStrings["dbFacturasFinancieros"].ConnectionString;

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand("sel_egreso_fechas_Auditoria", conn))
            {
                cmd.CommandTimeout = 120; 
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@fechai", SqlDbType.Date).Value = fechaInicio.Date;
                cmd.Parameters.Add("@fechaf", SqlDbType.Date).Value = fechaFin.Date;

                cmd.Parameters.Add("@rfce", SqlDbType.VarChar, 13)
                    .Value = (object)rfc ?? DBNull.Value;

                cmd.Parameters.Add("@estatus", SqlDbType.VarChar, 20)
                    .Value = (object)estatus ?? DBNull.Value;

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultados.Add(new AuditoriaResult
                        {
                            RFC = reader["RFC"].ToString(),
                            RazonSocial = reader["RazonSocial"].ToString(),
                            NombrePartida = reader["NombrePartida"].ToString(),
                            FechaEmision = (DateTime)reader["FechaEmision"],
                            ImporteTotalOdp = (decimal)reader["ImporteTotalOdp"],
                            NoOdp = reader["NoOdp"].ToString(),
                            Entregable = reader["Entregable"].ToString(),
                            ImporteTotalPorOdp = (decimal)reader["ImporteTotalPorOdp"],
                            clave_partida = reader["clave_partida"].ToString(),
                            FolioInternoFactura = reader["FolioInternoFactura"].ToString(),
                            Egreso = reader["Egreso"].ToString(),
                            nocontrato = reader["nocontrato"].ToString()
                        });
                    }
                }
            }

            return resultados;
        }


        protected void MostrarReporte2()
        {
            string rfc = string.IsNullOrWhiteSpace(txtRfc.Text) ? null : txtRfc.Text.Trim();
            string estatus = ddlEstatus.SelectedItem.Text.Trim();




            DateTime fechaInicio = DateTime.Parse(txtFechaInicio.Text);
            DateTime fechaFin = DateTime.Parse(txtFechaFin.Text);



            var datos = ObtenerAuditoria(fechaInicio, fechaFin, rfc, estatus);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Auditoría");

                ws.Cell(1, 1).Value = "REPORTE DE AUDITORÍA";
                ws.Range(1, 1, 1, 9).Merge().Style
                    .Font.SetBold().Font.FontSize = 14;

                ws.Cell(3, 1).Value = "RFC";
                ws.Cell(3, 2).Value = "Razón Social";
                ws.Cell(3, 3).Value = "Partida";
                ws.Cell(3, 4).Value = "Fecha";
                ws.Cell(3, 5).Value = "No Odp";
                ws.Cell(3, 6).Value = "Clave";
                ws.Cell(3, 7).Value = "Folio";
                ws.Cell(3, 8).Value = "Egreso";
                ws.Cell(3, 9).Value = "No contrato";

                int fila = 4;
                foreach (var item in datos)
                {
                    ws.Cell(fila, 1).Value = item.RFC;
                    ws.Cell(fila, 2).Value = item.RazonSocial;
                    ws.Cell(fila, 3).Value = item.NombrePartida;
                    ws.Cell(fila, 4).Value = item.FechaEmision;
                    ws.Cell(fila, 5).Value = item.NoOdp;
                    ws.Cell(fila, 6).Value = item.clave_partida;
                    ws.Cell(fila, 7).Value = item.FolioInternoFactura;
                    ws.Cell(fila, 8).Value = item.Egreso;
                    ws.Cell(fila, 9).Value = item.nocontrato;
                    fila++;
                }


                ws.RangeUsed().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Columns().AdjustToContents();
                ws.Column(4).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Column(8).Style.NumberFormat.Format = "$ #,##0.00";

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ReporteAuditoria.xlsx");

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        //        protected void MostrarReporte2()
        //        {
        //            string rfc = string.IsNullOrWhiteSpace(txtRfc.Text) ? null : txtRfc.Text.Trim();
        //            DateTime fechaInicio = DateTime.Parse(txtFechaInicio.Text);
        //            DateTime fechaFin = DateTime.Parse(txtFechaFin.Text);
        //            string estatus = ddlEstatus.SelectedItem.Text.Trim();

        //            using (var ctx = new dbFacturasFinancierosEntities())
        //            {
        //                var datos = ctx.Database.SqlQuery<sel_egreso_fechas_Auditoria_Result>(
        //    "EXEC sel_egreso_fechas_Auditoria @fechai, @fechaf, @rfce, @estatus",
        //    new SqlParameter("@fechai", fechaInicio),   // DateTime directo
        //    new SqlParameter("@fechaf", fechaFin),      // DateTime directo
        //    new SqlParameter("@rfce", (object)rfc ?? DBNull.Value),
        //    new SqlParameter("@estatus", (object)estatus ?? DBNull.Value)
        //).ToList();




        //                using (XLWorkbook wb = new XLWorkbook())
        //                {
        //                    var ws = wb.Worksheets.Add("Auditoría");

        //                    ws.Cell(1, 1).Value = "REPORTE DE AUDITORÍA";
        //                    ws.Range(1, 1, 1, 9).Merge().Style
        //                        .Font.SetBold().Font.FontSize = 14;


        //                    ws.Cell(3, 1).Value = "RFC";
        //                    ws.Cell(3, 2).Value = "Razón Social";
        //                    ws.Cell(3, 3).Value = "Partida";
        //                    ws.Cell(3, 4).Value = "Fecha";
        //                    ws.Cell(3, 5).Value = "No Odp";
        //                    ws.Cell(3, 6).Value = "Clave";
        //                    ws.Cell(3, 7).Value = "Folio";
        //                    ws.Cell(3, 8).Value = "Egreso";
        //                    ws.Cell(3, 9).Value = "No contrato";

        //                    int fila = 4;
        //                    foreach (var item in datos)
        //                    {
        //                        ws.Cell(fila, 1).Value = item.RFC;
        //                        ws.Cell(fila, 2).Value = item.RazonSocial;
        //                        ws.Cell(fila, 3).Value = item.NombrePartida;
        //                        ws.Cell(fila, 4).Value = item.FechaEmision;
        //                        ws.Cell(fila, 5).Value = item.NoOdp;
        //                        ws.Cell(fila, 6).Value = item.clave_partida;
        //                        ws.Cell(fila, 7).Value = item.FolioInternoFactura;
        //                        ws.Cell(fila, 8).Value = item.Egreso;
        //                        ws.Cell(fila, 9).Value = item.nocontrato;
        //                        fila++;
        //                    }

        //                    ws.Columns().AdjustToContents();

        //                    // Opcional: formato de fecha y moneda
        //                    ws.Column(4).Style.DateFormat.Format = "dd/MM/yyyy";
        //                    ws.Column(8).Style.NumberFormat.Format = "$ #,##0.00";

        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                    Response.AddHeader("content-disposition", "attachment;filename=ReporteAuditoria.xlsx");

        //                    using (MemoryStream ms = new MemoryStream())
        //                    {
        //                        wb.SaveAs(ms);
        //                        ms.WriteTo(Response.OutputStream);
        //                        Response.Flush();
        //                        Response.End();
        //                    }
        //                }
        //            }
        //        }

        protected void MostrarReporte()
        {
            //egreso.ProcessingMode = ProcessingMode.Remote;
            ////Le indicamos la URL donde se encuentra hospedado Reporting Services
            //egreso.ServerReport.ReportServerUrl = new Uri("http://10.8.3.199/reportserver");
            ////Le indicamos la carpeta y el Reporte que deseamos Ver
            //egreso.ServerReport.ReportPath = "/financieros/ListadoDeEgresoFechasAuditoria";




        }

        protected void btnVerReporte_Click(object sender, EventArgs e)
        {
            // Limpiar mensaje de error
            lblError.Text = "";

            // 1. Validar RFC
            string rfc = txtRfc.Text.Trim();
            if (string.IsNullOrWhiteSpace(rfc))
            {
                //lblError.Text = "Debe ingresar un RFC.";
                //lblError.ForeColor = System.Drawing.Color.Red;
                //return;
                rfc = null;

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


            //// 4. Configurar parámetros del reporte
            //ReportParameter[] parameters = new ReportParameter[4];
            //parameters[0] = new ReportParameter("rfce", rfc);
            //parameters[1] = new ReportParameter("fechai", fechai.ToString("yyyy-MM-dd"));
            //parameters[2] = new ReportParameter("fechaf", fechaf.ToString("yyyy-MM-dd"));
            //parameters[3] = new ReportParameter("estatus", idEstatus);

            //// 5. Configurar ReportViewer
            //egreso.ProcessingMode = ProcessingMode.Remote;
            //egreso.ServerReport.ReportServerUrl = new Uri("http://10.8.3.199/reportserver");
            //egreso.ServerReport.ReportPath = "/financieros/ListadoDeEgresoFechasAuditoria";

            //// 6. Asignar parámetros y cargar reporte
            //egreso.ServerReport.SetParameters(parameters);
            //egreso.ServerReport.Refresh();
            //MostrarReporte();
            MostrarReporte2();

        }

    }
}