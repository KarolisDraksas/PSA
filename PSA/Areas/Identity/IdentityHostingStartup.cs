using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSA.Areas.Identity.Data;
using PSA.Data;

[assembly: HostingStartup(typeof(PSA.Areas.Identity.IdentityHostingStartup))]
namespace PSA.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PSAContextDB>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PSAContextDBConnection")));

                services.AddDefaultIdentity<PSAUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                })
                    .AddEntityFrameworkStores<PSAContextDB>();
            });
        }
    }
}