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
    public class MovieSeedData : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasData
                (
                    new Movie { Id = 1,  DirectorId = 1, Name = "Full Metal Jacket", Year = 1987, Description = "A pragmatic U.S. Marine observes the dehumanizing effects the Vietnam War has on his fellow recruits from their brutal boot camp training to the bloody street fighting in Hue.", Image = "noimage.png" },
                    new Movie { Id = 2, DirectorId = 1, Name = "The Shining", Year = 1980, Description = "A family heads to an isolated hotel for the winter where a sinister presence influences the father into violence, while his psychic son sees horrific forebodings from both past and future", Image = "noimage.png" },
                    new Movie { Id = 3, DirectorId = 2, Name = "The Pianist", Year = 2002, Description = "A Polish Jewish musician struggles to survive the destruction of the Warsaw ghetto of World War II", Image = "noimage.png" },
                    new Movie { Id = 4, DirectorId = 2, Name = "An Offices and Spy", Year = 2019, Description = "In 1894, French Captain Alfred Dreyfus is wrongfully convicted of treason and sentenced to life imprisonment at Devil's island", Image = "noimage.png" },
                    new Movie { Id = 5, DirectorId = 3, Name = "Once Upon a Time in Hollywood", Year = 2019, Description = "A faded television actor and his stunt double strive to achieve fame and success in the final years of Hollywood's Golden Age in 1969 Los Angeles", Image = "noimage.png" },
                    new Movie { Id = 6, DirectorId = 3, Name = "Kill Bill Volume 1", Year = 2003, Description = "After awakening from a four-year coma, a former assassin wreaks vengeance on the team of assassins who betrayed her.", Image = "noimage.png" },
                    new Movie { Id = 7, DirectorId = 4, Name = "The Matinee Idol", Year = 1928, Description = "A Broadway matinee idol famous for his black-face portrayals anonymously joins an amateur acting troupe and falls in love with the leading lady", Image = "noimage.png" },
                    new Movie { Id = 8, DirectorId = 4, Name = "The Power Of The Press", Year = 1928, Description = "Naive newspaper cub Clem lands a scoop when he's sent out to cover a murder. In his enthusiasm he writes that the main suspect is Jane. When she confronts Clem she convinces him to help her prove her innocence", Image = "noimage.png" },
                    new Movie { Id = 9, DirectorId = 5, Name = "The Curios Case of Benjamin Button", Year = 2008, Description = "Tells the story of Benjamin Button, a man who starts aging backwards with consequences", Image = "noimage.png" },
                    new Movie { Id = 10, DirectorId = 5, Name = "The Social Network", Year = 2010, Description = "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea and by the co-founder who was later squeezed out of the business", Image = "noimage.png" }

                );
        }
    }
}
