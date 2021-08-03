<%@ Page Title="Banner" Language="C#" MasterPageFile="~/Admin/AdminMaster.master"
    AutoEventWireup="true" CodeFile="banner.aspx.cs" Inherits="Admin_banner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="../Client/js/jquery.magnifier.js"></script>
    <script src="../Client/js/jquery.jcarousel.min.js" type="text/javascript"></script>
    <link href="../Client/css/skin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=imgcat.ClientID %>');
            var file = document.querySelector('#<%=Fubanner.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
       
        function checkvalidation() {
            var valid = true;

            if ($("#<%=txtTitle.ClientID%>").val() == '' && valid == true) {
                valid = false;
                alert('Please Insert Banner Title');
                return false;
                $("#<%=txtTitle.ClientID%>").focus();
            }

            var fup = document.getElementById('<%=Fubanner.ClientID %>');
            var fileName = fup.value;
            if (fileName != "") {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1).toLowerCase();
                if (ext == "gif" || ext == "png" || ext == "jpg" || ext == "jpeg") {
                    return true;
                }
                else {
                    $("#message-green").show().fadeOut(5000);
                    document.getElementById('succ').innerHTML = "Please select valid image.";
                    return false;
                }
            }
            else {
                if (document.getElementById('<%=hnfImage.ClientID %>').value == "0") {
                    alert('Please upload image.');
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnImageName" runat="server" />
    <asp:HiddenField ID="hnfImage" runat="server" Value="0" />
    <asp:MultiView ID="mltBanner" runat="server">
        <asp:View ID="viewList" runat="server">
            <div style="float: right">
                <asp:Button ID="BtnAdd" runat="server" CssClass="button_bg" Text="Add New" OnClick="BtnAdd_Click" /></div>
            <br />
            <br />
            <div id="product-table">
                <div id="form_user_managementview">
                    <asp:GridView ID="GrdList" runat="server" SkinID="skinGrid" GridLines="None" AutoGenerateColumns="false"
                        EmptyDataText="No record found!" Width="100%" OnRowCommand="GrdList_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="table-header-check" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Banner Image"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" />
                                <ItemTemplate>
                                    <img src='<%# "../Banner/" + Eval("ImagePath") %>' data-magnifyto="500" class="magnify"
                                        alt="" width="150px" height="100px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-ForeColor="Black">
                                <HeaderStyle Width="66%" CssClass="table-header-check line-left" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Title") %>'></asp:Label></strong><br />
                                    <asp:Label ID="txtDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="table-header-check line-left" />
                                <HeaderTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Options"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbEdit" runat="server" CssClass="icon-101" CommandName="Edit1"
                                        CommandArgument='<%#Eval("ID")%>'></asp:LinkButton>
                                    <asp:LinkButton ID="lkbDelete" runat="server" CssClass="icon-102" CommandName="Delete1"
                                        CommandArgument='<%#Eval("ID")%>' OnClientClick="return confirm('Are you sure you want to delete?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </asp:View>
        <asp:View ID="viewAdd" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <strong>Banner Title</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Select Image</strong>
                    </td>
                    <td>
                        <asp:FileUpload ID="Fubanner" runat="server" onchange="previewFile()" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <td colspan="2">
                    &nbsp;
                </td>
                <%-- <tr>
                    <td>
                        <strong>Description</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="txtdesc" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <%--<strong>Image</strong>--%>
                    </td>
                    <td>
                        <asp:Image ID="imgcat" Width="100px" Height="100px" runat="server" Visible="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button_bg" Text="Submit" OnClientClick="return checkvalidation();"
                            OnClick="btnSubmit_Click" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="button_bg" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
            <div style="font-size: 11px; font-weight: bold; margin-top: 10px;">
                NOTE: Please upload image with size (1006*310) for better resolution.
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
