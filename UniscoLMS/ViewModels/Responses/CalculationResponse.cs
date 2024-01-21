namespace UniscoLMS.ViewModels.Responses
{
    public class CalculationResponse
    {
        public double finalPrice { get; set; }
        public double originalPrice { get; set; }

        public double finalPriceUSD { get; set; }
        public double originalPriceUSD { get; set; }
        public double conversionRateValue { get; set; }
        public double discountPercentage { get; set; }
        public long planID { get; set; }
        public bool isSpecialCountry { get; set; }
    }
}
