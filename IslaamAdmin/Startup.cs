using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IslaamAdmin
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvcCore();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
               options =>
               {
                   options.AccessDeniedPath = new PathString("/AppData/Index");
                   options.AccessDeniedPath = new PathString("/Account/Index");
                   options.LoginPath = new PathString("/Account/Login");
               }
           );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // if (env.IsDevelopment())
            //  {
            app.UseDeveloperExceptionPage();
            //  }
            app.UseAuthentication();
            app.UseMvc(
                  routes =>
                  {
                      routes.MapRoute("default", "{controller=Account}/{action=Login}/{id?}");
                  }
              );
            app.UseStaticFiles();
        }
    }
}
