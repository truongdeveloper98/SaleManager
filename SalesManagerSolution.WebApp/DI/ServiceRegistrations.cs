using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesManagerSolution.Core.Constants;
using SalesManagerSolution.Core.Interfaces.Authentications;
using SalesManagerSolution.Core.Services.Authentications;
using SalesManagerSolution.Domain.Entities;
using SalesManagerSolution.HttpClient;
using SalesManagerSolution.Infrastructure.EntityFramework;
using SalesManagerSolution.Infrastructure.Services.Authentications;

namespace SalesManagerSolution.WebApp.DI
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

			services.AddHttpClient();
			services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionsName));

			services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

			services.AddScoped<IAuthenticationService, AuthenticationService>();

			services.AddScoped<IProductApiClient,ProductApiClient>();
			services.AddScoped<ICategoryApiClient, CategoryApiClient>();
		}
	}
}
