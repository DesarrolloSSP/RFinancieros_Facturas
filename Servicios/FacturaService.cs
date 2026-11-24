using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using RFinancieros_Facturas.Datos;

namespace RFinancieros_Facturas.Servicios
{
    public class FacturaService
    {

        public bool ActualizarFactura(int id, int idArea, int idTipoPago, string noOdp, string folioInterno, string motivo,
        DateTime fechaDevolucion, string estado, string estreg, int folioSujeto, string usuario, string codigoPostal, int idPartida, int idContrato, DateTime? fechaVentanilla)
        {
            using (var db = new dbFacturasFinancierosEntities())
            {

                var factura = db.tdfacturas.FirstOrDefault(x => x.Id == id);

                if (factura == null)
                    return false;



                var usuarioDb = db.tcusuarios.FirstOrDefault(x => x.login == usuario);

                if (usuarioDb == null)
                    return false;

                factura.Idarea = idArea;
                factura.idtctipo_pago = idTipoPago;
                factura.NoOdp = noOdp;
                factura.FolioInternoFactura = folioInterno;
                factura.ConceptoDevol = motivo;
                factura.FechaDevol = fechaDevolucion;
                factura.FechaRevision = DateTime.Now;
                factura.Estado = estado;
                factura.Estreg = estreg;
                factura.Revisor = usuarioDb.iniciales;
                factura.FolioSujeto = folioSujeto;
                factura.CodigoPostal = codigoPostal;
                factura.idtcpartida = idPartida;
                factura.idtc_contrato = idContrato;
                factura.FechaVentanilla = fechaVentanilla;


                var facturas = db.tdfacturas.Where(x => x.NoOdp == noOdp).ToList();

                if (!facturas.Any())
                    return false;


                // Protección: No permitir 50 o más registros
                if (facturas.Count >= 50)
                {
                    throw new Exception(
                        "La operación fue cancelada: la ODP contiene 50 o más facturas.");
                }


                // Actualizar las facturas correspondientes
                foreach (var val in facturas)
                {
                    val.FolioSujeto = folioSujeto;
                }

                db.SaveChanges();
                return true;


            }

        }





        public int CapturarEgreso(int id, int egreso, string usuario, int idPartida, int folioSujeto)
        {
            using (var db = new dbFacturasFinancierosEntities())
            {

                var facturaSeleccionada = db.tdfacturas.FirstOrDefault(x => x.Id == id);

                if (facturaSeleccionada == null)
                    throw new Exception("No se encontró la factura seleccionada.");

                // Obtener la ODP de la factura
                string noOdp = facturaSeleccionada.NoOdp;

                // Validar que tenga una ODP asignada
                if (string.IsNullOrWhiteSpace(noOdp) || noOdp.Trim() == "0")
                {
                    throw new Exception(
                        "La factura no tiene una orden de pago válida asignada.");
                }

                // Obtener las iniciales del usuario
                var usuarioDb = db.tcusuarios
                    .FirstOrDefault(x => x.login == usuario);

                if (usuarioDb == null)
                    throw new Exception("No se encontró el usuario.");

                // Buscar todas las facturas de esa ODP
                var facturas = db.tdfacturas
                    .Where(x => x.NoOdp == noOdp)
                    .ToList();

                if (!facturas.Any())
                    return 0;

                // Protección: No permitir 50 o más registros
                if (facturas.Count >= 50)
                {
                    throw new Exception(
                        "La operación fue cancelada: la ODP contiene 50 o más facturas.");
                }

                // Actualizar las facturas correspondientes
                foreach (var factura in facturas)
                {
                    factura.Estreg = "E";
                    factura.Egreso = egreso;
                    factura.Validador = usuarioDb.iniciales;
                    factura.idtcpartida = idPartida;
                    //factura.FechaVentanilla = fechaVentanilla;
                    factura.FolioSujeto = folioSujeto;
                }

                return db.SaveChanges();

            }

        }


    }


}