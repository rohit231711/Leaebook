<%@ Page Title="Shipping" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Shipping.aspx.cs" Inherits="Admin_Shipping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Error" runat="server" Visible="false">
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
    <span id="spanSelectedMenu" style="display: none">eBooks</span>
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
                        <strong>Shipping Provider :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlName" runat="server">
                            <asp:ListItem Text="DHL" Value="DHL"></asp:ListItem>
                            <asp:ListItem Text="FedEx" Value="FedEx"></asp:ListItem>
                        </asp:DropDownList>--%>
                        <font class="required">*</font>
                    </td>
                </tr>
                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>API Url :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
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
                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button_bg" PostBackUrl="~/Admin/dashboard.aspx" CausesValidation="false" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <asp:HiddenField ID="hfID" runat="server" />
</asp:Content>

