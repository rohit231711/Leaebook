<%@ Page Title="Explore" Language="C#" MasterPageFile="~/Client/Client.master" AutoEventWireup="true"
    CodeFile="Explore.aspx.cs" Inherits="Client_Explore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="<%=ResolveUrl("css/style.css") %>" rel="stylesheet" type="text/css" />
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
    <script src="js/dropkick.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript" charset="utf-8">
        $(function () {

            $('.custom_theme').dropkick({
                theme: 'black',
                change: function (value, label) {
                    //  debugger;
//                    $('#=ddlLanguage.ClientID').change();

                }
            });

            $('.dk_container').first().focus();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contant_left">
        <div class="example">
            <div>
                <a href="<%=ResolveUrl("Explore.aspx") %>" style="font-weight: bold; color: Black;
                    text-decoration: none;">Explore</a>&nbsp;<img src="<%=ResolveUrl("images/geen-arrow.png") %>" />
                <a id="atocat" runat="server" style="font-weight: bold; color: Black; text-decoration: none;">
                    <%=Catname %></a>
                <img src="<%=ResolveUrl("images/geen-arrow.png") %>" />
                <a id="atosub" runat="server" style="font-weight: bold; color: Black; text-decoration: none;">
                    <%=SubCatName %></a>
            </div>
            <%--<asp:DropDownList CssClass="custom_theme" runat="server" ID="ddlLanguage" AutoPostBack="true"
                OnSelectedIndexChanged="ddlLanguage_OnSelectedIndexChanged">
            </asp:DropDownList>--%>
            <%-- <a href="#" target="_blank">
                <img src="images/grid.png" alt="" class="view" /></a> <a href="#" target="_blank">
                    <img src="images/list.png" alt="" class="view" /></a>--%>
        </div>
        <div class="explore">
            <div class="row2">
                <div class="divClear">
                </div>
                <asp:DataList ID="dtlistBooks" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                    RepeatLayout="Table">
                    <ItemTemplate>
                        <div class="box-main" style="width: 377px">
                            <div class="box">
                                <a href="Reader.aspx?id=<%#Eval("BookID") %>" target="_blank">
                                    <asp:Image ID="Image2" runat="server" Width="303" Height="330" src='<%#Eval("TitleImage") %>'
                                        alt="" CssClass="meg-img" />
                                </a>
                                <h5>
                                    <%#Eval("Title") %></h5>
                                <p>
                                    <%#Eval("Description") %>.</p>
                                <p class="ml-green">
                                    <%#Eval("Publisher") %></p>
                                <p class="ml-green2">
                                    <%#Convert.ToDateTime(Eval("CreatedOn")).ToString("Y") %></p>
                            </div>
                        </div>
                        <img src="images/big-line.png" alt="" width="301px" class="big-line" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label CssClass="cat" Style="text-align: center;" Visible='<%#bool.Parse((dtlistBooks.Items.Count==0).ToString())%>'
                            runat="server" ID="lblNoRecord" Text="No Books Found!"></asp:Label>
                    </FooterTemplate>
                </asp:DataList>
                <div class="divClear">
                </div>
                <br />
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnfirst"  OnClick="btnfirst_Click" ImageUrl="~/Client/images/first.png"
                                runat="server" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnprevious" OnClick="btnprevious_Click" ImageUrl="~/Client/images/prev.png"
                                runat="server" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnnext" OnClick="btnnext_Click" ImageUrl="~/Client/images/next.png"
                                runat="server" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnlast"  OnClick="btnlast_Click" ImageUrl="~/Client/images/last.png"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
