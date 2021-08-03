<%@ Page Title="Purchase/Order Report" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="PurchaseSubscriptionReport.aspx.cs" Inherits="Admin_ManageBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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


            $('#<%=ddlType.ClientID %>').bind('change', function () {
                // alert($('#<%=ddlType.ClientID  %> :selected').val());
                document.getElementById('<%=hndDdlID.ClientID %>').value = $('#<%=ddlType.ClientID  %> :selected').val();
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


        function status_cancelnew() {
            // alert("cancel");
            parent.$.fancybox.close();

        }
    </script>
    <style type="text/css">
        .zindex
        {
            z-index: 9999;
        }
    </style>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grdCustomer.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.grdCustomer.ClientID %>');
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
//            if (document.getElementById('<%=txtDate1.ClientID %>').value == "" && document.getElementById('<%=txtDate12.ClientID %>').value == "" && document.getElementById('<%=txtTitle.ClientID %>').value == "" && document.getElementById('<%=txtPublisher.ClientID %>').value == "") {
//                alert("Please enter at least one data");
//                return false;
//            }
//            document.getElementById('<%=hndDdlID.ClientID %>').value = $('#<%=ddlType.ClientID  %> :selected').val();
//            document.getElementById('<%=HDNDATE1.ClientID %>').value = $('#<%=txtDate1.ClientID  %>').val();
//            document.getElementById('<%=HDMDATE2.ClientID %>').value = $('#<%=txtDate12.ClientID  %>').val();
//            document.getElementById('<%=HDNTITLE.ClientID %>').value = $('#<%=txtTitle.ClientID  %>').val();
//            document.getElementById('<%=HDNPUBLISHER.ClientID %>').value = $('#<%=txtPublisher.ClientID  %>').val();
            //            document.getElementById('<%=btnsearchbtn.ClientID %>').click();
            
            if(document.getElementById('<%=txtName.ClientID %>').value == "" && document.getElementById('<%=txtEmail.ClientID %>').value == ""){
                alert("Please enter at least one data");
                return false;
            }
            document.getElementById('<%=hndName.ClientID %>').value = $('#<%=txtName.ClientID  %>').val();
            document.getElementById('<%=hndEmail.ClientID %>').value = $('#<%=txtEmail.ClientID  %>').val();
            document.getElementById('<%=btnsearchbtn.ClientID %>').click();
        }
    </script>
    <script type="text/javascript">
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= grdCustomer.ClientID %>');
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
    <asp:HiddenField ID="hndDdlID" runat="server" />
    <asp:HiddenField ID="HDNTITLE" runat="server" />
    <asp:HiddenField ID="HDNPUBLISHER" runat="server" />
    <asp:HiddenField ID="HDNDATE1" runat="server" />
    <asp:HiddenField ID="HDMDATE2" runat="server" />
    <asp:HiddenField ID="hndid" runat="server" />
     <asp:HiddenField ID="hndName" runat="server" />
      <asp:HiddenField ID="hndEmail" runat="server" />
    <div id="product-table">
        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left">
                            <%--<div id="actions-box">
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
                                    OnClientClick="show_search();return false;" />
                                <input name="search" id="search" value="1" type="hidden" />
                            </div>
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:ImageButton ID="imgbtnexcel" runat="server" ImageUrl="~/Admin/images/excel.png"
                                    BorderStyle="None" OnClick="imgbtnexcel_Click" Width="24px" Height="24px" ToolTip="Excel Report" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="grdCustomer" runat="server" SkinID="skinGrid" GridLines="None"
                AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="grdCustomer_PageIndexChanging"
                AllowSorting="true" PageSize="10" AllowPaging="true" OnRowCommand="grdCustomer_RowCommand"
                OnRowDataBound="grdCustomer_RowDataBound" OnSorting="grdCustomer_Sorting"  EmptyDataRowStyle-HorizontalAlign="Center"
                EmptyDataText="Sorry, no records found!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red">
                <Columns>
                    <%--<asp:TemplateField>
                        <HeaderStyle CssClass="table-header-check" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" />
                          
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbBook" CssClass="no-sort" runat="server" Text="Name" CommandName="Sort" CommandArgument="FirstName"></asp:LinkButton>
                            <asp:Image ID="imgTitle" Visible="false" runat="server" AlternateText=" " />
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:HiddenField ID="gdnID" runat="server" Value='<%#Eval("RegistrationID")%>' />
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <asp:LinkButton ID="lkbCategory" runat="server" Text="Email" CssClass="no-sort" CommandName="Sort" CommandArgument="EmailAddress"></asp:LinkButton>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--  <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("RegistrationID")%>' ToolTip="Edit"></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("RegistrationID")%>' />
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102 " CommandName="Delete1"
                                CommandArgument='<%#Eval("RegistrationID")%>' OnClientClick="deleteprompt(this.id);return false;" ToolTip="Delete"></asp:LinkButton>--%>
                            <asp:ImageButton ID="lnkPurchase" runat="server" CssClass="icon-1068" ImageUrl='~/Admin/images/table/mag1.png'
                                CommandName="Purchase" CommandArgument='<%#Eval("RegistrationID")%>' 
                                ToolTip='Subscribtion Details' />
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
                                        
                                        <tr class="light_bg" style="display:none;">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>Type</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="select" Width="200px" Height="28px"
                                                    OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                    <asp:ListItem Text="All" Value="2">All</asp:ListItem>
                                                    <asp:ListItem Text="Purchase" Value="0">Purchase</asp:ListItem>
                                                    <asp:ListItem Text="Subscription" Value="1">Subscription</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="light_bg" style="display:none;">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>Title</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="input_box "></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr class="light_bg" style="display:none;">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>Publisher</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtPublisher" runat="server" CssClass="input_box "></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr class="light_bg" style="display:none;">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>ToDate</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtDate1" runat="server" CssClass="input_box "></asp:TextBox>&nbsp;&nbsp;(MM/DD/YYYY)
                                                
                                            </td>
                                        </tr>
                                        <tr class="light_bg" style="display:none;">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>FromDate</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtDate12" runat="server" CssClass="input_box "></asp:TextBox>&nbsp;&nbsp;(MM/DD/YYYY)
                                               
                                            </td>
                                        </tr>
                                        <%--<tr class="white_bg">
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
                                        </tr>--%>
                                        

                                        <tr class="light_bg">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>Name</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="input_box "></asp:TextBox>
                                                
                                            </td>
                                        </tr>
                                        <tr class="light_bg">
                                            <td class="popup_listing_border" align="right" valign="middle" width="130">
                                                <strong>Email Address</strong>
                                            </td>
                                            <td class="popup_listing_border" align="left" height="37" width="11">
                                                &nbsp;
                                            </td>
                                            <td class="popup_listing_border" align="left" valign="middle" width="459">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="input_box "></asp:TextBox>
                                                
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
