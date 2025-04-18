using System.Text.Json.Serialization;

namespace GivFlowAPI.Zai
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public String AccessToken { get; set; }
    }
}
