using Application.Services;
using Domain.Interfaces;
using Domain.Mapping;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddSignalR();
            services.AddScoped<IHangFireJob, HangFireJob>();
            return services;
        }
    }
}
