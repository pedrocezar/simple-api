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
    [Trait("Repository", "Repository Finalizacao")]
    public class FinalizacaoRepositoryTest
    {
        private readonly Mock<ManutencaoContext> _mockContext;
        private readonly Fixture _fixture;

        public FinalizacaoRepositoryTest()
        {
            _mockContext = new Mock<ManutencaoContext>(new DbContextOptionsBuilder<ManutencaoContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Finalizacoes")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Finalizacao>>();

            _mockContext.Setup(mock => mock.Set<Finalizacao>()).ReturnsDbSet(entities);

            var repository = new FinalizacaoRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Finalizacao Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Finalizacao>();

            _mockContext.Setup(mock => mock.Set<Finalizacao>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new FinalizacaoRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Finalizacao")]
        public async Task Post()
        {
            var entity = _fixture.Create<Finalizacao>();

            _mockContext.Setup(mock => mock.Set<Finalizacao>()).ReturnsDbSet(new List<Finalizacao> { new Finalizacao() });

            var repository = new FinalizacaoRepository(_mockContext.Object);

            try
            {
                await repository.AddAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Edita Finalizacao Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Finalizacao>();

            _mockContext.Setup(mock => mock.Set<Finalizacao>()).ReturnsDbSet(new List<Finalizacao> { new Finalizacao() });

            var repository = new FinalizacaoRepository(_mockContext.Object);

            try
            {
                await repository.EditAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Remove Finalizacao Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Finalizacao>();

            _mockContext.Setup(mock => mock.Set<Finalizacao>()).ReturnsDbSet(new List<Finalizacao> { entity });

            var repository = new FinalizacaoRepository(_mockContext.Object);

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
