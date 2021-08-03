<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Left_part.ascx.cs" Inherits="Includes_Left_menu" %>
<div class="leftpanel">
    <h1>
        <Localized:LocalizedLiteral ID="lblnew" runat="server" Key="Categories" Colon="false" /></h1>
    <div class="leftmenu">
        <ul id="menu_ul" runat="server">
            <%--<li><a class="active" href="#">Biography</a></li>
            <li><a href="#">Fiction &amp; Literature</a></li>
            <li><a href="#">History</a></li>
            <li><a href="#">Non-Fiction</a></li>
            <li><a href="#">Professional</a></li>
            <li><a href="#">Romance</a></li>
            <li><a href="#">Technical</a></li>
            <li><a href="#">Travels</a></li>
            <li><a href="#">Sports</a></li>
            <li><a href="#">Business</a></li>
            <li><a href="#">Social Science</a></li>
            <li><a href="#">Medical</a></li>
            <li><a href="#">Game</a></li>
            <li class="catmarg"><a href="#">Photography</a></li>--%>
        </ul>
    </div>

    <!-- route -->
    <%--<div class="rmm">
        <ul>
            <li><a href="Biography.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="last" id="aBiography1" runat="server">
                <Localized:LocalizedLiteral ID="lblbiography1" runat="server" Key="biography" Colon="false" /></a></li>

        </ul>
    </div>
    <div class="menu">
        <ul>
            <li><a href="Biography.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="last" id="aBiography" runat="server">
                <Localized:LocalizedLiteral ID="lblbiography" runat="server" Key="biography" Colon="false" />
            </a></li>
        </ul>
    </div>--%>
    <!-- route over-->
    <div class="topsell">
        <div class="topselltxt">
            <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="topseller" Colon="false" />
        </div>
        <div class="chetanbhag">
            <div class="leftrigimg">
                <a href="#" id="bookimg_link" runat="server">
                    <img src="#" alt="" id="img_topseller" runat="server" height='306' width='223' /></a>
            </div>

            <div class="divcen">
                <div class="bbokname">
                    <a href="#" style='color: #616161' id="bookname_link" runat="server">
                        <asp:Label ID="lbl_booknmae" runat="server" Text=""></asp:Label>
                    </a>
                </div>
                <div class="dolorti" style="width: 223px;">
                    <div class="namkl" style="width: 50%; text-align: left;">
                        <asp:Label ID="lbl_amount" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="namkl" style="width: 50%; text-align: right;">
                        <asp:Label ID="lbl_amount1" runat="server" Text=""></asp:Label>
                    </div>

                </div>
            </div>
            <a href="#" class="buynowbutt" id="buynow_link" runat="server" onclick="">
                <asp:Label ID="buy" runat="server"></asp:Label></a>
            <%--             <a href="#" class="buynowbutt" id="buynow_link1" runat="server" ><Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="AddtoLibrary" Colon="false"/></a>--%>
        </div>
    </div>
</div>
