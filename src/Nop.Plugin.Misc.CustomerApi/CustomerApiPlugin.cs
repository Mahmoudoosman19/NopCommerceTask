using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Core;
using Nop.Plugin.Misc.CustomerApi.Model;

namespace Nop.Plugin.Misc.CustomerApi
{
    public class CustomerApiPlugin : BasePlugin
    {
        #region fields
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper; 
        #endregion

        #region ctor
        public CustomerApiPlugin(ILocalizationService localizationService, ISettingService settingService, IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
        }

        #endregion

        #region methods
        public override async Task InstallAsync()
        {
            var settings = new CustomerApiSettings
            {
                ApiKey = "default_key",
                CrmUrl = "https://ititasks.crm11.dynamics.com/m",
                ClientId = "cefdd43b-ca9d-4043-9285-9b9006c48f24",
                ClientSecret = "MVh8Q~MZWuW0IVTDJMq1yJP8npKZiQtraxvsNaEL"
            };

            await _settingService.SaveSettingAsync(settings);

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Misc.CustomerApi.Configure"] = "Configure Customer API Plugin",
                ["Plugins.Misc.CustomerApi.Fields.ApiKey"] = "API Key",
                ["Plugins.Misc.CustomerApi.Fields.CrmUrl"] = "CRM URL"
            });

            await base.InstallAsync();
        }


        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<Model.CustomerApiSettings>();

            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Misc.CustomerApi");

            await base.UninstallAsync();
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/CustomerApi/Configure";
        } 
        #endregion
    }
}
