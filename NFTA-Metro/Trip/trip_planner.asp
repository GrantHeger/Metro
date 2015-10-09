<%
	
	Response.redirect "http://tripplanner.nfta.com"
	
	
	'strDisplayTime and strDisplayTimeLater Replace printTime and printTimeLater found below.
	
	dim xHour, xAMPM, xMin, strDisplayTime
	'xDate = (FormatDateTime(date(),vblongdate))
	'xHour = (Hour(Now)+2) 'Add 2 hours because the server time is central and one more for non DST change
        xHour = (Hour(Now)) 
	If xHour > 12 Then
		'convet from military time to 12hr time
		xHour = xHour - 12 
		If xHour = 12 Then
			xAMPM = "AM"
		Else
			xAMPM = "PM"
		End If
	Else
		'display the time
		xHour = xHour
		If xHour = 12 Then
			xAMPM = "PM"
		Else
			xAMPM = "AM"
		End If
	End If
	xMin = Minute(Time)
		If xMin < 10 Then
		xMin = "0" & xMin
	End If
	
	strDisplayTime =  xHour & ":" & xMin & " " & xAMPM
	
	dim xHourL, xAMPML, xMinL, strDisplayTimeLater
	'xDate = (FormatDateTime(date(),vblongdate))
	xHourL = (Hour(Now)+5) 'Add 2 hours because the server time is central and one more for non DST change, + 2 for range
	If xHourL > 12 Then
		'convet from military time to 12hr time
		xHourL = xHourL - 12 
		If xHourL = 12 Then
			xAMPML = "AM"
		Else
			xAMPML = "PM"
		End If
	Else
		'display the time
		xHourL = xHourL
		If xHourL = 12 Then
			xAMPML = "PM"
		Else
			xAMPML = "AM"
		End If
	End If
	xMinL = Minute(Time)
		If xMinL < 10 Then
		xMinL = "0" & xMinL
	End If
	
	strDisplayTimeLater =  xHourL & ":" & xMinL & " " & xAMPML
	

	function printDate(rawDate)
		Dim myDay
		Dim myMonth
		Dim myYear
		
		myDay = day(rawDate)
		If Len(myDay)=1 Then myDay="0" & myDay
		myMonth = month(rawDate) 
		If Len(myMonth)=1 Then myMonth="0" & myMonth
		'myYear = year(rawDate)
		myYear = right(year(rawDate),2)
		
		printDate = myMonth & "/" & myDay & "/" & myYear
		'printDate = month(rawDate) & "/" & day(rawDate) & "/" & right(year(rawDate),2)
	end function
	
	function printTime(rawTime)
		rawHour = hour(rawTime)
		rawMinute = minute(rawTime)
		if rawHour > 12 then
			rawHour = rawHour - 12
			ampm = "PM"
		else
			if Len(rawHour)=1 Then rawHour="0" & rawHour			
			ampm = "AM"
		end if
		if Len(rawMinute)=1 Then rawMinute="0" & rawMinute
		printTime = rawHour & ":" & rawMinute & " " & ampm
	end function
	
	function printTimeLater(rawTime)
		plusTwo = CDate("2:00")
		rawTime = rawTime + plusTwo
		rawHour = hour(rawTime)
		rawMinute = minute(rawTime)
		if rawHour > 12 then
			rawHour = rawHour - 12
			ampm = "PM"
		else
			ampm = "AM"
		end if
		printTimeLater = rawHour & ":" & rawMinute & " " & ampm
	end function
	function convertDate(originalDate)
		Dim VBDate
		VBDate =  DateAdd("s", originalDate , "1/1/1970")
		VBDate = DateAdd("h", -5, VBDate) ' convert to your time zone:  use -5 for Eastern, -8 pacific etc.
		
		'Now that we have the time.. we have to check daylight savings time
		
		Dim StartDaylight
		Dim EndDaylight
		
		' get the last day of March by subtracting one day from 4/1
		StartDaylight = DateAdd("d", -1, DateSerial(Year(VBDate), 4, 1))
		
		' now skip to the next Sunday
		StartDaylight = DateAdd("d", 5 - WeekDay(StartDaylight), StartDaylight)
		StartDaylight = DateAdd("h", 2, StartDaylight)
		EndDaylight = DateSerial(Year(VBDate), 11, 1)
		
		' back up to the previous Sunday
		EndDaylight = DateAdd("d", -5 + WeekDay(EndDaylight), EndDaylight)
		EndDaylight = DateAdd("h", 1, EndDaylight)
		
		If (VBDate >= StartDaylight And VBDate < EndDaylight) Then
		VBDate = DateAdd("h", 1, VBDate)
		End If
		
		'Display your time
		convertDate = VBDate
	end function




%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>NFTA - Niagara Frontier Transportation Authority - Metro Trip Planner</title>
<meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
<meta name="copyright" content="Copyright (c) 1997-Present Niagara Frontier Transportation Authority" />
<link rel="stylesheet" type="text/css" media="screen" href="css/cssScreen.css" />
<link rel="stylesheet" type="text/css" media="print" href="css/cssPrint.css" />
<link rel="stylesheet" type="text/css" media="all" href="/css/cssTripPlanner.css" />

<link rel="shortcut icon" href="http://www.niagarafrontiertransportationauthority.com/img/logos/favicons/favicon-metro.ico" type="image/x-icon" />
<link rel="shortcut icon" href="http://www.niagarafrontiertransportationauthority.com/img/logos/favicons/icon-metro.png" type="image/x-icon" />

<!--[if lt IE 7]>
<script defer type="text/javascript" src="/js/pngfix.js"></script>
<![endif]-->
</head>
<body id="pageTripPlanner">
<form name="itin" id="itin_id" method="post" action="http://metrotrip.nfta.com/cgi-bin/itin.pl">
<input type="hidden" name="action" value="entry" />
<input type="hidden" name="resptype" value="U" /> <!-- F: Formatted, U: Unformatted -->
	<div id="all">
			<img src="../img/logo_trip_planner.gif" vspace="10" border="0" />
    
            <!--<p>These tools can help you figure out the best way to use your bus and rail system, based on location. All you need to know is where you want to go.</p>    
                <p>These features are based on the needs of customers like you. The more we 
                  know about your needs, the better we can make these features work. If something 
                  is not performing the way you had hoped or you have any ideas for its improvement, 
                  let us know by e-mailing: <img src="img/addressinfo.gif" width="87" height="15" alt="" /></p>
                  -->
            
         	<table width="100%" cellpadding="1" id="trip-planner-menu">
            	<tr>
                	<td align="left"valign="middle"><a href="trip_planner.asp#plan"><img src="../img/button_trip_plan.gif" alt="Plan by Location" hspace="10" vspace="0" border="0" align="absmiddle" />Plan a trip to and from a location</a></td>
                </tr>
                <tr>
                	<td align="left"valign="middle"><a href="trip_planner_schedule.asp#schedule"><img src="../img/button_trip_schedule.gif" alt="Get Schedule Information" hspace="10" vspace="0" border="0" align="absmiddle" />Get schedule information about a location</a></td>
                </tr>
                <tr>
                	<td align="left"><a href="trip_planner_services.asp#services"><img src="../img/button_trip_service.gif" alt="Find Services" hspace="10" vspace="0" border="0" align="absmiddle" />Find services around a location</a></td>
                </tr>
                <tr>
                    <td align="left"><a href="trip_planner_stops.asp#stops"><img src="../img/button_trip_stops.gif" alt="See Closest Stops" hspace="10" vspace="0" border="0" align="absmiddle" />See the closest stops near a location</a></td>
             	</tr>
         	</table>
           
            
            <hr />
            
            <!-- BEGIN TRIP PLANNER -->
            <div id="trip-planner">
            
            <a name="plan" id="plan"></a><h3>Plan a trip to and from a location</h3>
            
            <fieldset class="fieldset1">
            <legend class="legend"><img src="../img/legend1.gif" alt="1" border="0" /></legend>
                <p class="smalltext">Begin by entering where you are departing from and where you are going to. You can use addresses (ex. 181 Ellicott St), intersections (ex. Ellicott & N Division) or landmark names (ex. Shea's Buffalo Theatre).<br />
                <em>Tip: use '&amp;' instead of 'and'.</em></p>
                
                <p><strong>Departing From:</strong><br />
                <table cellpadding="1">
            		<tr>
                        <td align="left"valign="top"><span class="smalltext">(Address, Intersection, or Landmark)</span><br />
                        	<input type="text" name="Orig" size="35" maxlength="80" value="<%= Request.QueryString("From") %>" class="textfieldmedium" /></td>
                		<td align="left" valign="middle"><strong> &nbsp;&nbsp; -or- &nbsp;&nbsp; </strong>
                        </td>
                        <td align="left"valign="top">(Popular Locations)<br />
                            <select name="Orig1">
                            <!-- #include file="inc_tp_popular_locations.asp" -->
                            </select>
                        </td>
                  	</tr>
              	</table>
                </p>
                
                <hr>
                <p><strong>Going To:</strong><br />
                <table cellpadding="1">
            		<tr>
                        <td align="left"valign="top"><span class="smalltext">(Address, Intersection, or Landmark)</span><br />
                        	<input type="text" name="Dest" value="<%= Request.QueryString("To") %>" size="35" maxlength="80" class="textfieldmedium" /></td>
                		<td align="left" valign="middle"><strong> &nbsp;&nbsp; -or- &nbsp;&nbsp; </strong>
                        </td>
                        <td align="left"valign="top">(Popular Locations)<br />
                            <select name="Dest1">
							<!-- #include file="inc_tp_popular_locations.asp" -->
                            </select>
                        </td>
                  	</tr>
              	</table>
                </p>
           
            </fieldset>
            
            <fieldset class="fieldset2">
            <legend><img src="../img/legend2.gif" alt="2" border="0" /></legend>
                <p class="smalltext">Next, enter the date and time of your trip.</p>
                
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr align="left" valign="top">
                        <td>
                            <p><strong>Date:</strong><br />
                            <input type="text" name="Date" value="<%= printDate(date) %>" size="8" maxlength="10" class="textfieldsmall" /> (MM/DD/YY)&nbsp;&nbsp;</p>
                        </td>
                        <td>
                        	<p><strong>Time:</strong><br />
                            <input type="radio" checked="checked" name="Arr" value="D" /> Depart At<br />
                            <input type="radio" name="Arr" value="A" /> Arrive By
                       </td>
                       <td>
                            <br />&nbsp;&nbsp;&nbsp;<input type="text" name="Time" value="<%= strDisplayTime %>" size="8" maxlength="8" class="textfieldxsmall" /> (HH:MM AM/PM)
                        </td>
                    </tr>
                </table>
            </fieldset>
            
            <fieldset class="fieldset3">
            <legend><img src="../img/legend3.gif" alt="3" border="0" /></legend>
            <p class="smalltext">Decide how far you want to walk. Increasing the Max Walking Distance can increase the number of trips that might work for you.</p>
            <p><strong>Max Walking Distance:</strong> 
            <select name="Walk" size="1">
                <option value="0.25"> 1/4 mile</option>
                <option selected="selected" value="0.50"> 1/2 mile</option>
                <option value="0.75"> 3/4 mile</option>
                <option value="0.9999"> 1 mile</option>
            </select>
            </p>
            </fieldset>
            
            <fieldset class="fieldset4">
            <legend><img src="../img/legend4.gif" alt="4" border="0" /></legend>
            <p class="smalltext">Customize your trip by telling us what you'd like to minimize (time, transfers, or walking). You should also tell us if this needs to be an accessible trip.</p>
            <p><strong>Minimize:</strong><br />
            <input type="radio" checked="checked" name="Min" value="T" />Time<br />
            <input type="radio" name="Min" value="X" />Transfers<br />
            <input type="radio" name="Min" value="W" />Walking</p>
            
            <!--<p><input type="checkbox" name="Atr" value="Y" /> Accessible Trip Required</p>-->
            </fieldset>
            
            <p align="center"><input type="submit" value="Plan Trip" name="it_req" class="tripbutton" />&nbsp;&nbsp;&nbsp;<input type="reset" value="Reset" name="reset" class="resetbutton" /></p>
            
            </div>
                
 
    </div>  
    <div id="footer" style="width:100%;">	
        <div style="padding:10px 0; margin:0 auto; text-align:center; font-size:12px;">
            Copyright &copy; 1997-2011 Niagara Frontier Transportation Authority (NFTA)<br />181 Ellicott Street Buffalo, New York 14203&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;716.855.7300&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;TDD / Relay 711 or 800.622.1220&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="http://metro.nfta.com/Contact/">Contact NFTA-Metro</a>
        </div>
        
    </div>  
	<script type ="text/javascript"  src="../js/domroll.js"></script>  
</form>
</body>
</html>