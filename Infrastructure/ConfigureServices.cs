using Domain.Interfaces;
using Domain.Repositories;
using Domain.UnitOfWork;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWrok;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBConetxt>(options =>

                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDBConetxt).Assembly.FullName)));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.User.AllowedUserNameCharacters = null;
            }).AddEntityFrameworkStores<ApplicationDBConetxt>();
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHangfire(_configuration => _configuration
                   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                   .UseSimpleAssemblyNameTypeSerializer()
                   .UseRecommendedSerializerSettings()
                   .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                   {
                       CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                       SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                       QueuePollInterval = TimeSpan.Zero,
                       UseRecommendedIsolationLevel = true,
                       DisableGlobalLocks = true
                   }));
            services.AddHangfireServer();


            services.AddControllers();
            return services;
        }
    }
}
