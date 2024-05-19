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
    [Trait("Service", "Service Servico")]
    public class ServicoServiceTest
    {
        private readonly Mock<IServicoRepository> _mockServicoRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Fixture _fixture;
        private readonly Claim[] _claims;

        public ServicoServiceTest()
        {
            _mockServicoRepository = new Mock<IServicoRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _fixture = FixtureConfig.Get();
            _claims = _fixture.Create<Usuario>().Claims();
        }

        [Fact(DisplayName = "Lista Servicos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Servico>>();

            _mockServicoRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Servico, bool>>>())).ReturnsAsync(entities);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new ServicoService(_mockServicoRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterTodosAsync();

            Assert.True(response.ToList().Count() > 0);
        }

        [Fact(DisplayName = "Busca Servico Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Servico>();

            _mockServicoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Servico, bool>>>())).ReturnsAsync(entity);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new ServicoService(_mockServicoRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterPorIdAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Servico")]
        public async Task Post()
        {
            var entity = _fixture.Create<Servico>();

            _mockServicoRepository.Setup(mock => mock.AddAsync(It.IsAny<Servico>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new ServicoService(_mockServicoRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AdicionarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Edita Servico Existente")]
        public async Task Put()
         {
            var entity = _fixture.Create<Servico>();

            _mockServicoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<Servico, bool>>>())).ReturnsAsync(entity);
            _mockServicoRepository.Setup(mock => mock.EditAsync(It.IsAny<Servico>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new ServicoService(_mockServicoRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AlterarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Remove Servico Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Servico>();

            _mockServicoRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockServicoRepository.Setup(mock => mock.RemoveAsync(It.IsAny<Servico>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new ServicoService(_mockServicoRepository.Object, _mockHttpContextAccessor.Object);

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
