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
    [Trait("Service", "Service OrdemServico")]
    public class OrdemServicoServiceTest
    {
        private readonly Mock<IOrdemServicoRepository> _mockOrdemServicoRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Fixture _fixture;
        private readonly Claim[] _claims;

        public OrdemServicoServiceTest()
        {
            _mockOrdemServicoRepository = new Mock<IOrdemServicoRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _fixture = FixtureConfig.Get();
            _claims = _fixture.Create<Usuario>().Claims();
        }

        [Fact(DisplayName = "Lista OrdemServicos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<OrdemServico>>();

            _mockOrdemServicoRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<OrdemServico, bool>>>())).ReturnsAsync(entities);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new OrdemServicoService(_mockOrdemServicoRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterTodosAsync();

            Assert.True(response.ToList().Count() > 0);
        }

        [Fact(DisplayName = "Busca OrdemServico Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<OrdemServico>();

            _mockOrdemServicoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<OrdemServico, bool>>>())).ReturnsAsync(entity);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new OrdemServicoService(_mockOrdemServicoRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterPorIdAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra OrdemServico")]
        public async Task Post()
        {
            var entity = _fixture.Create<OrdemServico>();

            _mockOrdemServicoRepository.Setup(mock => mock.AddAsync(It.IsAny<OrdemServico>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new OrdemServicoService(_mockOrdemServicoRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AdicionarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Edita OrdemServico Existente")]
        public async Task Put()
         {
            var entity = _fixture.Create<OrdemServico>();

            _mockOrdemServicoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<OrdemServico, bool>>>())).ReturnsAsync(entity);
            _mockOrdemServicoRepository.Setup(mock => mock.EditAsync(It.IsAny<OrdemServico>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new OrdemServicoService(_mockOrdemServicoRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AlterarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Remove OrdemServico Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<OrdemServico>();

            _mockOrdemServicoRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockOrdemServicoRepository.Setup(mock => mock.RemoveAsync(It.IsAny<OrdemServico>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new OrdemServicoService(_mockOrdemServicoRepository.Object, _mockHttpContextAccessor.Object);

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
