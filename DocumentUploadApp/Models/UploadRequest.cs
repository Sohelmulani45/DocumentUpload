using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DocumentUploadApp.Models
{
    public class UploadRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
       
        public IFormFile? File { get; set; }
    }
}