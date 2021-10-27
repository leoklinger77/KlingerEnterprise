using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Employee.Domain.Message;
using KlingerSystem.Employee.Domain.Models;
using System;
using Xunit;

namespace KlingerSystem.Employee.Domain.Tests.Domain
{
    public class AddressTests
    {
        [Fact(DisplayName = "Criando um endereco valido")]
        [Trait("Dominio", "Employee")]
        public void Address_CriandoUmEnderecoValido_ComSucesso()
        {
            //Arrange
            var zipCode = "06622280";
            var street = "Santo Andre";
            var number = "130";
            var bairro = "Tereza";
            var cidade = "Jandira";
            var estado = "SP";
            var comple = "Oficial";
            var refe = "Perto do vale";
            var address = new Address(Guid.NewGuid(),zipCode, street, number, bairro, cidade, estado, comple, refe);

            //Act & Assert
            Assert.Equal(zipCode, address.ZipCode);
            Assert.Equal(street, address.Street);
            Assert.Equal(number, address.Number);
            Assert.Equal(bairro, address.Neighborhood);
            Assert.Equal(estado, address.State);
            Assert.Equal(cidade, address.City);
            Assert.Equal(comple, address.Complement);
            Assert.Equal(refe, address.Reference);
        }

        [Theory(DisplayName = "Criando um endereco ZipCode invalido")]
        [InlineData("0662228", "Santo Andre", "130", "Tereza", "Jandira", "SP", "OFICIAL", "PErto do Vale")]
        [InlineData("066222800", "Santo Andre", "130", "Tereza", "Jandira", "SP", "OFICIAL", "PErto do Vale")]
        [Trait("Dominio", "Employee")]
        public void Address_CriandoUmEnderecoComCepInvalido_DeveRetornarException(string zipCode, string street, string number,
                                                                            string neighborhood, string city, string state,
                                                                            string complement, string reference)
        {
            //Act & Assert            
            var result = Assert.Throws<DomainException>(() => new Address(Guid.NewGuid(), zipCode, street, number, neighborhood, city, state, complement, reference));

            Assert.Equal(ListAddressMessages.ZIPCODE_MSG_ERRO, result.Message);            
        }

        [Theory(DisplayName = "Criando um endereco Street invalido")]
        [InlineData("06622280", "Sa", "130", "Tereza", "Jandira", "SP", "OFICIAL", "PErto do Vale")]
        [InlineData("06622280", "Santo Andre de lima Guimação aldaberto de ferreira sebastiano adrino não sei mais o que inventa teste teste teste", "130", "Tereza", "Jandira", "SP", "OFICIAL", "PErto do Vale")]
        [Trait("Dominio", "Employee")]
        public void Address_CriandoUmEnderecoComRuaInvalido_DeveRetornarException(string zipCode, string street, string number,
                                                                            string neighborhood, string city, string state,
                                                                            string complement, string reference)
        {
            //Act & Assert            
            var result = Assert.Throws<DomainException>(() => new Address(Guid.NewGuid(), zipCode, street, number, neighborhood, city, state, complement, reference));

            Assert.Equal(ListAddressMessages.STREET_MSG_ERRO, result.Message);
        }

        [Theory(DisplayName = "Criando um endereco numero invalido")]
        [InlineData("06622280", "Sadsada", "", "Tereza", "Jandira", "SP", "OFICIAL", "PErto do Vale")]
        [InlineData("06622280", "dsads", "130456789123456789452", "Tereza", "Jandira", "SP", "OFICIAL", "PErto do Vale")]
        [Trait("Dominio", "Employee")]
        public void Address_CriandoUmEnderecoComNumeroInvalido_DeveRetornarException(string zipCode, string street, string number,
                                                                            string neighborhood, string city, string state,
                                                                            string complement, string reference)
        {
            //Act & Assert            
            var result = Assert.Throws<DomainException>(() => new Address(Guid.NewGuid(), zipCode, street, number, neighborhood, city, state, complement, reference));

            Assert.Equal(ListAddressMessages.NUMBER_MSG_ERRO, result.Message);
        }

        [Fact(DisplayName = "Criando um endereco Neighborhood invalido")]        
        [Trait("Dominio", "Employee")]
        public void Address_CriandoUmEnderecoComBairroInvalido_DeveRetornarException()
        {
            //Act & Assert            
            var result = Assert.Throws<DomainException>(() => new Address(Guid.NewGuid(), "06622280", "Santo Andre", "123", "T", "Jandira", "SP", "OFICIAL", "PErto do Vale"));

            Assert.Equal(ListAddressMessages.NEIGHBORHOOD_MSG_ERRO, result.Message);
        }

        [Fact(DisplayName = "Criando um endereco City invalido")]
        [Trait("Dominio", "Employee")]
        public void Address_CriandoUmEnderecoComCidadeInvalido_DeveRetornarException()
        {
            //Act & Assert            
            var result = Assert.Throws<DomainException>(() => new Address(Guid.NewGuid(), "06622280", "Santo Andre", "123", "Se", "J", "SP", "OFICIAL", "PErto do Vale"));

            Assert.Equal(ListAddressMessages.CITY_MSG_ERRO, result.Message);
        }

        [Fact(DisplayName = "Atualizando um endereco valido")]
        [Trait("Dominio", "Employee")]
        public void Address_AtualizandoUmEnderecoValido_ComSucesso()
        {
            //Arrange           
            var address = new Address(Guid.NewGuid(), "12345678", "Rua X", "789", "Fe", "Barueri", "RJ", "Teste", "Centro");

            var zipCode = "06622280";
            var street = "Santo Andre";
            var number = "130";
            var bairro = "Tereza";
            var cidade = "Jandira";
            var estado = "SP";
            var comple = "Oficial";
            var refe = "Perto do vale";

            //Act
            address.SetAddress(zipCode, street, number, bairro, cidade, estado, comple, refe);

            //Assert
            Assert.Equal(zipCode, address.ZipCode);
            Assert.Equal(street, address.Street);
            Assert.Equal(number, address.Number);
            Assert.Equal(bairro, address.Neighborhood);
            Assert.Equal(estado, address.State);
            Assert.Equal(cidade, address.City);
            Assert.Equal(comple, address.Complement);
            Assert.Equal(refe, address.Reference);
        }
    }
}
