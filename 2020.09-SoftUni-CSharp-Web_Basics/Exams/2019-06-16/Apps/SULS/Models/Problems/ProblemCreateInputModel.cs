using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SULS.Models.Problems
{
    public class ProblemCreateInputModel
    {
        [Required(ErrorMessage =
            "Name" + ValidationHelper.RequiredErrorMessage)]
        [RegularExpression(@"^.{5,20}$", ErrorMessage =
            "Name" + ValidationHelper.RegexErrorMessage +
            "Minimum length is 5, maximum length is 20")]
        public string Name { get; set; }

        [Range(50, 300, ErrorMessage =
            "Points" + ValidationHelper.RangeErrorMessage + "50 - 300")]
        public int Points { get; set; }
    }
}
