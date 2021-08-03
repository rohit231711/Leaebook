<%@ Page Title="" Language="C#" MasterPageFile="~/Client/Books.master" AutoEventWireup="true"
    CodeFile="Checkout.aspx.cs" Inherits="Client_Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(function () {

            $(".high_lights_row").hide();

            $("#tblart tr").hover(function () {
                $("#tblcart tr").css("background-color", "white");
                $(this).css("background-color", "#E2E2E2");

            });

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="account" style="height: 998px">
        <div class="left_part" style="width: 70%; height: 100%">
           
        </div>
        <div class="right_part" style="padding: 0; padding: 49px 0px 0px; width: 277px;">
        </div>
    </div>
</asp:Content>
