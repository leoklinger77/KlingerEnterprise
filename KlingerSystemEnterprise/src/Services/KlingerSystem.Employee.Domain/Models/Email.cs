using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Tools;
using KlingerSystem.Employee.Domain.Message;
using System;

namespace KlingerSystem.Employee.Domain.Models
{
    public class Email : Entity
    {   
        public Guid EmployeeId { get; private set; }
        public string AddressEmail { get; private set; }
        public Employee Employee { get; private set; }

        protected Email() { }
        public Email(Guid employeeId, string addressEmail)
        {
            EmployeeId = employeeId;
            AddressEmail = addressEmail;

            IsValid();            
        }

        public void SetEmail(string addressEmail)
        {
            AddressEmail = addressEmail;

            IsValid();
        }

        public override void IsValid()
        {
            Validation.ValidateIfEqual(Guid.Empty, EmployeeId, ListEmailMessages.EMPLOYEEID_ERRO_MGS);
            Validation.ValidateIfFalse(AddressEmail.IsEmail(), ListEmailMessages.EMAIL_INVALIDO);
        }
    }
}
