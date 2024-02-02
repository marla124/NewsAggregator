
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace NewsAggregatingProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo
            .File("log.txt", rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information)
            .Enrich.FromLogContext()

            .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
            builder.Configuration.AddJsonFile("AFINN-ru.json");
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterServices(builder.Configuration);
            builder.Services.ConfigureJwt(builder.Configuration);


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseHangfireDashboard();
                

            app.MapControllers();
            app.MapHangfireDashboard();

            app.Run();
        }
    }
}
