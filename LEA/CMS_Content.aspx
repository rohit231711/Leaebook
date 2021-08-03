<%@ Page Title="" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="CMS_Content.aspx.cs" Inherits="CMS_Content" %>

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
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <asp:Label ID="title" runat="server"></asp:Label></a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox">
            <div class="logintxt">
                <h1>
                    <asp:Label ID="cmsname" runat="server"></asp:Label>
                </h1>
            </div>
            <div class="abt-lft" runat="server" id="div_content">
            </div>
        </div>
    </div>
</asp:Content>
