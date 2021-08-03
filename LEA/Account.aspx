<%@ Page Title="My Account" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="Account.aspx.cs" Inherits="Account" %>

<%@ Register Src="~/Includes/account_leftmenu.ascx" TagName="leftmenu" TagPrefix="lm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/Rule.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.0/jquery.min.js"></script>    --%>
    <script type="text/javascript">

        //checkbox change password



        function valueChanged() {
            $('#autoUpdate').toggle();
            //if (this.checked) {
            //    $('#autoUpdate').toggle();
            //}
            //else {
            //    $('#autoUpdate').toggle();
            //}
        }

        function change() {
            //alert("out");
            if (document.getElementById('<%=chk_changepass.ClientID %>').checked == true) {
                //alert("in");
                $('#autoUpdate').toggle();
            }
        }


        // Email Validation

        function checkEmail() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            var email = document.getElementById('<%=txt_email.ClientID %>').value;
            var allowed = /^([a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$)/;
            if (!allowed.test(email)) {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor introduce válida Dirección de correo electrónico");
                }
                else {
                    alert("Please enter valid Email Address");
                }
                return false;
            }
        }
        // Validation
        function validation() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if (document.getElementById('<%=txt_firstname.ClientID %>').value == "") {

                //                if (cultureLanguage == "es-ES") {
                //                    alert("Por favor Nombre.");
                //                }
                //                else {
                //                    alert("Please enter Name.");
                //                }
                alert('<%= ResourceManager.GetString("usernamevalid", System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }
            if (document.getElementById('<%=txt_email.ClientID %>').value == "") {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Por favor, introduzca la dirección de correo electrónico.");
                //                }
                //                else {
                //                    alert("Please enter Email Address.");
                //                }
                alert('<%= ResourceManager.GetString("emailvalid",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }
            var validemail = checkEmail();
            if (validemail == false) {
                return false;
            }

            if (document.getElementById('<%=chk_changepass.ClientID %>').checked) {
                if (document.getElementById('<%=txt_currentpass.ClientID %>').value == "") {


                    alert('<%= ResourceManager.GetString("Pleaseenteroldpassword",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                    document.getElementById('<%=chk_changepass.ClientID %>').checked = false;
                    $('#autoUpdate').hide() = false;
                    return false;
                }
                if (document.getElementById('<%=txt_newpass.ClientID %>').value == "") {
                    //                    if (cultureLanguage == "es-ES") {
                    //                        alert("Por favor introduce la nueva contraseña.");
                    //                    }
                    //                    else {
                    //                        alert("Please enter New Password.");
                    //                        return false;
                    //                    }
                    alert('<%= ResourceManager.GetString("Pleaseenternewpassword",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                    document.getElementById('<%=chk_changepass.ClientID %>').checked = false;
                    $('#autoUpdate').hide() = false;
                    return false;
                }
                if (document.getElementById('<%=txt_cofirmpass.ClientID %>').value == "") {
                    //                    if (cultureLanguage == "es-ES") {
                    //                        alert("Por favor introduce Confirmar contraseña.");
                    //                    }
                    //                    else {
                    //                        alert("Please enter Confirm Password.");
                    //                        return false;
                    //                    }
                    alert('<%= ResourceManager.GetString("Pleaseenterconfirmpassword",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                    //document.getElementById('<%=chk_changepass.ClientID %>').checked = false;
                    $('#autoUpdate').hide() = false;
                    return false;
                }
                if (document.getElementById('<%=txt_newpass.ClientID %>').value != document.getElementById('<%=txt_cofirmpass.ClientID %>').value) {
                    alert('<%= ResourceManager.GetString("Confirmpassworddoesnotmatchwithpassword",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                    //document.getElementById('<%=chk_changepass.ClientID %>').checked = false;
                    $('#autoUpdate').hide() = false;
                    return false;
                }
            }

        }

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                //alert("You cannot select future date!");
                alert('<%= ResourceManager.GetString("NotAllowFutureDate",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }

        }

        function onkeyupp() {
            return false;
        }
    </script>
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="#">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
                </a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblaccountinformation" runat="server" Key="accountinformation"
                        Colon="false" />
                </a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="accbox account">
            <lm:leftmenu ID="leftmenu1" runat="server" />
            <div class="acc-rt">
                <div class="editacc">
                    <Localized:LocalizedLiteral ID="lbleditaccountinformation" runat="server" Key="editaccountinformation"
                        Colon="false" />
                </div>
                <div class="accpnl">
                    <Localized:LocalizedLiteral ID="lblaccountinformation1" runat="server" Key="accountinformation"
                        Colon="false" />
                </div>
                <div class="frm-txt">
                    <label class="name">
                        <Localized:LocalizedLiteral ID="lblfirstname" runat="server" Key="firstname" Colon="false" />
                        <span class="star">*</span></label>
                    <asp:TextBox ID="txt_firstname" runat="server" class="nametxt" onkeyup="isRule(this,objAlpha,100);" onchange="isRule(this,objAlpha,100);" Text=""></asp:TextBox>
                </div>
                <%--<div class="frm-txt">
                    <label class="name">
                        <Localized:LocalizedLiteral ID="lblgender" runat="server" Key="gender" Colon="false" />
                        <span class="star">*</span></label>
                    <div class="gender-txt">
                    <asp:RadioButton ID="rdo_male" class="radio" runat="server" GroupName="Gender" />
                    <span class="maletxt">
                        <Localized:LocalizedLiteral ID="lblmale" runat="server" Key="male" Colon="false" />
                    </span>
                    <asp:RadioButton ID="rdo_Female" class="radio" runat="server" GroupName="Gender" />
                    <span class="maletxt">
                        <Localized:LocalizedLiteral ID="lblfemale" runat="server" Key="female" Colon="false" />
                    </span>
                    </div>
                </div>--%>
                <%--<div class="frm-txt">
                    <label class="name">
                        <Localized:LocalizedLiteral ID="lblbirthdate" runat="server" Key="birthdate" Colon="false" />
                        <span class="star">*</span></label>
                    <asp:TextBox ID="txt_birthdate" runat="server" CssClass="nametxt disable_future_dates" Text="" onkeydown="return onkeyupp();"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MMM dd yyyy" PopupPosition="BottomLeft" TargetControlID="txt_birthdate"
                        OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_birthdate"
                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true">
                    </cc1:MaskedEditExtender>
                </div>--%>
                <div class="frm-txt">
                    <label class="name">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="Region" Colon="false" />
                        <span class="star">*</span></label>
                    <div class="combo">
                        <asp:DropDownList ID="ddlregion" AutoPostBack="true" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged" runat="server" class="nametxt"></asp:DropDownList>
                    </div>
                </div>

                <div class="frm-txt">
                    <label class="name">
                        <Localized:LocalizedLiteral ID="lblcountry" runat="server" Key="country" Colon="false" />
                        <span class="star">*</span></label>
                    <div class="combo">
                        <asp:DropDownList ID="dd_country" runat="server" class="nametxt"></asp:DropDownList>
                    </div>
                </div>
                <div class="frm-txt">
                    <label class="name">
                        <Localized:LocalizedLiteral ID="lblemailaddress" runat="server" Key="emailaddress1"
                            Colon="false" />
                        <span class="star">*</span></label>
                    <asp:TextBox ID="txt_email" runat="server" class="nametxt disable_future_dates" Text="" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="frm-txt">
                    <div class="chnge-txt">
                        <span class="pwdtxt">
                            <Localized:LocalizedLiteral ID="lblchangepassword" runat="server" Key="changepassword"
                                Colon="false" />
                        </span>
                        <span>
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chk_changepass" runat="server" class="pwdtxt" onchange="valueChanged()" Style="padding: 0px 0px;margin: -1px 24px;" />
                        </span>
                    </div>
                    <div id="autoUpdate" style="display: none">
                        <!--Change password-->
                        <div class="accpnl">
                            <Localized:LocalizedLiteral ID="lblchangepassword1" runat="server" Key="changepassword"
                                Colon="false" />
                        </div>
                        <div class="border-btm">
                            <div class="frm-txt">
                                <label class="name">
                                    <Localized:LocalizedLiteral ID="lblcurrentpassword" runat="server" Key="currentpassword"
                                        Colon="false" />
                                    <span class="star">*</span></label>
                                <asp:TextBox ID="txt_currentpass" runat="server" class="nametxt" Text="" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="frm-txt">
                                <label class="name">
                                    <Localized:LocalizedLiteral ID="lblnewpassword" runat="server" Key="newpassword"
                                        Colon="false" />
                                    <span class="star">*</span></label>
                                <asp:TextBox ID="txt_newpass" runat="server" class="nametxt" Text="" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="frm-txt">
                                <label class="name">
                                    <Localized:LocalizedLiteral ID="lblconfirmnewpassword" runat="server" Key="confirmnewpassword"
                                        Colon="false" />
                                    <span class="star">*</span></label>
                                <asp:TextBox ID="txt_cofirmpass" runat="server" class="nametxt" Text="" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <!--Change password-->
                    <div class="reqtxt1">
                        *
                    <Localized:LocalizedLiteral ID="lblrequiredfields" runat="server" Key="requiredfields"
                        Colon="false" />
                    </div>
                </div>
                <div class="btns frm-txt">
                    <a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="backbtn">
                        <Localized:LocalizedLiteral ID="lblback" runat="server" Key="back" Colon="false" />
                    </a>
                    <asp:LinkButton ID="btn_submit" runat="server" class="submitbtn" OnClientClick="return validation();"
                        OnClick="btn_submit_Click">
                        <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" />
                    </asp:LinkButton></div></div><!--rt end--></div><!--midle end--></div></asp:Content>