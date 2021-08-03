<%@ Page Title="" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="BiographyOld.aspx.cs" Inherits="BiographyOld" %>

<%@ Register Src="~/Includes/Left_part.ascx" TagName="leftpart" TagPrefix="lp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<title>        
        <%= cFunction.GetVal("BrowserTitle")%>
    </title>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    <%-- Biography Content --%>
                </div>    
                <div class="holder" style="text-align:right; width:100%"></div>            
            </div>
            <!--right end-->
        </div>
    </div>
    <asp:HiddenField ID="viewallbook" runat="server" Value="View All eBook"/>


    <link href="css/jPages.css" rel="stylesheet" />
    <script src="js/jPages.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("div.holder").jPages({
                containerID: "div_Books",
                perPage: 9,
                keyBrowse: true,
                scrollBrowse: true
            });
        });
    </script>
</asp:Content>
