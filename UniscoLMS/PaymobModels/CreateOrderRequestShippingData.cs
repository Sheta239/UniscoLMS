using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CreateOrderRequestShippingData
    {
        [JsonPropertyName("first_name")]
        public string firstName { get; set; }

        [JsonPropertyName("last_name")]
        public string fastName { get; set; } 

        [JsonPropertyName("email")]
        public string email { get; set; } 

        [JsonPropertyName("phone_number")]
        public string phoneNumber { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("state")]
        public string state { get; set; }

        [JsonPropertyName("city")]
        public string city { get; set; }

        [JsonPropertyName("street")]
        public string street { get; set; }

        [JsonPropertyName("building")]
        public string building { get; set; }

        [JsonPropertyName("floor")]
        public string floor { get; set; }

        [JsonPropertyName("apartment")]
        public string apartment { get; set; }

        [JsonPropertyName("postal_code")]
        public string postalCode { get; set; }

        [JsonPropertyName("extra_description")]
        public string extraDescription { get; set; }
    }
}
