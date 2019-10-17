using Clinicia.Common.Helpers;
using Clinicia.Common.Runtime.Security;
using Clinicia.Repositories;
using Clinicia.Repositories.Schemas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Clinicia.Infrastructure.ContainerConfigs
{
    public static class AuthServicesInstaller
    {
        public static void ConfigureServicesAuth(IServiceCollection services, IConfiguration configuration)
        {
            // Get options from app settings
            var jwtOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var appSettings = configuration.GetSection(nameof(AppSettings));

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions[nameof(JwtIssuerOptions.Secret)]));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(
                options =>
                {
                    options.Issuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)];
                    options.Audience = jwtOptions[nameof(JwtIssuerOptions.Audience)];
                    options.MobileExpirationInHours = jwtOptions[nameof(JwtIssuerOptions.MobileExpirationInHours)].ParseInt();
                    options.WebExpirationInHours = jwtOptions[nameof(JwtIssuerOptions.WebExpirationInHours)].ParseInt();
                    options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                });

            services.Configure<AppSettings>(
                options =>
                {
                    options.DefaultLanguage = appSettings[nameof(AppSettings.DefaultLanguage)];
                    options.LockoutTimeMinutes = appSettings[nameof(AppSettings.LockoutTimeMinutes)].ParseInt();
                });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,

                // When receiving a token, check that it is still valid.
                ValidateLifetime = true,

                // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time
                // when validating the lifetime. As we're creating the tokens locally and validating them on the same
                // machines which should have synchronised time, this can be set to zero. Where external tokens are
                // used, some leeway here could be useful.
                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(
                    configureOptions =>
                    {
                        configureOptions.ClaimsIssuer = jwtOptions[nameof(JwtIssuerOptions.Issuer)];
                        configureOptions.TokenValidationParameters = tokenValidationParameters;
                        configureOptions.SaveToken = true;
                    });


            // Configure Identity
            services
                .AddIdentity<DbUser, DbRole>()
                .AddEntityFrameworkStores<CliniciaDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityCore<DbDoctor>()
                .AddRoles<DbRole>()
                .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<DbDoctor, IdentityRole>>()
                .AddEntityFrameworkStores<CliniciaDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityCore<DbPatient>()
              .AddRoles<DbRole>()
              .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<DbPatient, IdentityRole>>()
              .AddEntityFrameworkStores<CliniciaDbContext>()
              .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(appSettings[nameof(AppSettings.LockoutTimeMinutes)].ParseInt());
                options.Lockout.MaxFailedAccessAttempts = 5;

                //Sign in settings  
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
        }
    }
}