using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.DTO_s.CategoryDTO
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Kategori adı zorunludur!!")]
        [MaxLength(100, ErrorMessage = "Maksimum karakter sayısını aştınız!!!")]
        [DisplayName("Kategori Adı")]
        public string Name { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
    }
}
