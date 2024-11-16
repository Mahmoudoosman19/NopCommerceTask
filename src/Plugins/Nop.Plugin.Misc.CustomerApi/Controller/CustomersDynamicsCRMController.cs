using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.CustomerApi.Model;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.CustomerApi.Controller
{
    [Route("api/customer")]
    public class CustomerApiController : BasePluginController
    {
        private readonly ISettingService _settingService;

        public CustomerApiController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Configure()
        {
            var settings = await _settingService.LoadSettingAsync<CustomerApiSettings>();

            var model = new CustomerApiSettings
            {
                CrmUrl = settings.CrmUrl,
                ClientId = settings.ClientId,
                ClientSecret = settings.ClientSecret
            };

            return View("~/Plugins/Misc/CustomerApi/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Configure(CustomerApiSettings model)
        {
            if (ModelState.IsValid)
            {
                await _settingService.SaveSettingAsync(model);

            }

            return View("~/Plugins/Misc/CustomerApi/Views/Configure.cshtml", model);
        }
    }

}


