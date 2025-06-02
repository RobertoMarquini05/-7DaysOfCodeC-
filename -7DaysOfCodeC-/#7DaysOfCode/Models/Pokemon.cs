using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _7DaysOfCode.Models
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("types")]
        public List<PokemonTypeWrapper> Types { get; set; }
        [JsonPropertyName("stats")]
        public List<PokemonStat> Stats { get; set; }
        [JsonPropertyName("abilities")]
        public List<PokemonAbilityWrapper> Abilities { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
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
