using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{
    public class AnonymousUser
    {
        public AnonymousUser()
        {
            Id = $"givflo-anon{DateTime.Now.Ticks}";

        }
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName => $"{Id}";

        [JsonPropertyName("last_name")]
        public string LastName => $"{Id}";

        [JsonPropertyName("email")]
        public string Email => $"{Id}@givflo.com";

        [JsonPropertyName("country")]
        public string Country => $"AUS";
    }

}
