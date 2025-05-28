using System.Text.Json.Serialization;

namespace _7DaysOfCode.Models
{
    public class PokemonEntry
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
