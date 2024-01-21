using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CallbackTransactionDataAcquirer
    {
        [JsonPropertyName("settlementDate")]
        public string SettlementDate { get; init; } = default!;

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; init; } = default!;

        [JsonPropertyName("id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("date")]
        public string Date { get; init; } = default!;

        [JsonPropertyName("merchantId")]
        public string MerchantId { get; init; } = default!;

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; init; } = default!;

        [JsonPropertyName("batch")]
        public int Batch { get; init; }
    }
}
