using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Core.Tools;
using System.Text.RegularExpressions;
using Xunit;

namespace KlingerSystem.Core.Tests
{
    public class ToolsTests
    {
        [Theory(DisplayName = "Valida CNPJ Vardadeiro")]
        [InlineData("31580229000176")]
        [InlineData("39731894000125")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void CNPJ_ValidaCNPJ_RetornaSucesso(string cnpj)
        {
            Assert.True(cnpj.IsCnpj());
        }

        [Theory(DisplayName = "Valida CNPJ Inválido")]
        [InlineData("3601855880")]
        [InlineData("360185588")]
        [InlineData("36018558811")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void CNPJ_ValidaCNPJ_DeveRetornarFalso(string cnpj)
        {
            Assert.False(cnpj.IsCnpj());
        }


        [Theory(DisplayName = "Valida CPF Vardadeiro")]
        [InlineData("36018556820")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void CPF_ValidaCPF_RetornaSucesso(string cpf)
        {
            Assert.True(cpf.IsCpf());
        }

        [Theory(DisplayName = "Valida CPF Inválido")]
        [InlineData("3601855880")]
        [InlineData("360185588")]
        [InlineData("36018558811")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void CPF_ValidaCPF_DeveRetornarFalso(string cpf)
        {
            Assert.False(cpf.IsCpf());
        }

        [Theory(DisplayName = "Valida Nome Completo invalido")]
        [InlineData("d")]
        [InlineData("d d")]
        [InlineData("d1 d3")]
        [InlineData(". dd")]
        [InlineData("leo_ds dsds")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void FullName_ValidaNomeCompleto_DeveRetornarFalso(string fullName)
        {
            Assert.False(fullName.IsFullName());
        }

        [Theory(DisplayName = "Valida Nome Completo valido")]
        [InlineData("Leo Klinger")]
        [InlineData("Leandro Klinger de Oliveira Proença")]
        [InlineData("Lydia Stuani")]
        [InlineData("Jasminy de Cássia")]
        [InlineData("Maria Aparecia de Oliveira")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void FullName_ValidaNomeCompleto_DevemRetornarTrue(string fullName)
        {
            Assert.True(fullName.IsFullName());
        }

        [Theory(DisplayName = "Valida Email valido")]
        [InlineData("leandro@gmai.com")]
        [InlineData("leandro.k@gmai.com")]
        [InlineData("leandro-k@gmai.com")]
        [InlineData("leandro_k@gmai.com")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void Email_ValidaEmail_DevemRetornarTrue(string email)
        {
            Assert.True(email.IsEmail());
        }

        [Theory(DisplayName = "Valida Email invalido")]
        [InlineData("leandro.@gmaicom")]
        [InlineData("leandro-@gmai.com.com")]
        [InlineData("leandro_gmai.com")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void Email_ValidaEmailInvalido_DevemRetornarExeption(string email)
        {
            Assert.False(email.IsEmail());
        }

        [Fact(DisplayName = "ValidateIfEqual")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateIfEqual_ValidarSeIgual_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => Validation.ValidateIfEqual(1, 1, message));
            Assert.Equal(message, result.Message);
        }

        [Fact(DisplayName = "ValidateIfdifferent")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateIfdifferent_ValidarSeForDiferenet_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => Validation.ValidateIfdifferent(1, 2, message));
            Assert.Equal(message, result.Message);
        }

        [Fact(DisplayName = "CharactersValidate max")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void CharactersValidate_ValidarCaracteresMaximosFornecidos_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => Validation.CharactersValidate("123", 2, message));
            Assert.Equal(message, result.Message);
        }

        [Fact(DisplayName = "CharactersValidate min e max")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void CharactersValidate_ValidarCaracteresMinimosMaximosFornecidos_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => Validation.CharactersValidate("1", 3, 2, message));
            Assert.Equal(message, result.Message);
        }

        [Fact(DisplayName = "CharactersValidate max e min")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void CharactersValidate_ValidarCaracteresMaximoEMinimoFornecidos_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => Validation.CharactersValidate("1234", 3, 2, message));
            Assert.Equal(message, result.Message);
        }

        [Fact(DisplayName = "ValidateIsNullOrEmpty")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateIsNullOrEmpty_ValidarSeNullOuVazio_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => Validation.ValidateIsNullOrEmpty(string.Empty, message));

            Assert.Equal(message, result.Message);
        }

        [Fact(DisplayName = "ValidateMinMax Double, Float, int, long, decimal")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateMinMax_ValidarMinMax_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var resultDouble = Assert.Throws<DomainException>(() => Validation.ValidateMinMax(double.Parse("5.00"), double.Parse("10.00"), double.Parse("20.00"), message));
            var resultFloat = Assert.Throws<DomainException>(() => Validation.ValidateMinMax(float.Parse("5.00"), float.Parse("10.00"), float.Parse("20.00"), message));
            var resultInt = Assert.Throws<DomainException>(() => Validation.ValidateMinMax(int.Parse("5"), int.Parse("10"), int.Parse("20"), message));
            var resultLong = Assert.Throws<DomainException>(() => Validation.ValidateMinMax(long.Parse("5"), long.Parse("10"), long.Parse("20"), message));
            var resultDecimal = Assert.Throws<DomainException>(() => Validation.ValidateMinMax(decimal.Parse("5.00"), decimal.Parse("10.00"), decimal.Parse("20.00"), message));

            Assert.Equal(message, resultDouble.Message);
            Assert.Equal(message, resultFloat.Message);
            Assert.Equal(message, resultInt.Message);
            Assert.Equal(message, resultLong.Message);
        }

        [Fact(DisplayName = "ValidateIfLessThan Double, Float, int, long, decimal")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateIfLessThan_ValidarSeForMenorQue_DeveRetorarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";
            //Act & Assert
            var resultDouble = Assert.Throws<DomainException>(() => Validation.ValidateIfLessThan(double.Parse("5.00"), double.Parse("10.00"), message));
            var resultFloat = Assert.Throws<DomainException>(() => Validation.ValidateIfLessThan(float.Parse("5.00"), float.Parse("10.00"), message));
            var resultInt = Assert.Throws<DomainException>(() => Validation.ValidateIfLessThan(int.Parse("5"), int.Parse("10"), message));
            var resultLong = Assert.Throws<DomainException>(() => Validation.ValidateIfLessThan(long.Parse("5"), long.Parse("10"), message));
            var resultDecimal = Assert.Throws<DomainException>(() => Validation.ValidateIfLessThan(decimal.Parse("5.00"), decimal.Parse("20.00"), message));

            Assert.Equal(message, resultDouble.Message);
            Assert.Equal(message, resultFloat.Message);
            Assert.Equal(message, resultInt.Message);
            Assert.Equal(message, resultLong.Message);
        }

        [Fact(DisplayName = "ValidateIfFalse")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateIfFalse_ValidarSeFalso_DeveRetornarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";

            //Act
            var resultDouble = Assert.Throws<DomainException>(() => Validation.ValidateIfFalse(false, message));

            //Assert
            Assert.Equal(message, resultDouble.Message);
        }

        [Fact(DisplayName = "ValidateIfTrue")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateIfTrue_ValidarSeTrue_DeveRetornarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";

            //Act
            var resultDouble = Assert.Throws<DomainException>(() => Validation.ValidateIfTrue(true, message));

            //Assert
            Assert.Equal(message, resultDouble.Message);
        }

        [Fact(DisplayName = "ValidateIfContains")]
        [Trait("BuildingBlocks", "ValidationMethods")]
        public void ValidateIfContains_ValidarSeContainValor_DeveRetornarException()
        {
            //Arrange            
            var message = "Mensagem Fornecida";

            //Act
            var resultDouble = Assert.Throws<DomainException>(() => Validation.ValidateIfContains("Leandro Klinger", "Lea", message));

            //Assert
            Assert.Equal(message, resultDouble.Message);
        }        
    }
}
