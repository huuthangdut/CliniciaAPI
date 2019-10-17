using Clinicia.Common.Enums;
using Clinicia.Common.Helpers;
using Clinicia.Common.Runtime.Security;
using Clinicia.Entities.Token;
using Clinicia.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clinicia.Services.Implementations
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtTokenService(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<JwtTokenResult> RequestTokenAsync(ClaimsIdentity identity, ApplicationType application)
        {
            return new JwtTokenResult
            {
                TokenType = "Bearer",
                AccessToken = await GenerateEncodedToken(identity, application)
            };
        }

        private async Task<string> GenerateEncodedToken(ClaimsIdentity identity, ApplicationType application)
        {
            _jwtOptions.IssuedAt = DateTime.Now;

            var claims = identity.Claims.Union(
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identity.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(
                        JwtRegisteredClaimNames.Iat,
                        _jwtOptions.IssuedAt.LocalToUtcTime().ToSecondsTimestamp().ToString(),
                        ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.System, application.ToString()),
                });

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.IssuedAt,
                GetExpirationDate(application),
                _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private DateTime GetExpirationDate(ApplicationType application)
        {
            switch (application)
            {
                case ApplicationType.Mobile:
                    return _jwtOptions.IssuedAt.AddHours(_jwtOptions.MobileExpirationInHours);

                case ApplicationType.Web:
                    return _jwtOptions.IssuedAt.AddHours(_jwtOptions.WebExpirationInHours);

                default:
                    throw new ArgumentOutOfRangeException(nameof(application), application, null);
            }
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
}