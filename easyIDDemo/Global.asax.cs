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

        private void SetIdPHost(string newHostName, SignInRequestMessage m)
        {
            var origBaseUri = new UriBuilder(m.BaseUri);
            origBaseUri.Host = newHostName;
            m.BaseUri = origBaseUri.Uri;
        }

        void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs e)
        {
            var host = HttpContext.Current.Request.Url.Host;
            if (host == "www.grean.id")
            {
                SetIdPHost("easyid.www.grean.id", e.SignInRequestMessage);
            }

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
                else if (authMethod == "dknemid-poces")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:dk:nemid:poces";
                    e.SignInRequestMessage.HomeRealm = "dknemid-poces";
                }
                else if (authMethod == "dknemid-moces")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:dk:nemid:moces";
                    e.SignInRequestMessage.HomeRealm = "dknemid-moces";
                }
                else if (authMethod == "dknemid-moces-codefile")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:dk:nemid:moces:codefile";
                    e.SignInRequestMessage.HomeRealm = "dknemid-moces-codefile";
                }
                else if (authMethod == "fi-tupas")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:fi:tupas";
                    e.SignInRequestMessage.HomeRealm = "fi-tupas";
                }
                else if (authMethod == "fi-mobile-id")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:fi:mobile-id";
                    e.SignInRequestMessage.HomeRealm = "fi-mobile-id";
                }
                else if (authMethod == "fi-all")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:fi:all";
                    e.SignInRequestMessage.HomeRealm = "fi-all";
                }
                else if (authMethod == "no-vipps")
                {
                    e.SignInRequestMessage.AuthenticationType = "urn:grn:authn:no:vipps";
                }
            }
        }
    }
}