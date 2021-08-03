<%@ Page Title="All Books" Language="C#" MasterPageFile="~/Client/Books.master" AutoEventWireup="true"
    CodeFile="AllBooks.aspx.cs" Inherits="Client_AllBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="high_li_part1300" style="margin-top: 43px;">
        <div class="cat">
            <div class="explore-head">
                <a href="AllBooks.aspx?t=<%=Type %>" style="font-weight: bold; color: Black; text-decoration: none;">
                    Books</a>&nbsp;<img src="<%=ResolveUrl("images/geen-arrow.png") %>" />
                <a id="atocat" runat="server" style="font-weight: bold; color: Black; text-decoration: none;">
                    <%=Catname %></a>
                <img src="<%=ResolveUrl("images/geen-arrow.png") %>" />
                <a id="atosub" runat="server" style="font-weight: bold; color: Black; text-decoration: none;">
                    <%=SubCatName %></a>
                <div class="divClear">
                </div>
                <div class="exp-list">
                    <asp:DataList RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Vertical" ID="dtcat"
                        runat="server">
                        <ItemTemplate>
                            <ul>
                                <li><a href="AllBooks.aspx?t=<%=Type %>&catid=<%#Eval("categoryID") %>">
                                    <%#Eval("CategoryName") %></a> </li>
                            </ul>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Vertical" ID="dtsubcat"
                        runat="server">
                        <ItemTemplate>
                            <ul>
                                <li><a href="AllBooks.aspx?t=<%=Type %>&subcatid=<%#Eval("CategoryID") %>">
                                    <%#Eval("CategoryName") %></a> </li>
                            </ul>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div class="red-style" style="text-transform: uppercase;">
            <%=Type%></div>
    </div>
    <div class="contant" style="margin: 0px; position: relative; top: 25px;">
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
