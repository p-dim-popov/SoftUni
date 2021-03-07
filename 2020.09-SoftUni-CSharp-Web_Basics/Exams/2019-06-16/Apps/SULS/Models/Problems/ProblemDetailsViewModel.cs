using System;
using System.Collections.Generic;
using System.Text;
using SULS.Models.Submissions;

namespace SULS.Models.Problems
{
    public class ProblemDetailsViewModel
    {
        public string Name { get; set; }
        public int MaxPoints { get; set; } 
        public IEnumerable<SubmissionDetailsViewModel> Submissions { get; set; }
    }
}
