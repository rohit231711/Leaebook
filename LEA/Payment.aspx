<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#amount").val() != undefined) {
                $('#btnSubmit').click();
                $('#btnSubmit').trigger('click');
            }
        });
    </script>
</head>
<body>
    <form action="https://www.paypal.com/cgi-bin/webscr" method="post">
        <%=HiddenFilds%>
        <input type="submit" value="" id="btnSubmit" style="visibility: hidden" />
    </form>
</body>
</html>
