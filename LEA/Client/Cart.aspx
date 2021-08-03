<%@ Page Title="User Cart" Language="C#" MasterPageFile="~/Client/Books.master"
    AutoEventWireup="true" CodeFile="Cart.aspx.cs" Inherits="Client_Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(function () {

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
                    Continue Shoping</a></h5>
            <br clear="all" />
            <div style="border-bottom: 1px solid black; padding: 10px; width: 610px; margin-left: 14px;">
                <h3>
                    Your Cart</h3>
                <br clear="all" />
            </div>
            <br clear="all" />
            <asp:DataList ID="dtMagagineCart" RepeatColumns="1" OnItemCommand="dtBookCart_OnItemCommand"
                RepeatDirection="Vertical" RepeatLayout="Table" runat="server" OnItemDataBound="dtMagagineCart_ItemDataBound">
                <ItemTemplate>
                    <table id="tblcart" width="610px" style="padding: 10px; font-size: 17px">
                        <tr>
                            <asp:HiddenField ID="hdnOrderID" Value='<%#Eval("TitleImage")%>' runat="server" />
                            <td valign="top">
                                <asp:Image Width="200px" Height="250px" Style="border: thin outset;" runat="server"
                                    ImageUrl='<%#Eval("TitleImage")%>' />
                            </td>
                            <td valign="top" style="width: 321px">
                                <b>
                                    <%#Eval("Title") %></b>
                                <br clear="all" />
                                <%#Eval("Issues") %>
                            </td>
                            <td valign="top" style="float: right;width: 150px;text-align: right;margin-right: 10px;">
                                <span>RM
                                    <asp:Label ID="lblprice" runat="server" Text='<%# Convert.ToDouble(Eval("Price"))%>'></asp:Label>
                                </span>
                            </td>
                            <td style="vertical-align: top;">
                                <span>
                                    <asp:ImageButton runat="server" OnClientClick="return confirm('Are you sure to delete item from cart? ');"
                                        ImageUrl="~/Admin/images/table/delete.png" CommandArgument='<%#Eval("orderid") %>'
                                        CommandName="Del" />
                                </span>
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
            <table class="subTable" border="0" cellpadding="3" cellspacing="3" style="font-size: 15px;">
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
                        <td colspan="2">
                            Total:&nbsp;&nbsp;
                       <%-- </td>
                        <td class="right ">--%>
                            RM &nbsp;<asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
            <table class="submitOrder" border="0" cellspacing="3" cellpadding="3">
                <tbody>
                    <tr>
                        <asp:ImageButton ID="btnCheckout" CausesValidation="true" OnClick="btnCheckout_Click" ImageUrl="~/Client/images/shoppingcart_checkout (2).png"
                            runat="server" />
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
