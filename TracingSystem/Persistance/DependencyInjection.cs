using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;

namespace TracingSystem.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddDbContext<TracingSystemDbContext>(options =>
            {
                options.UseSqlite("Data Source=TracingSyatem.db");
            })
                .AddScoped<ITracingSystemDbContext>(provider =>
                provider.GetService<TracingSystemDbContext>());
            return services;
        }
    }
}
