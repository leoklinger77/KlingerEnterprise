using FluentValidation.Results;
using KlingerSystem.Core.Messages;
using KlingerSystem.Employee.Api.Application.Events;
using KlingerSystem.Employee.Domain.Models;
using KlingerSystem.Employee.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using KlingerSystem.Employee.Api.Application.Commands.Messages;

namespace KlingerSystem.Employee.Api.Application.Commands.Handler
{
    public class EmployeeCommandHandler : CommandHandler,
                                          IRequestHandler<RegisterTheFirstEmployeeCommand, ValidationResult>,
                                          IRequestHandler<CommonEmployeeRegistrationCommand, ValidationResult>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ValidationResult> Handle(RegisterTheFirstEmployeeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            if (await _employeeRepository.FindEmployeeByEmail(message.Email) != null)
            {
                AddError(CommandMessages.Erro_EmailJaCadastrado);
                return ValidationResult;
            }

            if (await _employeeRepository.FindEmployeeByCompanyId(message.BusinessId) != null)
            {
                AddError(CommandMessages.Erro_FalhaAoRegistrarFuncionario);
                return ValidationResult;
            }

            var employee = Domain.Models.Employee.EmployeeFactory.EmployeeAdministrator(message.EmployeeId, message.BusinessId, message.FullName);
            employee.SetEmail(new Email(employee.Id, message.Email));

            await _employeeRepository.InsertEmail(employee.Email);
            await _employeeRepository.Insert(employee);

            employee.AddEvent(new RegisteredAdministratorEmployeeEvent(message.BusinessId, employee.Id, employee.FullName, message.Email));

            return await PersistData(_employeeRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(CommonEmployeeRegistrationCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            if (await _employeeRepository.FindEmployeeByEmailWithBusiness(message.BusinessId, message.Email) != null)
            {
                AddError(CommandMessages.Erro_EmailJaCadastrado);
                return ValidationResult;
            }

            if (await _employeeRepository.FindEmployeeByCompanyId(message.BusinessId) == null)
            {
                AddError(CommandMessages.Erro_FalhaAoRegistrarFuncionario);
                return ValidationResult;
            }

            if (!string.IsNullOrEmpty(message.Cpf) && await _employeeRepository.FindEmployeeByCpf(message.BusinessId, message.Cpf) != null)
            {
                AddError(CommandMessages.Erro_CpfJaCadastrado);
                return ValidationResult;
            }                       

            var employee = Domain.Models.Employee.EmployeeFactory.EmployeeCommom(message.BusinessId, message.FullName);
            employee.SetEmail(new Email(employee.Id, message.Email));

            await _employeeRepository.Insert(employee);

            return await PersistData(_employeeRepository.UnitOfWork);
        }
    }
}
