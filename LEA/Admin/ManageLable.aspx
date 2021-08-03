<%@ Page Title="Labels" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="ManageLable.aspx.cs" Inherits="Admin_AddCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="spanSelectedMenu" style="display: none">aCategory</span>
    <script type="text/javascript">
    
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
                    <%=Lables%>
                  
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
