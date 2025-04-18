using System.Text.Json.Serialization;

namespace GivFlowAPI.Zai
{
    public class AnonymousUser
    {
        public AnonymousUser()
        {
            Id = $"givflo-anon{DateTime.Now.Ticks}";

        }
        [JsonPropertyName("id")] 
        public String Id { get; set; }

        [JsonPropertyName("first_name")]
        public String FirstName => $"{Id}";

        [JsonPropertyName("last_name")]
        public String LastName => $"{Id}";

        [JsonPropertyName("email")]
        public String Email => $"{Id}@givflo.com";

        [JsonPropertyName("country")]
        public String Country => $"AUS";
    }

}
