using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Business.Domain.Models.Enum;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Tools;
using System;

namespace KlingerSystem.Business.Domain.Models
{
    public class Phone : Entity
    {
        public Guid CompanyId { get; private set; }
        public string Ddd { get; private set; }
        public string Number { get; private set; }
        public PhoneType PhoneType { get; private set; }
        public Company Company { get; private set; }
        protected Phone() { }

        public Phone(Guid companyId, string ddd, string number, PhoneType phoneType)
        {
            CompanyId = companyId;
            PhoneType = phoneType;

            SetPhone(ddd, number);
        }

        public void SetPhone(string ddd, string number)
        {
            Ddd = ddd;
            Number = number;

            IsValid();
        }

        public override void IsValid()
        {
            Validation.CharactersValidate(Ddd, 2, 2, ListPhoneMessages.DDD_ERRO_MSG);

            if (PhoneType == PhoneType.SmartPhone)
            {
                Validation.CharactersValidate(Number, 9, 9, ListPhoneMessages.SMARTPHONE_ERRO_MSG);
            }           

            if (PhoneType == PhoneType.Workstation)
            {
                Validation.CharactersValidate(Number, 9, 8, ListPhoneMessages.WORKSTATION_ERRO_MSG);
            }
        }
    }
}
