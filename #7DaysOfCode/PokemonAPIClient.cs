using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using _7DaysOfCode.Models;

namespace _7DaysOfCode
{
    public static class PokemonAPIClient
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<List<PokemonEntry>> GetPokemonsAsync()
        {
            try
            {
                // URL da API para lista de pokemons com limite padrão de 20 (pode passar ?limit=100 para pegar 100)
                string url = "https://pokeapi.co/api/v2/pokemon/";

                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                using var jsonDocument = JsonDocument.Parse(responseBody);
                var resultsElement = jsonDocument.RootElement.GetProperty("results");

                var pokemonList = JsonSerializer.Deserialize<List<PokemonEntry>>(resultsElement.GetRawText());

                return pokemonList ?? new List<PokemonEntry>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao obter lista de pokemons: {e.Message}");
                return new List<PokemonEntry>();
            }
        }
    }
}
