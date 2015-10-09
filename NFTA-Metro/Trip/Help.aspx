<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Help.aspx.vb" Inherits="NFTA_Metro._Help"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="ctl00_Header"><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><meta name="copyright" content="Copyright (c) NFTA-Metro" />


<link rel="shortcut icon" href="/img/favicon.ico" type="image/x-icon" />
    <script>
      (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
      })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

      ga('create', 'UA-5984582-9', 'auto');
      ga('send', 'pageview');

    </script>
  
    
    <title>NFTA-Metro | Trip Planner Help</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail" />

    <style>
    body {	
        margin:10px 0 0 0;
        padding:0;
	    font-size:small;
	    font-family:Trebuchet, Arial, sans-serif;
	    color:#302f2f;
	    background:#fff ;
	    width:100%;
	    text-align:left;
	    font-size:62.5%; /* 16px × 62.5% = 10px */
	    font-size:1em;
	    line-height:1.7em;
    }
    h2 {color:#fff; background-color:#004990; padding:5px;}
    h3 {color:#000; }
    
        a.custBtn {
      vertical-align: baseline;
      margin: 0 10px 0 2px;
      outline: none;
      cursor: pointer;
      text-align: center;
      text-decoration: none;
      font: 14px Arial, Helvetica, sans-serif;
      font-weight: bold;
      padding: 4px 10px;
      color: #5089B8 !important;
      border: solid 1px #edc96e;
      background: #edc96e;
      background: -webkit-gradient(linear, left top, left bottom, from(#edc96e), to(#edc96e));
      background: -moz-linear-gradient(top, #edc96e, #edc96e);
      /* filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#00adee', endColorstr='#0078a5'); */
      -webkit-border-radius: .5em;
      -moz-border-radius: .5em;
      border-radius: .5em;
      -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.2);
      -moz-box-shadow: 0 1px 2px rgba(0,0,0,.2);
      box-shadow: 0 1px 2px rgba(0,0,0,.2);
      /* filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#00adee', endColorstr='#0078a5'); */
    }
    
    #masthead {float:left; width:100%;}
    #logo {float:left; width:30%; position:relative; left:20px;}
    #pageH {float:left; width:40%;text-align:center;}
    #Lang {float:left; width:30%;text-align:right; position:relative; top:14px; left:-50px;}
    .section {padding:20px;}
    </style>
</head>

<body>
    <form name="aspnetForm" method="post" action="help.aspx" id="aspnetForm">
 
    <div id="masthead">
        <div id="logo">
            <img src="/img/logo_trip_planner.gif" border="0">
        </div>
        <div id="pageH">
            <h1>Trip Planner Help</h1>
        </div>
        <div id="Lang">
            <!--<select name="selectLanguageHeader" id="selectLanguageHeader" size="1" class="" onchange=" switchlanguage(document.getElementById('selectLanguageHeader').value);">
                <option value="en">English</option>
                <option value="es-mx">Espa&ntilde;ol</option>
            </select>-->
            
             <div id="google_translate_element"></div>
                <script type="text/javascript" >
                    function googleTranslateElementInit() {
                        new google.translate.TranslateElement({
                            pageLanguage: 'en',
                            includedLanguages: 'en,es',
                            gaTrack: true,
                            gaId: 'UA-5984582-9',
                            layout: google.translate.TranslateElement.InlineLayout.VERTICAL
                        }, 'google_translate_element');
                    }
                </script>
                <script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>


        </div>
    </div>
    <br clear="all" />
    
    <p align="center">
    
        <a class="custBtn" href="#tripplanning">Trip Planning</a>&nbsp;&nbsp;&nbsp;
        <a class="custBtn" href="#routeschedules">Route Schedules</a>&nbsp;&nbsp;&nbsp;
        <a class="custBtn"  href="#nextdepartures">Next Departures & Stops</a>&nbsp;&nbsp;&nbsp;
        <a class="custBtn" href="#landmarks">Landmarks</a>
        
    </p>
 
    <h2><a name="tripplanning">Trip Planning</a></h2>
    <div class="section">
        <h3>Entering Locations </h3>
        <p>To create a trip plan, enter a starting location (origin) and an ending location (destination) either by typing in an address, intersection or selecting a location from the Landmarks list.</p>
        <h3>For best results</h3>
        <p>When entering an address, start entering the partial address, then select the correct address from the "type ahead" drop down box. This step automatically validates your location within the system.</p>
        <p>The Trip Planner recognizes most street intersections and addresses as well as many landmarks in Erie and Niagara Counties.</p>
        <h3>Guidelines</h3>
        <p>If you are having difficulty with your desired address, make sure you have utilized the guidelines below for entering your location. If you are still having trouble with Trip Planner identifying your desired address, please provide us feedback to analyze the issue and determine the best solution.  Email your feedback to <a href="mailto:info@nfta.com">info@nfta.com</a>.</p>
        <h3>Addresses</h3>
            <ul>
                <li>Enter an address or select a location on the map</li>
                <li>Leave out the city, state, or ZIP code along with the address. The Trip Planner shows city names as options if needed</li>
                <li>You don't need to type in St., Street, Ave., Avenue, or similar street types unless they precede the street name. The Trip Planner shows possible alternatives as options if needed. (Examples: type 110 Jones instead of 110 Jones Boulevard</li>
                <li>You can leave out the street direction unless it is part of a street name. (Example: type 1000 Main instead of 1000 S Main)</li>
                <li>Some streets and addresses are unknown to the Trip Planner. You may need to enter another nearby location, such as an intersection or a landmark</li>
                <li>You don't need to enter suite or apartment numbers, just the house number and street name (Example: type in 401 Broadway instead of 401 Broadway Avenue Suite 800)</li>
            </ul>
        <h3>Intersections</h3>
            <ul>
                <li>Enter an intersection in the query form or select a location on the map</li>
                <li>Use the '&', '@' or 'and' between two street names to show an intersection. (Examples: 1st & B, Main @ Church, Main and Allen)</li>
                <li>Leave out the city, state, and ZIP code. The Trip Planner shows city names as options if needed</li>
                <li>You don't need to type in St., Street, Ave., Avenue, or similar street types unless they precede the street name. The Trip Planner shows possible alternatives as options if needed. (Examples: type 110 Jones instead of 110 Jones Boulevard.)</li>
                <li>You can  leave out the street direction unless it  is part of a street name. (Example: type 1000 Main instead of 1000 S Main. But type 1000 West Viewmont for 1000 West Viewmont Way W.)</li>
                <li>Some streets are unknown to the Trip Planner. You may need to enter another nearby intersection or a landmark or address</li>
            </ul>
        <h3>Landmarks</h3>
        <p>The Trip Planner recognizes many landmarks of these types:</p>
        <table border="0" cellpadding="3">
            <tr>
                <td align="center"><b>Landmark type</b></td>
                <td align="center"><b>Examples</b></td>
            </tr>
            <tr>
                <td>Bank</td>
                <td>Bank of America, M and T Crosspoint</td>
            </tr>
            <tr>
                <td>Business / Trade Schools</td>
                <td>Boces Erie 1, ITT Technical Institute, ETS Staffing</td>
            </tr>
            <tr>
                <td>Transportation Facilities</td>
                <td>Babcock Station, Greyhound Bus Lines, Frontier Station</td>
            </tr>
            <tr>
                <td>Schools</td>
                <td>Buffalo Seminary, Burgard High School, City Honors</td>
            </tr>
            <tr>
                <td>Stadium or Arena</td>
                <td>HarborCenter, Buffalo Bills, Coca Cola Field</td>
            </tr>
            <tr>
                <td>Hospitals / Medical Facilities</td>
                <td>Buffalo General Hospital, ECMC, Kenmore Mercy</td>
            </tr>
            <tr>
                <td>Transit Centers</td>
                <td>Appletree Business Park, Main and Niagara, Portage Road Transportation Center</td>
            </tr>
            <tr>
                <td>Park and Ride Lots</td>
                <td>Thruway Mall Park and Ride, Lasalle Station Park and Ride</td>
            </tr>
        </table>
        <ul>
            <li>Type a landmark name to enter it, or select a location on the map. Many landmarks have a short name or abbreviation that is easier to enter</li>
            <li>The Trip Planner offers a list of choices if there is more than one landmark with a similar name</li>
            <li>You can safely leave out the city, state, ZIP code, and punctuation</li>
            <li>You may need to enter an address or an intersection for some landmarks</li>
            <li>It is not possible to maintain some other types of landmark categories that might seem like a good idea, such as grocery stores, convenience stores and others</li>
            <li>The Trip Planner recognizes major landmarks in the categories shown above, but may be missing some. Use and address or intersection if necessary, and use the comments button or feedback link to suggest adding a landmark</li>
        </ul>
        <h3>Other Query Options</h3>
        <p>In addition to entering the locations of your point of origin and your destination, select the day and time you wish to travel and other criteria, including those listed under 'Advanced Options'.</p>
        <div align="center"><a class="custBtn" href="http://tripplanner.nfta.com">Back to Trip Planner</a></div>
    </div>
    <h2><a name="routeschedules">Route Schedules</a></h2>
    <div class="section">
        
        <h3>To view a route on the map</h3>
            <ul>
                <li>Select a date from the calendar</li>
                <li>Select a route from the list</li>
            </ul>
        <h3>To view route schedules</h3>
        <ul>
            <li>After completing the above steps, select Get Schedule</li>
        </ul>
        <div align="center"><a class="custBtn" href="http://tripplanner.nfta.com">Back to Trip Planner</a></div>
    </div>
    <h2><a name="nextdepartures">Next Departures & Stops</a></h2>
    <div class="section">
        <h3>To find a transit stop and the next departures of service using Address or Landmark</h3>
            <ul>
                <li>Select a date from the calendar</li>
                <li>Select a time</li>
                <li>Select a location by entering an address or landmark</li>
                <li>Select Find Nearby Stops</li>
                <li>Select a stop</li>
                <li>Select Get Next Departures</li>
            </ul>
        <h3>How to find Stop IDs</h3>
        <p>Find Stop IDs by entering an address and selecting the correct stop or selecting a point on the map to find a stop.</p>
        <h3>To find the next departures of service using Stop ID</h3>
        <ul>
            <li>Select a date from the calendar</li>
            <li>Select a time</li>
            <li>Enter a Stop ID</li>
            <li>Select Get Next Departures</li>
        </ul>
        <div align="center"><a class="custBtn" href="http://tripplanner.nfta.com">Back to Trip Planner</a></div>
    </div>
    <h2><a name="landmarks">Landmarks</a></h2>
    <div class="section">    
        
        <h3>To find a landmark</h3>
        <ul>
            <li>Select a landmark category</li>
            <li>Then select a Popular Location from within that category</li>
            <li>Select specific locations on the map (desktop Trip Planner only)</li>
        </ul>    
        <div align="center"><a class="custBtn" href="http://tripplanner.nfta.com">Back to Trip Planner</a></div>
    </div>
    
    <br clear="all" />
    <div id="footAddress" style="padding:10px 0; background:#302f2f; color:#fff; text-align:center;">
    
       Niagara Frontier Transportation Authority    &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;   181 Ellicott Street Buffalo, New York 14203   &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;   716-855-7300    &nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;   <img style="position:relative; top:2px; left:-3px" src="/img/tty-yellow.png" width="15" height="17" alt="TTY/Relay 711 or 800-662-1220" />TTY/Relay 711 or 800-662-1220
    </div>  
    <script type ="text/javascript"  src="/js/domroll.js"></script>  
</form>
</body>
</html>
