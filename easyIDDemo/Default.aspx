
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="easyIDDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Branding modul</h1>
        <p class="lead">Danmark har flere indbrud per indbygger end de fleste andre lande (are you scared yet?)</p>
        <p><a href="http://www1.codan.dk" class="btn btn-primary btn-lg">The only thing we have to fear is fear itself</a></p>
        <div class="col-md-10">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true">
                <asp:ListItem Enabled="true" Selected="True" Text="NO BankID central" Value="nobid-central" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="NO BankID mobile" Value="nobid-mobile" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="SE BankID remote" Value="sbid" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="SE BankID local" Value="sbid-local" class="margin-right-radio"></asp:ListItem>
<%--                <asp:ListItem Enabled="true" Selected="True" Text="Nøglekort" Value="nemid" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="Nøglekort erhverv" Value="nemid-moces" class="margin-right-radio"></asp:ListItem>
                <asp:ListItem Enabled="true" Selected="False" Text="Nøglefil" Value="oces" class="margin-right-radio"></asp:ListItem>--%>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="row">
        <% if (!User.Identity.IsAuthenticated) { %>

        <script type="text/javascript">
            var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
            var eventer = window[eventMethod];
            var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";

            // Listen to message from child window
            eventer(messageEvent, function (e) {
                if (e && e.data && e.origin === '<%= Request.Url.GetLeftPart(UriPartial.Authority) %>' && e.data.userLoggedIn) {
                    window.location = '/Contact';
                }
            }, false);
        </script>

        <div class="col-md-4">
            <div id="login">
                <iframe src="Login.aspx?authMethod=<%= this.AuthMethod %>" id="nemid" title="NemID" allowfullscreen="true" scrolling="no" frameborder="0" style="width:500px;height:450px;border:0px"></iframe>
            </div>
        </div>
        <% } %>
        <div class="col-md-4 pull-right">
            <h2>Et højrestillet CMS modul</h2>
            <p>
                Den forklarende tekst
            </p>
        </div>
    </div>

</asp:Content>
