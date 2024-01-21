using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CallbackTransactionDataMigsTransaction
    {
        public string Id { get; init; } = default!;

        [JsonPropertyName("frequency")]
        public string Frequency { get; init; } = default!;

        [JsonPropertyName("authorizationCode")]
        public string? AuthorizationCode { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("receipt")]
        public string? Receipt { get; init; }

        [JsonPropertyName("terminal")]
        public string? Terminal { get; init; }

        [JsonPropertyName("source")]
        public string? Source { get; init; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("acquirer")]
        public CallbackTransactionDataAcquirer Acquirer { get; init; } = default!;

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
