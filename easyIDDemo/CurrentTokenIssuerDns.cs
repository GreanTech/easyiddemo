using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace easyIDDemo
{
    public static class CurrentTokenIssuerDns
    {
        public static string Find(ClaimsIdentity identity)
        {
            var bootstrapContext = identity.BootstrapContext as BootstrapContext;
            if (bootstrapContext != null)
            {
                var bootstrapToken = bootstrapContext.SecurityToken as Saml2SecurityToken;
                if (bootstrapToken != null && bootstrapToken.IssuerToken != null)
                {
                    var issuerDns =
                        FederatedAuthentication.FederationConfiguration.IdentityConfiguration.IssuerNameRegistry
                            .GetIssuerName(bootstrapToken.IssuerToken);
                    return issuerDns;
                }
            }

            return null;
        }
    }
}