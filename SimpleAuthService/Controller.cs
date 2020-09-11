using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace S2SAuth.Sample.SimpleAuthService
{
    [ApiController]
    [Route("")]
    public class Controller : ControllerBase
    {
        [HttpGet("default")]
        public string GetDefaultInfo()
        {
            return "This information can be accessed by Default permission";
        }

        [HttpGet("auth")]
        [RequireS2SToken]
        public string GetAuthInfo()
        {
            return "This information can be accessed by restricted permission";
        }
    }
}
