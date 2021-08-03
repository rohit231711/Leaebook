<%@ Page Title="Advertisement" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddAdvertisements.aspx.cs" Inherits="Admin_AddAdvertisements" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">        
    function validate() {
        var valid = true;
        $(".user1").each(function () {
            if ($(this).val() == '' && valid == true) {
                var currentId = $(this).attr('id');
                if (currentId == "English") {
                    alert('Please enter Advertisement Title (English)');
                    valid = false;
                    return false;
                }
                else if (currentId == "Spanish") {
                    alert('Please enter Advertisement Title (Spanish)');
                    valid = false;
                    return false;
                }
                //alert(valid);
                //return valid;
            }
        });
        if (valid == true) {
            debugger;
            var fup = document.getElementById('<%=fpUpload.ClientID %>');
            var fileName = fup.value;
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg") {                   
                }
                else {
                    $("#message-green").show().fadeOut(5000);
                    document.getElementById('succ').innerHTML = "Please select valid image.";
                    return false;
                }
            }
            else {
                //alert("test");
                if (document.getElementById('<%=hnfImage.ClientID %>').value == "0") {
                    alert('Please upload image.');
                    return false;
                }
            }            
            if(document.getElementById('<%=txtAdvertisementURL.ClientID %>').value != "")
            {
                var urlregex = new RegExp(
                 "^(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&amp;%\$#\=~_\-]+))*$");
                     
                if (urlregex.test(document.getElementById('<%=txtAdvertisementURL.ClientID %>').value) == false)
                {
                    alert('Please enter valid URL link. like http://abc.com');
                    return false;
                }
            }
        }
        //return false;
        return valid;
    }
    </script>

    <asp:HiddenField ID="hnfImage" runat="server" value="0"/>
    <div id="product-table">
        <div class="searchdiv">
            <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                    <tr>
                        <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                            height="5">
                        </td>
                    </tr>
                    <%=Advertisements%>

                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Select Image :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:FileUpload ID="fpUpload" runat="server" />
                        </td>
                    </tr>                  
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Image ID="imgcat" ImageUrl="" AlternateText="image" Width="100px" Height="100px" runat="server" Visible="false"/>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Advertisement URL Link  :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtAdvertisementURL" runat="server"></asp:TextBox>
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
                            <asp:Button ID="Button1" Text="Cancel" runat="server" CssClass="button_bg" OnClick="Button1_Click"/>
                      
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>

