using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PhoneInventoryManagement.Providers;
using System;
using System.Web.Http;

namespace IdentitySample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                AllowInsecureHttp = true,
                Provider = new CustomOauthAuthorizationServerProvider()
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            ConfigureAuth(app);
        }
    }
}
