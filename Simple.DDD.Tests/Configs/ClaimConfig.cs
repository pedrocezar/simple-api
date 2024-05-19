using Simple.DDD.Domain.Entities;
using System.Security.Claims;

namespace Simple.DDD.Tests.Configs
{
    public static class ClaimConfig
    {
        public static IEnumerable<Claim> Get(int id, string nome, string email, string perfil)
        {
            return new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, perfil)
                };
        }

        public static Claim[] Claims(this Usuario usuario)
        {
            return new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Perfil.Nome)
                };
        }
    }
}
