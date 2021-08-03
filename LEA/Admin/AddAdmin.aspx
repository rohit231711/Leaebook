<%@ Page Title="Manage User" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="AddAdmin.aspx.cs" Inherits="Admin_AddAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Datepicker/jquery.js" type="text/javascript"></script>
    <script src="../Datepicker/jquery-ui.js" type="text/javascript"></script>
    <link href="../Datepicker/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../js/Rule.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="spanSelectedMenu" style="display: none">aAdmin</span>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#ContentPlaceHolder1_txtDate1").datepicker({
                inline: true
            });


        });

        function validate() {


           
            if (document.getElementById('<%=txtUserName.ClientID %>').value == "") {

                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please enter the user name.";
                return false;
            }

            if (document.getElementById('<%=txtEmail.ClientID %>').value == "") {                
                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please enter email address.";
                return false;
            }

            var email = document.getElementById('<%=txtEmail.ClientID %>');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email.value)) {               
                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please provide a valid email address.";
                email.focus;
                return false;
            }
            if (document.getElementById('<%=txtPassword.ClientID %>').value == "") {                
                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please enter password.";
                return false;
            }



        }


       
    </script>
    <div id="product-table">
        <div class="searchdiv">
            <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                    <tr>
                        <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                            height="5">
                        </td>
                    </tr>
                    <tr class="light_bg" style="display: none">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>First Name :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtFirstName" runat="server" onkeyup="isRule(this,objAlpha,100);" onchange="isRule(this,objAlpha,100);"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg" style="display: none">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Last Name :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtLastName" runat="server" onkeyup="isRule(this,objAlpha,100);" onchange="isRule(this,objAlpha,100);"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Name :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtUserName" runat="server"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Email :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtEmail" runat="server"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Password :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <%--<tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Confirm Password :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtConfirm" runat="server" TextMode="Password"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>--%>
                    <tr class="light_bg" style="display: none">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>News Letter :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:CheckBox ID="chkNewsLetter" runat="server" />
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Gender :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:DropDownList runat="server" ID="drpGender" style="width: 195px;height: 32px;">
                                <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Birth Date :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox ID="txtDate1" runat="server" CssClass="input_box"></asp:TextBox>
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
                            <asp:DropDownList runat="server" ID="drpCountry" style="width: 195px;height: 32px;">
                            </asp:DropDownList>
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
                            <asp:Button ID="Button1" Text="Cancel" runat="server" CssClass="button_bg" OnClick="Button1_Click" /><%--PostBackUrl="~/Admin/ManageRegistration.aspx"--%>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hfUserType" runat="server" />
</asp:Content>
