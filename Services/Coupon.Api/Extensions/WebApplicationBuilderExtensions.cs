using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Coupon.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder) {

            var apiSettingsSection = builder.Configuration.GetSection("ApiSettings");
            var secrets = apiSettingsSection.GetValue<string>("Secret");
            var issuer = apiSettingsSection.GetValue<string>("Issuer");
            var audience = apiSettingsSection.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secrets);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            builder.Services.AddAuthorization();


            return builder;
        }
    }
}
