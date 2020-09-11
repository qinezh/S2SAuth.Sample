using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace S2SAuth.Sample.Service.Controllers
{
    [ApiController]
    [Route("")]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet("default")]
        public string GetDefaultInfo()
        {
            return "This information can be accessed by Default permission";
        }

        [Authorize(Roles = "Readers,Writers,Admins", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Readers")]
        public string GetReaderInfo()
        {
            return "This information can be accessed by Reader permission";
        }

        [Authorize(Roles = "Writers,Admins", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Writers")]
        public string GetWriterInfo()
        {
            return "This information can be accessed by Writer permission";
        }

        [Authorize(Roles = "Admins", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Admins")]
        public string GetAdminInfo()
        {
            return "This information can be accessed by Admin permission";
        }
    }
}
