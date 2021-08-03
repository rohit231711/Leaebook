<%@ Page Title="My Library" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="MyLibrary.aspx.cs" Inherits="MyLibrary" %>

<%@ Register Src="~/Includes/Left_part.ascx" TagName="leftpart" TagPrefix="lp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="breadcrumb">
       		<div class="wrap">
                <ul>
                    <li><a href="#"><Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                    <li><span><a href="#"><Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="mylibrary" Colon="false" /></a></span></li>
                 </ul>
            </div>
       </div>
        
        <div class="wrap">
        	<div class="loginbox">
            	<lp:leftpart ID="leftpart" runat="server" />                
                <div class="rightbarmid">
                    <div class="rightbarmid" runat="server" id ="div_book">
                    <%-- MyLibrary Content --%>
                    </div>
                     </div><!--right end-->
                
            </div>
        </div>
</asp:Content>

