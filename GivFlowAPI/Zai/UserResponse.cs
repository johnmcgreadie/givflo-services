using System.Text.Json.Serialization;

namespace GivFlowAPI.Zai
{

    public class UserResponse
    {
        [JsonPropertyName("users")]
        public User User { get; set; }
    }
}
