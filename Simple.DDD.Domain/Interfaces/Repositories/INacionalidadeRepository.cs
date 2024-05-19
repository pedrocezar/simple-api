using Simple.DDD.Domain.Entities;

namespace Simple.DDD.Domain.Interfaces.Repositories
{
    public interface INacionalidadeRepository
    {
        Task<Nacionalidade> GetNacionalidadeAsync(string nome);
    }
}
