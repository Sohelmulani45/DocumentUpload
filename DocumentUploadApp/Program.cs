using DocumentUploadApp.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace DocumentUploadApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // ==========================
            // Allow Upload up to 500 MB
            // ==========================

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 524288000; // 500 MB
            });

            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 524288000; // 500 MB
            });

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 524288000; // 500 MB
            });

            var app = builder.Build();

         app.UseSwagger();

         app.UseSwaggerUI(c =>
         {
             c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentUpload API V1");
          });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
