using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(Famot.ScannerReportsService.REST.Startup))]
namespace Famot.ScannerReportsService.REST
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // accept access tokens from identityserver and require a scope of 'api1'
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = ConfigurationManager.AppSettings["StsServer"],
                ValidationMode = ValidationMode.ValidationEndpoint,

                RequiredScopes = new[] { ConfigurationManager.AppSettings["Scope"] }
            });

            // configure web api
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // require authentication for all controllers
            config.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);
        }
    }
}