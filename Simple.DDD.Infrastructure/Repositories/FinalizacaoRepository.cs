using Simple.DDD.Infrastructure.Contexts;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;

namespace Simple.DDD.Infrastructure.Repositories
{
    public class FinalizacaoRepository : BaseRepository<Finalizacao>, IFinalizacaoRepository
    {
        public FinalizacaoRepository(ManutencaoContext manutencaoContext) : base(manutencaoContext)
        {
        }
    }
}
