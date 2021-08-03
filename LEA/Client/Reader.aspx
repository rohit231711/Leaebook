<%@ Page Title="safs" Language="C#" AutoEventWireup="true" CodeFile="Reader.aspx.cs"
    Inherits="Client_Reader" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en" class="no-js">
<head>
    <meta charset="UTF-8" />
    <link href="../images/fevicon.png" rel="shortcut icon" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Fullscreen Pageflip Layout with BookBlock</title>
    <meta name="description" content="Fullscreen Pageflip Layout with BookBlock" />
    <meta name="keywords" content="fullscreen pageflip, booklet, layout, bookblock, jquery plugin, flipboard layout, sidebar menu" />
    <meta name="author" content="Codrops" />
    <link rel="stylesheet" type="text/css" href="css/jquery.jscrollpane.custom.css" />
    <link rel="stylesheet" type="text/css" href="css/bookblock.css" />
    <link rel="stylesheet" type="text/css" href="css/custom1.css" />
    <script src="js/modernizr.custom.79639.js"></script>
</head>
<body>
    <div style="background: #399901;">
        <a href="http://themagz.net/Client/Index.aspx">
            <img style="margin: 15px 0px 15px 40px;" alt="themagz" src="http://themagz.net/client/images/logo_green.png"></a></div>
    <div id="container" class="container">
        <div class="menu-panel">
            <h3>
                Table of Contents</h3>
            <ul id="menu-toc" class="menu-toc" style="margin-left: 0px; overflow: scroll; height: 100%;">
                <asp:Repeater ID="rptImages" runat="server" OnItemDataBound="rptImages_ItemDataBound">
                    <ItemTemplate>
                        <li><a id="link" runat="server"></a></li>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="bb-custom-wrapper">
            <div id="bb-bookblock" class="bb-bookblock">
                <%=Reader %>
            </div>
            <nav> <span id="bb-nav-prev">&larr;</span>
    <span id="bb-nav-next">&rarr;</span> </nav>
        </div>
    </div>
    <!-- /container -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="js/jquery.mousewheel.js"></script>
    <script src="js/jquery.jscrollpane.min.js"></script>
    <script src="js/jquerypp.custom.js"></script>
    <script src="js/jquery.bookblock.js"></script>
    <script src="js/page.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.magnifier.js"></script>
    <script>
        $(function () {

            Page.init();

        });
		</script>
</body>
</html>
