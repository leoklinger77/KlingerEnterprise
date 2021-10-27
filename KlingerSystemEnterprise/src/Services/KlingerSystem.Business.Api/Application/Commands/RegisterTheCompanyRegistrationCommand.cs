using FluentValidation;
using KlingerSystem.Business.Api.Application.Commands.Messages;
using KlingerSystem.Core.Messages;
using KlingerSystem.Core.Tools;

namespace KlingerSystem.Business.Api.Application.Commands
{
    public class RegisterTheCompanyRegistrationCommand : Command
    {
        public string FantasyName { get; private set; }

        public RegisterTheCompanyRegistrationCommand(string fantasyName)
        {
            FantasyName = fantasyName;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterTheCompanyCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RegisterTheCompanyCommandValidation : AbstractValidator<RegisterTheCompanyRegistrationCommand>
        {
            public RegisterTheCompanyCommandValidation()
            {
                RuleFor(c => c.FantasyName.IsFullName())
                    .Equal(true)
                    .WithMessage(CommandMessages.FantasyName_Invalido);                
            }
        }
    }
}
