using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaxcomAuth;
using PaxcomAuth.Models;

namespace WebAPI
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddPaxcomAuthorization(GetPaxcomAuth_Configurations());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private PaxcomAuth_Configurations GetPaxcomAuth_Configurations()
        {
            List<string> applicationActivities = new List<string>();
            applicationActivities.Add("View Paxcom Data");

            return new PaxcomAuth_Configurations()
            {
                ApplicationActivities = applicationActivities,
                ApplicationId = 1000,
                Application_IdentityScope = "sapp",
                AuthorizationServer = "https://qainoutauthapi.azurewebsites.net/",
                Auth_IdentityScope = "authorization",
                IdentityAuthority = "https://qainoutidentity.azurewebsites.net/",
                IdentityClientId = "api",
                IdentityClientSecret = "secret",
                IdentityRequiresHttps = false,      //keep this true for production
                ORGM_IdentityScope = "organization"
            };
        }
    }
}
