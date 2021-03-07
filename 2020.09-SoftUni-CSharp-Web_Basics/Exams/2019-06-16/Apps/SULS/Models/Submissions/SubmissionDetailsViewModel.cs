using System;

namespace SULS.Models.Submissions
{
    public class SubmissionDetailsViewModel
    {
        public string Username { get; set; }
        public int AchievedResult { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SubmissionId { get; set; }
    }
}
