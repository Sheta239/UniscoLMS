using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class PayPaymentKeyClaims
    {
        public int IntegrationId { get; init; }

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        [JsonPropertyName("user_id")]
        public int UserId { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("exp")]
        public int Exp { get; init; }

        [JsonPropertyName("order_id")]
        public int OrderId { get; init; }

        [JsonPropertyName("pmk_ip")]
        public string PmkIp { get; init; } = default!;

        [JsonPropertyName("lock_order_when_paid")]
        public bool LockOrderWhenPaid { get; init; }

        [JsonPropertyName("billing_data")]
        public PayPaymentKeyClaimsBillingData BillingData { get; init; } = default!;

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
