using Microsoft.EntityFrameworkCore;
using NewsAggregatorProject.Data;

namespace NewsAggregatingProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            const string ConnString = "Server=DESKTOP-2FD6QEU;DataBase=NewAggregator;Trusted_Connection=True;TrustServerCertificate=True;";

            builder.Services.AddDbContext<NewsAggregatorDBContext>(opt => opt.UseSqlServer(ConnString));
            // Add services to the container.
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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}