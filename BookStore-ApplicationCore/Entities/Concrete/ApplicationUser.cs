using MovieStore_ApplicationCore.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.Entities.Concrete
{
    public class ApplicationUser : IdentityUser
    {
        private DateTime _createdDate = DateTime.Now;
        public DateTime CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value;
        }

        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        private Status _status = Status.Active;
        public Status Status
        {
            get => _status;
            set => _status = value;
        }
    }
}
