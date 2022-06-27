using AdvertApi.models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAdvert1.Web.ServiceClients
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public AdvertApiClient(IConfiguration configuration, HttpClient client)
        {
            _client = client;
            _configuration = configuration;

            var createUrl = _configuration.GetSection(key:"AdvertApi").GetValue<string>(key:"CreateUrl");
            _client.BaseAddress = new Uri(createUrl);
            _client.DefaultRequestHeaders.Add(name: "Content-type", value: "application/json");

        }
        public async Task<AdvertResponse> Create(CreateAdvertModel model)
        {
            var advertApiModel = new AdvertModel(); //Automapper
            var jsonModel = JsonConvert.SerializeObject(model);
            var response = await _client.PostAsync(_client.BaseAddress, new StringContent(jsonModel)).ConfigureAwait(continueOnCapturedContext:false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);
            var advertResponse = new AdvertResponse(); //Automapper

            return advertResponse;
        }
    }
}
