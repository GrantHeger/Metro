<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MetroNowStatus.aspx.vb" Inherits="NFTA_Metro._NowStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>NFTA-Metro | Status Alerts</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />
    <META HTTP-EQUIV="REFRESH" CONTENT="5" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="copyright" content="Copyright (c) NFTA-Metro" />
    <link rel="stylesheet" type="text/css" media="screen" href="/css/cssScreen.css" />
    <link rel="stylesheet" type="text/css" media="print" href="/css/cssPrint.css" />
    <link rel="shortcut icon" href="/img/favicon.ico" type="image/x-icon" />
    <style>
        #statusAlertList{margin:1em; color:#fff;}
         .color2 { background:#b50000; } /* Red */
        .color1 { background:#f9ff00; color:#000; } /* Yellow */
        .color0 { background:#006b03; } /* Green */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id='statusAlertList'>
            
            
            <asp:Repeater ID="rptAlerts" runat="server" >
                <ItemTemplate>
                    <div class='theAlertDetails color<%#Eval("StatusColor")%>'>
                        <div class='AlertTitle'>
                            <span style='font-weight:100;'><strong><%#Eval("RouteDisplayNumber")%></strong><%#Eval("RouteName")%></span>
                        </div>
                        <div class="AlertStamp">
                            <em>posted: </em><%#Eval("DateAdded")%>
                        </div>
                        <br clear="all" />
                        <%#Eval("MessageEmail")%>
                    </div>            
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </form>
</body>
</html>
