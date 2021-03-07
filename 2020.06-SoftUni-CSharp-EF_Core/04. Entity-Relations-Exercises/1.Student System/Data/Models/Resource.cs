namespace P01_StudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum ResourceType { Video = 0, Presentation = 1, Document = 2, Other = 3 }

    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(max)")]
        [Required]
        public string Url { get; set; }
        public ResourceType ResourceType { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
