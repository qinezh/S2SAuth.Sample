using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace S2SAuth.Sample.Service.Controllers
{
    [ApiController]
    [Route("")]
    public class Controller : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Controller(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("rbac")]
        public async Task<List<object>> GetRBAC()
        {
            var accessToken = await GetCustomizedAccessToken();
            var client = _httpClientFactory.CreateClient();

            var urls = new List<string>
            {
                "https://localhost:44307/default",
                "https://localhost:44307/Readers",
                "https://localhost:44307/Writers",
                "https://localhost:44307/Admins"
            };

            var result = new List<object>();
            foreach (var url in urls)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                };
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.SendAsync(request);
                result.Add(new
                {
                    api_url = url,
                    status_code = response.StatusCode.ToString()
                });
            }

            return result;
        }

        [HttpGet]
        [Route("simple")]
        public async Task GetSimple()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44346/auth"),
            };

            var accessToken = await GetManagedAccessToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            await client.SendAsync(request);
        }

        private async Task<string> GetManagedAccessToken()
            => await new AzureServiceTokenProvider()
                        .GetAccessTokenAsync("https://management.azure.com");

        private async Task<string> GetCustomizedAccessToken()
            => await new AzureServiceTokenProvider()
                        .GetAccessTokenAsync("https://mslearn-hierarchy-sandbox.microsoftonedoc.com");
    }
}
