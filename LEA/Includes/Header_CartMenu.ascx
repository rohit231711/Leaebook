<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header_CartMenu.ascx.cs"
    Inherits="Includes_Header_CartMenu" %>

<div class="mycartmenu">
    <ul>
        <li class="css_welcomeuser"><a href="<%= Config.WebSiteMain %><%=(System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US" ? "us" : "")  %>/Login">
            <Localized:LocalizedLiteral ID="lblsignin" runat="server" Key="signin" Colon="false" />
        </a>
            <asp:Label ID="lbl_username" runat="server" Text="" Style="color: #6CB1FF; padding-right: 10px;"></asp:Label>
        </li>
        <li id="lblmyaccount" runat="server"><a href="<%= Config.WebSiteMain %>Account.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
            <Localized:LocalizedLiteral runat="server" Key="myaccount" Colon="false" />
        </a></li>
        <li>
            <a href="<%= Config.WebSiteMain %><%=(System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US" ? "us" : "")  %>/ShipmentTracking">
                <Localized:LocalizedLiteral ID="lblTrackOrder" runat="server" Key="trackorder" Colon="false" />
            </a>
        </li>
        <li class="menulastnm"><a href="#" id="checkoutlink" runat="server">
            <Localized:LocalizedLiteral ID="lblmycart" runat="server" Key="mycart" Colon="false" />
        </a>
            <a href="#" class="last" id="checkoutlink1" runat="server">
                <div class="boxesca">

                    <div class="arrow">
                        <img src="<%= Config.WebSiteMain %>images/cartarrow.png" alt="" />
                    </div>
                    <div class="mycartacbox">

                        <asp:Label ID="cntcart" runat="server" Style="color: #ffb849;"></asp:Label>

                    </div>
                </div>
            </a>
        </li>
        <li class="menulastnm">
            <div class="logout_top">
                <a href="../Login.aspx?logout=1&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lbllogout" runat="server" Key="logout" Colon="false" /></a>
            </div>
        </li>
    </ul>



</div>
<div style="clear: both;"></div>
