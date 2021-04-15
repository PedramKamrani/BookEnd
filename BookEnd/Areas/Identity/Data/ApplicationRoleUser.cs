using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEnd.Areas.Identity.Data
{
    public class ApplicationRoleUser:IdentityUserRole<string>
    {
        public virtual AplicationRole Role { get; set; }
        public virtual BookUser BookUser { get; set; }
    }
}
