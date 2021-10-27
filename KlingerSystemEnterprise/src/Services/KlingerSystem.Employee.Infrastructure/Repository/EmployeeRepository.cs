using KlingerSystem.Core.Interfaces;
using KlingerSystem.Employee.Domain.Interfaces;
using KlingerSystem.Employee.Domain.Models;
using KlingerSystem.Employee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerSystem.Employee.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public IUnitOfWork UnitOfWork => _employeeContext;
        public async Task<Domain.Models.Employee> FindEmployeeByCpf(Guid businessId, string cpf)
        {
            return await _employeeContext.Employee
                .AsNoTracking()
                .Where(x => x.BusinessId == businessId && x.Cpf.Number == cpf)
                .FirstOrDefaultAsync();
        }

        public async Task<Domain.Models.Employee> FindEmployeeByEmailWithBusiness(Guid businessId, string email)
        {
            return await _employeeContext.Employee
                .AsNoTracking()
                .Where(x => x.BusinessId == businessId && x.Email.AddressEmail == email)
                .FirstOrDefaultAsync();
        }
        public async Task<Domain.Models.Employee> FindEmployeeByEmail(string email)
        {
            return await _employeeContext.Employee
                .AsNoTracking()
                .Where(x => x.Email.AddressEmail == email)
                .FirstOrDefaultAsync();
        }

        public async Task<Domain.Models.Employee> FindEmployeeByCompanyId(Guid businessId)
        {
            return await _employeeContext.Employee
                .AsNoTracking()
                .Where(x => x.BusinessId == businessId)
                .FirstOrDefaultAsync();
        }

        public async Task Insert(Domain.Models.Employee employee)
        {
            await _employeeContext.Employee.AddAsync(employee);
        }

        public async Task InsertEmail(Email email)
        {
            await _employeeContext.Email.AddAsync(email);
        }

        public void Dispose()
        {
            _employeeContext?.DisposeAsync();
        }

        
    }
}
