using KlingerSystem.Core.DomainObjects;
using System;
using Xunit;

namespace KlingerSystem.Core.Tests
{
    public class DomainObjectsTests
    {
        [Fact(DisplayName = "Entidade CPF Invalido")]
        [Trait("BuildingBlocks", "DomainObjects")]
        public void Cpf_ValidaCPFInvalido_DeveRetornarException()
        {
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => new Cpf(""));
            Assert.Equal(Cpf.CPF_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Entidade CPF valido")]
        [Trait("BuildingBlocks", "DomainObjects")]
        public void Cpf_ValidaCPFValido_CadastraComSucesso()
        {
            //Arrange
            var number = "36018556820";

            //Act
            var cpf = new Cpf(number);

            //Assert
            Assert.Equal(number, cpf.Number);
            Assert.Equal(Cpf.CpfMaxLength, cpf.Number.Length);
        }

        [Fact(DisplayName = "Entidade RG número Invalido")]
        [Trait("BuildingBlocks", "DomainObjects")]
        public void Rg_ValidaRgInvalido_DeveRetornarException()
        {
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => new Rg("", DateTime.Now.AddYears(-5), RgIssuer.SSP));
            Assert.Equal(Rg.RG_ERRO_MSG, result.Message);
        }

        [Theory(DisplayName = "Entidade RG número valido")]
        [InlineData("355725551")]
        [InlineData("12345")]
        [Trait("BuildingBlocks", "DomainObjects")]
        public void Rg_ValidaRgvalido_DeveRetornarTrue(string number)
        {
            //Act & Assert
            var rg = new Rg(number, DateTime.Now.AddDays(-1), RgIssuer.SSP);

            Assert.Equal(number, rg.Number);
        }

        [Fact(DisplayName = "Entidade RG Data Expedição invalida")]
        [Trait("BuildingBlocks", "DomainObjects")]
        public void Rg_ValidaRgvalido_DeveRetornarExpetion()
        {
            //Act & Assert
            Assert.Throws<DomainException>(() => new Rg("12345", DateTime.Now, RgIssuer.SSP));
        }

        [Fact(DisplayName = "Entidade CNPJ Invalido")]
        [Trait("BuildingBlocks", "DomainObjects")]
        public void CNPJ_ValidaCNPJInvalido_DeveRetornarException()
        {
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => new Cnpj(""));
            Assert.Equal(Cnpj.CNPJ_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Entidade CNPJ valido")]
        [Trait("BuildingBlocks", "DomainObjects")]
        public void CNPJ_ValidaCNPJValido_CadastraComSucesso()
        {
            //Arrange
            var number = "58667378000123";

            //Act
            var cnpj = new Cnpj(number);

            //Assert
            Assert.Equal(number, cnpj.Number);
            Assert.Equal(Cnpj.CnpjMaxLength, cnpj.Number.Length);
        }
    }
}
