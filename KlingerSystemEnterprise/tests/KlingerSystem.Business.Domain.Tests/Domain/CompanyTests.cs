using KlingerSystem.Business.Domain.Message;
using KlingerSystem.Business.Domain.Models;
using KlingerSystem.Business.Domain.Models.Enum;
using KlingerSystem.Core.DomainObjects;
using Xunit;

namespace KlingerSystem.Business.Domain.Tests.Domain
{
    public class CompanyTests
    {
        [Fact(DisplayName = "Registrando a primeira empresa")]
        [Trait("Dominio", "Company")]
        public void Company_CadastrandoPrimeiroComercio_DeveExecutarComSucesso()
        {
            //Arrange
            var fantasyName = "Nome Fantasia";
            var company = Company.CompanyFactory.CompanyHeadequarters(fantasyName);

            //Act & Assert
            Assert.Equal(fantasyName, company.FantasyName);
            Assert.Equal(CompanyType.MATRIZ, company.CompanyType);
        }

        [Fact(DisplayName = "Alterando TipoEmpresa")]
        [Trait("Dominio", "Company")]
        public void Company_AtualizandoDadosTipoEmpresa_DeveExecutarComSucesso()
        {
            //Arrange            
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");

            //Act
            company.SetPersonType(PersonType.Physical);

            //Assert            
            Assert.Equal(PersonType.Physical, company.PersonType);
        }

        [Fact(DisplayName = "Incluindo CPF")]
        [Trait("Dominio", "Company")]
        public void Company_AtualizandoDadosTipoEmpresaIncluindoCpf_DeveExecutarComSucesso()
        {
            //Arrange
            var cpf = new Cpf("36018556820");
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            company.SetPersonType(PersonType.Physical);

            //Act
            company.SetCpf(cpf);

            //Assert            
            Assert.Equal(PersonType.Physical, company.PersonType);
            Assert.Equal(cpf, company.Cpf);
        }

        [Fact(DisplayName = "Tentando incluir Cpf para Juridical")]
        [Trait("Dominio", "Company")]
        public void Company_TentandoIncluirCpfParaJurifical_DeveRetonarException()
        {
            //Arrange
            var cpf = new Cpf("36018556820");
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            company.SetPersonType(PersonType.Juridical);

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => company.SetCpf(cpf));
            Assert.Equal(ListCompanyMessages.JURIDICO_CPF_ERRO, result.Message);
        }

        [Fact(DisplayName = "Incluindo Cnpj")]
        [Trait("Dominio", "Company")]
        public void Company_InclindoCnpj_DeveExecutarComSucesso()
        {
            //Arrange
            var cnpj = new Cnpj("50100910000114");
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");

            //Act
            company.SetCnpj(cnpj);

            //Assert
            Assert.Equal(cnpj, company.Cnpj);           
        }

        [Fact(DisplayName = "Tentando incluir Cnpj para Tipo Phisycal")]
        [Trait("Dominio", "Company")]
        public void Company_TentandoIncluirCnpjParaPhisycal_DeveRetonarException()
        {
            //Arrange            
            var cnpj = new Cnpj("50100910000114");
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            company.SetPersonType(PersonType.Physical);

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => company.SetCnpj(cnpj));
            Assert.Equal(ListCompanyMessages.PHISYCAL_CNPJ_ERRO, result.Message);
        }

        [Fact(DisplayName = "Incluindo Razao Social")]
        [Trait("Dominio", "Company")]
        public void Company_IncluindoRazaoSocial_DeveExecutarComSucesso()
        {
            //Arrange
            var razaoSocial = "Seila SA";
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");

            //Act
            company.SetCompanyName(razaoSocial);

            //Assert

            Assert.Equal(razaoSocial, company.CompanyName);
        }

        [Fact(DisplayName = "Alterando Nome Fantasia")]
        [Trait("Dominio", "Company")]
        public void Company_AlterandoNomeFantasia_DeveExecutarComSucesso()
        {
            //Arrange
            var fantasyName = "Seila alteração";
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");

            //Act
            company.SetFantasyName(fantasyName);

            //Assert

            Assert.Equal(fantasyName, company.FantasyName);
        }

        [Fact(DisplayName = "Razao social e nome fantasia para Tipo Physical")]
        [Trait("Dominio", "Company")]
        public void Company_RazaoSocialEFantasiaParaTipoPhysical_DeveRetornarException()
        {
            //Arrange
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            company.SetPersonType(PersonType.Physical);

            //Act & Assert
            var result1 =  Assert.Throws<DomainException>(() => company.SetFantasyName("Nome Fantasia"));
            var result2 = Assert.Throws<DomainException>(() => company.SetCompanyName("Razao Social"));

            Assert.Equal(ListCompanyMessages.PHYSICAL_FANTASYNAME_ERRO, result1.Message);
            Assert.Equal(ListCompanyMessages.PHYSICAL_COMPANYNAME_ERRO, result2.Message);
        }

        [Fact(DisplayName = "Incluindo InscricaoEstadual e Munical")]
        [Trait("Dominio", "Company")]
        public void Company_IncluindoMunicipalStateRegistration_DeveExecutarComSucesso()
        {
            //Arrange
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            var municipalregistration = "12312312313223";
            var stateregistration = "12345678912345";
            //Act
            company.SetMunicipalRegistration(municipalregistration);
            company.SetStateRegistration(stateregistration);

            //Assert
            Assert.Equal(municipalregistration, company.MunicipalRegistration);
            Assert.Equal(stateregistration,company.StateRegistration);
        }

        [Fact(DisplayName = "Incricao Municipal e Estual para Tipo Physical")]
        [Trait("Dominio", "Company")]
        public void Company_StateMunicipalRegistrarionParaTipoPhysical_DeveRetonarException()
        {
            //Arrange
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            company.SetPersonType(PersonType.Physical);
            var municipalregistration = "12312312313223";
            var stateregistration = "123132132132123";

            //Act & Assert
            var result1 = Assert.Throws<DomainException>(() => company.SetMunicipalRegistration(municipalregistration));
            var result2 = Assert.Throws<DomainException>(() => company.SetStateRegistration(stateregistration));

            Assert.Equal(ListCompanyMessages.PHYSICAL_MUNICIPALREGISTRATION_ERRO, result1.Message);
            Assert.Equal(ListCompanyMessages.PHYSICAL_STATEREGISTRATION_ERRO, result2.Message);
        }

        [Fact(DisplayName = "Inserindo Regime Tributario")]
        [Trait("Dominio", "Company")]
        public void Company_InserindoRegimeTributario_DeveExecutarComSucesso()
        {
            //Arrange
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            var taxRegime = TaxRegimeType.NacionalSimples;

            //Act
            company.SetTaxRegime(taxRegime);

            //Assert
            Assert.Equal(taxRegime, company.TaxRegime);
        }

        [Fact(DisplayName = "Insegindo Regime especial de tributação")]
        [Trait("Dominio", "Company")]
        public void Company_InserindoRegimeTributarioEspecial_DeveExecutarComSucesso()
        {
            //Arrange
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            var typeSpecialTaxRegime = TypeSpecialTaxRegime.IndividualMicroentrepreneur;

            //Act
            company.SetTypeSpecialTaxRegime(typeSpecialTaxRegime);

            //Assert
            Assert.Equal(typeSpecialTaxRegime, company.SpecialTaxRegime);
        }

        [Fact(DisplayName = "Validando tamanho dos campos string do Company")]
        [Trait("Dominio", "Company")]
        public void Company_TamanhoStringCampos_DeveRetonarException()
        {
            //Arrange
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");

            //Act & Assert
            var result1 = Assert.Throws<DomainException>(() => company.SetCompanyName(""));
            var result2 = Assert.Throws<DomainException>(() => company.SetFantasyName(""));
            var result3 = Assert.Throws<DomainException>(() => company.SetMunicipalRegistration(""));
            var result4 = Assert.Throws<DomainException>(() => company.SetStateRegistration(""));

            Assert.Equal(ListCompanyMessages.CompanyNameText_Erro, result1.Message);
            Assert.Equal(ListCompanyMessages.FantasyNameText_Erro, result2.Message);
            Assert.Equal(ListCompanyMessages.MunicipalRegistrationText_Erro, result3.Message);
            Assert.Equal(ListCompanyMessages.StateRegistrationText_Erro, result4.Message);
                        
        }

        [Fact(DisplayName = "Company Adiciona um endereco Valido")]
        [Trait("Dominio", "Company")]
        public void Company_AdicionaUmEndereco_ComSucesso()
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
            var company = Company.CompanyFactory.CompanyHeadequarters("Nome Fantasia");
            //Act
            company.SetAddress(new Address(company.Id, zipCode, street, number, bairro, cidade, estado, comple, refe));

            //Assert
            Assert.Equal(zipCode, company.Address.ZipCode);
            Assert.Equal(street, company.Address.Street);
            Assert.Equal(number, company.Address.Number);
            Assert.Equal(bairro, company.Address.Neighborhood);
            Assert.Equal(estado, company.Address.State);
            Assert.Equal(cidade, company.Address.City);
            Assert.Equal(comple, company.Address.Complement);
            Assert.Equal(refe, company.Address.Reference);
        }
    }
}
