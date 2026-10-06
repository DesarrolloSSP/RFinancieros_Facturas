using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using RFinancieros_Facturas.Datos.Entities;

using ClosedXML.Excel;


namespace RFinancieros_Facturas.Services
{

    public class ExcelService
    {
        public List<PagoExcelVM> LeerExcel(string rutaArchivo)
        {
            ValidarArchivo(rutaArchivo);

            List<PagoExcelVM> lista = new List<PagoExcelVM>();

            using (var workbook = new XLWorkbook(rutaArchivo))
            {
                var hoja = workbook.Worksheet(1);

                IXLRow filaEncabezados = ObtenerFilaEncabezados(hoja);

                var encabezados = ObtenerEncabezados(hoja);

                ValidarEncabezados(encabezados);

                foreach (IXLRow fila in hoja.RowsUsed())
                {
                    if (fila.RowNumber() <= filaEncabezados.RowNumber())
                        continue;

                    lista.Add(LeerFila(fila, encabezados));
                }
            }

            return lista;
        }

        #region Métodos privados

        private void ValidarArchivo(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
                throw new Exception("No existe el archivo.");

            string extension = Path.GetExtension(rutaArchivo).ToLower();

            if (extension != ".xlsx")
                throw new Exception("Solo se permiten archivos .xlsx");
        }

       
        private Dictionary<string, int> ObtenerEncabezados(IXLWorksheet hoja)
        {
            Dictionary<string, int> encabezados = new Dictionary<string, int>();

            IXLRow filaEncabezados = ObtenerFilaEncabezados(hoja);

            foreach (IXLCell celda in filaEncabezados.CellsUsed())
            {
                string nombre = celda.GetString().Trim().ToUpper();

                if (!encabezados.ContainsKey(nombre))
                {
                    encabezados.Add(nombre, celda.Address.ColumnNumber);
                }
            }

            return encabezados;
        }

        private void ValidarEncabezados(Dictionary<string, int> encabezados)
        {
            string[] requeridos =
            {
                "SECTOR/DEPENDENCIA",
                "BENEFICIARIO",
                "NO. DE BENEFICIARIO",
                "NO. FOLIO",
                "CONCEPTO",
                "FECHA",
                "MONTO",
                "PAGADO",
                "OBSERVACIONES",
                "FECHA DE PAGO",
                "NUMERO DE PAGO O REFERENCIA DE PAGO",
                "FUENTE FTTO.",
                "DESCRIPCION DE LA FUENTE",
                "OC",
                "CLASIFICACIÓN PROVEEDOR",
                "SECTOR",
                "DESCRIPCION DE SECTOR",
                "TIPO DE GASTO",
                "CUENTA DE CARGO",
                "BANCO CTA CARGO",
                "CUENTA ABONO",
                "BANCO CTA ABONO"
            };

            foreach (string columna in requeridos)
            {
                if (!encabezados.ContainsKey(columna.ToUpper()))
                    throw new Exception($"No existe la columna '{columna}'.");
            }
        }

        private PagoExcelVM LeerFila(IXLRow fila, Dictionary<string, int> encabezados)
        {
            PagoExcelVM pago = new PagoExcelVM();

            pago.SectorDependencia = ObtenerTexto(fila, encabezados, "SECTOR/DEPENDENCIA");
            pago.Beneficiario = ObtenerTexto(fila, encabezados, "BENEFICIARIO");
            pago.NumeroBeneficiario = ObtenerTexto(fila, encabezados, "NO. DE BENEFICIARIO");
            pago.Folio = ObtenerTexto(fila, encabezados, "NO. FOLIO");
            pago.Concepto = ObtenerTexto(fila, encabezados, "CONCEPTO");

            pago.Fecha = ObtenerFecha(fila, encabezados, "FECHA");

            pago.Monto = ObtenerDecimal(fila, encabezados, "MONTO");

            pago.Pagado = ObtenerTexto(fila, encabezados, "PAGADO");
            pago.Observaciones = ObtenerTexto(fila, encabezados, "OBSERVACIONES");

            pago.FechaPago = ObtenerFecha(fila, encabezados, "FECHA DE PAGO");

            pago.ReferenciaPago = ObtenerTexto(fila, encabezados, "NUMERO DE PAGO O REFERENCIA DE PAGO");

            pago.Fuente = ObtenerTexto(fila, encabezados, "FUENTE FTTO.");

            pago.DescripcionFuente = ObtenerTexto(fila, encabezados, "DESCRIPCION DE LA FUENTE");

            pago.OC = ObtenerTexto(fila, encabezados, "OC");

            pago.ClasificacionProveedor = ObtenerTexto(fila, encabezados, "CLASIFICACIÓN PROVEEDOR");

            pago.Sector = ObtenerTexto(fila, encabezados, "SECTOR");

            pago.DescripcionSector = ObtenerTexto(fila, encabezados, "DESCRIPCION DE SECTOR");

            pago.TipoGasto = ObtenerTexto(fila, encabezados, "TIPO DE GASTO");

            pago.CuentaCargo = ObtenerTexto(fila, encabezados, "CUENTA DE CARGO");

            pago.BancoCargo = ObtenerTexto(fila, encabezados, "BANCO CTA CARGO");

            pago.CuentaAbono = ObtenerTexto(fila, encabezados, "CUENTA ABONO");

            pago.BancoAbono = ObtenerTexto(fila, encabezados, "BANCO CTA ABONO");

            return pago;
        }

        private string ObtenerTexto(IXLRow fila,
                                    Dictionary<string, int> encabezados,
                                    string columna)
        {
            return fila.Cell(encabezados[columna]).GetString().Trim();
        }

        private DateTime? ObtenerFecha(IXLRow fila,
                                       Dictionary<string, int> encabezados,
                                       string columna)
        {
            var celda = fila.Cell(encabezados[columna]);

            if (celda.IsEmpty())
                return null;

            DateTime fecha;

            if (DateTime.TryParse(celda.GetString(), out fecha))
                return fecha;

            return null;
        }

        private decimal? ObtenerDecimal(IXLRow fila,
                                        Dictionary<string, int> encabezados,
                                        string columna)
        {
            var celda = fila.Cell(encabezados[columna]);

            if (celda.IsEmpty())
                return null;

            decimal numero;

            if (decimal.TryParse(celda.GetString(), out numero))
                return numero;

            return null;
        }



        private IXLRow ObtenerFilaEncabezados(IXLWorksheet hoja)
        {
            foreach (IXLRow fila in hoja.RowsUsed())
            {
                string valor = fila.Cell(1)
                                   .GetString()
                                   .Trim()
                                   .ToUpper();

                if (valor == "SECTOR/DEPENDENCIA")
                {
                    return fila;
                }
            }

            throw new Exception("No se encontró la fila de encabezados del archivo.");
        }


        #endregion
    }



}