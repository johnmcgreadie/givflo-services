using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{
    public class Item
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("amount")]
        public int AmountCents { get; set; }

        [JsonPropertyName("payment_type")]
        public int PaymentType { get; set; }

        [JsonPropertyName("buyer_id")]
        public string BuyerId { get; set; }

        [JsonPropertyName("seller_id")]
        public string SellerId { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }
    }

}
