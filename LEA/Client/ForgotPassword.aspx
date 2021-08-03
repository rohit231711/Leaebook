<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/Client/Client.master"
    AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Client_ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row2">
        <h1>
            Forgot Password?</h1>
        <div>
            <div runat="server" id="divcms">
            </div>
            <div class="contact">
                <div class="contact-form">
                    <h1 style="border: none;">
                        Enter Your Details</h1>
                    <div>
                        <asp:Label ID="lblMessage" Style="text-decoration: blink; font-size: 16px; color: Green"
                            Text="" runat="server"></asp:Label>
                    </div>
                    <br clear="all" />
                    <div class="text-boxes">
                        <label class="lab">
                            Email Address *</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="validate[required,custom[email]] tax-data"></asp:TextBox>
                    </div>
                    <div class="divClear">
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="sub" OnClick="btnSubmit_Click" />
                </div>
            </div>
            <img src="images/bottom-line.png" alt="" style="padding-top: 30px;" />
        </div>
    </div>
</asp:Content>
