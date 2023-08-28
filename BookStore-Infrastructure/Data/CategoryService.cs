using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_ApplicationCore.Interfaces;
using MovieStore_Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_Infrastructure.Data
{
    public class CategoryService : EfRepository<Category>, ICategoryService
    {
        public CategoryService(AppDbContext context) : base(context)
        {
        }
    }
}
