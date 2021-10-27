using KlingerSystem.Employee.Api.Application.Commands;
using KlingerSystem.Employee.Api.Application.Commands.Handler;
using KlingerSystem.Employee.Api.Application.Commands.Messages;
using KlingerSystem.Employee.Domain.Interfaces;
using MediatR;
using Moq;
using Moq.AutoMock;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace KlingerSystem.Employee.Api.Tests.CommandsTests
{
    public class EmployeeCommandHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly EmployeeCommandHandler _handler;

        public EmployeeCommandHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<EmployeeCommandHandler>();
        }

        [Fact(DisplayName = "Primero funcionario registrado")]
        [Trait("Application", "Commands Employee")]
        public async Task Adicionar_RegistarPrimeiroFuncionario_DeveExecutarComSucesso()
        {
            //Arrange
            var command = new RegisterTheFirstEmployeeCommand(Guid.NewGuid(), Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com");

            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            _mocker.GetMock<IMediator>().Setup(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None)).Returns(Task.CompletedTask);

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IEmployeeRepository>().Verify(r => r.Insert(It.IsAny<Domain.Models.Employee>()), Times.Once);
            _mocker.GetMock<IEmployeeRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Primero funcionario registrado com dados inválidos")]
        [Trait("Application", "Commands Employee")]
        public async Task Adicionar_RegistarPrimeiroFuncionario_DeveRetornarErros()
        {
            //Arrange
            var command = new RegisterTheFirstEmployeeCommand(Guid.Empty, Guid.Empty, "s", "leandrogmail.com");

            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(4, result.Errors.Count);
            Assert.Single(result.Errors.Where(x => x.ErrorMessage == CommandMessages.FullName_Invalido));
        }

        [Fact(DisplayName = "Funcionario registrado com dados repetido")]
        [Trait("Application", "Commands Employee")]
        public async Task Adicionar_RegistarPrimeiroFuncionarioComMesmoEmail_DeveRetornarErros()
        {
            //Arrange
            var businessId = Guid.NewGuid();
            var command = new RegisterTheFirstEmployeeCommand(businessId, Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com");
            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var employee = Domain.Models.Employee.EmployeeFactory.EmployeeAdministrator(command.EmployeeId, command.BusinessId, command.FullName);
            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.FindEmployeeByEmail(command.Email)).Returns(Task.FromResult(employee));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            _mocker.GetMock<IEmployeeRepository>().Verify(r => r.FindEmployeeByEmail(command.Email), Times.Once);
        }

        [Fact(DisplayName = "Primeiro funcionario para um comercio que ja tem funcionarios")]
        [Trait("Application", "Commands Employee")]
        public async Task Adicionar_PrimeiroFuncComercioComFuncionariosJaCadastrados_DeveRetornarErros()
        {
            //Arrange
            var businessId = Guid.NewGuid();
            var command = new RegisterTheFirstEmployeeCommand(businessId, Guid.NewGuid(), "Leandro Klinger", "leandro@gmail.com");
            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var employee = Domain.Models.Employee.EmployeeFactory.EmployeeAdministrator(command.EmployeeId, businessId, command.FullName);
            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.FindEmployeeByCompanyId(command.BusinessId)).Returns(Task.FromResult(employee));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact(DisplayName = "Cadatrando um funcionario Comum")]
        [Trait("Application", "Commands Employee")]
        public async Task Adiconar_FuncionarioCommom_DeveExecutarComSucesso()
        {
            //Arrange
            var businessId = Guid.NewGuid();
            var command = new CommonEmployeeRegistrationCommand(businessId, "Leandro Klinger", "leandro@gmail.com");

            var employee = Domain.Models.Employee.EmployeeFactory.EmployeeAdministrator(Guid.NewGuid(), businessId, command.FullName);
            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.FindEmployeeByCompanyId(command.BusinessId)).Returns(Task.FromResult(employee));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
            _mocker.GetMock<IEmployeeRepository>().Verify(r => r.Insert(It.IsAny<Domain.Models.Employee>()), Times.Once);
            _mocker.GetMock<IEmployeeRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Cadatrando um funcionario Comum de uma empresa nao cadastrada")]
        [Trait("Application", "Commands Employee")]
        public async Task Adiconar_FuncionarioCommomDeEmpresaNaoCadastrada_DeveRetornarErros()
        {
            //Arrange
            var businessId = Guid.NewGuid();
            var command = new CommonEmployeeRegistrationCommand(businessId, "Leandro Klinger", "leandro@gmail.com");
            _mocker.GetMock<IEmployeeRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(CommandMessages.Erro_FalhaAoRegistrarFuncionario, result.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Cadatrando um funcionario Comum com email já existente")]
        [Trait("Application", "Commands Employee")]
        public async Task Adiconar_FuncionarioCommomComEmailRepetido_DeveRetornarErros()
        {
            //Arrange
            var businessId = Guid.NewGuid();
            var email = "leandro@gmail.com";
            var command = new CommonEmployeeRegistrationCommand(businessId, "Leandro Klinger", email);
            var employeeRetornoEmpresaExistente = Domain.Models.Employee.EmployeeFactory.EmployeeAdministrator(Guid.NewGuid(), businessId, command.FullName);
            var employeeRetornoEmailExistente = Domain.Models.Employee.EmployeeFactory.EmployeeAdministrator(Guid.NewGuid(), businessId, command.FullName);

            _mocker.GetMock<IEmployeeRepository>()
                .Setup(r => r.UnitOfWork.Commit())
                .Returns(Task.FromResult(true));

            _mocker.GetMock<IEmployeeRepository>()
                .Setup(r => r.FindEmployeeByCompanyId(command.BusinessId))
                .Returns(Task.FromResult(employeeRetornoEmpresaExistente));

            _mocker.GetMock<IEmployeeRepository>()
                .Setup(r => r.FindEmployeeByEmailWithBusiness(command.BusinessId, email))
                .Returns(Task.FromResult(employeeRetornoEmailExistente));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            
            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            _mocker.GetMock<IEmployeeRepository>().Verify(r => r.FindEmployeeByEmailWithBusiness(businessId, email), Times.Once);
            Assert.Contains(CommandMessages.Erro_EmailJaCadastrado, result.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Cadatrando um funcionario Comum com Cpf repetido")]
        [Trait("Application", "Commands Employee")]
        public async Task Adiconar_FuncionarioCommomComCpf_DeveRetornarErros()
        {
            //Arrange
            var businessId = Guid.NewGuid();
            var cpf = "36018556820";
            var command = new CommonEmployeeRegistrationCommand(businessId, "Leandro Klinger", "Leandro@Klinger.com",cpf: cpf);
            var employeeRetornoEmpresaExistente = Domain.Models.Employee.EmployeeFactory.EmployeeAdministrator(Guid.NewGuid(), businessId, command.FullName);


            _mocker.GetMock<IEmployeeRepository>()
                .Setup(r => r.FindEmployeeByCompanyId(command.BusinessId))
                .Returns(Task.FromResult(employeeRetornoEmpresaExistente));

            _mocker.GetMock<IEmployeeRepository>()
                .Setup(r => r.FindEmployeeByCpf(command.BusinessId, cpf))
                .Returns(Task.FromResult(employeeRetornoEmpresaExistente));

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            _mocker.GetMock<IEmployeeRepository>().Verify(r => r.FindEmployeeByCpf(businessId, cpf), Times.Once);
            Assert.Contains(CommandMessages.Erro_CpfJaCadastrado, result.Errors.Select(x => x.ErrorMessage));
        }

    }
}
