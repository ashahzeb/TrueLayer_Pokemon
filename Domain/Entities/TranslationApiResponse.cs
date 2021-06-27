using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class TranslationApiResponse
    {
        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }
}