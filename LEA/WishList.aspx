<%@ Page Title="WishList" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="WishList.aspx.cs" Inherits="WishList" %>

<%@ Register Src="~/Includes/account_leftmenu.ascx" TagName="leftmenu" TagPrefix="lm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
        });
        function Delete() {
            var ID = $('#hdnDeleteValue').val();
            PageMethods.DeleteRecord(ID);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="#">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="mywishlist" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="accbox">
            <lm:leftmenu ID="leftmenu1" runat="server" />
            <div class="acc-rt">
                <div class="editacc">
                    <Localized:LocalizedLiteral ID="lblmywidhlist" runat="server" Key="mywishlist" Colon="false" />
                </div>
                <div class="ebook-title" id="wl" runat="server">
                    <div class="book1">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="lbl_book" Colon="false" />
                    </div>
                    <div class="edit">
                        <Localized:LocalizedLiteral ID="lblEdit" runat="server" Key="Edit" Colon="false" />
                    </div>
                    <div class="pricetxt">
                        <Localized:LocalizedLiteral ID="lblPrice" runat="server" Key="Price" Colon="false" />
                    </div>
                </div>

                <div class="ebook-box">
                    <asp:Repeater ID="rptRecords1" runat="server" OnItemCommand="del">
                        <ItemTemplate>
                            <div class="row1">
                                <div class="first">

                                    <%--<img src="Book/<%#(Eval("CategoryID"))%>/<%#(Eval("ImagePath"))%>" alt="" class="wishimg1" style = "height:135px; width:118px;"/>--%>
                                     <asp:LinkButton ID="LinkButton3"  CommandName="delete1" CommandArgument='<%# Eval("BookID") %>' runat="server" >
                                    <img src="<%# PicturePath("Book/"+Eval("CategoryID")+"/"+Eval("ImagePath").ToString().Replace(".jpg","_1.jpg")) %>" alt="" class="wishimg1" style="height: 135px; width: 98px;" />
                                         </asp:LinkButton>
                                    <div class="booktxt">
                                        <asp:LinkButton ID="LinkButton4"  CommandName="Title" CommandArgument='<%# Eval("BookID") %>' runat="server" >
                                        <span class="aname"><%#Convert.ToString(Eval("Title"))%></span>
                                            </asp:LinkButton>
                                        <span class="bname"><%#Convert.ToString(Eval("Autoher"))%></span>
                                        <span class="bname">
                                            <asp:Label runat="server" ID="order">
                                                <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="orderid" Colon="false" /></asp:Label>: <%#Convert.ToString(Eval("WishID"))%></span><span class="bname" runat="server" visible='<%# Convert.ToBoolean(Eval("IsPaperBook")) %>'>Quantity : <asp:Label ID="lblQuanitity" runat="server" Text='<%# Eval("Qauntity") %>' Visible='<%# Eval("IsPaperBook") %>'
                                                Style="width: 15%; padding: 5px 0px 5px 5px;" onkeydown="return onkeyupp();"></asp:Label><%--<input type="number" min="1" name="txtQuantity" value='<%# Eval("Qauntity") %>' id="txtQuantity"
                                                style="width: 15%; padding: 5px 0px 5px 5px;" onkeydown="return onkeyupp();" />--%></span><div class="rate" style="font-size: 10pt;">
                                            <img src="images/<%#Convert.ToString(Eval("Ratting"))%>_star.png" id="img_rate1" alt="" height="35px;" />
                                            <span class="ratetxt" style="margin-top: 9px; margin-left: 5px;">(<%#Convert.ToString(Eval("TotalReview"))%><asp:Label ID="tr" runat="server">
                                                    <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="rating" Colon="false" /></asp:Label>) </span></div></div><input type="hidden" value="0" id="hdnDeleteValue" /></div><div class="delbtn">
                                    <asp:LinkButton ID="LinkButton1" type="button" OnClientClick='<%# getRemoveString() %>' CommandName="delete" CommandArgument='<%#Eval("BookID") + "," + Eval("IsPaperBook") + "," + Eval("IseBook") %>' runat="server" Text="Delete"><img src="images/delete.png" alt="" /></asp:LinkButton></div><div class="ptxt">
                                    <asp:Label runat="server" ID="lblsymbol" Text='<%#Convert.ToBoolean(Eval("IsFree")) == true ? "Free" : "$" %>'></asp:Label><asp:Label runat="server" ID="lblPrice" Text='<%#Convert.ToBoolean(Eval("IsFree")) == true ? "0.00" : Eval("FinalCartPrice1")%>' />
                                    <asp:Label runat="server" ID="lblOriginalPrice" Style="display: none;" Text='<%#Convert.ToBoolean(Eval("IsFree")) == true ? "0.00" : Eval("FinalCartPrice1")%>' />
                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>





                </div>
                <!--box end-->
                <br />
                <asp:Label ID="lblDefaultMessage" runat="server" Text=" You have no any eBook on your Wish list" Style="font-family: abeezeeregular; color: Gray;" Visible="false">
                </asp:Label><div class="w100" id="tot" runat="server">
                    <div class="totalpnl">
                        <div class="totalbtn">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="total" Colon="false" />
                        </div>
                        <div class="ptxt1">$<asp:Label ID="lblAmount" runat="server" Colon="false" /></div>
                    </div>
                </div>
                <%--<a href="Checkout.aspxl=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>&id=" class="movebtn" onclick="move_to_cart">Move to Cart</a>--%>
                <div id="mov" runat="server">
                    <asp:LinkButton ID="btn_login" runat="server" class="movebtn" OnClick="move_to_cart">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="movetocart" Colon="false" />
                    </asp:LinkButton></div></div><!--rt end--></div><!--midle end--></div><!--wrap end--></asp:Content>