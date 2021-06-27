using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}