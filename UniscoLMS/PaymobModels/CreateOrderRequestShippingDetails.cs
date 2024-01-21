using System.Text.Json.Serialization;

namespace UniscoLMS.PaymobModels
{
    public class CreateOrderRequestShippingDetails
    {
        [JsonPropertyName("number_of_packages")]
        public int numberOfPackages { get; set; }

        [JsonPropertyName("weight")]
        public int weight { get; set; }

        [JsonPropertyName("length")]
        public int length { get; set; }

        [JsonPropertyName("width")]
        public int width { get; set; }

        [JsonPropertyName("height")]
        public int height { get; set; }

        [JsonPropertyName("weight_unit")]
        public string weightUnit { get; set; }

        [JsonPropertyName("contents")]
        public string contents { get; set; }

        [JsonPropertyName("notes")]
        public string notes { get; set; }
    }
}
