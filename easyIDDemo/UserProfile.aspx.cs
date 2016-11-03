using System;
using System.IdentityModel.Services;
using System.Web.UI;

namespace easyIDDemo
{
    public partial class UserProfile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            var ub = new UriBuilder(this.Request.Url);
            ub.Path = "/";
            ub.Query = "";
            ub.Fragment = "";
            WSFederationAuthenticationModule.FederatedSignOut(
                new Uri(FederatedAuthentication.FederationConfiguration.WsFederationConfiguration.Issuer, UriKind.Absolute), 
                ub.Uri);
        }
    }
}