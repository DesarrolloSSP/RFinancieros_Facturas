using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using RFinancieros_Facturas.Datos;
using RFinancieros_Facturas.Datos.Entities;
using RFinancieros_Facturas.Datos.Models;

namespace RFinancieros_Facturas.Services
{
    public class PagoSiafecService
    {
        public ResultadoImportacion Guardar(List<PagoExcelVM> lista)
        {
            ResultadoImportacion resultado = new ResultadoImportacion();

            using (var db = new dbFacturasFinancierosEntities())
            {
                try
                {
                    HashSet<string> dependenciasValidas = new HashSet<string>(db.Dependencia
                    .Select(x => x.NombreDependencia.Trim()).ToList(), StringComparer.OrdinalIgnoreCase);


                    // Folios que ya fueron encontrados/procesados
                    
                    HashSet<string> foliosProcesados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    foreach (PagoExcelVM item in lista)
                    {
                        
                        if (!string.Equals(item.Concepto?.Trim(), "Egreso", StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        
                        if (!dependenciasValidas.Contains(item.SectorDependencia?.Trim()))
                        {
                            resultado.DependenciasNoEncontradas++;
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(item.Folio))
                        {
                            resultado.Errores++;
                            continue;
                        }

                        string folio = item.Folio.Trim();

                        if (foliosProcesados.Contains(folio))
                        {
                            resultado.Duplicados++;
                            continue;
                        }
                        bool existe = db.PagosSiafev.Any(x => x.Folio == folio);

                        if (existe)
                        {
                            resultado.Duplicados++;
                            continue;
                        }

                        PagosSiafev pago = new PagosSiafev();

                        pago.SectorDependencia = item.SectorDependencia;
                        pago.Beneficiario = item.Beneficiario;
                        pago.NumeroBeneficiario = item.NumeroBeneficiario;
                        pago.Folio = folio;
                        pago.Concepto = item.Concepto;
                        pago.Fecha = item.Fecha;
                        pago.Monto = item.Monto;
                        // pago.Pagado = item.Pagado;
                        pago.Observaciones = item.Observaciones;
                        pago.FechaPago = item.FechaPago;
                        pago.ReferenciaPago = item.ReferenciaPago;
                        pago.FuenteFtto = item.Fuente;
                        pago.DescripcionFuente = item.DescripcionFuente;
                        pago.OC = item.OC;
                        pago.ClasificacionProveedor = item.ClasificacionProveedor;
                        pago.Sector = item.Sector;
                        pago.DescripcionSector = item.DescripcionSector;
                        pago.TipoGasto = item.TipoGasto;
                        pago.CuentaCargo = item.CuentaCargo;
                        pago.BancoCargo = item.BancoCargo;
                        pago.CuentaAbono = item.CuentaAbono;
                        pago.BancoAbono = item.BancoAbono;
                        pago.FechaImportacion = DateTime.Now;
                        db.PagosSiafev.Add(pago);
                        foliosProcesados.Add(folio);
                        resultado.Insertados++;
                    }

                    db.SaveChanges();

                    resultado.Mensaje = $"Se importaron {resultado.Insertados} registros. " +
                        $"Duplicados omitidos: {resultado.Duplicados}.";
                }
                catch (Exception ex)
                {
                    resultado.Errores++;
                    resultado.Mensaje = ex.Message;
                }

            }

            return resultado;
        }

    }
}