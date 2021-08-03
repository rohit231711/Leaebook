<%@ Page Title="Special Offer" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="Special-Offer.aspx.cs" Inherits="Special_Offer" %>

<%@ Register Src="~/Includes/Left_part.ascx" TagName="leftpart" TagPrefix="lp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        }
    </script>
    <script>
        $(document).ready(function () {
            hideAlert();
        });

        function loadMore() {
            $.ajax({
                type: "POST",
                url: "<%=Config.WebSiteMain%>Special-Offer.aspx/BindDatas",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });

            function OnSuccess(response) {
                //console.log(response.d);
                document.getElementById('<%=div_book.ClientID%>').innerHTML += response.d;
                var x = document.getElementById('divNoData');
                if (x != null) {
                    document.getElementById('aLoadData').style.display = 'none';
                }
            }
        }

        function hideAlert() {
            var x = document.getElementById('divNoData');
            if (x != null) {
                //alert("No More Data");
                document.getElementById('aLoadData').style.display = 'none';
            }
        }

    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <asp:Label ID="lblcat" runat="server"></asp:Label></a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox">
            <lp:leftpart ID="leftpart" runat="server" />
            <div class="rightbarmid">
                <div class="rightbarmid" runat="server" id="div_book" style="padding-bottom: 15px;">
                </div>
            </div>
        </div>
    </div>
    <div class="wrap">
        <div class="load_more_bg">
            <a id="aLoadData" class="boxbut load_more" style="cursor: pointer; margin-top: -40px; margin-bottom: 20px;" onclick="loadMore()">
                <Localized:LocalizedLiteral ID="lblMore" runat="server" Key="LoadMore" Colon="false" Text="Load More" />...
            </a>
        </div>
    </div>
    <asp:HiddenField ID="viewallbook" runat="server" Value="View All eBook" />

</asp:Content>

