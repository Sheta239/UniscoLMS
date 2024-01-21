using System;

namespace UniscoLMS.ViewModels.Responses
{
    public class PaymobPaymentResponse
    {
        public string responeId { get; set; }
        public string cardNumber { get; set; }
        public DateTime Date { get; set; }
        public bool rememberMe { get; set; }
        public long payMethod { get; set; }
        public string token { get; set; }
        public string trn { get; set; }
        public long status { get; set; }
        public int countryId { get; set; }
        public int countryPlanId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool isRenewal { get; set; }
        public string uuid { get; set; }
        public long amount { get; set; }
        public int itemType { get; set; }

    }
}
