using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Habitat
    {
        [JsonPropertyName("name")]
        public string HabitatName { get; set; }
    }
}