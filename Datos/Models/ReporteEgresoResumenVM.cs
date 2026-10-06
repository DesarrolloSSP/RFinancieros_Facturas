using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFinancieros_Facturas.Datos.Models
{
    public class ReporteEgresoResumenVM
    {
        public string Area { get; set; }

        public int CantidadFacturas { get; set; }

        public decimal? TotalImporte { get; set; }
    }
}