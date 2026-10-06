using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFinancieros_Facturas.Datos.Models
{


    public class ReporteEgresoVM
    {
        public int Id { get; set; }

        public string Folio { get; set; }

        public string Estado { get; set; }

        public decimal Importe { get; set; }

        public string Rfce { get; set; }

        public string Rsemisor { get; set; }

        public DateTime? FechaEmision { get; set; }

        public DateTime? FechaCertifica { get; set; }

        public string PacCertifico { get; set; }

        public int Idarea { get; set; }

        public int? IdPago { get; set; }

        public string NoOdp { get; set; }

        public string FolioInternoFactura { get; set; }

        public string ConceptoDevol { get; set; }

        public DateTime? FechaRevision { get; set; }

        public DateTime? FechaDevol { get; set; }

        public DateTime? FechaCaptura { get; set; }

        public string Estreg { get; set; } = null;

        public DateTime? FechaReingreso { get; set; }

        public string Revisor { get; set; } = null;

        public string Capturista { get; set; } = null;

        // IMPORTANTE: en SQL es int
        public int? Egreso { get; set; }

        // IMPORTANTE: en SQL es int
        public int? Idconcepto { get; set; }

        // IMPORTANTE: en SQL es int
        public int? FolioSujeto { get; set; }

        public string Validador { get; set; } = null;

        public string CodigoPostal { get; set; } = null;

        // En SQL es int
        public int? idtcterceros_institucional { get; set; }

        // En SQL es int
        public int? idtcpartida { get; set; }

        public string clave_partida { get; set; }

        public string partida { get; set; }

        // Viene de tcareas
        public string NombreArea { get; set; } = null;

        // Viene de PagosSiafev
        public DateTime? FechaPago { get; set; }

        // Viene de tcContrato
        public string nocontrato { get; set; } = null;

        // SUM(decimal)
        public decimal? SumaEgresoSIAFEV { get; set; }

        // SUM(numeric)
        public decimal? SumaEgresoVAREFACT { get; set; }

        public string Estatus { get; set; } = null;

        public DateTime? FechaEgreso { get; set; }
        public DateTime? FechaVentanilla { get; set; }
        public string EstatusDRF { get; set; } = null;


    }



}