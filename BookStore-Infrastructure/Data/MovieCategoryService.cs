using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_ApplicationCore.Interfaces;
using MovieStore_Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_Infrastructure.Data
{
    public class MovieCategoryService : IMovieCategoryService
    {
        private readonly AppDbContext _context;

        public MovieCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddMovieCategory(MovieCategory movieCategory)
        {
            await _context.MovieCategories.AddAsync(movieCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyMovieCategory(int movieId, int categoryId)
        {
            return await _context.MovieCategories.AnyAsync(x => x.MovieId == movieId && x.CategoryId == categoryId);
        }

        public async Task DeleteMovieCategory(int movieId, int categoryId)
        {
            var movieCategory = await _context.MovieCategories.FirstOrDefaultAsync(x => x.MovieId == movieId && x.CategoryId == categoryId);
            _context.MovieCategories.Remove(movieCategory);
            await _context.SaveChangesAsync();
        }

        public List<string> GetCategoryNamesByMovieId(int movieId)
        {
            var movieCategories = _context.MovieCategories.Where(x => x.MovieId == movieId).ToList();
            List<string> categoryNames = new List<string>();

            foreach (var movieCategory in movieCategories)
            {
                var category = _context.Categories.Find(movieCategory.CategoryId);
                categoryNames.Add(category.Name);
            }

            return categoryNames;

        }
    }
}
