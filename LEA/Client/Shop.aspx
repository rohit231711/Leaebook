<%@ Page Title="Shop" Language="C#" MasterPageFile="~/Client/Books.master" AutoEventWireup="true"
    CodeFile="Shop.aspx.cs" Inherits="Client_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.jcarousel.min.js") %>" type="text/javascript"></script>
    <link href="<%=ResolveUrl("css/skin.css") %>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .photoimgs
        {
            width: 985px;
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
    <div class="contant" style="margin: 0px; position: relative; top: -30px;">
        <div class="high_li_part1300" style="margin-top: 43px;">
            <div class="cat">
                <div class="explore-head">
                    <h3>
                        Category</h3>
                    <div class="divClear">
                    </div>
                    <div class="exp-list">
                        <asp:DataList RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Vertical" ID="rptcategory"
                            runat="server">
                            <ItemTemplate>
                                <ul>
                                    <li><a href="Books.aspx?catid=<%#Eval("CategoryID") %>">
                                        <%#Eval("CategoryName") %></a> </li>
                                </ul>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlTopten">
            <div class="slider1">
                <div class="slider-head">
                    <h1>
                        TOP 10 <a href="AllBooks.aspx?t=Top10" class="view">View All</a></h1>
                </div>
                <div class="photoimgs" style="margin-left: 0px;">
                    <!--<div class="leftarrow"></div>-->
                    <div id="Div1" style="padding-left: 0px; padding-right: 0px;" class="es-carousel-wrapper slider">
                        <div class="es-carousel">
                            <div id="Div2" class="es-carousel">
                                <ul id="Ul1" style="padding-left: 0px; margin-top: 0px; margin-bottom: 0px; left: -484px"
                                    class="jcarousel-skin-tango mycarousel">
                                    <asp:Repeater ID="rptBooklist" runat="server">
                                        <ItemTemplate>
                                            <li style="height: 100%;"><a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
                                                <asp:Image runat="server" Width="192" Height="240" src='<%#Eval("TitleImage") %>'
                                                    CssClass="meg-img" /></a>
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
                            imageW: 1024,
                            minItems: 4
                        });                        
                    </script>
                </div>
            </div>
        </asp:Panel>
        <div class="slider1">
            <div class="slider-head">
                <h1>
                    FEATURED<a href="AllBooks.aspx?t=Featured" class="view">View All</a></h1>
            </div>
            <div class="photoimgs">
                <!--<div class="leftarrow"></div>-->
                <div id="Div3" style="padding-left: 0px; padding-right: 0px;" class="es-carousel-wrapper slider">
                    <div class="es-carousel">
                        <div id="Div4" class="es-carousel">
                            <ul id="Ul2" style="padding-left: 0px; margin-top: 0px; margin-bottom: 0px; left: -484px"
                                class="jcarousel-skin-tango mycarousel">
                                <asp:Repeater ID="rptFeatured" runat="server">
                                    <ItemTemplate>
                                        <li style="height: 100%;"><a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
                                            <asp:Image ID="Image1" runat="server" Width="192" Height="240" src='<%#Eval("TitleImage") %>'
                                                alt="" CssClass="meg-img" /></a>
                                            <p class="meg-name">
                                                <%#Eval("CategoryName") %></p>
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
        <div class="slider1">
            <div class="slider-head">
                <h1>
                    NEW ARRIVALS<a href="AllBooks.aspx?t=New Arrival" class="view">View All</a></h1>
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
                                                <%#Eval("CategoryName") %></p>
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
