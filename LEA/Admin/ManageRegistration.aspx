<%@ Page Title="Manage User" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="ManageRegistration.aspx.cs" Inherits="Admin_ManageRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="spanSelectedMenu" style="display: none">aUser</span>
    <style>
        .viewcart-content {
            position: absolute;
            width: 80%;
            top: 15%;
            left: 0;
            right: 0;
            margin: 0 auto;
            background-color: #fff;
            z-index: 22222;
            background: #f5faff;
            border: 10px solid #356aa0;
            border-top: 15px solid #356aa0;
            border-radius: 10px;
        }

            .viewcart-content .modal-body {
                padding: 25px;
            }

        #ContentPlaceHolder1_grayoverlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            z-index: 1100;
            display: none;
        }

        .viewcart-content .close {
            position: absolute;
            top: -25px;
            right: -15px;
            width: 30px;
            height: 30px;
            background: transparent url(../images/fancybox_org.png) -40px 0px;
            cursor: pointer;
            border: none;
            z-index: 2222;
            text-indent: -5000px;
        }

        .viewcart-content h4.modal-title {
            color: #fff;
            background-color: #356aa0;
            margin: 0;
            padding: 0 15px 15px;
            font-size: 16px;
        }
        .viewcart-content td,.viewcart-content th{text-align:center;}
        .abccartbooktable tr td {
            border-left: 1px solid #D2D2D2;
            border-top: 1px solid #D2D2D2;
        }
        .abccartbooktable tr td:last-child {
            border-right: 1px solid #D2D2D2;
        }
        .abccartbooktable tr:last-child td {
            border-bottom: 1px solid #D2D2D2;
        }
    </style>


    <script type="text/javascript">

        $(document).ready(function () {
            $(".close").click(function () {
                $(".viewcart-content").toggle();
                $("#ContentPlaceHolder1_grayoverlay").toggle();

            });
        });

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
        function delete_oknew() {
            var btnhdn = document.getElementById('<%=btnHdn.ClientID %>');
            btnhdn.click();

        }

        function status_cancelnew() {
            parent.$.fancybox.close();

        }

        function status_cancel() {
            parent.$.fancybox.close();

        }
        function backcode() {
            debugger;
            if (document.getElementById('<%=txtname.ClientID %>').value == "" && document.getElementById('<%=txtemail.ClientID %>').value == "") {
                alert("Please enter Name or Email address");
                return false;
            }
            else {
                document.getElementById('<%=hndname.ClientID %>').value = document.getElementById('<%=txtname.ClientID %>').value;
                document.getElementById('<%=hndemail.ClientID %>').value = document.getElementById('<%=txtemail.ClientID %>').value;
                document.getElementById('<%=btnsearchbtn.ClientID %>').click();
            }
        }
    </script>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvRegistration.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
            //            $('.calander').each(function () {
            //                $(this).datepicker();
            //            });

        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.

            var TargetBaseControl = document.getElementById('<%= this.gvRegistration.ClientID %>');
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
            var gridView = document.getElementById('<%= gvRegistration.ClientID %>');
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
            // alert('Please select at lease one user.');
            $("#message-green").show().fadeOut(5000);
            document.getElementById('succ').innerHTML = "Please select at lease one user.";
            return false;
        }
        function conf() {
            if (validateCheckBoxes()) {
                $('#actions-box-slider').css("display", "none");
                $("#delete_line").html('Are you sure want to delete this?');
                $("#various_4").fancybox().delay(5000).trigger('click');


            }
            else {
                return false;
            }
        }
        function statusactive() {
            if (validateCheckBoxes()) {
                $('#actions-box-slider').css("display", "none");
                $("#status_line").html('Are you sure to active this User ?');
                $("#various_3").fancybox().delay(5000).trigger('click');
            }
            else {
                return false;
            }
        }
        function status_ok() {
            var status = $("#status_line").html();
            var substring = "inactive";
            console.log(status.indexOf(substring) > -1 + " " + status.indexOf(substring));
            if (status.indexOf(substring) > -1) {
                var btnhdn = document.getElementById('<%=btnInactive.ClientID %>');
                btnhdn.click();
            }
            else {
                var btnhdn = document.getElementById('<%=btnActive.ClientID %>');
                btnhdn.click();
            }
        }
        function statusinactive() {
            if (validateCheckBoxes()) {
                $('#actions-box-slider').css("display", "none");
                $("#status_line").html('Are you sure to inactive this User ?');
                $("#various_3").fancybox().delay(5000).trigger('click');
            }
            else {
                return false;
            }
        }
        <%--function status_ok() {
            debugger;
            var btnhdn = document.getElementById('<%=btnInactive.ClientID %>');
            btnhdn.click();
        }--%>




</script>

    <asp:Button ID="btnsearchbtn" runat="server" OnClick="btnsearchbtn_Click" Style="display: none;" />
    <asp:HiddenField ID="hndname" runat="server" />
    <asp:HiddenField ID="hndemail" runat="server" />
    <asp:HiddenField ID="hnd" runat="server" />
    <div id="product-table">
        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left">
                            <div id="actions-box">
                                <a class="action-slider" href=""></a>
                                <div id="actions-box-slider" style="display: none;">
                                    <a id="testCheck1" onclick="conf(); return false;" class="action-delete" style="cursor: pointer">Delete</a>
                                    <a id="testCheck2" onclick="statusactive(); return false;" class="action-delete" style="cursor: pointer">Active</a>
                                    <a id="testCheck3" onclick="statusinactive(); return false;" class="action-delete" style="cursor: pointer">Inactive</a>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </td>
                        <td width="16%">&nbsp;
                        </td>
                        <td width="55%">&nbsp;
                        </td>
                        <td width="16%" valign="bottom" align="right">
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="Search" runat="server" CssClass="button_bg" name="btn_search" Text="Search"
                                    OnClientClick="show_search();focusss();return false;" />
                                <input name="search" id="search" value="1" type="hidden" />
                                <asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />
                                <asp:Button ID="btnActive" runat="server" Style="display: none;" OnClick="btnActive_Click" />
                                <asp:Button ID="btnInactive" runat="server" Style="display: none;" OnClick="btnInactive_Click" />
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
                    <asp:TemplateField>
                        <HeaderStyle CssClass="table-header-check" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" />
                            <asp:HiddenField ID="hfRegistrationID" runat="server" Value='<%#Eval("RegistrationID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbFirstName" CssClass="no-sort" runat="server" CommandName="Sort"
                                Text="Name" CommandArgument="FirstName"></asp:LinkButton>
                            <asp:Image ID="imgFirstName" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
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
                    <asp:TemplateField ItemStyle-ForeColor="Black" HeaderText=" Registered Date">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbRegisteredDate" runat="server" CommandName="Sort" CommandArgument="Createddate"
                                Text="Registered Date" Style="background-image: none; cursor: pointer;"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Is Active</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <%# Eval("IsActive") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" Visible="false" CommandName="Edit1"
                                ToolTip="Edit" CommandArgument='<%#Eval("RegistrationID")%>'></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("RegistrationID")%>' />
                            <%--<asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                ToolTip="Delete" CommandArgument='<%#Eval("RegistrationID")%>'
                                OnClientClick="confirm('Are you sure want to delete this?');"></asp:LinkButton>--%>

                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                CommandArgument='<%#Eval("RegistrationID")%>' OnClientClick="return confirm('Are you sure want to delete this?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">view</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <%--   <asp:Button ID="Search" runat="server" CssClass="button_bg" name="btn_search" Text="Search"
                                    OnClientClick="show_search();focusss();return false;" />--%>
                            <%--<a id="lnkView" style="cursor: pointer;" onclick="return ViewOrder('<%# Eval("RegistrationID") %>');">View</a>--%>
                            <asp:LinkButton runat="server" ID="lbk_viewCart" class="btn btn-info btn-lg button-view-cart" CommandArgument='<%#Eval("RegistrationID")%>' CommandName="ViewCart" Text="View Cart"></asp:LinkButton>
                            <%--<asp:LinkButton runat="server" ID="LinkButton1"  class="modalButton" data-popup="popupOne" CommandArgument='<%#Eval("RegistrationID")%>' CommandName="ViewCart" Text="View Cart" ></asp:LinkButton>--%>
                            <%--<a href="#" class="modalButton" data-popup="popupOne">Model Popup</a>--%>
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
            <asp:Panel runat="server" ID="pnelserch" DefaultButton="btnsearch">
                <div class="abc" id="sdfs">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" width="550">
                        <tbody>
                            <tr>
                                <td class="popup_bg" height="35" width="535">
                                    <table align="left" border="0" cellpadding="0" cellspacing="0" width="520">
                                        <tbody>
                                            <tr>
                                                <td colspan="2">Search
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="20"></td>
                            </tr>
                            <tr>
                                <td style="padding-left: 12px;" align="center" height="100" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                                                    height="5"></td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>Name</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txtname" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="light_bg">
                                                <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                    <strong>Email</strong>
                                                </td>
                                                <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                                                </td>
                                                <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                    <input class="input_box" name="txt_srcref_id" id="txtemail" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <%--<tr class="light_bg">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>ToDate</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="input_box calander"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="light_bg">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>FromDate</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="input_box calander"></asp:TextBox>
                                            </td>
                                        </tr>--%>
                                            <tr class="white_bg">
                                                <td align="right">&nbsp;
                                                </td>
                                                <td align="left" height="37">&nbsp;
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
                                <td style="padding-left: 12px;" align="left" valign="top">&nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </asp:Panel>
        </div>

    </div>

    <div runat="server" id="myModal" class="modal fade viewcart-content abccartbooktable" visible="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclientclick="hide();">&times;</button>
                    <h4 class="modal-title">Cart Book Details</h4>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GridView1" runat="server" SkinID="skinGrid" GridLines="None"
                        AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvRegistration_PageIndexChanging"
                        AllowSorting="true" OnRowCommand="gvRegistration_RowCommand" OnRowDataBound="gvRegistration_RowDataBound"
                        OnSorting="gvRegistration_Sorting" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"
                        EmptyDataText="Sorry, no records found!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="table-header-check" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" Visible="false" />
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" Visible="false" />
                                    <%--<asp:HiddenField ID="hfRegistrationID" runat="server" Value='<%#Eval("RegistrationID")%>' />--%>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <HeaderStyle CssClass="table-header-options line-left" />
                                <HeaderTemplate>
                                    <span style="padding-left: 10px;">Title</span>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <%# Eval("Title") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <HeaderStyle CssClass="table-header-options line-left" />
                                <HeaderTemplate>
                                    <span style="padding-left: 10px;">Category</span>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <%# Eval("CategoryName") %>'
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <HeaderStyle CssClass="table-header-options line-left" />
                                <HeaderTemplate>
                                    <span style="padding-left: 10px;">Author</span>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <%# Eval("Autoher") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <HeaderStyle CssClass="table-header-options line-left" />
                                <HeaderTemplate>
                                    <span style="padding-left: 10px;">Language</span>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <%# Eval("Language") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <HeaderStyle CssClass="table-header-repeat line-left" />
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lkbFinalPrice" runat="server" Text="eBook Price" CssClass="no-sort" CommandName="sort" CommandArgument="FinalPrice"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFinalPrice" runat="server" Text='<%# Convert.ToInt32(Eval("IseBook")) == 1 ? Convert.ToInt32(Eval("IsFree")) == 1 ? "FREE" : Eval("FinalPrice") : "N/A" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <HeaderStyle CssClass="table-header-options line-left" />
                                <HeaderStyle CssClass="table-header-repeat line-left" />
                                <HeaderTemplate>
                                    <asp:LinkButton ID="lkbPaperBookFinalPrice" runat="server" Text="PaperBook Price" CssClass="no-sort" CommandName="sort" CommandArgument="PaperBookFinalPrice"></asp:LinkButton>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPaperBookFinalPrice" runat="server" Text='<%# Convert.ToInt32(Eval("IsPaperBook")) == 1 ? Convert.ToInt32(Eval("IsFreePaper")) == 1 ? "FREE" : Eval("PaperBookFinalPrice") : "N/A" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                <HeaderStyle CssClass="table-header-options line-left" />
                                <HeaderTemplate>
                                    <span style="padding-left: 10px;">Created On</span>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <%# Eval("Createdon", "{0:dd/MM/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>

            </div>
        </div>
    </div>
    <div id="grayoverlay" style="background-color: rgb(119, 119, 119); opacity: 0.7; cursor: pointer; height: 1020px; display: none;" runat="server"></div>


</asp:Content>
