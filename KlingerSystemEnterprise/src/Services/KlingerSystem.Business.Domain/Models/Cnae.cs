using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Interfaces;
using KlingerSystem.Core.Tools;
using System.Collections.Generic;

namespace KlingerSystem.Business.Domain.Models
{
    public class Cnae : Entity, IAggregateRoot
    {        
        public string Devision { get; private set; }
        public string Description { get; private set; }

        private List<Company> _companies = new List<Company>();
        public IReadOnlyCollection<Company> Companies => _companies;

        protected Cnae() { }

        public Cnae(string devision, string description)
        {
            SetCnae(devision, description);
        }

        public void SetCnae(string devision, string description)
        {
            Devision = devision;
            Description = description;

            IsValid();
        }

        public override void IsValid()
        {
            Validation.CharactersValidate(Devision, 7, 7, ListCnaeMessages.DevisionText_Erro);
            Validation.CharactersValidate(Description, 500, 5, ListCnaeMessages.DescriptionText_Erro);
        }
    }
}
