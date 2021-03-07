using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS.Data.Models
{
    public class Problem
    {
        [Key]
        public string Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public int Points { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
            = new HashSet<Submission>();
    }
}
