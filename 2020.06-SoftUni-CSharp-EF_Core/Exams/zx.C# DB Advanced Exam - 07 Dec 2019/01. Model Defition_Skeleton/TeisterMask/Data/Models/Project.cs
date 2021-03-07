using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        [MaxLength(40), Required]
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
            = new HashSet<Task>();
    }
}