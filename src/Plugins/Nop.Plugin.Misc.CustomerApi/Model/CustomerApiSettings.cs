using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.CustomerApi.Model
{
    public class CustomerApiSettings : ISettings
    {
        public string ApiKey { get; set; }
        public string CrmUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
