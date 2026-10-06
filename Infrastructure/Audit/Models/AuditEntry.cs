using System;
using System.Collections.Generic;

namespace RFinancieros_Facturas.Infrastructure.Audit.Models
{
    public class AuditEntry
    {
        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }

        public string Host { get; set; }

        public string Aplicacion { get; set; }

        public string StoredProcedure { get; set; }

        public int? SessionId { get; set; }

        public int? DuracionMs { get; set; }

        public string Tabla { get; set; }

        public string LlavePrimaria { get; set; }

        public AuditOperation Operacion { get; set; }

        public int CantidadRegistros { get; set; }

        public string Observaciones { get; set; }

        public List<AuditChange> Cambios { get; set; }

        public AuditEntry()
        {
            Fecha = DateTime.Now;
            CantidadRegistros = 1;
            Cambios = new List<AuditChange>();
        }
    }
}