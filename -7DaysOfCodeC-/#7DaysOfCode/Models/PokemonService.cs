using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using _7DaysOfCode.Models;

namespace _7DaysOfCode.Models
{
    public class PokemonService
    {
        private readonly HttpClient _client;

        public PokemonService()
        {
            _client = new HttpClient();
        }

        public async Task<Pokemon> GetPokemonAsync(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<Pokemon>(json, options);
        }
    }
}
