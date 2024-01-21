using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CreateOrderRequestOrderItem
    {
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; } 

        [JsonPropertyName("amount_cents")]
        public int amountCents { get; set; }

        [JsonPropertyName("quantity")]
        public int quantity { get; set; }
    }
}
