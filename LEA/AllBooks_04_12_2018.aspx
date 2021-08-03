<%@ Page Title="All eBooks" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="AllBooks.aspx.cs" Inherits="AllBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .rightbarmid{ width:95%;float:left}        
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
                data: "{'Sortby':'" + $('#ContentPlaceHolder1_ddlSortby').val() + "','For':'" + GetFor + "','LoadMore':'" + For + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger;                    
                   <%-- document.getElementById('<%=div_book.ClientID%>').innerHTML = response.d;      --%>              
                     if (For == 'LoadMore') {
                        document.getElementById('<%=div_book.ClientID%>').innerHTML += response.d;
                    }
                    else {
                        document.getElementById('<%=div_book.ClientID%>').innerHTML = response.d;
                    }

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
                    var x = document.getElementById('divNoData');
                    if (x != null) {
                        document.getElementById('aLoadData').style.display = 'none';
                    }
                    else {
                        document.getElementById('aLoadData').style.display = 'block';
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


        //function hideAlert() {
        //    var x = document.getElementById('divNoData');
        //    if (x != null) {
        //        alert("No More Data");
        //        document.getElementById('aLoadData').style.display = 'none';
        //    }
        //    else {
        //        document.getElementById('aLoadData').style.display = 'block';
        //    }
        //}

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

                  
                    <div class="cat-list-top-bar">
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlSortby" runat="server" onchange="loadMore('SortBy');" class="dropbtn">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        <div class="loginbox1 loginbox-2">            
            <%--<div class="ebook"><Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="alleBooks" Colon="false" /></div>--%>

            <div runat="server" id="div_book" ></div>
        </div>
    </div>
    <div class="wrap">
               <div class="load_more_bg load-more-btn-box">
            <a id="aLoadData" class="boxbut load_more" style="cursor: pointer;" onclick="loadMore('LoadMore')">
                <Localized:LocalizedLiteral ID="lblMore" runat="server" Key="LoadMore" Colon="false" Text="Load More" />...
            </a>
        </div>
    </div>
</asp:Content>



