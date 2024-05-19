using Simple.DDD.Domain.Entities;
using Simple.DDD.Domain.Exceptions;
using Simple.DDD.Domain.Interfaces.Repositories;
using Simple.DDD.Domain.Interfaces.Services;
using Simple.DDD.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Simple.DDD.Domain.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _repository;
        public readonly int? UserId;
        public readonly string UserPerfil;

        public BaseService(IBaseRepository<T> repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            UserId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.NameIdentifier).ToInt();
            UserPerfil = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
        }

        public async Task<List<T>> ObterTodosAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.ListAsync(expression);
        }

        public async Task<T> ObterAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.FindAsync(expression);
            if (entity == null)
                throw new InformacaoException(Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado");

            return entity;
        }

        public async Task<List<T>> ObterTodosAsync()
        {
            return await _repository.ListAsync(x => x.Ativo);
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            var entity = await _repository.FindAsync(x => x.Id == id && x.Ativo);
            if (entity == null)
                throw new InformacaoException(Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");

            return entity;
        }

        public async Task AdicionarAsync(T entity)
        {
            entity.DataInclusao = DateTime.Now;
            entity.UsuarioInclusao = UserId;
            await _repository.AddAsync(entity);
        }

        public async Task DeletarAsync(int id)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
                throw new InformacaoException(Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");

            entity.UsuarioAlteracao = UserId;
            entity.DataAlteracao = DateTime.Now;
            entity.Ativo = false;
            await _repository.EditAsync(entity);
        }

        public async Task AlterarAsync(T entity)
        {
            var find = await _repository.FindAsNoTrackingAsync(x => x.Id == entity.Id && x.Ativo);
            if (find == null)
                throw new InformacaoException(Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {entity.Id}");

            entity.DataInclusao = find.DataInclusao;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracao = UserId;
            await _repository.EditAsync(entity);
        }
    }
}
