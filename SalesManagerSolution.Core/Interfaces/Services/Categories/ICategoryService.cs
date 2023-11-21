using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Categories;
using SalesManagerSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.Core.Interfaces.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll();

        Task<CategoryViewModel> GetById(int id);
    }
}
