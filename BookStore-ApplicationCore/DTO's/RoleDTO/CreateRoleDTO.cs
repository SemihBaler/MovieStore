using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.DTO_s.RoleDTO
{
    public class CreateRoleDTO
    {
        [Required(ErrorMessage = "Lütfen rol adı giriniz!!")]
        [MinLength(3, ErrorMessage = "En az 3 karakter giriniz!")]
        public string Name { get; set; }
    }
}
