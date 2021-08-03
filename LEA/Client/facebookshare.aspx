<%@ Page Language="C#" AutoEventWireup="true" CodeFile="facebookshare.aspx.cs" Inherits="Client_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/images/fevicon.png" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/images/fevicon.png" type="image/ico" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js"></script>
    <script src="js/dropkick.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="js/jquery.validationEngine-en.js" type="text/javascript"></script>
    <link href="css/validationEngine.jquery.css" rel="stylesheet" />
    <style>
        .fancybox-inner
        {
            height: 340px;
        }
        .formError
        {
            left: 279px !important;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            //            alert('load');
//            window.location = "https://www.facebook.com/dialog/feed?app_id=741235135905751&display=popup&caption=An%20example%20caption&link=https%3A%2F%2Fdevelopers.facebook.com%2Fdocs%2Fdialogs%2F&redirect_uri=http://localhost:50807/SonicStudio/Client/facebookshare.aspx";
        });

        function PostStatus() {
            var msg = $('#txtPostStatus').val();
            alert(msg);
            if (msg != '') {
                //                $.ajax({
                //                    url: '/PostStatus/PostStatus',
                //                    type: 'POST',
                //                    data: { msg: msg },
                //                    success: function (authLink) {
                //                        if (authLink != 'No') {
                //                            window.open(authLink, 'title', 'width=660,height=500,status=no,scrollbars=yes,toolbar=0,menubar=no,resizable=yes,top=60,left=320');
                //                        }
                //                    }
                //                });

                $.ajax({
                    type: "POST",
                    url: "ws.asmx/PostStatus",
                    data: "{msg:'" + msg + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (authLink != 'No') {
                            window.open(authLink, 'title', 'width=660,height=500,status=no,scrollbars=yes,toolbar=0,menubar=no,resizable=yes,top=60,left=320');
                        }
                    }
                });

            }
            else {
                alert('please write your message.');
            }
        }

        function facyboxClose() {
            debugger;
            parent.$.fancybox.close();
            window.parent.location = parent.location.pathname;

        }
        $(function () {
            $('.baby_bear').dropkick();
            $('.mama_bear').dropkick();
            $('.papa_bear').dropkick();
        });
    </script>
</head>
<body style="background: none">
    <form runat="server">
    <fieldset style="color: Black;">
        <legend>Message</legend>
        <div>
            <label style="font-style: italic; color: Gray;">
                Write your status</label><br />
            <input type="text" name="txtPostStatus" id="txtPostStatus" style="height: 100px;
                width: 500px;" />
        </div>
        <div style="text-align: left;">
            <%--  <input id="btnPost" type="button" value="Post Status" onclick="PostStatus();" />--%>
            <asp:Button runat="server" ID="Post" OnClick="Post_Click" />
        </div>
    </fieldset>
    </form>
</body>
</html>
