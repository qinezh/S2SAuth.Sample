using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;

namespace S2SAuth.Sample.SimpleAuthService
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequireS2STokenAttribute : AuthorizeAttribute
    {
        public RequireS2STokenAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
            Policy = S2STokenDefaults.Policy;
        }
    }
}
