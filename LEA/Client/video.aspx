<%@ Page Title="App Download" Language="C#" MasterPageFile="~/Client/Books.master"
    AutoEventWireup="true" CodeFile="video.aspx.cs" Inherits="Client_video" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .ul
        {
            padding: 0;
            margin: 0;
            list-style-type: none;
        }
        .videoul li
        {
            list-style-type: none;
            padding-bottom: 3px;
        }
        .videoul li a
        {
            text-decoration: none;
            color: #4BA808;
        }
        .videoul li a:hover
        {
            text-decoration: underline;
        }
        #MediaPlayer1
        {
            height: 350px;
            width: 650px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="clear: both;">
    </div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 10px;
        margin-left: 17px">
        <tr>
            <td colspan="2">
                <div style="float: left; width: 50%">
                    <a href="../Reader/BookReader.exe">
                        <img src="../Reader/reader.jpg" alt="" />
                    </a>
                </div>
                <div style="float: left; width: 20%; text-align: justify; padding-left: 25px;">
                    <p>
                        <b style="font-size: 16px;">New Home Library.</b></p>
                    <p style="font-size: 14px;">
                        <br />
                        Crowded shelves and piles of Books are history - the new home library is sleeker
                        and more brilliant than ever. Enjoy your favorite Books on your computer and
                        sync your library easily to your smartphone or tablet devices too.
                    </p>
                </div>
                <div style="float: left; width: 20%; text-align: justify; padding-left: 25px;">
                    <p>
                        <b style="font-size: 16px;">Sleekify your library now.</b></p>
                    <p style="font-size: 14px;">
                        <br />
                        theMagz.net Reader is now available for your PC. Click download and follow the prompts
                        to install the reader. If you were asked to upgrade and need help, please click
                        here.
                    </p>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="width: 67%">
                <object id="MediaPlayer1" classid="CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95" codebase="http://activex.microsoft.com/activex/controls/mplayer/ en/nsmp2inf.cab#Version=5,1,52,701"
                    standby="Loading Microsoft Windows® Media Player components..." type="application/x-oleobject">
                    <param name="fileName" value="http://themagz.net/Video/Panel_Mask_PAL.wmv">
                    <param name="animationatStart" value="true">
                    <param name="transparentatStart" value="true">
                    <param name="autoStart" value="true">
                    <param name="showControls" value="true">
                    <param name="Volume" value="-200">
                    <embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
                        src="http://themagz.net/Video/Panel_Mask_PAL.wmv" name="MediaPlayer1" autostart="1"
                        width="650px" height="350px" showcontrols="1" volume="-200">
                </object>
            </td>
            <td style="width: 33%; vertical-align: top; padding-left: 10px;">
                <p style="font-size: 16px; font-weight: bold;">
                    theMagz.net Videos :</p>
                <br />
                <p style="font-size: 14px;">
                    Learn the secrets for getting the most out of Zinio, check out our promo videos
                    and more.
                </p>
                <br />
                <asp:Repeater ID="rptrVideo" runat="server">
                    <HeaderTemplate>
                        <ul class="videoul">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:LinkButton ID="lkbVideo" runat="server" PostBackUrl='<%# "playvideo.aspx?VideoURL="+Eval("ID") %>'
                                Text='<%# Eval("VideoName") %>'></asp:LinkButton></li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
</asp:Content>
