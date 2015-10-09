<%
	'strDisplayTime and strDisplayTimeLater Replace printTime and printTimeLater found below.
	
	dim xHour, xAMPM, xMin, strDisplayTime
	'xDate = (FormatDateTime(date(),vblongdate))
	xHour = (Hour(Now)+2) 'Add 2 hours because the server time is central and one more for non DST change
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