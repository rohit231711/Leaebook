<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/adminmaster.master" AutoEventWireup="true"
    CodeFile="accessrights.aspx.cs" Inherits="Admin_accessrights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr class="white_bg">
                <th>
                    User Name
                </th>
                <td>
                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr class="white_bg">
                <th valign="top" style="width: 180px;">
                    Rights Access
                </th>
                <td style="width: 350px;">
                    <asp:TreeView ID="tvMenu" runat="server" ShowLines="true" ExpandDepth="1" PopulateNodesFromClient="false"
                        ShowCheckBoxes="Leaf" OnSelectedNodeChanged="tvMenu_SelectedNodeChanged" NodeStyle-ForeColor="Black"
                        LeafNodeStyle-ForeColor="Black">
                        <Nodes>
                        </Nodes>
                    </asp:TreeView>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="white_bg">
                <th>
                    &nbsp;
                </th>
                <td valign="top">
                    <asp:Button ID="btnsave" runat="server" CssClass="button_bg" Text="Save" OnClick="btnsave_Click" />&nbsp;
                    <asp:Button ID="btncancel" runat="server" CssClass="button_bg" Text="Cancel" OnClick="btncancel_Click" />
                </td>
                <td>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
