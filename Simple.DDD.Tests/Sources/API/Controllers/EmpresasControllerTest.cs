using Simple.DDD.API.Controllers;
using Simple.DDD.Domain.Contracts.Requests;
using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Services;
using Simple.DDD.Tests.Configs;
using AutoFixture;
using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Simple.DDD.Tests.Sources.API.Controllers
{
    [Trait("Controller", "Controller de Empresas")]
    public class EmpresasControllerTest
    {
        private readonly Mock<IEmpresaService> _mockEmpresaService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        public EmpresasControllerTest()
        {
            _mockEmpresaService = new Mock<IEmpresaService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Busca um empresa por Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Empresa>();

            _mockEmpresaService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new EmpresasController(_mapper, _mockEmpresaService.Object);

            var response = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var empresaResponse = Assert.IsType<EmpresaResponse>(objectResult.Value);
            Assert.Equal(empresaResponse.Id, entity.Id);
        }

        [Fact(DisplayName = "Busca todas empresas")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Empresa>>();

            _mockEmpresaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new EmpresasController(_mapper, _mockEmpresaService.Object);

            var response = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var empresasResponse = Assert.IsType<List<EmpresaResponse>>(objectResult.Value);
            Assert.True(empresasResponse.Count() > 0);
        }

        [Fact(DisplayName = "Cadastra uma nova empresa")]
        public async Task Post()
        {
            var request = _fixture.Create<EmpresaRequest>();

            _mockEmpresaService.Setup(mock => mock.AdicionarAsync(It.IsAny<Empresa>())).Returns(Task.CompletedTask);

            var controller = new EmpresasController(_mapper, _mockEmpresaService.Object);

            var response = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(response);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Atualiza uma empresa existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = _fixture.Create<EmpresaRequest>();

            _mockEmpresaService.Setup(mock => mock.AlterarAsync(It.IsAny<Empresa>())).Returns(Task.CompletedTask);

            var controller = new EmpresasController(_mapper, _mockEmpresaService.Object);

            var response = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove uma empresa existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockEmpresaService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new EmpresasController(_mapper, _mockEmpresaService.Object);

            var response = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
