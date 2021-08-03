<%@ Page Title="My Account" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="Admin_MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
function checkvalidation() {
            var valid = true;
            
            

            if ($("#<%=txtUsername.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert Username.');
                $("#<%=txtUsername.ClientID%>").focus();
            }

            if ($("#<%=txtFirstname.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert Firstname.');
                $("#<%=txtFirstname.ClientID%>").focus();
            }

            if ($("#<%=txtLastname.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert Lastname.');
                $("#<%=txtLastname.ClientID%>").focus();
            }

            if ($("#<%=txtEmail.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please insert Email.');
                $("#<%=txtEmail.ClientID%>").focus();
            }

            


            return valid;
        }


        
        );

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID="Error" runat="server" style="display:none;">
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
    <span id="spanSelectedMenu" style="display: none">My Account</span>
    <%--<asp:HiddenField ID="EditMode" runat="server" Value="false" />--%>
    <div class="searchdiv">
        <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
            <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Username :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Firstname :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtFirstname" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Lastname :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtLastname" runat="server" CssClass="input_box user1"></asp:TextBox>
                        <font class="required">*</font>
                    </td>
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Email Address :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">
                        &nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input_box user1"></asp:TextBox>
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
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button_bg" OnClientClick="return checkvalidation();"
                            Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button_bg" PostBackUrl="~/Admin/dashboard.aspx"
                            CausesValidation="false" />
                    </td>
                </tr>

            </tbody>
            </table>
            </div>

</asp:Content>

