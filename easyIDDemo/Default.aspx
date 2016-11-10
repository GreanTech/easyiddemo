<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="easyIDDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Workaround to please IntelliSense (see http://stackoverflow.com/questions/31886413/the-name-o-does-not-exist-in-the-current-context/31886588#31886588): --> 
    <%="" %>

    <% if (!User.Identity.IsAuthenticated) { %>
    <div class="jumbotron">
        <p class="text-info">Welcome to the Grean easyID .NET demo site!</p>
        <p>Here, you'll find a working example of how to easily integrate various national login offerings into your web site.</p>
        <p>This sample will work with any .NET web site, from plain-vanilla ASP.NET (MVC and WebForms alike), over Sharepoint, to SiteCore and beyond.</p>        
    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="input-group">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="DropDownList" CssClass="form-control">
                    <asp:ListItem Enabled="true" Selected="True" Text="NO BankID kodebrik" Value="nobid-central"></asp:ListItem>
                    <asp:ListItem Enabled="true" Selected="False" Text="NO BankID mobil" Value="nobid-mobile"></asp:ListItem>
                    <asp:ListItem Enabled="true" Selected="False" Text="SE BankID annan enhet" Value="sbid"></asp:ListItem>
                    <asp:ListItem Enabled="true" Selected="False" Text="SE BankID denna enhet" Value="sbid-local"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <% } %>
    <% if (User.Identity.IsAuthenticated) { %>
    <div class="jumbotron">
        <p>Hi there, <code><%= Context.User.Identity.Name %></code>! Here's what easyID tells us about you:</p>
    </div>
    <div class="row">
        
        <table class="table table-striped">
            <thead><tr><th>Claim type</th><th>Value</th></tr></thead>
            <tbody>
                <% foreach (var claim in this.Claims) { %>
                    <tr><td><%=claim.Type%></td><td><%=claim.Value%></td></tr>
                <% } %>
            </tbody>
        </table>
    </div>
    <% } else { %>
    <script type="text/javascript" src="Scripts/userLoggedInListener.js"></script>
    <div class="row">
        <div id="login">
            <iframe src="" id="easyid" title="easyID" class="login-frame-<%=this.AuthMethod %>"
                allowfullscreen="true" scrolling="no" frameborder="0" class="hidden-frame"></iframe>
        </div>
    </div>
    <script type="text/javascript">
        var isiOS = function () {
            return /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
        }
        var authMethod = '<%=this.AuthMethod %>';
        var loginUrl = 'Login.aspx?authMethod=' + authMethod;
        if (isiOS() && authMethod == 'sbid-local')
        {
            console.log('Same-device SE bankid on iOS detected. Redirecting');
            document.location = loginUrl;
        } else {
            var frame = document.getElementById('easyid');
            frame.src = loginUrl;
            frame.class = 'visible-frame';
        }
    </script>
    <div class="row text-muted">
        <br /><span><%=this.AuthHint %></span><br /><span>Further details can be found <a href="<%= this.MoreDetails %>">here</a></span>
    </div>
    <% } %>

</asp:Content>
