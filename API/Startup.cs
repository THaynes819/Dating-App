using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {        
        // Add services to the container.
        public Startup()
        {
            var myAngularPolicy = "myAngularPolicy";
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddControllers();

            
            builder.Services.AddCors(options =>
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
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        
        // app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200/"));
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors(myAngularPolicy);
        
        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();

        }
    }
}

    //     //(options =>
    //     // {
    //     //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //     // });
    //     private readonly IConfiguration _config;
        
    //     public Startup(IConfiguration config)
    //     {
    //         _config = config;
    //     }

    //     public IConfiguration Configuration { get; }

    //     // This method gets called by the runtime. Use this method to add services to the container.
    //     public void ConfigureServices(IServiceCollection services)
    //     {
    //         services.AddDbContext<DataContext>(options =>
    //         {
    //             options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
    //         });
    //         services.AddControllers();
    //         services.AddCors();
            
    //     }

    //     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //     {

    //         app.UseHttpsRedirection();

    //         app.UseRouting();

    //         app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200")); //

    //         app.UseAuthorization();

    //         app.UseEndpoints(endpoints =>
    //         {
    //             endpoints.MapControllers();
    //         });
    //     }
    // }

