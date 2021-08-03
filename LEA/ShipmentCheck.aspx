<%@ Page Title="Payment" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="ShipmentCheck.aspx.cs" Inherits="ShipmentCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
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
                    alert("Por favor, introduzca hito.");
                }
                else {
                    alert("Please enter landmark.");
                }
                return false;
            }
            if (document.getElementById('<%=txtcity.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca la ciudad.");
                }
                else {
                    alert("Please enter city.");
                }
                return false;
            }
            if (document.getElementById('<%=txtstate.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca estado.");
                }
                else {
                    alert("Please enter state.");
                }
                return false;
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
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblcheckout" runat="server" Key="deliveryAdd" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <div class="wrap">

        <div class="loginbox1">

            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="checkout" Colon="false" /></h1>
            </div>
            <asp:Label ID="Label1" runat="server" Text="No data found" Style="font-family: abeezeeregular; color: Gray;" Visible="false">
            </asp:Label>
            <a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="contbtn">
                <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="Continuetoshopping" Colon="false" /></a>



            <div class="check-box" runat="server" id="Div1">

                <div class="ebook-title1">
                    <div class="book-new">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral15" runat="server" Key="lbl_book" Colon="false" />
                    </div>
                    <div class="edit2">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral13" runat="server" Key="Edit" Colon="false" />
                    </div>
                    <div class="pricetxt2">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral14" runat="server" Key="Price" Colon="false" />
                    </div>
                </div>

                <div class="ebook-box1">
                    <asp:UpdatePanel ID="upRepeter" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="delCart">
                                <ItemTemplate>
                                    <div class="row-new">
                                        <div class="first1">
                                            <asp:HiddenField ID="hdCartID" runat="server" Value='<%# Eval("OrderID") %>' />
                                            <asp:HiddenField ID="hdBookID" runat="server" Value='<%# Eval("BookID") %>' />
                                            <asp:HiddenField ID="hdPaper" runat="server" Value='<%# Eval("IspaperBook") %>' />
                                            <%--<img src="Book/<%#(Eval("CategoryID"))%>/<%#(Eval("ImagePath"))%>" alt="" class="wishimg1" style = "height:135px; width:118px;"/>--%>
                                            <img src="<%# PicturePath("Book/"+Eval("CategoryID")+"/"+Eval("ImagePath").ToString().Replace(".jpg","_1.jpg")) %>" alt="" class="wishimg1" style="height: 135px; width: 98px;" />
                                            <div class="booktxt">
                                                <span class="bname"><%#Convert.ToString(Eval("Title"))%></span>
                                                <span class="aname"><%#Convert.ToString(Eval("Autoher"))%></span>
                                                <span class="bname">
                                                    <asp:Label runat="server" ID="order">
                                                        <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="orderid" Colon="false" /></asp:Label>
                                                    : 
                                            <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("OrderNo")%>'></asp:Label>
                                                </span>
                                                <span class="bname">
                                                    <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="lbl_book" Colon="false" />
                                                    <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key='<%# getKeyFromBook(Convert.ToBoolean(Eval("IspaperBook")),Convert.ToBoolean(Eval("IseBook"))) %>' Colon="false" />
                                                </span>
                                                <span class="bname" runat="server" visible='<%# Eval("IspaperBook") %>'>Quantity :
                                                    <asp:TextBox ID="txtQuanitity" runat="server" Text='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) %>' onkeypress="return isNumberKey(event)" min='<%# getMinQuantity(Eval("BookID").ToString()) %>' max='<%# getMaxQuantity(Eval("BookID").ToString()) %>' type="number" Visible='<%# Eval("IspaperBook") %>'
                                                        Style="width: 15%; padding: 5px 0px 5px 5px;" onkeydown="return onkeyupp();" AutoPostBack="true" OnTextChanged="txtQuanitity_TextChanged" Enabled='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) >= 1?true:false%>'></asp:TextBox>
                                                    <%--<asp:Label ID="lblstock" runat="server" Text="Sorry Book Out Of Stock" ForeColor="Red" Visible='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) >= 1?false:true %>'></asp:Label>--%>
                                                </span>
                                                <span class="bname" runat="server" visible='<%# Eval("IspaperBook") %>'>
                                                    <Localized:LocalizedLabel ID="llblStock" runat="server" CssClass="bname" Key="BookNotInStock" Colon="false" ForeColor="Red" Visible='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) >= 1?false:true %>'></Localized:LocalizedLabel>
                                                </span>
                                                <div class="rate">
                                                    <img src="images/<%#Convert.ToString(Eval("Ratting"))%>_star.png" id="img_rate1" alt="" height="35px;" />
                                                    <span class="ratetxt" style="margin-top: 9px; margin-left: 5px;">(<%#Convert.ToString(Eval("TotalReview"))%>
                                                        <asp:Label ID="tr" runat="server">
                                                            <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="rating" Colon="false" /></asp:Label>)
                                                    </span>
                                                </div>
                                                <div class="movetxt">
                                                    <asp:LinkButton ID="move" OnClientClick='<%# getWishString() %>' CommandName="move" CommandArgument='<%#Eval("BookID")%>' runat="server" Text="Move">
                                                        <img src="images/heart.png" alt="" class="heartimg" /><Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="movetowishlist" Colon="false" />
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <input type="hidden" value="0" id="hdnDeleteValue" />
                                        </div>
                                        <div class="delbtn2">
                                            <asp:LinkButton ID="LinkButton1" type="button" OnClientClick='<%# getRemoveString() %>' CommandName="delete" CommandArgument='<%#Eval("BookID")+"@"+Eval("IseBook")+"@"+Eval("IsPaperbook")%>' runat="server" Text="Delete"><img src="images/delete.png" alt="" /></asp:LinkButton>
                                        </div>
                                        <div class="ptxt">
                                            <asp:Label runat="server" ID="lblsymbol" Text='<%#Convert.ToBoolean(Eval("IsFree")) == true ? "Free" : "$" %>'></asp:Label>
                                            <asp:Label runat="server" ID="lblPrice" Text='<%#Convert.ToBoolean(Eval("IsFree")) == true ? "0.00" : Eval("FinalCartPrice1")%>' />
                                            <asp:Label runat="server" ID="lblOriginalPrice" Style="display: none;" Text='<%#Convert.ToBoolean(Eval("IsFree")) == true ? "0.00" : Eval("FinalCartPrice1")%>' />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>

        </div>

    </div>

    <div class="wrap">

        <div class="loginbox1" style="margin-top: 30px;">

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
                    <%--<div class="book-new">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral50" runat="server" Key="lbl_book" Colon="false" />
                    </div>
                    <div class="edit2">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="Edit" Colon="false" />
                    </div>
                    <div class="pricetxt2">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="Price" Colon="false" />
                    </div>--%>
                </div>

                <div class="ebook-box1">

                    <asp:Repeater ID="rptRecords1" runat="server" OnItemCommand="del" OnItemDataBound="rptRecords1_ItemDataBound">
                        <ItemTemplate>
                            <div class="row-new">
                                <div class="first1">
                                    <div class="booktxt">
                                        <asp:HiddenField ID="hfAddID" runat="server" Value='<%#Eval("BookDeliveryAddID")%>' />
                                        <asp:Label runat="server" ID="lblName" class="bname"><%#Eval("Name")%></asp:Label>
                                        <asp:TextBox ID="txtName" runat="server" Text='<%#Eval("Name")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                        <asp:Label runat="server" ID="lblStreetAddress" class="aname"><%#Eval("StreetAddress")%></asp:Label>
                                        <asp:TextBox ID="txtStreetAddress" runat="server" Text='<%#Eval("StreetAddress")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                        <asp:Label runat="server" ID="lblLandmark" class="aname"><%#Eval("Landmark")%></asp:Label>
                                        <asp:TextBox ID="txtLandmark" runat="server" Text='<%#Eval("Landmark")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                        <asp:Label runat="server" ID="lblCity" class="aname"><%#Eval("City")%></asp:Label>
                                        <asp:Label runat="server" ID="lblState" class="aname"><%#Eval("State")%></asp:Label>
                                        <asp:TextBox ID="txtState" runat="server" Text='<%#Eval("State")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                        <asp:Label ID="lblCountry" runat="server" CssClass="aname" Text='<%#Eval("Country")%>'></asp:Label>
                                        <asp:DropDownList ID="ddlCountryRpt" runat="server" Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:DropDownList>
                                        <asp:Label runat="server" ID="lblCityCode" class="aname"><%#Eval("City") + " - " + Eval("Pincode")%></asp:Label>
                                        <asp:TextBox ID="txtCity" runat="server" Text='<%#Eval("City")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>
                                        <asp:TextBox ID="txtPincode" runat="server" Text='<%#Eval("Pincode")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <span class="aname"></span>
                                        <span class="bname">
                                            <asp:Label runat="server" ID="lblPhoneNumber" Text='<%#Convert.ToString(Eval("PhoneNumber"))%>'></asp:Label>
                                            <asp:TextBox runat="server" ID="txtPhoneNumber" Text='<%#Convert.ToString(Eval("PhoneNumber"))%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <div class="rate">
                                                <Localized:LocalizedLabel runat="server" ID="lblChargeAddress" Colon="false" Key="chargeWithAddress"></Localized:LocalizedLabel>
                                                : $
                                                <asp:Label ID="lblShipping" runat="server" Text="0"></asp:Label>
                                            </div>
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
                                <div class="ptxt">
                                    <asp:LinkButton ID="lnkDeliver" runat="server" CssClass="contbtn" OnClick="lnkDeliver_Click" CommandArgument='<%#Eval("BookDeliveryAddID")%>'>
                                        <Localized:LocalizedLabel ID="lblDeliver" runat="server" Key="deliverHere" Colon="false"></Localized:LocalizedLabel>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>

                </div>

                <asp:UpdatePanel ID="upAmount" runat="server">
                    <ContentTemplate>
                        <div class="w100">
                            <div class="totalpnl">
                                <div class="totalbtn">
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="total" Colon="false" />
                                </div>
                                <div class="ptxt1">
                                    <asp:Label ID="lblitems" runat="server"></asp:Label>
                                    $<asp:Label ID="lblAmount" runat="server" Colon="false" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="check-box">
                <div>
                    <asp:LinkButton ID="btn_login" runat="server" class="movebtn" Style="display: none;">
                        Add
                        <Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="deliveryAdd" Colon="false" />
                    </asp:LinkButton>
                    <a id="aClick" class="movebtn" onclick="toggle_visibility('dialog');" style="cursor: pointer;">Add
                        <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="deliveryAdd" Colon="false" />
                    </a>
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
                            <Localized:LocalizedLiteral ID="lblfirstname" runat="server" Key="name" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtname" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="address" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtaddress" runat="server" class="nametxt" TextMode="MultiLine" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="landmark" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtlandmark" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key="city" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtcity" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral9" runat="server" Key="state" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtstate" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral10" runat="server" Key="country" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="nametxt" AppendDataBoundItems="true"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtcountry" runat="server" class="nametxt" Text="" onkeypress="return onlyAlphabets(event,this);"></asp:TextBox>--%>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral11" runat="server" Key="pincode" Colon="false" />
                            <span class="star">*</span></label>
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
                            Hide
                            <%--<Localized:LocalizedLiteral ID="lblback" runat="server" Key="back" Colon="false" />--%>
                        </a>
                        <asp:LinkButton ID="btn_submit" runat="server" CommandName="submit" class="submitbtn" OnClientClick="return validation(); "
                            OnClick="btn_submit_Click">
                            <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" />
                        </asp:LinkButton>
                    </div>

                </div>
            </div>

        </div>

    </div>

</asp:Content>

