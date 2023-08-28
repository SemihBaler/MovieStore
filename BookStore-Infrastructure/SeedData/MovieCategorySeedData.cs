using MovieStore_ApplicationCore.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_Infrastructure.SeedData
{
    public class MovieCategorySeedData : IEntityTypeConfiguration<MovieCategory>
    {
        public void Configure(EntityTypeBuilder<MovieCategory> builder)
        {
            builder.HasData
                (
                    new MovieCategory { MovieId = 1, CategoryId = 1 },
                    new MovieCategory { MovieId = 1, CategoryId = 2 },
                    new MovieCategory { MovieId = 1, CategoryId = 3 },
                    new MovieCategory { MovieId = 2, CategoryId = 5 },
                    new MovieCategory { MovieId = 2, CategoryId = 2 },
                    new MovieCategory { MovieId = 2, CategoryId = 3 },
                    new MovieCategory { MovieId = 3, CategoryId = 1 },
                    new MovieCategory { MovieId = 3, CategoryId = 6 },
                    new MovieCategory { MovieId = 3, CategoryId = 7 },
                    new MovieCategory { MovieId = 4, CategoryId = 5 },
                    new MovieCategory { MovieId = 4, CategoryId = 1 },
                    new MovieCategory { MovieId = 4, CategoryId = 3 },
                    new MovieCategory { MovieId = 5, CategoryId = 4 },
                    new MovieCategory { MovieId = 5, CategoryId = 5 },
                    new MovieCategory { MovieId = 6, CategoryId = 1 },
                    new MovieCategory { MovieId = 6, CategoryId = 3 },
                    new MovieCategory { MovieId = 6, CategoryId = 5 },
                    new MovieCategory { MovieId = 7, CategoryId = 5 },
                    new MovieCategory { MovieId = 7, CategoryId = 6 },
                    new MovieCategory { MovieId = 8, CategoryId = 7 },
                    new MovieCategory { MovieId = 8, CategoryId = 1 },
                    new MovieCategory { MovieId = 9, CategoryId = 2 },
                    new MovieCategory { MovieId = 9, CategoryId = 3 },
                    new MovieCategory { MovieId = 10, CategoryId = 2 },
                    new MovieCategory { MovieId = 10, CategoryId = 5 }
                );
        }
    }
}
