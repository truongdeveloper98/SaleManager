using SalesManagerSolution.Core.ViewModels.Common;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Carts;
using SalesManagerSolution.Core.ViewModels.RequestViewModels.Categories;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Carts;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.Core.Interfaces.Services.Carts
{
	public interface ICartService
	{
		Task<PagedResult<CartViewModel>> GetAll();

		Task<CartViewModel> GetById(int id);

		Task<int> Create(CartResquestViewModel request);

		Task<int> Update(CartResquestViewModel request);

		Task<int> Delete(DeleteCartRequest request);
	}
}
