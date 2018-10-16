using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.UI;

namespace easyIDDemo
{
    public class ClaimRendition
    {
        public string Attribute;
        public string Type;
        public string TechIdentifier;
    }

    public class AuthMethodRendition
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public partial class _Default : Page
    {
        private class DetailInfo { public string AuthHint; public string MoreDetails; }
        private string authMethod = "nobid-central";
        private Dictionary<string, DetailInfo> hints;

        public _Default()
        {
            this.hints = new Dictionary<string, DetailInfo>
            {
                { "nobid-central",
                    new DetailInfo
                    {
                        AuthHint = "You can test with these credentials: Fødselsnummer '01017012345', Engangskode 'otp' and Personlig passord 'qwer1234'",
                        MoreDetails = "https://www.bankid.no/"
                    } },
                { "nobid-mobile",
                    new DetailInfo
                    {
                        AuthHint =  "You will need to get in touch with BankId Norge to get a SIM card for the test system.",
                        MoreDetails = "https://www.bankid.no/"
                    } },
                { "sbid",
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
                        MoreDetails = "http://docs.maksuturva.fi" } },
                { "fi-mobile-id",
                    new DetailInfo
                    {
                        AuthHint =
                            "We're awfully sorry, but you can only test this via a real mobile id, so you have to get such one yourself.",
                        MoreDetails = "https://mobiilivarmenne.fi/" } },
            };
        }

        public AuthMethodRendition[] GetAuthMethods()
        {
            var productionReady = new[] {
                new AuthMethodRendition { Name = "NO BankID kodebrik", Value = "nobid-central" },
                new AuthMethodRendition { Name = "NO BankID mobil", Value = "nobid-mobile" },
                new AuthMethodRendition { Name = "SE BankID denna enhet", Value = "sbid-local" },
                new AuthMethodRendition { Name = "SE BankID annan enhet", Value = "sbid" },
                new AuthMethodRendition { Name = "DK NemID privat", Value = "dknemid-poces" },
                new AuthMethodRendition { Name = "DK NemID erhverv", Value = "dknemid-moces" },
                new AuthMethodRendition { Name = "DK NemID nøglefil (erhverv)", Value = "dknemid-moces-codefile" }
            };

            if (this.Request.Url.Host == "www.grean.id")
            {
                return productionReady;
            }

            var inProgress = new AuthMethodRendition[] {
                new AuthMethodRendition { Name = "FI TUPAS", Value = "fi-tupas" },
                new AuthMethodRendition { Name = "FI Mobiilivarmenne", Value = "fi-mobile-id" }                
            };

            return productionReady.Concat(inProgress).ToArray();
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

        private ClaimRendition ToClaimRendition(Claim claim)
        {
            return new ClaimRendition
            {
                Attribute = claim.Value,
                Type = claim.Type.Split('/', ':').Last(),
                TechIdentifier = claim.Type
            };
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.authMethod = this.DropDownList.SelectedValue;
                //this.authMethod = this.RadioButtonList1.SelectedValue;
            }
        }
    }
}