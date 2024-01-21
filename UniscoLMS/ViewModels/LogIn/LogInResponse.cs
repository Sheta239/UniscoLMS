namespace UniscoLMS.ViewModels
{
    public class LogInResponse
    {
        public bool IsEmailVerified { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
