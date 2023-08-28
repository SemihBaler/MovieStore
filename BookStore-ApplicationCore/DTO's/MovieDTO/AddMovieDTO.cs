using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.DTO_s.MovieDTO
{
    public class AddMovieDTO
    {
        [Required(ErrorMessage = "Bu alan zorunludur!!")]
        [MaxLength(600, ErrorMessage = "Maksimum karakter sınırını geçtiniz!!")]
        [DisplayName("Film Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!!")]
        [MaxLength(800, ErrorMessage = "Maksimum karakter sınırını geçtiniz!!")]
        [DisplayName("Film Açıklaması")]
        public string Description { get; set; }

        [DisplayName("Yapım Yılı")]
        public int Year { get; set; }

        [DisplayName("Yönetmen")]
        public int DirectorId { get; set; }

        public string? Image { get; set; }
        public IFormFile? UploadImage { get; set; }
        public List<string> Categories { get; set; }
    }
}
