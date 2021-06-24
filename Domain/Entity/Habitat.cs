using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class Habitat
    {
        [JsonPropertyName("name")]
        public string HabitatName { get; set; }
    }
}