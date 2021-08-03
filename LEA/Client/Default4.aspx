<%@ Page Title="Comming Soon" Language="C#" MasterPageFile="~/Client/Books.master" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Client_Default4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<img src="../images/coming_soon.png" alt="Comming Soon" />
</asp:Content>

