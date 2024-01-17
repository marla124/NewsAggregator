using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Services;
using NewsAggregatingProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using NewsAggregatingProject.FluentValidation;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;
using NewsAggregatingProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NewsAggregatingProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString= builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<NewsAggregatingDBContext>(opt => opt.UseSqlServer(connectionString));
            // Add services to the container.

            var logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo
                .File("log.txt", rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information)
                .WriteTo
                .Console()
                .Enrich.FromLogContext()

                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(opt =>
                            {
                                opt.LoginPath = new PathString("/User/Login");
                                opt.AccessDeniedPath = new PathString("/User/AccessDenied");
                                opt.LogoutPath = new PathString("/User/Logout");
                            });
            builder.Services
                .AddValidatorsFromAssemblyContaining<UserRegisterValidator>();
            builder.Services.AddScoped<IRepository<News>, Repository<News>>();
            builder.Services.AddScoped<IRepository<Source>, Repository<Source>>();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<UserStatus>, Repository<UserStatus>>();
            builder.Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();

            //builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<INewsService, NewsService>();
            builder.Services.AddScoped<IUserService, UserService>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddControllersWithViews();

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
            //app.UseSerilogRequestLogging();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.Map("/NotFound", ()=>new NotFoundResult());


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}