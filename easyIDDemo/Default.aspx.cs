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
                    } }
            };
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
                Type = claim.Type.Split('/').Last(),
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