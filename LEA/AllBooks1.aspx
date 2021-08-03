<%@ Page Title="All eBooks" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="AllBooks.aspx.cs" Inherits="AllBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .rightbarmid {
            width: 95%;
            float: left
        }
    </style>

    <script>
        $(document).ready(function () {
            $('#liNew_Releases').addClass('active');

        });
        var GetFor = 'New Releases';
        function loadMore(For) {
            debugger;
            if (For == 'New Releases' || For == 'ebook' || For == 'Paper book') {
                GetFor = For;
            }
            $.ajax({
                type: "POST",
                url: "AllBooks.aspx/BindDatas",
                data: "{'Sortby':'" + $('#ContentPlaceHolder1_ddlSortby').val() + "','For':'" + GetFor + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger;
                    console.log(response.d.length);
                    document.getElementById('<%=div_book.ClientID%>').innerHTML = response.d;

                    if (For == 'New Releases' || For == 'ebook' || For == 'Paper book') {
                        GetFor = For;
                    }
                    if (GetFor != '') {
                        var GetForli = GetFor.replace(' ', '_');
                        $('#liNew_Releases').removeClass('active');
                        $('#liebook').removeClass('active');
                        $('#liPaper_book').removeClass('active');
                        $('#li' + GetForli).addClass('active');
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
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
                    <Localized:LocalizedLiteral ID="lblcategories" runat="server" Key="alleBooks" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="rightbarmid" style="padding-bottom: 15px;" id="divfilter" runat="server">
            <div class="mln-panel">

                <ul class="tabmain">
                    <li id="liNew_Releases"><a href="javascript:void(0)" onclick="loadMore('New Releases')"><%= Localization.ResourceManager.GetString("NewReleases", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                    <li id="liebook"><a href="javascript:void(0)" onclick="loadMore('ebook')"><%= Localization.ResourceManager.GetString("eBook", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                    <li id="liPaper_book"><a href="javascript:void(0)" onclick="loadMore('Paper book')"><%= Localization.ResourceManager.GetString("paperBook", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>

                </ul>
                <div class="product-search-bar">
                    <div class="serch-box">


                        <asp:DropDownList ID="drpglobalcat" runat="server" class="Globalsearchcat">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Story" Value="1"></asp:ListItem>
                            <asp:ListItem Text="English" Value="104"></asp:ListItem>
                        </asp:DropDownList>

                        <script type="text/javascript">
                            $("#txt_Globalsearch").keyup(function (event) {
                                if (event.keyCode == 13) {
                                    $("#Button1").click();
                                }
                            });
                        </script>
                        <div style="float: left;">
                            <div class="nav-search-field">
                                <asp:TextBox ID="txt_Globalsearch" runat="server" class="Globalsearchnickinp" Visible="false"></asp:TextBox>
                            </div>
                            <div id="nav-iss-attach"></div>
                        </div>
                        <div class="cat-gobtn">
                            <asp:Button ID="Button1" runat="server" Text="Go" class="nav-input" OnClick="btn_search" CssClass="Globalsearchbtn" />
                        </div>
                    </div>
                    <div class="cat-list-top-bar">
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlSortby" runat="server" onchange="loadMore('SortBy');" class="dropbtn">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="loginbox1">
            <%--<div class="ebook"><Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="alleBooks" Colon="false" /></div>--%>
            <label id="lblNoLateArrival" runat="server" style="display: none"></label>
            <div runat="server" id="div_book">
            </div>
          <%--  <div class="product-pagination">
                <ul>
                    <li><a href="#">Prev</a></li>
                    <li class="active"><a href="#">1</a></li>
                    <li><a href="#">2</a></li>
                    <li><a href="#">3</a></li>
                    <li><a href="#">4</a></li>
                    <li><a href="#">5</a></li>
                    <li><a href="#">...</a></li>
                    <li><a href="#">Next</a></li>
                </ul>
            </div> --%>
        </div>

        <div class="wrap">
        <div class="load_more_bg">
            <a id="aLoadData" class="boxbut load_more" style="cursor: pointer;" onclick="loadMore('LoadMore')">
                <localized:localizedliteral id="lblMore" runat="server" key="LoadMore" colon="false" text="Load More" />
                ...
            </a>
        </div>
    </div>

    </div>
</asp:Content>



