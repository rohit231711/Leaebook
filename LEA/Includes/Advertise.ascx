<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Advertise.ascx.cs" Inherits="Includes_Advertise" %>
<div class="advertsiebg">
    <script>
        $(document).ready(function () {
            abc();
        });
        function abc() {
            var str = $('#<%= hndimglst.ClientID %>').val();
            var str_array = str.split(',');
            var count = 0;
            images = new Array;
            title = new Array;
            link = new Array;            
            //alert(str_array.length);
            count = str_array.length;
            for (var i = 0; i < str_array.length; i++) {
                if (str_array[i] != "") {
                    var str_array1 = str_array[i].split('@@');
                    if (str_array1.length > 1) {
                        images[i] = "../Advertisements/" + str_array1[0];
                        title[i] = str_array1[1];
                        link[i] = str_array1[2];
                    }                  
                }
            }            
            if (count > 0) {
                count = count - 1;
            }
            //setInterval(function () {
            //    changeImage()
            //}, 2000);
            x = 0;
            function changeImage() {
                if (x < count - 1) {
                    x += 1;
                } else if (x = count) {
                    x = 0;
                }
                if (typeof images[x] != 'undefined') {                    
                        document.getElementById('ctl00_ContentPlaceHolder1_addvertise_img_ad1').src = images[x];
                        $('#<%= lbl_title1.ClientID %>').text(title[x]);                        
                        $('#<%= linkadd1.ClientID %>').attr('href', link[x]);                                                                                        
                }
                if (x < count - 1) {
                    x += 1;
                } else if (x = count) {
                    x = 0;
                }
                if (typeof images[x] != 'undefined') {
                    document.getElementById('ctl00_ContentPlaceHolder1_addvertise_img_ad2').src = images[x];
                    $('#<%= lbl_Title2.ClientID %>').text(title[x]);
                    $('#<%= linkadd2.ClientID %>').attr('href', link[x]);
                }
            }
        }
    </script>
    <div class="adbg">
        <img src="images/advertisebg.png" alt="" />
    </div>
    <div class="wrap">
        <a href="#" runat="server" id="linkadd1" target="_blank">
            <div class="advertiseimg" id="div_ad1" runat="server" visible="false">
                <div class="transpbg">
                    <asp:Label ID="lbl_title1" runat="server" Text=""></asp:Label>
                </div>        
                    <img id="img_ad1" src="#" alt="" runat="server" height="142" width="498" />           
            </div>
        </a>
        <a href="#" runat="server" id="linkadd2" target="_blank">
            <div class="advertisero" id="div_ad2" runat="server" visible="false">
                <div class="transpbg1">
                    <asp:Label ID="lbl_Title2" runat="server" Text=""></asp:Label>
                </div>
                <img id="img_ad2" src="#" alt="" runat="server" height="142" width="498" />
            </div>
        </a>
    </div>
    <asp:HiddenField ID="hndimglst" runat="server" Value="," />
</div>
