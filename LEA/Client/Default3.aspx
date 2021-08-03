<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Client_Default3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="row2" class="row2" style="display: none; height: 328px">
        <div class="login">
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
                <asp:Button ID="btnSubmit" runat="server" CssClass="login-bt" OnClick="btnSubmit_Click" />
                <a href="ForgotPassword.aspx" class="forgot" style="background: none; color: #009933 !important;
                    float: right;">Forgot Password?</a>
                <div class="divClear">
                </div>
                <div class="cheak-box">
                    <input id="chkRemPass" type="checkbox" class="chek" runat="server" /><label class="lab2">
                        Remember Me</label>
                </div>
                </form>
            </div>
            <div class="divClear">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
