using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Misc.CustomerApi
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(
                name: "Plugin.Misc.CustomerApi.Sync",
                pattern: "Plugins/Misc/CustomerApi/Sync",
                defaults: new { controller = "CustomerApi", action = "Sync" }
            );
        }

        public int Priority => -1; 
    }
}


