<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="easyIDDemo.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <iframe height="400" width="600" src="https://codan-forsikring.grean.id/passive/federation?wa=wsignin1.0&wauth=urn:grn:authn:no:bankid:central&wtrealm=urn:grn:app:easyid-demo&wreply=https://localhost:44301/"></iframe>
</asp:Content>
