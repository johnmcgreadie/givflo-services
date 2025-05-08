using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{

    public class TokenAuthResponse
    {
        [JsonPropertyName("token_auth")]
        public TokenAuth TokenAuth { get; set; }
    }
}
