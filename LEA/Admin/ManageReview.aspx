<%@ Page Title="Manage Review" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageReview.aspx.cs" Inherits="Admin_ManageReview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #pagination {
            line-height: 45px;
        }

            #pagination a {
                border: 1px solid #D2D2D2;
                padding: 3px 6px;
                color: #5788aa;
                border-radius: 5px;
                cursor: pointer;
            }

        .headerSpan {
            padding-left: 10px;
            color: #4f4f4f;
            font-family: Tahoma;
            font-size: 13px;
            font-weight: bold;
            line-height: 14px;
            margin: 0 0 0 10px;
            padding: 0 10px 0 0;
            text-decoration: none;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup1 {
            width: 64%;
            padding: 0;
            position: fixed;
            z-index: 100001;
            z-index: 100;
            left: 237px;
            top: 75px;
        }

            .modalPopup1 > div {
                position: relative;
                padding: 0; /*border-radius: 10px;*/
                background: #fff; /*background: -moz-linear-gradient(#fff, #999); background: -webkit-linear-gradient(#fff, #999); background: -o-linear-gradient(#fff, #999);*/
            }

            .modalPopup1 .close {
                background: #05cbbe;
                color: #FFFFFF;
                line-height: 35px;
                font-size: 15px;
                position: absolute;
                right: -12px;
                text-align: center;
                top: -10px;
                width: 35px;
                height: 35px;
                text-decoration: none;
                font-weight: bold;
                -webkit-border-radius: 100%;
                -moz-border-radius: 12px;
                border-radius: 100%;
                -moz-box-shadow: 1px 1px 3px #000;
                -webkit-box-shadow: 1px 1px 3px #000;
                box-shadow: 1px 1px 3px #000;
            }

            .modalPopup1 h2 {
                background: #356aa0;
                padding: 10px;
                color: #fff;
            }

            .modalPopup1 #ViewPendingData {
                text-align: left;
                padding: 15px;
                max-height: 400px;
                overflow-x: auto;
            }
            #ViewPendingData div{ width:100%;}
            #ViewPendingData div div{ width:49%; float:left; }
            #ViewPendingData div div label{ color:black; }
            #ViewPendingData div div span{ font-weight:bold; }
    </style>

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
            var tid = $("#" + id).parent().find(":hidden")[0].value;
            $("#h_delete").val(id);

        }

        function status_cancelnew() {
            parent.$.fancybox.close();

        }

        function status_cancel() {
            parent.$.fancybox.close();

        }
    </script>

    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            TotalChkBx = parseInt('<%= this.gvReview.Rows.Count %>');
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            var TargetBaseControl = document.getElementById('<%= this.gvReview.ClientID %>');
            var TargetChildControl = "chkMember";
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            if (CheckBox.checked) {
                for (i = 1; i < TargetBaseControl.rows.length; i++) {
                    var inputs = TargetBaseControl.rows[i].getElementsByTagName('input');
                    $("#form_user_managementview input[type=checkbox]").checkBox("toggle1");
                    inputs[0].checked = true;
                }
            }
            else {
                for (i = 1; i < TargetBaseControl.rows.length; i++) {
                    var inputs = TargetBaseControl.rows[i].getElementsByTagName('input');
                    $("#form_user_managementview input[type=checkbox]").checkBox("toggle2");
                    inputs[0].checked = false;
                }
            }
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            var HeaderCheckBox = document.getElementById(HCheckBox);
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

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
            var gridView = document.getElementById('<%= gvReview.ClientID %>');
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
        function statusapprove() {
            if (validateCheckBoxes()) {
                $('#actions-box-slider').css("display", "none");
                $("#status_line").html('Are you sure to approve this Review ?');
                $("#various_3").fancybox().delay(5000).trigger('click');
            }
            else {
                return false;
            }
        }

        function statusreject() {
            if (validateCheckBoxes()) {
                $('#actions-box-slider').css("display", "none");
                $("#status_line").html('Are you sure to reject this Review ?');
                $("#various_3").fancybox().delay(5000).trigger('click');
            }
            else {
                return false;
            }
        }

        function status_ok() {
            debugger;
            var status = $("#status_line").html();
            if (status.indexOf('delete') > -1) {
                var btnhdn = document.getElementById('<%=btnHdn.ClientID %>');
                btnhdn.click();
            }
            else if (status.indexOf('approve') > -1) {
                var btnhdn = document.getElementById('<%=btnApprove.ClientID %>');
                btnhdn.click();
            }
            else {
                var btnhdn = document.getElementById('<%=btnReject.ClientID %>');
                btnhdn.click();
            }
    }

    function ViewReview(ID) {
        debugger;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "ManageReview.aspx/ViewReviewDetails",
            data: "{'ReviewId':'" + ID + "'}",
            dataType: "json",
            success: function (data) {
                debugger;
                $('#lblTitle').html(data.d[0].Title);
                $('#lblName').html(data.d[0].Name);
                $('#lblRatting').html(data.d[0].Ratting);
                $('#lblDate').html(data.d[0].CreatedDate);
                $('#lblSummary').html(data.d[0].Summary);
                $('#lblReview').html(data.d[0].Review);
                $('#lblApprove').html(data.d[0].Approve);

                $find('popup').show();
                return true;
            },
            error: function (result) {
                alert("Error");
            }
        });
    }

    function ModelPopupClose() {
        $find('popup').hide();
        return false;
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <a id="testCheck2" onclick="statusapprove(); return false;" class="action-delete" style="cursor: pointer">Approve</a>
                                    <a id="testCheck3" onclick="statusreject(); return false;" class="action-delete" style="cursor: pointer">Reject</a>
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </td>
                        <td width="16%">&nbsp;
                        </td>
                        <td width="55%">
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input_box" Style="float:right;" placeholder="Search"></asp:TextBox>
                        </td>
                        <td width="16%" valign="bottom" align="right">
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="Search" runat="server" CssClass="button_bg" Text="Search" OnClick="Search_Click" />
                                <asp:Button ID="Cancel" runat="server" CssClass="button_bg" Text="Cancel" OnClick="Cancel_Click" />
                                <input name="search" id="search" value="1" type="hidden" />
                                <asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />
                                <asp:Button ID="btnApprove" runat="server" Style="display: none;" OnClick="btnApprove_Click" />
                                <asp:Button ID="btnReject" runat="server" Style="display: none;" OnClick="btnReject_Click" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvReview" runat="server" SkinID="skinGrid" GridLines="None"
                AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvReview_PageIndexChanging"
                AllowSorting="true" OnRowCommand="gvReview_RowCommand" OnRowDataBound="gvReview_RowDataBound"
                OnSorting="gvReview_Sorting" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeaderWhenEmpty="true"
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
                            <asp:HiddenField ID="hfReviewID" runat="server" Value='<%#Eval("ReviewID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbTitle" Style="background-image: none; cursor: pointer;" runat="server" CommandName="Sort"
                                Text="Book Title" CommandArgument="B.Title"></asp:LinkButton>
                            <asp:Image ID="imgFirstName" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <%# Eval("Title") %>'
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbFirstName" Style="background-image: none; cursor: pointer;" runat="server" CommandName="Sort"
                                Text="Name" CommandArgument="R.FirstName"></asp:LinkButton>
                            <asp:Image ID="imgEmailAddress" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <%# Eval("Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbRatting" runat="server" CommandName="Sort" CommandArgument="RR.Ratting"
                                Text="Ratting" Style="background-image: none; cursor: pointer;"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <%# Eval("Ratting") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCreatedDate" runat="server" CommandName="Sort" CommandArgument="RR.CreatedDate"
                                Text="Date" Style="background-image: none; cursor: pointer;"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <%# Eval("CreatedDate") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbApprove" runat="server" CommandName="Sort" CommandArgument="RR.Approve"
                                Text="IsApprove" Style="background-image: none; cursor: pointer;"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <%# Eval("Approve") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" ToolTip="Delete" CommandName="Delete1"
                                CommandArgument='<%#Eval("ReviewID")%>' OnClientClick="return confirm('Are you sure want to delete this?');"></asp:LinkButton>
                            <a id="lnkView" style="cursor: pointer;" class="icon-106" onclick="return ViewReview('<%# Eval("ReviewID") %>');"></a>
                            <%--<asp:LinkButton ID="lnkView" runat="server" CssClass="icon-106" OnClientClick="return ViewOrder('<%# Eval("ReviewID") %>');" ToolTip="View"></asp:LinkButton>--%>
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

        <asp:LinkButton ID="btnShow" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="Showmodelpopup" runat="server" PopupControlID="ShowReviewDetails"
            TargetControlID="btnShow" BackgroundCssClass="modalBackground" BehaviorID="popup">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="ShowReviewDetails" runat="server" CssClass="modalPopup1" align="center" Style="display: none;">
            <div>
                <asp:LinkButton ID="btnClose" runat="server" CssClass="close" OnClientClick="return ModelPopupClose();" Text="X" />
                <h2>Review Details</h2>
                <div id="ViewPendingData">
                    <div>
                        <div>
                            <span>Book Title :</span>
                            <label id="lblTitle"></label>
                        </div>
                        <div>
                            <span>Name :</span>
                            <label id="lblName"></label>
                        </div>
                    </div>
                    <div>
                        <div>
                            <span>Rating :</span>
                            <label id="lblRatting"></label>
                        </div>
                        <div>
                            <span>Date :</span>
                            <label id="lblDate"></label>
                        </div>
                    </div>
                    <div>
                        <div>
                            <span>Summary :</span>
                            <label id="lblSummary"></label>
                        </div>
                        <div>
                            <span>Review :</span>
                            <label id="lblReview"></label>
                        </div>
                    </div>
                    <div>
                        <div>
                            <span>Approve :</span>
                            <label id="lblApprove"></label>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

    </div>
</asp:Content>

