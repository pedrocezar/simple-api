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
    [Trait("Repository", "Repository Perfil")]
    public class PerfilRepositoryTest
    {
        private readonly Mock<ManutencaoContext> _mockContext;
        private readonly Fixture _fixture;

        public PerfilRepositoryTest()
        {
            _mockContext = new Mock<ManutencaoContext>(new DbContextOptionsBuilder<ManutencaoContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Perfis")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Perfil>>();

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(entities);

            var repository = new PerfilRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Perfi Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Perfil>();

            _mockContext.Setup(mock => mock.Set<Perfil>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new PerfilRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Perfil")]
        public async Task Post()
        {
            var entity = _fixture.Create<Perfil>();

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(new List<Perfil> { new Perfil() });

            var repository = new PerfilRepository(_mockContext.Object);

            try
            {
                await repository.AddAsync(entity);
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

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(new List<Perfil> { new Perfil() });

            var repository = new PerfilRepository(_mockContext.Object);

            try
            {
                await repository.EditAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Remove Perfil Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Perfil>();

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(new List<Perfil> { entity });

            var repository = new PerfilRepository(_mockContext.Object);

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
