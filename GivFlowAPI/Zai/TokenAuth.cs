using System.Text.Json.Serialization;

namespace GivFlowAPI.Zai
{
    public class TokenAuth
    {
        [JsonPropertyName("token_type")] 
        public String TokenType { get; set; }

        [JsonPropertyName("token")]
        public String Token { get; set; }

        [JsonPropertyName("user_id")]
        public String UserId { get; set; }
    }

}
