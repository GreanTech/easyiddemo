using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;

namespace easyIDDemo
{
    public class WIFAntiXssModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += OnPostAuthenticateRequest;
        }

        void OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current == null
                || HttpContext.Current.Request == null
                || HttpContext.Current.User == null
                || HttpContext.Current.User.Identity == null)
                return;

            // Accept POST with a wresult parameter for unauthenticated users only, to avoid XSS attacks on that parameter.
            var request = HttpContext.Current.Request;
            if (request.Params["wresult"] == null)
                // No wresult parameter -> we're done, Netscaler handles XSS protection for all other params
                return;

            if (!HttpContext.Current.User.Identity.IsAuthenticated
                && string.Compare(request.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase) == 0)
                // User is anonymous, and this is a POST request: 
                // Let WIF handle the token validation, and fail if the data is malformed somehow.
                return;

            var response = HttpContext.Current.Response;
            // Stop processing as fast as possible. 
            response.ClearContent();
            try
            {
                response.ClearHeaders();
            }
            catch (HttpException)
            {
                // There is no public API method for determining if headers has been written.
                // As this module is potentially hooked in after that has happened, we can only try
                // to minimize the data sent to a malicious client.
            }
            // Take same approach as Netscaler, except that ASP.NET does not seem to want to issue a 'connection' response header
            response.StatusCode = (int)HttpStatusCode.MovedPermanently;
            response.RedirectLocation = "/";
            response.CacheControl = "no-cache";
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
