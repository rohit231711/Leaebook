<%@ Page Title="Add Blog" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AddBlog.aspx.cs" Inherits="Admin_AddBlog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    debugger;
    function validate() {

        var valid = true;
       // var loopcount = 0;
        $(".user1").each(function () {

            if ($(this).val() == '' && valid == true) {
                //valid = false;
                var currentId = $(this).attr('id');

                //alert(currentId);
                if (currentId == "English") {
                    alert('Please enter Blog Title (English)');
                    valid = false;
                    return false;
                }
                else if (currentId == "Spanish") {
                    alert('Please enter Blog Title (Spanish)');
                    valid = false;
                    return false;
                }

               
                

            }

        });

        //var valid = true;
        // var loopcount = 0;
        $(".user2").each(function () {

            if ($(this).val() == '' && valid == true) {
                //valid = false;
                var currentId = $(this).attr('id');

                //alert(currentId);
                if (currentId == "English") {
                    alert('Please enter Blog Description (English)');
                    valid = false;
                    return false;
                }
                else if (currentId == "Spanish") {
                    alert('Please enter Blog Description (Spanish)');
                    valid = false;
                    return false;
                }

               // return false;
                

            }

        });


        if (valid == true) {
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
                    <%=Blog%>
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
                            <asp:Image ID="imgcat" Width="100px" Height="100px" runat="server" Visible="false"/>
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

