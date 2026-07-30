using System.ComponentModel.DataAnnotations;

namespace DocumentUploadApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public byte[]? FileData { get; set; }
    }
}