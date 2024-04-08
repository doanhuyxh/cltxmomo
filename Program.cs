using cltxmomo.Data;
using cltxmomo.Models;
using cltxmomo.Services;
using cltxmomo.Ultils;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static cltxmomo.Data.SeedData;

namespace cltxmomo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
               .SetBasePath(builder.Environment.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddMemoryCache();
            builder.Services.AddHangfire(config =>
            {

            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("MSSQL"), sqlServerOptions =>
                   {
                       sqlServerOptions.EnableRetryOnFailure(
                       maxRetryCount: 5,
                       maxRetryDelay: TimeSpan.FromSeconds(10),
                       errorNumbersToAdd: null
                        );
                   }));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddScoped<IIdentityDataInitializer, IdentityDataInitializer>();

            builder.Services.AddScoped<ICommon, Common>();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "cltxmomo";
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "cltxmomo";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Chính sách bảo mật (SameAsRequest, Always, None)
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict; // Giới hạn cookie chỉ được gửi trong cùng nguồn (Strict, Lax, None)
                options.LoginPath = "/dang-nhap";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/access-denied";
            });

            builder.Services.AddHostedService<AddDataHisotyWinServices>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var initializer = services.GetRequiredService<IIdentityDataInitializer>();
                await initializer.SeedData(
                    services.GetRequiredService<UserManager<ApplicationUser>>(),
                    services.GetRequiredService<RoleManager<IdentityRole>>()
                );
            }

            app.Run();
        }
    }
}