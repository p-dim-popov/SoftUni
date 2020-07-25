namespace P01_StudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum ContentType { Application = 0, Pdf = 1, Zip = 2 }

    public class Homework
    {
        [Key]
        public int HomeworkId { get; set; }
        [Column(TypeName = "varchar(max)")]
        [Required]
        public string Content { get; set; }
        public ContentType ContentType { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
