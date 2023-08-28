using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore_ApplicationCore.Entities.Concrete;

namespace MovieStore_ApplicationCore.DTO_s.MovieDTO
{
    public class UpdateMovieDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!!")]
        [MaxLength(600, ErrorMessage = "Maksimum karakter sınırını geçtiniz!!")]
        [DisplayName("Film Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!!")]
        [MaxLength(800, ErrorMessage = "Maksimum karakter sınırını geçtiniz!!")]
        [DisplayName("Film Açıklaması")]
        public string Description { get; set; }

        [DisplayName("Yapım Yılı")]
        [Required(ErrorMessage = "Yapım yılı zorunludur!!")]
        public int Year { get; set; }

        [DisplayName("Yönetmen")]
        public int DirectorId { get; set; }

        public string? Image { get; set; }
        public IFormFile? UploadImage { get; set; }

        public List<Category>? HasCategories { get; set; }
        public List<Category>? HasNotCategories { get; set; }


        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }
    }
}
