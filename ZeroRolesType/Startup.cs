using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroRolesType.Data;
using ZeroRolesType.Models;

namespace ZeroRolesType
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


            CreateBuiltIndata(serviceProvider).Wait();
        }

        private async Task CreateBuiltIndata(IServiceProvider serviceProvider)
        {
            try
            {
                var _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                _context.Database.EnsureCreated();

                // Default Roletypes Add
                if (!_context.RoleType.Any())
                {
                    _context.RoleType.Add(new RoleType { Name = "Director", Active = true });
                    _context.RoleType.Add(new RoleType { Name = "Manager", Active = true });
                    _context.RoleType.Add(new RoleType { Name = "Supervisor", Active = true });
                    _context.RoleType.Add(new RoleType { Name = "Agent", Active = true });

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
