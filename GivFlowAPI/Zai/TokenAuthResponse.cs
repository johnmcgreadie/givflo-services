using System.Text.Json.Serialization;

namespace GivFlowAPI.Zai
{

    public class TokenAuthResponse
    {
        [JsonPropertyName("token_auth")]
        public TokenAuth TokenAuth { get; set; }
    }
}
