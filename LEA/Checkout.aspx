<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Checkout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
        });
        function Delete() {
            var ID = $('#hdnDeleteValue').val();
            PageMethods.DeleteRecord(ID);
            return false;
        }
        function onkeyupp() {
            return false;
        }
    </script>
    <!-- Event snippet for Checkout conversion page
In your html page, add the snippet and call gtag_report_conversion when someone clicks on the chosen link or button. -->
<script>
    function gtag_report_conversion(url) {
        var callback = function () {
            if (typeof (url) != 'undefined') {
                window.location = url;
            }
        };
        gtag('event', 'conversion', {
            'send_to': 'AW-781010452/RDCECMuShI4BEJSMtfQC',
            'event_callback': callback
        });
        return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblcheckout" runat="server" Key="checkout" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <div class="wrap">

        <div class="loginbox1">

            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="lblcheckout1" runat="server" Key="checkout" Colon="false" /></h1>
            </div>
            <asp:Label ID="lblDefaultMessage" runat="server" Text="No data found" Style="font-family: abeezeeregular; color: Gray;" Visible="false">
            </asp:Label>
            <a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="contbtn">
                <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="Continuetoshopping" Colon="false" /></a>



            <div class="check-box" runat="server" id="chkout">

                <div class="ebook-title1">
                    <div class="book-new" ">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral15" runat="server" Key="lbl_book" Colon="false" />
                    </div>
                    <div class="edit2">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="Edit" Colon="false" />
                    </div>
                    <div class="pricetxt2">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="Price" Colon="false" />
                    </div>
                </div>

                <div class="ebook-box1">
                    <asp:Repeater ID="rptRecords1" runat="server" OnItemCommand="del">
                        <ItemTemplate>
                            <div class="row-new">
                                <div class="first1">
                                    <%--<img src="Book/<%#(Eval("CategoryID"))%>/<%#(Eval("ImagePath"))%>" alt="" class="wishimg1" style = "height:135px; width:118px;"/>--%>
                                    <%--<a href="Book-Detail.aspx">--%>
                                        <img src="<%# PicturePath("Book/"+Eval("CategoryID")+"/"+Eval("ImagePath").ToString().Replace(".jpg","_1.jpg")) %>" alt="" class="wishimg1" style="height: 135px; width: 98px;" />
                                       <%-- </a>--%>
                                    <div class="booktxt">
                                        <span class="bname"><%#Convert.ToString(Eval("Title"))%></span>
                                        <span class="aname"><%#Convert.ToString(Eval("Autoher"))%></span>
                                        <span class="bname">
                                            <asp:Label runat="server" ID="order">
                                                <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="orderid" Colon="false" /></asp:Label>
                                            : <%#Eval("OrderNo")%>
                                        </span>
                                        <span class="bname">
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="lbl_book" Colon="false" />
                                            <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key='<%# getKeyFromBook() %>' Colon="false" />
                                        </span>
                                        <span class="bname" runat="server" visible='<%# Eval("IspaperBook") %>'>Quantity :
                                            <asp:Label ID="lblQuanitity" runat="server" Text='<%# Eval("Qauntity") %>' Visible='<%# Eval("IspaperBook") %>'
                                                Style="width: 15%; padding: 5px 0px 5px 5px;" onkeydown="return onkeyupp();"></asp:Label>
                                            <%--<input type="number" min="1" name="txtQuantity" value='<%# Eval("Qauntity") %>' id="txtQuantity"
                                                style="width: 15%; padding: 5px 0px 5px 5px;" onkeydown="return onkeyupp();" />--%>
                                        </span>
                                        <div class="rate">
                                            <img src="images/<%#Convert.ToString(Eval("Ratting"))%>_star.png" id="img_rate1" alt="" height="35px;" />
                                            <span class="ratetxt" style="margin-top: 9px; margin-left: 5px;">(<%#Convert.ToString(Eval("TotalReview"))%>
                                                <asp:Label ID="tr" runat="server">
                                                    <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="rating" Colon="false" /></asp:Label>)
                                            </span>
                                        </div>
                                        <div class="movetxt">
                                            <asp:LinkButton ID="move" OnClientClick='<%# getWishString() %>' CommandName="move" CommandArgument='<%#Eval("BookID")%>' runat="server" Text="Move">
                                                <img src="images/heart.png" alt="" class="heartimg" /><Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="movetowishlist" Colon="false" />
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <input type="hidden" value="0" id="hdnDeleteValue" />
                                </div>
                                <div class="delbtn2">
                                    <asp:LinkButton ID="LinkButton1" type="button" OnClientClick='<%# getRemoveString() %>' CommandName="delete" CommandArgument='<%#Eval("BookID")+"@"+Eval("IseBook")+"@"+Eval("IsPaperbook")%>' runat="server" Text="Delete"><img src="images/delete.png" alt="" /></asp:LinkButton>
                                </div>
                                <div class="ptxt">
                                    <asp:Label runat="server" ID="lblsymbol" Text='<%# Convert.ToBoolean(Eval("IseBook")) && Convert.ToBoolean(Eval("IsFree")) == true ? "$" : "$" %>'></asp:Label>
                                    <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("FinalCartPrice1")%>' />
                                    <asp:Label runat="server" ID="lblOriginalPrice" Style="display: none;" Text='<%#Convert.ToBoolean(Eval("IsFree")) == true ? "0.00" : Eval("FinalCartPrice1")%>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>


                </div>
                <!--box end-->

                <div class="w100">
                    <div class="totalpnl">
                        <div class="totalbtn">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="total" Colon="false" />
                        </div>
                        <div class="ptxt1">$<asp:Label ID="lblAmount" runat="server" Colon="false" /></div>
                    </div>
                </div>
                <%--<a href="#" class="movebtn">Proceed to Checkout</a>--%>
                <div>
                    <asp:LinkButton ID="btn_login" runat="server" class="movebtn" OnClick="Proceed_to_Checkout">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="checkout" Colon="false" />
                    </asp:LinkButton>
                </div>
            </div>
            <!--rt end-->

        </div>
        <!--midle end-->
    </div>
    <!--wrap end-->
</asp:Content>

