using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace backend.Configuration.Identity.CookieAndJwt;

public static class CookieAndJwtConfiguration
{
    public static IServiceCollection AddCookieAndJwtConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.MetadataAddress = jwtSettings["MetadataAddress"]!;
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.Authority = jwtSettings["Authority"];
                options.Audience = jwtSettings["Audience"];
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Login";
            options.Cookie.Name = "crm_cookie";
            options.Cookie.HttpOnly = true;
            options.LogoutPath = "/Identity/Logout";
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Identity/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.Cookie.Expiration = TimeSpan.FromMinutes(30);
            options.Cookie.MaxAge = TimeSpan.FromMinutes(31);
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        });
        return services;
    }
}