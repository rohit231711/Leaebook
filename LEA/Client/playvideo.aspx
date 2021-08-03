<%@ Page Language="C#" AutoEventWireup="true" CodeFile="playvideo.aspx.cs" Inherits="playvideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #MediaPlayer1
        {
            height: 685px;
            width: 921px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <object id="MediaPlayer1" classid="CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95" codebase="http://activex.microsoft.com/activex/controls/mplayer/ en/nsmp2inf.cab#Version=5,1,52,701"
            standby="Loading Microsoft Windows® Media Player components..." type="application/x-oleobject">
            <param name="fileName" value='<%= GetUrl %>'>
            <param name="animationatStart" value="true">
            <param name="transparentatStart" value="true">
            <param name="autoStart" value="true">
            <param name="showControls" value="true">
            <param name="Volume" value="-200">
            <embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
                width="921px" height="685px" src='<%= GetUrl %>' name="MediaPlayer1" autostart="1"
                showcontrols="1" volume="-200">
        </object>
    </div>
    </form>
</body>
</html>
