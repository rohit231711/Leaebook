<%@ Page Title="DHL Quote Settings" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="DHLQuoteSettings.aspx.cs" Inherits="Admin_DHLQuoteSettings" %>

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

                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>From Country Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtFromCountryCode" runat="server" CssClass="input_box user1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>From Postal Code/City :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <style>
                            label {
                                color: black;
                            }
                        </style>
                        <asp:RadioButtonList runat="server" ID="rblCode" RepeatColumns="3" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">Postal Code</asp:ListItem>
                            <asp:ListItem Value="2">City</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <asp:TextBox ID="txtFromCodeCity" runat="server" CssClass="input_box user1"></asp:TextBox>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Payment Country Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPaymentCountryCode" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Ready Time :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtReadyTime" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Ready Time GMT Offset :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtReadyTimeGMTOffset" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Dimension Unit :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDimensionUnit" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Weight Unit :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtWeightUnit" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Payment Account Number :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPaymentAccountNumber" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Is Dutiable :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtIsDutiable" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Network Type Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtNetworkTypeCode" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Global Product Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtGlobalProductCode" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Local Product Code :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtLocalProductCode" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Special Service Type :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtSpecialServiceType" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Declared Currency :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDeclaredCurrency" runat="server" CssClass="input_box user1"></asp:TextBox>

                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Declared Value :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtDeclaredValue" runat="server" CssClass="input_box user1"></asp:TextBox>
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
