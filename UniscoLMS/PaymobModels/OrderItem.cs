using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class OrderItem
    {
        public string Name { get; init; } = default!;

        [JsonPropertyName("description")]
        public string Description { get; init; } = default!;

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
