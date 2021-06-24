using System.Text.Json.Serialization;

namespace Domain.Entity
{
    public class Language
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}