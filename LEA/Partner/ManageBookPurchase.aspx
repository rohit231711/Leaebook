<%@ Page Title="Book Purchase Report" Language="C#" MasterPageFile="~/Partner/PartnerMaster.master" AutoEventWireup="true" CodeFile="ManageBookPurchase.aspx.cs" Inherits="Partner_ManageBookPurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
 <span id="spanSelectedMenu" style="display: none">aUser</span>
    <script type="text/javascript">

        function focusss() {
            document.getElementById('<%= txtname.ClientID %>').focus();
        }
        $(function () {
            $("input").checkBox();
            $("#toggle-all").click(function () {
                $("#toggle-all").toggleClass("toggle-checked");
                $("#form_user_managementview input[type=checkbox]").checkBox("toggle");
                return false;
            });
        });
        function deleteprompt(id) {
            var tid = $("#" + id).parent().find(":hidden")[0].value;
            $("#h_delete").val(id);

        }

        function status_cancelnew() {
            parent.$.fancybox.close();

        }

        function backcode() {
            debugger;
            if (document.getElementById('<%=txtname.ClientID %>').value == "" && document.getElementById('<%=txtemail.ClientID %>').value == "" && document.getElementById('<%=txtbook.ClientID %>').value == "" && document.getElementById('<%=txt_CategoryName.ClientID %>').value == "" && document.getElementById('<%=txt_amount.ClientID %>').value == "" && document.getElementById('<%=txtFromDate.ClientID %>').value == "" && document.getElementById('<%=txtToDate.ClientID %>').value == "") {
                alert("Please enter at least one data");
                return false;
            }
            else {
                document.getElementById('<%=hndname.ClientID %>').value = document.getElementById('<%=txtname.ClientID %>').value;
                document.getElementById('<%=hndemail.ClientID %>').value = document.getElementById('<%=txtemail.ClientID %>').value;
                document.getElementById('<%=hndeBookName.ClientID %>').value = document.getElementById('<%=txtbook.ClientID %>').value;
                document.getElementById('<%=hndCategoryName.ClientID %>').value = document.getElementById('<%=txt_CategoryName.ClientID %>').value;
                document.getElementById('<%=hndAmount.ClientID %>').value = document.getElementById('<%=txt_amount.ClientID %>').value;
                document.getElementById('<%=hndFromdate.ClientID %>').value = $('#<%=txtFromDate.ClientID  %>').val();
                document.getElementById('<%=hndTodate.ClientID %>').value = $('#<%=txtToDate.ClientID  %>').val();
                document.getElementById('<%=btnsearchbtn.ClientID %>').click();
            }
        }
    </script>    
   
    <asp:Button ID="btnsearchbtn" runat="server" OnClick="btnsearchbtn_Click" Style="display: none;" />
    <asp:HiddenField ID="hndname" runat="server" />
    <asp:HiddenField ID="hndemail" runat="server" />
    <asp:HiddenField ID="hndeBookName" runat="server" />
    <asp:HiddenField ID="hndCategoryName" runat="server" />
    <asp:HiddenField ID="hndAmount" runat="server" />
    <asp:HiddenField ID="hndFromdate" runat="server" />
    <asp:HiddenField ID="hndTodate" runat="server" />

    <asp:HiddenField ID="hnd" runat="server" />
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
                                <asp:Button ID="Search" runat="server" CssClass="button_bg" name="btn_search" Text="Search"
                                    OnClientClick="show_search();focusss();return false;" />
                                <input name="search" id="search" value="1" type="hidden" />
                                <%--<asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />--%>
                            </div>
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;display:none;">
                                <asp:ImageButton ID="imgbtnexcel" runat="server" ImageUrl="~/Admin/images/excel.png"
                                    BorderStyle="None" OnClick="imgbtnexcel_Click" Width="24px" Height="24px" ToolTip="Excel Report" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div id="form_user_managementview">
            <asp:GridView ID="gvRegistration" runat="server" SkinID="skinGrid" GridLines="None"
                AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvRegistration_PageIndexChanging"
                AllowSorting="true" OnRowCommand="gvRegistration_RowCommand" OnRowDataBound="gvRegistration_RowDataBound"
                OnSorting="gvRegistration_Sorting" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"
                EmptyDataText="Sorry, no records found!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red">
                <Columns>                    
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbFirstName" CssClass="no-sort" runat="server" CommandName="Sort"
                                Text="Name" CommandArgument="FirstName"></asp:LinkButton>
                            <asp:Image ID="imgFirstName" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                            <asp:HiddenField ID="hfRegistrationID" runat="server" Value='<%#Eval("RegistrationID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbEmailAddress" CssClass="no-sort" runat="server" CommandName="Sort"
                                Text="Email" CommandArgument="EmailAddress"></asp:LinkButton>
                            <asp:Image ID="imgEmailAddress" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbeBookName" CssClass="no-sort" runat="server" CommandName="Sort"
                                Text="eBook Title" CommandArgument="Title"></asp:LinkButton>
                            <asp:Image ID="imgeBookName" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lbleBookName" runat="server" Text='<%# Eval("eBookName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCategoryName" CssClass="no-sort" runat="server" CommandName="Sort"
                                Text="Category" CommandArgument="CategoryName"></asp:LinkButton>
                            <asp:Image ID="imgeCategoryName" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbAmount" CssClass="no-sort" runat="server" CommandName="Sort"
                                Text="Price" CommandArgument="Amount"></asp:LinkButton>
                            <asp:Image ID="imgeAmount" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-ForeColor="Black" HeaderText=" Registered Date">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbRegisteredDate" runat="server" CommandName="Sort" CommandArgument="PurchaseDate"
                                Text="Purchase Date" Style="background-image: none; cursor: pointer;"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPurchaseDate" runat="server" Text='<%# Eval("PurchaseDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                </Columns>
            </asp:GridView>
        </div>
        <div id="pagination">
            <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                        Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
         <div class="pmt_receivebox" style="text-align: center; border: #d6d6d6 1px solid;
            background-color: #f1f1f1; width: 378px; margin-left: 325px; height: 38px; margin-top: 15px;
            vertical-align: -webkit-baseline-middle; padding-top: 20px;">
            <span class="txt">Total Payment : $
                <asp:Label ID="lblPayment" runat="server"></asp:Label>
            </span>
        </div>
    </div>
    <a id="various_2" href="#inline_2"></a>
    <div style="display: none;">
       
            <div id="inline_2" style="width: 550px; min-height: 250px;">
             <asp:Panel runat="server" ID="pnelserch" DefaultButton="btnsearch">
                <div class="abc" id="sdfs">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="550">
                        <tbody>
                            <tr>
                                <td class="popup_bg" height="35" width="535">
                                    <table align="left" border="0" cellpadding="0" cellspacing="0" width="520">
                                        <tbody>
                                            <tr>
                                                <td colspan="2">
                                                    Search
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="20">
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 12px;" align="center" height="100" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                                                    height="5">
                                                </td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>Name</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">
                                                    &nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txtname" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>Email</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">
                                                    &nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txtemail" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>eBook Title</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">
                                                    &nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txtbook" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>Category</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">
                                                    &nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txt_CategoryName" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>Price</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">
                                                    &nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txt_amount" type="text" runat="server" />
                                                </td>
                                            </tr>    
                                            <tr class="light_bg">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>To Date</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="input_box "></asp:TextBox>&nbsp;&nbsp;(MM/DD/YYYY)                                               
                                            </td>
                                        </tr>
                                        <tr class="light_bg">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>From Date</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="input_box "></asp:TextBox>&nbsp;&nbsp;(MM/DD/YYYY)                                               
                                            </td>
                                        </tr>
                                                                                  
                                            <tr class="white_bg">
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="left" height="37">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="btnsearch" runat="server" CssClass="button_bg" Text="Submit" OnClick="btnsearch_Click"
                                                        OnClientClick="return backcode();" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 12px;" align="left" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                     </asp:Panel>
            </div>
   
    </div>
</asp:Content>

