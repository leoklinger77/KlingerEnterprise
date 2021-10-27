using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Business.Domain.Models;
using KlingerSystem.Core.DomainObjects;
using System;
using Xunit;

namespace KlingerSystem.Business.Domain.Tests.Domain
{
    public class EmailTests
    {
        [Fact(DisplayName = "Criando um email")]
        [Trait("Dominio", "Company")]
        public void Email_CriandoUmEmailValido_CadastroComSucesso()
        {
            //Arrange
            var addressEmail = "leandro@gmail.com";
            var email = new Email(Guid.NewGuid(), addressEmail);

            //Act & Assert
            Assert.Equal(addressEmail, email.AddressEmail);
        }

        [Fact(DisplayName = "Tentativa de Criar email, com email invalido")]
        [Trait("Dominio", "Company")]
        public void Email_TentarCriarUmEmailInvalido_DeveRetornarException()
        {
            //Arrange
            var addressEmail = "leandrogmail.com";

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => new Email(Guid.NewGuid(), addressEmail));
            Assert.Equal(ListEmailMessages.EmailAddress_Erro_MSG, result.Message);
        }

        [Fact(DisplayName = "Tentativa de Criar email, com funcionario inválido")]
        [Trait("Dominio", "Company")]
        public void Email_TentarCriarUmEmailFuncionarioIdInvalido_DeveRetornarException()
        {
            //Arrange
            var addressEmail = "leandrogmail.com";

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => new Email(Guid.Empty, addressEmail));
            Assert.Equal(ListEmailMessages.CompanyId_ERRO_MGS, result.Message);
        }

        [Fact(DisplayName = "Atualiza seu email")]
        [Trait("Dominio", "Company")]
        public void Email_AtualizaSeuEmail_ComSucesso()
        {
            //Arrange
            var addressEmail = "leandro@gmail.com";
            var email = new Email(Guid.NewGuid(), "dsadsa@gmail.com");

            //Act 
            email.SetEmail(addressEmail);

            //Assert
            Assert.Equal(addressEmail, email.AddressEmail);
        }
    }
}
