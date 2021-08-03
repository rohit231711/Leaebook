<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7; IE=EmulateIE9">
    <title>Login</title>
    <link href="<%= ConfigurationManager.AppSettings.Get("SiteUrlMain") + "images/header_logo.png" %>" rel="shortcut icon" type="image/x-icon" />
    <%--<link rel="icon" type="image/png" href="../images/favicon.png">--%>
    <!--  jquery core -->
    <script type="text/javascript" src="../javascripts/js/jquery-1.4.1.min.js"></script>
    <!-- Custom jquery scripts -->
    <script src="../javascripts/js/custom_jquery.js" type="text/javascript"></script>
    <!-- MUST BE THE LAST SCRIPT IN <HEAD></HEAD></HEAD> png fix -->
    <script src="../javascripts/js/jquery.pngFix.pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="../javascripts/loginscripts.js"></script>
    <link href="../stylesheets/styles_new.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 7]><!-->
    <link href="../stylesheets/ie7styles_new.css" rel="stylesheet" type="text/css" />
    <!--<![endif]-->
    <!--[if ! lte IE 7]><!-->
    <link href="../stylesheets/styles_new.css" rel="stylesheet" type="text/css" />
    <!--<![endif]-->
    <script type="text/javascript">
        function validate() {
            if (document.getElementById('<%=txtUsername.ClientID %>').value == '') {

                var theControl = document.getElementById('<%=dvalert.ClientID %>');
                theControl.style.display = '';
                document.getElementById('<%=lblAlert.ClientID %>').innerHTML = "Please enter user name.";
                document.getElementById('<%=txtUsername.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtPassword.ClientID %>').value == '') {

                var theControl = document.getElementById('<%=dvalert.ClientID %>');
                theControl.style.display = '';
                document.getElementById('<%=lblAlert.ClientID %>').innerHTML = "Please enter password.";
                document.getElementById('<%=txtPassword.ClientID %>').focus();
                return false;
            }
        }
     
    </script>
</head>
<body class="login_bg">
    <form runat="server" id="rorm1">
    <div class="wrapper">
        <div class="login_header">
            <div class="login_content_main">
                <div class="login_logo">
                    <a href="#">
                        <img src="../images/header_logo.png" alt="logo" style="margin-top: -90px;" /></a></div>
                <div class="login_content_bg" style="margin-bottom: 100px;">
                    <div class="login_main">
                        <div class="login_field_main">
                            <div class="login_field">
                                User Name
                            </div>
                            <div class="login_field">
                                <asp:TextBox ID="txtUsername" runat="server" class="login_input"></asp:TextBox>
                            </div>
                            <div class="login_field_main1">
                                <div class="login_field">
                                    Password</div>
                                <div class="login_field">
                                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="login_password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="remember_main">
                                <div class="check_box">
                                    <%-- <input type="checkbox" class="myClass" id="login-check" name="login-check" runat="server" />--%>
                                    <asp:CheckBox ID="chklogin" runat="server" CssClass="myClass" />
                                </div>
                                <div class="remember">
                                    Remember me</div>
                                <div class="forgot">
                                    <!--<a id="various11" href="#inline11">Forgot Password?</a>-->
                                </div>
                            </div>
                            <div class="login_field_main">
                                <asp:Button ID="btnSubmit" runat="server" OnClientClick="return validate();" Text="Login"
                                    CssClass="login_button" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="clear">
                            </div>
                            <br />
                            <div id="dvalert" runat="server" visible="false">
                                <p style="background-color: #9FB3C7; border: 1px solid #3970A8; color: #264C73; z-index: 1;
                                    padding: 15px 15px 15px 40px; background-image: url('images/msg-error.gif');
                                    background-repeat: no-repeat; background-position: 5px 13px;">
                                    <strong>
                                        <asp:Label ID="lblAlert" runat="server"></asp:Label>
                                    </strong>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="clear">
    </div>
    <div class="push">
    </div>
    <div class="clear">
    </div>
    <footer>
  <div class="footer">
    <div class="footer">
    <div class="footer_bg">
      <div class="bottom_content1">
        <div class="bottom_left" style="width: 100%;">          
          <div class="copy">
          <center><asp:Label ID="lblCopyRights" runat="server"></asp:Label> </center>
          </div>
        </div>
        <div class="bottom_right1" style="width:10%"> </div>
        <div class="clear"></div>
      </div>
    </div>
    <div class="clear"></div>
  </div>
  </div>
</footer>
    </form>
</body>
</html>
