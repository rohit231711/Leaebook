<%@ Page Title="Book Detail" Language="C#" MasterPageFile="~/Client/Books.master"
    AutoEventWireup="true" CodeFile="BookDetail.aspx.cs" Inherits="Client_BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.magnifier.js"></script>
    <script src="<%=ResolveUrl("js/jquery.jcarousel.min.js") %>" type="text/javascript"></script>
    <link href="<%=ResolveUrl("css/skin.css") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .photoimgs
        {
            width: 1003px;
            float: left;
            overflow: hidden;
            margin-left: 0px;
        }
        .es-nav
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            $(".high_lights_row").hide();



        });
        function mycarousel_initCallback(carousel) {
            // Disable autoscrolling if the user clicks the prev or next button.
            carousel.buttonNext.bind('click', function () {
                carousel.startAuto(0);
            });

            carousel.buttonPrev.bind('click', function () {
                carousel.startAuto(0);
            });

            // Pause autoscrolling if the user moves with the cursor over the clip.
            carousel.clip.hover(function () {
                carousel.stopAuto();
            }, function () {
                carousel.startAuto();
            });
        };

        jQuery(document).ready(function () {


            $(".mycarousel").find("a").click(function () {

                window.location.href = $(this).attr("href");
            });
            jQuery('.mycarousel').jcarousel({
                auto: 0,
                scroll: 4,
                animation: 500,
                wrap: 'last',
                space: 1,
                initCallback: mycarousel_initCallback
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contant" style="margin: 0px; position: relative; top: 13px;">
        <asp:HiddenField ID="hdnCategoryID" runat="server" />
        <div class="meg-detail">
            <div>
                <div class="high_li_part1300" style="margin-left: 0px;">
                    <p class="category">
                        <a href="<%=ResolveUrl("Shop.aspx") %>" style="font-weight: bold; color: Black; text-decoration: none;">
                            Shop</a> > <span>
                                <%=BookName %></span></p>
                </div>
            </div>
            <div class="meg-slider">
                <div class="meg-big">
                    <asp:Image ID="bigimage" Width="315" Height="461" runat="server" />
                </div>
                <img src="images/meg-line.png" alt="" class="meg-line" />
                <div class="meg-thumb">
                    <p>
                        IN THIS ISSUE</p>
                    <ul>
                        <%--   <asp:Repeater ID="rptBooks" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%#Eval("DescriptionIMG") %>' class="magnify" data-magnifyto="500" >
                                        <embed src='<%#Eval("DescriptionIMG") %>' quality="high" type="application/x-shockwave-flash"   width="100%" height="100%" scale="exactfit" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                                    </a>
                                    <asp:Image ID="image" Width="81"   CssClass="magnify" data-magnifyto="500" Height="108"  Width="81"
                                        ImageUrl="" runat="server" />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>--%>
                        <asp:Repeater ID="rptBooks" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:Image ID="image" Width="81" CssClass="magnify" data-magnifyto="500" Height="108"
                                        ImageUrl='<%# Convert.ToString(Eval("DescriptionImages")).Trim() %>' runat="server" />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <br clear="all" />
                <asp:HyperLink ID="hypBookissues" Style="text-decoration: none; color: rgb(55, 106, 161);"
                    runat="server">View Back Issues</asp:HyperLink>
            </div>
            <div class="meg-contain">
                <h1 style="text-transform: uppercase; border: none; margin: 0; padding: 0; font-weight: bold;">
                    <%=BookName %></h1>
                <%-- <h2 class="bottom-title">
                    lorem ipsum dolor sit amet</h2>--%>
                <p class="meg-p" style="text-align: justify;">
                    <asp:Literal ID="litdetail" runat="server"></asp:Literal>
                </p>
                <p class="meg-price">
                    price subject to applicable taxes (vat)</p>
                <div class="meg-form">
                    <label class="meg-leb" style="padding: 10px 0 0 0">
                        publisher</label>
                    <p class="green-ngo" style="text-decoration: none">
                        <asp:Label runat="server" ID="lblPublisher"></asp:Label>
                        <asp:Image ID="imgPublisher" Style="width: 20PX; margin-left: 20PX; position: absolute;"
                            runat="server" Visible="false" />
                    </p>
                </div>
                <div class="divClear">
                </div>
                <%--   <div class="meg-form">
                    <label class="meg-leb" style="padding: 10px 0 0 0;text-decoration:none">
                        Available On</label>
                    <p class="green-ngo" style="text-decoration:none">
                        <asp:Label runat="server" ID="lblPublishDate"></asp:Label></p>
                </div>
                
                <div class="divClear">
                </div>--%>
                <div class="meg-form">
                    <label class="meg-leb" style="padding: 10px 0 0 0;">
                        Language</label>
                    <p class="green-ngo" style="text-transform: none;">
                        <asp:Label runat="server" ID="lblLanguage"></asp:Label></p>
                </div>
                <div class="divClear">
                </div>
                <div class="meg-form">
                    <label class="meg-leb" style="padding: 10px 0 0 0; text-decoration: none">
                        Country
                    </label>
                    <p class="green-ngo" style="text-decoration: none">
                        <asp:Label runat="server" ID="lblCountry"></asp:Label></p>
                </div>
                <br clear="all" />
                <div class="av-img">
                    <a href="#">
                        <img src="images/apple.png" alt="" />
                    </a><a href="#">
                        <img src="images/android.png" alt="" />
                    </a><a href="#">
                        <img src="images/windows.png" alt="" />
                    </a>
                </div>
                <div class="subscribe">
                    <a href="#">
                        <asp:ImageButton ImageUrl="~/Client/images/subscribe.png" runat="server" ID="imgbtnBuy"
                            AlternateText="" OnClick="imgbtnBuy_Click" Style="float: left; padding: 0 5px 0 0;" /></a>
                    <p class="issue-price">
                        <asp:Label runat="server" ID="lblissues" />
                        <asp:Label ID="lblIssuePrice" runat="server"></asp:Label>
                        <br />
                     <%--   <span style="">
                            <asp:Label ID="lblpriceperissue" runat="server"></asp:Label>
                        </span>--%>
                    </p>
                    <br clear="all" />
                    <hr />
                    <asp:LinkButton runat="server" OnClick="imgbtnSingleBuy_Click" Style="text-decoration: none;
                        color: Green; font-size: 15px">Or purchase
                        current issue<asp:Label runat="server" ID="CurrentPrice"></asp:Label></asp:LinkButton>
                        
                    <br clear="all" />
                    <hr />
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="imgAddtoWishlist_Click"
                        Style="text-decoration: none; color: Green; font-size: 15px">Add to WishList</asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="slider-data" style="margin: 15px 0 0 0;">
            <div class="slider-head">
                <h1 style="text-transform: uppercase;">
                    More From
                    <asp:Label ID="lblCategoryName" runat="server"></asp:Label>
                    <a class="view" runat="server" id="ancmag">View All</a></h1>
            </div>
            <div class="photoimgs">
                <!--<div class="leftarrow"></div>-->
                <div id="Div5" style="padding-left: 0px; padding-right: 0px;" class="es-carousel-wrapper slider">
                    <div class="es-carousel">
                        <div id="Div6" class="es-carousel">
                            <ul id="Ul3" style="padding-left: 0px; margin-top: 0px; margin-bottom: 0px; left: -484px"
                                class="jcarousel-skin-tango mycarousel">
                                <asp:Repeater ID="rptNewArrivals" runat="server">
                                    <ItemTemplate>
                                        <li style="height: 100%;"><a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
                                            <asp:Image ID="Image2" runat="server" Width="192" Height="240" src='<%#Eval("TitleImage") %>'
                                                alt="" CssClass="meg-img" /></a>
                                            <p class="meg-name">
                                                <%#Eval("Title") %></p>
                                            <p class="price">
                                                Issue
                                                <%#Eval("Issues") %>
                                                / RM <%#Eval("IssuesPrice")%></p>
                                            <br />
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <script type="text/javascript" src="js/jquery.elastislide.js"></script>
                <script type="text/javascript">
                    $('.slider').elastislide({
                        imageW: 500,
                        minItems: 4
                    });                        
                </script>
            </div>
        </div>
    </div>
</asp:Content>
