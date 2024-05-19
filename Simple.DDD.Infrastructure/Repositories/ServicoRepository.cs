using Simple.DDD.Infrastructure.Contexts;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;

namespace Simple.DDD.Infrastructure.Repositories
{
    public class ServicoRepository : BaseRepository<Servico>, IServicoRepository
    {
        public ServicoRepository(ManutencaoContext manutencaoContext) : base(manutencaoContext)
        {
        }
    }
}
