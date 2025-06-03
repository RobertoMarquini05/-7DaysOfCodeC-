using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using _7DaysOfCode.Models;

namespace _7DaysOfCode.Models
{
    public static class PokemonAPIClient
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<List<PokemonEntry>> GetPokemonsAsync()
        {
            try
            {
                string url = "https://pokeapi.co/api/v2/pokemon/?limit=20";
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

                var responseBody = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseBody))
                {
                    throw new Exception("API indisponível no momento: resposta vazia.");
                }

                using var jsonDocument = JsonDocument.Parse(responseBody);
                var resultsElement = jsonDocument.RootElement.GetProperty("results");

                var pokemonList = JsonSerializer.Deserialize<List<PokemonEntry>>(resultsElement.GetRawText());
                return pokemonList ?? new List<PokemonEntry>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro de rede ao obter lista de pokemons: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter lista de pokemons: {ex.Message}");
                return null;
            }
        }
    }
}