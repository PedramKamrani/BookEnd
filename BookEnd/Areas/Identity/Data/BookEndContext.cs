using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookEnd.Areas.Identity.Data;
using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookEnd.Models
{
    public class BookEndContext : IdentityDbContext<BookUser, AplicationRole,string,IdentityUserClaim<string>,ApplicationRoleUser,IdentityUserLogin<string>,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        public BookEndContext(DbContextOptions<BookEndContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AplicationRole>().ToTable("AppRole");
            builder.Entity<ApplicationRoleUser>().ToTable("AppUserRole");
            builder.Entity<ApplicationRoleUser>()
                .HasOne(o => o.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(o => o.RoleId);

            builder.Entity<BookUser>().ToTable("AppUser");
            builder.Entity<ApplicationRoleUser>()
                .HasOne(p => p.BookUser)
                .WithMany(o => o.RoleUsers)
                .HasForeignKey(k => k.UserId);
        }
    }
}
