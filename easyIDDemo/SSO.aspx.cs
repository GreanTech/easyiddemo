using System;
using System.IdentityModel.Services;

namespace easyIDDemo
{
    public partial class SSO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                FederatedAuthentication.WSFederationAuthenticationModule.SignOut(true);
                var ssoState = new EstablishSsoSessionState();
                if (!ssoState.IsEnabled(this.Request))
                {
                    Response.Redirect("/");
                }
            }
            else
            {
                Response.Redirect("/");
            }
        }
    }
}