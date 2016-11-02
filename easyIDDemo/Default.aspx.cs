using System;
using System.Web.UI;

namespace easyIDDemo
{
    public partial class _Default : Page
    {
        private string authMethod = "nobid-central";
        public string AuthMethod 
        { 
            get 
            {
                return this.authMethod;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                this.authMethod = this.RadioButtonList1.SelectedValue;
            }
        }
    }
}