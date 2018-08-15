using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MonitoringSystem.Data;
using MonitoringSystem.Models;
using MonitoringSystem.Persistences.IRepositories;
using MonitoringSystem.Persistences.Repositories;
using PMS.Hubs;

namespace MonitoringSystem
{
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFanRepository, FanRepository>();
            services.AddScoped<IHumidityRepository, HumidityRepository>();
            services.AddScoped<IRackRepository, RackRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ITemperatureRepository, TemperatureRepository>();
            services.AddScoped<IPlotRepository, PlotRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IFanStatusRepository, FanStatusRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            //.AddUserManager<UserManager<IdentityUser>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            // Enable Jwt Authentication 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddCookie(cfg => cfg.SlidingExpiration = true)
              .AddJwtBearer(cfg =>
              {
                  cfg.RequireHttpsMetadata = false;
                  cfg.SaveToken = true;

                  cfg.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidIssuer = Configuration["Tokens:Issuer"],
                      ValidAudience = Configuration["Tokens:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                  };
              });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", builder =>
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyOrigin()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
                });
            services.AddSignalR();
            services.AddResponseCompression();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddAutoMapper();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseMvc();

            app.UseCors("AllowAll");
            app.UseSignalR(routes =>
            {
                routes.MapHub<MonitoringSystemHub>("/hub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
