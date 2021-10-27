using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Business.Domain.Models.Enum;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Interfaces;
using KlingerSystem.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KlingerSystem.Business.Domain.Models
{
    public class Company : Entity, IAggregateRoot
    {
        public static int PHONE_COUNT_MAX => 2;

        public Guid? MatrizId { get; private set; }
        public Guid CnaeId { get; private set; }
        public string CompanyName { get; private set; }
        public string FantasyName { get; private set; }
        public Cpf Cpf { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public string MunicipalRegistration { get; private set; }
        public string StateRegistration { get; private set; }
        public string Site { get; private set; }
        public CompanyType CompanyType { get; private set; }
        public PersonType PersonType { get; private set; }
        public TaxRegimeType TaxRegime { get; private set; }
        public TypeSpecialTaxRegime SpecialTaxRegime { get; private set; }
        public Cnae Cnae { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }
        private List<Phone> _phones = new List<Phone>();
        public IReadOnlyCollection<Phone> Phones => _phones;

        public Company Matriz { get; private set; }

        protected Company() { }
        protected Company(string fantasyName)
        {
            SetFantasyName(fantasyName);
            Headequarters();
            SetPersonType(PersonType.Juridical);
        }
        public Company(Guid matrizId, string fantasyName, PersonType personType)
        {
            SetMatriz(matrizId);
            SetFantasyName(fantasyName);
            SetPersonType(personType);
            Branch();
        }
        public static class CompanyFactory
        {
            public static Company CompanyMatriz(string fantasyName)
            {
                return new Company(fantasyName);
            }
            public static Company CompanyFilial(Guid matriz, string fantasyName, PersonType personType)
            {
                return new Company(matriz, fantasyName, personType);
            }
        }

        public void AddPhone(Phone phone)
        {
            Validation.ValidateIfEqual(_phones.Count, PHONE_COUNT_MAX, ListCompanyMessages.PHONE_COUNT_MAX_ERRO_MSG);
            Validation.ValidateIfTrue(_phones.FirstOrDefault(x => x.Number == phone.Number) != null, ListCompanyMessages.NUMBER_REPIT_ERRO_MSG);
            _phones.Add(phone);
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
        public void SetCnae(Cnae cnae)
        {
            CnaeId = cnae.Id;
            Cnae = cnae;
        }
        public void SetEmail(Email email)
        {
            Email = email;
        }
        public void SetSite(string site)
        {
            Site = site;
        }

        public void SetMatriz(Guid matrizId)
        {
            Validation.ValidateIfEqual(Guid.Empty, matrizId, ListCompanyMessages.MatrizId_Erro);
            MatrizId = matrizId;
        }

        private void Headequarters()
        {
            CompanyType = CompanyType.MATRIZ;
        }
        private void Branch()
        {
            CompanyType = CompanyType.FILIAL;
        }
    }
}
