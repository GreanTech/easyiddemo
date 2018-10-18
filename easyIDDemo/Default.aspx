﻿
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="easyIDDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Workaround to please IntelliSense (see http://stackoverflow.com/questions/31886413/the-name-o-does-not-exist-in-the-current-context/31886588#31886588): --> 
    <%="" %>

    <% if (User.Identity.IsAuthenticated) { %>
    <div class="jumbotron">
        Hi there, <code><%= Context.User.Identity.Name %></code>! Here's what Criipto Verify tells us about you
    </div>
    <div class="row">
        
        <table class="table table-striped">
            <thead><tr><th>Attribute</th><th>Type</th><th>Tech identifier</th></tr></thead>
            <tbody>
                <% foreach (var claim in this.Claims) { %>
                    <tr><td><%=claim.Attribute%></td><td><%=claim.Type%></td><td><%=claim.TechIdentifier%></td></tr>
                <% } %>
            </tbody>
        </table>
    </div>
    <% } else { %>
    <div class="jumbotron">
        <p class="text-info">Welcome to the Criipto Verify .NET demo site!</p>
        <p>Here, you'll find a working example of how to easily integrate various national login offerings into your web site.</p>
        <p>This sample will work with any .NET web site, from plain-vanilla ASP.NET (MVC and WebForms alike), over Sharepoint, to SiteCore and beyond.</p>        
    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="input-group">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="DropDownList" CssClass="form-control" 
                    SelectMethod="GetAuthMethods"  DataTextField="Name" DataValueField="Value" >
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="Scripts/userLoggedInListener.js"></script>
    <div class="row">
        <div class="col-lg-6">
            <div id="login">
                <iframe src="" id="verify" title="Criipto-Verify" class="login-frame-<%=this.AuthMethod %>"
                    allowfullscreen="true" scrolling="no" frameborder="0" class="hidden-frame"></iframe>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var testUA = function (pattern) {
            return pattern.test(navigator.userAgent);
        };
        var isiOS = function () {
            return testUA(/iPad|iPhone|iPod/) && !window.MSStream;
        };
        var isiOSSafari = function () {
            return isiOS() && !testUA(/ CriOS\/[.0-9]*/);
        };
        var isiOSChrome = function () {
            return isiOS() && testUA(/ CriOS\/[.0-9]*/);
        };
        var isAndroid = function () { return testUA(/Android/); };
        var isWindowsPhone = function () { return testUA(/Windows Phone/i); };
        var isWindowsPhone8 = function () { return testUA(/Windows Phone 8/i); };

        var framed = function (loginUrl) {
            var frame = document.getElementById('verify');
            frame.src = loginUrl;
            frame.class = 'visible-frame';
        };
        var redirect = function (loginUrl) {
            document.location = loginUrl;
        };
        var selectStrategy = function (authMethod) {
            if (authMethod === 'sbid-local') {
                if (isWindowsPhone()) {
                    // WinPhone 8 UA string contains 'Android', so handle it first
                    if (isWindowsPhone8()) {
                        // The reason to use redirect here is because WP8 mis-interprets
                        // the X-Frame-Options ALLOW-FROM header that Criipto Verify sends.
                        console.log('Same-device SE bankid on WinPhone8 detected. Redirecting.');
                        return redirect;
                    } else {
                        console.log('Same-device SE bankid on WinPhone detected. Framing.');
                        return framed;
                    }
                } else if (isiOSSafari()) {
                    console.log('Same-device SE bankid on iOS Safari detected. Redirecting');
                    return redirect;
                } else if (isiOSChrome()) {
                    console.log('Same-device SE bankid on iOS Chrome detected. Redirecting');
                    return redirect;
                } else if (isAndroid()) {
                    console.log('Same-device SE bankid on Android detected. Redirecting');
                    return redirect;
                }
                return framed;
            } else if (authMethod === 'fi-tupas' || authMethod === 'fi-mobile-id' || authMethod === 'fi-all') {
                return redirect;
            }

            return framed;
        };

        var authMethod = '<%=this.AuthMethod %>';
        var loginUrl = 'Login.aspx?authMethod=' + authMethod;
        var strategy = selectStrategy(authMethod);
        strategy(loginUrl);
    </script>
    <div class="row text-muted">
        <br /><span><%=this.AuthHint %></span><br /><span>Further details can be found <a href="<%= this.MoreDetails %>">here</a></span>
    </div>
    <% } %>

</asp:Content>
