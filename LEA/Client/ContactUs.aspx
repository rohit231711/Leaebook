<%@ Page Title="" Language="C#" MasterPageFile="~/Client/Client.master" AutoEventWireup="true"
    CodeFile="ContactUs.aspx.cs" Inherits="Client_ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(function () {

            $(".sub").click(function () {

                $("#<%=txtFullName.ClientID %>").addClass("validate[custom[onlyLetterSp]]");
                $("#<%=txtEmail.ClientID %>").addClass("validate[required,custom[email]]");
                $("#<%=txtTelephone.ClientID %>").addClass("validate[custom[phone]]");
                $("#<%=txtMessage.ClientID %>").addClass("validate[required]");
                $(".area-box").addClass("validate[required]");
            });
        });

    </script>
    <asp:Literal ID="ltlMetaKeyword" runat="server"></asp:Literal>
    <asp:Literal ID="ltlMetaDescription" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row2">
        <h1>
            Contact Us</h1>
        <div>
            <div runat="server" id="divcms">
            </div>
            <div class="contact">
                <div class="contact-form">
                    <h1 style="border: none;">
                        Enter Your Details</h1>
                    <div class="text-boxes">
                        <label class="lab">
                            Full Name
                        </label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="tax-data"></asp:TextBox>
                        <label class="lab">
                            Email Address *</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="tax-data"></asp:TextBox>
                        <label class="lab">
                            Telephone Number
                        </label>
                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="tax-data"></asp:TextBox>
                    </div>
                    <div class="text-a">
                        <label class="lab">
                            Your Address *
                        </label>
                        <div class="divClear">
                        </div>
                        <div class="txt-area">
                            <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" CssClass="area-box"></asp:TextBox>
                        </div>
                    </div>
                    <div class="divClear">
                    </div>
                    <div class="text-a">
                        <label class="lab">
                            Your Message *
                        </label>
                        <div class="divClear">
                        </div>
                        <asp:TextBox ID="txtMessage" Height="86" Width="645" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="sub" Style="margin-top: 131px"
                        OnClick="btnSubmit_Click" />
                </div>
            </div>
            <img src="images/bottom-line.png" alt="" style="padding-top: 30px;" />
        </div>
    </div>
</asp:Content>
