using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.DTO_s.DirectorDTO
{
    public class UpdateDirectorDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Required(ErrorMessage = "Ad zorunludur!!")]
        [MaxLength(200, ErrorMessage = "Maksimum karakter sınırını geçtiniz!!")]
        [DisplayName("Yönetmen Adı")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur!!")]
        [MaxLength(200, ErrorMessage = "Maksimum karakter sınırını geçtiniz!!")]
        [DisplayName("Yönetmen Soyadı")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Bu alan zorunludur!!")]
        [DisplayName("Doğum Tarihi")]
        public DateTime BirthDate { get; set; }
    }
}
