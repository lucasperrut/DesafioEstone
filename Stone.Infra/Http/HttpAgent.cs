using Newtonsoft.Json;
using Stone.Common.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stone.Infra.Http
{
    public class HttpAgent : IHttpAgent
    {
        private HttpClient _client = new HttpClient();

        public async Task<T> GetAsync<T>(string url)
        {
            var result = await GetAsStringAsync(url);
            return JsonConvert.DeserializeObject<T>(result);
        }

        private async Task<string> GetAsStringAsync(string url)
        {
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
