<%@ Page Title="Books" Language="C#" MasterPageFile="~/Client/Books.master"
    AutoEventWireup="true" CodeFile="Books.aspx.cs" Inherits="Client_Books" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <script src="js/dropkick.js" type="text/javascript"></script>
    <link href="css/dropkick.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('.baby_bear').dropkick();
            $('.mama_bear').dropkick();
            $('.papa_bear').dropkick();
        });
    </script>
    <style type="text/css">
        /* Two custom themes */
        
        
        .dk_theme_black
        {
            background: #ffffff;
            border: 1px solid #cccccc;
            padding: 0px;
            height: 27px;
            width: 152px;
            margin: 0 15px 0 0;
        }
        .dk_theme_black .dk_toggle, .dk_theme_black.dk_open .dk_toggle
        {
            background-color: transparent;
            background: url(images/downgreen.png) top right no-repeat;
            color: #333333;
            text-shadow: none;
            height: 18px;
            font-size: 13px;
            padding: 5px;
            width: 142px !important;
        }
        .dk_theme_black .dk_options a
        {
            background-color: #333;
            color: #fff;
            text-shadow: none;
        }
        .dk_theme_black .dk_options a:hover, .dk_theme_black .dk_option_current a
        {
            background-color: #009933;
            color: #fff;
            text-shadow: #604A42 0 1px 0;
        }
    </style>
    <script type="text/javascript" charset="utf-8">
        $(function () {

            $('.custom_theme').dropkick({
                theme: 'black',
                change: function (value, label) {
                    //  debugger;
                    $('#<%=ddlLanguage.ClientID%>').change();

                }
            });

            $('.dk_container').first().focus();
        });
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="high_li_part1300" style="margin-top: 43px;">
        <div class="cat">
            <div class="explore-head">
                <a href="<%=ResolveUrl("Shop.aspx") %>" style="font-weight: bold; color: Black; text-decoration: none;">
                    Shop</a>&nbsp;<img src="<%=ResolveUrl("images/geen-arrow.png") %>" />
                <a id="atocat" runat="server" style="font-weight: bold; color: Black; text-decoration: none;">
                    <%=Catname %></a>
                <img src="<%=ResolveUrl("images/geen-arrow.png") %>" />
                <a id="atosub" runat="server" style="font-weight: bold; color: Black; text-decoration: none;">
                    <%=SubCatName %></a>
                <div class="divClear">
                </div>
                <div class="exp-list">
                    <asp:DataList RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Vertical" ID="rptCategory"
                        runat="server">
                        <ItemTemplate>
                            <ul>
                                <li><a href="Books.aspx?subcatid=<%#Eval("CategoryID") %>">
                                    <%#Eval("CategoryName") %></a> </li>
                            </ul>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div class="red-style" style="text-transform: uppercase;">
            <%=Catname %></div>
    </div>
    <div class="contant" style="margin: 0px; position: relative; top: 25px;">
        <div style="padding-left: 16px; width: 0px;">
            <%--  <asp:DropDownList ID="ddlPopular" runat="server">
            </asp:DropDownList>--%>
            Language:
            <asp:DropDownList CssClass="custom_theme" runat="server" ID="ddlLanguage" AutoPostBack="true"
                OnSelectedIndexChanged="ddlLanguage_OnSelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="slider-data">
            <div class="slider1">
                <ul>
                    <asp:DataList ID="dtlistBooks" runat="server" RepeatColumns="4" RepeatDirection="Vertical"
                        RepeatLayout="Table">
                        <ItemTemplate>
                            <li style="height: 100%;"><a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
                                <asp:Image ID="Image1" runat="server" Width="192" Height="240" src='<%#Eval("TitleImage") %>'
                                    alt="" CssClass="meg-img" /></a>
                                <p class="meg-name">
                                    <%#Eval("Title") %></p>
                                <p class="price">
                                    Issue
                                    <%#Eval("Issues") %>
                                    / RM <%#Eval("IssuesPrice") %></p>
                                <br />
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label CssClass="cat" Style="margin: 0px 0px 0px 30%; text-align: center;" Visible='<%#bool.Parse((dtlistBooks.Items.Count==0).ToString())%>'
                                runat="server" ID="lblNoRecord" Text="No Books Found!"></asp:Label>
                        </FooterTemplate>
                    </asp:DataList>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
