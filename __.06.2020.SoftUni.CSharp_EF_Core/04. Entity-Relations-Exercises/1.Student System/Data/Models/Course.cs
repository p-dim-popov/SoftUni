namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [MaxLength(80)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public ICollection<Resource> Resources { get; } = new HashSet<Resource>();
        public ICollection<StudentCourse> StudentsEnrolled { get; } = new HashSet<StudentCourse>();
        public ICollection<Homework> HomeworkSubmissions { get; } = new HashSet<Homework>();
    }
}
