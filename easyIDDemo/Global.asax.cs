using System;
using System.IdentityModel.Services;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace easyIDDemo
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IdentityConfig.ConfigureIdentity();
        }

        void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs e)
        {
            if (!String.IsNullOrEmpty(IdentityConfig.Realm))
            {
                e.SignInRequestMessage.Realm = IdentityConfig.Realm;
                var authMethod = "";
                if (HttpContext.Current.Request.Url.Query != null)
                {
                    var qs = HttpUtility.ParseQueryString(HttpContext.Current.Request.Url.Query);
                    if (qs["authMethod"] != null)
                    {
                        authMethod = qs["authMethod"];
                    }
                }
                e.SignInRequestMessage.Reply = 
                    new Uri(
                        HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority),
                        UriKind.Absolute).AbsoluteUri;
                if (authMethod == "nobid-mobile")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:no:bankid:mobile";
                    e.SignInRequestMessage.HomeRealm = "nobid-mobile";
                }
                else if (authMethod == "nobid-central")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:no:bankid:central";
                    //e.SignInRequestMessage.HomeRealm = "nobid-central";
                }
                else if (authMethod == "sbid")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:se:bankid:another-device";
                    e.SignInRequestMessage.HomeRealm = "sbid";
                }
                else if (authMethod == "sbid-local")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:se:bankid:same-device";
                    e.SignInRequestMessage.HomeRealm = "sbid-local";
                }
                else if (authMethod == "nemid")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:dk:nemid-poces";
                    e.SignInRequestMessage.HomeRealm = "nemid-poces";
                }
                else if (authMethod == "nemid-moces")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:dk:nemid-moces";
                    e.SignInRequestMessage.HomeRealm = "nemid-moces";
                }
                else if (authMethod == "oces")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:dk:nemid-oces";
                    e.SignInRequestMessage.HomeRealm = "nemid-oces";
                }
            }
        }
    }
}