using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFinancieros_Facturas.Funciones
{
    public class AuditoriaResult
    {

        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string NombrePartida { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal ImporteTotalOdp { get; set; }
        public string NoOdp { get; set; }
        public string Entregable { get; set; }
        public decimal ImporteTotalPorOdp { get; set; }
        public string clave_partida { get; set; }
        public string FolioInternoFactura { get; set; }
        public string Egreso { get; set; }
        public string nocontrato { get; set; }

    }
}