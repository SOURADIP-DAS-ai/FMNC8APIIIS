
using BHSAPIBaseNC8.DBConnection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BHSAPIBaseNC8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Allow any origin
                          .AllowAnyMethod()  // Allow any HTTP method
                          .AllowAnyHeader(); // Allow any header
                });
            });
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("FM", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "my Fin Manager",
                    Description = "These are my Fin Manager application management endpoints."
                });
            });

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //Activating connection manager
            new ConnectionManagerHelper();

            var app = builder.Build();

            // Use the CORS policy
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("FM/swagger.json", "V1");
            });
            //}
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
