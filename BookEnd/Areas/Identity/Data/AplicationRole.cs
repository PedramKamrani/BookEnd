using BookEnd.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Models.ViewModels
{
    public class AplicationRole:IdentityRole
    {
        public AplicationRole()
        {

        }
        public AplicationRole(string name)
            :base(name)
        {

        }
        public AplicationRole(string name, string discription)
            :base(name)
        {
            Discription = discription;
        }
        public string Discription { get; set; }
        public virtual List<ApplicationRoleUser> Users { get; set; }
    }
}
