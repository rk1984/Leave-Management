using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC.LeaveManagement.API.Configuration;
using ABC.LeaveManagement.API.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ABC.LeaveManagement.Data;
using ABC.LeaveManagement.Core.Entities;
using ABC.LeaveManagement.Core.Data;
using ABC.LeaveManagement.Data.Repository;
using ABC.LeaveManagement.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger.Model;
using Swashbuckle.Swagger;
using ABC.LeaveManagement.Data.IdentityModel;

namespace ABC.LeaveManagement.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<LeaveManagementDbContext>();

            services.AddIdentity<LeaveManagementUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                cfg.Cookies.ApplicationCookie.LoginPath = "/auth/login";
                cfg.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                          ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            })
            .AddEntityFrameworkStores<LeaveManagementDbContext>();


            services.AddScoped<IRepository<AbsenceRequest>, Repository<AbsenceRequest>>();
            services.AddScoped<IRepository<Employee>, Repository<Employee>>();
            services.AddScoped<IAbsenceRequestService, AbsenceRequestService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddTransient<LeaveManagementDbContextSeed>();

            // Add framework services.
            services.AddMvc();

            /*Adding swagger generation with default settings*/
            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info()
                {
                    Version = "v1",
                    Title = "ABC Leave Management API",
                    Description = "ABC Leave Management API Description",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Rexhep Kqiku", Email = "", Url = "http://github.com/rk1984" },
                    License = new License { Name = "Use under...", Url = "http://url.com" }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LeaveManagementDbContextSeed leaveManagementDbContextSeed)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            /*Enabling swagger file*/
            app.UseSwagger();
            /*Enabling Swagger ui, consider doing it on Development env only*/
            app.UseSwaggerUi();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                DisplayName = "Google",

                ClientId = "724404535545-tnlsb2qk4995ri16l238dke5hqnl2del.apps.googleusercontent.com",
                ClientSecret = "0cToDFhE7fXzWeizdXjA8WJP"
            });

            /*Normal MVC mappings*/
            app.UseMvc();

            if (_env.IsDevelopment())
                leaveManagementDbContextSeed.SeedData().Wait();
        }
    }
}
