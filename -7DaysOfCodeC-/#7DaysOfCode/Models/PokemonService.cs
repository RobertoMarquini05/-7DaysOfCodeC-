using System;
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
            try
            {
                var response = await _client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro desconhecido na API.";
                    try
                    {
                        using var errorDoc = JsonDocument.Parse(errorContent);
                        errorMessage = errorDoc.RootElement.GetProperty("message").GetString() ?? errorContent;
                    }
                    catch
                    {
                        // Se não for possível desserializar, usa mensagem genérica
                    }
                    throw new HttpRequestException($"Erro na API: {response.StatusCode} - {errorMessage}");
                }

                var json = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(json))
                {
                    throw new Exception("API indisponível no momento: resposta vazia.");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<Pokemon>(json, options);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro de rede ao obter detalhes do Pokémon: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter detalhes do Pokémon: {ex.Message}");
                return null;
            }
        }
    }
}