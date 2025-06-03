using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _7DaysOfCode.Models
{
    public class Mascote
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("types")]
        public List<PokemonTypeWrapper> Types { get; set; }
        [JsonPropertyName("abilities")]
        public List<PokemonAbilityWrapper> Abilities { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
        public int Alimentacao { get; set; }
        public int Humor { get; set; }
        public int Sono { get; set; }

        private readonly Random _random;

        public Mascote()
        {
            _random = new Random();
            Alimentacao = _random.Next(0, 11); // 0 a 10
            Humor = _random.Next(0, 11);
            Sono = _random.Next(0, 11);
        }

        public void Alimentar()
        {
            Alimentacao = Math.Min(10, Alimentacao + 2); // Aumenta alimentação em 2
            Humor = Math.Max(0, Humor - 1); // Reduz humor em 1
            Sono = Math.Max(0, Sono - 1); // Reduz sono em 1
        }

        public void Brincar()
        {
            Humor = Math.Min(10, Humor + 2); // Aumenta humor em 2
            Alimentacao = Math.Max(0, Alimentacao - 1); // Reduz alimentação em 1
            Sono = Math.Max(0, Sono - 1); // Reduz sono em 1
        }

        public void Dormir()
        {
            Sono = Math.Min(10, Sono + 3); // Aumenta sono em 3
            Alimentacao = Math.Max(0, Alimentacao - 1); // Reduz alimentação em 1
            Humor = Math.Max(0, Humor - 1); // Reduz humor em 1
        }
    }
}