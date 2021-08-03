<%@ Page Title="Manage eBooks" Language="C#" MasterPageFile="~/Partner/PartnerMaster.master"
    AutoEventWireup="true" CodeFile="ManageBook.aspx.cs" Inherits="Partner_ManageBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            return false;
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 46 || charCode > 57))
                return false;
            return true;
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

            //            PageMethods.Categorylist("1", function (result) {
            //                alert(result);
            //            });

            $('#<%=aSeatChange.ClientID %>').fancybox({
                'href': 'ManageBookSearch.aspx?ID=1',
                'type': 'iframe',
                'onClosed': function () {
                    window.parent.location.reload();
                }
            });
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
    </script>
    <asp:Button ID="btnsearchbtn" runat="server" OnClick="btnsearchbtn_Click" Style="display: none;" />
    <asp:HiddenField ID="hndBooktype" runat="server" />
    <asp:HiddenField ID="hndBooktitle" runat="server" />
    <asp:HiddenField ID="hndBookCategory" runat="server" />
    <asp:HiddenField ID="hndBookPublisher" runat="server" />
    <asp:HiddenField ID="hndid" runat="server" />
    <div id="product-table">
        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left">
                            <%--  <div id="actions-box">
                                <a class="action-slider" href=""></a>
                                <div id="actions-box-slider" style="display: none;">
                                    <a id="testCheck1" onclick="conf(); return false;" class="action-delete" style="cursor: pointer">
                                        Delete</a> <a id="testCheck2" onclick="statusactive()" class="action-delete" style="cursor: pointer">
                                            Active</a> <a id="testCheck3" onclick="statusinactive()" class="action-delete" style="cursor: pointer">
                                                Inactive</a>
                                </div>
                                <div class="clear">
                                </div>
                            </div>--%>

                            <div id="actions-box">
                                <a class="action-slider" href=""></a>
                                <div id="actions-box-slider" style="display: none;">
                                    <a id="callConfirm" onclick="conf(); return false;" class="action-delete" style="cursor: pointer">Delete</a> <%--<a id="testCheck2" onclick="statusactive()" class="action-delete" style="cursor: pointer">
                                            Active</a> <a id="testCheck3" onclick="statusinactive()" class="action-delete" style="cursor: pointer">
                                                Inactive</a>--%>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </td>
                        <td width="16%">&nbsp;
                        </td>
                        <td width="46%">&nbsp;
                        </td>
                        <td width="25%" valign="bottom" align="right">
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button_bg" OnClick="btnSave_Click" />
                            </div>
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="lkbAdd" Style="padding-left: 10px;" Text="Add New" CssClass="button_bg"
                                    runat="server" PostBackUrl="~/Partner/AddBookIssue.aspx" />
                                <asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />
                            </div>
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <%--  <asp:Button ID="Search" runat="server" CssClass="button_bg" name="btn_search" Text="Search"
                                    OnClientClick="show_search();return false;" />--%>
                                <asp:Button ID="aSeatChange" name="btn_search" Text="Search" runat="server" class="button_bg fancybox" />
                                <asp:Button ID="aClearSearch" Text="Clear Search" runat="server" CssClass="button_bg" OnClick="aClearSearch_Click" />
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
                OnSorting="gvBook_Sorting" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"
                EmptyDataText="Sorry, no records found!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="table-header-check" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" />
                            <asp:HiddenField ID="hfBookID" runat="server" Value='<%#Eval("BookID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbBook" CssClass="no-sort" runat="server" Text="Book Title" CommandName="sort" CommandArgument="Title"></asp:LinkButton>
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
                            <asp:LinkButton ID="lkbCategory" runat="server" Text="Category" CssClass="no-sort" CommandName="sort" CommandArgument="CategoryName"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbAuthorName" runat="server" Text="Author Name" CssClass="no-sort" CommandName="sort" CommandArgument="Autoher"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblAuthorName" runat="server" Text='<%# Eval("Autoher") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbLanguage" runat="server" Text="Book Language" CssClass="no-sort" CommandName="sort" CommandArgument="Language"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblLanguage" runat="server" Text='<%# Eval("Language") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbFinalPrice" runat="server" Text="eBook Price" CssClass="no-sort" CommandName="sort" CommandArgument="FinalPrice"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblFinalPrice" runat="server" Text='<%# Convert.ToInt32(Eval("IsFree")) == 1 ? "FREE" : Eval("FinalPrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbPaperBookFinalPrice" runat="server" Text="PaperBook Price" CssClass="no-sort" CommandName="sort" CommandArgument="PaperBookFinalPrice"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPaperBookFinalPrice" runat="server" Text='<%# Convert.ToInt32(Eval("IsFreePaper")) == 1 ? "FREE" : Eval("PaperBookFinalPrice") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCreatedOn" runat="server" Text="Created On" CssClass="no-sort" CommandName="sort" CommandArgument="CreatedOn"
                                PostBackUrl="javascript:void(0);"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("CreatedOn") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField>
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Order Index</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlOrderIndexGrid" runat="server"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="">Quantity</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOrderGrid" runat="server" Text='<%# Eval("OrderIndex") %>' ToolTip='<%# Eval("BookID") %>' Style="display: none;"></asp:Label>
                            <asp:TextBox ID="txtQuantity" runat="server" Enabled='<%# !Convert.ToBoolean(Eval("IsFreePaper")) %>' onkeypress="return isNumberKey(event);" Text='<%# Eval("Quantity") %>' Style="width: 50%;"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField>
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Is Active</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Eval("IsActive") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderStyle-Width="130px" ItemStyle-Width="110px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgStatus" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"Active":"InActive" %>' ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"~/images/active.png":"~/images/inactive.png" %>' Style="float: left; display: block; float: left; height: 20px; margin: 0 8px 0 0; width: 20px;" />
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("BookID")%>' ToolTip="Edit"></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("BookID")%>' />
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                CommandArgument='<%#Eval("BookID")%>' OnClientClick="return confirm('Are you sure you want to delete?');"></asp:LinkButton>
                            <asp:LinkButton ID="lnkIssue" runat="server" CssClass="icon-106" CommandName="Issue"
                                CommandArgument='<%#Eval("BookID")%>' ToolTip="Review Ratting"></asp:LinkButton>
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
                <div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
