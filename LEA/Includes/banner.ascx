<%@ Control Language="C#" AutoEventWireup="true" CodeFile="banner.ascx.cs" Inherits="Includes_banner" %>
<%--<link rel="stylesheet" type="text/css" href="LEAslider/css/demo.css" />--%>
<!-- <link rel="stylesheet" type="text/css" href="LEAslider/css/style.css" /> -->
<link rel="stylesheet" href="../css/flexslider.css" type="text/css" media="screen" />
<script type="text/javascript" src="LEAslider/js/modernizr.custom.53451.js"></script>


<div class="banner">

    <section class="slider dg-container">
        <div class="flexslider" style="border: 0;">
            <ul class="slides">
                <asp:Repeater ID="rptRecords1" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src="<%# "../Banner/" + Eval("ImagePath") %>" alt="" class="img<%# Eval("Number") %>" style="height: auto; max-height: 310px;" /></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </section>



    <%--<section id="dg-container" class="dg-container">
                <div class="welcome-images">
        	<img src="images/banenr.png" alt="" />
            <div class="edit-shadow">
        		<div class="shadow"><img src="images/bnr-shadow.png" alt="" /></div>
            </div>
        </div>
				<div class="dg-wrapper" >				
					  
				</div>
			</section>--%>
</div>

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
<!--<script type="text/javascript" src="LEAslider/js/jquery.gallery.js"></script>
<script type="text/javascript">
    $(function () {
        $('#dg-container').gallery({
            autoplay: true
        });
    });
</script> -->
<style type="text/css">
    .dg-wrapper a {
        background: none !important;
        box-shadow: none !important;
    }
</style>
<script src="../js/jquery.flexslider.js"></script>
<script>
    $(document).ready()
    $(window).load(function () {
        $('.flexslider').flexslider({
            animation: "slide"
        });
    });

</script>

