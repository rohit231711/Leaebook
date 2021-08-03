<%@ Page Title="Delivery Address" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="OrderAddress.aspx.cs" Inherits="OrderAddress" %>

<%@ Register Src="~/Includes/account_leftmenu.ascx" TagName="leftmenu" TagPrefix="lm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">



        function toggle_visibility(id) {
            var e = document.getElementById(id);
            if (e.style.display == 'block') {
                e.style.display = 'none';
            }
            else {
                e.style.display = 'block';
                document.getElementById("<%=txtname.ClientID%>").focus();
                document.getElementById("<%=txtname.ClientID%>").scrollIntoView();
            }
        }

        function validation() {
            debugger;
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if (document.getElementById('<%=txtname.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca su nombre.");
                }
                else {
                    alert("Please enter name.");
                }
                return false;
            }
            if (document.getElementById('<%=txtaddress.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca la dirección.");
                }
                else {
                    alert("Please enter address.");
                }
                return false;
            }

            if (document.getElementById('<%=txtlandmark.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Introduzca el punto de referencia.");
                }
                else {
                    alert("Please enter landmark.");
                }
                return false;
            }

            if (document.getElementById('<%=ddlregion.ClientID %>').value == "Select") {
                if (cultureLanguage == "es-ES") {
                    alert("Seleccione región.");
                }
                else {
                    alert("Please select region.");
                }
                return false;
            }

            if (document.getElementById('<%=ddlCountry.ClientID %>').value == "Select") {
                if (cultureLanguage == "es-ES") {
                    alert("Seleccione el país.");
                }
                else {
                    alert("Please select country.");
                }
                return false;
            }

             if (document.getElementById('<%=txtstate.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Introduzca el estado.");
                }
                else {
                    alert("Please enter state.");
                }
                return false;
            }

            if (document.getElementById('<%=txtcity.ClientID %>') == null)
            {
                if (document.getElementById('<%=ddlcityi.ClientID %>').value == "Select")
                {
                    if (cultureLanguage == "es-ES") {
                        alert("Seleccione la ciudad.");
                    }
                    else {
                        alert("Please select city.");
                    }
                    return false;
                }
            }
            else
            {
                if (document.getElementById('<%=txtcity.ClientID %>').value == "")
                {
                if (cultureLanguage == "es-ES") {
                    alert("Introduzca la ciudad.");
                }
                else {
                    alert("Please enter city.");
                }
                return false;
                }
            }

           
            /*if (document.getElementById('< %=txtcountry.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca país.");
                }
                else {
                    alert("Please enter country.");
                }
                return false;
            }*/
            if (document.getElementById('<%=txtpincode.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca el código PIN.");
                }
                else {
                    alert("Please enter pincode.");
                }
                return false;
            }
            if (document.getElementById('<%=txtPhone.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca el número de teléfono.");
                }
                else {
                    alert("Please enter phone number.");
                }
                return false;
            }
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57))
                return false;

            return true;
        }

        $(document).ready(function () {
            $("#<%= txtPhone.ClientID %>").keydown(function (e) {
                //console.log(e.keyCode);
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: Ctrl+C
                    (e.keyCode == 67 && e.ctrlKey === true) ||
                    // Allow: Ctrl+X
                    (e.keyCode == 88 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (e.keyCode == 107) ||
                    (e.keyCode == 109) ||
                    (e.keyCode == 61 && e.shiftKey === true) ||
                    (e.keyCode == 187 && e.shiftKey === true) ||
                    (e.keyCode == 189) ||
                    (e.keyCode == 57 && e.shiftKey === true) ||
                    (e.keyCode == 48 && e.shiftKey === true) ||
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="#">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
                </a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblorderHistory" runat="server" Key="orderHistory" Colon="false" />
                </a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="accbox">
            <lm:leftmenu ID="leftmenu1" runat="server" />
            <div class="orderacc-rt" style="margin-top: 30px;">

                <div class="logintxt">
                    <h1>
                        <Localized:LocalizedLiteral ID="lblcheckout1" runat="server" Key="deliveryAdd" Colon="false" /></h1>
                </div>
                <asp:Label ID="lblDefaultMessage" runat="server" Text="No data found" Style="font-family: abeezeeregular; color: Gray;" Visible="false">
                </asp:Label>
                <%--<a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="contbtn">
                <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="Continuetoshopping" Colon="false" /></a>--%>



                <div class="check-box" runat="server" id="chkout">

                    <div class="ebook-title1">
                    </div>

                    <div class="ebook-box1">

                        <asp:Repeater ID="rptRecords1" runat="server" OnItemCommand="del">
                            <ItemTemplate>
                                <div class="row-new">
                                    <div class="first1" style="width: 516px;">
                                        <div class="booktxt">
                                            <asp:HiddenField ID="hfAddID" runat="server" Value='<%#Eval("BookDeliveryAddID")%>' />
                                            <asp:Label runat="server" ID="lblName" class="bname"><%#Eval("Name")%></asp:Label>
                                            <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("Name")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblStreetAddress" class="aname"><%#Eval("StreetAddress")%></asp:Label>
                                            <asp:TextBox ID="txtStreetAddress" runat="server" Text='<%#Eval("StreetAddress")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblLandmark" class="aname"><%#Eval("Landmark")%></asp:Label>
                                            <asp:TextBox ID="txtLandmark" runat="server" Text='<%#Eval("Landmark")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblCity" class="aname" Text='<%#Eval("City")%>'></asp:Label>
                                            <asp:Label runat="server" ID="lblState" class="aname"><%#Eval("State")%></asp:Label>
                                            

                                            <%-- <asp:Label ID="lblreg" runat="server" CssClass="aname" Text='<%#Eval("Region")%>'></asp:Label>--%>
                                            <asp:DropDownList ID="ddlregrpt" AutoPostBack="true" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged" runat="server" Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:DropDownList>

                                            <asp:Label ID="lblCountry" runat="server" CssClass="aname" Text='<%#Eval("Country")%>'></asp:Label>
                                            <asp:DropDownList ID="ddlCountryRpt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryRpt_SelectedIndexChanged" Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:DropDownList>

                                            <asp:TextBox ID="txtState" runat="server" Text='<%#Eval("State")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>

                                            <asp:Label runat="server" ID="lblCityCode" class="aname"><%#Eval("City") + " - " + Eval("Pincode")%></asp:Label>

                                            <%-- <asp:DropDownList ID="ddlcity" runat="server" Text='<%#Eval("City")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:DropDownList>--%>
                                            <asp:DropDownList ID="txtCity" runat="server"  Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:DropDownList>
                                             <asp:TextBox ID="txtcity12" runat="server" Text='<%#Eval("City")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                          <div style="float: right;">  <asp:LinkButton ID="lnk_vis" runat="server" Text="Other" OnClick="lnk_vis_Click" Visible="false"></asp:LinkButton></div>

                                            <asp:TextBox ID="txtPincode" runat="server" Text='<%#Eval("Pincode")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <span class="aname"></span>
                                            <span class="bname">
                                                <asp:Label runat="server" ID="lblPhoneNumber" Text='<%#Convert.ToString(Eval("PhoneNumber"))%>'></asp:Label>
                                                <asp:TextBox runat="server" ID="txtPhoneNumber" Text='<%#Convert.ToString(Eval("PhoneNumber"))%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        </div>
                                        <input type="hidden" value="0" id="hdnDeleteValue" />
                                    </div>
                                    <div class="delbtn2">
                                        <asp:LinkButton Visible="false" ID="lnkUpdate" runat="server" CommandName="update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BookDeliveryAddID") %>'>
                                        <img src="images/updateicon2.gif" alt="update" />
                                        </asp:LinkButton>
                                        <asp:LinkButton Visible="false" ID="lnkCancel" runat="server" CommandName="cancel">
                                        <img src="images/cancel.png" alt="cancel" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" type="button" CommandName="edit" CommandArgument='<%#Eval("BookDeliveryAddID")%>' runat="server" Text="Edit" Style="margin: 0 10px;">
                                        <img src="images/edit.png" alt="edit" />
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton1" type="button" OnClientClick='<%# getRemoveString() %>' CommandName="delete" CommandArgument='<%#Eval("BookDeliveryAddID")%>' runat="server" Text="Delete">
                                        <img src="images/delete.png" alt="delete" />
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>

                        </asp:Repeater>

                    </div>
                </div>

                <div class="check-box">
                    <div>
                        <asp:LinkButton ID="btn_login" runat="server" class="movebtn" Style="display: none;">
                            Add
                        <Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="deliveryAdd" Colon="false" />
                        </asp:LinkButton><a id="aClick" class="movebtn" onclick="toggle_visibility('dialog');" style="cursor: pointer;">Add
                            <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="deliveryAdd" Colon="false" />
                        </a>
                    </div>
                </div>
            </div>

            <div id="dialog" class="wrap" style="display: none;" align="center">
                <div class="loginbox1">
                    <div class="perinfo" style="text-align: left;">
                        <Localized:LocalizedLiteral ID="lblpersonalinformation" runat="server" Key="deliveryAdd"
                            Colon="false" />
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="lblfirstname" runat="server" Key="shipName" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtname" runat="server" class="nametxt" Text=""></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1" style="width: 100%;">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="shipAdd1" Colon="false" />
                            (<Localized:LocalizedLiteral ID="LocalizedLiteral20" runat="server" Key="shipAdd1_Detail" Colon="false" />)
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtaddress" runat="server" class="nametxt" TextMode="MultiLine" Text=""></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1" style="width: 100%;">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="shipAdd2" Colon="false" />
                            (<Localized:LocalizedLiteral ID="LocalizedLiteral21" runat="server" Key="shipAdd2_Detail" Colon="false" />)
                        </label>
                        <asp:TextBox ID="txtlandmark" runat="server" class="nametxt" Text=""></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1" style="width: 100%;">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral16" runat="server" Key="Region" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:DropDownList ID="ddlregion" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged1" AutoPostBack="true" runat="server" CssClass="nametxt" AppendDataBoundItems="true"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtcountry" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>--%>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral10" runat="server" Key="country" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" runat="server" CssClass="nametxt" AppendDataBoundItems="true"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtcountry" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>--%>
                    </div>

                    <div class="frm-txt">
                        <label class="name1" style="width: 100%;">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral9" runat="server" Key="shipState" Colon="false" />
                            <span class="star">*</span>
                        </label>
                        <asp:TextBox ID="txtstate" runat="server" class="nametxt" Text=""></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key="city" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:DropDownList ID="ddlcityi" CssClass="nametxt" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcityi_SelectedIndexChanged"></asp:DropDownList>
                        <asp:TextBox ID="txtcity" runat="server" Visible="false" class="nametxt" Text=""></asp:TextBox>
                        <div style="float: right; display: none">
                            <asp:LinkButton ID="lnkother" runat="server" Text="Other" OnClick="lnkother_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral11" runat="server" Key="pincode" Colon="false" />
                        </label>
                        <asp:TextBox ID="txtpincode" runat="server" class="nametxt" Text="" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral12" runat="server" Key="Phone" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtPhone" runat="server" class="nametxt" Text=""></asp:TextBox>
                    </div>
                    <br />
                    <br />
                    <div class="btns">
                        <a onclick="toggle_visibility('dialog');" style="cursor: pointer;" class="back">
                            <img src="images/backarw.png" alt="" class="bkarw" />
                            Hide <%--<Localized:LocalizedLiteral ID="lblback" runat="server" Key="back" Colon="false" />--%></a><asp:LinkButton ID="btn_submit" runat="server" CommandName="submit" class="submitbtn" OnClientClick="return validation(); "
                                OnClick="btn_submit_Click">
                                <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" />
                            </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
