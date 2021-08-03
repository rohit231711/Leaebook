<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Activation.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>    
    <form id="form1" runat="server">
    <asp:HiddenField ID="hndEmail" runat="server" />
    <asp:HiddenField ID="hndPassword" runat="server" />
    <div>

    Hello , <asp:Label runat ="server" ID="lblName" Text=""></asp:Label> your account successfully activated.<br />
<br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Activation.aspx?login=1">Click here to login</asp:HyperLink>
    </div>
    </form>
</body>
</html>
