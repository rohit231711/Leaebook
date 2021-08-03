<%@ Page Title="" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="Book-Detail.aspx.cs" Inherits="Book_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .au-name, .atxt {
            font-size: 15px;
        }

        .vis-box-button-div {
            width: 100%;
            float: left;
        }

        .vis-box-button-capcha {
            float: left;
        }

        .BookTypePrice {
            width: 100%;
            display: inline-block;
            margin: 10px 0;
        }
        .asd-wbtn {
            width:100%;
        }
        .asd-wbtn a {
            vertical-align:top;
        }
        .asd-wbtn .wishbtn {
            min-height: 42px;
            margin-top:18px; 
        }
    </style>
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src='https://www.google.com/recaptcha/api.js' type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            debugger;
            img_find();
            console.clear();
            var dropdata = document.getElementById("<%=ddlBookType.ClientID%>").value;
            if (dropdata == "1") {
                var price = $("#ContentPlaceHolder1_lbl_finalprice").html();
                if (price == "Free" || price == "gratis") {
                    $('#ContentPlaceHolder1_div_wislist').css('display', 'none');
                    $('#Addtocart').css('display', 'none');
                    $('#library').css('display', 'inline-block');
                }
                else {
                    $('#ContentPlaceHolder1_lnk_wishlist').css('display', 'inline-block');
                    $('#ContentPlaceHolder1_btn_addcart').css('display', 'inline-block');
                }
                $('#ContentPlaceHolder1_paperBook').css('display', 'none');
                $('#ContentPlaceHolder1_eBook').css('display', 'inline-block');
            }
            else if (dropdata == "2") {
                $('#ContentPlaceHolder1_div_wislist').css('display', 'inline-block');
                $('#Addtocart').css('display', 'inline-block');
                $("#library").css('display', 'none');
                $('#ContentPlaceHolder1_eBook').css('display', 'none');
                $('#ContentPlaceHolder1_paperBook').css('display', 'inline-block');
            }
            else {
                $('#ContentPlaceHolder1_paperBook').css('display', 'inline-block');
                $('#ContentPlaceHolder1_eBook').css('display', 'inline-block');
                $('#ContentPlaceHolder1_div_wislist').css('display', 'inline-block');
                $('#Addtocart').css('display', 'inline-block');
            }

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
                //imgs[i].src = imgs[i].src.replace("/us", "");
                imgs[i].src = imgs[i].src.replace("/es", "");
                imgs[i].src = imgs[i].src.replace("/book-detail/us", "");
                imgs[i].src = imgs[i].src.replace("/book-detail/es", "");
                imgs[i].src = imgs[i].src.replace("/book-detail", "");
                imgSrcs.push(imgs[i].src);
            }
            console.clear();
            console.log(imgSrcs);
        }
        function ScrollOnDiv() {
            $('#writereviewbox').animate({ scrollTop: 0 }, 1000);
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#share_button').click(function (e) {
                e.preventDefault();
                FB.ui(
                    {
                        method: 'feed',
                        name: document.getElementById('<%=lbl_bookname.ClientID %>').innerText,
                        link: window.location.href.toString(),
                        picture: document.getElementById('<%=img_book.ClientID %>').src,
                        caption: document.getElementById('<%=lbl_bookname.ClientID %>').innerText,
                        description: 'https://www.leatodo.com',
                        message: 'https://www.leatodo.com'
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
                appId: '1459528584336427',
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
            debugger;
            if ($('[id$="Check_User_Session"]').val() == 'Session') {
                $('#writereviewbox').show();
                document.getElementById('<%=txt_summary.ClientID %>').focus();
                return false;
            }
            else {
                //window.location.href = 'http://localhost:49832/Login.aspx?l=' + '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
                window.location.href = 'http://www.leatodo.com/Login.aspx?l=' + '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
                $('#writereviewbox').hide();
                return false;
            }
        }

        function validation() {
            debugger;
            var d = new Date();
            document.getElementById("<%= hdnDatetime.ClientID %>").value = d.toLocaleString();
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if ($("#txt_Globalsearch").val() != "") {
                $("#txt_Globalsearch").keyup(function (event) {
                    if (event.keyCode == 13) {
                        $("#Button1").click();
                    }
                });
            }
            else {
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
        }

        function ChnageBookDiv(e) {
            debugger;
            if (e.value == '1') {
                var price = $("#ContentPlaceHolder1_lbl_finalprice").html();
                if (price == "Free" || price == "gratis") {
                    $('#ContentPlaceHolder1_div_wislist').css('display', 'none');
                    $('#Addtocart').css('display', 'none');
                    $('#library').css('display', 'inline-block');
                }
                else {
                    $('#ContentPlaceHolder1_lnk_wishlist').css('display', 'inline-block');
                    $('#ContentPlaceHolder1_btn_addcart').css('display', 'inline-block');
                }
                $('#ContentPlaceHolder1_paperBook').css('display', 'none');
                $('#ContentPlaceHolder1_eBook').css('display', 'inline-block');
            }
            else if (e.value == '2') {
                $('#ContentPlaceHolder1_div_wislist').css('display', 'inline-block');
                $('#Addtocart').css('display', 'inline-block');
                $("#library").css('display', 'none');
                $('#ContentPlaceHolder1_eBook').css('display', 'none');
                $('#ContentPlaceHolder1_paperBook').css('display', 'inline-block');
            }
            else {
                $('#ContentPlaceHolder1_paperBook').css('display', 'inline-block');
                $('#ContentPlaceHolder1_eBook').css('display', 'inline-block');
                $('#ContentPlaceHolder1_div_wislist').css('display', 'inline-block');
                $('#Addtocart').css('display', 'inline-block');
                 $("#library").css('display', 'none');
            }
        }

        function readmore(e) {
            debugger;
            if (e.innerText == 'Read More') {
                $('#ContentPlaceHolder1_ltrReviewList').css('max-height', '100%');
                e.innerText = "Read Less";
            }
            else {
                $('#ContentPlaceHolder1_ltrReviewList').css('max-height', '300px');
                e.innerText = "Read More";
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
                    <img id="img_book" runat="server" src="<%= Config.WebSiteMain %>images/bookimg.png" alt="" class="book" height="460"
                        width="335" />
                </div>
                <div class="sharebox">
                    <div class="sharetxt">
                    </div>
                    <ul>
                        <li class="share">Share</li>
                        <li class="fb">
                            <a id="fb" runat="server" target="_blank" href="https://www.facebook.com/">
                                <i class="fa fa-facebook" aria-hidden="true"></i></a></li>
                        <li class="tw">
                            <a id="ti" runat="server" target="_blank" href="https://twitter.com/">
                                <i class="fa fa-twitter" aria-hidden="true"></i></a>
                        </li>
                        <li class="pin">
                            <a id="insta" runat="server" target="_blank" href="https://in.pinterest.com/">
                                <i class="fa fa-pinterest-p" aria-hidden="true"></i></a>
                        </li>
                        <li class="gp">
                            <a id="gp" runat="server" target="_blank" href="https://plus.google.com">
                                <i class="fa fa-google-plus" aria-hidden="true"></i></a>
                        </li>

                    </ul>

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
                        Book
                        <asp:DropDownList ID="ddlBookType" runat="server" onchange="ChnageBookDiv(this);" AppendDataBoundItems="true"></asp:DropDownList>
                    </div>
                    <div class="au-name">
                        <Localized:LocalizedLabel ID="lbleBook" runat="server" Key="availableeBook" Colon="false"></Localized:LocalizedLabel>
                        <asp:Label ID="lbliseBook" runat="server"></asp:Label>
                        <asp:Image ID="imgeBook" runat="server" Visible="false" />
                    </div>
                    <div class="au-name">
                        <Localized:LocalizedLabel ID="lblpaperBook" runat="server" Key="availablePaperBook" Colon="false"></Localized:LocalizedLabel>
                        <asp:Label ID="lblispaperBook" runat="server"></asp:Label>
                        <asp:Label ID="lblStock" runat="server"></asp:Label>
                        <asp:Image ID="imgPaperBook" runat="server" Visible="false" />
                    </div>
                </div>

                <div class="wishbox">
                    <div runat="server" id="eBook" class="BookTypePrice">
                        <div class="au-name" style="width: 25%;">eBook Price</div>
                        <div class="prtxt" id="divprice" runat="server" style="padding: 0;">
                            <asp:Label ID="lbl_price" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="prblue" style="padding: 0;">
                            <asp:Label ID="lbl_finalprice" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div runat="server" id="paperBook" class="BookTypePrice">
                        <div class="au-name" style="width: 25%;">Paper Book Price</div>
                        <div class="prtxt" id="divpaperBookprice" runat="server" style="padding: 0;">
                            <asp:Label ID="lbl_paperBookprice" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="prblue" style="padding: 0;">
                            <asp:Label ID="lbl_paperBookfinalprice" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="au-name">
                    <Localized:LocalizedLiteral ID="llPhone" runat="server" Key="storePhone" Colon="false" />
                    <br />
                    <span class="atxt">
                        <asp:Label ID="lblNumber" runat="server" Text="Not Available"></asp:Label>
                    </span>
                </div>
                <div class="wishbox asd-wishbox" id="divadd" runat="server">
                    <div class="prblue" style="padding: 10px 0; margin: 0;">
                        <div class="au-name">
                            <a href="Index.aspx?id=&l=" class='boxbut' id="aBuyBook" runat="server" onserverclick="btn_addcart_Click" visible="false">
                                <Localized:LocalizedLiteral ID="LocalizedLiteral12" runat="server" Key="buynow" Colon="false" />
                            </a>
                        </div>
                    </div>
                    <div class="wbtn asd-wbtn">
                        <asp:LinkButton ID="lnk_wishlist" runat="server" OnClick="btn_WishList_Click">
                            <div class="wishbtn" id="div_wislist" runat="server">
                                <img src="<%= Config.WebSiteMain %>images/gray-heart.png" alt="" class="ghrt" />
                                <span class="wish">
                                    <Localized:LocalizedLiteral ID="lblwishlist" runat="server" Key="wishlist" Colon="false" />
                                </span>
                            </div>
                        </asp:LinkButton><asp:LinkButton ID="btn_addcart" runat="server" OnClick="btn_addcart_Click">
                            &nbsp;&nbsp;
                            <div class="crtbtn-1" id="div_addtocard" runat="server">
                                <div class="add-cart-btn" id="Addtocart" style="display: inline-block; float: left; background: #3a96fd; margin: 0 15px 16px 0px; border-radius: 4px; padding: 11px 15px; height: 42px; line-height: 20px;">
                                    <img src="<%= Config.WebSiteMain %>images/cart-icon.png" alt="" class="crtimg" />
                                    <span class="add">
                                        <Localized:LocalizedLiteral ID="lbladdtocart" runat="server" Key="addtocart" Colon="false" />
                                    </span>
                                </div>
                                <div class="add-cart-btn" id="library" style="display:none; float: left; background: #3a96fd; margin: 0 15px 16px 0px; border-radius: 4px; padding: 11px 15px; height: 42px; line-height: 20px;">
                                    <span class="add add-1">
                                        <Localized:LocalizedLiteral ID="lbladdtolib" runat="server" Key="AddtoLibrary1" Colon="false"
                                            Visible="false" />
                                    </span>
                                </div>
                            </div>
                        </asp:LinkButton></div></div><div class="writepnl">
                    <div class="starbox" style="width: 100%; margin-bottom: 10px">
                        <img src="/images/big-gry.png" id="img_rate1" alt="" runat="server" /> <img src="/images/big-gry.png" id="img_rate2" alt="" runat="server" /> <img src="/images/big-gry.png" id="img_rate3" alt="" runat="server" /> <img src="/images/big-gry.png" id="img_rate4" alt="" runat="server" /> <img src="/images/big-gry.png" id="img_rate5" alt="" runat="server" /> <span class="ratetxt2"><asp:Label ID="lbl_Totalrating" runat="server" Text=""></asp:Label></span></div><span style="font-family: 'amaranthregular'; padding-left: 11%; margin-top: 10px;">Rating : <asp:Label ID="lblRatingoutofall" runat="server" Text=""></asp:Label></span><asp:HiddenField ID="Check_User_Session" runat="server" />

                </div>
                <div class="descbox" id="div_description" runat="server">
                    <%--description--%>
                </div>
            </div>

            <div style="width: 100%; float: left; font-size: 24px; font-weight: bold; border-bottom: 1px solid #c2c2c2; font-family: 'amaranthregular';">
                <div style="float: left; padding-bottom: 10px">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral13" runat="server" Key="custreview" Colon="false" />
                </div>
                <div>
                    <asp:LinkButton ID="lnk_writereview" runat="server" OnClientClick="return WriteReview();" Visible="false">
                        <div class="rview">
                            <span class="rtxt">
                                <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                                <span>
                                    <Localized:LocalizedLiteral ID="lblwriteareview" runat="server" Key="writeareview"
                                        Colon="false" /></span>
                            </span>
                        </div>
                    </asp:LinkButton></div></div><div id="ltrReviewList" runat="server" style="font-family: 'amaranthregular'; float: left; width: 100%; max-height: 300px; overflow: hidden;">
            </div>
            <div style="width: 100%; float: left; padding-top: 20px" id="divReadmore" runat="server"><a id="reviviewreadmore" href="Javascript:void(0)" onclick="return readmore(this);">Read More</a></div><div class="rviewbox" id="writereviewbox" hidden>
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
                            *</span> <span class="qtxt">Quality</span> <div class="stars">
                            <div class="star1">
                                <span class="stxt">1 <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_1" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">2 <Localized:LocalizedLiteral ID="LocalizedLiteral4" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_2" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">3 <Localized:LocalizedLiteral ID="LocalizedLiteral5" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_3" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">4 <Localized:LocalizedLiteral ID="LocalizedLiteral6" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_4" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                            <div class="star1">
                                <span class="stxt">5 <Localized:LocalizedLiteral ID="LocalizedLiteral7" runat="server" Key="star" Colon="false" /></span><br />
                                <asp:RadioButton ID="rdo_5" runat="server" class="rdbtn" GroupName="rdostar" />
                            </div>
                        </div>
                    </div>
                    <div class="infobox">
                        <div class="infolft">
                            <%--<div class="frm-grp1">
                                <label class="nick">
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral8" runat="server" Key="nickname"
                                        Colon="false" /><span class="star">*</span></label>
                                <asp:TextBox ID="txt_nickname" runat="server" class="nickinp"></asp:TextBox>
                            </div>--%>
                            <div class="frm-grp1">
                                <label class="nick">
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral10" runat="server" Key="review" Colon="false" />
                                    <span class="star">*</span></label> <asp:TextBox ID="txt_summary" runat="server" class="nickinp"></asp:TextBox></div><div class="frm-grp1">
                                <div class="g-recaptcha" data-sitekey="6LcnLEEUAAAAAFV0xViQvgSNVbJVM9Z7T3PbTPwu"></div>
                            </div>
                        </div>
                        <div class="rviewbox1">
                            <label class="nick">
                                <Localized:LocalizedLiteral ID="LocalizedLiteral9" runat="server" Key="reviewsummary"
                                    Colon="false" /><span class="star">*</span></label> <asp:TextBox ID="txt_review" runat="server" class="rmsg" TextMode="MultiLine"></asp:TextBox></div><div class="vis-box-button-div">
                            <div class="vis-box-button-btn">
                                <asp:HiddenField ID="hdnDatetime" runat="server" />
                                <asp:LinkButton ID="btn_submit" runat="server" class="subrew" Style="float: left;" OnClientClick="return validation();"
                                    OnClick="btn_submit_Click">
                                    <Localized:LocalizedLiteral ID="LocalizedLiteral11" runat="server" Key="submitreview"
                                        Colon="false" />
                                </asp:LinkButton></div></div></div></div></div></div></div></asp:Content>