using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RGMVC.Options;
using System.Collections.Generic;
using System.Text;

namespace RGMVC.Installers
{
	public class MvcInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{

			var jwtSettings = new JwtSettings();
			configuration.Bind(nameof(jwtSettings),jwtSettings);
			
			services.AddSingleton(jwtSettings);

			services.AddMvc().AddMvcOptions(option => option.EnableEndpointRouting = false);
			
			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(token =>
				{
					token.SaveToken = true;
					token.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
						ValidateIssuer = false,
						ValidateAudience = false,
						RequireExpirationTime = false,
						ValidateLifetime = true
					};
				});

			services.AddSwaggerGen(swagger =>
			{
				swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Retro Games API", Version = "v1" });


				var security = new Dictionary<string, IEnumerable<string>>
				{
					{"Bearer", new string[0]}
				};

				swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the bearer scheme",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey	
				});

				
			});
		}
	}
}
