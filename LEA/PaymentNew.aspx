<%@ Page Title="Payment" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="PaymentNew.aspx.cs" Inherits="PaymentNew" %>

<script runat="server">


</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .body {
            font-family: sans-serif;
            font-weight: normal;
            margin: 10px;
            color: #999;
            background-color: #eee;
        }

        .form {
            margin: 40px 0;
        }

        .div {
            clear: both;
            /*margin: 0 0px;*/
        }

        .body > label {
            width: 237px;
            border-radius: 3px;
            border: 1px solid #D1D3D4;
            /*background-color: #6CB1FF;
    color: #424949;
    font-weight: 500;
        height: 23px;*/
        }

        /* hide input */
        .body > input[type=radio]:empty {
            display: none;
        }

        /* style label */
        .body > label {
            position: relative;
            float: left;
            line-height: 2.5em;
            text-indent: 3.25em;
            margin-top: 2em;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            .body > label:before {
                position: absolute;
                display: block;
                top: 0;
                bottom: 0;
                left: 0;
                content: '';
                width: 2.5em;
                background: #D1D3D4;
                border-radius: 3px 0 0 3px;
            }

        /* toggle hover */
        .body > input[type=radio]:hover:not(:checked) ~ label:before {
            content: '\2714';
            text-indent: .9em;
            color: #C2C2C2;
        }

        .body > input[type=radio]:hover:not(:checked) ~ label {
            color: #888;
        }

        /* toggle on */
        .body > input[type=radio]:checked ~ label:before {
            content: '\2714';
            text-indent: .9em;
            color: white;
            background-color: #6CB1FF;
        }

        .body > input[type=radio]:checked ~ label {
            color: #777;
        }

        /* radio focus */
        .body > input[type=radio]:focus ~ label:before {
            box-shadow: 0 0 0 3px #999;
        }

        .RadioButtonList {
            font-size: 1.4em;
            color: #243C7A;
            padding-left: 6px;
            width: 100%;
            line-height: 1.4em;
            margin: 2px 25px 0 0;
        }
    </style>
    <script type="text/javascript">

        function val(chk) {
            debugger;
            var elements = document.getElementsByClassName("shippingLabel");
            var dis = "";
            if (chk.firstElementChild.checked) {
                dis = "block";
            } else {
                dis = "none";
            }

            for (var i = 0; i < elements.length; i++) {
                elements[i].style.display = dis;
            }

            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i];
                if (elm.type == 'radio') {
                    if (elm.checked) {
                        var chkdhl = document.getElementById('<%= chkdhl.ClientID %>');
                        var x = document.getElementById('<%= lblAmount1.ClientID %>').innerText;
                        var y = "0";//document.getElementById(elm.id.replace('chkDeliver', 'hfAmount')).value;
                        var z = document.getElementById('<%= lblShipAmount1.ClientID %>').innerText;
                        var dhl = document.getElementById('<%= lbldhl.ClientID %>').innerText;

                        if (chkdhl.checked == false) {
                                <%--document.getElementById('<%= lblShipAmount.ClientID %>').innerText = "Shipping & Handling (Shipping DHL - Express World Wide) : $ " + (parseFloat(y)).toFixed(2);--%>
                            document.getElementById('<%= lblAmount.ClientID %>').innerText = (parseFloat(x) + parseFloat(y) + parseFloat(z)).toFixed(2);
                            break;
                        }
                        else {
                            document.getElementById('<%= lblAmount.ClientID %>').innerText = (parseFloat(x) + parseFloat(y) + parseFloat(dhl)).toFixed(2);
                            break;
                        }
                    }
                }
            }
        }

        $(document).ready(
            function () {

                for (i = 0; i < document.forms[0].elements.length; i++) {
                    elm = document.forms[0].elements[i];
                    if (elm.type == 'checkbox') {
                        if (elm.checked) {
                            var chkdhl = document.getElementById('<%= chkdhl.ClientID %>');
                            var x = document.getElementById('<%= lblAmount1.ClientID %>').innerText;
                            var y = "0";//document.getElementById(elm.id.replace('chkDeliver', 'hfAmount')).value;
                            var z = document.getElementById('<%= lblShipAmount1.ClientID %>').innerText;
                            var dhl = document.getElementById('<%= lbldhl.ClientID %>').innerText;

                            if (chkdhl.checked == false) {
                                <%--document.getElementById('<%= lblShipAmount.ClientID %>').innerText = "Shipping & Handling (Shipping DHL - Express World Wide) : $ " + (parseFloat(y)).toFixed(2);--%>
                                document.getElementById('<%= lblAmount.ClientID %>').innerText = (parseFloat(x) + parseFloat(y) + parseFloat(z)).toFixed(2);
                                break;
                            }
                            else {
                                document.getElementById('<%= lblAmount.ClientID %>').innerText = (parseFloat(x) + parseFloat(y) + parseFloat(dhl)).toFixed(2);
                                break;
                            }
                        }
                    }
                }


            });
        function SetSingleRadioButton(nameregex, current) {
            debugger;
            re = new RegExp(nameregex);
            for (i = 0; i < document.forms[0].elements.length; i++) {
                elm = document.forms[0].elements[i];
                if (elm.type == 'checkbox') {
                    if (elm != current) {
                        elm.checked = false;
                    }
                }
            }
            current.checked = true;
            var dhl = document.getElementById('<%= chkdhl.ClientID %>');
            var x = document.getElementById('<%= lblAmount1.ClientID %>').innerText;
            var y = "0";//document.getElementById(nameregex.replace('chkDeliver', 'hfAmount')).value;
            var z = document.getElementById('<%= lblShipAmount1.ClientID %>').innerText;
                <%--document.getElementById('<%= lblShipAmount.ClientID %>').innerText = "Shipping & Handling (Shipping DHL - Express World Wide) : $ " + (parseFloat(y)).toFixed(2);--%>
            document.getElementById('<%= lblAmount.ClientID %>').innerText = (parseFloat(x) + parseFloat(y) + parseFloat(z)).toFixed(2);
        }

        function toggle_visibility(id) {
            debugger;
            var e = document.getElementById(id);
            if (e.style.display == 'block') {
                e.style.display = 'none';
                $('.loginbox1').scrollTop(0);
                return false;
            }
            else {
                e.style.display = 'block';
                document.getElementById("<%=txtname.ClientID%>").focus();
                document.getElementsByClassName("loginbox1").scrollIntoView();
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
                <%--if (document.getElementById('<%=txtlandmark.ClientID %>').value == "") {
                    if (cultureLanguage == "es-ES") {
                        alert("Por favor, introduzca hito.");
                    }
                    else {
                        alert("Please enter landmark.");
                    }
                    return false;
                }--%>

            if (document.getElementById('<%=ddlregion.ClientID %>').value == "Select Region") {
                if (cultureLanguage == "es-ES") {
                    alert("Seleccione región.");
                }
                else {
                    alert("Please select region.");
                }
                return false;
            }

            if (document.getElementById('<%=ddlCountry.ClientID %>').value == "Select Country") {
                if (cultureLanguage == "es-ES") {
                    alert("Seleccione el país.");
                }
                else {
                    alert("Please select country.");
                }
                return false;
            }

                <%--if (document.getElementById('<%=txtstate.ClientID %>').value == "") {
                    if (cultureLanguage == "es-ES") {
                        alert("Por favor, introduzca estado.");
                    }
                    else {
                        alert("Please enter state.");
                    }
                    return false;
                }--%>

            if (document.getElementById('<%=txtcity.ClientID %>') == null) {
                if (document.getElementById('<%=ddlcityi.ClientID %>').value == "Select City") {
                    if (cultureLanguage == "es-ES") {
                        alert("Seleccione la ciudad.");
                    }
                    else {
                        alert("Please select city.");
                    }
                    return false;
                }
            }
            else {
                if (document.getElementById('<%=txtcity.ClientID %>').value == "") {
                    if (cultureLanguage == "es-ES") {
                        alert("Introduzca la ciudad.");
                    }
                    else {
                        alert("Please enter city.");
                    }
                    return false;
                }
            }

            if (document.getElementById('<%=txtpincode.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca el código PIN.");
                }
                else {
                    alert("Please enter Zipcode.");
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
            else {
                if (cultureLanguage == "es-ES") {
                    $("#ContentPlaceHolder1_btn_submit").html('Tratamiento');
                    return true;
                    $('#ContentPlaceHolder1_btn_submit').removeAttr("disabled");
                }
                else {
                    $("#ContentPlaceHolder1_btn_submit").html('Processing');

                    //$('#ContentPlaceHolder1_btn_submit').removeAttr("disabled");
                    $('#ContentPlaceHolder1_btn_submit').css('cursor', 'not-allowed');
                    $('#ContentPlaceHolder1_btn_submit').attr('disabled', true);
                }
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

        function btnCheckout() {
            debugger;

            <%--if (document.getElementById('<%=Addredd.ClientID%>').css("Block")) {
                return true;
                alert('First');
            }--%>
            //if (document.getElementById("book").innerText == "eBook") {
            //    return true;
            //}
            var isEbookAll = true;
            if ($(".book").length) {
                for (var i = 0; i < $(".book").length; i++) {
                    var bookText = $(".book")[i].textContent;
                    if (bookText != "eBook") {
                        //return true;
                        isEbookAll = false;
                    } else {

                    }
                }
            }
            if (isEbookAll == true) {
                return true;
            }
            else {
                var chkdhl = document.getElementById('<%= chkdhl.ClientID %>');
                if (chkdhl.checked == true) {
                    if (document.getElementById('<%= lbldhl1.ClientID %>').innerHTML.trim() == '$ 0') {
                        if (document.getElementById('<%=lblDefaultMessage.ClientID%>').innerHTML.trim() != 'No address found') {
                            alert("Please Postal Code in Address.");
                        }
                        if (document.getElementById('<%=lblDefaultMessage.ClientID%>').innerHTML.trim() == 'No address found') {
                            alert("Please Add Delievery Address.");
                            $('#dialog').css('display', 'block');
                        }
                        return false;

                    }
                }

                var chknational = document.getElementById('<%= chknational.ClientID %>');
                if (chknational.checked == true) {
                    if (document.getElementById('<%= lblShipAmount.ClientID %>').innerHTML.trim() == '$ 0') {
                        if (document.getElementById('<%=lblDefaultMessage.ClientID%>').innerHTML.trim() != 'No address found') {
                            alert("Please Postal Code in Address.");
                        }
                        if (document.getElementById('<%=lblDefaultMessage.ClientID%>').innerHTML.trim() == 'No address found') {
                            alert("Please Add Delievery Address.");
                            $('#dialog').css('display', 'block');
                        }
                        return false;
                    }
                }
            }
        }
    </script>
    <!-- Event snippet for Lead conversion page
In your html page, add the snippet and call gtag_report_conversion when someone clicks on the chosen link or button. -->

    <!-- Pixel code-->
    <!-- Facebook Pixel Code -->
    <script>
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                    n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script',
            'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '468506476975848');
        fbq('track', 'PageView');
        fbq('track', 'InitiateCheckout');
    </script>
    <noscript>
        <img height="1" width="1" style="display: none"
            src="https://www.facebook.com/tr?id=468506476975848&ev=PageView&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->


    <script type="text/javascript">
        function gtag_report_conversion(url) {
            var callback = function () {
                if (typeof (url) != 'undefined') {
                    window.location = url;
                }
            };
            gtag('event', 'conversion', {
                'send_to': 'AW-781010452/ri0mCMCqm4wBEJSMtfQC',
                'event_callback': callback
            });
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                   <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/
                </li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblcheckout" runat="server" Key="deliveryAdd" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <div class="wrap">

        <div class="loginbox1" style="padding-bottom: 20px">

            <div class="logintxt" style="display: none;">
                <h1>
                    <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="cart" Colon="false" /></h1>
            </div>
            <asp:Label ID="Label1" runat="server" Text="No data found" Style="font-family: abeezeeregular; color: Gray;" Visible="false">
            </asp:Label>
            <div id="aBack" runat="server">
                <a class="contbtn" href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="Continuetoshopping" Colon="false" /></a>
            </div>
            <div class="check-box" runat="server" id="Div1">

                <div class="ebook-title1">
                    <div class="book-new">
                        <%--<Localized:LocalizedLiteral ID="LocalizedLiteral15" runat="server" Key="lbl_book" Colon="false" />--%>
                        <asp:Label runat="server" ID="lbl_bok" Text="Book"></asp:Label>
                    </div>
                    <div class="edit2">
                        <%-- <Localized:LocalizedLiteral ID="LocalizedLiteral13" runat="server" Key="Edit" Colon="false" />--%>
                        <asp:Label runat="server" ID="Label3" Text="Remove"></asp:Label>
                    </div>
                    <div class="pricetxt2">
                        <%-- <Localized:LocalizedLiteral ID="LocalizedLiteral14" runat="server" Key="Price" Colon="false" />--%>
                        <asp:Label runat="server" ID="Label2" Text="Price"></asp:Label>
                    </div>
                </div>
                <asp:TextBox ID="txt_Url" runat="server" Visible="false"></asp:TextBox>
                <div class="ebook-box1">
                    <asp:UpdatePanel ID="upRepeter" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="delCart" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="row-new">
                                        <div class="first1">
                                            <asp:HiddenField ID="hdCartID" runat="server" Value='<%# Eval("OrderID") %>' />
                                            <asp:HiddenField ID="hdBookID" runat="server" Value='<%# Eval("BookID") %>' />
                                            <asp:HiddenField ID="hdPaper" runat="server" Value='<%# Eval("IspaperBook") %>' />
                                            <%--<img src="Book/<%#(Eval("CategoryID"))%>/<%#(Eval("ImagePath"))%>" alt="" class="wishimg1" style = "height:135px; width:118px;"/>--%>
                                            <%--<asp:LinkButton ID="abc"   runat="server" CommandName="cart" CommandArgument='<%#Convert.ToString(Eval("Title"))%>'>--%>
                                            <asp:LinkButton ID="LinkButton3" CommandName="delete1" CommandArgument='<%# Eval("BookID") %>' runat="server">
                                            <img src="<%# PicturePath("Book/"+Eval("CategoryID")+"/"+Eval("ImagePath").ToString().Replace(".jpg","_1.jpg")) %>" alt="" class="wishimg1" style="height: 135px; width: 98px;" id="img" />
                                            </asp:LinkButton>

                                            <div class="booktxt">
                                                <asp:LinkButton ID="LinkButton4" CommandName="Title" CommandArgument='<%# Eval("BookID") %>' runat="server">
                                                    <span id="spantitle" runat="server" class="aname"><%#Convert.ToString(Eval("Title"))%></span>
                                                </asp:LinkButton>
                                                <asp:TextBox ID="txt_Title" runat="server" Text='<%#Convert.ToString(Eval("Title"))%>' Visible="false"></asp:TextBox>
                                                <span class="bname"><%#Convert.ToString(Eval("Autoher"))%></span>
                                                <span class="bname">
                                                    <asp:Label runat="server" ID="order">
                                                        <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="orderid" Colon="false" /></asp:Label>:
                                                    <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("OrderNo")%>'></asp:Label></span><span class="bname"><Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="lbl_book" Colon="false" />
                                                        <asp:Label ID="lblBookType" CssClass="book" runat="server" Text='<%# getKeyFromBook(Convert.ToBoolean(Eval("IspaperBook")),Convert.ToBoolean(Eval("IseBook"))) %>' Style="display: none"></asp:Label><Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key='<%# getKeyFromBook(Convert.ToBoolean(Eval("IspaperBook")),Convert.ToBoolean(Eval("IseBook"))) %>' Colon="false" />
                                                    </span>
                                                <span class="bname" runat="server" visible='<%# Eval("IspaperBook") %>'>Quantity :
                                                    <asp:TextBox ID="txtQuanitity" runat="server" Text='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) %>' onkeypress="return isNumberKey(event)" min='<%# getMinQuantity(Eval("BookID").ToString()) %>' max='<%# getMaxQuantity(Eval("BookID").ToString()) %>' ToolTip='<%# getMaxQuantity(Eval("BookID").ToString()) %>' type="number" Visible='<%# Eval("IspaperBook") %>'
                                                        Style="width: 15%; padding: 5px 0px 5px 5px;" onkeydown="return onkeyupp();" ReadOnly="true" AutoPostBack="true" OnTextChanged="txtQuanitity_TextChanged" Enabled='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) >= 1?true:false%>'></asp:TextBox><asp:HiddenField ID="hfQuantity" runat="server" Value='<%# Eval("Qauntity") %>' />

                                                    <%--<asp:Label ID="lblstock" runat="server" Text="Sorry Book Out Of Stock" ForeColor="Red" Visible='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) >= 1?false:true %>'></asp:Label>--%>
                                                </span>
                                                <span class="bname" runat="server" visible='<%# Eval("IspaperBook") %>'>
                                                    <Localized:LocalizedLabel ID="llblStock" runat="server" CssClass="bname" Key="BookNotInStock" Colon="false" ForeColor="Red" Visible='<%# getQuantity(Eval("BookID").ToString(),Eval("Qauntity").ToString()) >= 1?false:true %>'></Localized:LocalizedLabel></span><div class="rate">
                                                        <img src="images/<%#Convert.ToString(Eval("Ratting"))%>_star.png" id="img_rate1" alt="" height="35px;" />
                                                        <span class="ratetxt" style="margin-top: 9px; margin-left: 5px;">(<%#Convert.ToString(Eval("TotalReview"))%><asp:Label ID="tr" runat="server">
                                                            <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="rating" Colon="false" /></asp:Label>) </span>
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
                                            <asp:LinkButton ID="btnRemoveCart" OnClientClick='<%# getRemoveString() %>' CommandName="delete" CommandArgument='<%#Eval("BookID")+"@"+Eval("IseBook")+"@"+Eval("IsPaperbook")%>' runat="server" Text="Delete">
                                                <img src="images/delete.png" alt="" class="heartimg" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="ptxt">
                                            <asp:Label runat="server" ID="lblsymbol" Text='<%# Convert.ToBoolean(Eval("IseBook")) && Convert.ToBoolean(Eval("IsFree")) == true ? "Free" : "$" %>'></asp:Label><asp:Label runat="server" ID="lblPrice" Text='<%#  /*Convert.ToBoolean(Eval("IsFree")) == true ? "" :*/ Eval("FinalCartPrice1") %>' />
                                            <asp:Label runat="server" ID="lblOriginalPrice" Style="display: none;" Text='<%# Convert.ToBoolean(Eval("IsFree")) == true ? "0.00" : Eval("FinalCartPrice1") %>' />
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
        <div class="loginbox1" style="margin-top: 0px; margin-bottom: 20px;">
            <div id="serdiv" runat="server">
                <asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div style="display: none">
                            <%-- style="display:none" --%>
                            <asp:RadioButtonList Width="100%" CssClass="" ID="rblTest" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblTest_SelectedIndexChanged" runat="server">
                                <asp:ListItem Text="Economic" class="div form body" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="International" class="div form body" Value="2"></asp:ListItem>
                                <asp:ListItem Text="International EMS-Currier" class="div form body" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Special" Value="4" class="div form body"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- <div class="div form body">                
                </div>--%>
            </div>
            <div class="logintxt" style="margin-top: 30px; display: none" id="Delivery" runat="server">
                <h1>
                    <Localized:LocalizedLiteral ID="lblcheckout1" runat="server" Key="deliveryAdd" Colon="false" /></h1>
            </div>
            <div class="check-box" style="margin-top: 5px; display: none" id="Addredd" runat="server">
                <div>
                    <a id="aClick" class="movebtn" onclick="toggle_visibility('dialog');" style="cursor: pointer; margin: 0;">Add
                        <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="deliveryAdd" Colon="false" />
                    </a>
                </div>
                <asp:Label ID="lblDefaultMessage" runat="server" Text="No data found" Style="font-family: abeezeeregular; color: Gray;" Visible="false">
                </asp:Label>
            </div>
            <div class="check-box" runat="server" id="chkout">
                <div class="ebook-title1"></div>
                <div class="ebook-box1">
                    <asp:Repeater ID="rptRecords1" runat="server" OnItemCommand="del" OnItemDataBound="rptRecords1_ItemDataBound">
                        <ItemTemplate>
                            <div class="row-new">
                                <div class="first1">
                                    <div class="booktxt" style="width: auto;">
                                        <asp:HiddenField ID="hfAddID" runat="server" Value='<%#Eval("BookDeliveryAddID")%>' />

                                        <asp:Label ID="locallizationname" runat="server" Visible="false" class="name1" Style="width: 100%">
                                            <Localized:LocalizedLiteral ID="lblfirstname" runat="server" Key="shipName" Colon="false" /></asp:Label><asp:Label runat="server" ID="lblName" class="bname"><%#Eval("Name")%></asp:Label><asp:TextBox ID="txtName" runat="server" Text='<%#Eval("Name")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox><asp:Label runat="server" ID="lblStreetAddress" class="aname"><%#Eval("StreetAddress")%></asp:Label><asp:Label ID="locallizationaddress1" runat="server" class="name1" Style="width: 100%" Visible="false">
                                                <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="shipAdd1" Colon="false" />
                                                (<Localized:LocalizedLiteral ID="LocalizedLiteral20" runat="server" Key="shipAdd1_Detail" Colon="false" />)</asp:Label><asp:TextBox ID="txtStreetAddress" runat="server" Text='<%#Eval("StreetAddress")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox><asp:Label runat="server" ID="lblLandmark" class="aname"><%#Eval("Landmark")%></asp:Label><asp:Label ID="locallizationaddress2" runat="server" class="name1" Style="width: 100%" Visible="false">
                                                    <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="shipAdd2" Colon="false" />
                                                    (<Localized:LocalizedLiteral ID="LocalizedLiteral21" runat="server" Key="shipAdd2_Detail" Colon="false" />)
                                                </asp:Label><asp:TextBox ID="txtLandmark" runat="server" Text='<%#Eval("Landmark")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox><asp:Label runat="server" ID="lblCity" class="aname" Text='<%#Eval("City")%>'></asp:Label><%-- <asp:TextBox ID="txtCity" runat="server" Text='<%#Eval("City")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:TextBox>--%><asp:Label runat="server" ID="lblState" class="aname"><%#Eval("State")%></asp:Label><asp:Label ID="lblCountry" runat="server" CssClass="aname" Text='<%#Eval("Country")%>'></asp:Label><asp:Label ID="locallizationregion" runat="server" Visible="false" class="name1" Style="width: 100%">
                                                    <Localized:LocalizedLiteral ID="LocalizedLiteral16" runat="server" Key="Region" Colon="false" /></asp:Label><asp:DropDownList ID="ddlregrpt" OnSelectedIndexChanged="ddlregrpt_SelectedIndexChanged" AutoPostBack="true" runat="server" Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:DropDownList>
                                        <asp:Label ID="locallizationcountry" runat="server" Visible="false" class="name1" Style="width: 100%">
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral22" runat="server" Key="country" Colon="false" /></asp:Label><asp:DropDownList ID="ddlCountryRpt" AutoPostBack="true" OnSelectedIndexChanged="ddlCountryRpt_SelectedIndexChanged" runat="server" Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;"></asp:DropDownList>
                                        <asp:Label ID="locallizationstate" runat="server" Visible="false" class="name1" Style="width: 100%">
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral28" runat="server" Key="state" Colon="false" />
                                        </asp:Label><asp:TextBox ID="txtState" runat="server" Text='<%#Eval("State")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;">
                                        </asp:TextBox><asp:Label ID="locallizationcity" runat="server" Visible="false" class="name1" Style="width: 100%">
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key="city" Colon="false" />
                                        </asp:Label><asp:DropDownList ID="txtCity" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="txtCity_SelectedIndexChanged" CssClass="nametxt" Style="margin-bottom: 5px;">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtcity12" runat="server" Text='<%#Eval("City")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;">
                                        </asp:TextBox><div style="float: right; display: none">
                                            <asp:LinkButton ID="lnk_vis" runat="server" Text="Other" OnClick="lnk_vis_Click" Visible="false">
                                            </asp:LinkButton>
                                        </div>
                                        <asp:Label runat="server" ID="lblCityCode" class="aname"><%# Eval("City").ToString().Replace("- 00000","") + (Convert.ToString(Eval("Pincode")) != "00000" ? (" - " + Eval("Pincode")) : "") %>
                                        </asp:Label><asp:Label ID="locallizationpincode" runat="server" Visible="false" class="name1" Style="width: 100%">
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral11" runat="server" Key="pincode" Colon="false" />
                                        </asp:Label><asp:TextBox ID="txtPincode" runat="server" Text='<%#Eval("Pincode")%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;" onkeypress="return isNumberKey(event)">
                                        </asp:TextBox><span class="aname"></span><span class="bname"><asp:Label runat="server" ID="lblPhoneNumber" Text='<%#Convert.ToString(Eval("PhoneNumber"))%>'>
                                        </asp:Label><asp:Label ID="locallizationPhone" runat="server" Visible="false" class="name1" Style="width: 100%">
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral12" runat="server" Key="Phone" Colon="false" />
                                        </asp:Label><asp:TextBox runat="server" ID="txtPhoneNumber" Text='<%#Convert.ToString(Eval("PhoneNumber"))%>' Visible="false" CssClass="nametxt" Style="margin-bottom: 5px;">
                                        </asp:TextBox></span><span class="bname" style="display: none;"><asp:Label runat="server" ID="lblShipping" CssClass="shippingLabel">
                                        </asp:Label></span>
                                    </div>
                                    <input type="hidden" value="0" id="hdnDeleteValue" />
                                </div>
                                <div class="delbtn2">
                                    <asp:LinkButton Visible="false" ID="lnkUpdate" runat="server" CommandName="update" CssClass="contbtn" Style="margin: 5px;" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BookDeliveryAddID") %>'>
                                        <Localized:LocalizedLiteral runat="server" Key="update" Colon="false"></Localized:LocalizedLiteral>
                                    </asp:LinkButton><asp:LinkButton Visible="false" ID="lnkCancel" runat="server" CommandName="cancel" CssClass="contbtn" Style="margin: 5px;">
                                        <Localized:LocalizedLiteral runat="server" Key="cancel" Colon="false"></Localized:LocalizedLiteral>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton2" type="button" CommandName="edit" CommandArgument='<%#Eval("BookDeliveryAddID")%>' runat="server" Text="Edit" Style="margin: 0 10px;">
                                        <img src="images/edit.png" alt="edit" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDeleteAddress" type="button" OnClientClick='<%#getRemoveString()%>' CommandName="deleteAddress" CommandArgument='<%#Eval("BookDeliveryAddID")%>' runat="server" Text="Delete">
                                        <img src="images/delete.png" alt="Delete" />
                                    </asp:LinkButton>
                                </div>
                                <div class="ptxt">
                                    <asp:LinkButton ID="lnkDeliver" runat="server" CssClass="contbtn" OnClick="lnkDeliver_Click" CommandArgument='<%#Eval("BookDeliveryAddID")%>' Style="display: none;">
                                        <Localized:LocalizedLabel ID="lblDeliver" runat="server" Key="deliverHere" Colon="false"></Localized:LocalizedLabel>
                                    </asp:LinkButton><asp:CheckBox ID="chkDeliver" AutoPostBack="true" OnCheckedChanged="chkDeliver_CheckedChanged" runat="server" />


                                    <%--  OnClientClick='<%# BindData1() %>' --%>
                                    <asp:HiddenField ID="hfAmount" runat="server" />
                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>
                </div>

            </div>

            <asp:UpdatePanel ID="upAmount" runat="server">
                <ContentTemplate>
                    <div class="asd-order-box">
                        <h2>
                            <Localized:LocalizedLiteral ID="LocalizedLiteral19" runat="server" Key="yourorder" Colon="false" /></h2>
                        <div class="asd-order-tbl">
                            <table width="100%">
                                <tr>
                                    <th><b>
                                        <Localized:LocalizedLiteral ID="LocalizedLiteral27" runat="server" Key="product" Colon="false" /></b></th>
                                    <th><b>
                                        <Localized:LocalizedLiteral ID="LocalizedLiteral25" runat="server" Key="total" Colon="false" /></b></th>
                                </tr>
                                <tr>
                                    <td style="width: 65%;">
                                        <asp:Literal ID="lblProduct" runat="server"></asp:Literal></td>
                                    <td>
                                        <asp:Literal ID="lblProductTotal" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral26" runat="server" Key="subtotal" Colon="false" />
                                        </b>
                                    </td>
                                    <td>
                                        <b>
                                            <asp:Literal ID="lblSubtotalAmount" runat="server"></asp:Literal></b></td>
                                </tr>
                                <tr id="Shipping" runat="server">
                                    <td>
                                        <b>
                                            <%--<Localized:LocalizedLiteral ID="LocalizedLiteral24" runat="server" Key="shipping" Colon="false" />--%>
                                            Shipping
                                        </b>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:RadioButton ID="chkdhl" runat="server" GroupName="Shiping" onchange="val(this);" Style="padding: 0px" />
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral18" runat="server" Key="DHL_shipping_Amount" Colon="false" />&nbsp; <b>
                                                <asp:Label ID="lbldhl1" runat="server">
                                                </asp:Label><br />
                                                <asp:RadioButton ID="chknational" runat="server" onchange="val(this);" GroupName="Shiping" />
                                                <Localized:LocalizedLiteral ID="LocalizedLiteral17" runat="server" Key="national_shipping_amount" Colon="false" />&nbsp; <b>
                                                    <asp:Label ID="lblShipAmount" runat="server" CssClass="Shipping">
                                                    </asp:Label><asp:Label ID="lblShipAmount1" runat="server" Style="display: none"></asp:Label><asp:Label ID="lbldhl" Text="0" runat="server" Style="display: none">
                                                    </asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="total" Colon="false" /></b>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblitems" runat="server" Style="display: none"></asp:Label><%--&nbsp;&nbsp;&nbsp;--%><b>$<asp:Label ID="lblAmount" runat="server" Colon="false" />
                                            <asp:Label ID="lblAmount1" runat="server" Style="display: none" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--<div class="logintxt" style="margin-top: 30px;">
                <h1>
                    <Localized:LocalizedLiteral ID="LocalizedLiteral19" runat="server" Key="shippingMethod" Colon="false" />
                </h1>
                <br />
                <h4 style="font-family: 'abeezeeregular'; font-size: 15px; font-weight: normal;">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral22" runat="server" Key="shippingDesc" Colon="false" />
                </h4>
            </div>--%>

            <%--<asp:UpdatePanel ID="upAmount" runat="server">
                <ContentTemplate>
                    <div class="w100">
                        <div style="float: right; margin-top: 50px; color: #616161; margin-right: 10%">
                            <asp:RadioButton ID="chkdhl" runat="server" GroupName="Shiping" onchange="val(this);" Style="padding: 0px" />
                            <asp:CheckBox ID="chkdhl" onchange="val(this);" runat="server" />
                            Text="DHL Shipping Amount"
                            <Localized:LocalizedLiteral ID="LocalizedLiteral18" runat="server" Key="DHL_shipping_Amount" Colon="false" />
                            <br />
                            <asp:RadioButton ID="chknational" runat="server" onchange="val(this);" GroupName="Shiping" />
                            <Localized:LocalizedLiteral ID="LocalizedLiteral17" runat="server" Key="national_shipping_amount" Colon="false" />
                            :&nbsp;
                            <asp:Label ID="lblShipAmount" runat="server"></asp:Label>
                            <asp:Label ID="lblShipAmount1" runat="server" Style="display: none"></asp:Label>
                            <asp:Label ID="lbldhl" Text="0" runat="server" Style="display: none"></asp:Label>
                        </div>
                    </div>
                    <div class="w100">
                        <div class="totalpnl">
                            <div class="totalbtn">
                                <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="total" Colon="false" />
                            </div>
                            <div class="ptxt1">
                                <asp:Label ID="lblitems" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp; $
                                <asp:Label ID="lblAmount" runat="server" Colon="false" />
                                <asp:Label ID="lblAmount1" runat="server" Style="display: none" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>--%>

            <div>
                <asp:LinkButton ID="btn_login" runat="server" class="movebtn" OnClientClick="return btnCheckout();" OnClick="btn_login_Click">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="checkout" Colon="false" />
                </asp:LinkButton>
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
                            (<Localized:LocalizedLiteral ID="LocalizedLiteral20" runat="server" Key="shipAdd1_Detail" Colon="false" />) <span class="star">*</span></label>
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
                        <asp:DropDownList ID="ddlregion" OnSelectedIndexChanged="ddlregion_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="nametxt" AppendDataBoundItems="true"></asp:DropDownList>
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
                        </label>
                        <asp:TextBox ID="txtstate" runat="server" class="nametxt" Text=""></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key="city" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:DropDownList ID="ddlcityi" CssClass="nametxt" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcityi_SelectedIndexChanged"></asp:DropDownList>
                        <asp:TextBox ID="txtcity" runat="server" Visible="false" class="nametxt" Text=""></asp:TextBox><div style="float: right; display: none">
                            <asp:LinkButton ID="lnkother" runat="server" Text="Other" OnClick="lnkother_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="llPincode" runat="server" Key="pincode" Colon="false" />
                        </label>
                        <asp:TextBox ID="txtpincode" runat="server" class="nametxt" Text="" onkeypress="return isNumberKey(event)"></asp:TextBox>
                    </div>
                    <div class="frm-txt">
                        <label class="name1">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral12" runat="server" Key="Phone" Colon="false" />
                            <span class="star">*</span></label>
                        <asp:TextBox ID="txtPhone" runat="server" class="nametxt" Text=""></asp:TextBox>
                    </div>
                    <div class="frm-txt" style="margin-bottom: 17px;">
                        <label class="name1" style="width: 100%;">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral23" runat="server" Key="add_address_text" Colon="false" />
                        </label>
                    </div>
                    <br />
                    <br />
                    <div class="btns" runat="server" id="btn">
                        <a onclick="toggle_visibility('dialog');" style="cursor: pointer;" class="back">
                            <img src="images/backarw.png" alt="" class="bkarw" />
                            <Localized:LocalizedLiteral ID="lblback" runat="server" Key="hide" Colon="false" /></a>
                        <asp:LinkButton ID="btn_submit" runat="server" CommandName="submit" class="submitbtn" OnClientClick="return validation();"
                            OnClick="btn_submit_Click" data-loading-text="<i class='fa fa-circle-o-notch fa-spin'></i> Processing">
                            <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" />
                        </asp:LinkButton><asp:Label ID="lbl_loader" runat="server" Text="Please Wait..." Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
