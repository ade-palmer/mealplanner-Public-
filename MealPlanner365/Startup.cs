using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner365.Contexts;
using MealPlanner365.Models;
using MealPlanner365.Services;
using MealPlanner365.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MealPlanner365
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
            services.AddDbContext<ApplicationDbContext>(options => options
                //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())) // TODO: Remove -  Outputs SQL to Web Server log
                //.UseSqlServer(Configuration.GetConnectionString("LocalDbConnection"))); // Development Connection
                .UseSqlServer(Configuration.GetConnectionString("ReleaseConnection"))); // Release Connection

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.User.RequireUniqueEmail = true;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.AllowedForNewUsers = true;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<MealPlanSettings>(Configuration.GetSection("MealPlanSettings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddScoped<IMealPlanRepository, MealPlanRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<IShoppingRepository, ShoppingRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "mealplan",
                    pattern: "MealPlan/{MealPlanId:Guid}/",
                    defaults: new { controller = "MealPlan", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "item",
                    pattern: "Item/{MealPlanId:Guid}/",
                    defaults: new { controller = "Item", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "category",
                    pattern: "category/{MealPlanId:Guid}/",
                    defaults: new { controller = "Category", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "meal",
                    pattern: "Meal/{MealPlanId:Guid}/",
                    defaults: new { controller = "Meal", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "shopping",
                    pattern: "Shopping/{MealPlanId:Guid}/",
                    defaults: new { controller = "Shopping", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "settings",
                    pattern: "settings/{MealPlanId:Guid}/",
                    defaults: new { controller = "settings", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
