using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CallbackTransactionDataMigsOrder
    {
        [JsonPropertyName("acceptPartialAmount")]
        public bool AcceptPartialAmount { get; init; }

        [JsonPropertyName("id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("status")]
        public string Status { get; init; } = default!;

        [JsonPropertyName("totalAuthorizedAmount")]
        public decimal TotalAuthorizedAmount { get; init; }

        [JsonPropertyName("totalCapturedAmount")]
        public decimal TotalCapturedAmount { get; init; }

        [JsonPropertyName("totalRefundedAmount")]
        public decimal TotalRefundedAmount { get; init; }

        [JsonPropertyName("creationTime")]
        public DateTimeOffset CreationTime { get; init; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
