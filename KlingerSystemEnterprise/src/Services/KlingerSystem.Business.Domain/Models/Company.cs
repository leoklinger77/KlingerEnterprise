using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Business.Domain.Models.Enum;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Interfaces;
using KlingerSystem.Core.Tools;

namespace KlingerSystem.Business.Domain.Models
{
    public class Company : Entity, IAggregateRoot
    {
        public string CompanyName { get; private set; }
        public string FantasyName { get; private set; }
        public Cpf Cpf { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public string MunicipalRegistration { get; private set; }
        public string StateRegistration { get; private set; }
        public CompanyType CompanyType { get; private set; }
        public PersonType PersonType { get; private set; }
        public TaxRegimeType TaxRegime { get; private set; }
        public TypeSpecialTaxRegime SpecialTaxRegime { get; private set; }

        public Address Address { get; private set; }
        protected Company() { }
        protected Company(string fantasyName)
        {
            FantasyName = fantasyName;
        }

        public static class CompanyFactory
        {
            public static Company CompanyHeadequarters(string fantasyName)
            {
                var company = new Company(fantasyName);
                company.Headequarters();
                company.SetPersonType(PersonType.Juridical);
                return company;
            }
        }
        public void SetCompanyName(string companyName)
        {
            Validation.ValidateIfEqual(PersonType, PersonType.Physical, ListCompanyMessages.PHYSICAL_COMPANYNAME_ERRO);
            Validation.CharactersValidate(companyName, 500, 5, ListCompanyMessages.CompanyNameText_Erro);
            CompanyName = companyName;
        }
        public void SetFantasyName(string fantasyName)
        {
            Validation.ValidateIfEqual(PersonType, PersonType.Physical, ListCompanyMessages.PHYSICAL_FANTASYNAME_ERRO);
            Validation.CharactersValidate(fantasyName, 255, 5, ListCompanyMessages.FantasyNameText_Erro);
            FantasyName = fantasyName;
        }
        public void SetCnpj(Cnpj cnpj)
        {
            Validation.ValidateIfEqual(PersonType, PersonType.Physical, ListCompanyMessages.PHISYCAL_CNPJ_ERRO);            
            Cnpj = cnpj;
        }
        public void SetCpf(Cpf cpf)
        {
            Validation.ValidateIfEqual(PersonType, PersonType.Juridical, ListCompanyMessages.JURIDICO_CPF_ERRO);
            Cpf = cpf;
        }        
        public void SetMunicipalRegistration(string municipalRegistration)
        {
            Validation.ValidateIfEqual(PersonType, PersonType.Physical, ListCompanyMessages.PHYSICAL_MUNICIPALREGISTRATION_ERRO);
            Validation.CharactersValidate(municipalRegistration, 20, 5, ListCompanyMessages.MunicipalRegistrationText_Erro);
            MunicipalRegistration = municipalRegistration;
        }
        public void SetStateRegistration(string stateRegistration)
        {
            Validation.ValidateIfEqual(PersonType, PersonType.Physical, ListCompanyMessages.PHYSICAL_STATEREGISTRATION_ERRO);
            Validation.CharactersValidate(stateRegistration, 14, 14, ListCompanyMessages.StateRegistrationText_Erro);
            StateRegistration = stateRegistration;
        }
        public void SetTaxRegime(TaxRegimeType taxRegime)
        {
            
            TaxRegime = taxRegime;
        }
        public void SetPersonType(PersonType person)
        {
            
            PersonType = person;
        }
        public void SetTypeSpecialTaxRegime(TypeSpecialTaxRegime specialTaxRegime)
        {
            
            SpecialTaxRegime = specialTaxRegime;
        }
        public void SetAddress(Address address)
        {
            Address = address;
        }
        private void Headequarters()
        {
            CompanyType = CompanyType.MATRIZ;
        }
    }
}
