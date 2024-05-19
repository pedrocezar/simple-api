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
    [Trait("Controller", "Controller Servicos")]
    public class ServicosControllerTest
    {
        private readonly Mock<IServicoService> _mockServicoService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public ServicosControllerTest()
        {
            _mockServicoService = new Mock<IServicoService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Servicos")]
        public async Task GetAsync()
        {
            var entities = _fixture.Create<List<Servico>>();

            _mockServicoService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new ServicosController(_mapper, _mockServicoService.Object);

            var actionResult = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<ServicoResponse>>(objectResult.Value);
            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Servico Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Servico>();

            _mockServicoService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new ServicosController(_mapper, _mockServicoService.Object);

            var actionResult = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<ServicoResponse>(objectResult.Value);
            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Servico")]
        public async Task Post()
        {
            var request = _fixture.Create<ServicoRequest>();

            _mockServicoService.Setup(mock => mock.AdicionarAsync(It.IsAny<Servico>())).Returns(Task.CompletedTask);

            var controller = new ServicosController(_mapper, _mockServicoService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Edita Servico Existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = _fixture.Create<ServicoRequest>();

            _mockServicoService.Setup(mock => mock.AlterarAsync(It.IsAny<Servico>())).Returns(Task.CompletedTask);

            var controller = new ServicosController(_mapper, _mockServicoService.Object);

            var actionResult = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove Servico Existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockServicoService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new ServicosController(_mapper, _mockServicoService.Object);

            var actionResult = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
