<%@ Page Title="Blog" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageBlog.aspx.cs" Theme="Default" Inherits="Admin_ManageBlog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .no-sort
        {
            background-image: none !important;
            cursor: default;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("input").checkBox();
            $("#toggle-all").click(function () {
                $("#toggle-all").toggleClass("toggle-checked");
                $("#form_user_managementview input[type=checkbox]").checkBox("toggle");
                return false;
            });
        });

        function deleteprompt(id) {
            // alert("#" + id);
            var tid = $("#" + id).parent().find(":hidden")[0].value;
            //  alert(tid);
            //deleteuser(tid);
            $("#h_delete").val(id);
            $("#delete_line").html('Are you sure to delete this ?');
            $("#various_4").fancybox().trigger('click');


        }
        function delete_oknew() {
            // alert("ok");
            var btnhdn = document.getElementById('<%=btnHdn.ClientID %>');
            btnhdn.click();

        }

        function status_cancelnew() {
            // alert("cancel");
            parent.$.fancybox.close();

        }
        function backcode() {
            
            debugger;
            if (document.getElementById('<%=txtTitleEnglish.ClientID %>').value == "" && document.getElementById('<%=txtTitleSpanish.ClientID %>').value == "") {
                
                alert("please insert Blog Title.");
                return false;
            }
            else {
                document.getElementById('<%=hndTitleEnglish.ClientID %>').value = document.getElementById('<%=txtTitleEnglish.ClientID %>').value;
                document.getElementById('<%=hndTitleSpanish.ClientID %>').value = document.getElementById('<%=txtTitleSpanish.ClientID %>').value;
                document.getElementById('<%=btnsearchbtn.ClientID %>').click();
            }
        }
      
    </script>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvCategory.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvCategory.ClientID %>');
            var TargetChildControl = "chkMember";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            if (CheckBox.checked) {
                for (i = 1; i < TargetBaseControl.rows.length; i++) {
                    var inputs = TargetBaseControl.rows[i].getElementsByTagName('input');
                    $("#form_user_managementview input[type=checkbox]").checkBox("toggle1");
                    inputs[0].checked = true;
                }
            }

            //if condition fails uncheck all checkboxes in gridview
            else {
                for (i = 1; i < TargetBaseControl.rows.length; i++) {
                    var inputs = TargetBaseControl.rows[i].getElementsByTagName('input');
                    $("#form_user_managementview input[type=checkbox]").checkBox("toggle2");
                    inputs[0].checked = false;
                }
            }
            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target base & child control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter;            
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx) {
                HeaderCheckBox.checked = false;
                $(".table-header-check input[type=checkbox]").checkBox("toggle2");
            }
            else if (Counter == TotalChkBx) {
                HeaderCheckBox.checked = true;
                $(".table-header-check input[type=checkbox]").checkBox("toggle1");
            }
        }
    </script>
    <script type="text/javascript">
        function focusss() {
            document.getElementById('<%= txtTitleEnglish.ClientID %>').focus();
        }
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= gvCategory.ClientID %>');
            var Leng = gridView.rows.length;
            if (Leng > 20) {
                Leng = 20;
            }
            for (var i = 1; i < Leng; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;
                            return true;
                        }
                    }
                }
            }
            // alert('Please select at lease one Category.');
            $("#message-green").show().fadeOut(5000);
            document.getElementById('succ').innerHTML = "Please select atleast one record.";
            return false;
        }
        function conf() {
            if (validateCheckBoxes()) {
                $('#actions-box-slider').css("display", "none");
                $("#delete_line").html('Are you sure to delete this ?');
                $("#various_4").fancybox().delay(5000).trigger('click');

            }
            else {
                return false;
            }
        }
    </script>
    <asp:Button ID="btnsearchbtn" runat="server" OnClick="btnsearchbtn_Click" Style="display: none;" />
    <asp:HiddenField ID="hndTitleEnglish" runat="server" />
    <asp:HiddenField ID="hndTitleSpanish" runat="server" />
    <div id="product-table">
        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left">
                            <div id="actions-box">
                                <a class="action-slider" href=""></a>
                                <div id="actions-box-slider" style="display: none;">
                                    <a id="callConfirm" onclick="conf(); return false;" class="action-delete" style="cursor: pointer">
                                        Delete</a> <%--<a id="testCheck2" onclick="statusactive()" class="action-delete" style="cursor: pointer">
                                            Active</a> <a id="testCheck3" onclick="statusinactive()" class="action-delete" style="cursor: pointer">
                                                Inactive</a>--%>
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
                                <asp:Button ID="lkbAdd" Style="padding-left: 10px;" Text="Add New" CssClass="button_bg"
                                    runat="server" PostBackUrl="~/Admin/AddBlog.aspx" />
                                <asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />
                            </div>
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="Search" runat="server" CssClass="button_bg" name="btn_search" Text="Search"
                                    OnClientClick="show_search();focusss();return false;" />
                                <input name="search" id="search" value="1" type="hidden" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvCategory" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                Width="100%" OnPageIndexChanging="gvCategory_PageIndexChanging" OnRowCommand="gvCategory_RowCommand" ShowHeaderWhenEmpty="true"
                OnRowDataBound="gvCategory_RowDataBound" OnSorting="gvCategory_Sorting" EmptyDataText="Sorry, no records found!"
                EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="table-header-check" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" />
                            <asp:HiddenField ID="hfCategoryID" runat="server" Value='<%#Eval("BolgID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbEngTitle" runat="server" CommandArgument="BlogTitleEnglish" CommandName="Sort"
                                Text="Blog Title (English)" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgCategoryeng" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblEngTitle" runat="server" Text='<%# Eval("BlogTitleEnglish") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbspaniceTitle" runat="server" CommandArgument="BlogTitleSpenice" CommandName="Sort"
                                Text="Blog Title (Spanish)" CssClass="no-sort"></asp:LinkButton>
                            <asp:Image ID="imgCategoryspa" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblspaniceTitle" runat="server" Text='<%# Eval("BlogTitleSpenice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px; font-size: 12px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("BolgID")%>'></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("BolgID")%>' />
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                CommandArgument='<%#Eval("BolgID")%>' OnClientClick="return confirm('Are you sure you want to delete?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
            <%--deleteprompt(this.id);return false;--%>
        </div>
        <div id="pagination">
            <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                        Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <a id="various_2" href="#inline_2"></a>
    <div style="display: none;">
        <div id="inline_2" style="width: 550px; min-height: 250px;">
            <div class="abc" id="sdfs">
                <asp:Panel runat="server" ID="pnelserch" DefaultButton="btnsearch">
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
                                                    <strong>Blog Title(English)</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">
                                                    &nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txtTitleEnglish" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>Blog Title(Spanish)</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">
                                                    &nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txtTitleSpanish" type="text" runat="server" />
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
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
