using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Tools;
using System;

namespace KlingerSystem.Business.Domain.Models
{
    public class Email : Entity
    {
        public Guid CompanyId { get; private set; }
        public string AddressEmail { get; private set; }
        public Company Company { get; private set; }

        protected Email() { }
        public Email(Guid companyId, string addressEmail)
        {
            CompanyId = companyId;
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
            Validation.ValidateIfEqual(Guid.Empty, CompanyId, ListEmailMessages.CompanyId_ERRO_MGS);
            Validation.ValidateIfFalse(AddressEmail.IsEmail(), ListEmailMessages.EmailAddress_Erro_MSG);
        }
    }
}
