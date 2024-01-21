namespace UniscoLMS.ViewModels.Requests
{
    public class CaptureRequest
    {
        public string auth_token { get; set; }
        public int transaction_id { get; set; }
        public int amount_cents { get; set; }
    }
}
