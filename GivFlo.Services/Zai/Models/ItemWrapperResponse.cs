using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{

    public class ItemWrapperResponse
    {
        [JsonPropertyName("Result")]
        public string Result { get; set; }
    }
}
