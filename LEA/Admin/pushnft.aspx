<%@ Page Title="Send Notification" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="pushnft.aspx.cs" Inherits="admin_pushnft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        label
        {
            color: #356AA0;
            line-height: 20px;
        }
    </style>
    <table width="100%" cellpadding="10" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="rdbnew" RepeatLayout="Table" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="For New Issue" Value="1"></asp:ListItem>
                    <asp:ListItem Text="For App Update" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtpushNotification" runat="server" TextMode="MultiLine" Height="103px"
                    Width="530px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSend" CssClass="button_bg" runat="server" Text="Send" OnClick="btnSend_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
