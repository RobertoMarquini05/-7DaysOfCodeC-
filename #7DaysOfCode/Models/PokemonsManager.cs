using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _7DaysOfCode.Models;

namespace _7DaysOfCode.Models
{
    public class PokemonsManager
    {
        private readonly List<PokemonEntry> _listaPokemons;
        private readonly PokemonService _servicoPokemon;

        public PokemonsManager()
        {
            _servicoPokemon = new PokemonService();
            _listaPokemons = PokemonAPIClient.GetPokemonsAsync().Result;
        }

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

        public PokemonEntry GetPokemonEntry(int indice)
        {
            if (indice >= 0 && indice < _listaPokemons.Count)
            {
                return _listaPokemons[indice];
            }
            return null;
        }
    }
}