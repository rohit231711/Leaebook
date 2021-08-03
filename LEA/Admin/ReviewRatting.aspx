<%@ Page Title="Review Ratting" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ReviewRatting.aspx.cs" Inherits="Admin_banner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="../Client/js/jquery.magnifier.js"></script>
    <script src="../Client/js/jquery.jcarousel.min.js" type="text/javascript"></script>
    <link href="../Client/css/skin.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnImageName" runat="server" />
    <asp:MultiView ID="mltBanner" runat="server">
        <asp:View ID="viewList" runat="server">
            <br />
            <div id="product-table">
                <div id="form_user_managementview">
                    <asp:GridView ID="GrdList" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                       EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataText="Sorry, no records found!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red" Width="100%" OnRowCommand="GrdList_RowCommand">
                        <Columns>
                            <asp:TemplateField ItemStyle-ForeColor="Black">
                                <HeaderStyle  CssClass="table-header-check" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label1weqwe" runat="server" Text="Name"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    
                                        <asp:Label ID="Label1qweqwe" runat="server" Text='<%# Eval("Name") %>'></asp:Label><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-ForeColor="Black">
                                <HeaderStyle CssClass="table-header-check" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label1sadsad" runat="server" Text="Summary"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px"  />
                                <ItemTemplate>
                                    
                                        <asp:Label ID="Label19867987" runat="server" Text='<%# Eval("Summary") %>'></asp:Label><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ItemStyle-ForeColor="Black">
                                <HeaderStyle CssClass="table-header-check" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label15464" runat="server" Text="Review"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    
                                        <asp:Label ID="Label14566" runat="server" Text='<%# Eval("Review") %>'></asp:Label><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-ForeColor="Black">
                                <HeaderStyle  CssClass="table-header-check" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label1879" runat="server" Text="Ratting"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    
                                        <asp:Label ID="Label1456" runat="server" Text='<%# Eval("Ratting") %>'></asp:Label><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="table-header-check line-left" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label123" runat="server" Text="Options"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle VerticalAlign="Top" />
                                <ItemTemplate>
                                    <%--          <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                        CommandArgument='<%#Eval("ReviewID")%>'></asp:LinkButton>--%>
                                    <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                        CommandArgument='<%#Eval("ReviewID")%>' OnClientClick="return confirm('Are you sure you want to delete?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
