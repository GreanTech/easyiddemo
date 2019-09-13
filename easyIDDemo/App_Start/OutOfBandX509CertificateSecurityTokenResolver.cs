using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace easyIDDemo
{
    public class OutOfBandX509CertificateSecurityTokenResolver
        : SecurityTokenResolver
    {
        private readonly SecurityTokenResolver innerResolver;
        private readonly X509Certificate2 [] certificates;

        public OutOfBandX509CertificateSecurityTokenResolver(
            SecurityTokenResolver innerResolver,
            params X509Certificate2 [] certificates)
        {
            if (innerResolver == null) throw new ArgumentNullException("innerResolver");
            if (certificates == null) throw new ArgumentNullException("certificate");

            this.innerResolver = innerResolver;
            this.certificates = certificates;
        }

        protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
        {
            key = null;
            SecurityToken token = null;
            if (base.TryResolveToken(keyIdentifierClause, out token) 
                && (token.SecurityKeys.Count > 0))
            {
                key = token.SecurityKeys[0];
                return true;
            }

            return false;
        }

        private bool TryMatchCertificates(Func<X509Certificate2, bool> predicate, out X509Certificate2 matched)
        {
            matched = this.certificates.FirstOrDefault(predicate);
            if (matched != null) return true;
            return false;
        }
        protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
        {
            token = null;
            X509Certificate2 matched = null;
            X509ThumbprintKeyIdentifierClause clause = keyIdentifierClause as X509ThumbprintKeyIdentifierClause;
            if ((clause != null) && this.TryMatchCertificates(clause.Matches, out matched))
            {
                token = new X509SecurityToken(matched);
                return true;
            }
            X509IssuerSerialKeyIdentifierClause clause2 = keyIdentifierClause as X509IssuerSerialKeyIdentifierClause;
            if ((clause2 != null) && TryMatchCertificates(clause2.Matches, out matched))
            {
                token = new X509SecurityToken(matched);
                return true;
            }
            X509SubjectKeyIdentifierClause clause3 = keyIdentifierClause as X509SubjectKeyIdentifierClause;
            if ((clause3 != null) && TryMatchCertificates(clause3.Matches, out matched))
            {
                token = new X509SecurityToken(matched);
                return true;
            }
            X509RawDataKeyIdentifierClause clause4 = keyIdentifierClause as X509RawDataKeyIdentifierClause;
            if ((clause4 != null) && TryMatchCertificates(clause4.Matches, out matched))
            {
                token = new X509SecurityToken(matched);
                return true;
            }

            if (this.innerResolver != null)
                return this.innerResolver.TryResolveToken(keyIdentifierClause, out token);

            return false;
        }

        protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
        {
            token = null;
            foreach (SecurityKeyIdentifierClause clause in keyIdentifier)
            {
                if (base.TryResolveToken(clause, out token))
                {
                    return true;
                }
            }
            return false;
        }

        public static string[] EasyIdSandboxSigningCertificates = new string[] {
            "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"
        };
    }
}