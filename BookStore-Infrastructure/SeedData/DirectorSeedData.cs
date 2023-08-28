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
    public class DirectorSeedData : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasData
                (
                    new Director { Id = 1, FirstName = "Stanley", LastName = "Kubrick", BirthDate = new DateTime(1928, 07, 26) },
                    new Director { Id = 2, FirstName = "Roman", LastName = "Polanski", BirthDate = new DateTime(1933, 08, 19) },
                    new Director { Id = 3, FirstName = "Quentine Jerome", LastName = "Tarantino", BirthDate = new DateTime(1963, 03, 27) },
                    new Director { Id = 4, FirstName = "Frank", LastName = "Capra", BirthDate = new DateTime(1897, 05, 17) },
                    new Director { Id = 5, FirstName = "David Leo", LastName = "Fincher", BirthDate = new DateTime(1962, 08, 28) }

                );
        }
    }
}
