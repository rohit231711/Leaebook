<%@ Page Title="Book Detail" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="NewBook-Detail.aspx.cs" Inherits="NewBook_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).ready(function () {
            img_find();
            console.clear();
        });
        $(document).mouseenter(function () {
            img_find();
            console.clear();
        });
        $(document).focus(function () {
            img_find();
            console.clear();
        });

        function img_find() {
            var imgs = document.getElementsByTagName("img");
            var imgSrcs = [];
            for (var i = 0; i < imgs.length; i++) {
                imgs[i].src = imgs[i].src.replace("/us", "");
                imgSrcs.push(imgs[i].src);
            }
            console.clear();
            console.log(imgSrcs);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#share_button').click(function (e) {
                e.preventDefault();
                FB.ui(
                        {
                            method: 'feed',
                            name: document.getElementById('<%=lbl_bookname.ClientID %>').value,
                            link: window.location.href.toString(),
                            picture: document.getElementById('<%=img_book.ClientID %>').value,
                            caption: document.getElementById('<%=lbl_bookname.ClientID %>').value,
                            description: 'http://www.leaebook.com',
                            message: 'http://www.leaebook.com'
                        });
            });
        });
    </script>
    <script type="text/javascript">
        (function (d) {
            var f = d.getElementsByTagName('SCRIPT')[0], p = d.createElement('SCRIPT');
            p.type = 'text/javascript';
            p.async = true;
            p.src = '//assets.pinterest.com/js/pinit.js';
            f.parentNode.insertBefore(p, f);
        }(document));
    </script>
    <script type="text/javascript">
        window.fbAsyncInit = function () {
            FB.init({
                appId: '216438618538882',
                xfbml: true,
                version: 'v2.1'
            });
        };

        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) { return; }
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    </script>
    <script type="text/javascript">
        window.twttr = (function (d, s, id) { var t, js, fjs = d.getElementsByTagName(s)[0]; if (d.getElementById(id)) { return } js = d.createElement(s); js.id = id; js.src = "https://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); return window.twttr || (t = { _e: [], ready: function (f) { t._e.push(f) } }) }(document, "script", "twitter-wjs"));
    </script>
    <style type="text/css">
        #share_button {
            background: #4c69ba;
            background: -webkit-gradient(linear, center top, center bottom, from(#4c69ba), to(#3b55a0));
            background: -webkit-linear-gradient(#4c69ba, #3b55a0);
            border: none;
            -webkit-border-radius: 2px;
            color: #fff;
            cursor: pointer;
            font-weight: bold;
            height: 20px;
            line-height: 20px;
            padding: 0;
            text-shadow: 0 -1px 0 #354c8c;
            white-space: nowrap;
            padding: 3px 7px;
            margin-right: 5px;
        }
    </style>
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
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span style="color: White;">
                    <asp:Label runat="server" ID="lbltit"></asp:Label></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="cartbox">
            <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; width: 100%; float: left; font-size: 15px; font-family: 'abeezeeregular'; line-height: 25px;"></asp:Label>
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
                        <div id="share_button" class="Fbshare" style="float: left;">
                            Share
                        </div>
                        <a class="twitter-share-button" href="https://twitter.com/share" data-related="twitterdev"
                            data-size="large" data-count="none">
                            <img src="images/twit-blk.png" alt="" />
                        </a><a href="http://www.pinterest.com/pin/create/button/?url=http%3A%2F%2Fwww.flickr.com%2Fphotos%2Fkentbrew%2F6851755809%2F&media=http%3A%2F%2Ffarm8.staticflickr.com%2F7027%2F6851755809_df5b2051c9_z.jpg&description=Next%20stop%3A%20Pinterest"
                            data-pin-do="buttonPin" style="height: 42px; margin-left: 10px;" data-pin-config="above">
                            <img src="images/pin-blk.png" alt="" />
                        </a>
                        <script src="https://apis.google.com/js/platform.js" async defer></script>
                        <div class="g-plus" data-action="share">
                        </div>
                        <div class="au-name">
                            <Localized:LocalizedLiteral ID="llPhone" runat="server" Key="storePhone" Colon="false" />
                            <br />
                            <span class="atxt">
                                <asp:Label ID="lblNumber" runat="server" Text="Not Available"></asp:Label>
                            </span>
                        </div>
                        <div></div>
                    </div>
                </div>
            </div>
            <div class="cart-rt">
                <div class="btitle">
                    <asp:Label ID="lbl_bookname" runat="server" Text=""></asp:Label>
                </div>
                <div class="btxt">
                    <div class="au-name">
                        <Localized:LocalizedLiteral ID="lblautor" runat="server" Key="Author" Colon="false" />
                        :<span class="atxt">
                            <asp:Label ID="lbl_author" runat="server" Text=""></asp:Label>
                        </span>
                    </div>
                    <div class="au-name">
                        <Localized:LocalizedLiteral ID="lblproductcode" runat="server" Key="productcode"
                            Colon="false" />
                        : <span class="atxt">
                            <asp:Label ID="lblpro_id" runat="server"></asp:Label></span>
                    </div>

                </div>
                <div class="btxt">
                    <div class="au-name">
                        <asp:Label ID="lblID" runat="server"></asp:Label>
                        Book
                        <asp:DropDownList ID="ddlBookType" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                    </div>
                    <div class="au-name">
                        Available as eBook :
                        <asp:Label ID="lbliseBook" runat="server"></asp:Label>
                        <asp:Image ID="imgeBook" runat="server" Visible="false" />
                    </div>
                    <div class="au-name">
                        Available as Paper Book :
                        <asp:Label ID="lblispaperBook" runat="server"></asp:Label>
                        <asp:Image ID="imgPaperBook" runat="server" Visible="false" />
                    </div>
                </div>

                <div class="wishbox">
                    <div runat="server" id="eBook" style="width: 100%; display: inline-block; margin: 10px 0;">
                        <div class="au-name" style="width: 25%;">eBook Price</div>
                        <div class="prtxt" id="divprice" runat="server" style="padding: 0;">
                            <asp:Label ID="lbl_price" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="prblue" style="padding: 0;">
                            <asp:Label ID="lbl_finalprice" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div runat="server" id="paperBook" style="width: 100%; display: inline-block; margin: 10px 0;">
                        <div class="au-name" style="width: 25%;">Paper Book Price</div>
                        <div class="prtxt" id="divpaperBookprice" runat="server" style="padding: 0;">
                            <asp:Label ID="lbl_paperBookprice" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="prblue" style="padding: 0;">
                            <asp:Label ID="lbl_paperBookfinalprice" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="wishbox">
                    <div class="wbtn">
                        <asp:LinkButton ID="lnk_wishlist" runat="server" OnClick="btn_WishList_Click">
                            <div class="wishbtn" id="div_wislist" runat="server">
                                <img src="images/gray-heart.png" alt="" class="ghrt" />
                                <span class="wish">
                                    <Localized:LocalizedLiteral ID="lblwishlist" runat="server" Key="wishlist" Colon="false" />
                                </span>
                            </div>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btn_addcart" runat="server" OnClick="btn_addcart_Click">
                            <div class="crtbtn" id="div_addtocard" runat="server">
                                <img src="images/cart-icon.png" alt="" class="crtimg" />
                                <span class="add">
                                    <Localized:LocalizedLiteral ID="lbladdtocart" runat="server" Key="addtocart" Colon="false" />
                                    <Localized:LocalizedLiteral ID="lbladdtolib" runat="server" Key="AddtoLibrary1" Colon="false"
                                        Visible="False" />
                                </span>
                            </div>
                        </asp:LinkButton>
                    </div>
                    <div class="prblue" style="padding: 10px 0; margin: 0;">
                        <div class="au-name">
                            <a href="Index.aspx?id=&l=" class='boxbut' id="aBuyBook" runat="server" onserverclick="btn_addcart_Click">
                                <Localized:LocalizedLiteral ID="LocalizedLiteral12" runat="server" Key="buynow" Colon="false" />
                            </a>
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
                    <asp:LinkButton ID="lnk_writereview" runat="server" OnClientClick="return WriteReview();">
                        <div class="rview">
                            <img src="images/review.png" alt="" class="rimg" />
                            <span class="rtxt">
                                <Localized:LocalizedLiteral ID="lblwriteareview" runat="server" Key="writeareview"
                                    Colon="false" />
                            </span>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="descbox" id="div_description" runat="server">
                    <%--description--%>
                </div>
            </div>
            <div class="rviewbox">
                <div class="rviewtxt">
                    <Localized:LocalizedLiteral ID="lblwriteyourownreview" runat="server" Key="writeyourownreview"
                        Colon="false" />
                </div>
                <div class="box">
                    <div class="retxt">
                        <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="areyoureviewing"
                            Colon="false" />
                    </div>
                    <div class="ratebox">
                        <span class="raterxr">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="howtorate"
                                Colon="false" />
                            *</span> <span class="qtxt">Quality</span>
                        <div class="stars">
                            <div class="star1">
                                <span class="stxt">1
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_1" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">2
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_2" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">3
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_3" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">4
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_4" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">5
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_5" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                        </div>
                    </div>
                    <div class="infobox">
                        <div class="infolft">
                            <div class="frm-grp1">
                                <label class="nick">
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key="nickname"
                                        Colon="false" /><span class="star">*</span></label>
                                <asp:TextBox ID="txt_nickname" runat="server" class="nickinp"></asp:TextBox>
                            </div>
                            <div class="frm-grp1">
                                <label class="nick">
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral9" runat="server" Key="reviewsummary"
                                        Colon="false" /><span class="star">*</span></label>
                                <asp:TextBox ID="txt_summary" runat="server" class="nickinp"></asp:TextBox>
                            </div>
                        </div>
                        <div class="rviewbox1">
                            <label class="nick">
                                <Localized:LocalizedLiteral ID="LocalizedLiteral10" runat="server" Key="review" Colon="false" />
                                <span class="star">*</span></label>
                            <asp:TextBox ID="txt_review" runat="server" class="rmsg" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <asp:LinkButton ID="btn_submit" runat="server" class="subrew" OnClientClick="return validation();"
                            OnClick="btn_submit_Click">
                            <Localized:LocalizedLiteral ID="LocalizedLiteral11" runat="server" Key="submitreview"
                                Colon="false" />
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
