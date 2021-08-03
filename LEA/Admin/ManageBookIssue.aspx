<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/adminmaster.master" AutoEventWireup="true" CodeFile="ManageBookIssue.aspx.cs" Inherits="Admin_ManageBookIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <span id="spanSelectedMenu" style="display: none">aBook</span>
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
            // alert(tid);
            //deleteuser(tid);
            $("#h_delete").val(id);
            $("#delete_line").html('Are you sure to delete this ?');
            $("#various_4").fancybox().trigger('click');

            document.getElementById('<%=hndid.ClientID %>').value = tid;
        }
        function delete_oknew() {
            // alert("ok");
            var btnhdn = document.getElementById('<%=btnHdn.ClientID %>');
            btnhdn.click();
            return true;
        }

        function status_cancelnew() {
            // alert("cancel");
            parent.$.fancybox.close();

        }
        function status(val) {
            if (validateCheckBoxes()) {
                document.getElementById('<%=hndst.ClientID %>').value = val;
                if (val == 1) {
                    //st = 1;
                    conf();
                }
                else {
                    active();
                }
            }
            else {
                $("#message-green").show().fadeOut(5000);
                document.getElementById('succ').innerHTML = "Please select at lease one row.";
            }
        }
    </script>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvBook.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvBook.ClientID %>');
            var TargetChildControl = "chkMember";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
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
        function backcode() {
            debugger;
            document.getElementById('<%=hndBooktype.ClientID %>').value = document.getElementById('<%=ddlBookType.ClientID %>').value;
            document.getElementById('<%=hndBooktitle.ClientID %>').value = document.getElementById('<%=txtBookTitle.ClientID %>').value;
            document.getElementById('<%=btnsearchbtn.ClientID %>').click();
        }
    </script>
    <script type="text/javascript">
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= gvBook.ClientID %>');
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
            // alert('Please select at lease one Book.');
            $("#message-green").show().fadeOut(5000);
            document.getElementById('succ').innerHTML = "Please select at lease one Book.";
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
        function active() {
            if (validateCheckBoxes()) {
                $('#actions-box-slider').css("display", "none");
                $("#status_line").html('Are you sure to active this Book ?');
                $("#various_3").fancybox().delay(5000).trigger('click');
            }
            else {
                return false;
            }
        }
        function status_ok() {
            var btnhdn = document.getElementById('<%=btnHdn.ClientID %>');
            btnhdn.click();
        }
    </script>
    <asp:Button ID="btnsearchbtn" runat="server" OnClick="btnsearchbtn_Click" Style="display: none;" />
    <asp:HiddenField ID="hndBooktype" runat="server" />
    <asp:HiddenField ID="hndBooktitle" runat="server" />
    <asp:HiddenField ID="hndid" runat="server" />
    <asp:HiddenField ID="hndst" runat="server" />
    <div id="product-table">
        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left">
                            <div id="actions-box">
                                <a class="action-slider" href=""></a>
                                <div id="actions-box-slider" style="display: none;">
                                    <a id="testCheck1" onclick="status('1'); return false;" class="action-delete" style="cursor: pointer">
                                        Delete</a> <%--<a id="testCheck2" onclick="status('2'); return false;" class="action-delete" style="cursor: pointer">
                                            Active</a> <a id="testCheck3" onclick="status('3'); return false;" class="action-delete" style="cursor: pointer">
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
                                    runat="server" PostBackUrl="~/Admin/AddBookIssue.aspx" />
                                <asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />
                            </div>
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="Search" runat="server" CssClass="button_bg" name="btn_search" Text="Search"
                                    OnClientClick="show_search();return false;" />
                                <input name="search" id="search" value="1" type="hidden" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvBook" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                Width="100%" OnPageIndexChanging="gvBook_PageIndexChanging" AllowSorting="true"
                OnRowCommand="gvBook_RowCommand" OnRowDataBound="gvBook_RowDataBound"
                OnSorting="gvBook_Sorting">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="table-header-check" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" />
                            <asp:HiddenField ID="hfBookID" runat="server" Value='<%#Eval("ID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbBook" CssClass="no-sort" runat="server" Text="Title"></asp:LinkButton>
                            <asp:Image ID="imgTitle" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCategory" runat="server" CssClass="no-sort" Text="Categoty Name"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCategory" runat="server" Text="Sub Category" CssClass="no-sort"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbBookName" runat="server" Text="Book" CssClass="no-sort"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBookName" runat="server" Text='<%# Eval("BookName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCreatedOn" runat="server" Text="Created On" CssClass="no-sort"
                                PostBackUrl="javascript:void(0);"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID")%>' />
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102 " CommandName="Delete1"
                                CommandArgument='<%#Eval("ID")%>' OnClientClick="deleteprompt(this.id);return false;"></asp:LinkButton>
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
    </div>
    <a id="various_2" href="#inline_2"></a>
    <div style="display: none;">
        <div id="inline_2" style="width: 550px; min-height: 250px;">
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
                                                <strong>Book Type</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:DropDownList ID="ddlBookType" runat="server" CssClass="select" Width="200px"
                                                    Height="28px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="light_bg">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>Book Title</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <input class="input_box" name="txt_srcref_id" id="txtBookTitle" type="text" runat="server" />
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
        </div>
    </div>
</asp:Content>

