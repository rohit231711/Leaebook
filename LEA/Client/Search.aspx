<%@ Page Title="Search" Language="C#" MasterPageFile="~/Client/Client.master" AutoEventWireup="true"
    CodeFile="Search.aspx.cs" Inherits="Client_Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(function () {

            $(".high_lights_row").hide();
        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Your Search Result:(<%=Title1 %>)</h3>
    <br clear="all" />
    <asp:DataList ID="dtlistBooks" runat="server" RepeatColumns="3" RepeatDirection="Vertical"
        RepeatLayout="Table">
        <ItemTemplate>
            <li style="height: 100%; list-style: none"><a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
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
</asp:Content>
