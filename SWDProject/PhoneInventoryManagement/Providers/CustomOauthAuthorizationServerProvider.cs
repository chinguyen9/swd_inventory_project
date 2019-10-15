using IdentitySample.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PhoneInventoryManagement.Providers
{
    public class CustomOauthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private ApplicationUserManager _userManager;


        public CustomOauthAuthorizationServerProvider()
        {
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = await _userManager.FindAsync(context.UserName, context.Password);

            if (user != null)
            {
                ClaimsIdentity claimIdentity = await _userManager.CreateIdentityAsync(user, context.Options.AuthenticationType);

                context.Validated(claimIdentity);
            }
            else
            {
                context.SetError("invalid_grant", "Wrong username or password");
                return;
            }


        }
    }
}