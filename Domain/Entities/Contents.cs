using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Contents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }
    }
}
