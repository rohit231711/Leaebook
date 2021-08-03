<%@ Page Title="Login" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function validation() {
            $(document).on('click', ".accbtn", function () {
                //alert('works');
            });
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if (document.getElementById('<%=txt_email.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca la dirección de correo electrónico.");
                }
                else {
                    alert("Please enter Email Address.");
                }
                return false;
            }
            var validemail = checkEmail();
            if (validemail == false) {
                return false;
            }
            if (document.getElementById('<%=txt_password.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca la contraseña.");
                }
                else {
                    alert("Please enter password.");
                }
                return false;
            }
        }

        function checkEmail() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            var email = document.getElementById('<%=txt_email.ClientID %>').value;
            var allowed = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!allowed.test(email)) {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Por favor introduce válida Dirección de correo electrónico");
                //                }
                //                else {
                //                    alert("Please enter valid Email Address"); 
                //                }
                alert('<%= ResourceManager.GetString("emailvaildplac", System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }
        }

    </script>
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="#">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
                </a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblloginorcreateanaccount" runat="server" Key="loginorcreateanaccount"
                        Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox">
            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="lblloginorcreateanaccount1" runat="server" Key="loginorcreateanaccount"
                        Colon="false" />
                </h1>
            </div>
            <div class="loginbg">
                <div class="lft">
                    <div class="cust-lft">
                        <div class="newcust">
                            <Localized:LocalizedLiteral ID="lblnewcustomers" runat="server" Key="newcustomers"
                                Colon="false" />
                        </div>
                        <p>
                            <Localized:LocalizedLiteral ID="lblnewcustomersnote" runat="server" Key="newcustomersnote"
                                Colon="false" />
                        </p>
                    </div>
                    <!--cust-lft end-->
                    <div class="btnbox">
                        <a href="Register.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="accbtn">
                            <Localized:LocalizedLiteral ID="lblcreateanaccount1" runat="server" Key="createanaccount"
                                Colon="false" />
                        </a>
                    </div>
                </div>
                <!--lft end-->
                <div class="cust-rt">
                    <div class="newcust2">
                        <Localized:LocalizedLiteral ID="lblregisteredcustomers" runat="server" Key="registeredcustomers"
                            Colon="false" />
                    </div>
                    <p>
                        <Localized:LocalizedLiteral ID="lblaccountwithlogin" runat="server" Key="accountwithlogin"
                            Colon="false" />
                    </p>
                    <div class="frm-grp">
                        <label class="emailtxt">
                            <Localized:LocalizedLiteral ID="lblemailaddress" runat="server" Key="emailaddress"
                                Colon="false" />
                            <span class="star">*</span></label>
                        <%--<input type="text" class="emailbox" value="" onfocus="if(this.value=='') this.value='';"
                            onblur="if(this.value=='') this.value='';" />--%>
                        <asp:TextBox ID="txt_email" class="emailbox" runat="server" Text=""></asp:TextBox>
                    </div>
                    <div class="frm-grp">
                        <label class="emailtxt">
                            <Localized:LocalizedLiteral ID="lblpassword" runat="server" Key="password" Colon="false" />
                            <span class="star">*</span></label>
                        <%--<input type="password" class="emailbox" value="" onfocus="if(this.value=='') this.value='';"
                            onblur="if(this.value=='') this.value='';" />--%>
                        <asp:TextBox ID="txt_password" class="emailbox" runat="server" Text="" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="frm-grp">
                        <label class="reqtxt">
                            *
                            <Localized:LocalizedLiteral ID="lblrequiredfields" runat="server" Key="requiredfields"
                                Colon="false" /></label>
                    </div>
                </div>
                <!--rt end-->
                <!-- <div class="btnbox-rt"> -->
                <div class="cust-rt" style="margin-top:0px">
                    <span class="forgottxt"><a href="Forgot-Password.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                        <Localized:LocalizedLiteral ID="lblforgotpassword" runat="server" Key="forgotyourpassword1"
                            Colon="false" />
                    </a></span>
                    <%--<a href="#" class="accbtn">--%> 
                    <asp:Button ID="btn_login" runat="server" class="accbtn" OnClientClick="return validation();" Text="Login"
                        OnClick="btn_login_Click">
                        </asp:Button><%--</a>--%></div></div><!--loginbg end--></div><!--loginbox end--></div></asp:Content>