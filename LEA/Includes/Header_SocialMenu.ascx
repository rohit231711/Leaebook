<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header_SocialMenu.ascx.cs"
    Inherits="Includes_Header_SocialMenu" %>
<div class="social">
    <%--<a href="" id="fb" runat = "server" target="_blank">
        <img src="<%= Config.WebSiteMain %>images/ficon.png" alt="" />
     </a> 
     <a href="" id="ti" runat = "server" target="_blank">
         <img src="<%= Config.WebSiteMain %>images/ticon.png" alt="" />
     </a> 
     <a href="" id="gp" runat = "server" target="_blank">
         <img src="<%= Config.WebSiteMain %>images/gplusicon.png" class="gogle" alt="" />
     </a>
    <a href="" id="insta" runat = "server" target="_blank">
         <img src="<%= Config.WebSiteMain %>images/insta.png" class="instagram" alt="">
     </a>--%>
    <ul>
        <li>
            <a  id="fb" runat="server" target="_blank">
            <i class="fa fa-facebook" aria-hidden="true"></i></a></li>
        <li>
            <a  id="ti" runat="server" target="_blank">
        <i class="fa fa-twitter" aria-hidden="true"></i></a>
        </li>
        <li>
            <a id="gp" runat="server" target="_blank">
        <i class="fa fa-google-plus" aria-hidden="true"></i></a>
        </li>
        <li>
            <a id="insta" runat="server" target="_blank">
        <i class="fa fa-instagram" aria-hidden="true"></i></a>
        </li>                    
    </ul>
</div>
