using System.IdentityModel.Tokens.Jwt;

namespace S2SAuth.Sample.SimpleAuthService
{
    public class S2SSecurityTokenValidator : JwtSecurityTokenHandler
    {
        public S2SSecurityTokenValidator()
        {
            MapInboundClaims = false;
        }
    }
}
