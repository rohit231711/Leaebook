<%@ Control Language="C#" AutoEventWireup="true" CodeFile="account_leftmenu.ascx.cs"
    Inherits="Includes_account_leftmenu" %>
<script type="text/javascript">
    var myinterval = "";
    jQuery(document).ready(function () {
        // binds form submission and fields to the validation engine
        //jQuery("#aspnetForm").validationEngine();

        myinterval = setInterval(SetActiveClass, 500);
    });
    function SetActiveClass() {

        $('a').each(function () {
            if ($(this).data("class") != undefined && $(this).data("class") != '') {
                //      alert($(this).data("class"));
                $(this).attr('class', $(this).data("class"));
                $(this).data("class", "");
                clearInterval(myinterval);
            }
        });
    }
</script>
<div class="leftpanel">
    <h1>
        <Localized:LocalizedLiteral ID="lblmyaccount" runat="server" Key="myaccount" Colon="false" />
    </h1>
    <div class="leftmenu">
        <ul>
            <li><a id="aAccount" runat="server" href="../Account.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                <Localized:LocalizedLiteral ID="lblaccountinformation" runat="server" Key="accountinformation"
                    Colon="false" />
            </a></li>
            <li><a id="aMyLibrary" runat="server" href="../MyAccountLibrary.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                <Localized:LocalizedLiteral ID="lblmylibrary" runat="server" Key="mylibrary" Colon="false" />
            </a></li>
            <li><a id="aWishlist" runat="server" href="../WishList.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                <Localized:LocalizedLiteral ID="lblmywishlist" runat="server" Key="mywishlist" Colon="false" />
            </a></li>
            <li><a id="aOrderreport" runat="server" href="../OrderReport.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                <Localized:LocalizedLiteral ID="lblorderreport" runat="server" Key="orderreport" Colon="false" />
            </a></li>
            <li>
                <a id="aorderHistory" runat="server">
                    <Localized:LocalizedLiteral ID="lblorderHistory" runat="server" Key="orderHistory" Colon="false"></Localized:LocalizedLiteral>
                </a>
            </li>
            <li>
                <a id="aorderAddress" runat="server">
                    <Localized:LocalizedLiteral ID="lbldelivery_address" runat="server" Key="delivery_address" Colon="false"></Localized:LocalizedLiteral>
                </a>
            </li>
        </ul>
    </div>
</div>
