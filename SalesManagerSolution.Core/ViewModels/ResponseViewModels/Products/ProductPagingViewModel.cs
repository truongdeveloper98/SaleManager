using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.Core.ViewModels.ResponseViewModels.Products
{
	public class ProductPagingViewModel : PagingRequestBase
	{
		public string Keyword { get; set; } = default!;

		public int? CategoryId { get; set; }
	}
}
