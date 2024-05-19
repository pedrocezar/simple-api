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
    public class OrdensController : BaseController<OrdemServico, OrdemServicoRequest, OrdemServicoResponse>
    {
        public OrdensController(IMapper mapper, IOrdemServicoService service) : base(mapper, service)
        {
        }
    }
}
