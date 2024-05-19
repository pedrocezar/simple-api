using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Simple.DDD.Domain.Services
{
    public class PerfilService : BaseService<Perfil>, IPerfilService
    {
        public PerfilService(IPerfilRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}
