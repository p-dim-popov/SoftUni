using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SULS.Models.Submissions
{
    public class SubmissionCreateInputModel
    {
        [Required(ErrorMessage =
            "Code" + ValidationHelper.RequiredErrorMessage)]
        [MinLength(30, ErrorMessage =
            "Code" + ValidationHelper.MinLengthErrorMessage + "30")]
        [MaxLength(800, ErrorMessage =
            "Code" + ValidationHelper.MaxLengthErrorMessage + "800")]
        public string Code { get; set; }
    }
}
