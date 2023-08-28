using MovieStore_ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_Infrastructure.Context.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole
                    {
                        Id = "d575f12c-a0d3-4ead-b94c-c0ee62ef7652",
                        Name = "Admin",
                        NormalizedName = "ADMIN".ToUpper()
                    }
                );

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData
                (
                    new ApplicationUser
                    {
                        Id = "9588b738-b7c2-4dab-96f7-5ccedde5be23",
                        UserName = "test",
                        NormalizedUserName = "TEST",
                        Email = "test@test.com",
                        PasswordHash = hasher.HashPassword(null, "1234")
                    }
                );

            builder.Entity<IdentityUserRole<string>>().HasData
                (
                    new IdentityUserRole<string>
                    {
                        RoleId = "d575f12c-a0d3-4ead-b94c-c0ee62ef7652",
                        UserId = "9588b738-b7c2-4dab-96f7-5ccedde5be23"
                    }
                );

        }
    }
}
