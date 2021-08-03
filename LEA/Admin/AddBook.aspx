<%@ Page Title="Add Book" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="AddBook.aspx.cs" Inherits="Admin_AddBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .MultiFile-label
        {
            line-height: 20px;
        }
    </style>
    <script type="text/javascript">
        function validdate() {
            alert('Please enter valid publisher date.');
        }
        $(document).ready(function () {
            $("#<%=btnSubmit.ClientID %>").click(function () {
                $("#<%=ddlCategory.ClientID%>").addClass("validate[required]]");
                $("#<%=ddlCategory.ClientID%>").addClass("validate[required]]");
                $("#<%=txtTitle.ClientID%>").addClass("validate[required]]");
                $("#<%=ddlCountry.ClientID%>").addClass("validate[required]]");
                $("#<%=ddlLanguage.ClientID%>").addClass("validate[required]]");
                $("#<%=txtDesc.ClientID%>").addClass("validate[required]]");
                $("#<%=txtPrice.ClientID%>").addClass("validate[required]]");
                $("#<%=txtDiscount.ClientID%>").addClass("validate[required]]");
                $("#<%=txtPublicsher.ClientID%>").addClass("validate[required]]");
                $("#<%=txtDate.ClientID%>").addClass("validate[required]]");
            });

            $("#<%=txtDiscount.ClientID %>").bind('focusout', function () {
                var Price = $("#<%=txtPrice.ClientID %>").val();
                var DiscountAmount = $("#<%=txtDiscount.ClientID %>").val();
                if (parseFloat(Price) > 0) {
                    $("#<%=txtSubscriptioinPrice.ClientID%>").val(Price);
                }
                if (parseFloat(Price) > 0 && parseFloat(DiscountAmount) > 0) {
                    var Amount = parseFloat(Price) * parseFloat(DiscountAmount);
                    $("#<%=txtSubscriptioinPrice.ClientID%>").val(parseFloat(Price) - (parseFloat(Amount) / 100));
                }
            });

            $("#<%=txtPrice.ClientID %>").bind('focusout', function () {
                var Price = $("#<%=txtPrice.ClientID %>").val();
                var DiscountAmount = $("#<%=txtDiscount.ClientID %>").val();
                if (parseFloat(Price) > 0) {
                    $("#<%=txtSubscriptioinPrice.ClientID%>").val(Price);
                }
                if (parseFloat(Price) > 0 && parseFloat(DiscountAmount) > 0) {
                    var Amount = parseFloat(Price) * parseFloat(DiscountAmount);
                    $("#<%=txtSubscriptioinPrice.ClientID%>").val(parseFloat(Price) - (parseFloat(Amount) / 100));
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="spanSelectedMenu" style="display: none">aBook</span>
    <div class="searchdiv">
        <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                        height="5">
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Book Category :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="select_box " AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                        <font class="required">*</font>
                    </td>
                </tr>
         
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Title :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="input_box"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Country :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="select_box ">
                        </asp:DropDownList>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Language :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="select_box ">
                        </asp:DropDownList>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Desscription :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Height="100" Width="400"
                            CssClass="input_box"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Price :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPrice" runat="server" ReadOnly="true" CssClass="input_box" Text="0"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Active :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkIsactive" runat="server" />
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Free :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkIsFree" runat="server" />
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Featured :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:CheckBox ID="chkIsFeartued" runat="server" />
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Discounted Price (%):</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="input_box"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDiscount"
                            FilterType="Custom" ValidChars="0123456789.">
                        </cc1:FilteredTextBoxExtender>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Publisher :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPublicsher" runat="server" CssClass="input_box"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Publish Date :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="input_box" AutoPostBack="true"
                            OnDataBinding="txtDate_DataBinding" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDate">
                        </cc1:CalendarExtender>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Publisher Logo :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:FileUpload ID="fuPdfUpload" runat="server" CssClass="file_1" />
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Total Subscribtion Price :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtSubscriptioinPrice" Enabled="false" runat="server" CssClass="input_box"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="white_bg">
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="left" height="37">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" OnClientClick="return validate();" runat="server" CssClass="button_bg"
                            Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="Button1" Text="Cancel" runat="server" CssClass="button_bg" PostBackUrl="~/Admin/ManageBook.aspx" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
