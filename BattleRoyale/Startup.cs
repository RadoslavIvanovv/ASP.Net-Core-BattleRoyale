using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Infrastructure;
using BattleRoyale.Services.AuctionItemServices;
using BattleRoyale.Services.BattleArenaServices;
using BattleRoyale.Services.HeroServices;
using BattleRoyale.Services.HomeServices;
using BattleRoyale.Services.ItemServices;
using BattleRoyale.Services.PetServices;
using BattleRoyale.Services.PlayerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BattleRoyale
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BattleRoyaleDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BattleRoyaleDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddMemoryCache();

            services.AddTransient<IHeroService, HeroService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IBattleArenaService, BattleArenaService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IPetService, PetService>();
            services.AddTransient<IAuctionItemService, AuctionItemService>();

            services.AddScoped<BattleRoyaleDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                //.UseStatusCodePagesWithRedirects("/Home/Error{0}")
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();

                  endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                  endpoints.MapRazorPages();
                });
        }
    }
}
