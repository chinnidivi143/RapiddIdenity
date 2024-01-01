using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

namespace RapiddIdenity
{
    public static class DependecyInjectionExtenstion
    {
        //public static IConfiguration _configuration;
        //static DependecyInjectionExtenstion(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public static IServiceCollection RegisterAll(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(DataAccessHelper.GetConnectionString("DevConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options => options.SignIn.RequireConfirmedEmail = true);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            return services;
        }
    }
}
