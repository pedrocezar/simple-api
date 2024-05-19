using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Simple.DDD.Domain.Services
{
    public class OrdemServicoService : BaseService<OrdemServico>, IOrdemServicoService
    {
        public OrdemServicoService(IOrdemServicoRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}
