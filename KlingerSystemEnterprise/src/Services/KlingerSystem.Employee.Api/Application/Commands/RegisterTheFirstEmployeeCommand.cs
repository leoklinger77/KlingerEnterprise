using FluentValidation;
using KlingerSystem.Core.Messages;
using KlingerSystem.Core.Tools;
using KlingerSystem.Employee.Api.Application.Commands.Messages;
using System;

namespace KlingerSystem.Employee.Api.Application.Commands
{
    public class RegisterTheFirstEmployeeCommand : Command
    { 
        public Guid BusinessId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }

        public RegisterTheFirstEmployeeCommand(Guid businessId, Guid employeeId, string fullName, string email)
        {
            AggregateId = employeeId;

            BusinessId = businessId;
            EmployeeId = employeeId;
            FullName = fullName;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterTheFirstEmployeeCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterTheFirstEmployeeCommandValidation : AbstractValidator<RegisterTheFirstEmployeeCommand>
        {            
            public RegisterTheFirstEmployeeCommandValidation()
            {
                RuleFor(c => c.BusinessId)
                    .NotEqual(Guid.Empty)
                    .WithMessage(CommandMessages.BusinessId_Invalido);

                RuleFor(c => c.EmployeeId)
                    .NotEqual(Guid.Empty)
                    .WithMessage(CommandMessages.EmployeeId_Invalido);

                RuleFor(c => c.FullName)
                    .NotEmpty()
                    .WithMessage(CommandMessages.FullName_Invalido);

                RuleFor(c => c.FullName.IsFullName())
                    .Equal(true)
                    .WithMessage(CommandMessages.FullName_Invalido);

                RuleFor(c => c.Email.IsEmail())
                    .Equal(true)
                    .WithMessage(CommandMessages.Email_Invalido);
            }
        }
    }    
}
