namespace RFinancieros_Facturas.Infrastructure.Audit.Models
{
    public class AuditChange
    {
        public string Column { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}