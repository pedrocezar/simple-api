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
    public class EmpresaRepositoryTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<ManutencaoContext> _mockManutencaoContext;

        public EmpresaRepositoryTest()
        {
            _fixture = FixtureConfig.Get();
            _mockManutencaoContext = new Mock<ManutencaoContext>(new DbContextOptionsBuilder<ManutencaoContext>().UseLazyLoadingProxies().Options);
        }

        [Fact(DisplayName = "Listar todas as empresas")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Empresa>>();

            _mockManutencaoContext.Setup(mock => mock.Set<Empresa>()).ReturnsDbSet(entities);

            var repository = new EmpresaRepository(_mockManutencaoContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Listar empresa por Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Empresa>();

            _mockManutencaoContext.Setup(mock => mock.Set<Empresa>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new EmpresaRepository(_mockManutencaoContext.Object);

            var id = entity.Id;
            var response = await repository.FindAsync(id);

            Assert.Equal(response.Id, id);
        }

        [Fact(DisplayName = "Cadastrar uma nova empresa")]
        public async Task Post()
        {
            var entity = _fixture.Create<Empresa>();

            _mockManutencaoContext.Setup(mock => mock.Set<Empresa>()).ReturnsDbSet(new List<Empresa>());

            var repository = new EmpresaRepository(_mockManutencaoContext.Object);

            var exception = await Record.ExceptionAsync(() => repository.AddAsync(entity));
            Assert.Null(exception);
        }

        [Fact(DisplayName = "Alterar uma empresa existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Empresa>();

            _mockManutencaoContext.Setup(mock => mock.Set<Empresa>()).ReturnsDbSet(new List<Empresa>());

            var repository = new EmpresaRepository(_mockManutencaoContext.Object);

            var exception = await Record.ExceptionAsync(() => repository.EditAsync(entity));
            Assert.Null(exception);
        }

        [Fact(DisplayName = "Excluir uma empresa existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Empresa>();

            _mockManutencaoContext.Setup(mock => mock.Set<Empresa>()).ReturnsDbSet(new List<Empresa>());

            var repository = new EmpresaRepository(_mockManutencaoContext.Object);

            var exception = await Record.ExceptionAsync(() => repository.RemoveAsync(entity));
            Assert.Null(exception);
        }
    }
}
