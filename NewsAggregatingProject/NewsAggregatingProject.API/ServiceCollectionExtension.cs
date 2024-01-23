﻿using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services.Interfaces;
using NewsAggregatingProject.Services;
using NewsAggregatingProject.API.Mappers;

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
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<NewsMapper>();

        }
    }
}
