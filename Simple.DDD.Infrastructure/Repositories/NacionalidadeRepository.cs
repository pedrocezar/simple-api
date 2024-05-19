using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Interfaces.Repositories;

namespace Simple.DDD.Infrastructure.Repositories
{
    public class NacionalidadeRepository : BaseApiRepository, INacionalidadeRepository
    {
        public NacionalidadeRepository(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Nacionalidade> GetNacionalidadeAsync(string nome)
        {
            return await GetAsync<Nacionalidade>($"/?name={nome}");
        }
    }
}
