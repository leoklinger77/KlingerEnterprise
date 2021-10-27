using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Tools;
using KlingerSystem.Employee.Domain.Message;
using KlingerSystem.Employee.Domain.Models.Enum;
using System;

namespace KlingerSystem.Employee.Domain.Models
{
    public class Phone : Entity
    {
        public Guid EmployeeId { get; private set; }
        public string Ddd { get; private set; }
        public string Number { get; private set; }
        public PhoneType PhoneType { get; private set; }
        public Employee Employee { get; private set; }
        protected Phone() { }

        public Phone(Guid employeeId, string ddd, string number, PhoneType phoneType)
        {
            EmployeeId = employeeId;
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

            if (PhoneType == PhoneType.Home)
            {
                Validation.CharactersValidate(Number, 8, 8, ListPhoneMessages.HOME_ERRO_MSG);
            }

            if (PhoneType == PhoneType.Workstation)
            {
                Validation.CharactersValidate(Number, 9, 8, ListPhoneMessages.WORKSTATION_ERRO_MSG);
            }
        }
    }
}
