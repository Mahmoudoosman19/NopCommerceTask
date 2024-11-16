using Nop.Core.Domain.Customers;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Common; 
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.CustomerApi.Events
{
    public class CustomerRegisteredEventConsumer : IConsumer<CustomerRegisteredEvent>
    {
        private readonly CrmHelper _crmHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IGenericAttributeService _genericAttributeService;

        #region ctor
        public CustomerRegisteredEventConsumer(
    CrmHelper crmHelper,
    ILocalizationService localizationService,
    IGenericAttributeService genericAttributeService)
        {
            _crmHelper = crmHelper;
            _localizationService = localizationService;
            _genericAttributeService = genericAttributeService;
        }
        #endregion

        #region methods

        public async Task HandleEventAsync(CustomerRegisteredEvent eventMessage)
        {
            var customer = eventMessage.Customer;

            var firstName = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.FirstNameAttribute);
            var lastName = await _genericAttributeService.GetAttributeAsync<string>(customer, NopCustomerDefaults.LastNameAttribute);

            if (await _crmHelper.CheckContactExistsByEmail(customer.Email))
            {
                throw new Exception(await _localizationService.GetResourceAsync("Plugins.Misc.CustomerApi.ContactExists"));
            }

            await _crmHelper.CreateContact(firstName, lastName, customer.Email);
        } 
        #endregion


    }
}
