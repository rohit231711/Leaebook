<%@ Page Title="User Wishlist" Language="C#" MasterPageFile="~/Client/Books.master"
    AutoEventWireup="true" CodeFile="WishList.aspx.cs" Inherits="Client_WishList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(function () {

            $(".fancybox").fancybox({ 'titleShow': false,
                'type': 'iframe',
                'href': 'Login.aspx',
                'width': '450px',
                'height': '300px',
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'hideOnOverlayClick': false


            });
            $(".high_lights_row").hide();

            $("#tblart tr").hover(function () {
                $("#tblcart tr").css("background-color", "white");
                $(this).css("background-color", "#E2E2E2");

            });

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="account" style="height: auto; float: left; margin: 1px auto 40px 16px">
        <div class="left_part" style="width: 70%; height: 100%">
            <h5 style="border-bottom-width: 0px;">
                <a href="Shop.aspx" style="color: green; text-decoration: none; padding-left: 14px;">
                    Continue</a></h5>
            <br clear="all" />
            <div style="border-bottom: 1px solid black; padding: 10px; width: 610px; margin-left: 14px;">
                <h3>
                    Your WishList</h3>
                <br clear="all" />
            </div>
            <br clear="all" />
            <div id="divshow" runat="server" class="left" style="width: 436px" visible="false">
                <div class="right1">
                    <div class="right-top-hdr">
                        <h3 style="margin: 0px; padding: 0px 19px 0px; width: 312px; background-color: #3B9A02;
                            color: White">
                            Alert</h3>
                        <p>
                            Please login to view wishlist
                        </p>
                    </div>
                    <div class="button1 fancybox">
                        <a href="#">LOGIN</a></div>
                    <div class="button1">
                        <a href="SignUp.aspx">REGISTRATION</a></div>
                </div>
            </div>
            <asp:DataList ID="dtBookWishList" RepeatColumns="1" OnItemCommand="dtBookWishList_OnItemCommand"
                RepeatDirection="Vertical" RepeatLayout="Table" runat="server" OnItemDataBound="dtBookWishList_ItemDataBound">
                <ItemTemplate>
                    <table id="tblcart" width="610px" style="padding: 10px">
                        <tr>
                            <td valign="top">
                                <a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
                                    <asp:Image Width="200px" Height="250px" Style="border: thin outset;" runat="server"
                                        ImageUrl='<%#Eval("TitleImage")%>' />
                                </a>
                            </td>
                            <td valign="top" style="width: 321px">
                                <b>
                                    <%#Eval("Title") %></b>
                            </td>
                            <td valign="top" style="float: right; width: 81px;">
                                RM
                                <asp:Label ID="lblprice" runat="server" Text='<%#Math.Round(Convert.ToDecimal(Eval("Price")))%>'></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton runat="server" OnClientClick="return confirm('Are you sure to delete item from WishList? ');"
                                    ImageUrl="~/Admin/images/table/delete.png" CommandArgument='<%#Eval("ID") %>'
                                    CommandName="Del" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
            <div class="divClear">
            </div>
            <br />
            <%--<table>
                <tr>
                    <td>
                        <asp:Button ID="btnfirst" OnClick="btnfirst_Click" runat="server" Font-Bold="true"
                            Text="<<" Height="31px" Width="43px" />
                    </td>
                    <td>
                        <asp:Button ID="btnprevious" OnClick="btnprevious_Click" runat="server" Font-Bold="true"
                            Text="<" Height="31px" Width="43px" />
                    </td>
                    <td>
                        <asp:Button ID="btnnext" OnClick="btnnext_Click" runat="server" Font-Bold="true"
                            Text=">" Height="31px" Width="43px" />
                    </td>
                    <td>
                        <asp:Button ID="btnlast" OnClick="btnlast_Click" runat="server" Font-Bold="true"
                            Text=">>" Height="31px" Width="43px" />
                    </td>
                </tr>
            </table>--%>
        </div>
        <div class="right_part" style="padding: 0; padding: 49px 0px 0px; width: 277px;">
            <h2>
                Item Summary</h2>
            <table class="subTable" border="0" cellpadding="3" cellspacing="3">
                <tbody>
                    <tr>
                        <td>
                            Subscriptions + Issues
                        </td>
                        <td class="right">
                            <asp:Label ID="lbltotalIssues" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            total:
                        </td>
                        <td class="right ">
                            RM.<asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
