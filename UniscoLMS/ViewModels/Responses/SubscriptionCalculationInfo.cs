namespace UniscoLMS.ViewModels.Responses
{
    public class SubscriptionCalculationInfo
    {
        public int? Id { get; set; }
        public string PlanNameEn { get; set; }
        public string PlanNameAr { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string MarketingMessageEn { get; set; }
        public string MarketingMessageAr { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public float Originalprice { get; set; }
        public decimal ConversionRate { get; set; }
        public float DiscountValue { get; set; }
        public bool? Enabled { get; set; }
    }
}
