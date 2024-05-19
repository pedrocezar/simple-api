using Simple.DDD.Domain.Contracts.Responses;
using Simple.DDD.Domain.Enums;
using Simple.DDD.Domain.Exceptions;
using Simple.DDD.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Simple.DDD.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var response = new InformacaoResponse();

            if (context.Exception is InformacaoException)
            {
                var informacaoException = (InformacaoException)context.Exception;

                response.Codigo = informacaoException.Codigo;
                response.Mensagens = informacaoException.Mensagens;
                response.Detalhe = $"{context.Exception.Message} | {context.Exception.InnerException?.Message}";
            }
            else
            {
                response.Codigo = StatusException.Erro;
                response.Mensagens = new List<string> { "Erro inesperdado" };
                response.Detalhe = $"{context.Exception?.Message} | {context.Exception?.InnerException?.Message}";
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Codigo.GetStatusCode()
            };

            OnException(context);
            return Task.CompletedTask;
        }
    }
}
