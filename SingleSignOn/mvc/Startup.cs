namespace mvc
{
    using System;
    using IdentityServer.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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
            // This local cookie is necessary because even though you’ll be using IdentityServer to authenticate the user and create a Single Sign-On (SSO) session, every individual client application will maintain its own, shorter-lived session.
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "cookie";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("cookie")
                .AddOpenIdConnect("oidc", options =>
                {
                    var identityServerAddress = Environment.GetEnvironmentVariable(Constants.EnvAuthServerAddress) ??
                                                Configuration[Constants.EnvAuthServerAddress];

                    if (string.IsNullOrEmpty(identityServerAddress))
                    {
                        // TODO: Use a logger instead
                        throw new InvalidOperationException("Auth server address not specified");
                    }

                    // By default, the ASP.NET Core OpenID Connect handler will use the implicit flow with the form post response mode.
                    // The implicit flow is in the process of being deprecated, and the form post response is becoming unreliable thanks to 3rd party cookies policies being rolled out by browsers.
                    // As a result, you have updated these to use the authorization code flow, PKCE, and the query string response mode.
                    options.Authority = identityServerAddress;
                    options.ClientId = "oidcClient";
                    options.ClientSecret = "SuperSecretPassword";

                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.ResponseMode = "query";

                    // options.CallbackPath = "/signin-oidc"; // default redirect URI

                    options.Scope.Add("oidc");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("role");
                    options.Scope.Add("api1.user.read");
                    options.SaveTokens = true;
                });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}