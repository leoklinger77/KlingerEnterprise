using KlingerSystem.Business.Domain.Interfaces;
using KlingerSystem.Business.Infrastructure.Data;
using KlingerSystem.Core.Interfaces;
using System;

namespace KlingerSystem.Business.Infrastructure.Repositorys
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly BusinessContext _context;

        public CompanyRepository(BusinessContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Dispose()
        {
            _context?.DisposeAsync();
        }
    }
}
