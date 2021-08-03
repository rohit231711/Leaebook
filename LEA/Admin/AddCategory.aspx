<%@ Page Title="Manage Category" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="AddCategory.aspx.cs" Inherits="Admin_AddCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="spanSelectedMenu" style="display: none">aCategory</span>
    <script type="text/javascript">
        function validate() {
            debugger;
            var valid = true;
            //$(".user1").each(function () {                
            //    var alpha = /^[a-zA-Z-,]+(\s{0,1}[a-zA-Z-, ])*$/;
            //    if ($(this).val() == '' && valid == true) {
            //        valid = false;
            //        alert('Please Insert Category.');
            //        return false;
            //        $(this).focus();
            //    }
            //});

            var category = document.getElementsByClassName('user1');
            for (i = 0; i < category.length; i++) {
                if (category[i].value == '' && valid == true) {
                    valid = false;
                    alert('Please Insert Category.');
                    category[i].focus();
                    return false;
                }
                else {
                    valid = true;
                }
            }

            var fup = document.getElementById('<%=fpUpload.ClientID %>');
            var fileName = fup.value;
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg") {
                    return true;
                }
                else {
                    $("#message-green").show().fadeOut(5000);
                    document.getElementById('succ').innerHTML = "Please select valid image.";
                    return false;
                }
            }
            else {
                if (document.getElementById('<%=hnfImage.ClientID %>').value == "0") {
                    alert('Please upload image.');
                    return false;
                }
            }
        }
    </script>

    <asp:HiddenField ID="hnfImage" runat="server" Value="0" />
    <div id="product-table">
        <div class="searchdiv">
            <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                    <tr>
                        <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                            height="5"></td>
                    </tr>
                    <%=Categories %>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Select Image :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:FileUpload ID="fpUpload" runat="server" Style="width: 195px;" />
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr>
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong> Title(English): </strong>
                        </td>
                         <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                        </td>
                        <td class="input_box user1" align="left" valign="middle" >
                            <asp:TextBox ID="txt_select" runat="server" class="input_box user1"></asp:TextBox>
                            </td>
                         
                    </tr>
                      <tr>
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong> Title(Spanish): </strong>
                        </td>
                         <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                        </td>
                        <td class="input_box user1" align="left" valign="middle" >
                            <asp:TextBox ID="txt_spanish" runat="server" class="input_box user1"></asp:TextBox>
                            </td>
                         
                    </tr>
                    <tr>
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong> Description(English): </strong>
                        </td>
                         <td  class="input_box user1" align="left" valign="middle" >&nbsp;
                        </td>
                        <td align="left" valign="middle">
                            <asp:TextBox ID="txt_desc" runat="server"  class="input_box user1"></asp:TextBox>
                            </td>
                         
                    </tr>
                     <tr>
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong> Description(Spanish): </strong>
                        </td>
                         <td  class="input_box user1" align="left" valign="middle" >&nbsp;
                        </td>
                        <td align="left" valign="middle">
                            <asp:TextBox ID="txt_desc1" runat="server"  class="input_box user1"></asp:TextBox>
                            </td>
                         
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Image ID="imgcat" Width="100px" Height="100px" runat="server" Visible="false" />
                        </td>
                    </tr>

                   

                    <tr class="white_bg">
                        <td align="right">&nbsp;
                        </td>
                        <td align="left" height="37">&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSubmit" OnClientClick="return validate();" runat="server" CssClass="button_bg"
                                Text="Submit" OnClick="btnSubmit_Click" /><%--btnSubmit_Click1--%>
                            <asp:Button ID="Button1" Text="Cancel" runat="server" CssClass="button_bg" OnClick="Button1_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
