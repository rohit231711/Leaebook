<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDTSuccess.aspx.cs" Inherits="PDT_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
  <script type="text/javascript">
      $(document).ready(function () {
          setTimeout(function () {
             <%-- window.location = '/MyLibrary.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';--%>
               window.location = '/Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
             
          }, 6000);
      });
  </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="display: none">
        <b>Personalized 'Thank you' for customer:</b><br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <b>PDT Response</b><br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <b>Cutom</b><br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <b>item number</b><br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <b>invoide no</b><br />
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
    </div>
    <div class="contant" style="margin: 0px; position: relative; top: 13px;">
        <div id="divshow" class="" style="width: 293px;background: none;position: fixed;top: 2%;left: 1%;">
            <div class="right1" style="width: 331px;">
                <div class="right-top-hdr">
                    <h3 id="h3" runat="server" style="background-color: #3B9A02; width: 379px; padding: 6px;
                        color: White">
                        <asp:Label ID="lblAlert" runat="server" Text="Alert"></asp:Label>
                    </h3>
                    <p style="margin-left: 0px; padding: 6px">
                        <asp:Label ID="lblMessage" Font-Size="14px" runat="server" Text=" Thank you for purchasing Book,your order detail is mailed to you."></asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
