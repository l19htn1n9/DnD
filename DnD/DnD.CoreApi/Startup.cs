using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DnD.Models.DatabaseContext;
using DnD.Models.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace DnD.CoreApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            try
            {
				var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
				Configuration = builder.Build();
            }
            catch(Exception e)
            {
                
            }
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
				services.Configure<IdentityOptions>(options =>
				{
					// avoid redirecting REST clients on 401
					options.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents
					{
						OnRedirectToLogin = ctx =>
						{
							ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
							return Task.FromResult(0);
						}
					};
				});
				// Add framework services.
				services.AddMvc();

				string connectionString = "Data Source=localhost;Database=DnD;Uid=DnD;Pwd=Admin123;SslMode=None";
				services.AddDbContext<DndEntities>(options =>
												   options.UseMySQL(connectionString));
				services.AddDbContext<UserDbContext>(options =>
												   options.UseMySQL(connectionString));

                services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>().AddUserManager<AppUserManager>();

				services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
                //services.AddScoped<IPasswordHasher<IdentityUser>, CustomPasswordHasher>();

			}
            catch(Exception e)
            {
                
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseIdentity();

			// secretKey contains a secret passphrase only your server knows
			var secretKey = Configuration.GetSection("JWTSettings:SecretKey").Value;
			var issuer = Configuration.GetSection("JWTSettings:Issuer").Value;
			var audience = Configuration.GetSection("JWTSettings:Audience").Value;
			var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,

				// Validate the JWT Issuer (iss) claim
				ValidateIssuer = true,
				ValidIssuer = issuer,

				// Validate the JWT Audience (aud) claim
				ValidateAudience = true,
				ValidAudience = audience
			};
			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				TokenValidationParameters = tokenValidationParameters
			});

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AutomaticAuthenticate = false,
				AutomaticChallenge = false
			});

            app.UseMvc();

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
		    using (var serviceScope = serviceScopeFactory.CreateScope())
			{
                var dbContext = serviceScope.ServiceProvider.GetService<UserDbContext>();
				dbContext.Database.EnsureCreated();
                var dndDbContext = serviceScope.ServiceProvider.GetService<DndEntities>();
                dndDbContext.Database.EnsureCreated();
			}
        }
    }
}
