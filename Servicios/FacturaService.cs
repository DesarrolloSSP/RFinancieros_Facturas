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
        DateTime fechaDevolucion, string estado, string estreg, int folioSujeto, string usuario, string codigoPostal, int idPartida, int idContrato)
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

                db.SaveChanges();

                return true;

            }

        }

    }


}