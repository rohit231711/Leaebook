<%@ Page Title="" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="sitemap.aspx.cs" Inherits="sitemap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblsitemap" runat="server" Key="sitemap" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <%-- <div class="rm" >--%>
    <div class="wrap">
        <div class="logintxt">
            <h1><a href="#" style="color: Black;">
                <Localized:LocalizedLiteral ID="lblsitemap1" runat="server" Key="sitemap" Colon="false" /></a></h1>
        </div>
        <div class="sitemap">
            <div class="site-left">
                <div>
                    <h5 class="list-main-title">
                        <a href="../Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aIndex1" runat="server">
                            <Localized:LocalizedLiteral ID="lblhome1" runat="server" Key="home" Colon="false" />
                        </a>

                    </h5>
                    <h5 class="list-main-title">
                        <a href="CMS_Content.aspx?id=24&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="awhatwedo1" runat="server">
                            <Localized:LocalizedLiteral ID="lblwhatwedo1" runat="server" Key="whatwedo" Colon="false" />
                        </a>

                    </h5>
                    <h5 class="list-main-title">
                        <a href="Special-Offer.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aspecialoffer1" runat="server">
                            <Localized:LocalizedLiteral ID="lblspecialoffer1" runat="server" Key="specialoffer" Colon="false" />
                        </a>
                    </h5>
                    <h5 class="list-main-title">
                        <a href="MyLibrary.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" style="margin-right: 7px;" id="aMyLibrary1" runat="server">
                            <Localized:LocalizedLiteral ID="lblmylibrary1" runat="server" Key="mylibrary" Colon="false" />
                        </a>

                    </h5>

                    <h5 class="list-main-title">
                        <Localized:LocalizedLiteral ID="lblinformation" runat="server" Key="information" Colon="false" /></h5>
                    <ul>
                        <li><a id="deliveryinfo" runat="server" href="CMS_Content.aspx?id=21&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lbldeliveryinformation" runat="server" Key="deliveryinformation" Colon="false" />
                        </a>

                        </li>
                        <li><a id="privacy" runat="server" href="CMS_Content.aspx?id=10&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblprivacypolicy1" runat="server" Key="privacypolicy" Colon="false" />
                        </a>

                        </li>
                        <li><a id="terms" runat="server" href="CMS_Content.aspx?id=5&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblterms" runat="server" Key="terms" Colon="false" />
                        </a>

                        </li>
                        <li><a id="refundPolicy" runat="server" href="CMS_Content.aspx?id=27&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblrefundPolicy" runat="server" Key="refundPolicy" Colon="false" />
                        </a>

                        </li>

                    </ul>


                    <!-- email-->
                    <h5 class="list-main-title">
                        <Localized:LocalizedLiteral ID="lblemailsignup" runat="server" Key="emailsignup" Colon="false" /></h5>
                    <ul>
                        <li><a id="latestnews" runat="server" href="CMS_Content.aspx?id=22&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lbllatestnews" runat="server" Key="latestnews" Colon="false" />
                            &amp; 
                             <Localized:LocalizedLiteral ID="lblpromotions" runat="server" Key="promotions" Colon="false" />
                        </a>

                        </li>
                        <li><a id="unsubscribe" runat="server" href="CMS_Content.aspx?id=23&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblunsubscribe" runat="server" Key="unsubscribe" Colon="false" />
                        </a>

                        </li>
                        <li><a id="partner" runat="server" href="PartnerRegister.aspx">
                            <Localized:LocalizedLiteral ID="lblPartner" runat="server" Key="RegisterPartner" Colon="false"></Localized:LocalizedLiteral>
                        </a>

                        </li>

                    </ul>
                    <h5 class="list-main-title"><a href="ContactUs.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aContactUs1" runat="server">
                        <Localized:LocalizedLiteral ID="lblcontact1" runat="server" Key="contact" Colon="false" />
                    </a>
                    </h5>
                    <h5 class="list-main-title"><a id="aboutus" runat="server" href="CMS_Content.aspx?id=2&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                        <Localized:LocalizedLiteral ID="lblaboutus1" runat="server" Key="aboutus" Colon="false" />
                    </a>

                    </h5>
                    <h5 class="list-main-title"><a href="Blog.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="last" id="aBlog1" runat="server">
                        <Localized:LocalizedLiteral ID="lblblog1" runat="server" Key="blog" Colon="false" /></a>

                    </h5>
                    <ul>

                        <%--<Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="blog" Colon="false" /></li>--%>
                        <asp:Repeater ID="rptRecords1" runat="server">
                            <ItemTemplate>

                                <li><a href="<%# Config.WebSiteMain %>Blog-Detail.aspx?BolgID=<%#Convert.ToString(Eval("BolgID"))%>&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>"><%#Convert.ToString(Eval("Title"))%></a><br />
                                </li>

                                <%--<a href="<%# Config.WebSiteMain %>Blog-Detail.aspx?BolgID=<%#Convert.ToString(Eval("BolgID"))%>&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" >--%>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>



                    <%-- <li  class="title"><a href="../Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aIndex1" runat="server">
                            <Localized:LocalizedLiteral ID="lblhome1" runat="server" Key="home" Colon="false" />
                        </a></li>
                        <li  class="title"><a href="CMS_Content.aspx?id=24&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="awhatwedo1" runat="server">
                            <Localized:LocalizedLiteral ID="lblwhatwedo1" runat="server" Key="whatwedo" Colon="false" />
                        </a></li>

                        <li  class="title"><a href="Special-Offer.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aspecialoffer1" runat="server">
                            <Localized:LocalizedLiteral ID="lblspecialoffer1" runat="server" Key="specialoffer" Colon="false" />
                        </a></li>
                        <li id="liLib1" runat="server"  class="title">
                            <a href="MyLibrary.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" style="margin-right: 7px;" id="aMyLibrary1" runat="server">
                                <Localized:LocalizedLiteral ID="lblmylibrary1" runat="server" Key="mylibrary" Colon="false" />
                            </a></li>
                        <li class="title">
                            <Localized:LocalizedLiteral ID="lblinformation" runat="server" Key="information" Colon="false" />
                        </li>                    --%>
                    <%--  <ul>
                            <li>
                                <a id="deliveryinfo" runat="server" href="CMS_Content.aspx?id=21&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                    <Localized:LocalizedLiteral ID="lbldeliveryinformation" runat="server" Key="deliveryinformation" Colon="false" />
                                </a>
                            </li>
                            <li><a id="privacy" runat="server" href="CMS_Content.aspx?id=10&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblprivacypolicy1" runat="server" Key="privacypolicy" Colon="false" />
                            </a></li>
                            <li><a id="terms" runat="server" href="CMS_Content.aspx?id=5&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblterms" runat="server" Key="terms" Colon="false" />
                            </a></li>
                            <li>
                                <a id="refundPolicy" runat="server" href="CMS_Content.aspx?id=27&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                    <Localized:LocalizedLiteral ID="lblrefundPolicy" runat="server" Key="refundPolicy" Colon="false" />
                                </a>
                            </li>
                        </ul>--%>
                    <%-- <li class="title">
                            <Localized:LocalizedLiteral ID="lblemailsignup" runat="server" Key="emailsignup" Colon="false" />
                        </li>
                        <ul>
                            <li><a id="latestnews" runat="server" href="CMS_Content.aspx?id=22&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lbllatestnews" runat="server" Key="latestnews" Colon="false" />
                                &amp; 
                             <Localized:LocalizedLiteral ID="lblpromotions" runat="server" Key="promotions" Colon="false" />
                            </a></li>
                            <li><a id="unsubscribe" runat="server" href="CMS_Content.aspx?id=23&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                                <Localized:LocalizedLiteral ID="lblunsubscribe" runat="server" Key="unsubscribe" Colon="false" />
                            </a></li>
                            <li>
                                <a id="partner" runat="server" href="PartnerRegister.aspx">
                                    <Localized:LocalizedLiteral ID="lblPartner" runat="server" Key="RegisterPartner" Colon="false"></Localized:LocalizedLiteral>
                                </a>
                            </li>
                        </ul>
                        <li><a href="ContactUs.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="aContactUs1" runat="server">
                            <Localized:LocalizedLiteral ID="lblcontact1" runat="server" Key="contact" Colon="false" />
                        </a></li>
                        <li><a id="aboutus" runat="server" href="CMS_Content.aspx?id=2&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                            <Localized:LocalizedLiteral ID="lblaboutus1" runat="server" Key="aboutus" Colon="false" />
                        </a></li>--%>

                    <%--  <li class="title"> <a href="Blog.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="last" id="aBlog1" runat="server">
                            <Localized:LocalizedLiteral ID="lblblog1" runat="server" Key="blog" Colon="false" /></a></li>
                        <%--<li> <Localized:LocalizedLiteral ID="lblblog1" runat="server" Key="blog" Colon="false" /></li>--%>
                    <%--<asp:Repeater ID="rptRecords1" runat="server">
                            <ItemTemplate>
                                <ul>
                                    <li><a href="<%# Config.WebSiteMain %>Blog-Detail.aspx?BolgID=<%#Convert.ToString(Eval("BolgID"))%>&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>"><%#Convert.ToString(Eval("Title"))%></a><br />
                                    </li>
                                </ul>
                                <%--<a href="<%# Config.WebSiteMain %>Blog-Detail.aspx?BolgID=<%#Convert.ToString(Eval("BolgID"))%>&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" >--%>
                    <%--  </ItemTemplate>
                        </asp:Repeater>--%>
                </div>
            </div>



            <div class="site-right">
                <%--<div style="font-family:'abeezeeregular';">
                    <li class="title"><a href="Category.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="acategory1" runat="server">
                        <Localized:LocalizedLiteral ID="lblcategories1" runat="server" Key="categories" Colon="false" />
                    </a></li>
                     <ul id="menu_ul" runat="server">
                     </ul>
                    
                </div>--%>
                <h5 class="list-main-title"><a href="Category.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" id="acategory1" runat="server">
                    <Localized:LocalizedLiteral ID="lblcategories1" runat="server" Key="categories" Colon="false" />
                </a>
                </h5>
                <ul id="menu_ul" runat="server">
                </ul>
            </div>
        </div>
    </div>
</asp:Content>

