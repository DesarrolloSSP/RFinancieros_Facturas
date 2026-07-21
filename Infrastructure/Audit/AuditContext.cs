
namespace RFinancieros_Facturas.Infrastructure.Audit
{
    public class AuditContext
    {
        public string User { get; set; }

        public string Host { get; set; }

        public string Application { get; set; }

        public string StoredProcedure { get; set; }

        public string SessionId { get; set; }
    }
}