using SalesManagerSolution.Core.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SalesManagerSolution.Core.Constants;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Authentications;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.HttpClient.System.User
{
	public class UserHttpClient : IUserHttpClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserHttpClient(IHttpClientFactory httpClientFactory,
				   IHttpContextAccessor httpContextAccessor,
					IConfiguration configuration)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_httpClientFactory = httpClientFactory;
		}
		public async Task<ApiResult<string>> Authencate(LoginRequestViewModel request)
		{
			var json = JsonConvert.SerializeObject(request);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
			var response = await client.PostAsync("/api/Authentications/Login", httpContent);

			if (response.IsSuccessStatusCode)
			{
				var token =  JsonConvert.DeserializeObject<AuthenticationResponseViewModel>(await response.Content.ReadAsStringAsync());

				return new ApiSuccessResult<string>(token.Token);
			}

			return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync()) ?? new ApiErrorResult<string>(string.Empty);
		}
	}
}
