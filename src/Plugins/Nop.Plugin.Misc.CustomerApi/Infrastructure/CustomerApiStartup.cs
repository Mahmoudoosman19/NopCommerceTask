using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.CustomerApi.Events;
using Nop.Plugin.Misc.CustomerApi.Model;
using Nop.Services.Configuration;
using Nop.Services.Events;
using NUglify.Css;

namespace Nop.Plugin.Misc.CustomerApi.Infrastructure
{
    public class CustomerApiStartup : INopStartup
    {
        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<CrmHelper>();
            services.AddScoped<IConsumer<CustomerRegisteredEvent>, CustomerRegisteredEventConsumer>();
        }

        public int Order => 1;
    }
}
