using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {        
        private readonly IConfiguration _config;
        //WebApplicationBuilder builder;
        string myAngularPolicy = "myAngularPolicy";
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //builder = WebApplication.CreateBuilder();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });
            services.AddControllers();

            
            services.AddCors(options =>
            {
                options.AddPolicy(myAngularPolicy,
                                    builder =>
                                    {
                                        builder.AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .WithOrigins("https://localhost:4200");
                                    });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // Configure the HTTP request pipeline.
            
                // app.UseSwagger();
                // app.UseSwaggerUI();
            
            
            app.UseHttpsRedirection();
            
            // app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200/"));
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(myAngularPolicy);
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

