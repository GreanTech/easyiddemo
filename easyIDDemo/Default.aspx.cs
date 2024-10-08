using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Security.Claims;
using System.Web.UI;

namespace easyIDDemo
{
    public class SamlAttribute
    {
        public SamlAttribute(string attributeNamespace, string attributeName)
        {
            AttributeNamespace = attributeNamespace;
            AttributeName = attributeName;
        }

        public string AttributeNamespace { get; }
        public string AttributeName { get; }

        public string AsClaimType()
        {
            return this.AttributeNamespace + "/" + this.AttributeName;
        }
    }

    public class ObservedClaimTypes
    {
        public static SamlAttribute[] Production = new SamlAttribute[]
        {
                new SamlAttribute (
                    attributeNamespace:"http://schemas.xmlsoap.org/ws/2005/05/identity/claims",
                    attributeName:"nameidentifier"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.xmlsoap.org/ws/2005/05/identity/claims",
                    attributeName:"name"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.xmlsoap.org/ws/2005/05/identity/claims",
                    attributeName:"givenname"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.xmlsoap.org/ws/2005/05/identity/claims",
                    attributeName:"surname"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.microsoft.com/ws/2008/06/identity/claims",
                    attributeName:"role"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.trygghansa.se/2015/11/identity/claims",
                    attributeName:"ssn"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.xmlsoap.org/ws/2005/05/identity/claims",
                    attributeName:"upn"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com/identities/default",
                    attributeName:"provider"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com/identities/default",
                    attributeName:"connection"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com/identities/default",
                    attributeName:"isSocial"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"clientID"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"identityscheme"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"authenticationtype"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"loginid"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"sessionindex"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"ipaddress"),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"country"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"picture"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"nickname"),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"pseudonym"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"old_prefixes"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"gauss_id"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"id_token"
                ),
                new SamlAttribute ( attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"checked_in_gauss"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"idScheme"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"email_verified"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"updated_at"
                ),
                new SamlAttribute (
                    attributeNamespace:"http://schemas.auth0.com",
                    attributeName:"created_at"
                )
        };
    }

    public class ClaimRendition
    {
        public string Attribute;
        public string Type;
        public string TechIdentifier;
        public string ObservedInProduction;
    }

    public class AuthMethodRendition
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class LanguageRendition
    {
        public string DisplayName { get; set; }
        public string TwoLetterIsoCode { get; set; }
    }

    public partial class _Default : Page
    {
        private class DetailInfo
        {
            public string AuthHint;
            public string MoreDetails;
        }

        private readonly LanguageRendition[] languages =
            new[]
            {
                new LanguageRendition { DisplayName = "English", TwoLetterIsoCode = "en" },
                new LanguageRendition { DisplayName = "Svenska (sv)", TwoLetterIsoCode = "sv" },
                new LanguageRendition { DisplayName = "Svenska (se)", TwoLetterIsoCode = "se" },
                new LanguageRendition { DisplayName = "Norsk", TwoLetterIsoCode = "nb" },
                new LanguageRendition { DisplayName = "Dansk", TwoLetterIsoCode = "da" },
                new LanguageRendition { DisplayName = "Suomi", TwoLetterIsoCode = "fi" },
                new LanguageRendition { DisplayName = "Browser", TwoLetterIsoCode = LanguageState.BrowserLanguage }
            };

        private string authMethod;
        private Dictionary<string, DetailInfo> hints;

        public _Default()
        {
            this.authMethod = "sbid-qr";
            this.hints = new Dictionary<string, DetailInfo>
            {
                { "sbid",
                    new DetailInfo
                    {
                        AuthHint =  "You'll need to install the official BankId mobile app on your device, and configure it to access the test system",
                        MoreDetails = "https://www.bankid.com/assets/bankid/rp/how-to-get-bankid-for-test-v1.5.pdf"
                    } },
                { "sbid-qr",
                    new DetailInfo
                    {
                        AuthHint =  "You'll need to install the official BankId mobile app on your device, and configure it to access the test system",
                        MoreDetails = "https://www.bankid.com/assets/bankid/rp/how-to-get-bankid-for-test-v1.5.pdf"
                    } },
                { "sbid-local",
                    new DetailInfo
                    {
                        AuthHint =  "You'll need to install the official BankId desktop app on your machine, and import test user keys",
                        MoreDetails = "https://www.bankid.com/assets/bankid/rp/how-to-get-bankid-for-test-v1.5.pdf"
                    } },
                { "dknemid-poces",
                    new DetailInfo
                    {
                        AuthHint =  "You can find and create test users at https://appletk.danid.dk/testtools. Log in with user 'oces' and password 'nemid4all'",
                        MoreDetails = "https://www.nets.eu/dk-da/kundeservice/nemid-tjenesteudbyder/NemID-tjenesteudbyderpakken/Pages/dokumentation.aspx"
                    } },
                { "dknemid-moces",
                    new DetailInfo
                    {
                        AuthHint =
                            " If you have already signed up for a TU agreement, you can use the test-administrator signature to create test users." +
                            "Or you can download some pre-cooked ones <a href=\"https://www.nets.eu/dk-da/kundeservice/nemid-tjenesteudbyder/NemID-tjenesteudbyderpakken/Pages/OCES-II-certifikat-eksempler.aspx\">here</a>",
                        MoreDetails ="https://www.nets.eu/dk-da/kundeservice/nemid-tjenesteudbyder/implementering"
                    } },
                { "dknemid-moces-codefile",
                    new DetailInfo
                    {
                        AuthHint =
                            "You'll need a browser that supports Java applets to use this particular authentication mechanism." +
                            " IE 11 Desktop (<em>not</em> Edge) and Firefox on OS X have been known to work." +
                            " If you have already signed up for a TU agreement, you can use the test-administrator signature to create test users." +
                            "Or you can download some pre-cooked ones <a href=\"https://www.nets.eu/dk-da/kundeservice/nemid-tjenesteudbyder/NemID-tjenesteudbyderpakken/Pages/OCES-II-certifikat-eksempler.aspx\">here</a>",
                        MoreDetails = "https://www.nets.eu/dk-da/kundeservice/nemid-tjenesteudbyder/implementering"
                    } },
                { "fi-tupas",
                    new DetailInfo
                    {
                        AuthHint =
                            "You can find test users <a href=\"http://docs.maksuturva.fi/fi/html/pages/4_2_1_verkkopankkien_testitunnukset.html\">here</a>" +
                            "There is also an English version <a href=\"http://docs.maksuturva.fi/en/html/pages/4_2_1_internet_banks__test_credentials.html?ms=EQAAIBA=&mw=NDAw&st=MA==&sct=MA==\">here</a>",
                        MoreDetails = "http://docs.maksuturva.fi"
                    }
                },
                { "fi-mobile-id",
                    new DetailInfo
                    {
                        AuthHint =
                            "You can only test this via a real mobile id, so you have to get such one for yourself.",
                        MoreDetails = "https://mobiilivarmenne.fi/"
                    }
                },
                { "fi-all",
                    new DetailInfo { AuthHint = "", MoreDetails = "" }
                },
                { "no-vipps",
                    new DetailInfo {
                        AuthHint = "You can sign up for Vipps Login with your Norwegian BankID.",
                        MoreDetails = "https://www.vipps.no/produkter-og-tjenester/bedrift/innlogging-og-identifisering/logg-inn-med-vipps/"
                    }
                },
                { "nobid-oidc",
                    new DetailInfo
                    {
                        AuthHint = "You can find and create test users at https://ra-preprod.bankidnorge.no/#/search/endUser. All kodebrik (aka 'Netcentric') test accounts are automatically created with Engangskode 'otp' and Personlig passord 'qwer1234'. If you need to test mobile logins, you will need to get in touch with BankId Norge to get a SIM card for the test system.",
                        MoreDetails = "https://www.bankid.no/"
                    }
                },
                { "dkmitid-low",
                    new DetailInfo { AuthHint = "Username-password", MoreDetails = "" }
                },
                { "dkmitid-substantial",
                    new DetailInfo { AuthHint = "Chip or code-reader", MoreDetails = "" }
                },
            };
        }

        protected override void OnInit(EventArgs e)
        {
            var authMethodState = new AuthMethodState();
            if (String.IsNullOrEmpty(authMethodState.GetState(this.Request)))
            {
                authMethodState.SetState(this.Response, this.authMethod);
            }

            var langState = new LanguageState();
            if (String.IsNullOrEmpty(langState.GetState(this.Request)))
            {
                langState.SetState(this.Response, LanguageState.BrowserLanguage);
            }
            var ssoState = new EstablishSsoSessionState();
            if (String.IsNullOrEmpty(ssoState.GetState(this.Request)))
            {
                ssoState.SetEnabled(this.Response, true);
            }
            base.OnInit(e);
        }

        public AuthMethodRendition[] GetAuthMethods()
        {
            var productionReady = new[] {
                new AuthMethodRendition { Name = "SE BankID QR", Value = "sbid-qr" },
            };

            if (this.Request.Url.Host == "www.grean.id" || this.Request.Url.Host == "easyid-demo-rules-migration.azurewebsites.net")
            {
                return productionReady;
            }

            var inProgress = new AuthMethodRendition[] {
                new AuthMethodRendition { Name = "DK MitID low", Value = "dkmitid-low" },
                new AuthMethodRendition { Name = "DK MitID subst", Value = "dkmitid-substantial" }
            };

            return productionReady.Concat(inProgress).ToArray();
        }

        public LanguageRendition[] GetLanguages()
        {
            return this.languages;
        }

        public string AuthMethod
        {
            get
            {
                return this.authMethod;
            }
        }

        public string AuthHint
        {
            get
            {
                return this.hints[this.authMethod].AuthHint;
            }
        }

        public string MoreDetails
        {
            get
            {
                return this.hints[this.authMethod].MoreDetails;
            }
        }

        public IEnumerable<ClaimRendition> Claims
        {
            get
            {
                var cp = User as ClaimsPrincipal;
                if (cp == null) return Enumerable.Empty<ClaimRendition>();

                return cp.Claims.Select(ToClaimRendition);
            }
        }

        public IEnumerable<SamlAttribute> MaybeMissingAttributes
        {
            get
            {
                var cp = User as ClaimsPrincipal;
                if (cp == null) return ObservedClaimTypes.Production;

                return ObservedClaimTypes.Production.Where(sat =>
                    !cp.FindAll(c => c.Type == sat.AsClaimType()).Any()
                );
            }
        }

        private ClaimRendition ToClaimRendition(Claim claim)
        {
            return new ClaimRendition
            {
                Attribute = claim.Value,
                Type = claim.Type.Split('/', ':').Last(),
                TechIdentifier = claim.Type,
                ObservedInProduction =
                    ObservedClaimTypes.Production
                        .Where(sat => sat.AsClaimType() == claim.Type)
                        .Any() ? "Yes" : ""
            };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.authMethod = this.DropDownList.SelectedValue;
                new AuthMethodState().SetState(this.Response, this.authMethod);
                new LanguageState().SetState(this.Response, this.DropDownListLanguage.SelectedValue);
                new EstablishSsoSessionState().SetEnabled(this.Response, this.CheckBoxSingleSignOnSession.Checked);
            }
        }
    }
}