<%@ Page Title="Niagara Frontier Transportation Authority (NFTA) - Metro - Schedules" Language="vb" AutoEventWireup="false" MasterPageFile="~/Metro.Master" 
    CodeBehind="Schedules.aspx.vb" Inherits="NFTA_Metro.Schedules" %>
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
        
        
     <style type="text/css">

    #wrapper {
	    width: 600px;
	    margin-left: auto;
	    margin-right: auto;
	    }

    
    
    .routeLink a, .routeLink a:link, .routeLink a:hover{	
	    
	    width: 596px;
	    background: #f6f6f6;
	    border-bottom: 3px solid #d3d3d3;
	    cursor: pointer;
	    padding:6px;
	    -moz-border-radius: 10px;
        border-radius: 10px;
        margin:12px 0 0 0;
        text-decoration:none;
         min-width: 600px;
         display:block;
         color:#000; 
         min-height:20px;
	    }
    	

    .routeLink a:hover {
	    background: #003366;
	    color:#fff;
	    }
    	
    .routeName {float:left;}
    
   
    
    .listRoutes, .listRoutesAlt {
    	background:#d2d2d2;
	    padding:10px 5px;
	    line-height:32px;
	    margin:5px;
        color:#000; 
        border:0px solid #006b03;
	    border-radius:4px; /*CSS3 border radius*/
        -moz-border-radius:4px;
        -webkit-border-radius:4px;	
      
     }
     
     .headingRoutes 
     {

	    padding:10px 5px 0 5px;
	    line-height:16px;
	    margin:10px;
        color:#000; 
        border:0px solid #006b03;
	    border-radius:4px; /*CSS3 border radius*/
        -moz-border-radius:4px;
        -webkit-border-radius:4px;	
      
     }
 
     .cellStatus {
        width:55px;
        float:left;
        text-align:center;      
     }
     .cellRoute {
        width:220px; float:left;  
     }
     
     .cellRevised{
        width:80px; text-align:right; float:left;  
     }
     
     .cellDelta {
        width:40px; padding-top:6px; text-align:right; float:left;  
     }
     
     .cellRevisedDelta { width:120px; text-align:right;  float:left;  }
     
     .cellLink {
        width:160px; text-align:right;       float:left;  
     }
     

    .center {text-align:center;}
    .bold {font-weight:900;}
  
    </style>   
</asp:Content> 

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Schedules</h1>
   
                    
    <div id="scheduleKey">
        <p><b>Metro Now Status:</b></p>
        <p class="indent"><a href="#" class="btn btn-primary btn-lg color0">&nbsp;</a> Green = scheduled service</p>
        <p class="indent"><a href="#" class="btn btn-primary btn-lg color1">&nbsp;</a> Yellow = minor delays/reroutes</p>
        <p class="indent"><a href="#" class="btn btn-primary btn-lg color2">&nbsp;</a> Red = major delays/cancellations</p>

        <p style="margin:16px 0 0 0;"><b>Route Changes:</b></p>
        
        <p class="indent">Effective Dates appearing in <b>bold type</b> = recent changes have been made</p>
        <p class="indent"><img src="/img/delta.png" alt="Upcoming route change" width="20" /> A Delta = upcoming schedule change</p>
        <p style="margin:16px 0 0 0; text-align:center; font-size:.8em;">Click on "view route details" for schedules, status alerts, change history, and for a preview of upcoming changes</p>
                    
    </div>      
                    
    
            
    <div id="scheduleTable">        
        <div class="headingRoutes">
            <div class="cellStatus bold">Status</div>
            <div class="cellRoute center bold">Route Name</div>
            <div class="cellRevisedDelta bold">Effective Date</div>
            <div class="cellLink center bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Route Details</div>
            <br clear="all" />
        </div>
        
     
        <asp:Repeater ID="rptCategories" runat="server">
            <ItemTemplate>
                <asp:Repeater ID="rptRoutes" runat="server">
                   <ItemTemplate>
                        <div class="listRoutes">
                            <div class="cellStatus"><a href="#" class="btn btn-primary btn-lg color<%#Eval("StatusColor")%>">&nbsp;&nbsp;&nbsp;</a></div>
                            <div class="cellRoute"><%#Eval("RouteDisplayNumber")%> <%#Eval("RouteName")%></div>
                            <div class="cellRevised"><%#Eval("Rev")%></div>
                            <div class="cellDelta"><%#Eval("Delta")%></div>
                            <div class="cellLink"><a class="btn btn-primary btn-lg details" href="Route.aspx?rt=<%#Eval("RouteNumber")%>">View Route Details</a></div>
                            <br clear="all" />
                        </div>
                        
                   </ItemTemplate>
                </asp:Repeater>      
            </ItemTemplate>     
        </asp:Repeater>
    </div>     
      <!-- Latest compiled and minified JavaScript -->
    <script type='text/javascript' src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <!--<script type='text/javascript' src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>-->


</asp:Content>

