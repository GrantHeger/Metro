<%@ Page Title="Niagara Frontier Transportation Authority (NFTA) - Metro - Schedules" Language="vb" AutoEventWireup="false" MasterPageFile="~/Metro.Master" 
    CodeBehind="MetroNow.aspx.vb" Inherits="NFTA_Metro.MetroNow" %>
<%@ Import Namespace="NFTA_Metro.nftaRoute" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Metro Now | Status Alerts</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />
    
    <!-- Removing The Default Zoom on Mobile -->
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
        
        <!-- Latest compiled and minified Bootstrap core CSS -->
        <link href="/bootstrap-3.2.0-dist/css/bootstrap.min.css" rel="stylesheet"> 
        <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"> -->

        <!-- Optional Bootstrap theme -->
        <link href="/bootstrap-3.2.0-dist/css/bootstrap-theme.min.css" rel="stylesheet"> 
        <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap-theme.min.css"> -->

       
        <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->

</asp:Content> 

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    
   <h1>Metro Now - System status alerts.</h1>
   <p class='txtCenter'>These may include Rider Alerts, Special Bulletins and other route specific information.</p>
   
    <asp:DropDownList ID="ddlNumToShow" Width="100" AutoPostBack="true" runat="server" Visible="false">
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
    </asp:DropDownList>
   
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
   
   
    <!-- Latest compiled and minified JavaScript -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js" type="text/javascript"></script>
    <!--<script type='text/javascript' src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>-->

</asp:Content>