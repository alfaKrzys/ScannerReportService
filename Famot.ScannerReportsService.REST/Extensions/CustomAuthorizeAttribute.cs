using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Famot.ScannerReportsService.REST.Extensions
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimType;
        private readonly string _claimValue;

        public CustomAuthorizeAttribute(string type, string value)
        {
            _claimType = type;
            _claimValue = value;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if (user.HasClaim(_claimType, _claimValue))
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
                actionContext.Response.StatusCode = System.Net.HttpStatusCode.Forbidden;
            }
        }
    }
}