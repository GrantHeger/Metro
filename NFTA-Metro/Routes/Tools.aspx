<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Tools.aspx.vb" Inherits="NFTA_Metro.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>NFTA-Metro Transit Tools</title>
    <style type="text/css">
           body {text-align:left;} 
          #TransitTools
          {
            background:#fff;
            margin:30px 4px;
            padding:10px 10px 10px 10px;
            border:2px solid #004990;
        	border-radius:8px; /*CSS3 border radius*/
            -moz-border-radius:8px;
            -webkit-border-radius:8px;	
            box-shadow:0 0 15px #959595; /*CSS3 shadow*/
            -moz-box-shadow:0 0 15px #959595;
            -webkit-box-shadow:0 0 15px #959595;
          
            }
            
             #homeAlerts
          {
            background:#fff;
            margin:30px 4px;
            padding:10px 10px 10px 10px;
            border:2px solid #b10100;
        	border-radius:8px; /*CSS3 border radius*/
            -moz-border-radius:8px;
            -webkit-border-radius:8px;	
            box-shadow:0 0 15px #959595; /*CSS3 shadow*/
            -moz-box-shadow:0 0 15px #959595;
            -webkit-box-shadow:0 0 15px #959595;
          
            }
            
            #homeAlertsItems 
            {
            	text-align:center;
            	max-width:570px;
            }
            
            .alert {float:left;}
            
          
          #homeAlerts a, #homeAlerts a:hover
          {
          	    color:#fff;
          	    background:#b10100;
          	    font-size:.9em;
          	     border:0px solid #b10100;
        	    border-radius:4px; /*CSS3 border radius*/
                -moz-border-radius:4px;
                -webkit-border-radius:4px;	
                box-shadow:0 0 15px #959595; /*CSS3 shadow*/
                -moz-box-shadow:0 0 15px #959595;
                -webkit-box-shadow:0 0 15px #959595;
                text-decoration:none;
                padding:4px 6px;
                margin:0px 3px;
          }
          
          #homeAlerts a:hover 
          {
          	color:#000;
          	background:#edc93e;
        
     
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center><a href="/"><img src="/img/logos/metro-logo.png" width="250" alt="NFTA-Metro" /></a></center>
        <div id="TransitTools">
            <img src="/img/h1-transittools.png" width="240" height="50" alt="Transit Tools" /><br /><br />
            <center>
            <a href="/programs/howto.aspx" target="_ht"><img src="/img/tt-btn-howto.png" class="domroll /img/tt-btn-howto-o.png" width="295" height="74" alt="How to videos" /></a><br />
            <a href="http://stp.nfta.com/" target="_stp"><img src="/img/tt-btn-stp.png" class="domroll /img/tt-btn-stp-o.png" width="295" height="74" alt="Wheres My Bus" /></a><br />
            <a href="/routes/schedules.aspx"><img src="/img/tt-btn-schedules.png" class="domroll /img/tt-btn-schedules-o.png" width="295" height="74" alt="Schedules" /></a><br />    
            <a href="/Routes/Systemmap.aspx"><img src="/img/tt-btn-systemmap.png" class="domroll /img/tt-btn-systemmap-o.png" width="295" height="74" alt="System Map" /></a> <br />
            <a href="/Trip/Trip_Planner.asp"><img src="/img/tt-btn-tripplanner.png" class="domroll /img/tt-btn-tripplanner-o.png" width="295" height="74" alt="Metro Trip Planner" /></a><br />
            <a href="/Routes/Fares.aspx"><img src="/img/tt-btn-fares.png" class="domroll /img/tt-btn-fares-o.png" width="295" height="74" alt="Fares" /></a><br />
            <a href="https://www.google.com/maps/dir///@42.87697,-78.845959,17709m/data=!3m1!1e3!4m4!4m3!1m0!1m0!3e3" target="_gt"><img src="/img/tt-btn-google.png" class="domroll /img/tt-btn-google-o.png" width="295" height="74" alt="Google Transit" /></a><br />     
            <a href="https://itunes.apple.com/us/app/nfta-metro/id908771775?mt=8&ign-mpt=uo%3D4" target="_new"><img src="/img/transit-app.png" class="domroll /img/transit-app-o.png" width="295" height="150" alt="Download NFTA-Metro Transit App"  /></a><br />
            <!--<a href="http://alerts.nfta.com"><img src="/img/tt-btn-iu.png" class="domroll /img/tt-btn-iu-o.png" width="295" height="74" alt="NFTA Instant Updates"  /></a><br />-->
           </center>
       </div>

    
        <div id="homeAlerts">
            <img src='/img/riderAlerts.png' width='240' height='50' alt='Rider Alerts' /><br /><br />
            <center>
            <div id="homeAlertsItems">
                <asp:PlaceHolder ID="phNoAlerts" runat="server">
                   There are no rider alerts at this time.<br /><br />
                </asp:PlaceHolder>
    	  	    <asp:Literal ID="litAlertLinks" runat="server" />  
    	  	    <br clear="all" />          
            </div>
            </center>
        </div> 
        <center><a href="/">click here</a> for more Metro information.</center>
    <script type ="text/javascript"  src="/js/domroll.js"></script> 
    </form>
</body>
</html>
