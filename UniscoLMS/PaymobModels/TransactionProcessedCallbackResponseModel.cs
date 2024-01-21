using System;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class TransactionProcessedCallbackResponseModel
    {
        [JsonPropertyName("response_received_at")]
        public DateTimeOffset ResponseReceivedAt { get; init; }

        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; init; } = default!;

        [JsonPropertyName("response")]
        public TransactionProcessedCallbackResponseModelObj Response { get; init; } = default!;
    }
}
