using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using RFinancieros_Facturas.Datos;
using RFinancieros_Facturas.Datos.Models;

namespace RFinancieros_Facturas.Reportes
{
    public partial class ReporteEgresoFechas : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }

        }




        protected void btnConsultarDatos_Click(object sender, EventArgs e)
        {
            consultarReporteResumen();
        }

        protected void consultarReporteResumen()
        {
            lblError.Text = "";

            DateTime fechai;
            DateTime fechaf;

            // Validar fecha inicial
            if (!DateTime.TryParse(
                txtFechaInicio.Text.Trim(),
                out fechai))
            {
                lblError.Text =
                    "La fecha de inicio no es válida.";

                lblError.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }

            // Validar fecha final
            if (!DateTime.TryParse(
                txtFechaFin.Text.Trim(),
                out fechaf))
            {
                lblError.Text =
                    "La fecha de fin no es válida.";

                lblError.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }

            // Validar rango
            if (fechai > fechaf)
            {
                lblError.Text =
                    "La fecha de inicio no puede ser mayor que la fecha de fin.";

                lblError.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }

            // RFC
            string rfc =
                txtRfc.Text.Trim();

            // Estatus
            string estatus =
                ddlEstatus.SelectedItem?.Text?.Trim();


            try
            {

                var datos = ObtenerResumen(fechai, fechaf, rfc, estatus);

                var datosEstatus = ObtenerEstatusFacturas(fechai, fechaf, rfc, estatus);

                if (datos == null || datos.Count == 0)
                {
                    lblError.Text = "No se encontraron registros para los criterios seleccionados.";

                    lblError.ForeColor = System.Drawing.Color.Red;

                    dgvResumen.DataSource = null;
                    dgvResumen.DataBind();

                    return;
                }

                if (datosEstatus == null || datosEstatus.Count == 0)
                {
                    lblError.Text = "No se encontraron registros para los criterios seleccionados.";
                    lblError.ForeColor = System.Drawing.Color.Red;

                    dgvEstatusFacturas.DataSource = null;
                    dgvEstatusFacturas.DataBind();

                    return;
                }

                PanelEgresos.Visible = true;
                // Mostrar resultados
                dgvResumen.DataSource = datos;
                dgvResumen.DataBind();




                PanelEstatus.Visible = true;
                dgvEstatusFacturas.DataSource = datosEstatus;
                dgvEstatusFacturas.DataBind();





                lblError.Text =
                    "Se encontraron " +
                    datos.Count +
                    " registros.";

                lblError.ForeColor =
                    System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblError.Text =
                    "Ocurrió un error al consultar: " +
                    ex.Message;

                lblError.ForeColor =
                    System.Drawing.Color.Red;
            }
        }



        protected void btnRptGeneral_Click(object sender, EventArgs e)
        {
            DescargarRptGeneral();
        }



        protected void DescargarRptGeneral()
        {
            lblError.Text = "";

            // =========================================================
            // RFC
            // =========================================================

            string rfc = txtRfc.Text.Trim();

            if (string.IsNullOrWhiteSpace(rfc))
            {
                rfc = null;
            }


            // =========================================================
            // FECHAS
            // =========================================================

            string fechaiStr = txtFechaInicio.Text.Trim();
            string fechafStr = txtFechaFin.Text.Trim();

            DateTime fechai;
            DateTime fechaf;

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
                lblError.Text =
                    "La fecha de inicio no puede ser mayor que la fecha de fin.";

                lblError.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }


            // =========================================================
            // ESTATUS
            // =========================================================

            string estatus =
                ddlEstatus.SelectedItem?.Text?.Trim();

            if (string.IsNullOrEmpty(estatus) ||
                ddlEstatus.SelectedIndex == 0)
            {
                lblError.Text = "Debe seleccionar un estatus.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }


            try
            {
                using (var db = new dbFacturasFinancierosEntities())
                {

                    // =====================================================
                    // PARÁMETROS
                    // =====================================================

                    var parametroFechaInicio =
                        new SqlParameter("@fechai", fechai.Date);

                    var parametroFechaFin =
                        new SqlParameter("@fechaf", fechaf.Date);

                    var parametroRfc =
                        new SqlParameter(
                            "@rfce",
                            (object)rfc ?? DBNull.Value);

                    var parametroEstatus =
                        new SqlParameter("@estatus", estatus);


                    // =====================================================
                    // EJECUTAR SP
                    // =====================================================

                    var datos = db.Database.SqlQuery<ReporteEgresoVM>(
                            "EXEC sel_egreso_fechas " +
                            "@fechai, @fechaf, @rfce, @estatus",

                            parametroFechaInicio,
                            parametroFechaFin,
                            parametroRfc,
                            parametroEstatus

                        ).ToList();


                    // =====================================================
                    // VALIDAR RESULTADOS
                    // =====================================================

                    if (datos == null || datos.Count == 0)
                    {
                        lblError.Text =
                            "No se encontraron registros para los criterios seleccionados.";

                        lblError.ForeColor =
                            System.Drawing.Color.Red;

                        return;
                    }


                    // =====================================================
                    // CREAR EXCEL
                    // =====================================================

                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        var hoja =
                            workbook.Worksheets.Add("Egresos");

                        // =================================================
                        // ENCABEZADOS
                        // =================================================

                        hoja.Cell(1, 1).Value = "Egreso";
                        hoja.Cell(1, 2).Value = "Fecha de Egreso";
                        hoja.Cell(1, 3).Value = "Folio";
                        hoja.Cell(1, 4).Value = "Estado";
                        hoja.Cell(1, 5).Value = "No. ODP";
                        hoja.Cell(1, 6).Value = "Fecha de Ingreso a Ventanilla";
                        hoja.Cell(1, 7).Value = "RFC";
                        hoja.Cell(1, 8).Value = "Emisor";
                        hoja.Cell(1, 9).Value = "Fecha Emisión";
                        hoja.Cell(1, 10).Value = "Factura";
                        hoja.Cell(1, 11).Value = "Importe";
                        hoja.Cell(1, 12).Value = "Fecha Pago";
                        hoja.Cell(1, 13).Value = "Área";
                        hoja.Cell(1, 14).Value = "No. Contrato";
                        hoja.Cell(1, 15).Value = "Suma Egreso SIAFEV";
                        hoja.Cell(1, 16).Value = "Suma Egreso VAREFACT";
                        hoja.Cell(1, 17).Value = "Clave partida";
                        hoja.Cell(1, 18).Value = "Partida";
                        hoja.Cell(1, 19).Value = "Estatus DRF";


                        // =================================================
                        // ESTILO ENCABEZADOS
                        // =================================================

                        var encabezado =
                            hoja.Range("A1:S1");

                        encabezado.Style.Font.Bold = true;

                        encabezado.Style.Font.FontColor =
                            XLColor.Black;

                        encabezado.Style.Fill.BackgroundColor =
                            XLColor.FromHtml("#E7E6E6");

                        encabezado.Style.Alignment.Horizontal =
                            XLAlignmentHorizontalValues.Center;

                        encabezado.Style.Alignment.Vertical =
                            XLAlignmentVerticalValues.Center;

                        encabezado.Style.Alignment.WrapText =
                            true;

                        encabezado.Style.Border.BottomBorder =
                            XLBorderStyleValues.Thin;

                        hoja.Row(1).Height = 30;


                        // =================================================
                        // DATOS
                        // =================================================

                        int fila = 2;

                        foreach (ReporteEgresoVM item in datos)
                        {

                            // -------------------------------------------------
                            // 1. Egreso
                            // -------------------------------------------------

                            hoja.Cell(fila, 1).Value = item.Egreso;


                            // -------------------------------------------------
                            // 2. Fecha de Egreso
                            // -------------------------------------------------
                            // Se escribe como texto para evitar problemas
                            // de conversión de DateTime / ticks.

                            //if (item.FechaEgreso.HasValue)
                            //{
                            //    hoja.Cell(fila, 2).Value =
                            //        item.FechaEgreso.Value.ToString("dd/MM/yyyy");
                            //}

                            if (item.FechaEgreso.HasValue &&
                            item.FechaEgreso.Value > DateTime.MinValue)
                            {
                                hoja.Cell(fila, 2).Value =
                                    item.FechaEgreso.Value.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                hoja.Cell(fila, 2).Value = "Sin Fecha";
                            }


                            // -------------------------------------------------
                            // 3. Folio
                            // -------------------------------------------------

                            hoja.Cell(fila, 3).Value =
                                item.Folio;


                            // -------------------------------------------------
                            // 4. Estado
                            // -------------------------------------------------

                            hoja.Cell(fila, 4).Value =
                                item.Estado;


                            // -------------------------------------------------
                            // 5. No. ODP
                            // -------------------------------------------------

                            hoja.Cell(fila, 5).Value =
                                item.NoOdp;


                            // -------------------------------------------------
                            // 6. Fecha de Ingreso a Ventanilla
                            // -------------------------------------------------

                            //if (item.FechaIngresoVentanilla.HasValue)
                            //{
                            //    hoja.Cell(fila, 6).Value =
                            //        item.FechaIngresoVentanilla.Value
                            //        .ToString("dd/MM/yyyy");
                            //}

                            if (item.FechaVentanilla.HasValue &&
                            item.FechaVentanilla.Value > DateTime.MinValue)
                            {
                                hoja.Cell(fila, 6).Value =
                                    item.FechaVentanilla.Value.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                hoja.Cell(fila, 6).Value = "Sin Fecha";
                            }


                            // -------------------------------------------------
                            // 7. RFC
                            // -------------------------------------------------

                            hoja.Cell(fila, 7).Value =
                                item.Rfce;


                            // -------------------------------------------------
                            // 8. Emisor
                            // -------------------------------------------------

                            hoja.Cell(fila, 8).Value =
                                item.Rsemisor;


                            // -------------------------------------------------
                            // 9. Fecha Emisión
                            // -------------------------------------------------

                            //if (item.FechaEmision.HasValue)
                            //{
                            //    hoja.Cell(fila, 9).Value =
                            //        item.FechaEmision.Value
                            //        .ToString("dd/MM/yyyy");
                            //}

                            if (item.FechaEmision.HasValue &&
                             item.FechaEmision.Value > DateTime.MinValue)
                            {
                                hoja.Cell(fila, 9).Value =
                                    item.FechaEmision.Value.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                hoja.Cell(fila, 9).Value = "Sin Fecha";
                            }


                            // -------------------------------------------------
                            // 10. Factura
                            // -------------------------------------------------

                            hoja.Cell(fila, 10).Value =
                                item.FolioInternoFactura;


                            // -------------------------------------------------
                            // 11. Importe
                            // -------------------------------------------------

                            hoja.Cell(fila, 11).Value =
                                item.Importe;


                            // -------------------------------------------------
                            // 12. Fecha Pago
                            // -------------------------------------------------

                            //if (item.FechaPago.HasValue)
                            //{
                            //    hoja.Cell(fila, 12).Value =
                            //        item.FechaPago.Value
                            //        .ToString("dd/MM/yyyy");
                            //}

                            if (item.FechaPago.HasValue &&
                            item.FechaPago.Value > DateTime.MinValue)
                            {
                                hoja.Cell(fila, 12).Value =
                                    item.FechaPago.Value.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                hoja.Cell(fila, 12).Value = "Sin Fecha";
                            }


                            // -------------------------------------------------
                            // 13. Área
                            // -------------------------------------------------

                            hoja.Cell(fila, 13).Value =
                                item.NombreArea;


                            // -------------------------------------------------
                            // 14. No. Contrato
                            // -------------------------------------------------

                            hoja.Cell(fila, 14).Value =
                                item.nocontrato;


                            // -------------------------------------------------
                            // 15. Suma Egreso SIAFEV
                            // -------------------------------------------------

                            if (item.SumaEgresoSIAFEV.HasValue)
                            {
                                hoja.Cell(fila, 15).Value =
                                    item.SumaEgresoSIAFEV.Value;
                            }


                            // -------------------------------------------------
                            // 16. Suma Egreso VAREFACT
                            // -------------------------------------------------

                            if (item.SumaEgresoVAREFACT.HasValue)
                            {
                                hoja.Cell(fila, 16).Value =
                                    item.SumaEgresoVAREFACT.Value;
                            }


                            // -------------------------------------------------
                            // 17. Clave partida
                            // -------------------------------------------------

                            hoja.Cell(fila, 17).Value =
                                item.clave_partida;


                            // -------------------------------------------------
                            // 18. Partida
                            // -------------------------------------------------

                            hoja.Cell(fila, 18).Value =
                                item.partida;


                            // -------------------------------------------------
                            // 19. Estatus DRF
                            // -------------------------------------------------

                            hoja.Cell(fila, 19).Value =
                                item.EstatusDRF;


                            fila++;
                        }


                        int ultimaFila = fila - 1;


                        // =================================================
                        // CENTRAR TODO EL CONTENIDO
                        // =================================================

                        var rangoDatos = hoja.Range(
                            1,
                            1,
                            ultimaFila,
                            19);

                        rangoDatos.Style.Alignment.Horizontal =
                            XLAlignmentHorizontalValues.Center;

                        rangoDatos.Style.Alignment.Vertical =
                            XLAlignmentVerticalValues.Center;



                        // =================================================
                        // FORMATO MONEDA
                        // =================================================

                        if (ultimaFila >= 2)
                        {
                            // Importe
                            hoja.Range(
                                2,
                                11,
                                ultimaFila,
                                11)
                                .Style.NumberFormat.Format =
                                "$#,##0.00";


                            // Suma Egreso SIAFEV
                            hoja.Range(
                                2,
                                15,
                                ultimaFila,
                                15)
                                .Style.NumberFormat.Format =
                                "$#,##0.00";


                            // Suma Egreso VAREFACT
                            hoja.Range(
                                2,
                                16,
                                ultimaFila,
                                16)
                                .Style.NumberFormat.Format =
                                "$#,##0.00";
                        }


                        // =================================================
                        // RANGO COMPLETO
                        // =================================================

                        var rangoDatos2 =
                            hoja.Range(
                                1,
                                1,
                                ultimaFila,
                                19);


                        // =================================================
                        // BORDES
                        // =================================================

                        rangoDatos.Style.Border.InsideBorder =
                            XLBorderStyleValues.Thin;

                        rangoDatos.Style.Border.OutsideBorder =
                            XLBorderStyleValues.Thin;


                        // =================================================
                        // ALINEACIÓN
                        // =================================================

                        rangoDatos.Style.Alignment.Vertical =
                            XLAlignmentVerticalValues.Center;


                        if (ultimaFila >= 2)
                        {
                            // Importe
                            hoja.Range(
                                2,
                                11,
                                ultimaFila,
                                11)
                                .Style.Alignment.Horizontal =
                                XLAlignmentHorizontalValues.Right;


                            // SIAFEV
                            hoja.Range(
                                2,
                                15,
                                ultimaFila,
                                15)
                                .Style.Alignment.Horizontal =
                                XLAlignmentHorizontalValues.Right;


                            // VAREFACT
                            hoja.Range(
                                2,
                                16,
                                ultimaFila,
                                16)
                                .Style.Alignment.Horizontal =
                                XLAlignmentHorizontalValues.Right;
                        }


                        // =================================================
                        // CENTRAR ENCABEZADOS
                        // =================================================

                        hoja.Range("A1:S1")
                            .Style.Alignment.Horizontal =
                            XLAlignmentHorizontalValues.Center;


                        hoja.Range("A1:S1")
                            .Style.Alignment.Vertical =
                            XLAlignmentVerticalValues.Center;


                        // =================================================
                        // AJUSTAR COLUMNAS
                        // =================================================

                        for (int i = 1; i <= 19; i++)
                        {
                            hoja.Column(i).AdjustToContents();


                            // Ancho máximo
                            if (hoja.Column(i).Width > 40)
                            {
                                hoja.Column(i).Width = 40;
                            }


                            // Ancho mínimo
                            if (hoja.Column(i).Width < 10)
                            {
                                hoja.Column(i).Width = 10;
                            }
                        }


                        // =================================================
                        // ANCHOS ESPECÍFICOS
                        // =================================================

                        // Fechas
                        hoja.Column(2).Width = 15;
                        hoja.Column(6).Width = 22;
                        hoja.Column(9).Width = 15;
                        hoja.Column(12).Width = 15;


                        // RFC
                        hoja.Column(7).Width = 16;


                        // Folio
                        hoja.Column(3).Width = 15;


                        // Importe
                        hoja.Column(11).Width = 16;


                        // SIAFEV
                        hoja.Column(15).Width = 20;


                        // VAREFACT
                        hoja.Column(16).Width = 22;


                        // Partida
                        hoja.Column(18).Width = 40;


                        // =================================================
                        // ENVOLVER TEXTO EN PARTIDA Y ÁREA
                        // =================================================

                        hoja.Column(13)
                            .Style.Alignment.WrapText = true;

                        hoja.Column(18)
                            .Style.Alignment.WrapText = true;


                        // =================================================
                        // CONGELAR ENCABEZADO
                        // =================================================

                        hoja.SheetView.FreezeRows(1);


                        // =================================================
                        // DESCARGAR
                        // =================================================

                        using (MemoryStream stream =
                            new MemoryStream())
                        {
                            workbook.SaveAs(stream);

                            byte[] archivo =
                                stream.ToArray();


                            Response.Clear();
                            Response.ClearContent();
                            Response.ClearHeaders();

                            Response.Buffer = true;

                            Response.ContentType =
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


                            string nombreArchivo =
                                "ReporteEgresos_" +
                                fechai.ToString("yyyyMMdd") +
                                "_" +
                                fechaf.ToString("yyyyMMdd") +
                                ".xlsx";


                            Response.AddHeader(
                                "Content-Disposition",
                                "attachment; filename=" +
                                nombreArchivo);


                            Response.AddHeader(
                                "Content-Length",
                                archivo.Length.ToString());


                            Response.TrySkipIisCustomErrors =
                                true;


                            Response.BinaryWrite(archivo);

                            Response.Flush();

                            HttpContext.Current
                                .ApplicationInstance
                                .CompleteRequest();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text =
                    "Ocurrió un error al generar el archivo: " +
                    ex.Message;

                lblError.ForeColor =
                    System.Drawing.Color.Red;
            }
        }



        private List<ReporteEgresoResumenVM> ObtenerResumen(DateTime fechai, DateTime fechaf, string rfc, string estatus)
        {
            using (var db = new dbFacturasFinancierosEntities())
            {
                var parametroFechaInicio =
                    new SqlParameter("@fechai", fechai.Date);

                var parametroFechaFin =
                    new SqlParameter("@fechaf", fechaf.Date);

                var parametroRfc =
                    new SqlParameter("@rfce", rfc);

                var parametroEstatus =
                    new SqlParameter("@estatus", estatus);

                var datos =
                    db.Database.SqlQuery<ReporteEgresoResumenVM>(
                        "EXEC sel_egreso_resumen " +
                        "@fechai, @fechaf, @rfce, @estatus",
                        parametroFechaInicio,
                        parametroFechaFin,
                        parametroRfc,
                        parametroEstatus
                    ).ToList();

                return datos;
            }
        }


        private List<ReporteEgresoEstatusDRF> ObtenerEstatusFacturas(DateTime fechai, DateTime fechaf, string rfc, string estatus)
        {
            using (var db = new dbFacturasFinancierosEntities())
            {
                var parametroFechaInicio =
                    new SqlParameter("@fechai", fechai.Date);

                var parametroFechaFin =
                    new SqlParameter("@fechaf", fechaf.Date);

                var parametroRfc =
                    new SqlParameter("@rfce", rfc);

                var parametroEstatus =
                    new SqlParameter("@estatus", estatus);

                var datos =
                    db.Database.SqlQuery<ReporteEgresoEstatusDRF>(
                        "EXEC sel_egreso_estatus_DRF " +
                        "@fechai, @fechaf, @rfce, @estatus",
                        parametroFechaInicio,
                        parametroFechaFin,
                        parametroRfc,
                        parametroEstatus
                    ).ToList();

                return datos;
            }
        }



        protected void generarRptResumen()
        {
            lblError.Text = "";

            DateTime fechai;
            DateTime fechaf;


            // ==========================================
            // 1. VALIDAR FECHA INICIO
            // ==========================================

            if (!DateTime.TryParse(txtFechaInicio.Text.Trim(), out fechai))
            {
                lblError.Text = "La fecha de inicio no es válida.";

                lblError.ForeColor = System.Drawing.Color.Red;

                return;
            }


            // ==========================================
            // 2. VALIDAR FECHA FIN
            // ==========================================

            if (!DateTime.TryParse(
                txtFechaFin.Text.Trim(),
                out fechaf))
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


            // ==========================================
            // 3. OBTENER FILTROS
            // ==========================================

            string rfc =
                txtRfc.Text.Trim();

            string estatus =
                ddlEstatus.SelectedItem?.Text?.Trim();


            try
            {
                // ==========================================
                // 4. OBTENER DATOS DEL SP
                // ==========================================

                var datos =
                    ObtenerResumen(
                        fechai,
                        fechaf,
                        rfc,
                        estatus);


                if (datos == null || datos.Count == 0)
                {
                    lblError.Text =
                        "No se encontraron registros para exportar.";

                    lblError.ForeColor =
                        System.Drawing.Color.Red;

                    return;
                }


                // ==========================================
                // 5. CREAR LIBRO DE EXCEL
                // ==========================================

                using (XLWorkbook workbook =
                    new XLWorkbook())
                {
                    var hoja =
                        workbook.Worksheets.Add("Resumen");


                    // ==========================================
                    // 6. ENCABEZADOS
                    // ==========================================

                    hoja.Cell(1, 1).Value =
                        "Área";

                    hoja.Cell(1, 2).Value =
                        "Cantidad Facturas";

                    hoja.Cell(1, 3).Value =
                        "Total Importe";


                    // ==========================================
                    // 7. ESTILO ENCABEZADOS
                    // ==========================================

                    var encabezado =
                        hoja.Range("A1:C1");

                    encabezado.Style.Font.Bold =
                        true;

                    encabezado.Style.Fill.BackgroundColor =
                        XLColor.FromHtml("#E7E6E6");

                    encabezado.Style.Font.FontColor =
                        XLColor.Black;

                    encabezado.Style.Alignment.Horizontal =
                        XLAlignmentHorizontalValues.Center;

                    encabezado.Style.Alignment.Vertical =
                        XLAlignmentVerticalValues.Center;

                    encabezado.Style.Alignment.WrapText =
                        true;

                    encabezado.Style.Border.BottomBorder =
                        XLBorderStyleValues.Thin;

                    hoja.Row(1).Height = 30;


                    // ==========================================
                    // 8. INSERTAR DATOS
                    // ==========================================

                    int fila = 2;

                    foreach (ReporteEgresoResumenVM item in datos)
                    {
                        hoja.Cell(fila, 1).Value =
                            item.Area;

                        hoja.Cell(fila, 2).Value =
                            item.CantidadFacturas;

                        if (item.TotalImporte.HasValue)
                        {
                            hoja.Cell(fila, 3).Value =
                                item.TotalImporte.Value;
                        }

                        fila++;
                    }


                    int ultimaFila =
                        fila - 1;


                    // ==========================================
                    // 9. FORMATO MONEDA
                    // ==========================================

                    if (ultimaFila >= 2)
                    {
                        hoja.Range(
                            2,
                            3,
                            ultimaFila,
                            3)
                            .Style.NumberFormat.Format =
                            "$#,##0.00";
                    }


                    // ==========================================
                    // 10. CENTRAR CONTENIDO
                    // ==========================================

                    var rangoContenido =
                        hoja.Range(
                            1,
                            1,
                            ultimaFila,
                            3);

                    rangoContenido.Style.Alignment.Horizontal =
                        XLAlignmentHorizontalValues.Center;

                    rangoContenido.Style.Alignment.Vertical =
                        XLAlignmentVerticalValues.Center;


                    // ==========================================
                    // 11. BORDES
                    // ==========================================

                    rangoContenido.Style.Border.InsideBorder =
                        XLBorderStyleValues.Thin;

                    rangoContenido.Style.Border.OutsideBorder =
                        XLBorderStyleValues.Thin;


                    // ==========================================
                    // 12. AJUSTAR COLUMNAS
                    // ==========================================

                    for (int i = 1; i <= 3; i++)
                    {
                        hoja.Column(i).AdjustToContents();

                        // Ancho máximo
                        if (hoja.Column(i).Width > 40)
                        {
                            hoja.Column(i).Width = 40;
                        }

                        // Ancho mínimo
                        if (hoja.Column(i).Width < 12)
                        {
                            hoja.Column(i).Width = 12;
                        }
                    }


                    // ==========================================
                    // 13. CONGELAR ENCABEZADO
                    // ==========================================

                    hoja.SheetView.FreezeRows(1);


                    // ==========================================
                    // 14. GENERAR ARCHIVO EN MEMORIA
                    // ==========================================

                    using (MemoryStream stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        byte[] archivo = stream.ToArray();


                        Response.Clear();
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.Buffer = true;

                        Response.ContentType =
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                        string nombreArchivo =
                            "ResumenEgresos_" +
                            fechai.ToString("yyyyMMdd") +
                            "_" +
                            fechaf.ToString("yyyyMMdd") +
                            ".xlsx";

                        Response.AddHeader(
                            "Content-Disposition",
                            "attachment; filename=" + nombreArchivo);

                        Response.AddHeader(
                            "Content-Length",
                            archivo.Length.ToString());

                        Response.OutputStream.Write(
                            archivo,
                            0,
                            archivo.Length);

                        Response.Flush();

                        // Evitar que WebForms agregue contenido HTML
                        Response.SuppressContent = true;

                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                        return;
                    }


                }
            }
            catch (Exception ex)
            {
                lblError.Text =
                    "ERROR: " +
                    ex.GetType().Name +
                    " - " +
                    ex.Message;

                lblError.ForeColor =
                    System.Drawing.Color.Red;
            }
        }



        protected void btnRptResumen_Click(object sender, EventArgs e)
        {
            generarRptResumen();
        }


        protected void generarRptEstatus()
        {
            lblError.Text = "";

            DateTime fechai;
            DateTime fechaf;


            if (!DateTime.TryParse(txtFechaInicio.Text.Trim(), out fechai))
            {
                lblError.Text = "La fecha de inicio no es válida.";

                lblError.ForeColor = System.Drawing.Color.Red;

                return;
            }




            if (!DateTime.TryParse(
                txtFechaFin.Text.Trim(),
                out fechaf))
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


            string rfc =
                txtRfc.Text.Trim();

            string estatus =
                ddlEstatus.SelectedItem?.Text?.Trim();


            try
            {


                var datos = ObtenerEstatusFacturas(fechai, fechaf, rfc, estatus);


                if (datos == null || datos.Count == 0)
                {
                    lblError.Text =
                        "No se encontraron registros para exportar.";

                    lblError.ForeColor =
                        System.Drawing.Color.Red;

                    return;
                }



                using (XLWorkbook workbook =
                    new XLWorkbook())
                {
                    var hoja =
                        workbook.Worksheets.Add("Resumen");


                    hoja.Cell(1, 1).Value =
                        "Estatus DRF";

                    hoja.Cell(1, 2).Value =
                        "Cantidad Facturas";

                    hoja.Cell(1, 3).Value =
                        "Total Importe";


                    var encabezado =
                        hoja.Range("A1:C1");

                    encabezado.Style.Font.Bold =
                        true;

                    encabezado.Style.Fill.BackgroundColor =
                        XLColor.FromHtml("#E7E6E6");

                    encabezado.Style.Font.FontColor =
                        XLColor.Black;

                    encabezado.Style.Alignment.Horizontal =
                        XLAlignmentHorizontalValues.Center;

                    encabezado.Style.Alignment.Vertical =
                        XLAlignmentVerticalValues.Center;

                    encabezado.Style.Alignment.WrapText =
                        true;

                    encabezado.Style.Border.BottomBorder =
                        XLBorderStyleValues.Thin;

                    hoja.Row(1).Height = 30;


                    int fila = 2;

                    foreach (ReporteEgresoEstatusDRF item in datos)
                    {
                        hoja.Cell(fila, 1).Value =
                            item.Estatus;

                        hoja.Cell(fila, 2).Value =
                            item.CantidadFacturas;

                        if (item.TotalImporte.HasValue)
                        {
                            hoja.Cell(fila, 3).Value =
                                item.TotalImporte.Value;
                        }

                        fila++;
                    }


                    int ultimaFila =
                        fila - 1;


                    if (ultimaFila >= 2)
                    {
                        hoja.Range(
                            2,
                            3,
                            ultimaFila,
                            3)
                            .Style.NumberFormat.Format =
                            "$#,##0.00";
                    }



                    var rangoContenido =
                        hoja.Range(
                            1,
                            1,
                            ultimaFila,
                            3);

                    rangoContenido.Style.Alignment.Horizontal =
                        XLAlignmentHorizontalValues.Center;

                    rangoContenido.Style.Alignment.Vertical =
                        XLAlignmentVerticalValues.Center;




                    rangoContenido.Style.Border.InsideBorder =
                        XLBorderStyleValues.Thin;

                    rangoContenido.Style.Border.OutsideBorder =
                        XLBorderStyleValues.Thin;


                    for (int i = 1; i <= 3; i++)
                    {
                        hoja.Column(i).AdjustToContents();

                        // Ancho máximo
                        if (hoja.Column(i).Width > 40)
                        {
                            hoja.Column(i).Width = 40;
                        }

                        // Ancho mínimo
                        if (hoja.Column(i).Width < 12)
                        {
                            hoja.Column(i).Width = 12;
                        }
                    }



                    hoja.SheetView.FreezeRows(1);




                    using (MemoryStream stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        byte[] archivo = stream.ToArray();


                        Response.Clear();
                        Response.ClearContent();
                        Response.ClearHeaders();

                        Response.Buffer = true;

                        Response.ContentType =
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                        string nombreArchivo =
                            "ReporteEstatus_" +
                            fechai.ToString("yyyyMMdd") +
                            "_" +
                            fechaf.ToString("yyyyMMdd") +
                            ".xlsx";

                        Response.AddHeader(
                            "Content-Disposition",
                            "attachment; filename=" + nombreArchivo);

                        Response.AddHeader(
                            "Content-Length",
                            archivo.Length.ToString());

                        Response.OutputStream.Write(
                            archivo,
                            0,
                            archivo.Length);

                        Response.Flush();

                        // Evitar que WebForms agregue contenido HTML
                        Response.SuppressContent = true;

                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                        return;
                    }


                }
            }
            catch (Exception ex)
            {
                lblError.Text =
                    "ERROR: " +
                    ex.GetType().Name +
                    " - " +
                    ex.Message;

                lblError.ForeColor =
                    System.Drawing.Color.Red;
            }
        }


        protected void btnRptEstatus_Click(object sender, EventArgs e)
        {
            generarRptEstatus();
        }
    }
}