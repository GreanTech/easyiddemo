<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="easyIDDemo.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Mit Codan.<asp:Button ID="LogoutButton" runat="server" OnClick="LogoutButton_Click" Text="Logout" />
    </h2>
    <h3>Se dine forskringer her.</h3>
    <address>
        Gammel Kongevej 60<br />
        Vesterbro<br />
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:support@codan.dk">support@codan.dk</a><br />
        <strong>Marketing:</strong> <a href="mailto:marketing@codan.dk">marketing@codan.dk</a>
    </address>
<%--    <pre id="ssoData"></pre>
    <pre id="profile"></pre>
    <pre id="idToken"></pre>
    <pre id="accessToken"></pre>
    <script src="//cdn.auth0.com/js/lock-8.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if (sessionStorage.getItem("gauss_id_token") == null) {
                var lock = new Auth0Lock('SsjFsSbN0k71oD5ouzm3smczjqz0WrdA', 'codan-test.auth0.com');
                var auth = lock.$auth0
                auth.getSSOData(false, function (err, ssoData) {
                    $("#ssoData").text(JSON.stringify(ssoData));
                    auth.login({
                        connection: ssoData.lastUsedConnection.name,
                        popup: false,
                        callbackOnLocationHash: false,
                        authParams: {
                            scope: "openid user_id email"
                        }
                    },
                    function (err, profile, id_token, access_token) {
                        sessionStorage.setItem("gauss_id_token", id_token);
                        $("#profile").text(JSON.stringify(profile));
                        $("#idToken").text(JSON.stringify(id_token));
                        $("#accessToken").text(JSON.stringify(access_token));
                    })
                });
            }
        });
    </script>--%>
</asp:Content>
