<%@ Page Title="My Library" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="MyAccountLibrary.aspx.cs" Inherits="MyAccountLibrary" %>
<%@ Register Src="~/Includes/account_leftmenu.ascx" TagName="leftmenu" TagPrefix="lm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

</asp:Content>

