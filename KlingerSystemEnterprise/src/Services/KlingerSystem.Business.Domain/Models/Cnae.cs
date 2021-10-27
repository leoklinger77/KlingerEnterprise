using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Interfaces;
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
            Devision = devision;
            Description = description;
        }

        public override void IsValid()
        {
            base.IsValid();
        }
    }
}
