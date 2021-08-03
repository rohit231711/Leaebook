<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageBookSearch.aspx.cs"
    Inherits="Admin_ManageBookSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('<%=txtBookTitle.ClientID %>').focus();
        });
        function backcode() {
            //            debugger;
            //            document.getElementById('<%=hndBooktitle.ClientID %>').value = document.getElementById('<%=txtBookTitle.ClientID %>').value;
            //            document.getElementById('<%=hndBookCategory.ClientID %>').value = document.getElementById('<%=ddlCategorydropdwon.ClientID %>').value;
            debugger;
            //alert('backcode')
            if ((document.getElementById('<%=ddlCategorydropdwon.ClientID %>')).options[(document.getElementById('<%=ddlCategorydropdwon.ClientID %>')).selectedIndex].value == 0 && document.getElementById('<%=txtBookTitle.ClientID %>').value == "" && document.getElementById('<%=txtAuthorName.ClientID %>').value == "" && document.getElementById('<%=txteBookLanguage.ClientID %>').value == "" && document.getElementById('<%=txtFinalPrice.ClientID %>').value == "") {
                //alert('inner');
                alert("Please select any option");
                return false;
            }
            else {
                document.getElementById('<%=btnsearchbtn.ClientID %>').click();
                $('#btnsearchbtn').click();
                $('#btnsearchbtn').trigger('click');
                return true;
            }
        }
        function fancyboxClose() {

            window.parent.$.fancybox.close();
            //parent.$.fancybox.close();

            //window.parent.location.reload();
        }
    </script>
      <link rel="stylesheet" type="text/css" href="../stylesheets/screen_blue.css" />
    <link rel="stylesheet" type="text/css" href="../styles/styles.css" />
    <link rel="stylesheet" type="text/css" href="../styles/green.css" />
    <link rel="stylesheet" type="text/css" href="../styles/superfish.css" />
    <link rel="stylesheet" type="text/css" href="../styles/menu.css" />
    <link rel="stylesheet" type="text/css" href="../uploadify/uploadify.css" />
    <link rel="icon" type="image/png" href="../images/header_logo.png" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hndBooktype" runat="server" />
        <asp:HiddenField ID="hndBooktitle" runat="server" />
        <asp:HiddenField ID="hndBookCategory" runat="server" />
        <asp:Panel runat="server" ID="pnelserch" DefaultButton="btnsearch">
            <asp:Button ID="btnsearchbtn" runat="server" OnClick="btnsearchbtn_Click" Style="display: none;" />
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="550">
                <tbody>
                    <tr>
                        <td height="20">
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 12px;" align="center" height="100" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr>
                                        <td class="popup_listing_border_search" colspan="3" id="searchmsg" align="center"
                                            height="5">
                                        </td>
                                    </tr>
                                    <tr class="light_bg">
                                        <td class="popup_listing_border" align="right" valign="middle" width="150">
                                            <strong>Category</strong>
                                        </td>
                                        <td class="popup_listing_border" align="left" height="37" width="11">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                                            <asp:DropDownList ID="ddlCategorydropdwon" runat="server" AutoPostBack="true" width="197px" Style="margin-left: 2px;"
                                                Height="28px" OnSelectedIndexChanged="ddlCategorydropdwon_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr class="light_bg">
                                        <td class="popup_listing_border" align="right" valign="middle" width="150">
                                            <strong>eBook Title</strong>
                                        </td>
                                        <td class="popup_listing_border" align="left" height="37" width="11">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                                            <input class="input_box" name="txt_srcref_id" id="txtBookTitle" style="width: 190px; height: 28px; margin-left: 5px;" type="text" runat="server" />
                                        </td>
                                    </tr>
                                   
                                </tbody>
                            </table>
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr class="light_bg">
                                        <td class="popup_listing_border" align="right" valign="middle" width="150">
                                            <strong>Author Name</strong>
                                        </td>
                                        <td class="popup_listing_border" align="left" height="37" width="11">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                                            <input class="input_box" name="txt_srcref_id" id="txtAuthorName" style="width: 190px; height: 28px; margin-left: 5px;" type="text" runat="server" />
                                        </td>
                                    </tr>
                                  
                                </tbody>
                            </table>
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr class="light_bg">
                                        <td class="popup_listing_border" align="right" valign="middle" width="150">
                                            <strong>eBook Language</strong>
                                        </td>
                                        <td class="popup_listing_border" align="left" height="37" width="11">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                                            <input class="input_box" name="txt_srcref_id" id="txteBookLanguage" style="width: 190px; height: 28px; margin-left: 1px;" type="text" runat="server" />
                                        </td>
                                    </tr>
                                    
                                </tbody>
                            </table>
                              <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr class="light_bg">
                                        <td class="popup_listing_border" align="right" valign="middle" width="150">
                                            <strong>Final Price</strong>
                                        </td>
                                        <td class="popup_listing_border" align="left" height="37" width="11">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                                            <input class="input_box" name="txt_srcref_id" id="txtFinalPrice" style="width: 190px; height: 28px; margin-left: 5px;" type="text" runat="server" />
                                        </td>
                                    </tr>
                                   
                                </tbody>
                            </table>
                              <table border="0" cellpadding="0" cellspacing="0" width="100%" style="display:none;">
                                <tbody>
                                    <tr class="light_bg">
                                        <td class="popup_listing_border" align="right" valign="middle" width="150">
                                            <strong>Created on</strong>
                                        </td>
                                        <td class="popup_listing_border" align="left" height="37" width="11">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                                            <input class="input_box" name="txt_srcref_id" id="txtCreatedOn" style="width: 190px; height: 28px; margin-left: 5px;" type="text" runat="server" />
                                        </td>
                                    </tr>
                                    
                                </tbody>
                            </table>
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tbody>
                                    <tr class="light_bg">
                                        <td class="popup_listing_border" align="right" valign="middle" width="150">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" height="37" width="11">
                                            &nbsp;
                                        </td>
                                        <td class="popup_listing_border" align="left" valign="middle" width="459">
                                         <asp:Button ID="btnsearch" runat="server" CssClass="button_bg" Text="Submit" OnClientClick="return backcode();" OnClick="btnsearchbtn_Click" />
                                        </td>
                                    </tr>
                                   
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 12px;" align="left" valign="top">
                            &nbsp;
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
