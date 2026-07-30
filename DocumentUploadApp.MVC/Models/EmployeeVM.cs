using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DocumentUploadApp.MVC.Models
{
    public class EmployeeVM
    {
        [Required]
        public string Name { get; set; } = "";

       public DateTime CreatedDate { get; set; }= DateTime.UtcNow;


        public IFormFile? File { get; set; }
        public string? ContentType { get; set; }
    }
}