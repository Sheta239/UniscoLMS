namespace UniscoLMS.ViewModels
{
    public class ForgetPasswordRequest
    {
        public string verificationCode { get; set; }
        public string userEmail { get; set; }
        public string newPassword { get; set; }
    }
}
