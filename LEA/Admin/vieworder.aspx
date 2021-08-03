<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vieworder.aspx.cs" Inherits="Admin_vieworder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="product-table">
            <asp:DataList ID="dlBookIssue" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                CellPadding="5" CellSpacing="5" BorderStyle="None" Width="100%">
                <ItemTemplate>
                    <table width="100%" cellpadding="5" cellspacing="5" border="1">
                        <tr>
                            <td rowspan="6" style="width: 30%;">
                                <asp:Image ID="img1" runat="server" ImageUrl='<%#Eval("FrontImage")%>' BorderStyle="None"
                                    Width="180" Height="270" />
                            </td>
                            <td colspan="2" style="font-size: 16px;">
                                <b>
                                    <%#Eval("BookTitle")%></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                LANGUAGE :
                            </td>
                            <td>
                                <%#Eval("Language")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                COUNTRY :
                            </td>
                            <td>
                                <%#Eval("countryname")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PUBLISHER
                            </td>
                            <td>
                                <%#Eval("Publisher")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PUBLISH DATE
                            </td>
                            <td>
                                <%# Eval("PublishDate","{0:d}")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PRICE :
                            </td>
                            <td>
                                <%# "RM "+Convert.ToDecimal(Eval("Price").ToString()).ToString("0.00")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    </form>
</body>
</html>
