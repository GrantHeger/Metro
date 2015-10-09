<%@ Page Title="Niagara Frontier Transportation Authority (NFTA) - Metro - Schedules" Language="vb" AutoEventWireup="false" MasterPageFile="~/Metro.Master" 
    CodeBehind="MetroNowHistory.aspx.vb" Inherits="NFTA_Metro.RouteHis" %>
<%@ Import Namespace="NFTA_Metro.nftaRoute" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Schedules</title>
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
    <div id="quickSelect" >
        <select name="menu1" id="menu1">
            <option value="">--- Change Route ---</option>
            <asp:Repeater ID="rptCategories" runat="server">
                <ItemTemplate>
                    <asp:Repeater ID="rptRoutes" runat="server">
                        <ItemTemplate>
                           <option value="Route.aspx?rt=<%#Eval("RouteNumber")%>"><%#Eval("RouteDisplayNumber")%> <%#Eval("RouteName")%></option>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater> 
        </select> 
        <script type="text/javascript">
         var urlmenu = document.getElementById( 'menu1' );
         urlmenu.onchange = function() {
              window.open(  this.options[ this.selectedIndex ].value );
         };
        </script>
        &nbsp;&nbsp;&nbsp;&nbsp;<a class="return" href="Schedules.aspx">&#9668; view all schedules</a>
   </div>
        
    <asp:PlaceHolder runat="server" id="phShowRt">
      <h1><asp:Literal runat="server" ID="litRouteDisplayName"></asp:Literal></h1>
        <div style="clear:both;  margin:0 auto; text-align:left">     
            <asp:Literal runat="server" ID="litAlert"></asp:Literal>                                   
        </div>
    </asp:PlaceHolder> 
    
    <asp:PlaceHolder runat="server" id="phHideRt" Visible="false">
        <h2 class="newsheader"><em>Routes</em></h2>    	
        <p>No schedule information is available for the Route selected. <a href="schedules.aspx">Go back</a> to the the Routes page and select a current Route.
        </p>
        
    </asp:PlaceHolder>  
    <!-- Latest compiled and minified JavaScript -->
    <script type='text/javascript' src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <!--<script type='text/javascript' src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>-->

</asp:Content>