<%@ Page Title="Add Cms" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="AddCms.aspx.cs" Inherits="Admin_AddCms" ValidateRequest="false" %>

    
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="spanSelectedMenu" style="display: none">aConfigure</span>
    <script type="text/javascript">
        //        $('#ctl00_ContentPlaceHolder1_btnSubmit').live("click", function () {
        //            jQuery("#aspnetForm").validationEngine();
        //        });

//        function validate() {

//            if (document.getElementById('').value == "") {
//                $("#message-green").show().fadeOut(5000);
//                document.getElementById('succ').innerHTML = "Please insert page name.";
//                return false;
//            }


        //        }
        $(document).ready(function () {
            ApplyTextEditor();
        });
        var MYeditor;
        function ApplyTextEditor() {
            var Editorcount = 1;
            //alert('hi');
            $('.editor').each(function () {
                MYeditor = CKEDITOR.replace('editor' + Editorcount);
                Editorcount = Editorcount + 1;
            });
            //alert('done');

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
                    <%--<tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Title :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtTitle" runat="server"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Description :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <CKEditor:CKEditorControl ID="CKEditor1" runat="server">
                            </CKEditor:CKEditorControl>
                        </td>
                    </tr>--%>

                    <%=CMS %>
                    <div id = "meta" runat="server">
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Meta Title :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtMetaTitle" runat="server"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Meta KeyWord :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                            <asp:TextBox CssClass="input_box" ID="txtMetaKeyWord" runat="server"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    <tr class="light_bg">
                        <td class="popup_listing_border" align="right" valign="middle" width="130">
                            <strong>Meta Description :</strong>
                        </td>
                        <td class="popup_listing_border" align="left" height="37" width="11">
                            &nbsp;
                        </td>
                        <td class="popup_listing_border" align="left" valign="middle" width="559">
                            <asp:TextBox CssClass="input_box" ID="txtMetaDescription" runat="server"></asp:TextBox>
                            <font class="required">*</font>
                        </td>
                    </tr>
                    </div>
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
                            <asp:Button ID="Button1" Text="Cancel" runat="server" CssClass="button_bg" PostBackUrl="~/Admin/ManageCms.aspx" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
