<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SystemMap.aspx.vb" Inherits="NFTA_Metro._SM"  %>
<!DOCTYPE html ">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Niagara Frontier Transportation Authority (NFTA) - Metro - System Maps</title>
     <link href="/js/bootstrap/css/bootstrap-theme.min.css" rel="Stylesheet" type="text/css" />
     <link href="/js/bootstrap/css/bootstrap.min.css" rel="Stylesheet" type="text/css" />
     
     <style type="text/css">
        v\:* {behavior:url(#default#VML)}
        html, body {
            width:100%; height:100%;
        }
        html {
            overflow: hidden
        }
        body {
            margin:0px 0px 0px 0px; 
            padding:0px;
            /* background-color matched to panel, to hide small gap below it */
            background-color:#ccc;   
        }
        
        h2 {font-size:1.2em; font-weight:900;}
        
        
        #map {
            /*margin-right could be 315px instead, but then the map 'overflow' spilled 18px to the right of where it should*/
           /*margin-right:330px; */
            height:100%;
        }
        
        #logopanel  {
        	Position:absolute;
            bottom:0px; 
            width:100%;
            height:45px;
            min-height:45px;
            background:#5089b8;
            font-weight:900;
            margin:auto 0;
            padding-top:7px; 
            color:#fff;
            font-family:Arial, Helvetica, sans-serif; 
            font-size:13px;
            text-align:center;
            color:#fff;
   
        }
        
        #logopanel p {color:#fff; overflow:auto; max-height:50px;}
        
        #logopanel a {	color:#fee679;}
        
         	
         #logopanel a.btnMap, #logopanel a.btnMap:hover   {font-size:12px; padding:5px 8px; font-weight:900; text-decoration:none; background:#fee679; color:#5089b8;  -moz-border-radius: 4px; border-radius: 4px;}
        
        #logopanel a.btnMap:hover {background:red; color:#fff;}
        
        #rightpanel {
            position: absolute; left:5px; top:0; width:300px; height:99%; overflow-y: scroll; overflow-x: hidden;
            background: #fff; font-family:Arial, Helvetica, sans-serif; font-size:13px; 
			color:#000;
			/* border:3px solid #002663;  */
            padding: 0px 5px 0px 10px;
            /* In MSIE; padding-bottom MUST be 0px or panel's scrollbar will go under status bar */
             border:#fff 1px solid;
            border-radius:8px; /*CSS3 border radius*/
            -moz-border-radius:8px;
            -webkit-border-radius:8px;
        }
   
        .controlPanel {
        padding: 20px 40px 20px 20px;
        width: 320px;
        height:90%;
        background: #5089b8;
        border:#5089b8 8px solid;
        border-radius:8px; /*CSS3 border radius*/
        -moz-border-radius:8px;
        -webkit-border-radius:8px;
        box-shadow:0 0 10px #888888; /*CSS3 shadow*/
        -moz-box-shadow:0 0 10px #888888;
        -webkit-box-shadow:0 0 10px #888888;
        }
        
             
        ::-webkit-scrollbar {
            width: 14px;
        }
         
        /* Track */
        ::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3); 
            -webkit-border-radius: 6px;
            border-radius: 6px;
        }
         
        /* Handle */
        ::-webkit-scrollbar-thumb {
            -webkit-border-radius: 6px;
            border-radius: 6px;
            background: rgba(255,231,121,1.0); 
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.2); 
        }
        ::-webkit-scrollbar-thumb:window-inactive {
	        background: rgba(255,0,0,0.4); 
        }




           .railStation {

             
             font-family: "Lucida Grande", "Arial", sans-serif;
             font-size: 13px;
             font-weight: 900;
             text-align: left;
             white-space: nowrap;
           }
    </style>
   
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=true" type="text/javascript"></script>
    <script src="/js/StyleMarker.js" type="text/javascript"></script>
    <script src="/js/markerwithlabel.js" type="text/javascript"></script>
    <script src="/js/geolocationmarker-compiled.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jquery-1.10.2.min.js"></script>
    <script src="/js/jquery.tabSlideOut.v1.3.js" type="text/javascript"></script>
    <script src="/js/SystemMap.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $('.controlPanel').tabSlideOut({
                tabHandle: '.controlContent',               //class of the element that will become your tab
                pathToTabImage: '/img/panel.png',           //path to the image for the tab //Optionally can be set using css
                imageHeight: '429px',                       //height of tab image           //Optionally can be set using css
                imageWidth: '57px',                         //width of tab image            //Optionally can be set using css
                tabLocation: 'right',                       //side of screen where tab lives, top, right, bottom, or left
                speed: 300,                                 //speed of animation
                action: 'click',                            //options: 'click' or 'hover', action to trigger animation
                topPos: '30px',                             //position from the top/ use if tabLocation is left or right
                leftPos: '0px',                             //position from left/ use if tabLocation is bottom or top
                fixedPosition: false                         //options: true makes it stick(fixed position) on scroll
            });
        });
    </script>
   
    <asp:Literal ID="litRouteScript" runat="server"></asp:Literal>
    <script type="text/javascript">
        $(document).ready(function () {
            SetGeoLocation();            
            function SetGeoLocation() { 
                      
            // Try HTML5 geolocation
                if(navigator.geolocation) {                                                
                    navigator.geolocation.getCurrentPosition(function(position) {           
                    var pos = new google.maps.LatLng(position.coords.latitude,                
                                              position.coords.longitude);              
                                      
                    var imgHere = 'http://metro.nfta.com/img/here.png'                                                              
                    var marker = new google.maps.Marker({
                        position: pos,
                        map: map,
                        icon: imgHere,
                        title:"You are here!"
                    });
                    
             
                    
                    //var circle = new google.maps.Circle({
                       // center: pos,
                       // radius: 100,
                       // map: map, //your map,
                       // strokeColor:"#cccccc",
                       // strokeOpacity:0.2,
                       // strokeWeight:2,
                       // fillColor:"#999999",
                       // fillOpacity:0.2
                   // });
                    
                    //var infowindow = new google.maps.InfoWindow({                         
                         //map:    map,                                                      
                         //position: pos,                                                    
                         //Content: 'You are here'                            
                    //});                                                                   
                                                                                       
                 map.setCenter(pos);                                                   
             }, function() {                                                           
                 handleNoGeolocation(true);                                            
             });                                                                       
         } else {                                                                      
          // Browser doesn't support Geolocation                                       
          handleNoGeolocation(false);                                                  
         }                                                                             
                                                                   
        function handleNoGeolocation(errorFlag) {                                      
         if (errorFlag) {                                                              
             var content = 'Error: The Geolocation service failed.';                   
             } else {                                                                      
                 var content = 'Error: Your browser doesn\'t support geolocation.';        
             }                                                                             
                                                                                           
                 var options = {                                                           
                     map: map,                                                             
                     position: new google.maps.LatLng(60, 105),                            
                      content: content                                                     
                 };                                                                        
             }                                                                             
             var infowindow = new google.maps.InfoWindow(options);                         
             //map.setCenter(options.position);                                            
                                                                                        
              // END Geolocation                                                           
            
         }
                 
    });
    </script>


</head>
<body>
    <div id="map" style="width: 100%; height: 100%"></div>
    <div id="logopanel">
        <table width="100%" cellpadding="4">
            <tr>
                <td align="center"><img src="/img/metro-yellow.png" width="114" height="30" alt="Metro" /></td>
                <!--<td align="center"><p>This system map represents our general service area.</p></td>-->
                <td align="center">
                    <a href="/pdfs/Systemmap.pdf" target="_new" class="btnMap">Download System Map [PDF]</a>
                    &nbsp;
                    <a href="/Contact/Feedback.aspx" target="_new" class="btnMap">Provide Map Feedback</a>
                </td>
            </tr>
        </table>
    </div>
    <div class="controlPanel">
        <a class="controlContent"></a>     
        <div id="rightpanel">
            <!--<p align="center"><img src="/img/metro-logo-cp.png" width="184" height="50" alt="NFTA-Metro" /></p>-->
            <div>
                <h2><input type="checkbox" name="trainBox" id="trainBox" checked="checked" onclick="catClick(this, 'trains')" /> Metro Rail:</h2>
                <div class="itemsDiv" style="padding: 0 0 0 20px;">
                    <asp:Literal ID="litTrainMarkup" runat="server"></asp:Literal>
                </div>
            </div>
            
            <div>
                <h2><%--<input type="checkbox" name="routesBox" id="routesBox" checked="checked" onclick="catClick(this,'routes')" />--%><input type="checkbox" name="routesBox" id="routesBox" checked="checked" onclick="catClick(this,'routes')" /> Bus Routes:</h2>
                <div class="itemsDiv" style="padding:0 0 0 20px;">
                    <asp:Literal ID="litMarkup" runat="server"></asp:Literal>
                </div>
            </div>
            
            <!-- DO NOT REMOVE THIS DIV -->
            <div>
                <h2><input type="checkbox" checked="checked" name="pnrBox" id="pnrBox" onclick="catClick(this,'pnr')" /> Park & Rides:</h2>
                <div class="itemsDiv" style="padding:0 0 0 20px;">
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr001Box" id="pnr001Box" onclick="pnrClick(this, 'pnr001', 0)" />Appletree Business Park<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr002Box" id="pnr002Box" onclick="pnrClick(this, 'pnr002', 1)" />Athol Springs<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr003Box" id="pnr003Box" onclick="pnrClick(this, 'pnr003', 2)" />Crosspoint Business Park<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr004Box" id="pnr004Box" onclick="pnrClick(this, 'pnr004', 3)" />Eastern Hills Mall<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr005Box" id="pnr005Box" onclick="pnrClick(this, 'pnr005', 4)" />Erie Community College South<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr006Box" id="pnr006Box" onclick="pnrClick(this, 'pnr006', 5)" />Highland Elementary School<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr007Box" id="pnr007Box" onclick="pnrClick(this, 'pnr007', 6)" />Holtz Drive<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr008Box" id="pnr008Box" onclick="pnrClick(this, 'pnr008', 7)" />LaSalle Station<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr009Box" id="pnr009Box" onclick="pnrClick(this, 'pnr009', 8)" />Main and Niagara City of Tonawanda<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr010Box" id="pnr010Box" onclick="pnrClick(this, 'pnr010', 9)" />Main and Union, Williamsville<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr011Box" id="pnr011Box" onclick="pnrClick(this, 'pnr011', 10)" />McKinley Mall<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr012Box" id="pnr012Box" onclick="pnrClick(this, 'pnr012', 11)" />North Boston<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr013Box" id="pnr013Box" onclick="pnrClick(this, 'pnr013', 12)" />Route 20A and 219<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr014Box" id="pnr014Box" onclick="pnrClick(this, 'pnr014', 13)" />University Station<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr015Box" id="pnr015Box" onclick="pnrClick(this, 'pnr015', 14)" />Village of Angola<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="pnr016Box" id="pnr016Box" onclick="pnrClick(this, 'pnr016', 15)" />West Seneca Municipal Parking
                </div>               
            
            </div>

            <div>
                <h2><input type="checkbox" checked="checked" name="transitBox" id="transitBox" onclick="catClick(this,'transit')" /> Transit Centers:</h2>
                <div class="itemsDiv" style="padding:0 0 0 20px;">
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="transit001Box" id="transit001Box" onclick="transitClick(this, 'transit001', 0)" />Main and Niagara City of Tonawanda<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="transit002Box" id="transit002Box" onclick="transitClick(this, 'transit002', 1)" />Portage Road<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="transit003Box" id="transit003Box" onclick="transitClick(this, 'transit003', 2)" />Lackawanna Victory<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="transit004Box" id="transit004Box" onclick="transitClick(this, 'transit004', 3)" />Niagara Falls Transportation Center<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="transit005Box" id="transit005Box" onclick="transitClick(this, 'transit005', 4)" />Southgate Plaza<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="transit006Box" id="transit006Box" onclick="transitClick(this, 'transit006', 5)" />Thruway Mall<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="transit007Box" id="transit007Box" onclick="transitClick(this, 'transit007', 6)" />Metropolitan Transportation Center<br />
                </div>                     
            </div>
            
            
            <div>
                <h2><input type="checkbox" checked="checked" name="carshareBox" id="carshareBox" onclick="catClick(this,'carshare')" /> Buffalo CarShare:</h2>
                <div class="itemsDiv" style="padding:0 0 0 20px;">
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare001Box" id="carshare001Box" onclick="carshareClick(this, 'carshare001', 0)" />Quaker Bonnet at Allen/Elmwood<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare002Box" id="carshare002Box" onclick="carshareClick(this, 'carshare002', 1)" />Allen/Medical Station<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare003Box" id="carshare003Box" onclick="carshareClick(this, 'carshare003', 2)" />BCS Office<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare004Box" id="carshare004Box" onclick="carshareClick(this, 'carshare004', 3)" />Brent Manor at 366 Elmwood<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare005Box" id="carshare005Box" onclick="carshareClick(this, 'carshare005', 4)" />St. Joe's/University Station<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare006Box" id="carshare006Box" onclick="carshareClick(this, 'carshare006', 5)" />271 Grant/Delavan<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare007Box" id="carshare007Box" onclick="carshareClick(this, 'carshare007', 6)" />Delavan/Jefferson Parking Ramp<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare008Box" id="carshare008Box" onclick="carshareClick(this, 'carshare008', 7)" />1272 Delaware/Auburn<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare009Box" id="carshare009Box" onclick="carshareClick(this, 'carshare009', 8)" />Joe's Service at Elmwood/Amherst<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare010Box" id="carshare010Box" onclick="carshareClick(this, 'carshare010', 9)" />Buffalo State Lot M-2<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare011Box" id="carshare011Box" onclick="carshareClick(this, 'carshare011', 10)" />Buffalo State Lot C<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare012Box" id="carshare012Box" onclick="carshareClick(this, 'carshare012', 11)" />The Mansion at 414 Delaware/Edward<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare013Box" id="carshare013Box" onclick="carshareClick(this, 'carshare013', 12)" />EV - Innovation Center<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare014Box" id="carshare014Box" onclick="carshareClick(this, 'carshare014', 13)" />Allen/Medical<br />
                    <input class="itemCheckbox" checked="checked" type="checkbox" name="carshare015Box" id="carshare015Box" onclick="carshareClick(this, 'carshare015', 14)" />490 Rhode Island St.<br />
                </div>                     
            </div>
        </div>  
    </div>
 
</body> 
</html>	

