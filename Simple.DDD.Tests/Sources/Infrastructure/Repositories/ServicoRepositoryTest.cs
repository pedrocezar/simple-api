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
    [Trait("Repository", "Repository Servico")]
    public class ServicoRepositoryTest
    {
        private readonly Mock<ManutencaoContext> _mockContext;
        private readonly Fixture _fixture;

        public ServicoRepositoryTest()
        {
            _mockContext = new Mock<ManutencaoContext>(new DbContextOptionsBuilder<ManutencaoContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Servicos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Servico>>();

            _mockContext.Setup(mock => mock.Set<Servico>()).ReturnsDbSet(entities);

            var repository = new ServicoRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Servico Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Servico>();

            _mockContext.Setup(mock => mock.Set<Servico>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new ServicoRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Servico")]
        public async Task Post()
        {
            var entity = _fixture.Create<Servico>();

            _mockContext.Setup(mock => mock.Set<Servico>()).ReturnsDbSet(new List<Servico> { new Servico() });

            var repository = new ServicoRepository(_mockContext.Object);

            try
            {
                await repository.AddAsync(entity);
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

            _mockContext.Setup(mock => mock.Set<Servico>()).ReturnsDbSet(new List<Servico> { new Servico() });

            var repository = new ServicoRepository(_mockContext.Object);

            try
            {
                await repository.EditAsync(entity);
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

            _mockContext.Setup(mock => mock.Set<Servico>()).ReturnsDbSet(new List<Servico> { entity });

            var repository = new ServicoRepository(_mockContext.Object);

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
