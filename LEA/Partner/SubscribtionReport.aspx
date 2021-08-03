<%@ Page Title="Order Report" Language="C#" MasterPageFile="~/Partner/PartnerMaster.master"
    AutoEventWireup="true" CodeFile="SubscribtionReport.aspx.cs" Inherits="Partner_SubscribtionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="font-size: 13px; font-weight: bold;
            margin-bottom: 10px;">
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:Label runat="server" ID="lblName" Style="margin-left: 10px; color: gray; font-weight: normal"
                        Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:Label runat="server" ID="lblEmail" Style="margin-left: 10px; color: gray; font-weight: normal"
                        Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="account">
        <div id="product-table">
            <asp:Panel ID="pnlSubscriptions" runat="server">
                <div class="profile" style="width: 100%;">
                    <div class="intro" id="Div2" style="display: block; width: 100%">
                        <asp:GridView ID="grdSubscriptions" OnPageIndexChanging="grdSubscriptions_OnPageIndexChanging"
                            runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                            Width="100%" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbBook" runat="server" CssClass="no-sort" Text="eBook"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" Width="70" Height="100" src='<%#PicturePath("/Book/"+Eval("CategoryID")+"/"+Eval("ImagePath")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbTitle" runat="server" CssClass="no-sort" Text="eBook Title"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbIssues" runat="server" CssClass="no-sort" Text="Issues"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("Issues") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbDelivered" runat="server" CssClass="no-sort" Text="Delivered"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelivered" runat="server" Text='<%# Eval("delivered") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbRemaining" runat="server" CssClass="no-sort" Text="Remaining"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemaining" runat="server" Text='<%#Convert.ToString(Convert.ToInt32(Eval("Issues"))-Convert.ToInt32(Eval("delivered"))) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbStatus" runat="server" CssClass="no-sort" Text="Purchase Date"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("OrderDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbStatus" runat="server" CssClass="no-sort" Text="Status"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Convert.ToString(Convert.ToInt32(Eval("Issues"))-Convert.ToInt32(Eval("delivered")))=="0"?"Paid":"Active" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbStatus" runat="server" CssClass="no-sort" Text="Order ID & Date"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("OrderID") + "</br>" + Eval("OrderDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle CssClass="table-header-repeat line-left" />
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lkbPrice" runat="server" CssClass="no-sort" Text="Price"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%#"$"+Convert.ToString(Eval("Price")) %>'></asp:Label>
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
