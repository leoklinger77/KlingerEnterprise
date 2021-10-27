using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Employee.Api.Application.Commands;
using KlingerSystem.Employee.Api.Application.Commands.Messages;
using KlingerSystem.Employee.Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace KlingerSystem.Employee.Api.Tests.CommandsTests
{
    public class EmployeeCommandTests
    {

        [Fact(DisplayName = "RegisterTheFirstEmployee valido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterTheFirstEmployeeCommand_ValidandoComandos_ExecutaComSucesso()
        {
            //Arrange
            var command = new RegisterTheFirstEmployeeCommand(Guid.NewGuid(), Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com");

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "RegisterTheFirstEmployee inválidos")]
        [Trait("Application", "Commands Employee")]
        public void RegisterTheFirstEmployeeCommand_ValidandoComandos_DeveRetornarErros()
        {
            //Arrange
            var command = new RegisterTheFirstEmployeeCommand(Guid.Empty, Guid.Empty, "L r", "leandrogmail.com");

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.BusinessId_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.EmployeeId_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.FullName_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.Email_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "CommonEmployeeRegistration valido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandos_ExecutaComSucesso()
        {
            //Arrange
            var email = "leandro@gmail.com";
            var fullame = "Leandro Klinger";
            var businessId = Guid.NewGuid();
            var command = new CommonEmployeeRegistrationCommand(businessId, fullame, email);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
            Assert.Equal(email, command.Email);
            Assert.Equal(fullame, command.FullName);
            Assert.Equal(businessId, command.BusinessId);
        }

        [Fact(DisplayName = "CommonEmployeeRegistration invalido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandos_DeveRetornarErros()
        {
            //Arrange
            var email = "Leandro@Kmailcom";
            var fullame = "L m";
            var businessId = Guid.Empty;
            var command = new CommonEmployeeRegistrationCommand(businessId, fullame, email);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.BusinessId_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.FullName_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.Email_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "CommonEmployeeRegistration com cpf Opcional valido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandosComCpfValido_ExecutaComSucesso()
        {
            //Arrange            
            var cpf = "36018556820";
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", cpf);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
            Assert.Equal(cpf, command.Cpf);
        }

        [Fact(DisplayName = "CommonEmployeeRegistration com cpf Opcional invalido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandosComCpfInvalido_ExecutaComSucesso()
        {
            //Arrange            
            var cpf = "36018556821";
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", cpf: cpf);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.Cpf_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "CommonEmployeeRegistration com rg Opcional valido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandosComRgValido_ExecutaComSucesso()
        {
            //Arrange            
            var rg = new CommonEmployeeRegistrationCommand.RgCommand("12345", DateTime.Now.AddDays(-5), RgIssuer.SSP);
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", rg: rg);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
            Assert.Equal(rg, command.Rg);
        }

        [Fact(DisplayName = "CommonEmployeeRegistration com rg Opcional invalido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandosComRgInvalido_DeveRetornarErros()
        {
            //Arrange            
            var rg = new CommonEmployeeRegistrationCommand.RgCommand("123", DateTime.Now, 0);
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", rg: rg);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.RgNumber_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.RgExpeditionDate_invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.RgIssuere_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "CommonEmployeeRegistration com dados BirthDate,ImagePath,Commission,Annotation e TypeSexo Valido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandosRestanteDosParemetros_ExecutaComSucesso()
        {
            //Arrange
            var birthDate = DateTime.Now.Date.AddDays(-5);
            var imagePath = Guid.NewGuid() + ".jpg";
            double commission = 1.00;
            var annotation = "Anotação";
            int typeSexo = 1;
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com",
                            birthDate: birthDate,
                            imagePath: imagePath,
                            commission: commission,
                            annotation: annotation,
                            typeSexo: typeSexo);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
            Assert.Equal(birthDate, command.BirthDate);
            Assert.Equal(imagePath, command.ImagePath);
            Assert.Equal(commission, command.Commission);
            Assert.Equal(annotation, command.Annotation);
            Assert.Equal(typeSexo, command.TypeSexo);
        }

        [Fact(DisplayName = "CommonEmployeeRegistration com dados BirthDate,ImagePath,Commission,Annotation e TypeSexo Invalido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_ValidandoCommandosRestanteDosParemetros_DevemRetornarErros()
        {
            //Arrange
            var birthDate = DateTime.Now.Date;
            var imagePath = Guid.Empty + ".jpg";
            double commission = -1;
            var annotation = string.Empty;
            int typeSexo = 4;
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com",
                            birthDate: birthDate,
                            imagePath: imagePath,
                            commission: commission,
                            annotation: annotation,
                            typeSexo: typeSexo);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.BirthDate_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.ImagePath_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.Commission_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));            
            Assert.Contains(CommandMessages.TypeSexo_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "CommonEmployeeRegistration adicionado Endereco Valido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_AdicionadoEndereço_DeveExecutarComSucesso()
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
            var address = new CommonEmployeeRegistrationCommand.AddressCommand(zipCode, street, number, bairro, cidade, estado, complement: comple, reference: refe);
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", address: address);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
            Assert.Equal(zipCode, command.Address.ZipCode);
            Assert.Equal(street, command.Address.Street);
            Assert.Equal(number, command.Address.Number);
            Assert.Equal(bairro, command.Address.Neighborhood);
            Assert.Equal(estado, command.Address.State);
            Assert.Equal(cidade, command.Address.City);
            Assert.Equal(comple, command.Address.Complement);
            Assert.Equal(refe, command.Address.Reference);
        }

        [Fact(DisplayName = "CommonEmployeeRegistration adicionado Endereco inalido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_AdicionadoEndereço_DevemRetornarErros()
        {
            //Arrange
            var zipCode = "";
            var street = "";
            var number = "";
            var bairro = "";
            var cidade = "";
            var estado = "";
            var address = new CommonEmployeeRegistrationCommand.AddressCommand(zipCode, street, number, bairro, cidade, estado, complement: null, reference: null);
            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", address: address);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.AddressZipCode_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressStreet_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressNumber_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressNeighborhood_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressCity_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressState_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));

            Assert.Contains(CommandMessages.AddressZipCodeText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressStreetText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressNeighborhoodText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressCityText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressStateText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.AddressNumberText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));

            Assert.Equal(12, command.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "CommonEmployeeRegistration Adiciando Telefones validos")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_AdicionandoPhone_DeveExecutarComSucesso()
        {
            //Arrange
            var phoneHome = new CommonEmployeeRegistrationCommand.PhoneCommand("11", "47893236", PhoneType.Home);
            var list = new List<CommonEmployeeRegistrationCommand.PhoneCommand>();
            list.Add(phoneHome);

            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", phones: list);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.True(result);
            Assert.Equal(phoneHome, command.Phones.FirstOrDefault(x => x.Number == phoneHome.Number));
        }

        [Fact(DisplayName = "CommonEmployeeRegistration Adiciando Telefones invalidos")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_AdicionandoPhone_DevemRetornarErros()
        {
            //Arrange
            var phoneHome = new CommonEmployeeRegistrationCommand.PhoneCommand("011", "47893", PhoneType.Home);
            var list = new List<CommonEmployeeRegistrationCommand.PhoneCommand>();
            list.Add(phoneHome);

            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", phones: list);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.PhoneDddText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(CommandMessages.PhoneHomeText_Invalido, command.ValidationResult.Errors.Select(x => x.ErrorMessage));
            Assert.Equal(2, command.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "CommonEmployeeRegistration Adiciando Telefones acima do permitido")]
        [Trait("Application", "Commands Employee")]
        public void RegisterFuncionario_AdicionandoPhoneQtdeAcimaDoPermitido_DevemRetornarErros()
        {
            //Arrange
            var phoneHome1 = new CommonEmployeeRegistrationCommand.PhoneCommand("11", "47893236", PhoneType.Home);
            var phoneHome2 = new CommonEmployeeRegistrationCommand.PhoneCommand("11", "12345678", PhoneType.Workstation);
            var phoneHome3 = new CommonEmployeeRegistrationCommand.PhoneCommand("11", "947893236", PhoneType.SmartPhone);
            var phoneHome4NaoPermitido = new CommonEmployeeRegistrationCommand.PhoneCommand("11", "947893236", PhoneType.SmartPhone);
            var list = new List<CommonEmployeeRegistrationCommand.PhoneCommand>();
            list.Add(phoneHome1);
            list.Add(phoneHome2);
            list.Add(phoneHome3);
            list.Add(phoneHome4NaoPermitido);

            var command = new CommonEmployeeRegistrationCommand(Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com", phones: list);

            //Act
            var result = command.IsValid();

            //Assert
            Assert.False(result);
            Assert.Contains(CommandMessages.PhoneQtdeMax, command.ValidationResult.Errors.Select(x => x.ErrorMessage));            
            Assert.Single(command.ValidationResult.Errors);
        }        
    }
}
