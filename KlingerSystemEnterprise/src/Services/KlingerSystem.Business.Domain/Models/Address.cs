using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Tools;
using System;

namespace KlingerSystem.Business.Domain.Models
{
    public class Address : Entity
    {
        public Guid CompanyId { get; private set; }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Reference { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public Company Employee { get; private set; }

        protected Address() { }

        public Address(Guid companyId, string zipCode, string street, string number, string neighborhood, string city, string state, string complement = null, string reference = null)
        {
            CompanyId = companyId;
            SetAddress(zipCode, street, number, neighborhood, city, state, complement, reference);
        }

        public void SetAddress(string zipCode, string street, string number, string neighborhood, string city, string state, string complement = null, string reference = null)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Complement = complement;
            Reference = reference;

            IsValid();
        }

        public override void IsValid()
        {
            Validation.ValidateIsNullOrEmpty(ZipCode, ListAddressMessages.ZIPCODE_MSG_ERRO);
            Validation.CharactersValidate(ZipCode, 8, 8, ListAddressMessages.ZIPCODE_MSG_ERRO);

            Validation.ValidateIsNullOrEmpty(Street, ListAddressMessages.STREET_MSG_ERRO);
            Validation.CharactersValidate(Street, 100, 5, ListAddressMessages.STREET_MSG_ERRO);

            Validation.ValidateIsNullOrEmpty(Number, ListAddressMessages.NUMBER_MSG_ERRO);
            Validation.CharactersValidate(Number, 20, 1, ListAddressMessages.NUMBER_MSG_ERRO);

            Validation.ValidateIsNullOrEmpty(Neighborhood, ListAddressMessages.NEIGHBORHOOD_MSG_ERRO);
            Validation.CharactersValidate(Neighborhood, 100, 2, ListAddressMessages.NEIGHBORHOOD_MSG_ERRO);

            Validation.ValidateIsNullOrEmpty(City, ListAddressMessages.CITY_MSG_ERRO);
            Validation.CharactersValidate(City, 100, 2, ListAddressMessages.CITY_MSG_ERRO);
        }
    }
}
