<%@ Page Title="Register" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="PartnerRegister.aspx.cs" Inherits="PartnerRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.0/jquery.min.js"></script>


    <script type="text/javascript">
        // date validation
        function isValidDate(dateStr) {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            var datePat = /^(\d{2,2})(\/)(\d{2,2})\2(\d{4}|\d{4})$/;
            var matchArray = dateStr.match(datePat); // is the format ok?
            if (matchArray == null) {
                if (cultureLanguage == "es-ES") {
                    alert("Date must be in MM/DD/YYYY format")
                }
                else {
                    alert("La fecha debe estar en MM/DD/YYYY formato")
                }
                return false;
            }
            month = matchArray[1]; // parse date into variables
            day = matchArray[3];
            year = matchArray[4];
            if (month < 1 || month > 12) { // check month range
                if (cultureLanguage == "es-ES") {
                    alert("Mes debe estar entre 1 y 12");
                }
                else {
                    alert("Month must be between 1 and 12");
                }
                return false;
            }
            if (day < 1 || day > 31) {
                if (cultureLanguage == "es-ES") {
                    alert("Día debe estar entre 1 y 31");
                }
                else {
                    alert("Day must be between 1 and 31");
                }
                return false;
            }
            if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
                if (cultureLanguage == "es-ES") {
                    alert("Mes " + month + " no tiene 31 días!")
                }
                else {
                    alert("Month " + month + " doesn't have 31 days!")
                }
                return false;
            }
            if (month == 2) { // check for february 29th
                var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
                if (day > 29 || (day == 29 && !isleap)) {
                    if (cultureLanguage == "es-ES") {
                        alert("febrero " + year + " no tiene " + day + " días!");
                    }
                    else {
                        alert("February " + year + " doesn't have " + day + " days!");
                    }
                    return false;
                }
            }
        }
        // Email Validation
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
        // Validation
        function validation() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if (document.getElementById('<%=txt_firstname.ClientID %>').value == "") {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Por favor ingrese Nombre.");
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
            <%--if (document.getElementById('<%=rdo_male.ClientID %>').checked == false && document.getElementById('<%=rdo_Female.ClientID %>').checked == false) {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Seleccione de Género.");
                //                }
                //                else {
                //                    alert("Please Select Gender.");
                //                }
                alert('<%= ResourceManager.GetString("gendervalid",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }--%>
            <%--var date = document.getElementById('<%=txt_birthdate.ClientID %>').value;
            var validdate = isValidDate(date);
            if (validdate == false) {
                return false;
            }--%>
            if (document.getElementById('<%=dd_country.ClientID %>').value == "0") {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Por favor introduce País.");
                //                }
                //                else {
                //                    alert("Please enter Country.");
                //                }
                alert('<%= ResourceManager.GetString("countryvalid",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }

            if (document.getElementById('<%=txt_password.ClientID %>').value == "") {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Por favor introduce la contraseña.");
                //                }
                //                else {
                //                    alert("Please enter Password.");
                //                }
                alert('<%= ResourceManager.GetString("Pleaseenternewpassword",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }
            if (document.getElementById('<%=txt_password.ClientID %>').value.trimLeft().trimRight().length < 6) {                
                if (cultureLanguage == "es-ES") {
                    alert("La contraseña debe tener al menos 6 caracteres.");
                    return false;
                }
                else {
                    alert("Password should be at least 6 character.");
                    return false;
                }
            }
            if (document.getElementById('<%=txt_ConfirmPassword.ClientID %>').value == "") {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Por favor introduce Confirmar contraseña.");
                //                }
                //                else {
                //                    alert("Please enter Confirm Password.");
                //                }
                alert('<%= ResourceManager.GetString("Pleaseenterconfirmpassword",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }
            if (document.getElementById('<%=txt_password.ClientID %>').value != document.getElementById('<%=txt_ConfirmPassword.ClientID %>').value) {
                //                if (cultureLanguage == "es-ES") {
                //                    alert("Contraseña y Confirmar contraseña no es la misma.");
                //                }
                //                else {
                //                    alert("Password and Confirm Password not same.");
                //                }
                alert('<%= ResourceManager.GetString("Confirmpassworddoesnotmatchwithpassword",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>');
                return false;
            }
            //            var answer = confirm("Your message has been sent successfully.")
            //            if (answer) {
            ////                alert("Bye bye!")
            //                //                window.location = "http://www.google.com/";
            //                return true;
            //                
            //            }


            <%--var myDate = new Date(document.getElementById('<%=txt_birthdate.ClientID %>').value);
            //alert(myDate);
            var today = new Date();
            {
                //alert(today);
                if (myDate > today) {
                    alert('You cannot enter a date in the future!')
                    return false;
                }
                else
                return true;
            }--%>
        }

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
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
                    <Localized:LocalizedLiteral ID="lblcreateanaccount" runat="server" Key="createanaccount"
                        Colon="false" />
                </a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox1">
            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="lblcreateanaccount1" runat="server" Key="createanaccount"
                        Colon="false" />
                </h1>
            </div>
            <div class="perinfo">
                <Localized:LocalizedLiteral ID="lblpersonalinformation" runat="server" Key="personalinformation"
                    Colon="false" />
            </div>
            <div class="frm-txt">
                <label class="name1">
                    <Localized:LocalizedLiteral ID="lblfirstname" runat="server" Key="firstname" Colon="false" />
                    <span class="star">*</span></label>
                <asp:TextBox ID="txt_firstname" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
            </div>
            <%--    <div class="frm-txt">
                <label class="name1">
                    <Localized:LocalizedLiteral ID="lbllastname" runat="server" Key="lastname" Colon="false" />
                    <span class="star">*</span></label>
                <asp:TextBox ID="txt_lastname" runat="server" class="nametxt" Text=""></asp:TextBox>
            </div>--%>
            <div class="frm-txt">
                <label class="name1">
                    <Localized:LocalizedLiteral ID="lblemail" runat="server" Key="email" Colon="false" />
                    <span class="star">*</span></label>
                <asp:TextBox ID="txt_email" runat="server" class="nametxt" Text=""></asp:TextBox>
            </div>
            <%--<div class="frm-txt">
                <label class="name1">
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
                <label class="birthtxt">
                    <Localized:LocalizedLiteral ID="lblbirthdate" runat="server" Key="birthdate" Colon="false" />
                    <span class="star">*</span></label>
                <asp:TextBox ID="txt_birthdate" runat="server" CssClass="nametxt" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txt_birthdate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_birthdate"
                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true">
                </cc1:MaskedEditExtender>
            </div>--%>
            <div class="frm-txt">
                <label class="name1">
                    <Localized:LocalizedLiteral ID="lblcountry" runat="server" Key="country" Colon="false" />
                    <span class="star">*</span></label>
                <div class="combo">
                    <asp:DropDownList ID="dd_country" runat="server" class="coun-combo nametxt">
                        <asp:ListItem>
                                <%--<Localized:LocalizedLiteral ID="lblselectcountry" runat="server" Key="selectcountry" Colon="false" />--%>
                        </asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="perinfo">
                <Localized:LocalizedLiteral ID="lbllogininformation" runat="server" Key="logininformation"
                    Colon="false" />
            </div>
            <div class="login-info asd-login-info">
                <%--<div class="frm-txt">
                    <label class="name1">
                        <Localized:LocalizedLiteral ID="lblusername" runat="server" Key="username" Colon="false" />
                        <span class="star">*</span></label>
                    <asp:TextBox ID="txt_username" runat="server" class="nametxt" Text=""></asp:TextBox>
                </div>--%>
                <div class="frm-txt">
                    <label class="name1">
                        <Localized:LocalizedLiteral ID="lblpassword1" runat="server" Key="password" Colon="false" />
                        <span class="star">*</span></label>
                    <asp:TextBox ID="txt_password" runat="server" class="nametxt" Text="" TextMode="Password"></asp:TextBox>
                </div>
                <div class="frm-txt">
                    <label class="name1">
                        <Localized:LocalizedLiteral ID="lblconfirmpassword" runat="server" Key="confirmpassword"
                            Colon="false" />
                        <span class="star">*</span></label>
                    <asp:TextBox ID="txt_ConfirmPassword" class="nametxt" runat="server" Text="" TextMode="Password"></asp:TextBox>
                </div>
            </div>
            <div class="reqtxt1">
                *
                <Localized:LocalizedLiteral ID="lblrequiredfields" runat="server" Key="requiredfields"
                    Colon="false" />
            </div>
            <div class="btns">
                <a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="back">
                    <img src="images/backarw.png" alt="" class="bkarw" />
                    <Localized:LocalizedLiteral ID="lblback" runat="server" Key="back" Colon="false" />
                </a>
                <asp:LinkButton ID="btn_submit" runat="server" CommandName="submit" class="submitbtn" OnClientClick="return validation(); "
                    OnClick="btn_submit_Click">
                    <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" />
                </asp:LinkButton>
            </div>
        </div>
        <!--midle end-->
    </div>




    <!--wrap end-->
</asp:Content>
