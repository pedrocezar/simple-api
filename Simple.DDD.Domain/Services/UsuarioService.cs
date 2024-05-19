using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Exceptions;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Interfaces.Services;
using Simple.DDD.Domain.Settings;
using Simple.DDD.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Simple.DDD.Domain.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly INacionalidadeRepository _nacionalidadeRepository;
        private readonly AppSetting _appSettings;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository,
            INacionalidadeRepository nacionalidadeRepository,
            AppSetting appSettings,
            IHttpContextAccessor httpContextAccessor) : base(usuarioRepository, httpContextAccessor)
        {
            _usuarioRepository = usuarioRepository;
            _nacionalidadeRepository = nacionalidadeRepository;
            _appSettings = appSettings;
        }

        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, BCrypt.Net.BCrypt.GenerateSalt());
            await AdicionarAsync(usuario);
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, BCrypt.Net.BCrypt.GenerateSalt());
            await AlterarAsync(usuario);
        }

        public async Task AtualizarTelefoneAsync(int id, string telefone)
        {
            var entity = await ObterPorIdAsync(id);
            entity.Telefone = telefone;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracao = UserId;
            await _usuarioRepository.EditAsync(entity);
        }

        public async Task<Nacionalidade> ObterNacionalidadeAsync(string nome)
        {
            return await _nacionalidadeRepository.GetNacionalidadeAsync(nome);
        }

        public async Task<List<Usuario>> ObterTodosUsuarioAsync()
        {
            if (UserPerfil == ConstanteUtil.PerfilClienteNome)
                return await ObterTodosAsync(x => x.Ativo && x.Id == UserId);
            else
                return await ObterTodosAsync();
        }

        public async Task<Usuario> ObterPorIdUsuarioAsync(int id)
        {
            if (UserPerfil == ConstanteUtil.PerfilClienteNome)
                return await ObterAsync(x => x.Id == id && x.Ativo && x.Id == UserId);
            else
                return await ObterPorIdAsync(id);
        }

        public async Task<AutenticaoResponse> AutenticarAsync(string email, string senha)
        {
            var entity = await ObterAsync(x => x.Email.Equals(email) && x.Ativo);

            if (!BCrypt.Net.BCrypt.Verify(senha, entity.Senha))
                throw new InformacaoException(Enums.StatusException.FormatoIncorreto, "Usuário ou senha incorreta");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                    new Claim(ClaimTypes.Name, entity.Nome),
                    new Claim(ClaimTypes.Email, entity.Email),
                    new Claim(ClaimTypes.Role, entity.Perfil.Nome)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSecurityKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AutenticaoResponse
            {
                Token = tokenString,
                DataExpiracao = tokenDescriptor.Expires
            };
        }
    }
}
