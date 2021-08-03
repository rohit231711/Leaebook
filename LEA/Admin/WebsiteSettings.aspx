<%@ Page Title="Website Setting" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="WebsiteSettings.aspx.cs" Inherits="Admin_WebsiteSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function alertMe() {

            $("#message-green").show().fadeOut(5000);
            document.getElementById('succ').innerHTML = "Updated Successfully";
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <strong>FaceBookLink :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtFaceBook" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>TwiterLink :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtTwiter" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>GoogleLink :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtGoogle" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>PinterestLink :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtPinterest" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>InstagramLink :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtInstagram" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Coutact Us :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtContactus" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Book Store Phone :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtBookStorePhone" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Base URL :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtBaseURL" runat="server" CssClass="input_box user1"></asp:TextBox>
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

