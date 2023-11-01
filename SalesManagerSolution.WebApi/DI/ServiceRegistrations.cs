using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesManagerSolution.Core.Constants;
using SalesManagerSolution.Core.Interfaces.Authentications;
using SalesManagerSolution.Core.Services.Authentications;
using SalesManagerSolution.Domain.Entities;
using SalesManagerSolution.Infrastructure.EntityFramework;
using SalesManagerSolution.Infrastructure.Services.Authentications;

namespace SalesManagerSolution.WebApi.DI
{
	public static class ServiceRegistrations
	{

		public static void ConfigureAppServices(this IServiceCollection services, ConfigurationManager configuration, bool IsProduction)
		{
			var connectionString = configuration.GetConnectionString(SystemConstants.MainConnectionString);
			if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Stage")
			{
				services.AddDbContext<ApplicationDbContext>(options =>
							options.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING"), x => x.MigrationsAssembly("SalesManagerSolution.Database")));
			}
			else
			{
				services.AddDbContext<ApplicationDbContext>(options =>
							options.UseSqlServer(connectionString));
			}

			services.AddIdentity<AppUser, AppRole>()
						.AddEntityFrameworkStores<ApplicationDbContext>()
						.AddDefaultTokenProviders();


			//builder.Services DI
			services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionsName));
			services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			
			string issuer = configuration.GetValue<string>("JwtSettings:Issuer") ?? string.Empty;
			string signingKey = configuration.GetValue<string>("JwtSettings:Secret") ?? string.Empty;
			byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = issuer,
					ValidateAudience = true,
					ValidAudience = issuer,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ClockSkew = System.TimeSpan.Zero,
					IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
				};
			});

			services.AddCors(option =>
			{
				option.AddPolicy("_myAllowSpecificOrigins",
					builder =>
					{
						builder.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});


		}
	}
}
