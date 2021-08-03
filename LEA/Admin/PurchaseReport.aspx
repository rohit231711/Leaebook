<%@ Page Title="Purchase Report" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="PurchaseReport.aspx.cs" Inherits="PurchaseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="font-size: 13px;font-weight: bold;margin-bottom: 10px;">
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:Label runat="server" ID="lblName" style="margin-left:10px;color:gray;font-weight:normal" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:Label runat="server" ID="lblEmail" style="margin-left:10px;color:gray;font-weight:normal" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="account">
        <div id="product-table">
            <asp:Panel runat="server" ID="pnlPaymenthistory">
                <div class="profile" style="width: 100%">
                    <div class="intro" id="Div1" style="display: block; width: 100%">
                        <asp:GridView ID="gvPaymentHistory" OnPageIndexChanging="gvPaymentHistory_OnPageIndexChanging" ShowHeaderWhenEmpty="true"
                            runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                            Width="100%" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbBook" runat="server" CssClass="no-sort" Text="OrderDate"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderDate" runat="server" Text='<%#Convert.ToDateTime(Eval("PurchaseDate")).ToString("d") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbOrderID" runat="server" CssClass="no-sort" Text="OrderID"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbBookTitle" runat="server" CssClass="no-sort" Text="Title"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbIssues" runat="server" CssClass="no-sort" Text="Issues"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("issues") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbPrice" runat="server" CssClass="no-sort" Text="Price"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%#Convert.ToString(Eval("Price")+" RM.") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblNoRecord" runat="server" Visible="false">No Record Found...</asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
