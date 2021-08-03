<%@ Page Title="eBooks" Language="C#" MasterPageFile="~/Partner/Partnermaster.master"
    AutoEventWireup="true" EnableEventValidation="true" CodeFile="AddBookIssue.aspx.cs"
    Inherits="Partner_AddBookIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />--%>
    <script src="../Datepicker/jquery.js" type="text/javascript"></script>
    <script src="../Datepicker/jquery-ui.js" type="text/javascript"></script>
    <link href="../Datepicker/jquery-ui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .MultiFile-label {
            line-height: 20px;
        }

        label {
            color: black;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var BusinessRuleID = document.getElementById('<%=hdnBookID.ClientID %>').value;
            if (BusinessRuleID != "" && BusinessRuleID != "0") {
                AddNewRow(BusinessRuleID);
            }
            else {
                AddNewRow(BusinessRuleID);
            }
        });

        function AddNewRow(BusinessRuleID) {
            //debugger;
            PageMethods.AddmultiRow(BusinessRuleID, AddPartnerSuccess, AddPartnerFail);
        }

        function AddPartnerSuccess(Result, userContext, methodName) {
            if (Result.length > 0) {
                var Count = 0;
                var str = Result.split('@');

                var str1 = str[0];
                var str2 = str[1];
                $('#countryCharge').append(str1);

            }
            else {
                AddNewRow();
            }
        }
        function AddPartnerFail() {
        }


        function ValidationConfirmed(ConfirmMessage, ErrorClass, checkAvailablepanel) {
            var found = 0;
            $('.' + ErrorClass.toString().concat("addnewmultiplerowpanel")).each(function () {
                found = found + 1;
            });
            if (found >= 1) {
                if (ConfirmMessage != '' && ErrorClass != '') {
                    var r = confirm("" + ConfirmMessage + "");
                    if (r == true) {
                        ErrorClass = ErrorClass.toString().concat("addnewmultiplerowpanel");
                        $('.' + ErrorClass).remove();
                        //calculate();
                        //remove_setTotalDeposit();
                    } else {
                    }
                }
                return false;
            }
            else {
                notyfy({
                    text: DeletevalidateMsg,
                    type: 'error',
                    dismissQueue: true,
                    layout: 'error'
                });
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function BookTypeDiv() {
            var RB1 = document.getElementById("<%=rblType.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            var label = RB1.getElementsByTagName("label");

            var rbvalue = "";
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    rbvalue = label[i].innerHTML;
                    break;
                }
            }
            if (rbvalue == "pdf") {
                document.getElementById("pdf").style.display = 'block';
                document.getElementById("epub").style.display = 'none';
            }
            else if (rbvalue == "ePub") {
                document.getElementById("pdf").style.display = 'none';
                document.getElementById("epub").style.display = 'block';
            }

            if (document.getElementById("<%=chkPapaerBook.ClientID%>").checked == true && document.getElementById("<%=chkeBook.ClientID %>").checked == false) {
                $('#bookType').hide();
                document.getElementById("pdf").style.display = 'none';
                document.getElementById("epub").style.display = 'none';
                document.getElementById("ImagePaper").style.display = 'block';
            }
            else {
                $('#bookType').show();
                document.getElementById("pdf").style.display = 'block';
                document.getElementById("epub").style.display = 'none';
                document.getElementById("ImagePaper").style.display = 'none';
            }
        }

        function Checkfiles() {
            var fup = document.getElementById('<%=fpUpload.ClientID %>');
            var fileName = fup.value;
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext == "pdf" || ext == "PDF") {
                    return true;
                }
                else {
                    $("#message-green").show().fadeOut(5000);
                    document.getElementById('succ').innerHTML = "Upload pdf only.";
                    return false;
                }
            }

            fup = document.getElementById('<%=fpUploadePub.ClientID %>');
            fileName = fup.value;
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext.toLowerCase() == "epub") {
                    return true;
                }
                else {
                    $("#message-green").show().fadeOut(5000);
                    document.getElementById('succ').innerHTML = "Upload epub only.";
                    return false;
                }
            }

            fup = document.getElementById('<%=fpUploadimage.ClientID %>');
            fileName = fup.value;
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext.toLowerCase() == "jpg" || ext.toLowerCase() == "jpeg" || ext.toLowerCase() == "png" || ext.toLowerCase() == "gif") {
                    return true;
                }
                else {
                    $("#message-green").show().fadeOut(5000);
                    document.getElementById('succ').innerHTML = "Upload image only.";
                    return false;
                }
            }
        }

        function onkeyupp() {
            return false;
        }

        function finalprice() {
            var price = document.getElementById("<%=txtPrice.ClientID %>").value;
            var discount = document.getElementById("<%=txtDiscount.ClientID %>").value;
            var dis;
            var finalPrice;
            if (discount == 0 || discount > 100) {
                document.getElementById("<%=txtFinalPrice.ClientID %>").value = parseFloat(price).toFixed(2);
            }
            else {
                dis = (price * discount) / 100;
                finalPrice = price - dis;
                document.getElementById("<%=txtFinalPrice.ClientID %>").value = finalPrice.toFixed(2);
            }
        }
        function Discount() {
            var dis = document.getElementById("<%=txtDiscount.ClientID %>").value;
            if (dis > 100) {
                alert("maximum discount 100%");
                $("#<%=txtDiscount.ClientID%>").val("0");
                $("#<%=txtDiscount.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function NoChangedis() {
            var price = document.getElementById("<%=txtPrice.ClientID %>").value;
            var dis = document.getElementById("<%=txtDiscount.ClientID %>").value;
            var fp;
            var dis1;
            if (dis != null) {
                dis1 = (price * dis) / 100;
                fp = price - dis1;
                document.getElementById("<%=txtFinalPrice.ClientID %>").value = fp;
            }
            else {
                return false;
            }
        }

        function paperBookfinalprice() {
            var price = document.getElementById("<%=txtPaperBookPrice.ClientID %>").value;
            var discount = document.getElementById("<%=txtPaperBookDiscount.ClientID %>").value;
            var dis;
            var finalPrice;
            if (discount == 0 || discount > 100) {
                document.getElementById("<%=txtFinalPaperBookPrice.ClientID %>").value = parseFloat(price).toFixed(2);
            }
            else {
                dis = (price * discount) / 100;
                finalPrice = price - dis;
                document.getElementById("<%=txtFinalPaperBookPrice.ClientID %>").value = finalPrice.toFixed(2);
            }
        }
        function paperBookDiscount() {
            var dis = document.getElementById("<%=txtPaperBookDiscount.ClientID %>").value;
            if (dis > 100) {
                alert("maximum discount 100%");
                $("#<%=txtPaperBookDiscount.ClientID%>").val("0");
                $("#<%=txtPaperBookDiscount.ClientID%>").focus();
                return false;
            }
            return true;
        }

        function paperBookNoChangedis() {
            var price = document.getElementById("<%=txtPaperBookPrice.ClientID %>").value;
            var dis = document.getElementById("<%=txtPaperBookDiscount.ClientID %>").value;
            var fp;
            var dis1;
            if (dis != null) {
                dis1 = (price * dis) / 100;
                fp = price - dis1;
                document.getElementById("<%=txtFinalPaperBookPrice.ClientID %>").value = fp;
            }
            else {
                return false;
            }
        }

        function validdate() {
            alert('Please enter valid publisher date.');
        }
        function checkvalidation() {
            var valid = true;

            if ($("#<%=ddlCategory.ClientID%>").val() == '' && valid == true) {
                valid = false;
                //alert('Please select category.');
                $("#<%=ddlCategory.ClientID%>").focus();
            }

            if ($("#<%=txtLanguage.ClientID%>").val() == '' && valid == true) {
                valid = false;
                //alert('Please insert language.');
                $("#<%=txtLanguage.ClientID%>").focus();
            }

            $(".titleClass").each(function () {
                if ($(this).val() == '' && valid == true) {
                    valid = false;
                    alert('Please insert title.');
                    $(this).focus();
                }
            });

            $(".DescriptionClass").each(function () {
                if ($(this).val() == '' && valid == true) {
                    valid = false;
                    alert('Please insert description.');
                    $(this).focus();
                }
            });

            if ($('#prc').css('display') != 'none' && $("#<%=txtPrice.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert price.');
                $("#<%=txtPrice.ClientID%>").focus();
            }

            if ($('#prcPaper').css('display') != 'none' && $("#<%=txtPaperBookPrice.ClientID%>").val() == '0' && document.getElementById("#<%=chkPapaerBook.ClientID%>").checked && valid == true) {
                valid = false;
                alert('Please insert paper book price.');
                $("#<%=txtPaperBookPrice.ClientID%>").focus();
            }

            if ($('#dis').css('display') != 'none' && $("#<%=txtDiscount.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert discount.');
                $("#<%=txtDiscount.ClientID%>").focus();
            }

            if ($('#disPaper').css('display') != 'none' && $("#<%=txtPaperBookDiscount.ClientID%>").val() == '' && document.getElementById("#<%=chkPapaerBook.ClientID%>").checked && valid == true) {
                valid = false;
                alert('Please insert paper book discount.');
                $("#<%=txtPaperBookDiscount.ClientID%>").focus();
            }

            if ($("#<%=txtDate1.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert publish date.');
                $("#<%=txtDate1.ClientID%>").focus();
            }

            if ($("#<%=txtAuthorName.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert author name.');
                $("#<%=txtAuthorName.ClientID%>").focus();
            }

            if ($("#<%=txtDealerEmail.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert dealer email.');
                $("#<%=txtDealerEmail.ClientID%>").focus();
            }

            if ($("#<%=EditMode.ClientID%>").val() == 'false') {
                if ($("#<%=fuPdfUpload.ClientID%>").val() == '' && valid == true) {
                    //valid = false;
                    //alert('Please insert pdf file.');
                    $("#<%=fuPdfUpload.ClientID%>").focus();
                }
            }
            var fup = document.getElementById('<%=fuPdfUpload.ClientID %>');
            //var fup1 = document.getElementById('<%=fuPdfUpload.ClientID %>');            
            var fileName = fup.value;
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext == "pdf" || ext == "PDF") {
                    return true;
                }
                else {
                    alert('Upload pdf only.');
                    valid = false;
                    return false;
                }
            }

            if (document.getElementById("<%=chkeBook.ClientID%>").checked == false && document.getElementById("<%=chkPapaerBook.ClientID%>").checked == false) {
                valid = false;
                alert('Please select any one type.');
                $("#<%=chkPapaerBook.ClientID%>").focus();
            }

            return valid;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57))
                return false;

            return true;
        }

        $(document).ready(function () {

            $("#<%=txtDate1.ClientID%>").datepicker({
                inline: true,
                dateFormat: "dd/mm/yy"
            });

            $("#<%=txtSpecialOfferStart.ClientID%>").datepicker({
                inline: true,
                dateFormat: "dd/mm/yy"
            });

            $("#<%=txtSpecialOfferEnd.ClientID%>").datepicker({
                inline: true,
                dateFormat: "dd/mm/yy"
            });

            $('.finalpriceclasss').bind('keyup', function () {
                return false;
            });

            $('.finalpriceclasss').bind('focus', function () {
                $('#ContentPlaceHolder1_chkIsactive').focus();
            });

            $(".close-error").click(function () {
                $('#<%=Error.ClientID%>').hide();
            });
        });

        $(document).ready(function () {
            //debugger;
            $('#prcPaper').hide();
            $('#disPaper').hide();
            $('#fpPaper').hide();
            $('#paperQty').hide();
            $('#extrabook').hide();
            $('#prc').hide();
            $('#dis').hide();
            $('#fp').hide();
            if (document.getElementById("<%=chkIsFree.ClientID %>").checked == false) {
                $('#prc').show();
                $('#dis').show();
                $('#fp').show();
                //$('#prcPaper').show();
                //$('#disPaper').show();
                //$('#fpPaper').show();
                if ($("#<%=EditMode.ClientID%>").val() == 'false') {
                    document.getElementById("<%=chkeBook.ClientID %>").checked = true;
                    //document.getElementById("< %=chkPapaerBook.ClientID %>").checked = true;
                }
            }
            if (document.getElementById("<%=chkIsPaperFree.ClientID %>").checked == false) {
                //$('#prc').show();
                //$('#dis').show();
                //$('#fp').show();
                $('#prcPaper').show();
                $('#disPaper').show();
                $('#fpPaper').show();
                if ($("#<%=EditMode.ClientID%>").val() == 'false') {
                    //document.getElementById("< %=chkeBook.ClientID %>").checked = true;
                    document.getElementById("<%=chkPapaerBook.ClientID %>").checked = true;
                }
            }
            if (document.getElementById("<%=chkPapaerBook.ClientID %>").checked == false) {
                $('#prcPaper').hide();
                $('#disPaper').hide();
                $('#fpPaper').hide();
                $('#paperQty').hide();
                $('#extrabook').hide();
                document.getElementById("<%=txtPaperBookPrice.ClientID %>").value = "";
                document.getElementById("<%=txtFinalPaperBookPrice.ClientID %>").value = "";
                document.getElementById("<%=txtPaperBookDiscount.ClientID %>").value = "";
                document.getElementById("<%=txtQuantity.ClientID %>").value = "";
            }
            if (document.getElementById("<%=chkPapaerBook.ClientID %>").checked == true) {
                $('#prcPaper').show();
                $('#disPaper').show();
                $('#fpPaper').show();
                $('#paperQty').show();
                $('#extrabook').show();
            }
            if (document.getElementById("<%=chkeBook.ClientID %>").checked == false) {
                $('#prc').hide();
                $('#dis').hide();
                $('#fp').hide();
                document.getElementById("<%=txtPrice.ClientID %>").value = "";
                document.getElementById("<%=txtFinalPrice.ClientID %>").value = "";
                document.getElementById("<%=txtDiscount.ClientID %>").value = "";
            }
            if (document.getElementById("<%=chkeBook.ClientID %>").checked == true) {
                $('#prc').show();
                $('#dis').show();
                $('#fp').show();
            }

            if (document.getElementById("<%=chkIsFree.ClientID %>").checked == true) {
                $('#prc').hide();
                $('#dis').hide();
                $('#fp').hide();
                document.getElementById("<%=txtPrice.ClientID %>").value = "";
                document.getElementById("<%=txtFinalPrice.ClientID %>").value = "";
                document.getElementById("<%=txtDiscount.ClientID %>").value = "";
                /*$('#prcPaper').hide();
                $('#disPaper').hide();
                $('#fpPaper').hide();
                document.getElementById("< %=txtPaperBookPrice.ClientID %>").value = "";
                document.getElementById("< %=txtFinalPaperBookPrice.ClientID %>").value = "";
                document.getElementById("< %=txtPaperBookDiscount.ClientID %>").value = "";*/
            }
            if (document.getElementById("<%=chkIsPaperFree.ClientID %>").checked == true) {
                /*$('#prc').hide();
                $('#dis').hide();
                $('#fp').hide();
                document.getElementById("< %=txtPrice.ClientID %>").value = "";
                document.getElementById("< %=txtFinalPrice.ClientID %>").value = "";
                document.getElementById("< %=txtDiscount.ClientID %>").value = "";*/
                $('#prcPaper').hide();
                $('#disPaper').hide();
                $('#fpPaper').hide();
                $('#paperQty').hide();
                $('#extrabook').hide();
                document.getElementById("<%=txtPaperBookPrice.ClientID %>").value = "";
                document.getElementById("<%=txtFinalPaperBookPrice.ClientID %>").value = "";
                document.getElementById("<%=txtPaperBookDiscount.ClientID %>").value = "";
                document.getElementById("<%=txtQuantity.ClientID %>").value = "";
            }
            if (document.getElementById("<%=chkSpecial.ClientID %>").checked == false) {
                $('#soStart').hide();
                $('#soEnd').hide();
                document.getElementById("<%=txtSpecialOfferStart.ClientID %>").value = "";
                document.getElementById("<%=txtSpecialOfferEnd.ClientID %>").value = "";
            }
            if (document.getElementById("<%=chkSpecial.ClientID %>").checked == true) {
                $('#soStart').show();
                $('#soEnd').show();
            }
            $("#<%=chkIsFree.ClientID %>").click(function () {
                if (document.getElementById("<%=chkIsFree.ClientID %>").checked == true) {
                    $('#prc').hide();
                    $('#dis').hide();
                    $('#fp').hide();
                    //$('#prcPaper').hide();
                    //$('#disPaper').hide();
                    //$('#fpPaper').hide();
                }
                if (document.getElementById("<%=chkIsFree.ClientID %>").checked == false) {
                    $('#prc').show();
                    $('#dis').show();
                    $('#fp').show();
                    //$('#prcPaper').show();
                    //$('#disPaper').show();
                    //$('#fpPaper').show();
                    document.getElementById("<%=chkeBook.ClientID %>").checked = true;
                    //document.getElementById("< %=chkPapaerBook.ClientID %>").checked = true;
                }
            })

            $("#<%=chkIsPaperFree.ClientID %>").click(function () {
                if (document.getElementById("<%=chkIsPaperFree.ClientID %>").checked == true) {
                    //$('#prc').hide();
                    //$('#dis').hide();
                    //$('#fp').hide();
                    $('#prcPaper').hide();
                    $('#disPaper').hide();
                    $('#fpPaper').hide();
                    $('#paperQty').hide();
                    $('#extrabook').hide();
                }
                if (document.getElementById("<%=chkIsPaperFree.ClientID %>").checked == false) {
                    //$('#prc').show();
                    //$('#dis').show();
                    //$('#fp').show();
                    $('#prcPaper').show();
                    $('#disPaper').show();
                    $('#fpPaper').show();
                    $('#paperQty').show();
                    $('#extrabook').show();
                    //document.getElementById("< %=chkeBook.ClientID %>").checked = true;
                    document.getElementById("<%=chkPapaerBook.ClientID %>").checked = true;
                }
            })

            $("#<%=chkPapaerBook.ClientID %>").click(function () {
                if (document.getElementById("<%=chkPapaerBook.ClientID %>").checked == false) {
                    $('#prcPaper').hide();
                    $('#disPaper').hide();
                    $('#fpPaper').hide();
                    $('#paperQty').hide();
                    $('#extrabook').hide();
                    document.getElementById("<%=txtFinalPaperBookPrice.ClientID %>").value = "";
                    document.getElementById("<%=txtPaperBookDiscount.ClientID %>").value = "";
                    document.getElementById("<%=txtPaperBookPrice.ClientID %>").value = "";
                }
                if (document.getElementById("<%=chkPapaerBook.ClientID %>").checked == true) {
                    if (document.getElementById("<%=chkIsPaperFree.ClientID %>").checked == false) {
                        $('#prcPaper').show();
                        $('#disPaper').show();
                        $('#fpPaper').show();
                        $('#paperQty').show();
                        $('#extrabook').show();
                    }
                }
            })

            $("#<%=chkeBook.ClientID %>").click(function () {
                if (document.getElementById("<%=chkeBook.ClientID %>").checked == false) {
                    $('#prc').hide();
                    $('#dis').hide();
                    $('#fp').hide();
                    document.getElementById("<%=txtPrice.ClientID %>").value = "";
                    document.getElementById("<%=txtFinalPrice.ClientID %>").value = "";
                    document.getElementById("<%=txtDiscount.ClientID %>").value = "";
                    $('#bookType').hide();
                    document.getElementById("pdf").style.display = 'none';
                    document.getElementById("epub").style.display = 'none';
                    document.getElementById("ImagePaper").style.display = 'block';
                }
                else {
                    $('#bookType').show();
                    document.getElementById("pdf").style.display = 'block';
                    document.getElementById("epub").style.display = 'none';
                    document.getElementById("ImagePaper").style.display = 'none';
                }
                if (document.getElementById("<%=chkeBook.ClientID %>").checked == true) {
                    if (document.getElementById("<%=chkIsFree.ClientID %>").checked == false) {
                        $('#prc').show();
                        $('#dis').show();
                        $('#fp').show();
                        $('#bookType').show();
                        document.getElementById("pdf").style.display = 'block';
                        document.getElementById("epub").style.display = 'none';
                        document.getElementById("ImagePaper").style.display = 'none';
                    }
                }
                else {
                    $('#bookType').hide();
                    document.getElementById("pdf").style.display = 'none';
                    document.getElementById("epub").style.display = 'none';
                    document.getElementById("ImagePaper").style.display = 'block';
                }
            })

            $("#<%=chkSpecial.ClientID %>").click(function () {
                if (document.getElementById("<%=chkSpecial.ClientID %>").checked == false) {
                    $('#soStart').hide();
                    $('#soEnd').hide();
                }
                if (document.getElementById("<%=chkSpecial.ClientID %>").checked == true) {
                    $('#soStart').show();
                    $('#soEnd').show();
                }
            })
        });

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Error" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td>
                        <div class="green-left">
                            <span id="ErrorMessage" runat="server"></span>
                        </div>
                        <div class="green-right">
                            <a class="close-error">
                                <img src="../images/table/icon_close_green.gif" alt="" /></a>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <span id="spanSelectedMenu" style="display: none">eBooks</span>
    <asp:HiddenField ID="EditMode" runat="server" Value="false" />
    <div class="searchdiv">
        <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                        height="5"></td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Category :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="select_box text-input"
                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Language :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtLanguage" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <%--  <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Title :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>--%>
                <%= BookTitleDescription %>


                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Book Type :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkeBook" runat="server" Text="eBook" />
                        <asp:CheckBox ID="chkPapaerBook" runat="server" Text="Paper Book" />
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Free :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkIsFree" runat="server" name="chk" Text="eBook Free" />
                        <asp:CheckBox ID="chkIsPaperFree" runat="server" Text="Paperbook Free" />
                    </td>
                </tr>

                <tr class="light_bg" id="prc">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>eBook Price :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPrice" runat="server" onkeypress="return isNumberKey(event)" OnBlur="NoChangedis();" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>

                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPrice" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr class="light_bg" id="dis">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>eBook Discount(%) :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDiscount" runat="server" onkeypress="isNumberKey(event); " CssClass="input_box user1" OnBlur="finalprice(); Discount();" MaxLength="3"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg" id="fp">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>eBook Final Price :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtFinalPrice" runat="server" CssClass="input_box finalpriceclasss user1"
                            onkeydown="return onkeyupp();"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg" id="prcPaper">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Paper Book Price :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPaperBookPrice" runat="server" onkeypress="return isNumberKey(event)" OnBlur="paperBookNoChangedis();" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg" id="disPaper">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Paper Book Discount(%) :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPaperBookDiscount" runat="server" onkeypress="return isNumberKey(event)" OnBlur="paperBookfinalprice(); paperBookDiscount();" MaxLength="3" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg" id="fpPaper">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Paper Book Final Price :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtFinalPaperBookPrice" runat="server" CssClass="input_box finalpriceclasss user1"
                            onkeydown="return onkeyupp();"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg" id="paperQty">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Paper Book Quantity :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="input_box user1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr id="extrabook">
                    <td colspan="3">
                        <table style="width:100%">
                            <tr class="light_bg">
                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                    <strong>Paper Book Weight(KG) :</strong>
                                </td>
                                <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                                </td>
                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                    <asp:TextBox ID="txtWeight" runat="server" CssClass="input_box user1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <font class="required">*</font>
                                </td>
                            </tr>
                            <tr class="light_bg">
                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                    <strong>Paper Book DimWeight(KG) :</strong>
                                </td>
                                <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                                </td>
                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                    <asp:TextBox ID="txtDimWeight" runat="server" CssClass="input_box user1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <font class="required">*</font>
                                </td>
                            </tr>
                            <tr class="light_bg">
                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                    <strong>Paper Book Width(CM) :</strong>
                                </td>
                                <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                                </td>
                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                    <asp:TextBox ID="txtWidth" runat="server" CssClass="input_box user1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <font class="required">*</font>
                                </td>
                            </tr>
                            <tr class="light_bg">
                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                    <strong>Paper Book Height(CM) :</strong>
                                </td>
                                <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                                </td>
                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                    <asp:TextBox ID="txtHeight" runat="server" CssClass="input_box user1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <font class="required">*</font>
                                </td>
                            </tr>
                            <tr class="light_bg">
                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                    <strong>Paper Book Depth(CM) :</strong>
                                </td>
                                <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                                </td>
                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                    <asp:TextBox ID="txtDepth" runat="server" CssClass="input_box user1" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <font class="required">*</font>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Active :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkIsactive" runat="server" />
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Editor’s choice :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkIsFeartued" runat="server" />
                    </td>
                </tr>
                <tr class="light_bg" style="display: none;">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Top Seller :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkIsSpecial" runat="server" />
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Publish Date :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <%--         <asp:TextBox ID="txtDate" runat="server" CssClass="input_box user1 hasDatepicker"
                            Text=""></asp:TextBox>--%>
                        <asp:TextBox ID="txtDate1" runat="server" CssClass="input_box"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg" style="display: none;">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Description image No.</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDescriptionPages" runat="server" Text="1" CssClass="input_box user1"></asp:TextBox><%-- Add Multiple description image comma (,) separated.--%>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Select Book type :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:RadioButtonList ID="rblType" runat="server" onchange="BookTypeDiv();" RepeatColumns="2" RepeatDirection="Vertical">
                            <asp:ListItem Selected="True">pdf</asp:ListItem>
                            <asp:ListItem>ePub</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Select eBook :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <div id="pdf" style="display: block">
                            <asp:FileUpload ID="fuPdfUpload" runat="server" CssClass="file_1" />
                        </div>
                        <div id="epub" style="display: none">
                            <asp:FileUpload ID="fpUploadePub" runat="server" CssClass="file_1" />
                            Epub File
                            <br />
                            <asp:FileUpload ID="fpUploadimage" runat="server" CssClass="file_1" Style="margin-top: 5px;" />
                            Epub Cover Image
                        </div>
                        <div id="ImagePaper" style="display:none">
                            <asp:FileUpload ID="fpImagePaper" runat="server" CssClass="file_1" Style="margin-top: 5px;" />
                            Paper Book Cover Image
                        </div>
                        <asp:Panel ID="pnl2" runat="server" Style="display: none;">
                            <asp:FileUpload ID="fpUpload" runat="server" CssClass="file_1 multi accept-jpg|jpeg|png|PNG|jpeg|JPEG max-10" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="popup_listing_border" align="right" valign="middle" width="130"></td>
                    <td></td>
                    <td>
                        <a href="" id="a_book" runat="server">
                            <asp:Image ID="imgbook" Width="100px" Height="100px" runat="server" Visible="false" />
                        </a>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Author name :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtAuthorName" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Dealer Email :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDealerEmail" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg" style="display: none;">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Order Index :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:DropDownList ID="ddlOrderIndex" runat="server" CssClass="input_box user1"></asp:DropDownList>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Special Offer :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkSpecial" runat="server" />
                    </td>
                </tr>
                <tr class="light_bg" id="soStart">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Special Offer Start :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtSpecialOfferStart" runat="server" CssClass="input_box"></asp:TextBox>
                    </td>
                </tr>
                <tr class="light_bg" id="soEnd">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Special Offer End :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtSpecialOfferEnd" runat="server" CssClass="input_box"></asp:TextBox>
                    </td>
                </tr>
                <tr class="light_bg" style="display: none;">
                    <td align="left" colspan="3">
                        <table style="width: 50%; margin-left: 15%;">
                            <tr>
                                <th style="text-align: left;">Country</th>
                                <th style="text-align: left;">Charge</th>
                                <th style="text-align: left;"></th>
                            </tr>
                        </table>
                        <table id="countryCharge" style="width: 50%; margin-left: 15%;"></table>
                    </td>
                </tr>
                <tr class="light_bg pnl3" style="display: none;">
                    <td align="right">
                        <strong>Explorer pdf start page No.</strong>
                    </td>
                    <td align="left" height="37">&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPDFStartPage" CssClass="input_box user1" runat="server" Text="1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="light_bg pnl3" style="display: none;">
                    <td align="right">
                        <strong>Explorer pdf end page No.</strong>
                    </td>
                    <td align="left" height="37">&nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPDFEndPage" CssClass="input_box user1" runat="server" Text="1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="light_bg" id="trimg" runat="server">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong></strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:DataList ID="dlImages" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"
                            CellSpacing="5" CellPadding="5" ShowHeader="false" OnItemCommand="dlImages_ItemCommand">
                            <ItemTemplate>
                                <table border="0" cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td style="margin-left: 10px;">
                                            <asp:Image ID="img1" runat="server" ImageUrl='<%# "~/Magagine/"+Eval("ID")+"/"+Eval("Images") %>'
                                                Width="60" Height="60" BorderStyle="None" />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/images/delete.png"
                                                BorderStyle="None" CommandArgument='<%# Eval("ID") %>' CommandName="delete1" />
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr class="white_bg">
                    <td align="right">&nbsp;
                    </td>
                    <td align="left" height="37">&nbsp;
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button_bg" OnClientClick="return checkvalidation();"
                            Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button_bg" PostBackUrl="~/Partner/ManageBook.aspx"
                            CausesValidation="false" />
                        <asp:HiddenField ID="hdnBookID" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
