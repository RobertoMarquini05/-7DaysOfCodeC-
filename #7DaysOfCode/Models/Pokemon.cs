using System.Collections.Generic;

namespace _7DaysOfCode.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public List<PokemonTypeWrapper> Types { get; set; }
        public List<PokemonStat> Stats { get; set; }
        public List<PokemonAbilityWrapper> Abilities { get; set; }
    }

    public class PokemonTypeWrapper
    {
        public NamedAPIResource Type { get; set; }
    }

    public class PokemonStat
    {
        public int Base_Stat { get; set; }
        public NamedAPIResource Stat { get; set; }
    }

    public class PokemonAbilityWrapper
    {
        public bool Is_Hidden { get; set; }
        public NamedAPIResource Ability { get; set; }
    }

    public class NamedAPIResource
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
