namespace UniscoLMS.ViewModels.Requests
{
    public class RecurringRequest
    {
        public decimal amount { get; set; }
        public string token { get; set; }
        public string trn { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string timeStamp { get; set; }
        public int? countryId { get; set; }
        public int countryPlanId { get; set; }

    }
}
