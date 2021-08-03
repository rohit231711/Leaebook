<%@ Page Language="C#" Title="Payment" AutoEventWireup="true" CodeFile="payment.aspx.cs" MasterPageFile="~/Client/Books.master"
    Inherits="Client_payment" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link href="<%=ResolveUrl("css/style.css") %>" rel="stylesheet" type="text/css" />
    <title></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contant" style="margin: 0px; position: relative; top: 13px;">
        <div id="divshow" class="" style="width: 436px; background: none">
            <div class="right1" style="width: 553px;">
                <div class="right-top-hdr">
                    <h3 id="h3" runat="server" style="background-color: #3B9A02; width: 541px; padding: 6px;
                        color: White">
                        <asp:Label ID="lblAlert" runat="server" Text="Alert"></asp:Label>
                    </h3>
                    <p style="margin-left: 0px; padding: 6px">
                        <asp:Label ID="lblMessage" Font-Size="14px" runat="server" Text=" Thank you for purchasing Book,your order detail is mailed to you."></asp:Label>
                    </p>
                </div>
            </div>
        </div>
        <%--<div style="margin-top: 72px;">
        <center>
            <h2>
                <asp:Label ID="lbltext" Text="Thank you for purchasing Book,Your Order Confirmation is mailed to you."
                    runat="server"></asp:Label>
            </h2>
        </center>
    </div>--%>
    </div>
</asp:Content>
