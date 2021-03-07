﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(40), Required]
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime DueDate { get; set; }
        public ExecutionType ExecutionType { get; set; }
        public LabelType LabelType { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public virtual ICollection<EmployeeTask> EmployeesTasks { get; set; }
            = new HashSet<EmployeeTask>();
    }
}