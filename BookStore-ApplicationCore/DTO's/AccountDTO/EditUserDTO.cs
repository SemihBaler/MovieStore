using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStore_ApplicationCore.Entities.Concrete;

namespace MovieStore_ApplicationCore.DTO_s.AccountDTO
{
    public class EditUserDTO
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur!!")]
        [DisplayName("Kullanıcı Adı")]
        [MinLength(3, ErrorMessage = "En az 3 karakter girmelisiniz!!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-Mail zorunludur!!")]
        [DisplayName("E-Mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Emailinizi doğru formatta giriniz!!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur!!")]
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public EditUserDTO()
        {
            
        }

        public EditUserDTO(ApplicationUser user)
        {
            UserName = user.UserName;
            Password = user.PasswordHash;
            Email = user.Email;
        }
    }
}
