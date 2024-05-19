using Simple.DDD.Infrastructure.Contexts;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;

namespace Simple.DDD.Infrastructure.Repositories
{
    public class OrdemServicoRepository : BaseRepository<OrdemServico>, IOrdemServicoRepository
    {
        public OrdemServicoRepository(ManutencaoContext manutencaoContext) : base(manutencaoContext)
        {
        }
    }
}
