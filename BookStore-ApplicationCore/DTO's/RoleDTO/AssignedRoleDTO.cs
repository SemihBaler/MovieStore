using MovieStore_ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore_ApplicationCore.DTO_s.RoleDTO
{
    public class AssignedRoleDTO
    {
        public IdentityRole Role { get; set; }

        public IEnumerable<ApplicationUser>? HasRole { get; set; }
        public IEnumerable<ApplicationUser>? HasNotRole { get; set; }

        public string RoleName { get; set; }

        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }
    }
}
