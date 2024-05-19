using Simple.DDD.Domain.Enums;
using Simple.DDD.Domain.Exceptions;
using System.Net;
using System.Net.Http.Formatting;

namespace Simple.DDD.Infrastructure.Repositories
{
    public class BaseApiRepository
    {
        public readonly HttpClient _httpClient;

        public BaseApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);

            return await ReadAsync<T>(response);
        }

        public async Task<string> GetAsync(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> PostAsJsonAsync<T>(string uri, object objeto)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, objeto);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);

            return await ReadAsync<T>(response);
        }

        public async Task<T> PostAsContentAsync<T>(string uri, HttpContent httpContent)
        {
            var response = await _httpClient.PostAsync(uri, httpContent);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);

            return await ReadAsync<T>(response);
        }

        public async Task PostAsJsonNoContentAsync(string uri, object objeto)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, objeto);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);
        }

        public async Task PatchAsJsonNoContentAsync(string uri, object objeto)
        {
            var content = new ObjectContent<object>(objeto, new JsonMediaTypeFormatter());
            var response = await _httpClient.PatchAsync(uri, content);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);
        }

        public async Task DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);
        }


        private async Task TratarErroAsync(HttpResponseMessage response)
        {
            var body = await ReadAsync<string>(response);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new InformacaoException(StatusException.NaoAutorizado, $"Usuário sem acesso. {body}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new InformacaoException(StatusException.AcessoProibido, $"Usuário sem permissão. {body}");
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new InformacaoException(StatusException.FormatoIncorreto, $"Requisição inválida. {body}");
            else
                throw new InformacaoException(StatusException.Erro, $"Erro ao fazer a requisição. StatusCode: {response.StatusCode} {body}");
        }

        private async Task<T> ReadAsync<T>(HttpResponseMessage response)
        {
            return await response.Content.ReadAsAsync<T>();
        }
    }
}
