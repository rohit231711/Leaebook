<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="PasswordChange.aspx.cs" Inherits="Admin_PasswordChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function validate() {

            //alert(document.getElementById('<%=txtoldpassword.ClientID %>').value);
            //alert(document.getElementById('<%=hdnPassword.ClientID %>').value);
            if (document.getElementById('<%=txtoldpassword.ClientID %>').value == "") {
                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please enter old password.";
                document.getElementById('<%=txtoldpassword.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtnewpassword.ClientID %>').value == "") {
                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please enter new password.";
                document.getElementById('<%=txtnewpassword.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtconfirmpassword.ClientID %>').value == "") {
                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please enter confirm password.";
                document.getElementById('<%=txtconfirmpassword.ClientID %>').focus();
                return false;
            }

            
            if (document.getElementById('<%=txtnewpassword.ClientID %>').value != "" && document.getElementById('<%=txtconfirmpassword.ClientID %>').value != "") {
                if (document.getElementById('<%=txtnewpassword.ClientID %>').value != document.getElementById('<%=txtconfirmpassword.ClientID %>').value) {
                    // alert("Password does not match.");
                    $("#message-green").show().fadeOut(5000);
                    document.getElementById('succ').innerHTML = "Password does not match.";
                    document.getElementById('<%=txtnewpassword.ClientID %>').value = "";
                    document.getElementById('<%=txtconfirmpassword.ClientID %>').value = "";
                    document.getElementById('<%=txtnewpassword.ClientID %>').focus();
                    return false;
                }
            }

            if (document.getElementById('<%=txtoldpassword.ClientID %>').value != document.getElementById('<%=hdnPassword.ClientID %>').value)
            {
                $("#message-green").show().fadeOut(5000);

                document.getElementById('succ').innerHTML = "Your old password is wrong";

                document.getElementById('<%=txtoldpassword.ClientID %>').focus();
                document.getElementById('<%=txtconfirmpassword.ClientID %>').value = "";
                document.getElementById('<%=txtnewpassword.ClientID %>').value = "";
                return false;
            }
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:HiddenField runat="server" ID="hdnPassword" Value="0" />
    <span id="spanSelectedMenu" style="display: none">aChangePassword</span>
    <div id="product-table">
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
                            <strong>Old Password :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox ID="txtoldpassword"  CssClass="input_box" runat="server"
                                TextMode="Password"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>New Password:</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox ID="txtnewpassword" runat="server" CssClass="input_box" TextMode="Password"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Confirm Password :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox ID="txtconfirmpassword" runat="server" CssClass="input_box" TextMode="Password"></asp:TextBox>
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
                            <asp:Button ID="btnsave" runat="server" CssClass="button_bg" Text="Submit" OnClick="btnsave_Click"
                                OnClientClick="return validate();" />
                                
                                &nbsp<input value="Cancel" class="button_bg"
                                    name="btn_reset" type="reset">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
