namespace UniscoLMS.ViewModels.Responses
{
    public class MainUserInfo
    {
               public string name { get; set; }
        public string email { get; set; }
        public string uuid { get; set; }
        public bool hasLearningGoal { get; set; }
        public bool hasPurchasedCourse { get; set; }
        public bool interests { get; set; }
        public bool isEmailVerified { get; set; }
        public bool? isMentor { get; set; }
        public bool subscribed { get; set; }
        public int subscriptionStatus { get; set; }
        public int userType { get; set; }
    }
}
