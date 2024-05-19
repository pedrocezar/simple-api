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
    [Trait("Controller", "Controller Ordens")]
    public class OrdensControllerTest
    {
        private readonly Mock<IOrdemServicoService> _mockOrdemServicoService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public OrdensControllerTest()
        {
            _mockOrdemServicoService = new Mock<IOrdemServicoService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista OrdemServicos")]
        public async Task GetAsync()
        {
            var entities = _fixture.Create<List<OrdemServico>>();

            _mockOrdemServicoService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new OrdensController(_mapper, _mockOrdemServicoService.Object);

            var actionResult = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<OrdemServicoResponse>>(objectResult.Value);
            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca OrdemServico Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<OrdemServico>();

            _mockOrdemServicoService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new OrdensController(_mapper, _mockOrdemServicoService.Object);

            var actionResult = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<OrdemServicoResponse>(objectResult.Value);
            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra OrdemServico")]
        public async Task Post()
        {
            var request = _fixture.Create<OrdemServicoRequest>();

            _mockOrdemServicoService.Setup(mock => mock.AdicionarAsync(It.IsAny<OrdemServico>())).Returns(Task.CompletedTask);

            var controller = new OrdensController(_mapper, _mockOrdemServicoService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Edita OrdemServico Existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = _fixture.Create<OrdemServicoRequest>();

            _mockOrdemServicoService.Setup(mock => mock.AlterarAsync(It.IsAny<OrdemServico>())).Returns(Task.CompletedTask);

            var controller = new OrdensController(_mapper, _mockOrdemServicoService.Object);

            var actionResult = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove OrdemServico Existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockOrdemServicoService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new OrdensController(_mapper, _mockOrdemServicoService.Object);

            var actionResult = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
