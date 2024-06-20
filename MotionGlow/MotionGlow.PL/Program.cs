using Microsoft.EntityFrameworkCore;
using MotionGlow.BLL.IServices;
using MotionGlow.BLL.Services;
using MotionGlow.DAL.Data;

namespace MotionGlow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Entity Framework Core to use SQL Server
            services.AddDbContext<MotionGlowDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register services
            services.AddControllersWithViews();

            // Add other services (like your business logic services)
            services.AddScoped<IESP32_DeviceService, ESP32_DeviceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPIRSensorService, PIRSensorService>();
            services.AddScoped<ISensorActivityLogService, SensorActivityLogService>();
            services.AddScoped<ISoundSensorService, SoundSensorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}