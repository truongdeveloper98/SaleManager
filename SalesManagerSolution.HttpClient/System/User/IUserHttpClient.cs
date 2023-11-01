using eShopSolution.ViewModels.Common;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.HttpClient.System.User
{
	public interface IUserHttpClient
	{
		Task<ApiResult<string>> Authencate(LoginRequestViewModel request);
	}
}
