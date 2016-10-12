using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using LasMargaritas.WebAPI.Providers;
using System.Web.Http;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(LasMargaritas.WebAPI.Startup))]
namespace LasMargaritas.WebAPI
{ 
    public class Startup
	{
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration(); 
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}