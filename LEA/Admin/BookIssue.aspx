<%@ Page Title="Book Issues" Language="C#" MasterPageFile="~/Admin/adminmaster.master"
    AutoEventWireup="true" CodeFile="BookIssue.aspx.cs" Inherits="Admin_BookIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="float:right;height:48px;">
        <asp:Button ID="btnCancel" Text="Back" runat="server" CssClass="button_bg" PostBackUrl="~/Admin/ManageBook.aspx"
            CausesValidation="false" />
    </div>
    <div id="product-table">
        <asp:DataList ID="dlBookIssue" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
            CellPadding="5" CellSpacing="5" BorderStyle="None" Width="100%" OnItemCommand="dlBookIssue_ItemCommand">
            <ItemTemplate>
                <div style="width: 35%; float: left;">
                    <asp:Image ID="img1" runat="server" ImageUrl='<%#Eval("FrontImage")%>' Width="180"
                        Height="270" />
                </div>
                <div style="width: 55%; float: left;">
                    <br />
                    <b style="text-transform: uppercase">
                        <%#Eval("BookTitle")%></b><br />
                    <br />
                    <b>Language :</b>
                    <%#Eval("Language")%><br />
                    <br />
                    <b>Publish date :</b>
                    <%# Eval("PublishDate","{0:d}")%><br />
                    <br />
                    <b>Price :</b>
                    <%# "RM "+Convert.ToDecimal(Eval("Price").ToString()).ToString("0.00")%><br />
                    <br />
                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Admin/images/table/edit.png"
                        ToolTip="Edit" CommandName="Edt" CommandArgument='<%#Eval("ID") %>' />&nbsp;
                    <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="~/Admin/images/table/delete.png"
                        CommandArgument='<%#Eval("ID") %>' ToolTip="Delete" CommandName="Del" OnClientClick="return confirm('Are you sure you want to delete?');" />
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
