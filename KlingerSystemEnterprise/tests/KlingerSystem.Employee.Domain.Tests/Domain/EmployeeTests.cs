using KlingerSystem.Core.DomainObjects;
using KlingerSystem.Employee.Domain.Message;
using KlingerSystem.Employee.Domain.Models;
using KlingerSystem.Employee.Domain.Models.Enum;
using System;
using System.Linq;
using Xunit;

namespace KlingerSystem.Employee.Domain.Tests.Domain
{
    public class EmployeeTests
    {
        private readonly Models.Employee _employeeAdministrator;        

        public EmployeeTests()
        {
            _employeeAdministrator = Models.Employee.EmployeeFactory.EmployeeAdministrator(Guid.NewGuid(), Guid.NewGuid(), "Leandro Klinger");            
        }

        [Fact(DisplayName = "Primeiro funcionario Administrador")]
        [Trait("Dominio", "Employee")]
        public void Employee_PrimeiroFuncionarioAdiministrador_DeveExecutarComSucesso()
        {
            //Arrange
            var name = "Leandro Klinger";
            var employee = Models.Employee.EmployeeFactory.EmployeeAdministrator(Guid.NewGuid(), Guid.NewGuid(), name);

            //Act & Assert
            Assert.Equal(EmployeeType.Administratior, employee.EmployeeType);
            Assert.Equal(name, employee.FullName);
        }

        [Theory(DisplayName = "Funcionario nome completo inválido")]
        [InlineData("")]
        [InlineData("d")]
        [InlineData("d d")]
        [InlineData("d1 d3")]
        [InlineData(". d3")]
        [InlineData("leo_ds dsds")]
        [Trait("Dominio", "Employee")]
        public void Employee_NomeCompletoInvalido_DeveRetornarException(string fullName)
        {
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => Models.Employee.EmployeeFactory.EmployeeAdministrator(Guid.NewGuid(), Guid.NewGuid(), fullName));
            Assert.Equal(ListEmployeeMessages.FULLNAME_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Funcionario Adiciona CPF")]
        [Trait("Dominio", "Employee")]
        public void Employee_InsereCPF_DeveExecutarComSucesso()
        {
            //Arrange                        
            var cpf = "36018556820";
            //Act
            _employeeAdministrator.AddCpf(new Cpf(cpf));

            //Assert
            Assert.Equal(cpf, _employeeAdministrator.Cpf.Number);
        }

        [Fact(DisplayName = "Funcionario nao pode trocar de CPF")]
        [Trait("Dominio", "Employee")]
        public void Employee_TentaTrocarSeuCPF_DeveRetornarException()
        {
            //Arrange                        
            var cpf = "36018556820";
            _employeeAdministrator.AddCpf(new Cpf(cpf));

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.AddCpf(new Cpf(cpf)));
            Assert.Equal(ListEmployeeMessages.CPF_ALTERACAO_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Funcionario cadastrado seu RG")]
        [Trait("Dominio", "Employee")]
        public void Employee_CadastraRg_DeveExecutarComSucesso()
        {
            //Act
            var rgNumber = "12345";
            _employeeAdministrator.AddRg(new Rg(rgNumber, DateTime.Now.AddDays(-1), RgIssuer.SSP));

            //Assert
            Assert.Equal(rgNumber, _employeeAdministrator.Rg.Number);
        }

        [Fact(DisplayName = "Funcionario nao pode trocar seu RG")]
        [Trait("Dominio", "Employee")]
        public void Employee_TentaTrocarRg_DeveRetonarException()
        {
            //Arrange                        
            _employeeAdministrator.AddRg(new Rg("12345", DateTime.Now.AddDays(-1), RgIssuer.SSP));

            //Act
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.AddRg(new Rg("12345", DateTime.Now.AddDays(-1), RgIssuer.SSP)));
            Assert.Equal(ListEmployeeMessages.RG_ALTERACAO_ERRO_MSG, result.Message);

        }

        [Fact(DisplayName = "Funcionario informa Nascimento Maior que Hoje")]
        [Trait("Dominio", "Employee")]
        public void Employee_NascimentoInvalido_DeveRetornarException()
        {
            //Act & Assert
            var result1 = Assert.Throws<DomainException>(() => _employeeAdministrator.SetBirthDate(DateTime.Now));
            var result2 = Assert.Throws<DomainException>(() => _employeeAdministrator.SetBirthDate(DateTime.Now.AddDays(1)));

            Assert.Equal(ListEmployeeMessages.BIRTHDATE_ERRO_MSG, result1.Message);
            Assert.Equal(ListEmployeeMessages.BIRTHDATE_ERRO_MSG, result2.Message);
        }

        [Fact(DisplayName = "Funcionario Cadastra seu Nascimento")]
        [Trait("Dominio", "Employee")]
        public void Employee_CadastraSeuNascimento_DeveExecutarComSucesso()
        {
            //Arrange                        
            var date = DateTime.Now.AddYears(-25);

            //Act
            _employeeAdministrator.SetBirthDate(date);

            //Assert
            Assert.Equal(date, _employeeAdministrator.BirthDate.Value);
        }

        [Fact(DisplayName = "Funcionario atualiza seu Nascimento")]
        [Trait("Dominio", "Employee")]
        public void Employee_AtualizaSeuNascimento_DeveExecutarComSucesso()
        {
            //Arrange                        
            _employeeAdministrator.SetBirthDate(DateTime.Now.AddYears(-30));

            //Act
            var date = DateTime.Now.AddYears(-25);
            _employeeAdministrator.SetBirthDate(date);

            //Assert
            Assert.Equal(date, _employeeAdministrator.BirthDate.Value);
        }

        [Fact(DisplayName = "Funcionario informa seu Sexo valido")]
        [Trait("Dominio", "Employee")]
        public void Employee_InformaSeuSexo_DeveExecutarComSucesso()
        {
            //Act
            _employeeAdministrator.SetTypeSexo(TypeSexo.Male);

            //Assert
            Assert.Equal(TypeSexo.Male, _employeeAdministrator.TypeSexo);
        }

        [Fact(DisplayName = "Funcionario cadastra uma foto")]
        [Trait("Dominio", "Employee")]
        public void Employee_CadastraUmaFoto_DeveExecutarComSucesso()
        {
            //Act
            var imagePath = Guid.NewGuid().ToString() + ".jpg";
            _employeeAdministrator.SetImagePath(imagePath);

            //Assert
            Assert.Equal(imagePath, _employeeAdministrator.ImagePath);
        }

        [Fact(DisplayName = "Funcionario cadastra uma foto inválida")]
        [Trait("Dominio", "Employee")]
        public void Employee_CadastraUmaFotoInvalida_DeveRetornarException()
        {
            //Act
            var imagePath = Guid.Empty.ToString() + ".jpg";
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.SetImagePath(imagePath));

            //Assert
            Assert.Equal(ListEmployeeMessages.IMAGEPATH_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Funcionario atualiza uma foto")]
        [Trait("Dominio", "Employee")]
        public void Employee_AtualizaUmaFoto_DeveExecutarComSucesso()
        {
            //Arrange                        
            var imagePath = Guid.NewGuid().ToString() + ".jpg";
            _employeeAdministrator.SetImagePath(Guid.NewGuid().ToString() + ".jpg");

            //Act
            _employeeAdministrator.SetImagePath(imagePath);

            //Assert
            Assert.Equal(imagePath, _employeeAdministrator.ImagePath);
        }

        [Fact(DisplayName = "Funcionario insere sua comissão")]
        [Trait("Dominio", "Employee")]
        public void Employee_CadastraSuaComissao_DeveExecutarComSucesso()
        {
            //Arrange                        
            var commission = 2;
            //Act
            _employeeAdministrator.SetCommission(commission);

            //Assert
            Assert.Equal(0.02, _employeeAdministrator.Commission.Value);
        }

        [Fact(DisplayName = "Funcionario insere sua comissão menor que 0")]
        [Trait("Dominio", "Employee")]
        public void Employee_AtualizaSuaComissaoComValorMenorQueZero_DeveRetornarException()
        {
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.SetCommission(Models.Employee.COMMISSION_MIN - 1));
            Assert.Equal(ListEmployeeMessages.COMMISSION_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Funcionario insere sua comissão menor que 100")]
        [Trait("Dominio", "Employee")]
        public void Employee_AtualizaSuaComissaoComValorMenorQueCem_DeveRetornarException()
        {
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.SetCommission(Models.Employee.COMMISSION_MAX + 1));
            Assert.Equal(ListEmployeeMessages.COMMISSION_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Funcionario Adiciona um e-mail")]
        [Trait("Dominio", "Employee")]
        public void Employee_CadastraSeuEmail_DeveExecutarComSucesso()
        {
            //Arrange                                   
            var addressEmail = "leandro@gmail.com";

            //Act
            _employeeAdministrator.SetEmail(new Email(_employeeAdministrator.Id, addressEmail));

            //Assert
            Assert.Equal(addressEmail, _employeeAdministrator.Email.AddressEmail);
        }

        [Fact(DisplayName = "Funcionario atualiza seu um e-mail")]
        [Trait("Dominio", "Employee")]
        public void Employee_AtualizaSeuEmail_ComSucesso()
        {
            //Arrange                                    
            var addressEmail = "leandro@gmail.com";
            _employeeAdministrator.SetEmail(new Email(_employeeAdministrator.Id, "lydia@gmail.com"));
            //Act

            _employeeAdministrator.Email.SetEmail(addressEmail);

            //Assert
            Assert.Equal(addressEmail, _employeeAdministrator.Email.AddressEmail);
        }

        [Fact(DisplayName = "Funcionario Adiciona um endereco Valido")]
        [Trait("Dominio", "Employee")]
        public void Employee_AdicionaUmEndereco_ComSucesso()
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
            //Act
            _employeeAdministrator.SetAddress(new Address(_employeeAdministrator.Id, zipCode, street, number, bairro, cidade, estado, comple, refe));

            //Assert
            Assert.Equal(zipCode, _employeeAdministrator.Address.ZipCode);
            Assert.Equal(street, _employeeAdministrator.Address.Street);
            Assert.Equal(number, _employeeAdministrator.Address.Number);
            Assert.Equal(bairro, _employeeAdministrator.Address.Neighborhood);
            Assert.Equal(estado, _employeeAdministrator.Address.State);
            Assert.Equal(cidade, _employeeAdministrator.Address.City);
            Assert.Equal(comple, _employeeAdministrator.Address.Complement);
            Assert.Equal(refe, _employeeAdministrator.Address.Reference);
        }

        [Fact(DisplayName = "Funcionario Atualiza um endereco Valido")]
        [Trait("Dominio", "Employee")]
        public void Employee_AtualizaUmEndereco_ComSucesso()
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
            _employeeAdministrator.SetAddress(new Address(Guid.NewGuid(), "12345678", "Rua X", "789", "Fe", "Barueri", "RJ", "Teste", "Centro"));

            //Act

            _employeeAdministrator.Address.SetAddress(zipCode, street, number, bairro, cidade, estado, comple, refe);

            //Assert
            Assert.Equal(zipCode, _employeeAdministrator.Address.ZipCode);
            Assert.Equal(street, _employeeAdministrator.Address.Street);
            Assert.Equal(number, _employeeAdministrator.Address.Number);
            Assert.Equal(bairro, _employeeAdministrator.Address.Neighborhood);
            Assert.Equal(estado, _employeeAdministrator.Address.State);
            Assert.Equal(cidade, _employeeAdministrator.Address.City);
            Assert.Equal(comple, _employeeAdministrator.Address.Complement);
            Assert.Equal(refe, _employeeAdministrator.Address.Reference);
        }

        [Fact(DisplayName = "Funcionario adiciona um Contato Telefone")]
        [Trait("Dominio", "Employee")]
        public void Employee_AdicionaUmTelefone_ComSucesso()
        {
            //Arrange            
            var ddd = "11";
            var number = "47893236";

            //Act
            _employeeAdministrator.AddPhone(new Phone(_employeeAdministrator.Id, ddd, number, PhoneType.Workstation));

            //Assert
            Assert.Equal(number, _employeeAdministrator.Phones.Where(x => x.Number == number).FirstOrDefault().Number);
        }

        [Fact(DisplayName = "Funcionario adiciona Telefones mais que o permitido")]
        [Trait("Dominio", "Employee")]
        public void Employee_AdicionaTelefoneMaisQueOPermitido_DeveRetornarException()
        {
            //Arrange            
            _employeeAdministrator.AddPhone(new Phone(_employeeAdministrator.Id, "11", "954665152", PhoneType.Workstation));
            _employeeAdministrator.AddPhone(new Phone(_employeeAdministrator.Id, "11", "954665151", PhoneType.Workstation));
            _employeeAdministrator.AddPhone(new Phone(_employeeAdministrator.Id, "11", "954665153", PhoneType.Workstation));

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.AddPhone(new Phone(_employeeAdministrator.Id, "11", "954665155", PhoneType.Workstation)));
            Assert.Equal(ListEmployeeMessages.PHONE_COUNT_MAX_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Funcionario adiciona Telefones repetidos")]
        [Trait("Dominio", "Employee")]
        public void Employee_AdicionaTelefoneTelefoneRepetido_DeveRetornarException()
        {
            //Arrange            
            _employeeAdministrator.AddPhone(new Phone(_employeeAdministrator.Id, "11", "47893236", PhoneType.Home));

            //Act & Assert
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.AddPhone(new Phone(_employeeAdministrator.Id, "11", "47893236", PhoneType.Workstation)));
            Assert.Equal(ListEmployeeMessages.NUMBER_REPIT_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Funcionario Atualiza um Telefone")]
        [Trait("Dominio", "Employee")]
        public void Employee_AtualizaUmTelefone_ExecutaComSucesso()
        {
            //Arrange            
            var phone = new Phone(_employeeAdministrator.Id, "11", "47893236", PhoneType.Home);

            var ddd = "55";
            var number = "12345678";
            _employeeAdministrator.AddPhone(phone);

            //Act
            _employeeAdministrator.Phones.FirstOrDefault(x => x.Id == phone.Id).SetPhone(ddd, number);

            //Assert
            Assert.Equal(ddd, _employeeAdministrator.Phones.FirstOrDefault(x => x.Id == phone.Id).Ddd);
            Assert.Equal(number, _employeeAdministrator.Phones.FirstOrDefault(x => x.Id == phone.Id).Number);
        }

        [Fact(DisplayName = "Funcionario insere sua anotação")]
        [Trait("Dominio", "Employee")]
        public void Employee_CadastraUmaAnotacao_Executa()
        {
            //Arrange                        
            var annotation = "Anotação do funcionario";

            //Act
            _employeeAdministrator.SetAnnotation(annotation);

            //Assert
            Assert.Equal(annotation, _employeeAdministrator.Annotation);
        }

        [Fact(DisplayName = "Funcionario insere sua anotação invalida")]
        [Trait("Dominio", "Employee")]
        public void Employee_FuncionarioCadastraUmaAnotacaoInvalida_DeveRetornarException()
        {
            //Act & Assert
            var result = Assert.Throws<DomainException>(() => _employeeAdministrator.SetAnnotation(string.Empty));
            Assert.Equal(ListEmployeeMessages.ANNOTATION_REPIT_ERRO_MSG, result.Message);
        }

        [Fact(DisplayName = "Criando um funcionario Comum simples")]
        [Trait("Dominio", "Employee")]
        public void Employee_AdicionandoUmFuncionarioComum_ComSucesso()
        {
            //Arrange
            var nome = "Leandro Klinger";

            //Act
            var employee = Models.Employee.EmployeeFactory.EmployeeCommom(Guid.NewGuid(), nome);

            //Assert
            Assert.Equal(nome, employee.FullName);
        }

        [Fact(DisplayName = "Criando um funcionario Comum Completo")]
        [Trait("Dominio", "Employee")]
        public void Employee_AdicionandoUmFuncionarioCompleto_ComSucesso()
        {
            //Arrange
            var nome = "Leandro Klinger";
            var cpf = new Cpf("36018556820");
            var rg = new Rg("355725551", DateTime.Now.AddDays(-1), RgIssuer.SSP);
            var birthDate = DateTime.Now.AddYears(-20);
            var imagePath = Guid.NewGuid() + ".jpg";
            var commision = 1.00;
            var annotation = "Anotação do funcionario";
            var typeSexo = TypeSexo.Male;

            //Act
            var employee = Models.Employee.EmployeeFactory.EmployeeCommom(Guid.NewGuid(), nome, cpf, rg, birthDate, imagePath, commision, annotation, typeSexo);
            //Assert
            Assert.Equal(nome, employee.FullName);
            Assert.Equal(nome, employee.FullName);
            Assert.Equal(cpf, employee.Cpf);
            Assert.Equal(rg, employee.Rg);
            Assert.Equal(birthDate, employee.BirthDate);
            Assert.Equal(imagePath, employee.ImagePath);
            Assert.Equal(commision / 100, employee.Commission);
            Assert.Equal(annotation, employee.Annotation);
            Assert.Equal(typeSexo, employee.TypeSexo);
        }
    }
}
