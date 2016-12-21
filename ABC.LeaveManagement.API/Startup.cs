using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json.Linq;

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
            })
            .AddEntityFrameworkStores<LeaveManagementDbContext>()
            .AddDefaultTokenProviders();


            services.AddScoped<IRepository<AbsenceRequest>, Repository<AbsenceRequest>>();
            services.AddScoped<IRepository<Employee>, Repository<Employee>>();
            services.AddScoped<IAbsenceRequestService, AbsenceRequestService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddTransient<LeaveManagementDbContextSeed>();

            // Add authentication services
            services.AddAuthentication(
                options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

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
                LoginPath = "/api/auth/externallogin",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {                
                AuthenticationScheme = "Google",
                DisplayName = "Google",

                ClientId = "724404535545-tnlsb2qk4995ri16l238dke5hqnl2del.apps.googleusercontent.com",
                ClientSecret = "0cToDFhE7fXzWeizdXjA8WJP",

                Scope = { "email", "openid" },

                Events = new OAuthEvents()
                {
                    // The OnCreatingTicket event is called after the user has been authenticated and the OAuth middleware has
                    // created an auth ticket. We need to manually call the UserInformationEndpoint to retrieve the user's information,
                    // parse the resulting JSON to extract the relevant information, and add the correct claims.
                    OnCreatingTicket = async context =>
                    {
                        // Retrieve user info
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                        request.Headers.Add("x-li-format", "json");
                            // Tell LinkedIn we want the result in JSON, otherwise it will return XML

                        var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        // Extract the user info object
                        var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                        // Add the Name Identifier claim
                        var userId = user.Value<string>("id");
                        if (!string.IsNullOrEmpty(userId))
                        {
                            context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId,
                                ClaimValueTypes.String, context.Options.ClaimsIssuer));
                        }

                        // Add the Name claim
                        var formattedName = user.Value<string>("formattedName");
                        if (!string.IsNullOrEmpty(formattedName))
                        {
                            context.Identity.AddClaim(new Claim(ClaimTypes.Name, formattedName, ClaimValueTypes.String,
                                context.Options.ClaimsIssuer));
                        }

                        // Add the email address claim
                        var email = user.Value<string>("emailAddress");
                        if (!string.IsNullOrEmpty(email))
                        {
                            context.Identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String,
                                context.Options.ClaimsIssuer));
                        }

                        // Add the Profile Picture claim
                        var pictureUrl = user.Value<string>("pictureUrl");
                        if (!string.IsNullOrEmpty(email))
                        {
                            context.Identity.AddClaim(new Claim("profile-picture", pictureUrl, ClaimValueTypes.String,
                                context.Options.ClaimsIssuer));
                        }
                    }
                }
            });

            app.Map("/api/auth/externallogin", builder =>
            {
                builder.Run(async context =>
                {
                    // Return a challenge to invoke the LinkedIn authentication scheme
                    await
                        context.Authentication.ChallengeAsync("Google",
                            properties: new AuthenticationProperties() {RedirectUri = "/api/absencerequest/"});
                });
            });

            /*Normal MVC mappings*/
            app.UseMvc();

            if (_env.IsDevelopment())
                leaveManagementDbContextSeed.SeedData().Wait();
        }
    }
}
