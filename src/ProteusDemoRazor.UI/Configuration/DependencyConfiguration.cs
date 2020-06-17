using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Proteus.UI.HealthCheck;
using Proteus.UI.Interfaces;
using Proteus.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proteus.UI.Configuration
{

    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Add Web Layer
            services.AddAutoMapper(typeof(Startup)); // Add AutoMapper
            services.AddScoped<IIndexPageService, IndexPageService>();
            //services.AddScoped<IProductPageService, ProductPageService>();
           // services.AddScoped<ICategoryPageService, CategoryPageService>();

            // Add Miscellaneous
            services.AddHttpContextAccessor();
            services.AddHealthChecks()
                .AddCheck<IndexPageHealthCheck>("home_page_health_check");


        }

        
    }
}
