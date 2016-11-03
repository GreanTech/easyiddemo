<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="easyIDDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Workaround to please IntelliSense (see http://stackoverflow.com/questions/31886413/the-name-o-does-not-exist-in-the-current-context/31886588#31886588): --> 
    <%="" %>

    <div class="jumbotron">
        <p class="text-info">Welcome to the Grean easyID .NET demo site!</p>
        <p>Here, you'll find a working example of how to easily integrate various national login offerings into your web site.</p>
        <p>These examples will work with any .NET web site, from plain-vanilla ASP.NET (MVC and WebForms alike), over Sharepoint, to SiteCore and beyond.</p>        
    </div>
    <% if (User.Identity.IsAuthenticated) { %>
        <p>Hi there, <%= Context.User.Identity.Name %>!</p>
    <% } else { %>
        <div class="col-md-10">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true">
                <asp:ListItem Enabled="true" Selected="True" Text="NO BankID kodebrik" Value="nobid-central" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="NO BankID mobil" Value="nobid-mobile" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="SE BankID annan enhet" Value="sbid" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="SE BankID denna enhet" Value="sbid-local" class="margin-right-radio"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <% } %>
    <div class="row">
        <% if (!User.Identity.IsAuthenticated) { %>

        <script type="text/javascript">
            var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
            var eventer = window[eventMethod];
            var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";

            // Listen to message from child window
            eventer(messageEvent, function (e) {
                if (e && e.data && e.origin === '<%= Request.Url.GetLeftPart(UriPartial.Authority) %>' && e.data.userLoggedIn) {
                    window.location = '/';
                }
            }, false);
        </script>

        <div class="col-md-5 pull-left">
            <div id="login">
                <iframe src="Login.aspx?authMethod=<%= this.AuthMethod %>" id="easyid" title="easyID"
                    allowfullscreen="true" scrolling="no" frameborder="0" style="width:500px;height:300px;border:0px"></iframe>
            </div>
        </div>
        <div class="col-md-5 pull-right text-muted">
            <br /><span><%=this.AuthHint %></span><br /><span>Further details can be found <a href="<%= this.MoreDetails %>">here</a></span>
        </div>
        <% } %>
    </div>

</asp:Content>
