using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _7DaysOfCode.Models;
using _7DaysOfCode.Services;

namespace _7DaysOfCode.BLL
{
    public class PokemonsManager
    {
        private readonly List<PokemonEntry> _listaPokemons;
        private readonly PokemonService _servicoPokemon;
        /// <summary>
        /// Inicializa o gerenciador de Pokémon, carregando a lista de Pokémon da API.
        /// </summary>
        public PokemonsManager()
        {
            _servicoPokemon = new PokemonService();
            _listaPokemons = PokemonAPIClient.GetPokemonsAsync().Result;
        }

        /// <summary>
        /// Exibe a lista dos primeiros 5 Pokémon disponíveis para escolha.
        /// </summary>
        public void Listar()
        {
            Console.Clear();
            Console.WriteLine("Vamos escolher o seu Pokémon!");
            Console.WriteLine("========================================================");
            Console.WriteLine("Aqui vão algumas opções!");
            Console.WriteLine("========================================================");
            for (int i = 0; i < 5 && i < _listaPokemons.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {_listaPokemons[i].Name}");
            }
            Console.WriteLine("========================================================");
        }

        /// <summary>
        /// Obtém as características de um Pokémon selecionado pelo usuário.
        /// </summary>
        public async Task GetCharacteristicsAsync()
        {
            try
            {
                Console.WriteLine("Digite o número do Pokémon para ver suas características:");
                string entrada = Console.ReadLine();
                if (!int.TryParse(entrada, out int indice) || indice < 1 || indice > _listaPokemons.Count)
                {
                    ExibirMensagemErro("Número inválido! Escolha um número válido da lista.");
                    Console.ReadLine();
                    return;
                }

                var pokemonSelecionado = _listaPokemons[indice - 1];
                var pokemon = await _servicoPokemon.GetPokemonAsync(pokemonSelecionado.Url);

                ExibirCaracteristicasPokemon(pokemon);
            }
            catch (Exception e)
            {
                ExibirMensagemErro($"Erro: {e.Message}");
            }

            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
        }

        /// <summary>
        /// Retorna uma entrada de Pokémon com base no índice fornecido.
        /// </summary>
        public PokemonEntry GetPokemonEntry(int indice)
        {
            if (indice >= 0 && indice < _listaPokemons.Count)
            {
                return _listaPokemons[indice];
            }
            return null;
        }

        /// <summary>
        /// Exibe as características de um Pokémon na tela.
        /// </summary>
        private static void ExibirCaracteristicasPokemon(Pokemon pokemon)
        {
            Console.WriteLine($"\nPokémon: {pokemon.Name}");
            var tipos = string.Join(", ", pokemon.Types?.ConvertAll(t => t.Type.Name) ?? new List<string>());
            Console.WriteLine($"Tipos: {tipos}");
            Console.WriteLine("Estatísticas Base:");
            foreach (var estatistica in pokemon.Stats)
            {
                Console.WriteLine($"  {estatistica.Stat.Name}: {estatistica.Base_Stat}");
            }
            var habilidades = string.Join(", ", pokemon.Abilities?.ConvertAll(a =>
            {
                string tipo = a.Is_Hidden ? "(oculta)" : "(normal)";
                return $"{a.Ability.Name} {tipo}";
            }) ?? new List<string>());
            Console.WriteLine($"Habilidades: {habilidades}");
            Console.WriteLine($"Altura: {pokemon.Height / 10.0:F1} metros");
            Console.WriteLine($"Peso: {pokemon.Weight / 10.0:F1} kg");
            Console.WriteLine("========================================================");
        }

        /// <summary>
        /// Exibe uma mensagem de erro na tela.
        /// </summary>
        private static void ExibirMensagemErro(string mensagem)
        {
            Console.WriteLine(mensagem);
        }
    }
}

