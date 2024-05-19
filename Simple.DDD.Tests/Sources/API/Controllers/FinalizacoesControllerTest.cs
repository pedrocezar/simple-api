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
    [Trait("Controller", "Controller Finalizacoes")]
    public class FinalizacaosControllerTest
    {
        private readonly Mock<IFinalizacaoService> _mockFinalizacaoService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public FinalizacaosControllerTest()
        {
            _mockFinalizacaoService = new Mock<IFinalizacaoService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Finalizacoes")]
        public async Task GetAsync()
        {
            var entities = _fixture.Create<List<Finalizacao>>();

            _mockFinalizacaoService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new FinalizacoesController(_mapper, _mockFinalizacaoService.Object);

            var actionResult = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<FinalizacaoResponse>>(objectResult.Value);
            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Finalizacao Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Finalizacao>();

            _mockFinalizacaoService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new FinalizacoesController(_mapper, _mockFinalizacaoService.Object);

            var actionResult = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<FinalizacaoResponse>(objectResult.Value);
            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Finalizacao")]
        public async Task Post()
        {
            var request = _fixture.Create<FinalizacaoRequest>();

            _mockFinalizacaoService.Setup(mock => mock.AdicionarAsync(It.IsAny<Finalizacao>())).Returns(Task.CompletedTask);

            var controller = new FinalizacoesController(_mapper, _mockFinalizacaoService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Edita Finalizacao Existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = _fixture.Create<FinalizacaoRequest>();

            _mockFinalizacaoService.Setup(mock => mock.AlterarAsync(It.IsAny<Finalizacao>())).Returns(Task.CompletedTask);

            var controller = new FinalizacoesController(_mapper, _mockFinalizacaoService.Object);

            var actionResult = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove Finalizacao Existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockFinalizacaoService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new FinalizacoesController(_mapper, _mockFinalizacaoService.Object);

            var actionResult = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
