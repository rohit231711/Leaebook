<%@ Page Title="Track Order" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="TrackOrder.aspx.cs" Inherits="TrackOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function checkEmail() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            var email = document.getElementById('<%=txt_email.ClientID %>').value;
            var allowed = /^([a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$)/;
            if (!allowed.test(email)) {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor introduce válida Dirección de correo electrónico");
                }
                else {
                    alert("Please enter valid Email Address");
                }
                return false;
            }
        }

        function validation() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if (document.getElementById('<%=txt_email.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca la dirección de correo electrónico.");
                }
                else {
                    alert("Please enter Email Address.");
                }
                return false;
            }
            var validemail = checkEmail();
            if (validemail == false) {
                return false;
            }
            if (document.getElementById('<%=txt_orderid.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca Identificación del fin.");
                }
                else {
                    alert("Please enter order id.");
                }
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lbltrackorder" runat="server" Key="trackorder" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <div class="wrap">
        <div class="loginbox">
            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="lbltrackorder1" runat="server" Key="trackorder" Colon="false" />
                </h1>
            </div>

            <div class="loginbg">
                <asp:MultiView ID="mvTrack" runat="server">
                    <asp:View runat="server" ID="emailView">
                        <div class="lft" style="width: 100%;">
                            <div class="cust-lft" style="width: 92%;">
                                <div class="newcust" style="width: 98%;">
                                    <Localized:LocalizedLiteral ID="lblnewcustomers" runat="server" Key="trackorder" Colon="false" />
                                </div>
                                <div class="frm-grp" id="email" runat="server" visible="true">
                                    <label class="emailtxt">
                                        <Localized:LocalizedLiteral ID="lblemailaddress" runat="server" Key="emailaddress"
                                            Colon="false" />
                                        <span class="star">*</span></label>
                                    <asp:TextBox ID="txt_email" class="emailbox" runat="server" Text=""></asp:TextBox>
                                </div>
                                <div class="frm-grp">
                                    <label class="emailtxt">
                                        <Localized:LocalizedLiteral ID="lblorderid" runat="server" Key="orderid" Colon="false" />
                                        <span class="star">*</span></label>
                                    <asp:TextBox ID="txt_orderid" class="emailbox" runat="server" Text=""></asp:TextBox>
                                </div>
                                <div class="frm-grp">
                                    <label class="reqtxt">
                                        *
                            <Localized:LocalizedLiteral ID="lblrequiredfields" runat="server" Key="requiredfields" Colon="false" /></label>
                                </div>
                            </div>
                            <div class="btnbox-rt" style="margin: 0; width: 95%; float: left;">
                                <asp:Button ID="btn_track" runat="server" class="accbtn" OnClientClick="return validation();" Text="Track"
                                    OnClick="btn_track_Click"></asp:Button>
                            </div>
                        </div>
                    </asp:View>

                    <asp:View runat="server" ID="bookView">
                        <div class="lft" style="width: 98%;">
                            <div class="cust-lft" style="width: 92%; min-height: 75px;">
                                <div class="newcust" style="width: 98%;">
                                    <Localized:LocalizedLiteral ID="lblbookDetail" runat="server" Key="bookDetail" Colon="false" />
                                </div>
                            </div>
                            <div class="ebook-box1" style="border: 0; width: 95%; background-color: #fff;">
                                <asp:Repeater ID="rptRecords1" runat="server">
                                    <ItemTemplate>
                                        <div class="row-new">
                                            <div class="first1" style="width: 50%;">
                                                <asp:HiddenField ID="hfPurchaseID" runat="server" Value='<%# Eval("PurchaseID") %>' />
                                                <img src="<%# PicturePath("Book/"+Eval("CategoryID")+"/"+Eval("ImagePath").ToString().Replace(".jpg","_1.jpg")) %>" alt="" class="wishimg1" style="height: 135px; width: 98px;" />
                                                <div class="booktxt">
                                                    <span class="bname"><%#Convert.ToString(Eval("Title"))%></span>
                                                    <span class="aname"><%#Convert.ToString(Eval("Autoher"))%></span>
                                                    <span class="bname">
                                                        <asp:Label runat="server" ID="order">
                                                            <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="orderid" Colon="false" /></asp:Label>
                                                        : <%#Convert.ToString(Eval("OrderID"))%></span>
                                                    <span class="bname">
                                                        <asp:Label runat="server" ID="Label1">
                                                            <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="Date" Colon="false" /></asp:Label>
                                                        : <%#Convert.ToDateTime(Eval("PurchaseDate")).ToShortDateString()%></span>

                                                    <div class="movetxt">
                                                        <div class="ptxt"><%#Convert.ToBoolean(Eval("IsFree")) == true ? "Free" : "$" + Eval("Amount1")%></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="delbtn2" style="width: 40%;">
                                                <asp:Panel ID="innerRptDiv" runat="server">
                                        No Delivery Data Found
                                    </asp:Panel>
                                    <asp:Repeater ID="innerRpt" runat="server">
                                        <ItemTemplate>
                                            <span class="orderbname" style="width: 30%; text-align: left;">
                                                <%# Eval("OrderStatus") %>
                                            </span>
                                            <span class="orderbname" style="width: 70%; text-align: left;">
                                                <%# Eval("StatusDate") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                            </div>
                                            <div class="ptxt" style="width: 10%;">
                                                <%#Convert.ToBoolean(Eval("IsFree")) == true ? "Free" : "$" + Eval("Amount1")%>
                                            </div>
                                            <div class="row-new" style="border: 0; padding-bottom: 0;">
                                                <a class="boxbut" style="width: 100px; margin: 7px 10px;">Return</a>
                                                <a class="boxbut" href="<%# setHref(Eval("BookID").ToString())  %>">Review Product</a>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="btns">
                            <a href="TrackOrder.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" style="cursor: pointer;" class="back">
                                <img src="images/backarw.png" alt="" class="bkarw" />
                                <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="back" Colon="false" />
                            </a>
                        </div>
                    </asp:View>

                    <asp:View runat="server" ID="emailValidation">
                        <div class="lft" style="width: 98%;">
                            <asp:Label ID="lblError" runat="server" CssClass="reqtxt" Style="padding: 15px 15px 0 15px; font-size: 20px;"></asp:Label>
                        </div>
                        <div class="btns">
                            <a href="TrackOrder.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>" style="cursor: pointer;" class="back">
                                <img src="images/backarw.png" alt="" class="bkarw" />
                                <Localized:LocalizedLiteral ID="lblback" runat="server" Key="back" Colon="false" />
                            </a>
                        </div>
                    </asp:View>
                </asp:MultiView>

            </div>

        </div>
    </div>
</asp:Content>

