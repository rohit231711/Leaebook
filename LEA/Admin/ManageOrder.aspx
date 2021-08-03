<%@ Page Title="Manage Order" Language="C#" MasterPageFile="~/Admin/adminmaster.master"
    AutoEventWireup="true" CodeFile="ManageOrder.aspx.cs" Inherits="Admin_ManageOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <span id="spanSelectedMenu" style="display: none;">ManageOrder</span>
    <script language="javascript" type="text/javascript">

        function view11(id) {
            debugger;
            var oid = $("#" + id).parents().find(":hidden")[0].value;
            $.fancybox({ 'href': 'vieworder.aspx?id=' + oid, 'type': 'iframe', 'width': '90%', 'height': '90%', 'hideOnOverlayClick': false

            });
        }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hndid" runat="server" />
    <div id="product-table">
        <div id="form_user_managementview">
            <asp:GridView ID="gvOrderList" runat="server" SkinID="skinGrid" GridLines="None"
                AutoGenerateColumns="false" Width="100%" OnRowCommand="gvOrderList_RowCommand" ShowHeaderWhenEmpty="true"
                OnRowDataBound="gvOrderList_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbOrderNo" runat="server" Text="Order No" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgOrderNo" Visible="false" runat="server" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:HiddenField ID="hndID" runat="server" Value='<%# Eval("OrderID") %>' />
                            <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCustomerName" runat="server" Text="Customer Name" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgCustomerName" Visible="false" runat="server" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbOrderDate" runat="server" Text="Order Date" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgOrderDate" Visible="false" runat="server" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("OrderDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbStatus" runat="server" Text="Payment Status" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgStatus" Visible="false" runat="server" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("PaymentStatus").ToString()=="False" ? "Unpaid":"Paid" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbApprove" runat="server" Text="Is Approve" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgApprove" Visible="false" runat="server" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="img" runat="server" ImageUrl='<%# Eval("IsApprove").ToString()=="True" ? "~/images/active.png":"~/images/1.png" %>'
                                Style="cursor: pointer;" BorderStyle="None" CommandArgument='<%#Eval("PurchaseID")%>'
                                CommandName="Approve" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px; font-size: 12px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("OrderID")%>' ToolTip="Edit" OnClientClick="deleteprompt(this.id);return false;"></asp:LinkButton>
                            <asp:LinkButton ID="lkbview" runat="server" CssClass="icon-107" CommandName="View1"
                                CommandArgument='<%#Eval("OrderID")%>' OnClientClick="view11(this.id); return false; "
                                ToolTip="View"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
    <a id="various_3" href="vieworder.aspx"></a>
</asp:Content>
