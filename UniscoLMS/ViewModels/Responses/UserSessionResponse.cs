using UniscoLMS.DataBaseModels;

namespace UniscoLMS.ViewModels.Responses
{
    public class UserCourseResponse
    {
        public string Url { get; set; }
        public DateTime MeetingDate { get; set; }
        public User Expert { get; set; }
        public int Duration { get; set; }
        public string TagName { get; set; }
        public string SessinType { get; set; }

    }
}
