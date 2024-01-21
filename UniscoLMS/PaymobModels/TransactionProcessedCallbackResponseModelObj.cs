using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class TransactionProcessedCallbackResponseModelObj
    {
        public string? Encoding { get; init; }

        [JsonPropertyName("headers")]
        public string? Headers { get; init; }

        [JsonPropertyName("content")]
        public string? Content { get; init; }

        [JsonPropertyName("status")]
        public string? Status { get; init; }
    }
}
