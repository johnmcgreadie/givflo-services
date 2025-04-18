using System.Text.Json.Serialization;

namespace GivFlowAPI.Zai
{
    public class User
    {
        [JsonPropertyName("id")] 
        public String Id { get; set; }

        [JsonPropertyName("first_name")]
        public String FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public String LastName { get; set; }

        [JsonPropertyName("email")]
        public String Email { get; set; }
    }

}
