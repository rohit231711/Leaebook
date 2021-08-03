<%@ Page Title="" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="Biography.aspx.cs" Inherits="Biography" %>

<%@ Register Src="~/Includes/Left_part.ascx" TagName="leftpart" TagPrefix="lp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(document).ready(function () {
            $('#liNew_Releases').addClass('active');
            hideAlert();
        });
        var GetFor = 'New Releases';
        function loadMore(For) {
            debugger            
            if (For == 'New Releases' || For == 'ebook' || For == 'Paper book') {
                GetFor = For;
            }
            $.ajax({
                type: "POST",
                url: "Biography.aspx/BindDatas",
                data: "{'Sortby':'" + $('#ContentPlaceHolder1_ddlSortby').val() + "','For':'" + GetFor + "','LoadMore':'" + For + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    debugger;
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

        function hideAlert() {
            debugger;
            var x = document.getElementById('divNoData');
            if (x != null) {
                alert("No More Data");
                document.getElementById('aLoadData').style.display = 'none';
            }
            else {
                document.getElementById('aLoadData').style.display = 'block';
            }
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
                    <asp:Label ID="lblcat" runat="server"></asp:Label>
                <asp:Label ID="lbldesc" runat="server"></asp:Label></a></span></li>
                <%--<Localized:LocalizedLiteral ID="lblbiography" runat="server" Key="biography" Colon="false" />--%>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox">
            <lp:leftpart ID="leftpart" runat="server" />
            <div class="rightbarmid" style="padding-bottom: 15px;">
                <div class="mln-panel">
                    <ul class="tabmain">
                        <li id="liNew_Releases"><a href="javascript:void(0)" onclick="loadMore('New Releases')"><%= Localization.ResourceManager.GetString("NewReleases", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                        <li id="liebook"><a href="javascript:void(0)" onclick="loadMore('ebook')"><%= Localization.ResourceManager.GetString("eBook", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                        <li id="liPaper_book"><a href="javascript:void(0)" onclick="loadMore('Paper book')"><%= Localization.ResourceManager.GetString("paperBook", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                    </ul>
                    <div class="cat-list-top-bar">                        
                        <a href='Category.aspx?l=<%= System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToString() %>' class='titnewre1'><%= Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a>
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlSortby" runat="server" onchange="loadMore('SortBy');" class="dropbtn">                                
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="rightbarmid">
                <div class="rightbarmid" runat="server" id="div_book" style="padding-bottom: 15px;">
                </div>
            </div>
        </div>
    </div>
    <div class="wrap">
        <div class="load_more_bg">
            <a id="aLoadData" class="boxbut load_more" style="cursor: pointer; margin-top: -40px; margin-bottom: 20px;" onclick="loadMore('LoadMore')">
                <Localized:LocalizedLiteral ID="lblMore" runat="server" Key="LoadMore" Colon="false" Text="Load More" />...
            </a>
        </div>
    </div>
    <asp:HiddenField ID="viewallbook" runat="server" Value="View All eBook" />

</asp:Content>
