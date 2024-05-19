using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Exceptions;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Services;
using Simple.DDD.Tests.Configs;
using AutoFixture;
using Bogus;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Simple.DDD.Tests.Sources.Domain.Services
{
    public class EmpresaServiceTest
    {
        private readonly Mock<IEmpresaRepository> _mockEmpresaRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Fixture _fixture;
        private readonly Faker _faker;

        public EmpresaServiceTest()
        {
            _mockEmpresaRepository = new Mock<IEmpresaRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _fixture = FixtureConfig.Get();
            _faker = new Faker();
        }

        [Theory(DisplayName = "Busca um empresa por Id")]
        [InlineData("Cliente")]
        [InlineData("Tecnico")]
        public async Task GetById(string perfil)
        {
            var entity = _fixture.Create<Empresa>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockEmpresaRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Empresa, bool>>>())).ReturnsAsync(entity);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new EmpresaService(_mockEmpresaRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterPorIdAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Theory(DisplayName = "Busca um empresa por Id não existente")]
        [InlineData("Cliente")]
        [InlineData("Tecnico")]
        public async Task GetByIdErro(string perfil)
        {
            var id = _fixture.Create<int>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockEmpresaRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Empresa, bool>>>())).ReturnsAsync((Empresa)null);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new EmpresaService(_mockEmpresaRepository.Object, _mockHttpContextAccessor.Object);

            await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(id));
        }

        [Theory(DisplayName = "Busca todas empresas")]
        [InlineData("Cliente")]
        [InlineData("Tecnico")]
        public async Task Get(string perfil)
        {
            var entities = _fixture.Create<List<Empresa>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockEmpresaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Empresa, bool>>>())).ReturnsAsync(entities);

            var service = new EmpresaService(_mockEmpresaRepository.Object, _mockHttpContextAccessor.Object);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var response = await service.ObterTodosAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Cadastra uma nova empresa")]
        public async Task Post()
        {
            var entity = _fixture.Create<Empresa>();

            _mockEmpresaRepository.Setup(mock => mock.AddAsync(It.IsAny<Empresa>())).Returns(Task.CompletedTask);

            var service = new EmpresaService(_mockEmpresaRepository.Object, _mockHttpContextAccessor.Object);

            var exception = await Record.ExceptionAsync(() => service.AdicionarAsync(entity));
            Assert.Null(exception);
        }

        [Fact(DisplayName = "Atualiza uma empresa existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Empresa>();

            _mockEmpresaRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<Empresa, bool>>>())).ReturnsAsync(entity);
            _mockEmpresaRepository.Setup(mock => mock.EditAsync(It.IsAny<Empresa>())).Returns(Task.CompletedTask);

            var service = new EmpresaService(_mockEmpresaRepository.Object, _mockHttpContextAccessor.Object);

            var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
            Assert.Null(exception);
        }

        [Fact(DisplayName = "Remove uma empresa existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Empresa>();

            _mockEmpresaRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockEmpresaRepository.Setup(mock => mock.RemoveAsync(It.IsAny<Empresa>())).Returns(Task.CompletedTask);

            var service = new EmpresaService(_mockEmpresaRepository.Object, _mockHttpContextAccessor.Object);

            var exception = await Record.ExceptionAsync(() => service.DeletarAsync(entity.Id));
            Assert.Null(exception);
        }
    }
}
