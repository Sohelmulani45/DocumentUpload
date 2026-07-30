using DocumentUploadApp.Data;
using DocumentUploadApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentUploadApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public DetailsController(
            AppDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Details/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // GET: api/Details/Download/1
        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            if (employee.FileData == null)
                return NotFound("File not found.");

            return File(
                employee.FileData,
                employee.ContentType ?? "application/octet-stream",
                employee.FileName);
        }

        // POST: api/Details/Upload

        [RequestSizeLimit(524288000)] // 500 MB
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadRequest request)
        {
            byte[]? fileBytes = null;
            string? fileName = null;
            string? contentType = null;

            if (request.File != null && request.File.Length > 0)
            {
                using var memoryStream = new MemoryStream();

                await request.File.CopyToAsync(memoryStream);

                fileBytes = memoryStream.ToArray();

                fileName = request.File.FileName;

                contentType = request.File.ContentType;
            }

            Employee employee = new Employee
            {
                Name = request.Name,
                CreatedDate = DateTime.UtcNow,
                FileName = fileName,
                ContentType = contentType,
                FileData = fileBytes
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Data Saved Successfully",
                employee.Id,
                employee.Name,
                employee.CreatedDate,
                employee.FileName
            });
        }
    }
}