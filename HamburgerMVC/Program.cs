using HamburgerMVC.Models;
using HamburgerMVC.Repositories.Interfaces;
using HamburgerMVC.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace HamburgerMVC
{
    public class Program
    {
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context context)
        //{
        //    SeedData.Initialize(app.ApplicationServices);
        //}
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

      
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<Context>(); 

            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
            });
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Register/Login";
            //    options.AccessDeniedPath = "/AccessDenied";
            //});


            //builder.Services.ConfigureApplicationCookie(
            //    option =>
            //    {
            //        option.Cookie.Name = "UserCookie";
            //        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //        option.SlidingExpiration = true;
            //    }

            //);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}