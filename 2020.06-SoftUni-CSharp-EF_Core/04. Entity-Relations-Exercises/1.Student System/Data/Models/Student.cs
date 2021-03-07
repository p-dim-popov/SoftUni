namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "char(10)")]
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? Birthday { get; set; }

        public ICollection<StudentCourse> CourseEnrollments { get; } = new HashSet<StudentCourse>();
        public ICollection<Homework> HomeworkSubmissions { get; } = new HashSet<Homework>();
    }
}
