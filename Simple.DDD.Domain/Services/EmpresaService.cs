using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Simple.DDD.Domain.Services
{
    public class EmpresaService : BaseService<Empresa>, IEmpresaService
    {
        public EmpresaService(IEmpresaRepository empresaRepository, IHttpContextAccessor httpContextAccessor) : base(empresaRepository, httpContextAccessor)
        {
        }
    }
}
