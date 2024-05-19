using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Entities;

namespace Simple.DDD.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Task CriarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task<Nacionalidade> ObterNacionalidadeAsync(string nome);
        Task<AutenticaoResponse> AutenticarAsync(string email, string senha);
        Task AtualizarTelefoneAsync(int id, string telefone);
        Task<List<Usuario>> ObterTodosUsuarioAsync();
        Task<Usuario> ObterPorIdUsuarioAsync(int id);
    }
}
