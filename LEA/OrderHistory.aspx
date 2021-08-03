<%@ Page Title="Order History" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="OrderHistory" %>

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
                    <Localized:LocalizedLiteral ID="lblorderHistory" runat="server" Key="orderHistory" Colon="false" />
                </a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="accbox">
            <lm:leftmenu ID="leftmenu1" runat="server" />
            <div class="orderacc-rt" style="margin-bottom: 20px;">
                <div class="editacc">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="orderHistory" Colon="false" />
                </div>
                <div>
                    &nbsp;<br />
                </div>
                <asp:Label ID="lblDefaultMessage" runat="server" Text="You have no order History" Style="font-family: abeezeeregular; color: Gray;"
                    Visible="false">
                </asp:Label>

                <div class="orderebook-title" id="Divorderebook" runat="server">
                    <div class="orderbook1">
                        &nbsp;<Localized:LocalizedLiteral ID="lblebook" runat="server" Key="ebooks" Colon="false" />
                    </div>
                    <div class="orderedit">
                        &nbsp;&nbsp;&nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="name" Colon="false" />
                    </div>
                    <div class="orderedit">
                        &nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="author_name" Colon="false" />
                    </div>
                    <div class="orderedit">
                        &nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="orderid" Colon="false" />
                        &
                        <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="Date" Colon="false" />
                    </div>
                    <div class="orderpricetxt">
                        &nbsp;&nbsp;&nbsp;<Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="Price" Colon="false" />
                    </div>
                </div>
                <div class="orderebook-box" id="divOrder" runat="server">
                    <asp:Repeater ID="rptRecords1" runat="server">
                        <ItemTemplate>
                            <div class="orderrow1">
                                <div class="orderfirst">
                                    <img src="<%# PicturePath("Book/"+Eval("CategoryID")+"/"+Eval("ImagePath").ToString().Replace(".jpg","_1.jpg")) %>" alt=""
                                        class="wishimg1" style="height: 135px; width: 118px;" />
                                </div>
                                <div class="orderdelbtn">
                                    <span class="orderbname">
                                        <%#Convert.ToString(Eval("Title"))%>
                                        <br />
                                        <br />
                                        <div id="ebook" runat="server" visible='<%# !string.IsNullOrEmpty(Eval("IseBook").ToString()) ? Eval("IseBook"):false %>'>
                                            Type : eBook
                                        </div>
                                        <div id="paperbook" runat="server" visible='<%# !string.IsNullOrEmpty(Eval("IsPaperBook").ToString()) ? Eval("IsPaperBook"):false %>'>
                                            Type : Paper Book - Quantity : <%# Eval("Qauntity") %>
                                        </div>
                                    </span>
                                </div>
                                <div class="orderdelbtn">
                                    <span class="orderbname">
                                        <%# Eval("Autoher")%>
                                    </span>
                                </div>
                                <div class="orderdelbtn">
                                    <span class="orderbname">
                                        <%# Eval("OrderID")%>
                                        <br />
                                        <%# Eval("PurchaseDate")%>
                                        <div id="divTrack" runat="server" visible='<%# !string.IsNullOrEmpty(Eval("IsPaperBook").ToString()) ? Eval("IsPaperBook"):false %>'>
                                            <%# Eval("AppCode")%>
                                            <%--<a href='<%# "ViewOrder.aspx?Purchase=" + Eval("PurchaseID") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name %>' class="boxbut load_more">Track Order</a>--%>
                                        </div>
                                    </span>
                                </div>
                                <div class="orderptxt">
                                    <%#Convert.ToBoolean(Eval("IsFree")) == true ? "Free " : "$ " + Eval("Amount")%>
                                </div>
                                <div class="clear"></div>
                                <div class="orderptxt" style="float: right;">
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>

            </div>
        </div>
    </div>
</asp:Content>

