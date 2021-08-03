<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/Client/Client.master" AutoEventWireup="true"
    CodeFile="SignUp.aspx.cs" Inherits="Client_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {


            $("#<%=txtEmail.ClientID %>").blur(function () {

                var email = $(this).val();
                if (email != '') {
                    $.ajax({
                        type: "POST",
                        url: "SignUp.aspx/getUserExists",
                        data: "{email:'" + email + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            if (eval(msg.d)) {
                                $("#<%=txtEmail.ClientID %>").validationEngine("showPrompt", "Email is already taken.", "error", true);

                                $("#<%=txtEmail.ClientID %>").focus();

                            }


                        }
                    });

                }

            });

            $("#<%=btnSubmit.ClientID %>").click(function () {

                $("#<%=txtFname.ClientID%>").addClass("validate[required,custom[onlyLetterSp]]");
                $("#<%=txtLname.ClientID%>").addClass("validate[required,custom[onlyLetterSp]]");
                $("#<%=txtEmail.ClientID%>").addClass("validate[required,custom[email]]");
                $("#<%=txtPassword.ClientID%>").addClass("validate[required]]");
                $("#<%=txtretypepassword.ClientID%>").addClass("validate[required, equals[ctl00_ContentPlaceHolder1_txtPassword]]");


                var email = $("#<%=txtEmail.ClientID %>").val();

                if (email != '') {
                    $.ajax({
                        type: "POST",
                        url: "SignUp.aspx/getUserExists",
                        data: "{email:'" + email + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            if (eval(msg.d)) {
                                $("#<%=txtEmail.ClientID %>").validationEngine("showPrompt", "Email is already taken.", "error", true);
                                return false;
                            }
                            else {

                                return true;
                            }

                        }
                    });

                }



            });

        });
    

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row2">
        <h1>
            Sign Up</h1>
        <div class="login2">
            <div class="login-form">
                <h1 style="border: none;">
                    Create New Account</h1>
                <div>
                    <asp:Label ID="lblMessage" Style="text-decoration: blink; font-size: 16px; color: Green"
                        Text="" runat="server"></asp:Label>
                </div>
                <br clear="all" />
                <label class="lab2">
                    First Name :<span style="color: Red">*</span>
                </label>
                <div class="divClear">
                </div>
                <div class="user-text1">
                    <asp:TextBox ID="txtFname" runat="server" CssClass="validate[required,custom[onlyLetterSp]] user1"></asp:TextBox>
                </div>
                <label class="lab2">
                    Last Name :<span style="color: Red">*</span>
                </label>
                <div class="divClear">
                </div>
                <div class="user-text1">
                    <asp:TextBox ID="txtLname" runat="server" CssClass="validate[required,custom[onlyLetterSp]] user1"></asp:TextBox>
                </div>
                <label class="lab2">
                    Email :<span style="color: Red">*</span>
                </label>
                <div class="divClear">
                </div>
                <div class="user-text1">
                    <asp:TextBox ID="txtEmail" CssClass="validate[required,custom[email]] user1" runat="server"></asp:TextBox>
                </div>
                <label class="lab2">
                    Password :<span style="color: Red">*</span></label>
                <div class="divClear">
                </div>
                <div class="user-pass1">
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="validate[required] user1"></asp:TextBox>
                </div>
                <label class="lab2">
                    Confirm Password :<span style="color: Red">*</span></label>
                <div class="divClear">
                </div>
                <div class="user-pass1">
                    <asp:TextBox ID="txtretypepassword" TextMode="Password" runat="server" CssClass="validate[required,equals[ctl00_ContentPlaceHolder1_txtPassword]] user1" />
                </div>
                <div class="cheak-box">
                    <input id="chk1" runat="server" name="" type="checkbox" value="" class="chek validate[required]" /><label
                        class="lab2">
                        I agree to the Terms and Conditions</label>
                </div>
                <asp:Button ID="btnSubmit" runat="server" CssClass="login-bt2 sub" OnClick="btnSubmit_Click" />
            </div>
            <div class="divClear">
            </div>
        </div>
    </div>
</asp:Content>
