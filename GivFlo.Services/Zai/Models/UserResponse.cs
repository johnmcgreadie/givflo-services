using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{

    public class UserResponse
    {
        [JsonPropertyName("users")]
        public User User { get; set; }
    }
}
