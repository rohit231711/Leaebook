<%@ Page Title="Category" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
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
            console.log(imgSrcs);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblcategories" runat="server" Key="categories" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <div class="wrap">
        <div class="loginbox1">
            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="ShopbyCategories" Colon="false" /></h1>
            </div>
            <div class="catimgs">
                <asp:Repeater ID="rptRecords1" runat="server">
                    <ItemTemplate>
                        <a href="<%=Config.WebSiteMain %>Biography.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>&catid=<%#(Eval("CategoryID"))%>" class="trvlimg">
                            <img src="Category/new_<%#(Eval("CImagePath"))%>" alt="" class="trvel" style="width: 234px; height: 99px;" />
                            <span class="trvltxt"><%#Convert.ToString(Eval("CategoryName"))%></span>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>


            <div class="ebook">
                <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="NeweBook" Colon="false" /></div>
            <%--<div class="ebookimg">
                    <asp:Repeater ID="Repeater1" runat="server" >
                         <ItemTemplate>
                            <div class="box1book2">
                                <div class="bookimg">
                    		<a href="#"><img src="Book/<%#(Eval("CategoryID"))%>/<%#(Eval("ImagePath"))%>" alt="" /></a>
                            </div>
                            <div class="namkl2"><%#Convert.ToString(Eval("Title"))%></div>
                            <div class="author"><%#Convert.ToString(Eval("Autoher"))%>/div>
                            <div class="namkl">$<%#Convert.ToString(Eval("Price"))%></div>
                            <a href="#" class="boxbut">Buy Now</a>
                            </div>
                         </ItemTemplate>
                    </asp:Repeater>
                </div>--%>

            <div runat="server" id="div_book">
            </div>
        </div>
    </div>
</asp:Content>

