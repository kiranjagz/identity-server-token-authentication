using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Http.Console.App
{
    public interface IJsonPlaceHolder
    {
        Task<string> Get();
    }

    public class JsonPlaceHolder : IJsonPlaceHolder
    {
        private HttpClient _httpClient;

        public JsonPlaceHolder(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Get()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("todos/3");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return jsonResponse;
        }
    }
}
