using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace S2SAuth.Sample.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DemoController> _logger;

        public DemoController(IHttpClientFactory httpClientFactory, ILogger<DemoController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<object>> Get()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var accessToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://mslearn-hierarchy-internal.microsoftonedoc.com");

            var client = _httpClientFactory.CreateClient();

            var urls = new List<string>
            {
                "https://localhost:5000/Permission/default",
                "https://localhost:5000/Permission/Readers",
                "https://localhost:5000/Permission/Writers",
                "https://localhost:5000/Permission/Admins"
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
    }
}
