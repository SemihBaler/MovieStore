using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Proje çalıştığında sizin yerinize update-database komutunu çalıştırır.
            Database.Migrate();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCategory>().HasKey(x => new 
            {
                x.MovieId, x.CategoryId
            });

            modelBuilder.ApplyConfiguration(new DirectorSeedData());
            modelBuilder.ApplyConfiguration(new MovieSeedData());
            modelBuilder.ApplyConfiguration(new CategorySeedData());
            modelBuilder.ApplyConfiguration(new MovieCategorySeedData());


            base.OnModelCreating(modelBuilder);
        }
    }
}
