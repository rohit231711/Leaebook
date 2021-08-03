<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="Admin_dashboard" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Tenantcontainer').highcharts({

                chart: {
                    type: 'column',
                    inverted: false
                },
                title: {
                    text: '<%=TenantTitle %>'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [<%= TenantCategories %>],
                    title: {
                        text: "",
                        fontSize: '15px'
                    }
                },
                yAxis: {
                    min: 0,
                    tickInterval: 5,
                    title: {
                        text: "No. Of Users",
                        align: 'high'
                    },
                    labels: {
                        overflow: 'justify'
                    }
                },
                tooltip: {
                    valueSuffix: ' '
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -40,
                    y: 100,
                    floating: true,
                    borderWidth: 1,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor || '#FFFFFF'),
                    shadow: true
                },
                credits: {
                    enabled: false
                },
                series: [<%=TenantSeries %>]
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#Tenantcontainer1').highcharts({
                chart: {
                    type: 'column',
                    inverted: false
                },
                title: {
                    text: '<%=TenantTitle1 %>'
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [<%= TenantCategories1 %>],
                    title: {
                        text: "",
                        fontSize: '15px'
                    }
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: "Selling Of Each Month",
                        align: 'high'
                    },
                    labels: {
                        overflow: 'justify'
                    }
                },
                tooltip: {
                    valuePrefix: '$ '
                },
                //                tooltip: {
                //                    formatter: function() {
                //                    return customFormatPointName(this.point.name) + ' $ ' + this.y;
                //                        }
                //                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -40,
                    y: 100,
                    floating: true,
                    borderWidth: 1,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor || '#FFFFFF'),
                    shadow: true
                },
                credits: {
                    enabled: false
                },
                series: [<%=TenantSeries1 %>]
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <span id="spanSelectedMenu" style="display: none">aDashboard</span>
    <div id="table-content">
        <div class="deshbord_banner">
            <!--<script type="text/javascript" src="js/jquery.min.js"></script>-->


            <%--     <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto">
            </div>--%>
        </div>
        <div style="width: 50%; float: left;" id="Tenantcontainer">
            <%--<div>--%>
            <%-- <!-- Ordered bars Chart -->
                    <div id="chart_ordered_bars" style="height: 275px; width:500px;" >
                        <asp:Chart ID="Chart1" runat="server" Style="height: 275px; width:500px;">
                            <Series>
                                <asp:Series Name="Series1" ChartType="Column" Color="Gray" >
                                    
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>--%>
        </div>

        <div style="width: 50%; float: left;" id="Tenantcontainer1">
            <%-- <div>
                    <!-- Ordered bars Chart -->
                    <div id="Div1" style="height: 275px; width:500px;" >
                        <asp:Chart ID="Chart2" runat="server" Style="height: 275px; width:500px;">
                            <Series>
                                <asp:Series Name="Series1" ChartType="Column" Color="Gray" >
                                    
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" >
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </div>--%>
        </div>

    </div>
    <%--<div style="width: 47%; float: right;"><label><b>Total Amount : </b><%=TotAmount%></label></div>--%>
    <div style="width: 100%; float: right; text-align: center;">
        <label><b>Total Amount : </b><%=TotAmount%></label>
    </div>
</asp:Content>

