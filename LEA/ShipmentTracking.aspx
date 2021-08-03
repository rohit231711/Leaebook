<%@ Page Title="Shipment Tracking" Language="C#" MasterPageFile="~/LEA_Master.master" AutoEventWireup="true" CodeFile="ShipmentTracking.aspx.cs" Inherits="ShipmentTracking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .tracking-results .tracking-result {
            clear: both;
        }

        .tracking .hd {
            display: none;
        }

        .tracking-results .tracking-result table.result-summary {
            border-bottom: 1px solid #d1d1d1;
            margin-bottom: .5em;
        }

        .tracking-results table {
            border-spacing: 0;
            border-collapse: collapse;
            empty-cells: show;
            width: 100%;
        }

        .tracking-results .tracking-result table.result-summary td {
            background-color: #e8e8e4;
            border: none;
        }

        .tracking-results table th, .tracking-results table td {
            font-size: 11px;
            font-size: 0.6875rem;
            line-height: 1.1818181818181819;
            border-right: 3px solid #ffffff;
            border-top: 1px solid #d1d1d1;
            padding: 0.36363636363636365em 0.5454545454545454em;
            text-align: left;
            vertical-align: top;
        }

        .tracking-results .tracking-result table.result-checkpoints.show {
            display: table;
        }

        .tracking-results .tracking-result table.result-checkpoints {
            display: none;
            margin: .8em 0;
        }

        .tracking-results table {
            border-spacing: 0;
            border-collapse: collapse;
            empty-cells: show;
            width: 100%;
        }

        .tracking-results .tracking-result table.result-summary .column-delivery-state {
            width: 5%;
        }

        .tracking-results .tracking-result table.result-summary .column-waybill {
            width: 32%;
        }

        .tracking-results .tracking-result table.result-summary .column-destination {
            width: 63%;
        }

        .tracking-results .tracking-result table.result-checkpoints .column-counter {
            width: 5%;
        }

        .tracking-results .tracking-result table.result-checkpoints .column-description {
            width: 32%;
        }

        .tracking-results .tracking-result table.result-checkpoints .column-location {
            width: 54%;
        }

        .tracking-results .tracking-result table.result-checkpoints .column-time {
            width: 9%;
        }
    </style>
    <script>
        function validation() {
            var cultureLanguage = '<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>';
            if (document.getElementById('<%=txtTrackNumber.ClientID %>').value == "") {
                if (cultureLanguage == "es-ES") {
                    alert("Por favor, introduzca Identificación del fin.");
                }
                else {
                    alert("Please enter tracking number.");
                }
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb">
        <div class="wrap">
            <ul>
                <li><a href="Index.aspx?l=<%=System.Threading.Thread.CurrentThread.CurrentCulture.Name %>">
                    <Localized:LocalizedLiteral ID="lblhome" runat="server" Key="home" Colon="false" /></a>/</li>
                <li><span><a href="#">
                    <Localized:LocalizedLiteral ID="lbltrackorder" runat="server" Key="trackorder" Colon="false" /></a></span></li>
            </ul>
        </div>
    </div>

    <div class="wrap">
        <div class="logintxt">
                <h1>
                    <Localized:LocalizedLiteral ID="lbltrackorder1" runat="server" Key="trackorder" Colon="false" />
                </h1>
            </div>
        <div class="loginbox1" style="margin-bottom: 30px;">
            <div class="loginbox1">
                <div class="perinfo" style="text-align: left;">
                    <Localized:LocalizedLiteral ID="lblpersonalinformation" runat="server" Key="trackorder"
                        Colon="false" />
                </div>
                <div class="frm-txt">
                    <asp:TextBox ID="txtTrackNumber" runat="server" class="nametxt" Style="width: 50%; float: left;"></asp:TextBox>
                    <asp:LinkButton ID="btnTrack" runat="server" class="submitbtn" OnClick="btnTrack_Click" OnClientClick="return validation();">
                        <Localized:LocalizedLiteral ID="lblsubmit" runat="server" Key="submit" Colon="false" />
                    </asp:LinkButton>
                </div>

                <div class="clear"></div>
                <div class="tracking-result express hd" runat="server" id="divNoTrack" visible="false">
                    <table class="result-summary" style="width: 100%;margin: 15px 0;">
                        <colgroup>
                            <col class="column-delivery-state">
                            <col class="column-waybill">
                            <col class="column-destination">
                        </colgroup>
                        <tbody>
                            <tr>
                                <td class="delivery code100" title=""></td>
                                <td class="waybill result-has-no-remarks">
                                    <strong>Not Found Any Tracking Details:</strong>
                                    <span style="display:block">No result found for your query. Please try again.</span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="tracking-result express hd" runat="server" id="divTrack" visible="false">
                    <table class="result-summary" style="width: 100%;font-family: 'abeezeeregular';">
                        <colgroup>
                            <col class="column-delivery-state">
                            <col class="column-waybill">
                            <col class="column-destination">
                        </colgroup>
                        <tbody>
                            <tr>
                                <td class="delivery code100" title=""></td>
                                <td class="waybill result-has-no-remarks">
                                    <strong>Waybill:
                                        <asp:Label runat="server" ID="lblWaybill"></asp:Label></strong>
                                    <span style="display:block">
                                        <asp:Label runat="server" ID="lblInfo"></asp:Label></span>
                                </td>
                                <td>
                                    <span>Thursday, February 14, 2013  at 15:14</span><br />
                                    <span>Origin Service Area:</span><br />
                                    <asp:Label runat="server" ID="lblOrigin"></asp:Label><br />
                                    <span>Destination Service Area:</span><br />
                                    <asp:Label runat="server" ID="lblDestination"></asp:Label><br />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="result-checkpoints show" style="font-family: sans-serif;width: 50%;">
                        <colgroup>
                            <col class="column-counter">
                            <col class="column-description">
                            <col class="column-location">
                            <col class="column-time">
                        </colgroup>
                        <thead>
                            <tr>
                                <th colspan="2">
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </th>
                                <th>Location</th>
                                <th>Time</th>

                            </tr>
                        </thead>
                        <tr>
                            <td>1</td>
                            <td>
                                <asp:Label runat="server" ID="lblInfo1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblOrigin1"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

