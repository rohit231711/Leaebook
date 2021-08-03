<%@ Page Title="Blog Detail" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true"
    CodeFile="Blog-Detail.aspx.cs" Inherits="Blog_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" />
                </a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblBlog" runat="server" Key="blog" Colon="false" /></a></span>/</li>
                <li><asp:Label ID="lbltit" runat="server"></asp:Label></li>
            </ul>
        </div>
    </div>
    <div class="wrap">
        <div class="loginbox">           
            <asp:Repeater ID="rptRecords1" runat="server">
                <ItemTemplate>
                    <div class="blogpnl">
                        <div class="logintxt">
                            <h1>
                                <%#Convert.ToString(Eval("Title"))%></h1>
                        </div>
                        <div class="blogpnl">
                            <div class="blogimg1">                              
                                <img src="<%# PicturePath("Blog/"+Eval("BlogImage")+"") %>" alt="" style="height: 237px; width: 430px;" /></div>                                
                            <p>
                                <%#Convert.ToString(Eval("Description"))%></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!--midle end-->
    </div>
    <!--wrap end-->
</asp:Content>
