using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class Flavor
    {
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }
}