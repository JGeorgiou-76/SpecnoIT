using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config){
            
            //Adding the Token Service
            services.AddScoped<ITokenService, TokenService>();
            
            //Adding the service for our Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();

            //Adding Auto Mapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            //Adding the Connection String for the Database
            services.AddDbContext<DataContext>(options => {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}