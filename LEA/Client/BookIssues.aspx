<%@ Page Title="Book Issues" Language="C#" MasterPageFile="~/Client/Books.master" AutoEventWireup="true"
    CodeFile="BookIssues.aspx.cs" Inherits="Client_BookIssues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="contant" style="margin: 0px; position: relative; top: 25px;">
       
        <div class="slider-data">
            <div class="slider1">
                <ul>
                    <asp:DataList ID="dtlistBooks" runat="server" RepeatColumns="4" RepeatDirection="Vertical"
                        RepeatLayout="Table">
                        <ItemTemplate>
                                <li style="height: 100%;"><a href="BookDetail.aspx?BookID=<%#Eval("BookID") %>&is=1">
                                <asp:Image ID="Image1" runat="server" Width="192" Height="240" src='<%#Eval("TitleImage") %>'
                                    alt="" CssClass="meg-img" /></a>
                                <p class="meg-name">
                                    <%#Eval("Title") %></p>
                                <p class="price">
                                    RM <%#Eval("singleIssuePrice") %></p>
                                <br />
                            </li>
                        </ItemTemplate>
                    </asp:DataList>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
