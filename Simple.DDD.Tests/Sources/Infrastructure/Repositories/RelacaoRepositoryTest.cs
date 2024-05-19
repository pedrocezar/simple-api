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
    [Trait("Repository", "Repository Relacao")]
    public class RelacaoRepositoryTest
    {
        private readonly Mock<ManutencaoContext> _mockContext;
        private readonly Fixture _fixture;

        public RelacaoRepositoryTest()
        {
            _mockContext = new Mock<ManutencaoContext>(new DbContextOptionsBuilder<ManutencaoContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Relacoes")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Relacao>>();

            _mockContext.Setup(mock => mock.Set<Relacao>()).ReturnsDbSet(entities);

            var repository = new RelacaoRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Relacao Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Relacao>();

            _mockContext.Setup(mock => mock.Set<Relacao>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new RelacaoRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Relacao")]
        public async Task Post()
        {
            var entity = _fixture.Create<Relacao>();

            _mockContext.Setup(mock => mock.Set<Relacao>()).ReturnsDbSet(new List<Relacao> { new Relacao() });

            var repository = new RelacaoRepository(_mockContext.Object);

            try
            {
                await repository.AddAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Edita Relacao Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Relacao>();

            _mockContext.Setup(mock => mock.Set<Relacao>()).ReturnsDbSet(new List<Relacao> { new Relacao() });

            var repository = new RelacaoRepository(_mockContext.Object);

            try
            {
                await repository.EditAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Remove Relacao Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Relacao>();

            _mockContext.Setup(mock => mock.Set<Relacao>()).ReturnsDbSet(new List<Relacao> { entity });

            var repository = new RelacaoRepository(_mockContext.Object);

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
