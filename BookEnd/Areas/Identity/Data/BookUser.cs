using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookEnd.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BookUser class
    public class BookUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDate { get; set; }
        public virtual List<ApplicationRoleUser> RoleUsers { get; set; }
    }
}
