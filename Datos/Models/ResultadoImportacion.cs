using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFinancieros_Facturas.Datos.Models
{
    public class ResultadoImportacion
    {
        public int Insertados { get; set; }

        public int Duplicados { get; set; }

        public int Errores { get; set; }

        public string Mensaje { get; set; }
        public int DependenciasNoEncontradas { get; set; }

        public int ConceptosNoEgreso { get; set; }

    }
}