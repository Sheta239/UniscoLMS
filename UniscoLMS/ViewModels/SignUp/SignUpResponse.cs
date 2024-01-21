namespace UniscoLMS.ViewModels
{
    public class SignUpResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
