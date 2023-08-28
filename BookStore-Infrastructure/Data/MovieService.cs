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
    public class MovieService : EfRepository<Movie>, IMovieService
    {
        public MovieService(AppDbContext context) : base(context)
        {
        }
    }
}
