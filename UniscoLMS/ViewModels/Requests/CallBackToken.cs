using System.Text.Json.Serialization;

namespace UniscoLMS.ViewModels.Requests
{
    public class CallBackToken
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("masked_pan")]
        public string MaskedPan { get; set; }

        [JsonPropertyName("merchant_id")]
        public int MerchantId { get; set; }

        [JsonPropertyName("card_subtype")]
        public string CardSubtype { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        [JsonPropertyName("user_added")]
        public bool UserAdded { get; set; }

    }
}
