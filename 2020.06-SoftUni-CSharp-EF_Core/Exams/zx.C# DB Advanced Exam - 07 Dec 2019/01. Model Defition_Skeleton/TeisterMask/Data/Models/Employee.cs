﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(40), Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Phone { get; set; }

        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
            = new HashSet<EmployeeTask>();
    }
}