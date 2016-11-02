using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace easyIDDemo
{
    public partial class Contact : Page
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