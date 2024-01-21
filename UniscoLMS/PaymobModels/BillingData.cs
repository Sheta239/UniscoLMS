using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class BillingData
    {
        public BillingData(
       string firstName,
       string lastName,
       string phoneNumber,
       string email,
       string country = "NA",
       string state = "NA",
       string city = "NA",
       string apartment = "NA",
       string street = "NA",
       string floor = "NA",
       string building = "NA",
       string shippingMethod = "NA",
       string postalCode = "NA"
   )
        {

            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.country = country;
            this.state = state;
            this.city = city;
            this.apartment = apartment;
            this.street = street;
            this.floor = floor;
            this.building = building;
            this.shippingMethod = shippingMethod;
            this.postalCode = postalCode;
        }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("first_name")]
        public string firstName { get; set; }

        [JsonPropertyName("last_name")]
        public string lastName { get; set; }

        [JsonPropertyName("phone_number")]
        public string phoneNumber { get; set; }

        [JsonPropertyName("country")]
        public string country { get; }

        [JsonPropertyName("state")]
        public string state { get; }

        [JsonPropertyName("city")]
        public string city { get; }

        [JsonPropertyName("apartment")]

        public string apartment { get; }

        [JsonPropertyName("street")]
        public string street { get; }

        [JsonPropertyName("floor")]
        public string floor { get; }

        [JsonPropertyName("building")]
        public string building { get; }

        [JsonPropertyName("shipping_method")]
        public string shippingMethod { get; }

        [JsonPropertyName("postal_code")]
        public string postalCode { get; }
    }
}
