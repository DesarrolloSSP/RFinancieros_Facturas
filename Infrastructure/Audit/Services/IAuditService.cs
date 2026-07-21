using RFinancieros_Facturas.Infrastructure.Audit.Models;

namespace RFinancieros_Facturas.Infrastructure.Audit.Services
{
    public interface IAuditService
    {
        void Register(AuditEntry entry);
    }
}