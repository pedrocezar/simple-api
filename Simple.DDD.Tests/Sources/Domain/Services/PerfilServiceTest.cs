using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Services;
using Simple.DDD.Tests.Configs;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;
using Xunit;

namespace Simple.DDD.Tests.Sources.Domain.Services
{
    [Trait("Service", "Service Pefil")]
    public class PerfilServiceTest
    {
        private readonly Mock<IPerfilRepository> _mockPerfilRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Fixture _fixture;
        private readonly Claim[] _claims;

        public PerfilServiceTest()
        {
            _mockPerfilRepository = new Mock<IPerfilRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _fixture = FixtureConfig.Get();
            _claims = _fixture.Create<Usuario>().Claims();
        }

        [Fact(DisplayName = "Lista Perfis")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Perfil>>();

            _mockPerfilRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Perfil, bool>>>())).ReturnsAsync(entities);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterTodosAsync();

            Assert.True(response.ToList().Count() > 0);
        }

        [Fact(DisplayName = "Busca Perfil Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Perfil>();

            _mockPerfilRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Perfil, bool>>>())).ReturnsAsync(entity);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterPorIdAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Perfil")]
        public async Task Post()
        {
            var entity = _fixture.Create<Perfil>();

            _mockPerfilRepository.Setup(mock => mock.AddAsync(It.IsAny<Perfil>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AdicionarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Edita Perfil Existente")]
        public async Task Put()
         {
            var entity = _fixture.Create<Perfil>();

            _mockPerfilRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<Perfil, bool>>>())).ReturnsAsync(entity);
            _mockPerfilRepository.Setup(mock => mock.EditAsync(It.IsAny<Perfil>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AlterarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Remove Pefil Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Perfil>();

            _mockPerfilRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockPerfilRepository.Setup(mock => mock.RemoveAsync(It.IsAny<Perfil>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.DeletarAsync(entity.Id);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
