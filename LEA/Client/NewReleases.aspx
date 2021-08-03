<%@ Page Title="New Releases" Language="C#" MasterPageFile="~/Client/Client.master" AutoEventWireup="true"
    CodeFile="NewReleases.aspx.cs" Inherits="Client_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <div class="row1">
        <h1>
            Featured Book</h1>
        <a href="BookDetail.aspx?BookID=<%=FeaturedID %>">
            <img src="<%=Featured %>" width="356px" height="407px" alt="" style="float: left;" />
        </a><a href="BookDetail.aspx?BookID=<%=BestSellerID %>">
            <img src="<%=BestSeller %>" alt="" align="right" width="356px" height="407px" style="float: right;" />
        </a>
    </div>--%>
    <div class="row2">
        <h1>
            New Releases</h1>
        <ul class="mag_list">
            <asp:DataList runat="server" ID="dtNewRealeases" RepeatColumns="3" RepeatDirection="Vertical"
                RepeatLayout="Table">
                <ItemTemplate>
                    <li style="width: 200px; overflow: hidden;"><a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
                        <asp:Image ID="Image1" runat="server" Width="192" Height="240" src='<%#Eval("TitleImage") %>'
                            CssClass="mag-img" />
                    </a><a href="#" class="mag_name" style="width: 185px">
                        <%#Eval("Title") %></a>
                        <p style="height: 30px;overflow-y: hidden;"> 
                            <%#Eval("Description") %></p>
                        <a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>">
                            <input type="button" value="" class="detail" />
                        </a></li>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label CssClass="cat" Style="margin: 0px 0px 0px 20%; text-align: center;" Visible='<%#bool.Parse((dtNewRealeases.Items.Count==0).ToString())%>'
                        runat="server" ID="lblNoRecord" Text="No Books Found!"></asp:Label>
                </FooterTemplate>
            </asp:DataList>
        </ul>
    </div>
  
</asp:Content>
