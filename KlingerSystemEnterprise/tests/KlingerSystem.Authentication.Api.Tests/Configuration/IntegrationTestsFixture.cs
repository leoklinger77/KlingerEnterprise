using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace KlingerSystem.Authentication.Api.Tests.Configuration
{
    [CollectionDefinition(nameof(IntegrationApiTestFixtureCollection))]
    public class IntegrationApiTestFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupApiTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public string UsuarioEmail { get; set; }
        public string UsuarioSenha { get; set; }

        public string UsuarioToken;

        public readonly AuthenticationAppFactory<TStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            //var clientOption = new WebApplicationFactoryClientOptions
            //{
            //    AllowAutoRedirect = true,
            //    BaseAddress = new Uri("http://localhost"),
            //    HandleCookies = true,
            //    MaxAutomaticRedirections = 7
            //};

            Factory = new AuthenticationAppFactory<TStartup>();
            Client = Factory.CreateClient();
        }

        public void GerarUserSenha()
        {
            var faker = new Faker("pt_BR");
            UsuarioEmail = faker.Internet.Email().ToLower();
            UsuarioSenha = faker.Internet.Password(8, false, "", "@1Ab_");
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}
