using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeBScraper_CourseProject_.Areas.Identity;
using WeBScraper_CourseProject_.Data;

namespace WeBScraper_CourseProject_.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {            
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

            var connectionString = config.GetConnectionString("DefaultConnection");

            services.AddScoped<IScraperService, ScraperService>();
            services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(connectionString));

            return services;
        }
    }
}
