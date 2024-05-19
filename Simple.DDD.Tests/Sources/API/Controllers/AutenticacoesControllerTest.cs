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
    public class AutenticacoesControllerTest
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public AutenticacoesControllerTest()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Cria Token")]
        public async Task Post()
        {
            var request = _fixture.Create<AutenticaoRequest>();
            var response = _fixture.Create<AutenticaoResponse>();

            _mockUsuarioService.Setup(mock => mock.AutenticarAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(response);

            var controller = new AutenticacoesController(_mockUsuarioService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var responseResult = Assert.IsType<AutenticaoResponse>(objectResult.Value);
            Assert.Equal(responseResult.Token, response.Token);
        }
    }
}
