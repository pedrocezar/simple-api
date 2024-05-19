using Simple.DDD.API.Controllers;
using Simple.DDD.Domain.Contracts.Requests;
using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Services;
using Simple.DDD.Tests.Configs;
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Simple.DDD.Tests.Sources.API.Controllers
{
    [Trait("Controller", "Controller Usuarios")]
    public class UsuariosControllerTest
    {
        private readonly Mock<IUsuarioService> _mockUsuariolService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public UsuariosControllerTest()
        {
            _mockUsuariolService = new Mock<IUsuarioService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Usuarios")]
        public async Task GetAsync()
        {
            var entities = _fixture.Create<List<Usuario>>();

            _mockUsuariolService.Setup(mock => mock.ObterTodosUsuarioAsync()).ReturnsAsync(entities);

            var controller = new UsuariosController(_mapper, _mockUsuariolService.Object);

            var actionResult = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<UsuarioResponse>>(objectResult.Value);
            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Usuario Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Usuario>();

            _mockUsuariolService.Setup(mock => mock.ObterPorIdUsuarioAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new UsuariosController(_mapper, _mockUsuariolService.Object);

            var actionResult = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<UsuarioResponse>(objectResult.Value);
            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Busca Nacionalidade Nome")]
        public async Task GetNacionalidade()
        {
            var entity = _fixture.Create<Nacionalidade>();

            _mockUsuariolService.Setup(mock => mock.ObterNacionalidadeAsync(It.IsAny<string>())).ReturnsAsync(entity);

            var controller = new UsuariosController(_mapper, _mockUsuariolService.Object);

            var actionResult = await controller.GetNacionalidadeAsync(entity.Name);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<NacionalidadeResponse>(objectResult.Value);
            Assert.NotNull(response.Sigla);
        }

        [Fact(DisplayName = "Cadastra Usuario")]
        public async Task Post()
        {
            var request = _fixture.Create<UsuarioRequest>();

            _mockUsuariolService.Setup(mock => mock.AdicionarAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var controller = new UsuariosController(_mapper, _mockUsuariolService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Edita Usuario Existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = _fixture.Create<UsuarioRequest>();

            _mockUsuariolService.Setup(mock => mock.AlterarAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var controller = new UsuariosController(_mapper, _mockUsuariolService.Object);

            var actionResult = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove Usuario Existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockUsuariolService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new UsuariosController(_mapper, _mockUsuariolService.Object);

            var actionResult = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
