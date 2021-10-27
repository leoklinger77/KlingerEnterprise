using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Employee.Domain.Message;
using KlingerSystem.Employee.Domain.Models;
using KlingerSystem.Employee.Domain.Models.Enum;
using System;
using Xunit;

namespace KlingerSystem.Employee.Domain.Tests.Domain
{
    public class PhoneTests
    {
        [Fact(DisplayName = "Criando um telefone celular valido")]
        [Trait("Dominio", "Employee")]
        public void Phone_CriandoUmTelefoneCelularValido_ComSucesso()
        {
            //Arrange
            var ddd = "11";
            var number = "954665152";

            //Act
            var phone = new Phone(Guid.NewGuid(), ddd, number, PhoneType.SmartPhone);

            //Assert
            Assert.Equal(ddd, phone.Ddd);
            Assert.Equal(number, phone.Number);
            Assert.Equal(PhoneType.SmartPhone, phone.PhoneType);
        }

        [Fact(DisplayName = "Criando um telefone residencial valido")]
        [Trait("Dominio", "Employee")]
        public void Phone_CriandoUmTelefoneResidencialValido_ComSucesso()
        {
            //Arrange
            var ddd = "11";
            var number = "47893236";

            //Act
            var phone = new Phone(Guid.NewGuid(), ddd, number, PhoneType.Home);

            //Assert
            Assert.Equal(ddd, phone.Ddd);
            Assert.Equal(number, phone.Number);
            Assert.Equal(PhoneType.Home, phone.PhoneType);
        }

        [Fact(DisplayName = "Criando um telefone comercial valido")]
        [Trait("Dominio", "Employee")]
        public void Phone_CriandoUmTelefoneComercialValido_ComSucesso()
        {
            //Arrange
            var ddd = "11";
            var number = "47893236";
            var celular = "954665152";

            //Act
            var phone1 = new Phone(Guid.NewGuid(), ddd, number, PhoneType.Workstation);
            var phone2 = new Phone(Guid.NewGuid(), ddd, celular, PhoneType.Workstation);

            //Assert
            Assert.Equal(ddd, phone1.Ddd);
            Assert.Equal(number, phone1.Number);
            Assert.Equal(PhoneType.Workstation, phone1.PhoneType);

            Assert.Equal(ddd, phone2.Ddd);
            Assert.Equal(celular, phone2.Number);
            Assert.Equal(PhoneType.Workstation, phone2.PhoneType);
        }


        [Fact(DisplayName = "Criando um telefone celular invalido")]
        [Trait("Dominio", "Employee")]
        public void Phone_CriandoUmTelefoneCelularInvalido_DeveRetonarException()
        {
            //Arrange
            var ddd = "11";
            var number = "95466515";

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => new Phone(Guid.NewGuid(), ddd, number, PhoneType.SmartPhone));
            Assert.Equal(ListPhoneMessages.SMARTPHONE_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Criando um telefone residencial invalido")]
        [Trait("Dominio", "Employee")]
        public void Phone_CriandoUmTelefoneResidencialInvalido_DeveRetonarException()
        {
            //Arrange
            var ddd = "11";
            var number = "4789323";

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => new Phone(Guid.NewGuid(), ddd, number, PhoneType.Home));
            Assert.Equal(ListPhoneMessages.HOME_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Criando um telefone comercial invalido")]
        [Trait("Dominio", "Employee")]
        public void Phone_CriandoUmTelefoneComercialInvalido_DeveRetonarException()
        {
            //Arrange
            var ddd = "11";
            var number1 = "4789323";
            var number2 = "9546651523";

            //Act & Assert
            var result1 = Assert.Throws<DomainException>(() => new Phone(Guid.NewGuid(), ddd, number1, PhoneType.Workstation));
            var result2 = Assert.Throws<DomainException>(() => new Phone(Guid.NewGuid(), ddd, number2, PhoneType.Workstation));

            Assert.Equal(ListPhoneMessages.WORKSTATION_ERRO_MSG, result1.Message);
            Assert.Equal(ListPhoneMessages.WORKSTATION_ERRO_MSG, result2.Message);
        }

        [Fact(DisplayName = "Criando um telefone ddd invalido")]
        [Trait("Dominio", "Employee")]
        public void Phone_CriandoUmTelefoneDDDInvalido_DeveRetonarException()
        {
            //Arrange
            var ddd = "011";
            var number1 = "47893236";

            //Act & Assert
            var result1 = Assert.Throws<DomainException>(() => new Phone(Guid.NewGuid(), ddd, number1, PhoneType.Workstation));

            Assert.Equal(ListPhoneMessages.DDD_ERRO_MSG, result1.Message);
        }

        [Fact(DisplayName = "Atualizando um telefone valido")]
        [Trait("Dominio", "Employee")]
        public void Phone_AtualizandoUmTelefoneValidoo_ComSucesso()
        {
            //Arrange
            var ddd = "11";
            var number = "47893236";
            var phone = new Phone(Guid.NewGuid(), "55", "954665152", PhoneType.Workstation);

            //Act 
            phone.SetPhone(ddd, number);

            //Assert
            Assert.Equal(number, phone.Number);
        }
    }
}
