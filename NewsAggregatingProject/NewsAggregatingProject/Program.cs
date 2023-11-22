using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;
using NewAggregating.Repsitories;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.MVC7.Services;

namespace NewsAggregatingProject.MVC7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            const string ConnString = "Server=DESKTOP-2FD6QEU;DataBase=NewAggregator;Trusted_Connection=True;TrustServerCertificate=True;";

            builder.Services.AddDbContext<NewsAggregatingDBContext>(opt =>opt.UseSqlServer(ConnString));
            // Add services to the container.
            builder.Services.AddScoped<IRepository<New>, Repository<New>>();
            builder.Services.AddScoped<IRepository<Source>, Repository<Source>>();
            builder.Services.AddScoped<IRepository<User>, Repository<User>>();
            builder.Services.AddScoped<IRepository<RatingScale>, Repository<RatingScale>>();
            builder.Services.AddScoped<IRepository<UserStatus>, Repository<UserStatus>>();
            builder.Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();


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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}