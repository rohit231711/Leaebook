<%@ Page Title="Manage Cms" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageCms.aspx.cs" Inherits="Admin_ManageCms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<span id="spanSelectedMenu" style="display: none">aConfigure</span>
<div id="product-table">
        <div class="searchdiv" style="display:none;">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left">
                            <div id="actions-box">
                                <a class="action-slider" href=""></a>
                                <div id="actions-box-slider" style="display: none;">
                                    <a id="callConfirm" onclick="conf(); return false;" class="action-delete" style="cursor: pointer">
                                        Delete</a> <a id="testCheck2" onclick="statusactive()" class="action-delete" style="cursor: pointer">
                                            Active</a> <a id="testCheck3" onclick="statusinactive()" class="action-delete" style="cursor: pointer">
                                                Inactive</a>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </td>
                        <td width="16%">
                            &nbsp;
                        </td>
                        <td width="55%">
                            &nbsp;
                        </td>
                        <td width="16%" valign="bottom" align="right">
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="lkbAdd" Style="padding-left: 10px;display:none;" Text="Add New" CssClass="button_bg"
                                    runat="server" PostBackUrl="~/Admin/AddCategory.aspx" 
                                    onclick="lkbAdd_Click" />
                               
                            </div>
                           
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvCms" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                Width="100%"  OnRowCommand="gvCms_RowCommand" AllowPaging="true" PageSize="15"  OnPageIndexChanging="gvCategory_PageIndexChanging"
                onrowdatabound="gvCms_RowDataBound" AllowSorting="true">
                <Columns>
                
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCategory" runat="server" Text="Page" CssClass="no-sort" CommandName="sorting" CommandArgument="Page"></asp:LinkButton>
                         
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px; font-size: 12px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>                                                  
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>   
    </div>
</asp:Content>

