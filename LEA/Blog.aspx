<%@ Page Title="Blog" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="Blog.aspx.cs" Inherits="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script>
        $(document).ready(function () {
            img_find();
            console.clear();
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
                imgSrcs.push(imgs[i].src);
            }
            console.clear();
            console.log( imgSrcs);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
                </a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblBlog" runat="server" Key="blog" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox">
            <div class="logintxt">
                <h1>
                    <a href="#" style="color:Black;">
                        <Localized:LocalizedLiteral ID="lblblog1" runat="server" Key="blog" Colon="false" /></a></h1>
            </div>
            <asp:Repeater ID="rptRecords1" runat="server">
                <ItemTemplate>
                    <div class="blogpnl">
                        <%--<div class="blogimg"><img src="Blog/<%#(Eval("BlogImage"))%>" alt="" style = "height:237px; width:430px;" /></div>--%>
                        <div class="blogimg">
                            <img src="<%# PicturePath("Blog/"+Eval("BlogImage")+"") %>" alt="" style="height: 237px;
                                width: 430px;" /></div>
                        <div class="blogtxt">
                            <div style="clear: both;">
                                <h1>
                                    <%#Convert.ToString(Eval("Title"))%></h1>
                                <span class="bldt">
                                    <%#Convert.ToString(Eval("CreatedDate1"))%></span>
                                <p>
                                    <%#Convert.ToString(Eval("Description"))%>
                                </p>
                            </div>
                            <div style="clear: both;">
                                <a href="<%# Config.WebSiteMain %>Blog-Detail.aspx?BolgID=<%#Convert.ToString(Eval("BolgID"))%>&l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" class="readbtn">
                                    <img src="images/blarw.png" alt="" class="rdarw" /><Localized:LocalizedLiteral ID="lblRead"
                                        runat="server" Key="readmore" Colon="false" /></a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
