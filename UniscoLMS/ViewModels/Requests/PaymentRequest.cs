using Newtonsoft.Json;

namespace UniscoLMS.ViewModels.Requests
{
    public class PaymentRequest
    {
        [JsonProperty("amount")]
        public float amount { get; set; }

        [JsonProperty("remember_me")]
        public string remember_me { get; set; }
        [JsonProperty("trn")]
        public string trn { get; set; }

        [JsonProperty("timeStamp")]
        public string timeStamp { get; set; }
        [JsonProperty("countryId")]
        public int? countryId { get; set; }
        [JsonProperty("countryPlanId")]
        public int countryPlanId { get; set; }

        public User user { get; set; }
        public Card card { get; set; }
        public int itemType { get; set; }
        public string token_name { get; set; }

        public string paypalPlanId { get; set; }
        public decimal countryPlanPrice { get; set; }
        public string returnUrl { get; set; }
        public string cancelUrl { get; set; }

        public class User
        {
            [JsonProperty("firstName")]
            public string firstName { get; set; }
            [JsonProperty("lastName")]
            public string lastName { get; set; }
            [JsonProperty("phoneNumber")]
            public string phoneNumber { get; set; }
            [JsonProperty("email")]
            public string email { get; set; }
        }

        public class Card
        {
            public string card_number { get; set; }

            public int expiry_month { get; set; }

            public int expiry_year { get; set; }

            public string card_holder_name { get; set; }

            public string card_security_code { get; set; }
        }
    }
}
