<%@ Page Title="Order Management" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="OrderManagement.aspx.cs" Inherits="Admin_OrderManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script src="../Datepicker/jquery.js" type="text/javascript"></script>
    <script src="../Datepicker/jquery-ui.js" type="text/javascript"></script>
    <link href="../Datepicker/jquery-ui.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
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

        .model-table tbody tr td {
            border: 1px solid #c2c2c2;
            padding: 5px 0px 5px 10px;
            line-height: normal;
        }

        .model-table thead tr th {
            border: 1px solid #c2c2c2;
            padding: 5px 0px 5px 10px;
            line-height: normal;
        }

        .model-div-total-address {
            width: 100%;
            padding-top: 10px;
        }

            .model-div-total-address div {
                width: 50%;
            }

        .model-div-address label {
            font-weight: bold;
            color: black;
            display: block;
        }

        .model-div-address span {
            line-height: 18px;
        }

            .model-div-address span pre {
                margin: 0px;
                display: inline-flex;
            }

        .model-div-total label {
            font-weight: bold;
            color: black;
            padding-right: 20px;
        }

        .model-div-shippping label {
            font-weight: bold;
            color: black;
            padding-right: 20px;
        }

        .model-div-shippping span {
            /*margin-right: 8%;*/
        }

        .model-div-address {
            float: left;
        }

        .model-div-total {
            float: right;
            width: 21% !important;
        }

        .model-div-total-address:after {
            clear: both;
            display: block;
            content: "";
        }

        .model-div-shippping {
            width: auto !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#<%=txtStartDate.ClientID%>").datepicker({
                inline: true,
                dateFormat: "dd/mm/yy"
            });

            $("#<%=txtEndDate.ClientID%>").datepicker({
                inline: true,                
                dateFormat: "dd/mm/yy"
            });
    });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="product-table">

        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="16%" align="left"></td>
                        <td width="25%">&nbsp;
                        </td>
                        <td width="50%">
                            <asp:TextBox ID="txtStartDate" runat="server" placeholder="Start date" CssClass="input_box"></asp:TextBox>
                            <asp:TextBox ID="txtEndDate" runat="server" placeholder="End date" CssClass="input_box"></asp:TextBox>
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Text to search" CssClass="input_box"></asp:TextBox>
                        </td>
                        <td width="5%">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" OnClientClick="return ValidateSearch();" Text="Search" CssClass="button_bg" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="searchdiv">
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="searchdiv">
                <tbody>
                    <tr>
                        <td width="13%" align="left"></td>
                        <td width="16%">&nbsp;
                        </td>
                        <td width="55%">&nbsp;
                        </td>
                        <td width="16%" valign="bottom" align="right">
                            <div style="padding-bottom: 1px; padding-right: 5px; text-align: right; float: right;">
                                <asp:Button ID="lkbChange" Style="padding-left: 10px;" Text="Change Status" CssClass="button_bg" runat="server" OnClick="lkbChange_Click" />
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="form_user_managementview">
            <asp:GridView ID="gvBookStatus" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                Width="100%" AllowSorting="true" OnRowDataBound="gvBookStatus_RowDataBound" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvBookStatus_PageIndexChanging">
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Order ID</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Buyer Name</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            <asp:HiddenField ID="hfUserID" runat="server" Value='<%# Eval("UserID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Title</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                            <asp:HiddenField ID="hfPurchaseID" runat="server" Value='<%# Eval("PurchaseID") %>' />
                            <asp:HiddenField ID="hfBookID" runat="server" Value='<%# Eval("BookID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Dealer Email</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblDealerEmail" runat="server" Text='<%# Eval("DealerEmail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Buyer Email</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblBuyerEmail" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Purchase Date</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPurchaseDate" runat="server" Text='<%# Eval("PurchaseDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Book Type</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblPaperBook" runat="server" Text='<%# Convert.ToBoolean(Eval("IsPaperBook")) ? " Paper Book " : "" %>'></asp:Label>
                            <asp:Label ID="lblBookType" runat="server" Text='<%# Convert.ToBoolean(Eval("IseBook")) ? " eBook " : "" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-ForeColor="Black">
                        <HeaderStyle CssClass="table-header-repeat line-left" />
                        <HeaderTemplate>
                            <span class="headerSpan">Order Status</span>
                        </HeaderTemplate>
                        <ItemStyle Font-Size="14px" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("OrderStatus") %>'></asp:Label>
                            <asp:DropDownList ID="ddlOrderStatus" runat="server" onclick="disableDropDown(this);">
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                <asp:ListItem Text="Processing" Value="Processing"></asp:ListItem>
                                <asp:ListItem Text="Shipping" Value="Shipping"></asp:ListItem>
                                <asp:ListItem Text="Delivered" Value="Delivered"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <a id="lnkView" style="cursor: pointer;" onclick="return ViewOrder('<%# Eval("OrderID") %>');">View</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderStyle-Width="70px" ItemStyle-Width="70px">
                        <HeaderStyle CssClass="table-header-options line-left" />
                        <HeaderTemplate>
                            <span style="padding-left: 10px;">Options</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                CommandArgument='<%#Eval("ShipperID")%>'></asp:LinkButton>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ShipperID")%>' />
                            <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102 " CommandName="Delete1"
                                CommandArgument='<%#Eval("ShipperID")%>' OnClientClick="deleteprompt(this.id);return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>

        <asp:LinkButton ID="btnShow" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="Showmodelpopup" runat="server" PopupControlID="ShowOrderDetails"
            TargetControlID="btnShow" BackgroundCssClass="modalBackground" BehaviorID="popup">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="ShowOrderDetails" runat="server" CssClass="modalPopup1" align="center" Style="display: none;">
            <div>
                <asp:LinkButton ID="btnClose" runat="server" CssClass="close" OnClientClick="return ModelPopupClose();" Text="X" />
                <h2>Pending Order Details</h2>
                <div id="ViewPendingData">
                    <table id="tblViewData" class="model-table" style="width: 100%"></table>
                    <div class="model-div-total-address">
                        <div class="model-div-address">
                            <label>Address : </label>
                            <span id="span_OrderAddress"></span>
                        </div>
                        <div class="model-div-total">
                            <label>Total : </label>
                            <span id="span_totalamount"></span>
                            <div class="model-div-shippping" style="padding-bottom:5px">
                                <label style="padding-right:0px">Shipping Charge : </label>
                                <span id="span_shipingcharge"></span>
                            </div>
                             <div class="model-div-shippping" style="padding-bottom:5px">
                                <label style="padding-right:0px">Shipping Type : </label>
                                <span id="ShippingType"></span>
                            </div>
                            <div class="model-div-shippping" style="border-top: 1px solid #c2c2c2c2;padding-top: 5px;">
                                <label>Final Price : </label>
                                <span id="span_finalprice"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

    </div>
    <script type="text/javascript">

        function ViewOrder(ID) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "OrderManagement.aspx/ViewOrderDetails",
                data: "{'OrderId':'" + ID + "'}",
                dataType: "json",
                success: function (data) {
                    debugger;
                    var table = $('#tblViewData');
                    table.html('');
                    table.append('<thead><tr><th>Title</th><th>Quantity</th><th>Book Type</th><th>Price</th><th>Sub Total</th></tr></thead>');
                    table.append('<tbody>');
                    var list = data.d;
                    var temptotalamount = 0.00;
                    for (i = 0; i < list.length; i++) {
                        temptotalamount = temptotalamount + parseFloat(list[i].Subtotal);
                        table.append('<tr><td>' + list[i].Title + '</td><td>' + list[i].Quantity + '</td><td>' + list[i].Type + '</td><td>' + '$' + list[i].Price + '</td><td>' + '$' + list[i].Subtotal + '</td></tr>');
                    }
                    table.append('</tbody>');

                    $('#span_OrderAddress').html('<pre>' + data.d[0].Address + '</pre>');
                    $('#span_totalamount').html('$' + temptotalamount);
                    $('#span_shipingcharge').html('$' + data.d[0].ShippingCharge);
                    var FinalPrice = parseFloat(temptotalamount) + parseFloat(data.d[0].ShippingCharge);
                    $('#span_finalprice').html('$' + parseFloat(FinalPrice).toFixed(2));

                    $('#ShippingType').html(data.d[0].ShippingType);

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

        function ValidateSearch() {
            if ($('#ContentPlaceHolder1_txtEndDate').val().trim() != '') {
                if ($('#ContentPlaceHolder1_txtStartDate').val().trim() == '') {
                    alert('Please Select both date');
                    return false;
                }
            }

            if ($('#ContentPlaceHolder1_txtStartDate').val().trim() != '') {
                if ($('#ContentPlaceHolder1_txtEndDate').val().trim() == '') {
                    alert('Please Select both date');
                    return false;
                }
            }
        }

        function disableDropDown(drop) {
            var dropdown = document.getElementById(drop.id);
            var label = document.getElementById(drop.id.replace("ddl", "lbl"));
            if (label.innerHTML == "Approved") {
                $("select#" + dropdown.id + " option[value*='Pending']").prop('disabled', true);
                //$("select#" + dropdown.id + " option[value*='']").prop('disabled', true);
            }
            else if (label.innerHTML == "Cancel") {
                $("select#" + dropdown.id + " option[value*='Pending']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Delivered']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Processing']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Shipping']").prop('disabled', true);
            }
            else if (label.innerHTML == "Processing") {
                $("select#" + dropdown.id + " option[value*='Pending']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Approved']").prop('disabled', true);
            }
            else if (label.innerHTML == "Shipping") {
                $("select#" + dropdown.id + " option[value*='Pending']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Approved']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Processing']").prop('disabled', true);
            }
            else if (label.innerHTML == "Delivered") {
                $("select#" + dropdown.id + " option[value*='Pending']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Approved']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Processing']").prop('disabled', true);
                $("select#" + dropdown.id + " option[value*='Shipping']").prop('disabled', true);
            }
        }
    </script>
</asp:Content>

