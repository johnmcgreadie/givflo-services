using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{

    public class ItemsResponse
    {
        [JsonPropertyName("items")]
        public Item Items { get; set; }
    }
}
