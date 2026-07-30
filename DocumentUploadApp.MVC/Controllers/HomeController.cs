using DocumentUploadApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DocumentUploadApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: Home/Index
        [HttpPost]
        public async Task<IActionResult> Index(EmployeeVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Store UTC DateTime
            model.CreatedDate = DateTime.UtcNow;

            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://documentupload-py7y.onrender.com/");

            using var form = new MultipartFormDataContent();

            form.Add(new StringContent(model.Name), "Name");
            form.Add(new StringContent(model.CreatedDate.ToString("O")), "CreatedDate");

            if (model.File != null)
            {
                // Don't use MemoryStream
                var stream = model.File.OpenReadStream();

                var fileContent = new StreamContent(stream);

                fileContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(model.File.ContentType);

                form.Add(fileContent, "File", model.File.FileName);
            }

            var response = await client.PostAsync("api/Details/Upload", form);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                ViewBag.Message = error;

                return View(model);
            }

            var json = await response.Content.ReadAsStringAsync();

            var employee = JsonSerializer.Deserialize<EmployeeDetailsVM>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return RedirectToAction("Details", new { id = employee!.Id });
        }

        // GET: Home/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient();

            client.BaseAddress = new Uri("https://documentupload-py7y.onrender.com/");

            var response = await client.GetAsync($"api/Details/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();

            var employee = JsonSerializer.Deserialize<EmployeeDetailsVM>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return View(employee);
        }
    }
}