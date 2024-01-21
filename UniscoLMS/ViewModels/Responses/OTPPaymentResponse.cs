using Newtonsoft.Json;
using System;

namespace UniscoLMS.ViewModels.Responses
{
    public class OTPPaymentResponse
    {
        [JsonProperty("responeId")]
        public string responeId { get; set; }
        [JsonProperty("cardNumber")]
        public string cardNumber { get; set; }
        [JsonProperty("Date")]
        public DateTime? Date { get; set; }
        [JsonProperty("rememberMe")]
        public bool rememberMe { get; set; }
        [JsonProperty("payMethod")]
        public long payMethod { get; set; }
        [JsonProperty("token")]
        public string token { get; set; }
        [JsonProperty("trn")]
        public string trn { get; set; }
        [JsonProperty("status")]
        public long status { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("mainUserId")]
        public Guid userId { get; set; }
        [JsonProperty("amount")]
        public long amount { get; set; }

    }
}
