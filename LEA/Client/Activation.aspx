<%@ Page Title="" Language="C#" MasterPageFile="~/Client/Client.master" AutoEventWireup="true"
    CodeFile="Activation.aspx.cs" Inherits="Activation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="font-size: 31px;color: #4ba808;">
        Thank you,
        <asp:Label runat="server" ID="lblName"></asp:Label>
        your account successfully activated.
    </div>
</asp:Content>
