using AutoMapper;
using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_ApplicationCore.Interfaces;
using MovieStore_Infrastructure.AutoMapper;
using MovieStore_Infrastructure.Context;
using MovieStore_Infrastructure.Context.Identity;
using MovieStore_Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MovieStore_WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var mappingConfig = new MapperConfiguration(mc => 
            {
                mc.AddProfile(new Mapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectionStringIdentity = builder.Configuration.GetConnectionString("IdentityConnection");

            builder.Services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDbContext<StoreIdentityDbContext>(options => 
            {
                options.UseSqlServer(connectionStringIdentity);
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(x => 
            {
                x.SignIn.RequireConfirmedPhoneNumber = false;
                x.SignIn.RequireConfirmedEmail = false;
                x.SignIn.RequireConfirmedAccount = false;

                x.User.RequireUniqueEmail = true;

                x.Password.RequiredLength = 1;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<StoreIdentityDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(x => x.LoginPath = "/Accounts/LogIn");

            builder.Services.AddScoped(typeof(IRepositoryService<>), typeof(EfRepository<>));
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IDirectorService, DirectorService>();
            builder.Services.AddScoped<IMovieCategoryService, MovieCategoryService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}