
namespace UniscoLMS.ViewModels
{
    public class UserProfileResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string GoogleId { get; set; }
        public string Mobile { get; set; }
        public bool? EmailValidation { get; set; }
        public bool? PhoneValidation { get; set; }
        public bool? Approved { get; set; }
        public bool? Verified { get; set; }

        public string Bio { get; set; }
  
    }


}
