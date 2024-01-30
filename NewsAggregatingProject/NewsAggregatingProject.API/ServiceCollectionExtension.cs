using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.CQS.Commands;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services;
using NewsAggregatingProject.Services.Interfaces;
using System.Text;

namespace NewsAggregatingProject.API
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<NewsAggregatingDBContext>(opt => opt.UseSqlServer(connectionString));


            services.AddScoped<IRepository<News>, Repository<News>>();
            services.AddScoped<IRepository<Source>, Repository<Source>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<UserStatus>, Repository<UserStatus>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<ISourceService, SourceService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();

            services.AddScoped<NewsMapper>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AddNewsCommand).Assembly);
            });

        }
        public static void ConfigureJwt
           (this IServiceCollection services, IConfiguration configuration)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var secret = configuration["Jwt:Secret"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                    };
                });

        }
    }
}
