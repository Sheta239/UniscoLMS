using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniscoLMS.ViewModels.Requests
{
    public class CheckoutOrderRequest
    {
        [JsonProperty("amount")]
        public decimal amount { get; set; }

        [JsonProperty("remember_me")]
        public string remember_me { get; set; }

        [JsonProperty("trn")]
        public string trn { get; set; }

        public User user { get; set; }


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




    }
}
