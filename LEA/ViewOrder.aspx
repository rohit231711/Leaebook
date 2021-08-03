<%@ Page Title="View Order" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="ViewOrder.aspx.cs" Inherits="ViewOrder" %>

<%@ Register Src="~/Includes/account_leftmenu.ascx" TagName="leftmenu" TagPrefix="lm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="#">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
                </a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblviewOrder" runat="server" Key="viewOrder" Colon="false" />
                </a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="accbox">
            <lm:leftmenu ID="leftmenu1" runat="server" />
            <div class="orderacc-rt" style="margin-bottom: 20px;">
                <div class="editacc">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="viewOrder" Colon="false" />
                </div>
                <div>
                    &nbsp;<br />
                </div>

                <div class="orderebook-title" id="Divorderebook" runat="server">
                    <div class="orderbook1">
                        &nbsp;<Localized:LocalizedLiteral ID="lblebook" runat="server" Key="ebooks" Colon="false" />
                    </div>
                    <div class="orderedit">
                        <%--&nbsp;&nbsp;&nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="name" Colon="false" />--%>
                        &nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="orderid" Colon="false" />
                        &
                        <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="Date" Colon="false" />
                    </div>
                    <div class="orderedit">
                        &nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="orderHistory" Colon="false" />
                    </div>
                    <div class="orderedit">
                        &nbsp;
                    </div>
                    <div class="orderpricetxt">
                        &nbsp;&nbsp;&nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="Price" Colon="false" />
                    </div>
                </div>

                <div class="orderebook-box">
                    <asp:Repeater ID="rptRecords1" runat="server">
                        <ItemTemplate>
                            <div class="orderrow1">
                                <div class="orderfirst">
                                    <img src="<%# PicturePath("Book/"+Eval("CategoryID")+"/"+Eval("ImagePath").ToString().Replace(".jpg","_1.jpg")) %>" alt=""
                                        class="wishimg1" style="height: 135px; width: 118px;" />
                                    <span class="wishimg1 orderbname">
                                        <%#Convert.ToString(Eval("Title"))%>
                                        <span class="orderbname">
                                            <%# Eval("Autoher")%>
                                        </span>
                                        <br />
                                        <br />
                                        <div id="ebook" runat="server" visible='<%# Eval("IseBook") %>'>
                                            Type : eBook
                                        </div>
                                        <div id="paperbook" runat="server" visible='<%# Eval("IsPaperBook") %>'>
                                            Type : Paper Book 
                                        <br />
                                            Quantity : <%# Eval("Qauntity") %>
                                        </div>
                                    </span>
                                </div>
                                <div class="orderdelbtn">
                                    <span class="orderbname">
                                        <%# Eval("OrderID")%>
                                        <br />
                                        <br />
                                        <%# Eval("PurchaseDate")%>
                                    </span>
                                </div>
                                <div class="orderdelbtn" style="width: 280px;">
                                    <asp:Panel ID="innerRptDiv" runat="server">
                                        No Delivery Data Found
                                    </asp:Panel>
                                    <asp:Repeater ID="innerRpt" runat="server">
                                        <ItemTemplate>
                                            <span class="orderbname" style="width: 30%; text-align: left;">
                                                <%# Eval("OrderStatus") %>
                                            </span>
                                            <span class="orderbname" style="width: 70%; text-align: left;">
                                                <%# Eval("StatusDate") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="orderptxt">
                                    <%#Convert.ToBoolean(Eval("IsFree")) == true ? "Free " : "$" + Eval("Price")%>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </div>

</asp:Content>

