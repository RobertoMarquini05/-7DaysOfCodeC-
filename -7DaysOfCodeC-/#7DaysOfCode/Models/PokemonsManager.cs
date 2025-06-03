using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _7DaysOfCode.Models;
using _7DaysOfCode.View;

namespace _7DaysOfCode.Models
{
    public class PokemonsManager
    {
        private readonly List<PokemonEntry> _listaPokemons;
        private readonly PokemonService _servicoPokemon;
        private readonly PokemonView _pokemonView;

        public PokemonsManager()
        {
            try
            {
                _servicoPokemon = new PokemonService();
                _pokemonView = new PokemonView();
                _listaPokemons = PokemonAPIClient.GetPokemonsAsync().Result;
            }
            catch (Exception ex)
            {
                _pokemonView.ExibirMensagemErro($"Erro ao inicializar PokemonsManager: {ex.Message}");
                _listaPokemons = new List<PokemonEntry>();
            }
        }

        public List<PokemonEntry> Listar()
        {
            try
            {
                Console.Clear();
                if (_listaPokemons == null || _listaPokemons.Count == 0)
                {
                    _pokemonView.ExibirMensagemErro("Nenhum Pokémon disponível. A API pode estar fora do ar.");
                    return _listaPokemons;
                }

                Console.WriteLine("Vamos escolher o seu Pokémon!");
                Console.WriteLine("========================================================");
                Console.WriteLine("Aqui vão algumas opções!");
                Console.WriteLine("========================================================");
                for (int i = 0; i < 5 && i < _listaPokemons.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {_listaPokemons[i].Name}");
                }
                Console.WriteLine("========================================================");
                return _listaPokemons;
            }
            catch (Exception ex)
            {
                _pokemonView.ExibirMensagemErro($"Erro ao listar Pokémon: {ex.Message}");
                return _listaPokemons;
            }
        }

        public PokemonEntry GetPokemonEntry(int indice)
        {
            try
            {
                if (indice >= 0 && indice < _listaPokemons.Count)
                {
                    return _listaPokemons[indice];
                }
                return null;
            }
            catch (Exception ex)
            {
                _pokemonView.ExibirMensagemErro($"Erro ao obter Pokémon: {ex.Message}");
                return null;
            }
        }
    }
}