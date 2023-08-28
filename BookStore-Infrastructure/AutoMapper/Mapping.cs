using AutoMapper;
using MovieStore_ApplicationCore.DTO_s.CategoryDTO;
using MovieStore_ApplicationCore.DTO_s.DirectorDTO;
using MovieStore_ApplicationCore.DTO_s.MovieDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_Infrastructure.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, AddCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();

            CreateMap<Director, AddDirectorDTO>().ReverseMap();
            CreateMap<Director, UpdateDirectorDTO>().ReverseMap();

            CreateMap<Movie, AddMovieDTO>().ReverseMap();
            CreateMap<Movie, UpdateMovieDTO>().ReverseMap();
        }
    }
}
