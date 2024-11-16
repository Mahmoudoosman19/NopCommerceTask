using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Nop.Plugin.Misc.CustomerApi.Model;
using Nop.Services.Configuration;
using Azure.Core;
using Newtonsoft.Json.Linq;
using Microsoft.Rest;
using AutoMapper.Configuration;

public class CrmHelper
{
    private readonly ServiceClient _serviceClient;
    private readonly ISettingService _settingService;

    #region ctor
    public CrmHelper(ISettingService settingService)
    {
        _settingService = settingService;

        var settings = settingService.LoadSettingAsync<CustomerApiSettings>().Result;

        string crmUrl = settings.CrmUrl;
        string clientId = settings.ClientId;
        string clientSecret = settings.ClientSecret;

        var authToken = GetOAuthToken(clientId, clientSecret, crmUrl).Result;

        string connectionString = $"AuthType=OAuth;Url={crmUrl};ClientId={clientId};ClientSecret={clientSecret};Authority=https://login.microsoftonline.com/common;AccessToken={authToken}";

        _serviceClient = new ServiceClient(connectionString);
    } 
    #endregion

    #region methods
    private async Task<string> GetOAuthToken(string clientId, string clientSecret, string crmUrl)
    {
        var authority = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

        var client = new HttpClient();
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            new KeyValuePair<string, string>("scope", crmUrl + ".default"),
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        var response = await client.PostAsync(authority, content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = JObject.Parse(responseString);
            return tokenResponse["access_token"].ToString();
        }
        else
        {
            throw new Exception("Error retrieving OAuth token: " + responseString);
        }
    }

    public async Task<bool> CheckContactExistsByEmail(string email)
    {
        var query = new QueryExpression("contact")
        {
            ColumnSet = new ColumnSet("emailaddress1")
        };
        query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, email);

        var entities = await _serviceClient.RetrieveMultipleAsync(query);
        return entities.Entities.Count > 0;
    }

    public async Task CreateContact(string firstName, string lastName, string email)
    {
        var contact = new Entity("contact")
        {
            ["firstname"] = firstName,
            ["lastname"] = lastName,
            ["emailaddress1"] = email
        };

        await _serviceClient.CreateAsync(contact);
    } 
    #endregion
}


