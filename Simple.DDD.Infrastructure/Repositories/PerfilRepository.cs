using Simple.DDD.Infrastructure.Contexts;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;

namespace Simple.DDD.Infrastructure.Repositories
{
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(ManutencaoContext manutencaoContext) : base(manutencaoContext)
        {
        }
    }
}
