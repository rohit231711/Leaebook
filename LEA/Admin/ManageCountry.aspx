<%@ Page Title="Manage Country" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageCountry.aspx.cs" Inherits="Admin_ManageCountry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvCountry.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;

            //            PageMethods.Categorylist("1", function (result) {
            //                alert(result);
            //            });

            <%--$('#<%=aSeatChange.ClientID %>').fancybox({
                'href': 'ManageBookSearch.aspx?ID=1',
                'type': 'iframe',
                'onClosed': function () {
                    window.parent.location.reload();
                }
            });--%>
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvCountry.ClientID %>');
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
        $("#<%=txtSearch.ClientID%>").keyup(function (event) {
            debugger;
            if (event.keyCode == 13) {
                $("#<%=btnSearch.ClientID%>").click();
            }
        });

        function checkFunction(evt) {
            console.log(evt.keyCode);
            if (evt.keyCode == 13) {
                debugger;
                $("#<%=btnSearch.ClientID%>").click();
                return false;
            }
        }
    </script>
    <style>
        .GridPager a, .GridPager span {
            display: block;
            height: 15px;
            width: 15px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
            padding: 5px;
        }

        .GridPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hndcategory" runat="server" />
    <div id="product-table">
        <div class="searchdiv">
            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                <a id="lkbAdd" style="padding: 7px;" class="button_bg" href="/Admin/AddCountry.aspx">Add New</a>
            </div>
            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;margin-top:-6px">
                <asp:TextBox runat="server" ID="txtSearch" placeholder="Search country by name" onkeyup="checkFunction(event)"/>
                <asp:Button ID="btnSearch" Style="padding-left: 10px;" Text="Search" CssClass="button_bg"
                    runat="server" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvCountry" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                Width="100%" OnPageIndexChanging="gvCountry_PageIndexChanging" AllowSorting="true" AllowPaging="true" PageSize="15"
                OnRowCommand="gvCountry_RowCommand" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"
                EmptyDataText="Sorry, no records found!" EmptyDataRowStyle-Font-Bold="true" OnSorting="gvCategory_Sorting" EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="table-header-check" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" />
                            <asp:HiddenField ID="hfCountryID" runat="server" Value='<%#Eval("countryid")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkreg" CssClass="no-sort" runat="server" Text="Region" CommandName="sort" CommandArgument="Title"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblreg" runat="server" Text='<%# Eval("region") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbBook" CssClass="no-sort" runat="server" Text="Country Name" CommandName="sort" CommandArgument="Title"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblcountryname" runat="server" Text='<%# Eval("countryname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbISOCode" CssClass="no-sort" runat="server" Text="ISO Code" CommandName="sort" CommandArgument="Title"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("ISOCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="130px" ItemStyle-Width="110px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgStatus" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"Active":"InActive" %>' ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) == true ?"~/images/active.png":"~/images/inactive.png" %>' Style="float: left; display: block; float: left; height: 20px; margin: 0 8px 0 0; width: 20px;" />
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("countryid")%>' ToolTip="Edit"></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("countryid")%>' />
                            <%--<asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                CommandArgument='<%#Eval("countryid")%>' OnClientClick="return confirm('Are you sure you want to delete?');"></asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="GridPager" />
            </asp:GridView>
        </div>
        <div id="pagination">
            <%--<asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                        Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>--%>
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

