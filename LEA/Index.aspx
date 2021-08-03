<%@ Page Title="" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<%@ Register Src="~/Includes/Left_part.ascx" TagName="LeftMenu" TagPrefix="menu" %>
<%@ Register Src="~/Includes/banner.ascx" TagName="Benner" TagPrefix="br" %>
<%@ Register Src="~/Includes/Advertise.ascx" TagName="Advertise" TagPrefix="ad" %>
<%@ Register Src="Includes/Editorschoice.ascx" TagName="Editors" TagPrefix="ed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script>
        $(document).ready(function () {
            $('#liNew_Releases').addClass('active');
            hideAlert();
        });
        var GetFor = 'New Releases';
        function loadMore(For) {
            debugger;
            if (For == 'New Releases' || For == 'ebook' || For == 'Paper book') {
                GetFor = For;
            }
            $.ajax({
                type: "POST",
                url: "Index.aspx/BindDatas",
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
    <!-- Facebook Pixel Code -->
    <script>
        !function (f, b, e, v, n, t, s) {
            if (f.fbq) return; n = f.fbq = function () {
                n.callMethod ?
                    n.callMethod.apply(n, arguments) : n.queue.push(arguments)
            };
            if (!f._fbq) f._fbq = n; n.push = n; n.loaded = !0; n.version = '2.0';
            n.queue = []; t = b.createElement(e); t.async = !0;
            t.src = v; s = b.getElementsByTagName(e)[0];
            s.parentNode.insertBefore(t, s)
        }(window, document, 'script',
            'https://connect.facebook.net/en_US/fbevents.js');
        fbq('init', '468506476975848');
        fbq('track', 'PageView');
    </script>
    <noscript>
        <img height="1" width="1" style="display: none"
            src="https://www.facebook.com/tr?id=468506476975848&ev=PageView&noscript=1" />
    </noscript>
    <!-- End Facebook Pixel Code -->


</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--************************banner**********************-->

    <!--************************middlecontent**********************-->
    <div class="wrap">
        <div class="middalecontent">
            <br:Benner ID="benner" runat="server" />
            <%--<marquee>--%>
            <asp:Label ID="lblCountry" runat="server" Style="font-family: 'amaranthregular'; font-size: 18px; font-weight: normal; width: 100%; float: left;"></asp:Label>
            <%--</marquee>--%>
            <menu:LeftMenu ID="leftmenu" runat="server" />

            <%--<asp:HyperLink ID="hlBook" runat="server" NavigateUrl="~/book-detail/1/OLAS-DEL-HOMBRE" CssClass="boxbut load_more">Try to Click</asp:HyperLink>
            <a href="book-detail/1/OLAS-DEL-HOMBRE" class="boxbut load_more">Try to Click Again</a>--%>
            <div class="rightbarmid" style="padding-bottom: 15px;">
                <div class="mln-panel">
                    <ul class="tabmain">
                        <li id="liNew_Releases"><a href="javascript:void(0)" onclick="loadMore('New Releases')"><%= Localization.ResourceManager.GetString("NewReleases", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                        <li id="liebook"><a href="javascript:void(0)" onclick="loadMore('ebook')"><%= Localization.ResourceManager.GetString("eBook", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                        <li id="liPaper_book"><a href="javascript:void(0)" onclick="loadMore('Paper book')"><%= Localization.ResourceManager.GetString("paperBook", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a></li>
                    </ul>
                    <div class="cat-list-top-bar">
                        

                        <a href='AllBooks.?l=<%= System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToString() %>' class='titnewre1'><%= Localization.ResourceManager.GetString("viewallbook", System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToString() %></a>
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlSortby" runat="server" onchange="loadMore('SortBy');" class="dropbtn">
                                <%--<asp:ListItem Text="Sort by" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="Free" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Paid" Value="0"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%--<a href="javascript:void(0);" onclick="myFunction()" class="dropbtn">Sort by<img src="images/drow.png" style="margin-left: 5px"></a>
                        <div id="myDropdown" class="dropdown-content">
                            <a href="javascript:void(0)">Free</a>
                            <a href="javascript:void(0)">Paid</a>
                        </div>--%>

                    <script>
                        function myFunction() {
                            document.getElementById("myDropdown").classList.toggle("show");
                        }
                        window.onclick = function (event) {
                            if (!event.target.matches('.dropbtn')) {

                                var dropdowns = document.getElementsByClassName("dropdown-content");
                                var i;
                                for (i = 0; i < dropdowns.length; i++) {
                                    var openDropdown = dropdowns[i];
                                    if (openDropdown.classList.contains('show')) {
                                        openDropdown.classList.remove('show');
                                    }
                                }
                            }
                        }
                    </script>

                </div>
            </div>

            <div class="rightbarmid" runat="server" id="div_book" style="padding-bottom: 15px;">
                <div class="">
                    testing
                </div>
            </div>
            <%--New Releases Books--%>
            <div style="display: none;">
                <Localized:LocalizedLiteral ID="lblnew" runat="server" Key="NewReleases" Colon="false" Text="New Releases" />
            </div>
            <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; width: 100%; float: left; font-size: 15px; font-family: 'abeezeeregular'; line-height: 25px;"></asp:Label>
            <%--<div class="holder" style="text-align:right; width:100%"></div>--%>
        </div>
    </div>
    <div class="wrap">
        <%--<div class="bookbox" style="border: none;">&nbsp;</div>--%>
        <div class="load_more_bg">
            <a id="aLoadData" class="boxbut load_more" style="cursor: pointer;" onclick="loadMore('LoadMore')">
                <Localized:LocalizedLiteral ID="lblMore" runat="server" Key="LoadMore" Colon="false" Text="Load More" />...
            </a>
        </div>
    </div>
    <%--<div style="text-align: right; width: 100%" id="PageNo" runat="server"></div>--%>

    <asp:HiddenField ID="newrelease" runat="server" Value="New Releases" />
    <asp:HiddenField ID="viewallbook" runat="server" Value="View All eBook" />
    <ad:Advertise ID="addvertise" runat="server" />
    <!--************************Editorschoice**********************-->
    <ed:Editors ID="Editors1" runat="server" />
</asp:Content>

