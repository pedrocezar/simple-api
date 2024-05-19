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
    public class PerfisController : BaseController<Perfil, PerfilRequest, PerfilResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPerfilService _perfilService;

        public PerfisController(IMapper mapper, IPerfilService perfilService) : base(mapper, perfilService)
        {
            _mapper = mapper;
            _perfilService = perfilService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        public override async Task<ActionResult> PostAsync([FromBody] PerfilRequest request)
        {
            var entity = _mapper.Map<Perfil>(request);
            await _perfilService.AdicionarAsync(entity);
            return Created(nameof(PostAsync), new { id = entity.Id });
        }
    }
}
