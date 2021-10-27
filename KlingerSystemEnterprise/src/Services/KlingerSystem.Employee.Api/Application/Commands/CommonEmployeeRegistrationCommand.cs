using FluentValidation;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Messages;
using KlingerSystem.Core.Tools;
using KlingerSystem.Employee.Api.Application.Commands.Messages;
using KlingerSystem.Employee.Domain.Models.Enum;
using System;
using System.Collections.Generic;

namespace KlingerSystem.Employee.Api.Application.Commands
{
    public class CommonEmployeeRegistrationCommand : Command
    {
        public Guid BusinessId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public RgCommand Rg { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string ImagePath { get; private set; }
        public double? Commission { get; private set; }
        public string Annotation { get; private set; }
        public int TypeSexo { get; private set; }
        public AddressCommand Address { get; private set; }
        private List<PhoneCommand> _phones = new List<PhoneCommand>();
        public IReadOnlyCollection<PhoneCommand> Phones => _phones;
        public CommonEmployeeRegistrationCommand(Guid businessId, string fullName, string email,
            string cpf = null, RgCommand rg = null,
            DateTime? birthDate = null, string imagePath = null, double? commission = null, string annotation = null, int typeSexo = 0,
            AddressCommand address = null, List<PhoneCommand> phones = null)
        {
            BusinessId = businessId;
            FullName = fullName;
            Email = email;
            Cpf = cpf;
            Rg = rg;

            BirthDate = birthDate;
            ImagePath = imagePath;
            Commission = commission;
            Annotation = annotation;
            TypeSexo = typeSexo;
            Address = address;


            if (phones != null)
            {
                _phones = phones;
            }
                
        }

        public override bool IsValid()
        {
            ValidationResult = new CommonEmployeeRegistrationCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CommonEmployeeRegistrationCommandValidation : AbstractValidator<CommonEmployeeRegistrationCommand>
        {
            public CommonEmployeeRegistrationCommandValidation()
            {
                RuleFor(c => c.BusinessId)
                    .NotEqual(Guid.Empty)
                    .WithMessage(CommandMessages.BusinessId_Invalido);

                RuleFor(c => c.FullName.IsFullName())
                    .Equal(true)
                    .WithMessage(CommandMessages.FullName_Invalido);

                RuleFor(c => c.Email.IsEmail())
                    .Equal(true)
                    .WithMessage(CommandMessages.Email_Invalido);

                RuleFor(c => c.Phones.Count > 3)
                   .Equal(false)
                   .WithMessage(CommandMessages.PhoneQtdeMax);

                //Optional
                RuleFor(c => c.Annotation)
                    .Length(0, 999)
                    .WithMessage(CommandMessages.Annotation_Invalido);

                When(x => x.Cpf != null, () =>
                {
                    RuleFor(c => c.Cpf.IsCpf())
                       .Equal(true)
                       .WithMessage(CommandMessages.Cpf_Invalido);
                });

                When(x => x.Rg != null, () =>
                {
                    RuleFor(x => x.Rg.Number)
                       .NotEmpty()
                       .WithMessage(CommandMessages.RgNumber_Invalido)
                       .Length(Core.DomainObjects.Rg.RgMinLength, Core.DomainObjects.Rg.RgMaxLength)
                       .WithMessage(CommandMessages.RgNumber_Invalido);

                    RuleFor(x => Core.DomainObjects.Rg.IsDateValid(x.Rg.ExpeditionDate))
                       .Equal(true)
                       .WithMessage(CommandMessages.RgExpeditionDate_invalido);

                    RuleFor(x => x.Rg.Issuer.IsEnum<RgIssuer>())
                        .Equal(true)
                        .WithMessage(CommandMessages.RgIssuere_Invalido);
                });

                When(x => x.BirthDate.HasValue, () =>
                {
                    RuleFor(x => Domain.Models.Employee.BirthDateValid(x.BirthDate.Value))
                        .Equal(false)
                        .WithMessage(CommandMessages.BirthDate_Invalido);
                });

                When(x => x.ImagePath != null, () =>
                {
                    RuleFor(c => c.ImagePath.Contains(Guid.Empty.ToString()))
                        .Equal(false)
                        .WithMessage(CommandMessages.ImagePath_Invalido);
                });

                When(x => x.Commission.HasValue, () =>
                {
                    RuleFor(c => c.Commission)
                        .GreaterThan(0)
                        .WithMessage(CommandMessages.Commission_Invalido);
                });

                When(x => x.TypeSexo != 0, () =>
                {
                    RuleFor(c => ExtensionsMethods.IsEnum<TypeSexo>(Enum.Parse<TypeSexo>(c.TypeSexo.ToString())))
                        .Equal(true)
                        .WithMessage(CommandMessages.TypeSexo_Invalido);
                });

                When(x => x.Address != null, () =>
                {
                    RuleFor(x => x.Address.ZipCode)
                        .NotEmpty().WithMessage(CommandMessages.AddressZipCode_Invalido)
                        .Length(8, 8).WithMessage(CommandMessages.AddressZipCodeText_Invalido);

                    RuleFor(x => x.Address.Street)
                        .NotEmpty().WithMessage(CommandMessages.AddressStreet_Invalido)
                        .Length(2, 100).WithMessage(CommandMessages.AddressStreetText_Invalido);

                    RuleFor(x => x.Address.Number)
                        .NotEmpty().WithMessage(CommandMessages.AddressNumber_Invalido)
                        .Length(1, 20).WithMessage(CommandMessages.AddressNumberText_Invalido);

                    RuleFor(x => x.Address.Neighborhood)
                        .NotEmpty().WithMessage(CommandMessages.AddressNeighborhood_Invalido)
                        .Length(2, 100).WithMessage(CommandMessages.AddressNeighborhoodText_Invalido);

                    RuleFor(x => x.Address.City)
                        .NotEmpty().WithMessage(CommandMessages.AddressCity_Invalido)
                        .Length(2, 100).WithMessage(CommandMessages.AddressCityText_Invalido);

                    RuleFor(x => x.Address.State)
                        .NotEmpty().WithMessage(CommandMessages.AddressState_Invalido)
                        .Length(2, 2).WithMessage(CommandMessages.AddressStateText_Invalido);


                    RuleFor(x => x.Address.Complement)
                        .Length(1, 100).WithMessage(CommandMessages.AddressComplement_Invalido);
                    RuleFor(x => x.Address.Reference)
                        .Length(1, 100).WithMessage(CommandMessages.AddressReference_Invalido);
                });

                When(x => x.Phones.Count > 0, () =>
                {
                    RuleForEach(x => x.Phones).ChildRules(phone => 
                    {
                        phone.RuleFor(x => x.Ddd)
                            .NotEmpty().WithMessage(CommandMessages.PhoneDdd_Invalido)
                            .Length(2, 2).WithMessage(CommandMessages.PhoneDddText_Invalido);

                        phone.When(x => x.PhoneType == PhoneType.Home, () => 
                        {
                            phone.RuleFor(x => x.Number)
                            .NotEmpty().WithMessage(CommandMessages.PhoneHome_Invalido)
                            .Length(8, 8).WithMessage(CommandMessages.PhoneHomeText_Invalido);
                        });

                        phone.When(x => x.PhoneType == PhoneType.SmartPhone, () =>
                        {
                            phone.RuleFor(x => x.Number)
                            .NotEmpty().WithMessage(CommandMessages.PhoneSmartPhone_Invalido)
                            .Length(9, 9).WithMessage(CommandMessages.PhoneSmartPhone_Invalido);
                        });

                        phone.When(x => x.PhoneType == PhoneType.Workstation, () =>
                        {
                            phone.RuleFor(x => x.Number)
                            .NotEmpty().WithMessage(CommandMessages.PhoneWorkstation_Invalido)
                            .Length(8, 9).WithMessage(CommandMessages.PhoneWorkstation_Invalido);
                        });
                    });
                });
            }
        }

        public class RgCommand
        {
            public string Number { get; private set; }
            public DateTime ExpeditionDate { get; private set; }
            public RgIssuer Issuer { get; private set; }

            public RgCommand(string number, DateTime expeditionDate, RgIssuer issuer)
            {
                Number = number;
                ExpeditionDate = expeditionDate;
                Issuer = issuer;
            }
        }
        public class AddressCommand
        {
            public string ZipCode { get; private set; }
            public string Street { get; private set; }
            public string Number { get; private set; }
            public string Complement { get; private set; }
            public string Reference { get; private set; }
            public string Neighborhood { get; private set; }
            public string City { get; private set; }
            public string State { get; private set; }

            public AddressCommand(string zipCode, string street, string number, string neighborhood, string city, string state, string complement = null, string reference = null)
            {
                ZipCode = zipCode;
                Street = street;
                Number = number;
                Neighborhood = neighborhood;
                City = city;
                State = state;
                Complement = complement;
                Reference = reference;
            }
        }
        public class PhoneCommand
        {
            public string Ddd { get; private set; }
            public string Number { get; private set; }
            public PhoneType PhoneType { get; private set; }

            public PhoneCommand(string ddd, string number, PhoneType phoneType)
            {
                Ddd = ddd;
                Number = number;
                PhoneType = phoneType;
            }
        }
    }
}
