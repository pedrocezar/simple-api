using Simple.DDD.Domain.Contracts.Requests;
using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Services;
using Simple.DDD.Domain.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Simple.DDD.API.Controllers
{
    [Authorize(Roles = ConstanteUtil.PerfilTecnicoNome)]
    public class EmpresasController : BaseController<Empresa, EmpresaRequest, EmpresaResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IMapper mapper, IEmpresaService empresaService) : base(mapper, empresaService)
        {
            _mapper = mapper;
            _empresaService = empresaService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        public override async Task<ActionResult> PostAsync([FromBody] EmpresaRequest request)
        {
            var entity = _mapper.Map<Empresa>(request);
            await _empresaService.AdicionarAsync(entity);
            return Created(nameof(PostAsync), new { id = entity.Id });
        }
    }
}
