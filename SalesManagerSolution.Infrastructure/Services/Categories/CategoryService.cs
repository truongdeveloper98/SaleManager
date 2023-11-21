using Microsoft.EntityFrameworkCore;
using SalesManagerSolution.Core.Interfaces.Services.Categories;
using SalesManagerSolution.Core.ViewModels.ResponseViewModels.Categories;
using SalesManagerSolution.Domain.Entities;
using SalesManagerSolution.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagerSolution.Infrastructure.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var query = from c in _context.Categories
                        select new { c};
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();
        }


        public async Task<CategoryViewModel> GetById(int id)
        {
            var query = from c in _context.Categories
                        where  c.Id == id
                        select new { c};
            var result = await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();

            return result ?? new CategoryViewModel();
        }

    }
}
