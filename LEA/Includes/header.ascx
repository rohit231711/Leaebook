<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="Includes_header" %>
<div class="logo">
    <a href="<%= Config.WebSiteMain %>Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aIndex1"  runat="server">
        <img src="<%= Config.WebSiteMain %>images/header_logo.png" alt="" />
    </a>
</div>
<div class="forreading">
    <Localized:LocalizedLiteral ID="lblforreading" runat="server" Key="ForReading" Colon="false" />
</div>
