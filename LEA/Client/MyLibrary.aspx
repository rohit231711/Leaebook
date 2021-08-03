<%@ Page Title="My Library" Language="C#" MasterPageFile="~/Client/Books.master"
    AutoEventWireup="true" CodeFile="MyLibrary.aspx.cs" Inherits="Client_MyLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/dropkick.js" type="text/javascript"></script>
    <link href="css/dropkick.css" rel="stylesheet" type="text/css" />
    <!--[if IE 7]>
<style>

.account
{
margin-top:0;
}
</style>

<![endif]-->
    <script type="text/javascript">


        $(".fancybox").fancybox({ 'titleShow': false,
            'type': 'iframe',
            'href': 'Login.aspx',
            'width': '450px',
            'height': '300px',
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'hideOnOverlayClick': false


        });


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
            width: 179px;
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
            width: 174px !important;
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
                    $('#<%=ddlAllTitle.ClientID%>').change();

                }
            });

            $('.dk_container').first().focus();
        });
    </script>
    <style>
        .styled select
        {
            background: transparent;
            width: 150px;
            padding: 5px;
            font-size: 16px;
            border: 1px solid #ccc;
            height: 34px;
            -webkit-appearance: none;
        }
        
        .styled
        {
            width: 120px;
            height: 34px;
            overflow: hidden;
            background: url(http://www.stackoverflow.com/favicon.ico) no-repeat 96% #ddd;
        }
    </style>
    <script type="text/javascript">

        $(function () {



            $("#tblart tr").hover(function () {
                $("#tblcart tr").css("background-color", "white");
                $(this).css("background-color", "#E2E2E2");

            });

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="account">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div id="divshow" runat="server" class="left">
                                    <div class="right1">
                                        <div class="right-top-hdr">
                                            <h3 style="margin: 0px; padding: 0px 19px 0px; width: 312px; background-color: #3B9A02;
                                                color: White">
                                                Alert</h3>
                                            <p>
                                                Please login to view library
                                            </p>
                                        </div>
                                        <div class="button1 fancybox">
                                            <a href="#">LOGIN</a></div>
                                        <div class="button1">
                                            <a href="SignUp.aspx">REGISTRATION</a></div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="contant" id="div1" runat="server" style="margin: 0px; top: 25px;">
                                    <div style="margin-left: 49px;">
                                        <table>
                                            <tr>
                                                <td style="color: Green; font-size: 20px; text-transform: uppercase; width: 276px;">
                                                    Your Reading List
                                                </td>
                                                <td style="width: 0px">
                                                    <asp:DropDownList CssClass="custom_theme" Visible="false" ID="ddlSortByDate" runat="server">
                                                        <asp:ListItem Text="View By Date" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList CssClass="custom_theme" ID="ddlAllTitle" AutoPostBack="true" runat="server"
                                                        OnSelectedIndexChanged="ddlAllTitle_SelectedIndexChanged" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="View By Title" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <br clear="all" />
                                    <div class="slider-data">
                                        <div class="slider1">
                                            <ul>
                                                <asp:DataList ID="dtlistBooks" Style="border-collapse: collapse; margin-left: 10px;"
                                                    runat="server" RepeatColumns="4">
                                                    <ItemTemplate>
                                                        <li style="height: 100%;"><a href="video.aspx">
                                                            <asp:Image ID="Image1" runat="server" Width="192" Height="240" src='<%#Eval("Image") %>'
                                                                alt="" CssClass="meg-img" /></a>
                                                            <p class="meg-name">
                                                                <%#Eval("Title") %></p>
                                                            <p class="price">
                                                                <%# DataBinder.Eval(Container.DataItem, "Date", "{0:Y}") %>
                                                            </p>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
