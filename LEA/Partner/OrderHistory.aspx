<%@ Page Title="Order History" Language="C#" MasterPageFile="~/Partner/PartnerMaster.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="Partner_OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        .headerSpan {
            padding-left: 10px;
            color: #4f4f4f;
            font-family: Tahoma;
            font-size: 13px;
            font-weight: bold;
            line-height: 14px;
            margin: 0 0 0 10px;
            padding: 0 10px 0 0;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="product-table">
        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left"></td>
                        <td width="16%">&nbsp;
                        </td>
                        <td width="55%">&nbsp;
                        </td>
                        <td width="16%" valign="bottom" align="right">
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvBookStatus" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                Width="100%" AllowSorting="true" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvBookStatus_PageIndexChanging">
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Order ID</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Buyer Name</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("UserID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Title</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            <asp:HiddenField ID="hfPurchaseID" runat="server" Value='<%# Eval("PurchaseID") %>' />
                            <asp:HiddenField ID="hfBookID" runat="server" Value='<%# Eval("BookID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Dealer Email</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblDealerEmail" runat="server" Text='<%# Eval("DealerEmail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Buyer Email</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBuyerEmail" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Purchase Date</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPurchaseDate" runat="server" Text='<%# Eval("PurchaseDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Book Type</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPaperBook" runat="server" Text='<%# Convert.ToBoolean(Eval("IsPaperBook")) ? " Paper Book " : "" %>'></asp:Label>
                            <asp:Label ID="lblBookType" runat="server" Text='<%# Convert.ToBoolean(Eval("IseBook")) ? " eBook " : "" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Order Status</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("OrderStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("ShipperID")%>'></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ShipperID")%>' />
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102 " CommandName="Delete1"
                                CommandArgument='<%#Eval("ShipperID")%>' OnClientClick="deleteprompt(this.id);return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>

    </div>
    
</asp:Content>

