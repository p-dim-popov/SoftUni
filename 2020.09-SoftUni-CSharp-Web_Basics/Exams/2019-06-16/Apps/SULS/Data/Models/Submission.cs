using System;
using System.ComponentModel.DataAnnotations;
using SULS.Models;

namespace SULS.Data.Models
{
    public class Submission
    {
        [Key]
        public string Id { get; set; }

        [Required, MaxLength(800)]
        public string Code { get; set; }

        public int AchievedResult { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public Problem Problem { get; set; }
        
        public User User { get; set; }
    }
}
