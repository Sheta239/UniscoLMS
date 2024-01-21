using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UniscoLMS.PaymobModels
{
    public class CreateOrderRequest
    {

        [JsonProperty("auth_token")]
        public string auth_token { get; set; }
        [JsonProperty("amount_cents")]
        public string amount_cents { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }

        [JsonProperty("merchant_order_id")]
        public string merchant_order_id { get; set; }

        [JsonProperty("delivery_needed")]
        public string delivery_needed { get; set; }
        [JsonProperty("shipping_data")]
        public CreateOrderRequestShippingData? shipping_data { get; set; }
        [JsonProperty("shipping_details")]
        public CreateOrderRequestShippingDetails? shipping_details { get; set; }
        [JsonProperty("items")]
        public IEnumerable<CreateOrderRequestOrderItem>? items { get;  private init; } =
        Array.Empty<CreateOrderRequestOrderItem>();


        public static CreateOrderRequest CreateOrder(
        long amountCents,
        string currency = "EGP",
        string? merchantOrderId = null
    )
        {

            return new()
            {
                amount_cents = amountCents.ToString(),
                delivery_needed = "false",
                currency = currency,
                merchant_order_id = merchantOrderId,
            };
        }

    }
}
