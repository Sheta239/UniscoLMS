using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CallbackToken
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("token")]
        public string Token { get; init; } = default!;

        [JsonPropertyName("masked_pan")]
        public string MaskedPan { get; init; } = default!;

        [JsonPropertyName("merchant_id")]
        public int MerchantId { get; init; }

        [JsonPropertyName("card_subtype")]
        public string CardSubtype { get; init; } = default!;

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; init; } = default!;

        [JsonPropertyName("email")]
        public string Email { get; init; } = default!;

        [JsonPropertyName("order_id")]
        public string OrderId { get; init; } = default!;

        [JsonPropertyName("user_added")]
        public bool UserAdded { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }

        public string ToConcatenatedString()
        {
            return
                CardSubtype +
                CreatedAt +
                Email +
                Id.ToString() +
                MaskedPan +
                MerchantId.ToString() +
                OrderId.ToString() +
                Token;
        }

    }
}
