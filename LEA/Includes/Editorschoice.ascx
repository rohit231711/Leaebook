<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Editorschoice.ascx.cs"
    Inherits="Includes_Editorschoice" %>
<div class="editorschoice">
    <div class="edit-shadow">
        <div class="shadow">
            <img src="../images/edit-shadow.png" alt="" /></div>
    </div>
    <div class="wrap">
        <div class="choicemain">
            <div class="edittxt">
                <Localized:LocalizedLiteral ID="lbleditorchoice" runat="server" Key="editorchoice"
                    Colon="false" />
            </div>
            <div class="editorsimagbox" runat="server" id="div_editor1" visible="false">
                <a href="#" id="lnkimg_editor1" runat="server">
                    <img src="#" alt="" runat="server" id="editor1" height="325" width="232" />
                </a>
                <div class="morel">
                <p style="padding-top: 15px;">
                    <%--<Localized:LocalizedLiteral ID="lblloremipsum" runat="server" Key="loremipsum" Colon="false" />--%>
                    <a href="#" id="lnklbl_editor1" runat="server">
                        <asp:Label ID="lbl_title1" runat="server" Text=""></asp:Label>
                    </a>
                </p>
                </div>
                <div class="moreli">
                    <a href="#" id="lnk_editor1" runat="server">
                        <Localized:LocalizedLiteral ID="lblmore" runat="server" Key="more" Colon="false" />
                    </a>
                </div>
            </div>
            <div class="editorsimagbox" runat="server" id="div_editor2" visible="false">
                <a href="#" id="lnkimg_editor2" runat="server">
                    <img src="../images/No_Image.jpg" alt="" runat="server" id="editor2" height="325"
                        width="232" />
                </a>
                <div class="morel">
                <p style="padding-top: 15px;">
                    <%--<Localized:LocalizedLiteral ID="lblloremipsum1" runat="server" Key="loremipsum" Colon="false" />--%>
                    <a href="#" id="lnklbl_editor2" runat="server">
                        <asp:Label ID="lbl_title2" runat="server" Text=""></asp:Label>
                    </a>
                </p>
                </div>
                <div class="moreli">
                    <a href="#" id="lnk_editor2" runat="server">
                        <Localized:LocalizedLiteral ID="lblmore1" runat="server" Key="more" Colon="false" />
                    </a>
                </div>
            </div>
            <div class="editorsimagbox" runat="server" id="div_editor3" visible="false">
                <a href="#" id="lnkimg_editor3" runat="server">
                    <img src="../images/No_Image.jpg" alt="" runat="server" id="editor3" height="325"
                        width="232" />
                </a>
                <div class="morel">
                <p style="padding-top: 15px;">
                    <%--<Localized:LocalizedLiteral ID="lblloremipsum2" runat="server" Key="loremipsum" Colon="false" />--%>
                    <a href="#" id="lnklbl_editor3" runat="server">
                        <asp:Label ID="lbl_title3" runat="server" Text=""></asp:Label>
                    </a>
                </p>
                </div>
                <div class="moreli">
                    <a href="#" id="lnk_editor3" runat="server">
                        <Localized:LocalizedLiteral ID="lblmore2" runat="server" Key="more" Colon="false" />
                    </a>
                </div>
            </div>
            <div class="editorsimagbox boxbolast" runat="server" id="div_editor4" visible="false">
                <a href="#" id="lnkimg_editor4" runat="server">
                    <img src="../images/No_Image.jpg" alt="" runat="server" id="editor4" height="325"
                        width="232" />
                </a>
                <div class="morel">
                <p style="padding-top: 15px;" >
                    <%--<Localized:LocalizedLiteral ID="lblloremipsum3" runat="server" Key="loremipsum" Colon="false" />--%>
                  
                    <a href="#" id="lnklbl_editor4" runat="server" >
                        <asp:Label ID="lbl_title4" runat="server" Text=""></asp:Label>
                    </a>
                   
                </p>
                </div>
                <div class="moreli">
                    <a href="#" id="lnk_editor4" runat="server">
                        <Localized:LocalizedLiteral ID="lblmore3" runat="server" Key="more" Colon="false" />
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
