<%@ Page Title="Forgot-Password" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="Forgot-Password.aspx.cs" Inherits="Forgot_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
        function Validation()
        {            
            if (document.getElementById('<%=txtEmail.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor introduzca su correo electrónico.");
                }
                else {
                    alert("Please enter email.");
                }
                return false;
            }
            else {
                test(document.getElementById('<%=txtEmail.ClientID %>').value);
            }
        }

        function test(sEmail) {
            debugger;
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            //var filter = /^(([^<>()[\]\\.,;:\s@@\"]+(\.[^<>()[\]\\.,;:\s@@\"]+)*)|(\".+\"))@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor ingrese una identificación válida de correo electrónico.");
                }
                else {
                    alert("Please enter valid email id.");
                }
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--************************middlecontent**********************-->
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="#">
                <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
                </a>/</li>
                <li><span><a href="#">
                <Localized:LocalizedLiteral ID="lblforgotpassword" runat="server" Key="forgotpassword" Colon="false" />
                </a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox1">
            <div class="logintxt" runat="server">
                <asp:Label ID="lbl_error" style="color:Red;font-size:20px;" runat="server" Text=""></asp:Label>
            </div>
            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="lblforgotyourpassword" runat="server" Key="forgotyourpassword" Colon="false" />
                </h1>
            </div>
            <div class="forgot-box"> 
                <p>
                    <Localized:LocalizedLiteral ID="lblforgotpasswordnote" runat="server" Key="forgotpasswordnote" Colon="false" />
                    </p>
                <label class="email">
                    <Localized:LocalizedLiteral ID="lblemailaddress" runat="server" Key="emailaddress" Colon="false" />
                    <span class="star">*</span></label>
                <input type="text" class="emailbox2" value="" onfocus="if(this.value=='') this.value='';"
                    onblur="if(this.value=='') this.value='';" id = "txtEmail" runat="server" style = "width:332px; height:46px;"/>
            </div>
            <div class="reqtxt1">
                * <Localized:LocalizedLiteral ID="lblrequiredfields" runat="server" Key="requiredfields" Colon="false" /></div>
            <div class="btns">
                <a href="login.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="back">
                    <img src="images/backarw.png" class="bkarw" alt="" /><Localized:LocalizedLiteral ID="lblbacktologin" runat="server" Key="backtologin" Colon="false" />
                    </a> <%--<a href="#"
                        class="submitbtn"><Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" /></a>--%>

                        <asp:LinkButton ID="btn_login" runat="server" class="submitbtn" OnClientClick="return Validation();" OnClick="btn_submit">
                        <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" /></asp:LinkButton>
            </div>
        </div>
        <!--midle end-->
    </div>
</asp:Content>
