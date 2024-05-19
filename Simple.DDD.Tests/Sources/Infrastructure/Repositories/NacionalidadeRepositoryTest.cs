using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Exceptions;
using Simple.DDD.Infrastructure.Repositories;
using Simple.DDD.Tests.Configs;
using AutoFixture;
using Bogus;
using Moq;
using Moq.Protected;
using System.Net.Http.Json;
using Xunit;

namespace Simple.DDD.Tests.Sources.Infrastructure.Repositories
{
    public  class NacionalidadeRepositoryTest
    {
        private readonly Fixture _fixture;
        private readonly Faker _faker;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;

        public NacionalidadeRepositoryTest()
        {
            _fixture = FixtureConfig.Get();
            _faker = new Faker();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        }

        [Fact(DisplayName = "Buscar a nacionalidade pelo nome")]
        public async Task GetByName()
        {
            var entity = _fixture.Create<Nacionalidade>();

            var httpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(entity)
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var url = _faker.Internet.Url();

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(url)
            };

            var repository = new NacionalidadeRepository(httpClient);

            var response = await repository.GetNacionalidadeAsync(entity.Name);

            Assert.Equal(response.Name, entity.Name);
        }

        [Fact(DisplayName = "Buscar a nacionalidade pelo nome com erro 500")]
        public async Task GetByNameErro500()
        {
            var nome = _faker.Person.FirstName;

            var httpResponseMessage = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Content = JsonContent.Create("Erro ao fazer a consulta.")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var url = _faker.Internet.Url();

            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(url)
            };

            var repository = new NacionalidadeRepository(httpClient);

            await Assert.ThrowsAnyAsync<InformacaoException>(() => repository.GetNacionalidadeAsync(nome));
        }
    }
}
