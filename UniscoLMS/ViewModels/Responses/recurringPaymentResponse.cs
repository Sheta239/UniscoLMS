namespace UniscoLMS.ViewModels.Responses
{
    public class recurringPaymentResponse
    {
        public bool isSuccess { get; set; }
        public int orderId { get; set; }
        public string createdAt { get; set; }
        public int transactionId { get; set; }
        public string cardNumber { get; set; }
    }
}
