<%@ Page Title="Region" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="region.aspx.cs" Inherits="Admin_region" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function focusss() {
          <%--  document.getElementById('<%= txtname.ClientID %>').focus();--%>
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
           <%-- if (document.getElementById('<%=txtname.ClientID %>').value == "" && document.getElementById('<%=txtemail.ClientID %>').value == "") {
                alert("Please enter Name or Email address");
                return false;
            }
            else {
                document.getElementById('<%=hndname.ClientID %>').value = document.getElementById('<%=txtname.ClientID %>').value;
                document.getElementById('<%=hndemail.ClientID %>').value = document.getElementById('<%=txtemail.ClientID %>').value;
                document.getElementById('<%=btnsearchbtn.ClientID %>').click();
            }--%>
        }
    </script>

    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.grd_services.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
            //            $('.calander').each(function () {
            //                $(this).datepicker();
            //            });

        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.

            var TargetBaseControl = document.getElementById('<%= this.grd_services.ClientID %>');
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
            var gridView = document.getElementById('<%= grd_services.ClientID %>');
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
            document.getElementById('succ').innerHTML = "Please select at lease one region.";
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

         function validate() {
            debugger;
            var valid = true;
            var category = document.getElementsByClassName('user1');
            for (i = 0; i < category.length; i++) {
                if (category[i].value == '' && valid == true || category[i].value == 'Select') {
                    valid = false;
                    alert('Please insert all mandatory fields.');
                    category[i].focus();
                    return false;
                }
                else {
                    valid = true;
                }
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span id="spanSelectedMenu" style="display: none">aUser</span>
    <asp:Button ID="btnsearchbtn" runat="server"  Style="display: none;" />
    <asp:HiddenField ID="hndname" runat="server" />
    <asp:HiddenField ID="hndemail" runat="server" />
    <asp:HiddenField ID="hnd" runat="server" />

    <div runat="server" id="grd">
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
                                <asp:Button ID="btnHdn" runat="server" Style="display: none;" OnClick="lkbDelete_Click" />
                                <asp:Button ID="btnActive" runat="server" Style="display: none;" OnClick="btnActive_Click" />
                                <asp:Button ID="btnInactive" runat="server" Style="display: none;" OnClick="btnInactive_Click" />
                            </div>

                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                    <asp:Button ID="lkbAdd" Style="padding-left: 10px;" OnClick="lkbAdd_Click" Text="Add New" CssClass="button_bg" runat="server" />
                                    <asp:Button ID="Button1" runat="server" Style="display: none;" />
                            </div>

                        </td>

                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="grd_services" runat="server"  AllowPaging="true" PageSize="10" SkinID="skinGrid" GridLines="None"
                    AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="grd_services_PageIndexChanging"
                    AllowSorting="true" DataKeyNames="Id"
                    EmptyDataRowStyle-HorizontalAlign="Center"
                    EmptyDataText="Sorry, no records found!" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-ForeColor="Red">
                    <RowStyle CssClass="rowcss" />

                    <AlternatingRowStyle CssClass="arowcss" />
                    <PagerStyle CssClass="arowcss" BackColor="#f7f5f2" HorizontalAlign="left" />
                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />

                    <Columns>

                        <asp:TemplateField>
                            <HeaderStyle CssClass="table-header-check" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:HeaderClick(this);" />
                            </HeaderTemplate>
                            <ItemStyle Font-Size="14px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMember" runat="server" CssClass="temp" />
                                <asp:HiddenField ID="hfRegistrationID" runat="server" Value='<%#Eval("Id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                       
                        <asp:TemplateField ItemStyle-ForeColor="Black">
                            <HeaderStyle CssClass="table-header-repeat line-left" />
                            <HeaderTemplate>
                                <span style="padding-left: 10px; font-size: 12px;">NO.</span>
                            </HeaderTemplate>
                            <ItemStyle Font-Size="14px" />
                            <ItemTemplate>
                                <asp:Label ID="lblno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-ForeColor="Black">
                            <HeaderStyle CssClass="table-header-repeat line-left" />
                            <HeaderTemplate>
                                 <asp:LinkButton ID="lnkreg" OnClick="lnkreg_Click" CssClass="no-sort" runat="server"   Text="Region"></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemStyle Font-Size="14px" />
                            <ItemTemplate>
                                <asp:Label ID="lblreg" runat="server" Text='<%# Eval("Region") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-ForeColor="Black">
                            <HeaderStyle CssClass="table-header-repeat line-left" />
                            <HeaderTemplate>
                                <span style="padding-left: 10px; font-size: 12px;">No. Of Country</span>
                            </HeaderTemplate>
                            <ItemStyle Font-Size="14px" />
                            <ItemTemplate>
                                <asp:Label ID="lblto" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField ItemStyle-ForeColor="Black" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                            <HeaderStyle CssClass="table-header-repeat line-left" />
                            <HeaderTemplate>
                                <span style="padding-left: 10px; font-size: 12px;">Status</span>
                            </HeaderTemplate>
                            <ItemStyle Font-Size="14px" />
                            <ItemTemplate>
                                <asp:Label ID="lblst" runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                            <HeaderStyle CssClass="table-header-options line-left" />
                            <HeaderTemplate>
                                <span style="padding-left: 10px;">Options</span>
                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:ImageButton ID="lnkPurchase" runat="server"  OnCommand="lnkPurchase_Command"  CssClass="icon-1068" ImageUrl='~/Admin/images/table/edit.png'
                                    CommandName='<%# Eval("Id")%>'
                                    ToolTip='Edit' />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton ID="lnkdel" runat="server" OnCommand="lnkdel_Command"  CssClass="icon-1068" ImageUrl='~/Admin/images/table/delete.png'
                                        CommandName='<%# Eval("Id")%>'
                                        ToolTip='Delete' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
    
    </div></div>

    <div id="add" runat="server">
        <table align="center" id="id-form" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                        height="5"></td>
                </tr>

                  <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                        <strong>Region :</strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                        <asp:TextBox ID="txtregion"  style="width: 195px;height: 32px;" class="input_box user1" runat="server"></asp:TextBox>
                        <font class="required">*</font>
                    </td> 
                </tr>

                <tr class="light_bg">
                    <td class="popup_listing_border" align="right" valign="middle" width="130">
                      <%--  <strong>Country :</strong>--%>
                         <strong><asp:Label ID="lblh" runat="server" Text="Country :" ></asp:Label></strong>
                    </td>
                    <td class="popup_listing_border" align="left" height="37" width="11">&nbsp;
                    </td>
                    <td class="popup_listing_border" align="left" valign="middle" width="459">
                      <asp:CheckBoxList ID="chkHobbies" RepeatColumns="5" RepeatLayout="Table"  runat="server"></asp:CheckBoxList>
                    </td>
                </tr>

                <tr class="white_bg">
                    <td align="right">&nbsp;
                    </td>
                    <td align="left" height="37">&nbsp;
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmit"  runat="server" OnClientClick="return validate();" CssClass="button_bg" OnClick="btnSubmit_Click" Text="Submit" /><%--btnSubmit_Click1--%>
                        <asp:Button ID="Button2" Text="Cancel" OnClick="Button2_Click"  runat="server" CssClass="button_bg" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

