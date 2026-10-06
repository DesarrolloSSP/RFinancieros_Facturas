using RFinancieros_Facturas.Infrastructure.Audit.Models;
using RFinancieros_Facturas.Infrastructure.Audit.Repositories;

namespace RFinancieros_Facturas.Infrastructure.Audit.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _repository;

        public AuditService()
        {
            _repository = new SqlAuditRepository();
        }

        public AuditService(IAuditRepository repository)
        {
            _repository = repository;
        }

        public void Register(AuditEntry entry)
        {
            _repository.Save(entry);
        }
    }
}