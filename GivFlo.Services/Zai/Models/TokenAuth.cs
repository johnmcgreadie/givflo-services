using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{
    public class TokenAuth
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }

}
