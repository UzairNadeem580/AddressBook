using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AddressBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext for Entity Framework Core (if using a database)
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Enable CORS (Cross-Origin Resource Sharing)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Add controllers with JSON serialization
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names as-is
                        options.JsonSerializerOptions.DictionaryKeyPolicy = null; // Preserve dictionary keys as-is
                    });

            // Other service configurations...

            // Example: Add other services, such as authentication, authorization, etc.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable CORS
            app.UseCors("AllowAll");

            app.UseRouting();

            // Other middleware configurations...

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Additional endpoint mappings if needed...
            });
        }
    }
}
