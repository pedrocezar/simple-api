using Simple.DDD.Infrastructure.Contexts;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;

namespace Simple.DDD.Infrastructure.Repositories
{
    public class RelacaoRepository : BaseRepository<Relacao>, IRelacaoRepository
    {
        public RelacaoRepository(ManutencaoContext manutencaoContext) : base(manutencaoContext)
        {
        }
    }
}
