using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class PaymentKeyRequest
    {
        public PaymentKeyRequest(
        int integrationId,
        int orderId,
        BillingData billingData,
        int amountCents,
        string currency = "EGP",
        string lockOrderWhenPaid = "true",
        int expiration = 36000
    )
        {

            integration_id = integrationId;
            order_id = orderId;
            amount_cents = amountCents.ToString();
            this.currency = currency;
            this.expiration = expiration;
            lock_order_when_paid = lockOrderWhenPaid;
            billing_data = billingData;
        }
        [JsonPropertyName("auth_token")]
        public string auth_token { get; set; }
        [JsonPropertyName("integration_id")]
        public int integration_id { get; }
        [JsonPropertyName("order_id")]
        public int order_id { get; }
        [JsonPropertyName("amount_cents")]
        public string amount_cents { get; }
        [JsonPropertyName("currency")]
        public string currency { get; }
        [JsonPropertyName("expiration")]
        public int expiration { get; }
        [JsonPropertyName("lock_order_when_paid")]
        public string lock_order_when_paid { get; }
        [JsonPropertyName("billing_data")]
        public BillingData billing_data { get; }
    }
}
