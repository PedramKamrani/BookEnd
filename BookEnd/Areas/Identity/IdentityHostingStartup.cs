using System;
using BookEnd.Areas.Identity.Data;
using BookEnd.Models;
using BookEnd.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BookEnd.Areas.Identity.IdentityHostingStartup))]
namespace BookEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BookEndContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BookEndContextConnection")));

                //services.AddDefaultIdentity<BookUser>()
                //    .AddEntityFrameworkStores<BookEndContext>();
                services.AddIdentity<BookUser, AplicationRole>()
                   //.AddDefaultUI()
                   .AddEntityFrameworkStores<BookEndContext>()
                   .AddDefaultTokenProviders();
                //services.Configure<IdentityOptions>(op =>
                //{
                //    op.SignIn.RequireConfirmedPhoneNumber = true;
                //});
            });
        }
    }
}