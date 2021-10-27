﻿using KlingerSystem.Business.Domain.Models;
using KlingerSystem.Core.DomainObjects;
using Xunit;

namespace KlingerSystem.Business.Domain.Tests.Domain
{
    public class CnaeTests
    {
        [Fact(DisplayName = "Cadastrando um Cnae valido")]
        [Trait("Dominio", "Company")]
        public void Cnae_CadatrandoUmCnae_DeveExecutarComSucesso()
        {
            //Arrange
            var division = "3250706";
            var description = "Serviços de prótese dentária	";

            //Act
            var cnae = new Cnae(division, description);

            //Assert
            Assert.Equal(division, cnae.Devision);
            Assert.Equal(description, cnae.Description);

        }

        [Fact(DisplayName = "Cadastrando um Cnae invalido")]
        [Trait("Dominio", "Company")]
        public void Cnae_CadatrandoUmCnae_DeveRetonarException()
        {
            //Arrange
            var division = "0";
            var description = "Se";

            //Act & Assert
            var result1 = Assert.Throws<DomainException>(() => new Cnae(division, "Serviços de prótese dentária"));
            var result2 = Assert.Throws<DomainException>(() => new Cnae("3250706", description));

            //Assert.Equal(division, result1.Message);
            //Assert.Equal(description, result1.Message);
        }
    }
}
