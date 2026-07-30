namespace DocumentUploadApp.MVC.Models;

public class EmployeeDetailsVM
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public DateTime CreatedDate { get; set; }

    public string? FileName { get; set; }

    public string? ContentType { get; set; }
}