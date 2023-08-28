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
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                    new Category { Id = 1, Name = "Action" },
                    new Category { Id = 2, Name = "Adventure" },
                    new Category { Id = 3, Name = "Comedy" },
                    new Category { Id = 4, Name = "Fantasy" },
                    new Category { Id = 5, Name = "Horror" },
                    new Category { Id = 6, Name = "Mystery" },
                    new Category { Id = 7, Name = "Romance" }
                );
        }
    }
}
