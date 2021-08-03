<%@ Page Title="Account" Language="C#" MasterPageFile="~/Client/Books.master"
    AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Client_Account" EnableTheming="true"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .brdbtm
        {
            border-bottom: 1px solid #D6D6D6;
            text-transform: capitalize;
            font-size: 13px;
        }
        
        .lnkhover
        {
            background: none repeat scroll 0 0 #DDDDDD;
            color: #555333;
            font-size: 12px;
            text-decoration: none;
        }
    </style>
    <script type="text/javascript">

        function validation() {
            var firstname = $("#<%=txtFirstName.ClientID%>").val();
            var lastname = $("#<%=txtLastName.ClientID%>").val();

          
            if (firstname == '') {
                alert('Please insert firstname.');
                return false;
            }
            else if (lastname == '') {
                alert('Please insert lastname.');
                return false;
            }
            else {
                return true;

            }
        }




        $(document).ready(function () {

            $(".Alpha").keypress(function (event) {

                // Allow: backspace, delete, tab and escape
                if (event.charCode == 127 || event.keyCode == 127 || event.charCode == 8 || event.keyCode == 8 || event.charCode == 9 || event.charCode == 27 || event.keyCode == 27 || event.charCode == 32 || event.charCode == 32 || event.keyCode == 9 || (event.charCode == 0 && event.keyCode == 46) || (event.charCode == 0 && event.keyCode == 37) || (event.charCode == 0 && event.keyCode == 38) || (event.charCode == 0 && event.keyCode == 39) || (event.charCode == 0 && event.keyCode == 40) || (event.charCode == 0 && event.keyCode == 36) || (event.charCode == 0 && event.keyCode == 35) ||
                // Allow: Ctrl+A
            ((event.charCode == 65) && event.ctrlKey === true)
                // Allow: home, end, left, right
            ) {
                    // let it happen, don't do anything
                    return;
                }
                else {
                    // Ensure that it is a number and stop the keypress
                    if ((event.charCode < 65 || event.charCode > 90) && (event.charCode < 97 || event.charCode > 122)) {
                        event.preventDefault();
                    }
                }
            });


//            $('#<%=txtFirstName.ClientID%>').keyup(function () {
//                var $th = $(this);
//                $th.val($th.val().replace(/[^a-zA-Z0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
//            });

//            $('#<%=txtLastName.ClientID%>').keyup(function () {
//                var $th = $(this);
//                $th.val($th.val().replace(/[^a-zA-Z0-9]/g, function (str) { alert('You typed " ' + str + ' ".\n\nPlease use only letters and numbers.'); return ''; }));
//            });


            $("#<%=txtCurrentPass.ClientID %>").blur(function () {

                var password = $(this).val();
                if (password != '') {
                    $.ajax({
                        type: "POST",
                        url: "Account.aspx/checkPassword",
                        data: "{password:'" + password + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            if (eval(msg.d)) {
                                $("#<%=txtCurrentPass.ClientID %>").validationEngine("showPrompt", "Password not match with old password.", "error", true);

                                $("#<%=txtCurrentPass.ClientID %>").focus();

                            }


                        }
                    });

                }

            });

            $("#<%=lkbSave.ClientID %>").click(function () {

                $("#<%=txtCurrentPass.ClientID%>").addClass("validate[required]]");
                $("#<%=txtNewPass.ClientID%>").addClass("validate[required]]");
                $("#<%=txtConfirm.ClientID%>").addClass("validate[required, equals[ctl00_ContentPlaceHolder1_txtNewPass]]");

            });


        });



        $(function () {

            $(".high_lights_row").hide();

            $("#tblart tr").hover(function () {
                $("#tblcart tr").css("background-color", "white");
                $(this).css("background-color", "#E2E2E2");

            });

        });

        function toggle_visibility(example1, example2) {
            var e1 = document.getElementById(example1);
            var e2 = document.getElementById(example2);

            if (e1.style.display == 'block') {
                e1.style.display = 'none';


            }
            else {
                e1.style.display = 'block';

            }
            if (e2.style.display == 'block') {
                e2.style.display = 'none';


            }
            else {
                e2.style.display = 'block';

            }


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="account">
        <div class="left_part" style="width: 239px">
            <h4 style="border-bottom: 1px solid #D6D6D6;">
                Account setting</h4>
            <ul>
                <li id="aAccount" runat="server"><a href="#" runat="server" onserverclick="aAccountClick">
                    Account Information</a></li>
                <li id="aSub" runat="server"><a id="A1" href="#" runat="server" onserverclick="aSubClick">
                    Subscription</a></li>
                <li id="apurchase" runat="server"><a href="#" runat="server" onserverclick="aPaymentClick">
                    Purchase history</a></li>
            </ul>
        </div>
        <div class="right_part" style="float: left">
            <asp:Panel ID="pnlProfile" runat="server">
                <div class="profile">
                    <div class="details">
                        profile Details</div>
                    <div class="edit">
                        <a id="test" style="cursor: pointer;" onclick="toggle_visibility('example1','example2');">
                            Edit</a>
                    </div>
                    <div class="intro" id="example1" style="display: block;">
                        <div class="intro_left">
                            <p>
                                First &nbsp;Name :</p>
                            <p>
                                Last &nbsp;Name :</p>
                            <p>
                                Email &nbsp;Address :</p>
                        </div>
                        <div class="intro_right">
                            <p>
                                <asp:Label ID="lblFirstName" Style="text-transform: capitalize;" runat="server"></asp:Label></p>
                            <p>
                                <asp:Label ID="lblLastName" Style="text-transform: capitalize;" runat="server"></asp:Label></p>
                            <p>
                                <asp:Label ID="lblEmailAddress" runat="server"></asp:Label></p>
                        </div>
                    </div>
                    <div id="example2" style="display: none;" class="intro">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 120px; color: #666666; vertical-align: top;">
                                    First Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="inputbox Alpha"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="color: #666666; vertical-align: top;">
                                    Last Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="inputbox Alpha"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div class="savebatan" style="float: none;">
                                        <asp:LinkButton ID="lkbUpdate" OnClientClick="return validation();" runat="server" OnClick="lkbUpdate_Click"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%-- <div class="profile">
                        <div class="details">
                            Linked Account</div>
                    </div>
                    <div class="facebook">
                        <img src='<%=ResolveUrl("images/facebook_icon.png") %>' alt="" width="16" height="16"
                            class="iconface" />
                        <p>
                            Facebook <span>is not linked</span> with Zone. <a href="#">Link Facebook</a></p>
                    </div>--%>
                    <div class="profile">
                        <div class="details">
                            Sign-in Details</div>
                    </div>
                    <div class="signin" style="margin-bottom: 0px;">
                        <div class="signin_left">
                            <p>
                                Enter Current Password :
                            </p>
                        </div>
                        <div class="signin_right">
                            <asp:TextBox ID="txtCurrentPass" TextMode="Password" runat="server" CssClass="inputbox"></asp:TextBox>
                        </div>
                        <div class="signin_left">
                            <p>
                                Enter New Password :
                            </p>
                        </div>
                        <div class="signin_right">
                            <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" CssClass="inputbox"></asp:TextBox>
                        </div>
                        <div class="signin_left">
                            <p>
                                Confirm New Password :
                            </p>
                        </div>
                        <div class="signin_right">
                            <asp:TextBox ID="txtConfirm" TextMode="Password" runat="server" CssClass="inputbox"></asp:TextBox>
                        </div>
                    </div>
                    <div class="savebatan">
                        <asp:ImageButton ID="lkbSave" runat="server" ImageUrl="~/Client/images/save-changes.png"
                            CssClass="login-bt2 sub" OnClick="lkbSave_Click" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" Visible="false" ID="pnlPaymenthistory">
                <div class="profile" style="width: 672px">
                    <div class="details">
                        Payment Details</div>
                    <div class="intro" id="Div1" style="display: block; width: 100%">
                        <asp:GridView ID="gvPaymentHistory" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPaymentHistory_OnPageIndexChanging"
                            runat="server" SkinID="skinGrid" AutoGenerateColumns="false" EM Width="100%"
                            PagerStyle-ForeColor="White" PagerStyle-BackColor="#3C9F01">
                            <Columns>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbBook" CssClass="no-sort" runat="server" Text="OrderDate"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderDate" runat="server" Text='<%#Convert.ToDateTime(Eval("PurchaseDate")).ToString("d") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbOrderID" runat="server" CssClass="no-sort" Text="OrderID"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderID" runat="server" Text='<%# Eval("OrderNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbBookTitle" runat="server" Text="Title" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbIssues" runat="server" Text="Issues" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("issues") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbPrice" runat="server" Text="Price" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%#Convert.ToString(Eval("Price")+" RM.") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlSubscriptions" runat="server">
                <div class="profile" style="width: 672px">
                    <div class="details" style="width: 202px">
                        Subscription Details</div>
                    <div class="intro" id="Div2" style="display: block; width: 100%">
                        <asp:GridView ID="grdSubscriptions" AllowPaging="true" PageSize="4" OnPageIndexChanging="grdSubscriptions_OnPageIndexChanging"
                            runat="server" SkinID="skinGrid" AutoGenerateColumns="false" Width="100%" PagerStyle-ForeColor="White"
                            PagerStyle-BackColor="#3C9F01">
                            <Columns>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbBook" CssClass="no-sort" runat="server" Text="Book"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" Width="70" Height="100" src='<%#Eval("Image") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbTitle" runat="server" CssClass="no-sort" Text="Title"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbIssues" runat="server" Text="Issues" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssues" runat="server" Text='<%# Eval("Issues") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbDelivered" runat="server" Text="Delivered" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDelivered" runat="server" Text='<%# Eval("delivered") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbRemaining" runat="server" Text="Remaining" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemaining" runat="server" Text='<%#Convert.ToString(Convert.ToInt32(Eval("Issues"))-Convert.ToInt32(Eval("delivered"))) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbStatus" runat="server" Text="Status" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Convert.ToString(Convert.ToInt32(Eval("Issues"))-Convert.ToInt32(Eval("delivered")))=="0"?"Expired":"Active" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-ForeColor="Black">
                                    <HeaderStyle HorizontalAlign="Left" CssClass="brdbtm" />
                                    <HeaderTemplate>
                                        <asp:Label ID="lkbPrice" runat="server" Text="Price" CssClass="no-sort"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemStyle Font-Size="14px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%#Convert.ToString(Eval("Price")+" RM.") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
