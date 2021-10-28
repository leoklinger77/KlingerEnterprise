using KlingerSystem.Authentication.Api.Models;
using KlingerSystem.Authentication.Api.Tests.Configuration;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace KlingerSystem.Authentication.Api.Tests.Integration
{
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class OnboardingTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public OnboardingTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Realizar cadastro com sucesso")]
        [Trait("Integração", "Onboarding")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {
            var userData = new UserRegister
            {
                Email = "teste@teste.com",
                Password = "Teste@123",
                ConfirmPassword = "Teste@123",
                FantasyName = "Leandro Klinger",
                FullName = "Leandro klinger"
            };

            // Recriando o client para evitar configurações de Web
            //_testsFixture.Client = _testsFixture.Factory.CreateClient();

            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Authentication/Register", userData);
            response.EnsureSuccessStatusCode();
            _testsFixture.UsuarioToken = await response.Content.ReadAsStringAsync();
        }

    }
}
