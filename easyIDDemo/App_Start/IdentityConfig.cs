using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace easyIDDemo
{
    // For more information on ASP.NET Identity, please visit http://go.microsoft.com/fwlink/?LinkId=301882
    public static class IdentityConfig 
    {
        public static string AudienceUri { get; private set; }
        public static string Realm { get; private set; }

        public static void ConfigureIdentity()
        {
            // Set the realm for the application
            Realm = ConfigurationManager.AppSettings["ida:realm"];

            // Set the audienceUri for the application
            AudienceUri = ConfigurationManager.AppSettings["ida:AudienceUri"];
            if (!String.IsNullOrEmpty(AudienceUri))
            {
                UpdateAudienceUri();
            }

            var idConfig = FederatedAuthentication.FederationConfiguration.IdentityConfiguration;
            var defaultIssuerTokenResolver = idConfig.IssuerTokenResolver;
            var oobResolvers =
                    OutOfBandX509CertificateSecurityTokenResolver.EasyIdSandboxSigningCertificates.Select(
                        sandboxSigningCertificate =>
                {
                    var rawData = Convert.FromBase64String(sandboxSigningCertificate);
                    var easyIdSandboxCert = new X509Certificate2(rawData);
                    return
                        new OutOfBandX509CertificateSecurityTokenResolver(defaultIssuerTokenResolver, easyIdSandboxCert);
                });
            idConfig.IssuerTokenResolver = new AggregateTokenResolver(oobResolvers);
            ;
        }

        public static void UpdateAudienceUri()
        {
            int count = FederatedAuthentication.FederationConfiguration.IdentityConfiguration
                .AudienceRestriction.AllowedAudienceUris.Count(
                    uri => String.Equals(uri.OriginalString, AudienceUri, StringComparison.OrdinalIgnoreCase));
            if (count == 0)
            {
                FederatedAuthentication.FederationConfiguration.IdentityConfiguration
                    .AudienceRestriction.AllowedAudienceUris.Add(new Uri(IdentityConfig.AudienceUri));
            }
        }
    }
}
