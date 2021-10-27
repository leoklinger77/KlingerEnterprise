using KlingerSystem.Core.Interfaces;
using KlingerSystem.Employee.Domain.Models;
using System;
using System.Threading.Tasks;

namespace KlingerSystem.Employee.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Models.Employee>
    {
        Task Insert(Models.Employee employee);
        Task InsertEmail(Email email);
        Task<Models.Employee> FindEmployeeByEmailWithBusiness(Guid businessId, string email);
        Task<Models.Employee> FindEmployeeByEmail(string email);
        Task<Models.Employee> FindEmployeeByCompanyId(Guid businessId);
        Task<Models.Employee> FindEmployeeByCpf(Guid businessId, string cpf);
    }
}
