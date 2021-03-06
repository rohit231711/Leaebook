<%@ Page Title="Add-To-Cart" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="Add-To-Cart.aspx.cs" Inherits="Add_To_Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function WriteReview() {
            document.getElementById('<%=txt_nickname.ClientID %>').focus();
            return false;
        }
        function validation() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if (document.getElementById("<%= rdo_1.ClientID %>").checked || document.getElementById("<%= rdo_2.ClientID %>").checked || document.getElementById("<%= rdo_3.ClientID %>").checked || document.getElementById("<%= rdo_4.ClientID %>").checked || document.getElementById("<%= rdo_5.ClientID %>").checked) {
            }
            else {
                if (cultureLanguage == "es-ES") {
                    alert("Seleccione Ratting estrella.");
                }
                else {
                    alert("Please Select Ratting Star.");
                }
                return false;
            }
            if (document.getElementById('<%=txt_nickname.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor introduce apodo.");
                }
                else {
                    alert("Please enter nickname.");
                }
                return false;
            }
            if (document.getElementById('<%=txt_summary.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor introduce resumen.");
                }
                else {
                    alert("Please enter summary.");
                }
                return false;
            }
            if (document.getElementById('<%=txt_review.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor introduce opinión.");
                }
                else {
                    alert("Please enter review.");
                }
                return false;
            }
        }
    </script>
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="#"><Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#"><Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="BookName" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="cartbox">
            <div class="cart-lft">
                <div class="bookbox">
                    <img id="img_book" runat="server" src="images/bookimg.png" alt="" class="book" height="460"
                        width="335" />
                </div>
                <div class="sharebox">
                    <div class="sharetxt">
                        <Localized:LocalizedLiteral ID="lblshare" runat="server" Key="share" Colon="false" />                        
                        </div>
                    <div class="sh-icon">
                        <a href="#" class="fb">
                            <img src="images/fb-blk.png" alt="" /></a> <a href="#" class="twit">
                                <img src="images/twit-blk.png" alt="" /></a> <a href="#" class="fb">
                                    <img src="images/gplus-blk.png" alt="" /></a> <a href="#" class="">
                                        <img src="images/pin-blk.png" alt="" /></a>
                    </div>
                </div>
            </div>
            <div class="cart-rt">
                <div class="btitle">
                    <asp:Label ID="lbl_bookname" runat="server" Text=""></asp:Label>
                </div>
                <div class="btxt">
                    <div class="au-name">
                        <Localized:LocalizedLiteral ID="lblautor" runat="server" Key="autor" Colon="false" /> :<span class="atxt">
                            <asp:Label ID="lbl_author" runat="server" Text=""></asp:Label>
                        </span>
                    </div>
                    <div class="au-name">                        
                        <Localized:LocalizedLiteral ID="lblproductcode" runat="server" Key="productcode" Colon="false" />
                        : <span class="atxt">012 3456</span></div>
                    <div class="au-name">                    
                        <Localized:LocalizedLiteral ID="lblavailability" runat="server" Key="availability" Colon="false" />
                        :<span class="stock">In Stock</span></div>
                </div>
                <div class="wishbox">
                    <div class="prtxt">
                        $<asp:Label ID="lbl_price" runat="server" Text=""></asp:Label></div>
                    <div class="prblue">
                        $<asp:Label ID="lbl_finalprice" runat="server" Text=""></asp:Label></div>
                    <div class="wbtn">
                        <div class="wishbtn">
                            <img src="images/gray-heart.png" alt="" class="ghrt" />
                            <span class="wish">
                                <asp:LinkButton ID="lnk_wishlist" runat="server" OnClick="btn_WishList_Click">                                 
                               <Localized:LocalizedLiteral ID="lblwishlist" runat="server" Key="wishlist" Colon="false" />
                                </asp:LinkButton>
                            </span>
                        </div>
                        <div class="crtbtn">
                            <img src="images/cart-icon.png" alt="" class="crtimg" />
                            <span class="add">
                                <asp:LinkButton ID="btn_addcart" runat="server" OnClick="btn_addcart_Click">                                 
                            <Localized:LocalizedLiteral ID="lbladdtocart" runat="server" Key="addtocart" Colon="false" />
                            </asp:LinkButton>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="writepnl">
                    <div class="starbox">
                        <img src="images/big-gry.png" id="img_rate1" alt="" runat="server" />
                        <img src="images/big-gry.png" id="img_rate2" alt="" runat="server" />
                        <img src="images/big-gry.png" id="img_rate3" alt="" runat="server" />
                        <img src="images/big-gry.png" id="img_rate4" alt="" runat="server" />
                        <img src="images/big-gry.png" id="img_rate5" alt="" runat="server" />
                        <span class="ratetxt2">
                            <asp:Label ID="lbl_Totalrating" runat="server" Text=""></asp:Label>
                        </span>
                    </div>
                    <div class="rview">
                        <img src="images/review.png" alt="" class="rimg" />
                        <span class="rtxt">
                            <asp:LinkButton ID="lnk_writereview" runat="server" OnClientClick="return WriteReview();">                                        
                            <Localized:LocalizedLiteral ID="lblwriteareview" runat="server" Key="writeareview" Colon="false" />
                            </asp:LinkButton>
                        </span>
                    </div>
                </div>
                <div class="descbox" id="div_description" runat="server">
                    <%--description--%>
                </div>
            </div>
            <div class="rviewbox">
                <div class="rviewtxt">
                    <Localized:LocalizedLiteral ID="lblwriteyourownreview" runat="server" Key="writeyourownreview" Colon="false" />                    
                    </div>
                <div class="box">
                    <div class="retxt">
                        You're reviewing
                        </div>
                    <div class="ratebox">
                        <span class="raterxr">How do you rate this product? *</span> <span class="qtxt">Quality</span>
                        <div class="stars">
                            <div class="star1">
                                <span class="stxt">1 star</span><br />
                                <asp:RadioButton ID="rdo_1" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">2 star</span><br />
                                <asp:RadioButton ID="rdo_2" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">3 star</span><br />
                                <asp:RadioButton ID="rdo_3" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">4 star</span><br />
                                <asp:RadioButton ID="rdo_4" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">5 star</span><br />
                                <asp:RadioButton ID="rdo_5" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                        </div>
                    </div>
                    <div class="infobox">
                        <div class="infolft">
                            <div class="frm-grp1">
                                <label class="nick">
                                    Nickname<span class="star">*</span></label>
                                <asp:TextBox ID="txt_nickname" runat="server" class="nickinp"></asp:TextBox>
                            </div>
                            <div class="frm-grp1">
                                <label class="nick">
                                    Summary of Your Review<span class="star">*</span></label>
                                <asp:TextBox ID="txt_summary" runat="server" class="nickinp"></asp:TextBox>
                            </div>
                        </div>
                        <div class="rviewbox1">
                            <label class="nick">
                                Review<span class="star">*</span></label>
                            <asp:TextBox ID="txt_review" runat="server" class="rmsg" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <asp:LinkButton ID="btn_submit" runat="server" class="accbtn" OnClientClick="return validation();"
                            OnClick="btn_submit_Click">
                                Submit Review
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
