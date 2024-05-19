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
    [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
    public class UsuariosController : BaseController<Usuario, UsuarioRequest, UsuarioResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IMapper mapper, IUsuarioService service) : base(mapper, service)
        {
            _mapper = mapper;
            _usuarioService = service;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        public override async Task<ActionResult> PostAsync([FromBody] UsuarioRequest request)
        {
            var entity = _mapper.Map<Usuario>(request);
            await _usuarioService.CriarUsuarioAsync(entity);
            return Created(nameof(PostAsync), new { id = entity.Id });
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> PatchAsync([FromRoute] int id, [FromBody] UsuarioTelefoneRequest request)
        {
            await _usuarioService.AtualizarTelefoneAsync(id, request.Telefone);
            return Ok();
        }

        [HttpGet("nome")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UsuarioResponse>>> GetAsync([FromQuery] string nome)
        {
            var entities = await _usuarioService.ObterTodosAsync(x => x.Nome.Contains(nome));
            var response = _mapper.Map<List<UsuarioResponse>>(entities);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public override async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] UsuarioRequest request)
        {
            var entity = _mapper.Map<Usuario>(request);
            entity.Id = id;
            await _usuarioService.AtualizarUsuarioAsync(entity);
            return NoContent();
        }

        [HttpGet("nacionalidade")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<NacionalidadeResponse>> GetNacionalidadeAsync([FromQuery] string nome)
        {
            var entity = await _usuarioService.ObterNacionalidadeAsync(nome);
            var response = _mapper.Map<NacionalidadeResponse>(entity);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public override async Task<ActionResult<UsuarioResponse>> GetByIdAsync([FromRoute] int id)
        {
            var entity = await _usuarioService.ObterPorIdUsuarioAsync(id);
            return Ok(_mapper.Map<UsuarioResponse>(entity));
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public override async Task<ActionResult<List<UsuarioResponse>>> GetAsync()
        {
            var entity = await _usuarioService.ObterTodosUsuarioAsync();
            return Ok(_mapper.Map<List<UsuarioResponse>>(entity));
        }
    }
}
