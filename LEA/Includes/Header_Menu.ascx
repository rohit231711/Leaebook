<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header_Menu.ascx.cs" Inherits="Includes_Header_Menu" %>
<!--************************hedar start**********************-->

<div class="rmm">
    <ul>
        <li><a href="../Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aIndex1" runat="server">
       <Localized:LocalizedLiteral ID="lblhome1" runat="server" Key="home" Colon="false" />
        </a></li>
        <li><a href="CMS_Content.aspx?id=24&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="awhatwedo1" runat="server">
        <Localized:LocalizedLiteral ID="lblwhatwedo1" runat="server" Key="whatwedo" Colon="false" />
        </a></li>
        <li><a href="Category.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="acategory1" runat="server">
           <Localized:LocalizedLiteral ID="lblcategories1" runat="server" Key="categories" Colon="false" />
        </a></li>
        <li><a href="Special-Offer.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aspecialoffer1" runat="server">
           <Localized:LocalizedLiteral ID="lblspecialoffer1" runat="server" Key="specialoffer" Colon="false" />
        </a></li>
        <li id="liLib1" runat="server">
            <a href="MyLibrary.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" style = "margin-right: 7px;" id="aMyLibrary1" runat="server">
        <Localized:LocalizedLiteral ID="lblmylibrary1" runat="server" Key="mylibrary" Colon="false" />
        </a></li>
        <li><a href="ContactUs.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aContactUs1" runat="server">
      <Localized:LocalizedLiteral ID="lblcontact1" runat="server" Key="contact" Colon="false" />
        </a></li>
        
        <li><a href="Blog.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="last" id="aBlog1" runat="server">
        <Localized:LocalizedLiteral ID="lblblog1" runat="server" Key="blog" Colon="false" /></a></li>
            
    </ul>
</div>

<div class="menu">
    <ul>
        <li><a href="../Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aIndex" runat="server">
            <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
        </a></li>
        <li><a href="CMS_Content.aspx?id=24&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="awhatwedo" runat="server">
            <Localized:LocalizedLiteral ID="lblwhatwedo" runat="server" Key="whatwedo" Colon="false" />
        </a></li>
        <li><a href="Category.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="acategory" runat="server">
            <Localized:LocalizedLiteral ID="lblcategories" runat="server" Key="categories" Colon="false" />
        </a></li>
        <li><a href="Special-Offer.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aspecialoffer" runat="server">
           <Localized:LocalizedLiteral ID="lblspecialoffer" runat="server" Key="specialoffer" Colon="false" />
        </a></li>
        <li id="liLib" runat="server">
            <a href="MyLibrary.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" style = "margin-right: 7px;" id="aMyLibrary" runat="server">
            <Localized:LocalizedLiteral ID="lblmylibrary" runat="server" Key="mylibrary" Colon="false" />
        </a></li>
        <li><a href="ContactUs.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aContactUs" runat="server">
            <Localized:LocalizedLiteral ID="lblcontact" runat="server" Key="contact" Colon="false" />
        </a></li>
        
        <li><a href="Blog.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="last" id="aBlog" runat="server">
            <Localized:LocalizedLiteral ID="lblblog" runat="server" Key="blog" Colon="false" />
        </a></li>

    </ul>
</div>

