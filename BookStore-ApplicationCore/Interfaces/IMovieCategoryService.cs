using MovieStore_ApplicationCore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.Interfaces
{
    public interface IMovieCategoryService
    {
        List<string> GetCategoryNamesByMovieId(int movieId);
        Task AddMovieCategory(MovieCategory movieCategory);
        Task<bool> AnyMovieCategory(int movieId, int categoryId);
        Task DeleteMovieCategory(int movieId, int categoryId);
    }
}
