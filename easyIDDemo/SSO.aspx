<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SSO.aspx.cs" Inherits="easyIDDemo.SSO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Scripts/userLoggedInListener.js"></script>
    <iframe src="/Login" id="verify-sso" title="Criipto-Verify-SSO" 
        allowfullscreen="true" scrolling="no" frameborder="0" class="hidden-frame">
    </iframe>
</asp:Content>
