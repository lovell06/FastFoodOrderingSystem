using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodOrderingSystem.Api.Configurations;

public sealed class JwtBearerConfigureOptions(IOptions<JwtOption> options) : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOption _jwtOption = options.Value;
    
    public void Configure(JwtBearerOptions options)
    {
        Configure(Options.DefaultName, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme)
            return;
        
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtOption.Issuer,

            ValidateAudience = true,
            ValidAudience = _jwtOption.Audience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtOption.Key))
        };

        options.Events = new JwtBearerEvents()
        {
            OnChallenge = context =>
            {
                var logger =
                    context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerConfigureOptions>>();
                
                logger.LogWarning(
                    "Unauthorized. Error={0}. Description={1}",
                    context.Error,
                    context.ErrorDescription);
                
                return Task.CompletedTask;
            },
            
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerConfigureOptions>>();
                logger.LogWarning(context.Exception, "JWT validation failed.");
                return Task.CompletedTask;
            },

            OnForbidden = async context =>
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Permission denied."
                });
            }
        };
    }
}