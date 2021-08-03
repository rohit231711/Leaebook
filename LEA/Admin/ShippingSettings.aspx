<%@ Page Title="Shipping Settings" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ShippingSettings.aspx.cs" Inherits="Admin_ShippingSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function alertMe() {

            $("#message-green").show().fadeOut(5000);
            document.getElementById('succ').innerHTML = "Updated Successfully";
            return false;
        }
    </script>
    <style type="text/css">
        .input_box {
            width: 250px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="EditMode" runat="server" Value="false" />
    <div class="searchdiv">
        <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center" height="5"></td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Site ID :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtSiteID" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Password :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Account Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperAccountNumber" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Billing Account Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtBillingAccountNumber" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Duty Account Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDutyAccountNumber" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Company Name :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>AddressLine1 :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>AddressLine2 :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtAddressLine2" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>AddressLine3 :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtAddressLine3" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>City :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtCity" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Postal Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Country Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtCountryCode" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Country Name :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtCountryName" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Person Name :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPersonName" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Phone Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Phone Extension :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPhoneExtension" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Fax Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Telex :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtTelex" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Email :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper ID :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperID" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Company Name :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperCompanyName" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Registered Account :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperRegisteredAccount" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper AddressLine1 :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperAddressLine1" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper AddressLine2 :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperAddressLine2" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper City :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperCity" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Postal Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperPostalCode" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Country Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperCountryCode" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Country Name :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperCountryName" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Person Name :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperPersonName" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Phone Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperPhoneNumber" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Phone Extension :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperPhoneExtension" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Fax Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperFaxNumber" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Telex :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperTelex" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Shipper Email :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtShipperEmail" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="white_bg">
                    <td align="right">&nbsp;
                    </td>
                    <td align="left" height="37">&nbsp;
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button_bg" Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button_bg" PostBackUrl="~/Admin/dashboard.aspx"
                            CausesValidation="false" />
                    </td>
                </tr>

            </tbody>
        </table>
    </div>
</asp:Content>
