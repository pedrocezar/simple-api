using Simple.DDD.Domain.Contracts.Requests;
using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using static Simple.DDD.Domain.Utils.ConstanteUtil;

namespace Simple.DDD.API.Controllers
{
    [Authorize(Roles = PerfilTecnicoNome)]
    public class FinalizacoesController : BaseController<Finalizacao, FinalizacaoRequest, FinalizacaoResponse>
    {
        public FinalizacoesController(IMapper mapper, IFinalizacaoService service) : base(mapper, service)
        {
        }
    }
}
