<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="http://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src='https://www.google.com/recaptcha/api.js' type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            img_find();
            console.clear();
        });
        $(document).mouseenter(function () {
            img_find();
            console.clear();
        });
        $(document).focus(function () {
            img_find();
            console.clear();
        });

        function img_find() {
            var imgs = document.getElementsByTagName("img");
            var imgSrcs = [];
            for (var i = 0; i < imgs.length; i++) {
                imgs[i].src = imgs[i].src.replace("/us", "");
                imgSrcs.push(imgs[i].src);
            }
            console.clear();
            console.log(imgSrcs);
        }
    </script>
    <script type="text/javascript">
        // Email Validation
        function checkEmail() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
        var email = document.getElementById('<%=txtEmail.ClientID %>').value;
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
    // Validation
    function name() {
        alert("shgsdfjsdf");
        //        var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
        //        if(a == '')
        //        {
        //            if (cultureLanguage == "es-ES") {
        //                alert("hi");
        //                document.getElementById('<%=txtName.ClientID %>').value == "Nombre";
        //            }
        //            else {
        //            //alert("Please enter Phone Number.");
        //                document.getElementById('<%=txtName.ClientID %>').value == "Name";
        //            }
        //        }
    }


        function onfocus1(value) {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
        alert(value);
        if (value == '<%= ResourceManager.GetString("Message",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>')
            document.getElementById('<%=txtmsgbox.ClientID %>').value == "";

    }

    function onblur1(value) {
        if (value == '')
            return value = '<%= ResourceManager.GetString("Message",System.Threading.Thread.CurrentThread.CurrentCulture.Name)%>';
    }

    function Validation() {
        var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';

    if (document.getElementById('<%=txtName.ClientID %>').value == "" || document.getElementById('<%=txtName.ClientID %>').value == "Name") {
        if (cultureLanguage == "es-ES") {
            alert("Por favor ingrese el nombre.");
        }
        else {
            alert("Please enter Name.");
        }
        return false;
    }
    if (document.getElementById('<%=txtEmail.ClientID %>').value == "" || document.getElementById('<%=txtEmail.ClientID %>').value == "Email") {
        if (cultureLanguage == "es-ES") {
            alert("Por favor introduzca Correo.");
        }
        else {
            alert("Please enter Email.");
        }
        return false;
    }
    var validemail = checkEmail();
    if (validemail == false) {
        return false;
    }
    if (document.getElementById('<%=txtPhone.ClientID %>').value == "" || document.getElementById('<%=txtPhone.ClientID %>').value == "Phone") {
            if (cultureLanguage == "es-ES") {
                alert("Por favor, ingrese el número telefónico.");
            }
            else {
                alert("Please enter Phone Number.");
            }
            return false;
        }
        if (document.getElementById('<%=txtmsgbox.ClientID %>').value == "" || document.getElementById('<%=txtmsgbox.ClientID %>').value == "Message") {
        if (cultureLanguage == "es-ES") {
            alert("Por favor introduce mensaje.");
        }
        else {
            alert("Please enter Message.");
        }
        return false;
    }
}


function NumberOnly(evt) {

    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;

}


function onlyAlphabets(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
            return true;
        else
            return false;
    }
    catch (err) {
        alert(err.Description);
    }
}
    </script>

    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no">
    <%--<script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false"></script>--%>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBob_TQY3wijXwHFaMo_2zgzJ7c5ud1dDw&callback=initMap"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadGmaps();
        });

        function LoadGmaps() {
            var myLatlng = new google.maps.LatLng(40.6704279, -73.9685033);
            var myOptions = {
                zoom: 16,
                center: myLatlng,
                disableDefaultUI: true,
                panControl: false,
                zoomControl: true,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.SMALL
                },

                mapTypeControl: true,
                mapTypeControlOptions: {
                    style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR
                },
                streetViewControl: true,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var map = new google.maps.Map(document.getElementById("MyGmaps"), myOptions);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: "12East Flatbush"
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb" onload="LoadGmaps()" onunload="GUnload()">
        <div class="wrap">
            <ul>
                <li><a href="#">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lblContact" runat="server" Key="contactus" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <div class="wrap">
        <div class="loginbox1">
            <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="LocalizedLiteral2" runat="server" Key="contactus" Colon="false" /></h1>
            </div>
            <div class="cntmap">
                <div id="MyGmaps" style="width: 100%; height: 316px; border: 1px solid #CECECE;"></div>
            </div>
            <%--<div class="cntmap"><img src="images/map.png" alt="" /></div>--%>
            <div class="cntlft">
                <div class="cnt-title">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Key="Contactform" Colon="false" /></div>
                <div class="cnt-grp">
                    <div class="cnt-nme">
                        <Localized:LocalizedTextBox type="text" Cssclass="cntname" PlaceholderKey="name" ID="txtName" runat="server" style="height: 42px;" onkeypress="return onlyAlphabets(event,this);"></Localized:LocalizedTextBox>
                    </div>
                    <div class="cnt-nme">
                        <Localized:LocalizedTextBox type="text" Cssclass="cntname" PlaceholderKey="email" ID="txtEmail" runat="server" style="height: 42px;"></Localized:LocalizedTextBox>
                    </div>
                    <div class="cnt-nme mgr">
                        <Localized:LocalizedTextBox type="text" Cssclass="cntname" PlaceholderKey="Phone" ID="txtPhone" runat="server" style="height: 42px;" MaxLength="16" onkeypress="return NumberOnly(event);"></Localized:LocalizedTextBox>
                    </div>
                </div>
                <div class="cnt-grp">
                    <%--<textarea class="msgbox" rows="" cols=""  onfocus="onfocus1();" onblur="if(this.value=='') this.value='Message';" >Message</textarea>--%>
                    <div class="adtxt" style="padding: 0px 0px; border-bottom: none">
                        <Localized:LocalizedLabel ID="lblWrite" runat="server" Key="writeHere"></Localized:LocalizedLabel>
                    </div>
                    <asp:TextBox ID="txtmsgbox" TextMode="multiline" class="msgbox" runat="server" Style="width: 100%; height: 160px;"></asp:TextBox>
                </div>                
                <div class="cnt-grp" style="padding-bottom:20px">
                    <div style="float:left;">
                        <div class="g-recaptcha" data-sitekey="6LcnLEEUAAAAAFV0xViQvgSNVbJVM9Z7T3PbTPwu"></div>
                    </div>
                    <div style="float:right">
                        <%--<a href="#" class="cntsubmit">submit</a>--%>
                        <asp:LinkButton ID="btn_login" runat="server" class="cntsubmit" OnClick="btn_submit" OnClientClick="return Validation();">
                            <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" />
                        </asp:LinkButton>
                    </div>
                </div>                                          
            </div>
            <!--lft end-->
            <%--<div class="cnt-rt">
                	<div class="cnt-info"><asp:Label ID = "lblCont_info" runat="server" /></div>
                    <div class="cnttxt1"><asp:Label ID = "lblCont_Desc" runat="server" /></div>
                    <span class="title">Lorem Ipsum is simply dummy </span>
                    <div class="adtxt">
                    	125, Ipsum is simply dummy industry. Lorem Ipsum,
						has been the industry's,
						standard dummy.012 456.
                    </div>
                    <div class="phonetxt">
                    	<div class="phtxt">Phone : <span class="num">+ 91 0123 456 789</span></div>
                        <div class="phtxt">Email : <span class="num">test@test.com</span></div>
                        <div class="phtxt">Fax : <span class="num">12345678</span></div>
                    </div>
                </div><!--rt end--> --%>
            <div class="cnt-rt">
                <div class="cnt-info">
                    <Localized:LocalizedLiteral ID="LocalizedLiteral3" runat="server" Key="contactInfo" Colon="false" /></div>
                <asp:Label ID="lblCont_info" runat="server" />

            </div>
        </div>
        <!--midle end-->
    </div>
    <!--wrap end-->
</asp:Content>

