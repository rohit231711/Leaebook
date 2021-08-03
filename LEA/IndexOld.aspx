<%@ Page Title="Home" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="IndexOld.aspx.cs" Inherits="IndexOld" %>

<%@ Register Src="~/Includes/Left_part.ascx" TagName="LeftMenu" TagPrefix="menu" %>
<%@ Register Src="~/Includes/banner.ascx" TagName="Benner" TagPrefix="br" %>
<%@ Register Src="~/Includes/Advertise.ascx" TagName="Advertise" TagPrefix="ad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="js/jquery-1.8.2.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--************************banner**********************-->
    <br:Benner ID="benner" runat="server" />
    <!--************************middlecontent**********************-->
    <div class="wrap">
        <div class="middalecontent">
            <menu:LeftMenu ID="leftmenu" runat="server" />
            <div class="rightbarmid" runat="server" id="div_book" style="padding-bottom: 15px;">
            </div>
            <%--New Releases Books--%>
            <div style="display: none;">
                <Localized:LocalizedLiteral ID="lblnew" runat="server" Key="NewReleases" Colon="false" Text="New Releases" />
            </div>

            <div class="holder" style="text-align:right; width:100%"></div>
        </div>
    </div>
    <div style="text-align:right; width:100%" ID="PageNo" runat="server">
           
            </div>

    <asp:HiddenField ID="newrelease" runat="server" Value="New Releases" />
    <asp:HiddenField ID="viewallbook" runat="server" Value="View All eBook" />
    <ad:Advertise ID="addvertise" runat="server" />

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
