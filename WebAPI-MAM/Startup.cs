using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace WebAPI_MAM
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
               Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            // Add services to the container.

            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddDbContext<ApplicationDbContext>(options => //Connection DataBase
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")
                )
            ); 

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c=>
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebAPI_MAM", Version="v1" }
                )
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });
        }
    }
}
