using KlingerSystem.Business.Api.Application.Commands;
using Xunit;

namespace KlingerSystem.Business.Api.Tests.CommandsTests
{
    public class BusinessCommandTests
    {
        [Fact(DisplayName = "Registrando o primeiro comercio")]
        [Trait(" Application", "Commands Business")]
        public void Company_RegistrandoSeuPrimeiroComercio_DeveExecutarComSucesso()
        {
            //Arrange
            var command = new RegisterTheCompanyRegistrationCommand("Nome Fantasia");

            //Act 
            var result = command.IsValid();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Registrando o primeiro comercio")]
        [Trait("Application", "Commands Business")]
        public void Company_RegistrandoSeuPrimeiroComercioComNomeInvalido_DeveRetornarException()
        {
            //Arrange
            var command = new RegisterTheCompanyRegistrationCommand("");

            //Act 
            var result = command.IsValid();

            //Assert
            Assert.False(result);
        }
    }
}
