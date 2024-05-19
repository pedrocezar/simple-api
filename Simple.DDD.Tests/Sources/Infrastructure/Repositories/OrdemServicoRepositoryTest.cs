using Simple.DDD.Domain.Entities;
using Simple.DDD.Infrastructure.Contexts;
using Simple.DDD.Infrastructure.Repositories;
using Simple.DDD.Tests.Configs;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Simple.DDD.Tests.Sources.Infrastructure.Repositories
{
    [Trait("Repository", "Repository OrdemServico")]
    public class OrdemServicoRepositoryTest
    {
        private readonly Mock<ManutencaoContext> _mockContext;
        private readonly Fixture _fixture;

        public OrdemServicoRepositoryTest()
        {
            _mockContext = new Mock<ManutencaoContext>(new DbContextOptionsBuilder<ManutencaoContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista OrdemServicos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<OrdemServico>>();

            _mockContext.Setup(mock => mock.Set<OrdemServico>()).ReturnsDbSet(entities);

            var repository = new OrdemServicoRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca OrdemServico Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<OrdemServico>();

            _mockContext.Setup(mock => mock.Set<OrdemServico>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new OrdemServicoRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra OrdemServico")]
        public async Task Post()
        {
            var entity = _fixture.Create<OrdemServico>();

            _mockContext.Setup(mock => mock.Set<OrdemServico>()).ReturnsDbSet(new List<OrdemServico> { new OrdemServico() });

            var repository = new OrdemServicoRepository(_mockContext.Object);

            try
            {
                await repository.AddAsync(entity);
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

            _mockContext.Setup(mock => mock.Set<OrdemServico>()).ReturnsDbSet(new List<OrdemServico> { new OrdemServico() });

            var repository = new OrdemServicoRepository(_mockContext.Object);

            try
            {
                await repository.EditAsync(entity);
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

            _mockContext.Setup(mock => mock.Set<OrdemServico>()).ReturnsDbSet(new List<OrdemServico> { entity });

            var repository = new OrdemServicoRepository(_mockContext.Object);

            try
            {
                await repository.RemoveAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
