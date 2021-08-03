<%@ Page Title="My Library" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="MyAccountLibrary.aspx.cs" Inherits="MyAccountLibrary" %>
<%@ Register Src="~/Includes/account_leftmenu.ascx" TagName="leftmenu" TagPrefix="lm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        var GetFor = 'New Releases';
        function loadMore(For) {
            debugger
            if (For == 'New Releases' || For == 'ebook' || For == 'Paper book') {
                GetFor = For;
            }
            $.ajax({
                type: "POST",
                url: "MyLibrary.aspx/BindData",
                data: "{'LoadMore':'" + For + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
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
                document.getElementById('aLoadData').style.display = 'none';
            }
            else {
                document.getElementById('aLoadData').style.display = 'block';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="breadcrumb">
       		<div class="wrap">
                <ul>
                    <li><a href="#"><Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                    <li><span><a href="#"><Localized:LocalizedLiteral ID="lblLibrary" runat="server" Key="mylibrary" Colon="false" /></a></span></li>
                 </ul>
            </div>
       </div>
       <div class="wrap">
        	<div class="loginbox">
            <lm:leftmenu ID="leftmenu1" runat="server" />
            <%--<div class="leftpanel">
                	<h1><Localized:LocalizedLiteral ID="lblMyAcc" runat="server" Key="myaccount" Colon="false" /></h1>
                    <div class="leftmenu">
                    	<ul>
                        	<li><a href="#"><Localized:LocalizedLiteral ID="lblAcc" runat="server" Key="accountinformation" Colon="false" /></a></li>
                            <li><a class="active"  href="#"><Localized:LocalizedLiteral ID="lblLibrary1" runat="server" Key="mylibrary" Colon="false" /></a></li>
                            <li><a href="#"><Localized:LocalizedLiteral ID="lbllist" runat="server" Key="mywishlist" Colon="false" /></a></li>
                           
                        </ul>
                    </div>
                  
                    
                </div>--%>

                <div class="rightbarmid nvd-new-library" id="div_book" runat="server">
                <!--My Account Library Content-->
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
</asp:Content>

