<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="easyIDDemo.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <% if (User.Identity.IsAuthenticated) { %>            
            <script type="text/javascript" src="Scripts/userLoggedInNotifier.js"></script>
        <% } %>
    </div>
    </form>
</body>
</html>
