using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagementRazorViews.DatabaseContext;
using UserManagementRazorViews.Filters;
using UserManagementRazorViews.Interfaces;
using UserManagementRazorViews.ModelBinders;
using UserManagementRazorViews.Models;
using UserManagementRazorViews.Repositories;
using UserManagementRazorViews.Services;
using UserManagementRazorViews.Validators;

namespace UserManagementRazorViews
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
            services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add<LoggingAuthorizationFilter>();
                opt.Filters.Add<LoggingResourceFilter>();
                opt.Filters.Add<LoggingActionFilter>();
                opt.Filters.Add<LoggingExceptionFilter>();
                opt.Filters.Add<LoggingResultFilter>();
                opt.ModelBinderProviders.Insert(0, new ManageUserModelBinderProvider());
                
            }).AddFluentValidation();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppConnectionString"));
                options.EnableSensitiveDataLogging();
            });

            services.AddTransient<IValidator<ManageUserViewModel>, UserValidator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<ILogHandler, LogHandler>(); //stores log for rendering on UI
            services.AddSingleton<IContentService, ContentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
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
                    pattern: "{controller=Users}/{action=Index}/{id?}");
            });
        }
    }
}
