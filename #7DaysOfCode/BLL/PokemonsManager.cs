using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _7DaysOfCode.Models;
using _7DaysOfCode.Services;

namespace _7DaysOfCode.BLL
{
    public class PokemonsManager
    {
        private List<PokemonEntry> _listaPokemons;
        private readonly PokemonService _service;

        public PokemonsManager()
        {
            _service = new PokemonService();
            // Carregar lista de pokemons da API, pegar o resultado async corretamente:
            _listaPokemons = PokemonAPIClient.GetPokemonsAsync().Result;
        }

        public void Listar()
        {
            Console.Clear();
            Console.WriteLine("Vamos escolher o seu Pokemon !");
            Console.WriteLine("========================================================");
            Console.WriteLine("Aqui vão algumas opções !");
            Console.WriteLine("========================================================");
            for (int i = 0; i < _listaPokemons.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_listaPokemons[i].Name}");
            }
            Console.WriteLine("========================================================");
        }

        public async Task GetCharacteristicsAsync()
        {
            try
            {
                Console.WriteLine("Digite o número do Pokémon para ver suas características:");
                var input = Console.ReadLine();
                if (!int.TryParse(input, out int index) || index < 1 || index > _listaPokemons.Count)
                {
                    Console.WriteLine("Número inválido! Escolha um número válido da lista.");
                    Console.ReadLine();
                    return;
                }

                var selectedPokemon = _listaPokemons[index - 1];

                var pokemon = await _service.GetPokemonAsync(selectedPokemon.Url);

                Console.WriteLine($"\nPokémon: {pokemon.Name}");

                var tipos = string.Join(", ", pokemon.Types?.ConvertAll(t => t.Type.Name) ?? new List<string>());
                Console.WriteLine($"Tipos: {tipos}");

                Console.WriteLine("Estatísticas Base:");
                if (pokemon.Stats != null)
                {
                    foreach (var stat in pokemon.Stats)
                    {
                        Console.WriteLine($"  {stat.Stat.Name}: {stat.Base_Stat}");
                    }
                }

                var habilidades = string.Join(", ", pokemon.Abilities?.ConvertAll(a =>
                {
                    var tipo = a.Is_Hidden ? "(oculta)" : "(normal)";
                    return $"{a.Ability.Name} {tipo}";
                }) ?? new List<string>());
                Console.WriteLine($"Habilidades: {habilidades}");
                Console.WriteLine("========================================================");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }

            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }
    }
}
