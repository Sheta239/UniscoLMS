using UniscoLMS.DataBaseModels;
using System.Collections.Generic;

namespace UniscoLMS.ViewModels.Responses
{
    public class ExpertSessionsResponse
    {
        public string Url { get; set; }
        public DateTime MeetingDate { get; set; }
        public List<User> Learners { get; set; }
        public int Duration { get; set; }
        public string TagName { get; set; }
        public string SessinType { get; set; }
    }
}
