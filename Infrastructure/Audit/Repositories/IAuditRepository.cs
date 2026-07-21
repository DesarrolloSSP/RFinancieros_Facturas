using RFinancieros_Facturas.Infrastructure.Audit.Models;

namespace RFinancieros_Facturas.Infrastructure.Audit.Repositories
{
    public interface IAuditRepository
    {
        long Save(AuditEntry entry);
    }
}