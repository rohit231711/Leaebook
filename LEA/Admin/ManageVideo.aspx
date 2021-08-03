<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/adminmaster.master" AutoEventWireup="true"
    CodeFile="ManageVideo.aspx.cs" Inherits="Admin_ManageVideo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function deleteprompt(id) {
            var vid = $("#" + id).parent().find(":hidden")[0].value;
            document.getElementById('<%=hndvalue.ClientID %>').value = vid;
            $("#h_delete").val(id);
            $("#delete_line").html('Are you sure to delete this ?');
            $("#various_4").fancybox().trigger('click');

        }
        function delete_oknew() {
            var btnhdn = document.getElementById('<%=btnHdn.ClientID %>');
            btnhdn.click();

        }
        function status_cancelnew() {
          
            parent.$.fancybox.close();

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<span id="spanSelectedMenu" style="display: none">aVideo</span>
    <asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />
    <asp:HiddenField ID="hndvalue" runat="server" />
    <div id="product-table">
        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left">
                        </td>
                        <td width="16%">
                            &nbsp;
                        </td>
                        <td width="55%">
                            &nbsp;
                        </td>
                        <td width="16%" valign="bottom" align="right">
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="lkbAdd" Style="padding-left: 10px;" Text="Add New" CssClass="button_bg"
                                    runat="server" PostBackUrl="~/Admin/AddVedio.aspx" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvvideo" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                Width="100%">
                <Columns>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbName" runat="server" Text="Video Name" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgName" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("VideoName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px; font-size: 12px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID")%>' />
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                CommandArgument='<%#Eval("ID")%>' OnClientClick="deleteprompt(this.id);return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
