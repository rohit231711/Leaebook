<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Client_Login" %>

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
        	left:279px !important;
        }
    </style>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("#form1").validationEngine();



        });
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
    <form id="form1" runat="server">
    <div class="login-form">
        <h1 style="border: none;">
            Account Login</h1>
        <form action="">
        <label class="lab2">
            Email
        </label>
        <div class="divClear">
        </div>
        <div class="user-text">
            <asp:TextBox ID="txtEmail" autocomplete="off" runat="server" CssClass="validate[required,custom[email]] user"></asp:TextBox>
        </div>
        <label class="lab2">
            Password</label>
        <div class="divClear">
        </div>
        <div class="user-pass">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="validate[required] user"></asp:TextBox>
        </div>
        <asp:Button ID="btnSubmit1" runat="server" CssClass="login-bt" CausesValidation="false"
            OnClick="btnSubmit_Click" />
        <a onclick="facyboxClose(); window.parent.location.href='forgotpassword.aspx'" href="#" class="forgot" style="background: none; color: #009933 !important;
            float: right;">Forgot Password?</a>
        <div class="divClear">
        </div>
        <div class="cheak-box">
            <input id="chkRemPass" type="checkbox" class="chek" runat="server" /><label class="lab2">
                Remember Me</label>
        </div>
        </form>
    </div>
    </form>
</body>
</html>
