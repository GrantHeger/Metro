Imports System.Text
Imports NFTA_RouteDB
Imports System.Data.Linq

' This class processes the gtfs routes data
' into script tags containing the arrays needed
' in the markup as well as the markup tags
Public Class RouteBuilder
    Private Shared _instance As RouteBuilder = Nothing

    Private db As NFTA_RouteDB.NFTARoutesDBDataContext
    Private _trainMarkup As String = ""
    Private _markup As String = ""
    Private _script As String = ""
    Private _trainIndex As Integer

    Private Sub New()
        db = New NFTA_RouteDB.NFTARoutesDBDataContext
        LoadRouteArrayScript()
    End Sub

    Public Shared ReadOnly Property GetInstance() As RouteBuilder
        Get
            If _instance Is Nothing Then
                _instance = New RouteBuilder
            End If
            Return _instance
        End Get
    End Property

    Private Sub LoadRouteArrayScript()
        Dim script As New StringBuilder
        Dim markup As New StringBuilder
        Dim trainMarkup As New StringBuilder

        script.Append("<script type=""text/javascript"">" & Environment.NewLine)
        script.Append(Environment.NewLine)
        script.Append(" var arrInboundRoutes = []; " & Environment.NewLine)
        script.Append(" var arrOutboundRoutes = []; " & Environment.NewLine)

        script.Append(" var arrRouteCenterPoints = []; " & Environment.NewLine)
        script.Append(" var arrTrainCenterPoints = []; " & Environment.NewLine)
        script.Append(" var arrRouteMarkers = []; " & Environment.NewLine) ' New array to display a symbol in the middle of a route
        script.Append(" var arrTrains = []; " & Environment.NewLine)
        script.Append(" var arrTrainRouteMarkers = []; " & Environment.NewLine)
        script.Append(" var highestZIndex = 1000; " & Environment.NewLine)
        script.Append(" var oldZIndex = 0;" & Environment.NewLine)
        script.Append(" var newZIndex = 0;" & Environment.NewLine)
        script.Append(" var styles = [  " & Environment.NewLine)
        script.Append("     { " & Environment.NewLine)
        'script.Append("    featureType: ""road"",  " & Environment.NewLine)
        script.Append("     stylers: [ " & Environment.NewLine)
        script.Append("     { saturation: -100 }  " & Environment.NewLine)
        script.Append("     ] " & Environment.NewLine)
        script.Append("     },{ " & Environment.NewLine)
        script.Append("     featureType: ""water"", " & Environment.NewLine)
        script.Append("     stylers: [ " & Environment.NewLine)
        script.Append("     { hue: ""#0066ff"" },  " & Environment.NewLine)
        script.Append("     { saturation: 92 }    " & Environment.NewLine)
        script.Append("     ] " & Environment.NewLine)
        script.Append("     } " & Environment.NewLine)
        script.Append("     ]; " & Environment.NewLine)


        AddInitializationCode(script)

        Dim trips As List(Of gtfs_trip)
        '--****************************** TO DO ************************************-- 
        '
        'stop using gtfs_service table
        'use Route table - category_Id (2 = rail)
        ' or use Route_category table
        ' I hard coded serviceID = 3  (Bus) 
        '
        '--*************************************************************************-- 
        'Dim serviceID = db.gtfs_services.Where(Function(s As gtfs_service) s.Mode = "Bus").Select(Function(i As gtfs_service) i.Service_Id).SingleOrDefault
        Dim serviceID = 3

        'Trips from gtfs_trips table where service_id = 3 (use 'bus' value from gtfs_service table)
        trips = db.gtfs_trips.Where(Function(t As gtfs_trip) t.service_id = serviceID).ToList

        Dim routeIdsWithServiceIds = trips.Select(Function(i As gtfs_trip) i.route_id).Distinct.ToList

        'check to see if Map in Route table is set to true
        Dim routeIdsThatAreShowOnMap = db.Routes.Where(Function(m As NFTA_RouteDB.Route) m.Map = True).Select(Function(i As NFTA_RouteDB.Route) i.RouteNumber).ToList

        'check to see if category_id in Route table is 2 or rail
        'Dim routesThatAreNotRail = db.Routes.Where(Function(r As NFTA_RouteDB.Route) r.Category_Id <> 2).Select(Function(i As NFTA_RouteDB.Route) i.RouteNumber).ToList


        ' First proccess all but route 45 (rail), all with service id's of bus, all that map are set to true
        ' (See below for 45 Rail processing  - About line 265)
        Dim routeIDs As New List(Of Integer)
        'routeIDs = db.gtfs_routes.Where(Function(n As gtfs_route) n.route_short_name <> 45 And routeIdsWithServiceIds.Contains(n.route_id) And routeIdsThatAreShowOnMap.Contains(n.route_id)).OrderBy(Function(o As gtfs_route) o.route_short_name).Select(Function(r As gtfs_route) r.route_id).ToList
        routeIDs = db.gtfs_routes.Where(Function(n As gtfs_route) n.route_short_name <> 45 And routeIdsWithServiceIds.Contains(n.route_id) And routeIdsThatAreShowOnMap.Contains(n.route_id)).OrderBy(Function(o As gtfs_route) o.route_short_name).Select(Function(r As gtfs_route) r.route_id).ToList

       

        Dim routeCounter As Integer = 1
        Dim inboundShapes As List(Of NFTA_RouteDB.gtfs_shape)
        Dim outboundShapes As List(Of NFTA_RouteDB.gtfs_shape)
        For Each rid In routeIDs

            Dim bigRoute As NFTA_RouteDB.Route = db.Routes.Where(Function(r As NFTA_RouteDB.Route) r.RouteNumber = rid.ToString).SingleOrDefault

            Dim routeID As Integer = rid
            Dim textColor As String = bigRoute.TextColor
            Dim lineColor As String = bigRoute.RouteColor

            Dim shapeTakeIn As Integer
            If bigRoute.Shape_In > 1 Then
                shapeTakeIn = bigRoute.Shape_In - 1
            Else
                shapeTakeIn = 0
            End If
            Dim shapeTakeOut As Integer = bigRoute.Shape_Out - 1
            If bigRoute.Shape_Out > 1 Then
                shapeTakeOut = bigRoute.Shape_Out - 1

            Else
                shapeTakeOut = 0
            End If

            Dim inboundTrip = trips.Where(Function(i As gtfs_trip) i.direction_id = 0 And i.route_id = routeID).Skip(shapeTakeIn).Take(1).SingleOrDefault
            Dim outboundTrip = trips.Where(Function(i As gtfs_trip) i.direction_id = 1 And i.route_id = routeID).Skip(shapeTakeOut).Take(1).SingleOrDefault

            ' set up our array
            script.Append(" var route" & routeCounter.ToString & "InboundCoords = [" & Environment.NewLine)
            If Not inboundTrip Is Nothing Then
                inboundShapes = db.gtfs_shapes.Where(Function(s As gtfs_shape) s.shape_id = inboundTrip.shape_id).OrderBy(Function(d As gtfs_shape) d.shape_pt_sequence).ToList
                Dim first As Boolean = True
                For Each inboundShape As gtfs_shape In inboundShapes
                    If first Then
                        script.Append(" new google.maps.LatLng(" & inboundShape.shape_pt_lat.ToString & ", " & inboundShape.shape_pt_lon.ToString & ") " & Environment.NewLine)
                    Else
                        script.Append(", new google.maps.LatLng(" & inboundShape.shape_pt_lat.ToString & ", " & inboundShape.shape_pt_lon.ToString & ") " & Environment.NewLine)
                    End If
                    first = False
                Next
            End If

            script.Append("]; ")

            script.Append(" var route" & routeCounter.ToString & "OutboundCoords = [" & Environment.NewLine)
            If Not outboundTrip Is Nothing Then
                outboundShapes = db.gtfs_shapes.Where(Function(s As gtfs_shape) s.shape_id = outboundTrip.shape_id).OrderBy(Function(d As gtfs_shape) d.shape_pt_sequence).ToList

                Dim first As Boolean = True
                For Each outboundShape As gtfs_shape In outboundShapes
                    If first Then
                        script.Append(" new google.maps.LatLng(" & outboundShape.shape_pt_lat.ToString & ", " & outboundShape.shape_pt_lon.ToString & ") " & Environment.NewLine)
                    Else
                        script.Append(", new google.maps.LatLng(" & outboundShape.shape_pt_lat.ToString & ", " & outboundShape.shape_pt_lon.ToString & ") " & Environment.NewLine)
                    End If
                    first = False
                Next
            End If

            script.Append("]; ")

            ' Now we have inbound and outbound shapes regardless of their existings (can be empty)
            Dim middleIndex As Integer

            If Not inboundTrip Is Nothing Then
                inboundShapes = db.gtfs_shapes.Where(Function(s As gtfs_shape) s.shape_id = inboundTrip.shape_id).OrderBy(Function(d As gtfs_shape) d.shape_pt_sequence).ToList
                middleIndex = inboundShapes.Count / 2
                script.Append(" arrRouteCenterPoints[" & (routeCounter - 1).ToString & "] = new google.maps.LatLng(" & inboundShapes(middleIndex).shape_pt_lat.ToString & ", " & inboundShapes(middleIndex).shape_pt_lon.ToString & ");")

            Else
                outboundShapes = db.gtfs_shapes.Where(Function(s As gtfs_shape) s.shape_id = outboundTrip.shape_id).OrderBy(Function(d As gtfs_shape) d.shape_pt_sequence).ToList
                middleIndex = outboundShapes.Count / 2
                script.Append(" arrRouteCenterPoints[" & (routeCounter - 1).ToString & "] = new google.maps.LatLng(" & inboundShapes(middleIndex).shape_pt_lat.ToString & ", " & inboundShapes(middleIndex).shape_pt_lon.ToString & ");")
            End If


            script.Append("var routeMarker" & routeCounter.ToString & " = new StyledMarker({" & Environment.NewLine)
            script.Append(" styleIcon: new StyledIcon(StyledIconTypes.BUBBLE,{color:'#" & lineColor & "', text:'" & bigRoute.RouteNumber.ToString & " - " & bigRoute.RouteName & "', fore:'#" & textColor & "'}), ")
            script.Append(" position: new google.maps.LatLng(" & inboundShapes(middleIndex).shape_pt_lat.ToString & ", " & inboundShapes(middleIndex).shape_pt_lon & ")," & Environment.NewLine)
            script.Append(" map: map, " & Environment.NewLine)
            script.Append(" title: '" & bigRoute.RouteNumber.ToString & " - " & bigRoute.RouteName & "'" & Environment.NewLine)
            script.Append("});")

            script.Append(" routeMarker" & routeCounter.ToString & ".setVisible(true);")
            script.Append(" arrRouteMarkers[" & (routeCounter - 1).ToString & "] = routeMarker" & routeCounter.ToString & "; " & Environment.NewLine)

            script.Append(" var routeInbound" & routeCounter.ToString & " = new google.maps.Polyline({" & Environment.NewLine)
            script.Append(" path: route" & routeCounter.ToString & "InboundCoords, " & Environment.NewLine)
            script.Append(" strokeColor: '#")

            script.Append(lineColor & "', " & Environment.NewLine)
            script.Append(" strokeOpacity: 1.0, " & Environment.NewLine)
            script.Append(" strokeWeight: 6 }); " & Environment.NewLine)
            script.Append(Environment.NewLine)
            script.Append(Environment.NewLine)

            script.Append(" var routeOutbound" & routeCounter.ToString & " = new google.maps.Polyline({" & Environment.NewLine)
            script.Append(" path: route" & routeCounter.ToString & "OutboundCoords, " & Environment.NewLine)
            script.Append(" strokeColor: '#")

            script.Append(lineColor & "', " & Environment.NewLine)
            script.Append(" strokeOpacity: 1.0, " & Environment.NewLine)
            script.Append(" strokeWeight: 6 }); " & Environment.NewLine)
            script.Append(Environment.NewLine)
            script.Append(Environment.NewLine)

            script.Append(" routeOutbound" & routeCounter.ToString & ".setVisible(true);" & Environment.NewLine)
            script.Append(" routeOutbound" & routeCounter.ToString & ".setMap(map);" & Environment.NewLine)
            script.Append(" routeInbound" & routeCounter.ToString & ".setVisible(true);" & Environment.NewLine)
            script.Append(" routeInbound" & routeCounter.ToString & ".setMap(map);" & Environment.NewLine)

            script.Append(" arrInboundRoutes[" & (routeCounter - 1).ToString & "] = routeInbound" & routeCounter.ToString & "; " & Environment.NewLine)
            script.Append(" arrOutboundRoutes[" & (routeCounter - 1).ToString & "] = routeOutbound" & routeCounter.ToString & "; " & Environment.NewLine)

            script.Append(" var infowindow" & routeCounter.ToString & " = new google.maps.InfoWindow({" & Environment.NewLine)
            Dim viewMapSchedule As String = "<a href=""/Routes/Route.aspx?rt=" & bigRoute.RouteNumber.ToString & """ target=""_blank"">Route Information</a>"

            script.Append(" content: '" & bigRoute.RouteNumber.ToString & " - " & bigRoute.RouteName & "<br />" & viewMapSchedule & "'" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            script.Append("google.maps.event.addListener(routeMarker" & routeCounter.ToString & ", ""click"", function(event) {" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".setPosition(event.latLng);" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".open(map);" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            script.Append("google.maps.event.addListener(routeMarker" & routeCounter.ToString & ", ""mouseover"", function(event) {" & Environment.NewLine)
            script.Append(" highestZIndex = highestZIndex + 1;" & Environment.NewLine)
            script.Append(" arrRouteMarkers[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrOutboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrInboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append("});")

            script.Append("google.maps.event.addListener(routeInbound" & routeCounter.ToString & ", ""click"", function(event) {" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".setPosition(event.latLng);" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".open(map);" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            script.Append("google.maps.event.addListener(routeInbound" & routeCounter.ToString & ", ""mouseover"", function(event) {" & Environment.NewLine)
            script.Append(" highestZIndex = highestZIndex + 1;" & Environment.NewLine)
            script.Append(" arrRouteMarkers[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrInboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrOutboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            script.Append("google.maps.event.addListener(routeOutbound" & routeCounter.ToString & ", ""click"", function(event) {" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".setPosition(event.latLng);" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".open(map);" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            script.Append("google.maps.event.addListener(routeOutbound" & routeCounter.ToString & ", ""mouseover"", function(event) {" & Environment.NewLine)
            script.Append(" highestZIndex = highestZIndex + 1;" & Environment.NewLine)
            script.Append(" arrRouteMarkers[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrInboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrOutboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            script.Append("google.maps.event.addListener(routeInbound" & routeCounter.ToString & ", ""click"", function(event) {" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".setPosition(event.latLng);" & Environment.NewLine)
            script.Append(" infowindow" & routeCounter.ToString & ".open(map);" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            script.Append("google.maps.event.addListener(routeOutbound" & routeCounter.ToString & ", ""mouseover"", function(event) {" & Environment.NewLine)
            script.Append(" highestZIndex = highestZIndex + 1;" & Environment.NewLine)
            script.Append(" arrRouteMarkers[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrInboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrOutboundRoutes[" & (routeCounter - 1).ToString & "].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            ' swap out below to precheck the metro routes
            markup.Append("<div style='float:left;'><input class=""itemCheckbox"" type=""checkbox"" name=""route" & routeCounter.ToString & "Box"" id=route" & routeCounter.ToString & "Box"" checked=""checked"" onclick=""routeClick(this, 'route" & routeCounter.ToString & "Box', " & (routeCounter - 1).ToString & ")"" /> " & bigRoute.RouteNumber.ToString & " - " & bigRoute.RouteName & " </div><div style='float:left; background:#" & lineColor & "; margin:10px 10px 0px 10px; height:5px; width:20px;'> &nbsp;&nbsp;&nbsp; </div><br clear='all' />")
            'markup.Append("<div style='float:left;'><input class=""itemCheckbox"" type=""checkbox"" name=""route" & routeCounter.ToString & "Box"" id=route" & routeCounter.ToString & "Box"" onclick=""routeClick(this, 'route" & routeCounter.ToString & "Box', " & (routeCounter - 1).ToString & ")"" /> " & bigRoute.RouteNumber.ToString & " - " & bigRoute.RouteName & " </div><div style='float:left; background:#" & lineColor & "; margin:10px; height:5px; width:20px;'> &nbsp;&nbsp;&nbsp; </div><br clear='all' />")


            routeCounter = routeCounter + 1

        Next


        ' Now process route 45!

        'RouteNumber is a string.  Need to use RouteID (in this case 45 Rail is RouteID = 1)
        'Dim trainRoute = db.Routes.Where(Function(r As NFTA_RouteDB.Route) r.RouteNumber = 45).SingleOrDefault
        Dim trainRoute = db.Routes.Where(Function(r As NFTA_RouteDB.Route) r.RouteId = 1).SingleOrDefault

        If (Not trainRoute Is Nothing) Then

            ' Get rail stations
            Dim railStations = db.RailStations.ToList


            script.Append(Environment.NewLine)
            script.Append(Environment.NewLine)
            script.Append(" var trainCoords =[")

            Dim textColor As String = trainRoute.TextColor
            Dim lineColor As String = trainRoute.RouteColor
            ' set up our array
            'script.Append(" var route" & routeCounter.ToString & "Coords = [" & Environment.NewLine)

            ' route info
            'Dim route = db.gtfs_routes.Where(Function(r As gtfs_route) r.route_id = trainRoute.route_id).SingleOrDefault
            'Dim color = db.gtfs_colors.Where(Function(c As gtfs_color) c.color_id = trainRoute.route_color).SingleOrDefault
            ' Now we are requerying trips
            ' Get first shape ID of directionid 0 for inbound
            trips = db.gtfs_trips.Where(Function(f As gtfs_trip) f.route_id = trainRoute.RouteNumber).ToList
            Dim inboundTrip = trips.Where(Function(i As gtfs_trip) i.direction_id = 0 And i.route_id = trainRoute.RouteNumber).Take(1).SingleOrDefault
            inboundShapes = db.gtfs_shapes.Where(Function(s As gtfs_shape) s.shape_id = inboundTrip.shape_id).OrderBy(Function(d As gtfs_shape) d.shape_pt_sequence).ToList
            Dim first As Boolean = True
            For Each inboundShape As gtfs_shape In inboundShapes
                If first Then

                    script.Append(" new google.maps.LatLng(" & inboundShape.shape_pt_lat.ToString & ", " & inboundShape.shape_pt_lon.ToString & ") " & Environment.NewLine)
                Else
                    script.Append(", new google.maps.LatLng(" & inboundShape.shape_pt_lat.ToString & ", " & inboundShape.shape_pt_lon.ToString & ") " & Environment.NewLine)

                End If
                first = False
            Next


            'We don't need append outboud for Rail.  It is the same as inbound and will not change
            ' now add outboud shapes to the SAME array
            'Dim outboundTrip = trips.Where(Function(i As gtfs_trip) i.direction_id = 1 And i.route_id = trainRoute.RouteNumber).Take(1).SingleOrDefault
            'Dim outboundShapes = db.gtfs_shapes.Where(Function(s As gtfs_shape) s.shape_id = outboundTrip.shape_id).OrderBy(Function(d As gtfs_shape) d.shape_pt_sequence).ToList

            'For Each outboundShape As gtfs_shape In outboundShapes
            '' do not worry about FIRST.  We are appending to our collection
            'script.Append(", new google.maps.LatLng(" & outboundShape.shape_pt_lat.ToString & ", " & outboundShape.shape_pt_lon.ToString & ") " & Environment.NewLine)

            'Next

            script.Append("]; ")

            script.Append(Environment.NewLine)
            script.Append(Environment.NewLine)

            ' TODO: This goes away
            Dim middleIndex As Integer = inboundShapes.Count / 2

            ' TODO: Comment this out all places used
            script.Append(" arrTrainCenterPoints[0] =  new google.maps.LatLng(" & inboundShapes(middleIndex).shape_pt_lat.ToString & ", " & inboundShapes(middleIndex).shape_pt_lon.ToString & ");" & Environment.NewLine)

            'script.Append("var trainMarker = new StyledMarker({" & Environment.NewLine)
            'script.Append(" styleIcon: new StyledIcon(StyledIconTypes.BUBBLE,{color:'#" & lineColor & "', text:'" & trainRoute.RouteName & "', fore:'#" & textColor & "'}), ")
            'script.Append(" position: new google.maps.LatLng(" & inboundShapes(middleIndex).shape_pt_lat.ToString & ", " & inboundShapes(middleIndex).shape_pt_lon & ")," & Environment.NewLine)
            'script.Append(" map: map, " & Environment.NewLine)
            'script.Append(" title: '" & trainRoute.RouteName & "'" & Environment.NewLine)
            'script.Append("});")


            If Not railStations Is Nothing And railStations.Count > 0 Then
                Dim counter As Integer = 0
                

                For Each railS As RailStation In railStations

                    script.Append(" var railInfowindow" & counter.ToString & " = new google.maps.InfoWindow({" & Environment.NewLine)
                    Dim viewRailSchedule As String = "<a href=""/Routes/Route.aspx?rt=" & railS.Station.ToString & """ target=""_blank"">Route Information</a>"

                    script.Append(" content: '" & railS.Station.ToString & " - " & railS.Station & "<br />" & viewRailSchedule & "'" & Environment.NewLine)
                    script.Append("});" & Environment.NewLine)

                    script.Append("var railMarker_" & counter.ToString & " = new MarkerWithLabel({" & Environment.NewLine)
                    script.Append("     position: new google.maps.LatLng(" & railS.Lat.ToString & ", " & railS.Lon.ToString & "), " & Environment.NewLine)
                    script.Append("     labelContent: '" & railS.Station & "', ")
                    script.Append("     icon:'http://metro.nfta.com/img/stationMarker.png', " & Environment.NewLine)
                    script.Append("     labelClass: 'railStation', " & Environment.NewLine)
                    script.Append("     labelAnchor: new google.maps.Point(-18, 21), ")
                    script.Append("     labelStyle: {color: '" & trainRoute.RouteColor & "'} " & Environment.NewLine)
                    script.Append("});")
                    script.Append(" railMarker_" & counter.ToString & ".setMap(map)" & Environment.NewLine)
                    script.Append(" railMarker_" & counter.ToString & ".setVisible(true)" & Environment.NewLine)


                    script.Append("google.maps.event.addListener(railMarker_" & counter.ToString & ", ""mouseover"", function(event) { " & Environment.NewLine)
                    script.Append("highestZIndex = highestZIndex + 1;   " & Environment.NewLine)
                    script.Append("arrTrainRouteMarkers[" & counter.ToString & "].setOptions({zIndex:highestZIndex});   " & Environment.NewLine)
                    script.Append("arrTrains[0].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
                    script.Append("});          " & Environment.NewLine)

                    script.Append("google.maps.event.addListener(railMarker_" & counter.ToString & ", ""click"", function(event) {" & Environment.NewLine)
                    script.Append(" railInfowindow" & counter.ToString & ".setPosition(event.latLng);" & Environment.NewLine)
                    script.Append(" railInfowindow" & counter.ToString & ".open(map);" & Environment.NewLine)
                    script.Append("});" & Environment.NewLine)

                    script.Append(" arrTrainRouteMarkers[" & counter.ToString & "] = railMarker_" & counter.ToString & "; " & Environment.NewLine)

                    counter = counter + 1

                    ' delete if errors thrown
                    routeCounter = routeCounter + 1
                Next

            End If


            script.Append(" var train = new google.maps.Polyline({" & Environment.NewLine)
            script.Append(" path: trainCoords, " & Environment.NewLine)
            script.Append(" strokeColor: '#")

            script.Append(lineColor & "', " & Environment.NewLine)
            script.Append(" strokeOpacity: 1.0, " & Environment.NewLine)
            script.Append(" strokeWeight: 10 }); " & Environment.NewLine)
            script.Append(Environment.NewLine)
            script.Append(Environment.NewLine)
            script.Append(" train.setMap(map);" & Environment.NewLine)

            script.Append(" arrTrains[0] = train; " & Environment.NewLine)

            script.Append(" var infowindow = new google.maps.InfoWindow({" & Environment.NewLine)
            Dim viewMapSchedule As String = "<a href=""/Routes/Route.aspx?rt=" & trainRoute.RouteNumber.ToString & """ target=""_blank"">View Schedule</a>"
            script.Append(" content: '" & trainRoute.RouteName & "<br />" & viewMapSchedule & "'" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)


            script.Append("google.maps.event.addListener(train, ""click"", function(event) {" & Environment.NewLine)
            script.Append(" infowindow.setPosition(event.latLng);" & Environment.NewLine)
            script.Append(" infowindow.open(map);" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            script.Append("google.maps.event.addListener(train, ""mouseover"", function(event) {" & Environment.NewLine)
            script.Append(" highestZIndex = highestZIndex + 1;" & Environment.NewLine)
            script.Append(" arrTrainRouteMarkers[0].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append(" arrTrains[0].setOptions({zIndex:highestZIndex});" & Environment.NewLine)
            script.Append("});" & Environment.NewLine)

            trainMarkup.Append("<div style='float:left;'><input class=""itemCheckbox"" type=""checkbox"" name=""routeBox"" id=routeBox"" checked=""checked"" onclick=""routeClick(this, 'routeBox', " & (-1).ToString & ")"" /> " & trainRoute.RouteName & " </div><div style='float:left; background:#" & lineColor & "; margin:10px; height:5px; width:20px;'> &nbsp;&nbsp;&nbsp; </div><br clear='all' />")

        End If


        script.Append("} google.maps.event.addDomListener(window, 'load', initialize);")

        script.Append("</script>")

        _script = script.ToString
        _markup = markup.ToString
        _trainMarkup = trainMarkup.ToString

    End Sub

    Private Sub AddInitializationCode(ByRef script As StringBuilder)
        script.Append("function initialize() {                                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("  var myLatLng = new google.maps.LatLng(42.924755, -78.832449);		                                                                                                                                                         " & Environment.NewLine)

        script.Append("  // Create a new StyledMapType object, passing it the array of styles,                                                                                                                                                      " & Environment.NewLine)
        script.Append("  // as well as the name to be displayed on the map type control.                                                                                                                                                            " & Environment.NewLine)
        script.Append("  var styledMap = new google.maps.StyledMapType(styles, {name: 'System Map'});                                                                                                                                               " & Environment.NewLine)
        script.Append("  var mapOptions = {          " & Environment.NewLine)
        script.Append("    zoom: 16,                 " & Environment.NewLine)
        script.Append("    center: myLatLng,         " & Environment.NewLine)
        script.Append("    mapTypeId: google.maps.MapTypeId.ROADMAP,   " & Environment.NewLine)
        script.Append("    mapTypeControl: true,                       " & Environment.NewLine)
        script.Append("	                                               " & Environment.NewLine)
        script.Append("    mapTypeControlOptions: {                    " & Environment.NewLine)
        script.Append("      style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,  " & Environment.NewLine)
        script.Append("  mapTypeIds: [google.maps.MapTypeId.ROADMAP, 'map_style']},  " & Environment.NewLine)
        script.Append("    zoomControl: true,                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("    zoomControlOptions: {                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("      style: google.maps.ZoomControlStyle.SMALL                                                                                                                                                                               " & Environment.NewLine)
        script.Append("    }                                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  };                                                                                                                                                                                                                          " & Environment.NewLine)
        script.Append("                                 " & Environment.NewLine)
        script.Append("                                 " & Environment.NewLine)
        script.Append("map = new google.maps.Map(document.getElementById('map'), mapOptions);                                                                                                                                                        " & Environment.NewLine)
        script.Append("  //Associate the styled map with the MapTypeId and set it to display.                                                                                                                                                        " & Environment.NewLine)
        script.Append("   map.mapTypes.set('map_style', styledMap);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("    map.setMapTypeId('map_style');                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("      " & Environment.NewLine)


        script.Append("// ===========================================================================" & Environment.NewLine)
        script.Append("// =============    TRANSIT CENTERS, PARK n Rides, and Carshare   =========== " & Environment.NewLine)
        script.Append("// ========================================================================== " & Environment.NewLine)

        script.Append("                                                 " & Environment.NewLine)
        script.Append("// ===============  Transit Center  1 =========================                                                                                                                                                               " & Environment.NewLine)
        script.Append("var transit001 = new google.maps.Marker({                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(43.020856, -78.877559),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Main and Niagara Transit Center' ,                                                                                                                                                                                " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-transit.png',                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  transit001.setMap(map);                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("  transit001.setVisible(true);                                                                                                                                                                                               " & Environment.NewLine)
        script.Append("  arrTransit[0] = transit001;                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("var strtransit001 = '<h3>Transit Center</h3><b>Main and Niagara</b><br />Main and Niagara Street Tonawanda, NY<br /><br />Services Routes: 25, 57, 61 and 79';	                                     " & Environment.NewLine)
        script.Append("var infowindowtransit001 = new google.maps.InfoWindow({                                                                                                                                                                       " & Environment.NewLine)
        script.Append("content: strtransit001                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(transit001, ""click"", function(event) {	                                                                                                                                                     " & Environment.NewLine)
        script.Append("	infowindowtransit001.setPosition(event.latLng);                                                                                                                                                                              " & Environment.NewLine)
        script.Append("	infowindowtransit001.open(map);                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Transit Center 1 =========================                                                                                                                                                            " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  Transit Center  2 =========================                                                                                                                                                               " & Environment.NewLine)
        script.Append("var transit002 = new google.maps.Marker({                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(43.099716, -79.051796),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Portage Road Transit Center' ,                                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-transit.png',                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  transit002.setMap(map);                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("  transit002.setVisible(true);                                                                                                                                                                                               " & Environment.NewLine)
        script.Append("  arrTransit[1] = transit002;                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("var strtransit002 = '<h3>Transit Center</h3><b>Portage Road</b><br />1124 Portage Rd, Niagara Falls, NY 14301<br /><br />Services Routes: 40, 50, 52, 54 and 55';	                                                     " & Environment.NewLine)
        script.Append("var infowindowtransit002 = new google.maps.InfoWindow({                                                                                                                                                                       " & Environment.NewLine)
        script.Append("content: strtransit002                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(transit002, ""click"", function(event) {	                                                                                                                                                     " & Environment.NewLine)
        script.Append("	infowindowtransit002.setPosition(event.latLng);                                                                                                                                                                              " & Environment.NewLine)
        script.Append("	infowindowtransit002.open(map);                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Transit Center 2 =========================                                                                                                                                                            " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  Transit Center  3 =========================                                                                                                                                                               " & Environment.NewLine)
        script.Append("var transit003 = new google.maps.Marker({                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.826966, -78.823968),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Lackawanna Victory Transit Center' ,                                                                                                                                                                                                " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-transit.png',                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  transit003.setMap(map);                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("  transit003.setVisible(true);                                                                                                                                                                                               " & Environment.NewLine)
        script.Append("  arrTransit[2] = transit003;                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("var strtransit003 = '<h3>Transit Center</h3><b>Lackawanna Victory</b><br />2694-2704 S Park Ave, Lackawanna, NY<br /><br />Services Routes: 16, 36 and 42';	                                                                 " & Environment.NewLine)
        script.Append("var infowindowtransit003 = new google.maps.InfoWindow({                                                                                                                                                                       " & Environment.NewLine)
        script.Append("content: strtransit003                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(transit003, ""click"", function(event) {	                                                                                                                                                     " & Environment.NewLine)
        script.Append("	infowindowtransit003.setPosition(event.latLng);                                                                                                                                                                              " & Environment.NewLine)
        script.Append("	infowindowtransit003.open(map);                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Transit Center 3 =========================                                                                                                                                                            " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  Transit Center  4 =========================                                                                                                                                                               " & Environment.NewLine)
        script.Append("var transit004 = new google.maps.Marker({                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(43.100626, -78.980273),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Niagara Falls Transportation Center' ,                                                                                                                                                                               " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-transit.png',                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  transit004.setMap(map);                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("  transit004.setVisible(true);                                                                                                                                                                                               " & Environment.NewLine)
        script.Append("  arrTransit[3] = transit004;                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("var strtransit004 = '<h3>Transit Center</h3><b>Niagara Falls Transportation Center</b><br />2180 Factory Outlet Blvd Niagara Falls, NY 14301<br /><br />Services Routes: 50, 52, 54, 55 and 60';	                 " & Environment.NewLine)
        script.Append("var infowindowtransit004 = new google.maps.InfoWindow({                                                                                                                                                                       " & Environment.NewLine)
        script.Append("content: strtransit004                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(transit004, ""click"", function(event) {	                                                                                                                                                     " & Environment.NewLine)
        script.Append("	infowindowtransit004.setPosition(event.latLng);                                                                                                                                                                              " & Environment.NewLine)
        script.Append("	infowindowtransit004.open(map);                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Transit Center 4 =========================                                                                                                                                                            " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  Transit Center  5 =========================                                                                                                                                                               " & Environment.NewLine)
        script.Append("var transit005 = new google.maps.Marker({                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.827554, -78.754293),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Southgate Plaza Transit Center' ,                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-transit.png',                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  transit005.setMap(map);                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("  transit005.setVisible(true);                                                                                                                                                                                               " & Environment.NewLine)
        script.Append("  arrTransit[4] = transit005;                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("var strtransit005 = '<h3>Transit Center</h3><b>Southgate Plaza</b><br />950 Union Road West Seneca, NY 14224<br /><br />Services Routes: 15 and 42';	                                                                     " & Environment.NewLine)
        script.Append("var infowindowtransit005 = new google.maps.InfoWindow({                                                                                                                                                                       " & Environment.NewLine)
        script.Append("content: strtransit005                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(transit005, ""click"", function(event) {	                                                                                                                                                     " & Environment.NewLine)
        script.Append("	infowindowtransit005.setPosition(event.latLng);                                                                                                                                                                              " & Environment.NewLine)
        script.Append("	infowindowtransit005.open(map);                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Transit Center 5 =========================                                                                                                                                                            " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  Transit Center  6 =========================                                                                                                                                                               " & Environment.NewLine)
        script.Append("var transit006 = new google.maps.Marker({                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.902704, -78.783768),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Thruway Mall Transit Center' ,                                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-transit.png',                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  transit006.setMap(map);                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("  transit006.setVisible(true);                                                                                                                                                                                               " & Environment.NewLine)
        script.Append("  arrTransit[5] = transit006;                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("var strtransit006 = '<h3>Transit Center</h3><b>Thruway Mall</b><br />Harlem and Thruway Plaza Drive Buffalo, NY 14225<br /><br />Services Routes: 4, 6, 22, 26, 32 and 46';	                                             " & Environment.NewLine)
        script.Append("var infowindowtransit006 = new google.maps.InfoWindow({                                                                                                                                                                       " & Environment.NewLine)
        script.Append("content: strtransit006                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(transit006, ""click"", function(event) {	                                                                                                                                                     " & Environment.NewLine)
        script.Append("	infowindowtransit006.setPosition(event.latLng);                                                                                                                                                                              " & Environment.NewLine)
        script.Append("	infowindowtransit006.open(map);                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Transit Center 6 =========================                                                                                                                                                            " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  Transit Center  7 =========================                                                                                                                                                               " & Environment.NewLine)
        script.Append("var transit007 = new google.maps.Marker({                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.883179, -78.872127),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Metropolitan Transportation Center' ,                                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-transit.png',                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  transit007.setMap(map);                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("  transit007.setVisible(true);                                                                                                                                                                                               " & Environment.NewLine)
        script.Append("  arrTransit[6] = transit007;                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("var strtransit007 = '<h3>Transit Center</h3><b>Metropolitan Transportation Center</b><br />181 Ellicott Street Buffalo, NY 14203<br /><br />Services Routes: 3, 5, 6, 8, 11, 20, 24, 25, 40, 60, 61, 64, 68, 74, 76 and 79';	                                             " & Environment.NewLine)
        script.Append("var infowindowtransit007 = new google.maps.InfoWindow({                                                                                                                                                                       " & Environment.NewLine)
        script.Append("content: strtransit007                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(transit007, ""click"", function(event) {	                                                                                                                                                     " & Environment.NewLine)
        script.Append("	infowindowtransit007.setPosition(event.latLng);                                                                                                                                                                              " & Environment.NewLine)
        script.Append("	infowindowtransit007.open(map);                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Transit Center 7 =========================                                                                                                                                                            " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  Park 1 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr001 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.888985, -78.750653),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Appletree Business Park',                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("	//animation: google.maps.Animation.BOUNCE,                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr001.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr001.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[0] = pnr001;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr001 = '<h3>Park & Ride</h3><b>Appletree Business Park</b><br />2875 Union Road Cheektowaga, NY 14227<br /><br />Services Routes: 1 and 69';	                                                                 " & Environment.NewLine)
        script.Append("var infowindowpnr001 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr001                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr001, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr001.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr001.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End Park 1 =========================                                                                                                                                                                      " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 2 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr002 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.773057, -78.861936),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Athol Springs' ,                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr002.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr002.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[1] = pnr002;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr002 = '<h3>Park & Ride</h3><b>Athol Springs</b><br />24066 Lake Shore Road Hamburg, NY 14075<br /><br />Services Routes: 74 and 76';	                                                                             " & Environment.NewLine)
        script.Append("var infowindowpnr002 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr002                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr002, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr002.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr002.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 2 =========================                                                                                                                                                                      " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 3 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr003 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(43.043602, -78.750383),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Crosspoint Business Park' ,                                                                                                                                                                                          " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});	                                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr003.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr003.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[2] = pnr003;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr003 = '<h3>Crosspoint Business Park</h3><b>Athol Springs</b><br />300 Crosspointe Parkway Getzville, NY 14068<br /><br />Services Routes: 44 and 64';	                                                             " & Environment.NewLine)
        script.Append("var infowindowpnr003 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr003                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr003, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr003.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr003.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 3 =========================                                                                                                                                                                      " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 4 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr004 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.973424, -78.694555),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Eastern Hills Mall' ,                                                                                                                                                                                                " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr004.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr004.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[3] = pnr004;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr004 = '<h3>Park & Ride</h3><b>Eastern Hills Mall</b><br />4545 Transit Road Williamsville, NY 14221<br /><br />Services Routes: 48 and 66';	                                                                     " & Environment.NewLine)
        script.Append("var infowindowpnr004 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr004                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr004, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr004.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr004.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 4 =========================                                                                                                                                                                      " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 5 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr005 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.77245, -78.796269),                                                                                                                                                                      " & Environment.NewLine)
        script.Append("	title: 'Erie Community College South' ,                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr005.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr005.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[4] = pnr005;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr005 = '<h3>Park & Ride</h3><b>Erie Community College South</b><br />4041 Southwestern Blvd. Orchard Park, NY 14127<br /><br />Services Routes: 14 and 72';	                                                         " & Environment.NewLine)
        script.Append("var infowindowpnr005 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr005                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr005, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr005.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr005.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 5 =========================                                                                                                                                                                      " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 6 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr006 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.704749, -78.972073),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Highland Elementary School' ,                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr006.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr006.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[5] = pnr006;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr006 = '<h3>Park & Ride</h3><b>Highland Elementary School</b><br />6745 Erie Road Derby, NY 14047<br /><br />Services Route: 76';	                                                                                 " & Environment.NewLine)
        script.Append("var infowindowpnr006 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr006                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr006, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr006.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr006.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 6 =========================                                                                                                                                                                      " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 7 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr007 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.937293, -78.717955),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Holtz Drive' ,                                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr007.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr007.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[6] = pnr007;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr007 = '<h3>Park & Ride</h3><b>Holtz Drive</b><br />62 Holtz Drive Cheektowaga, NY 14225<br /><br />Services Routes: 47 and 204';	                                                                                 " & Environment.NewLine)
        script.Append("var infowindowpnr007 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr007                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr007, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr007.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr007.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK   7 =========================                                                                                                                                                                    " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 8 =========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr008 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.947748, -78.830092),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'LaSalle Station' ,                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr008.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr008.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[7] = pnr008;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr008 = '<h3>Park & Ride</h3><b>LaSalle Station</b><br />3030 Main Sreet, Buffalo, NY 14214<br /><br />Services Metro Rail and  Routes: 8 and 23';	                                                                                 " & Environment.NewLine)
        script.Append("var infowindowpnr008 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr008                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr008, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr008.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr008.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK  8 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 9 =========================                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var pnr009 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(43.020856, -78.877559),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Main and Niagara City of Tonawanda' ,                                                                                                                                                                                " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr009.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr009.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[8] = pnr009;                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var strpnr009 = '<h3>Park & Ride</h3><b>Main and Niagara City of Tonawanda</b><br />Main and Niagara Street Tonawanda, NY<br /><br />Services Routes: 25, 57, 61 and 79';	                                             " & Environment.NewLine)
        script.Append("var infowindowpnr009 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr009                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr009, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr009.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr009.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  End PARK 9 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 10 =========================                                                                                                                                                                         " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("var pnr010 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.960983, -78.755817),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Main and Union, Williamsville' ,                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr010.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr010.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[9] = pnr010;                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("var strpnr010 = '<h3>Park & Ride</h3><b>Main and Union, Williamsville</b><br />24 S Union Road Buffalo, NY 14221<br /><br />Services Routes: 47, 48, and 66';	                                                         " & Environment.NewLine)
        script.Append("var infowindowpnr010 = new google.maps.InfoWindow({                                                                                                                                                                          " & Environment.NewLine)
        script.Append("content: strpnr010                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr010, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr010.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr010.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 10 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 11 =========================                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var pnr011 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.783423, -78.811237),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'McKinley Mall' ,                                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr011.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr011.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[10] = pnr011;                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("var strpnr011 = '<h3>Park & Ride</h3><b>McKinley Mall</b><br />3701 McKinley Parkway Blasdell, NY 14219<br /><br />Services Routes: 14, 16 and 36';	                                                                             " & Environment.NewLine)
        script.Append("var infowindowpnr011 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr011                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr011, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr011.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr011.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 11 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 12 =========================                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var pnr012 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.686336, -78.777545),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'North Boston' ,                                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  pnr012.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr012.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[11] = pnr012;                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("var strpnr012 = '<h3>Park & Ride</h3><b>North Boston</b><br />Boston State Road North Boston, NY<br /><br />Services Route: 74';	                                                                                             " & Environment.NewLine)
        script.Append("var infowindowpnr012 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr012                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr012, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr012.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr012.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 12 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 13 ========================                                                                                                                                                                          " & Environment.NewLine)
        script.Append("var pnr013 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.763641, -78.762531),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Route 20A and 219' ,                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                      " & Environment.NewLine)
        script.Append("});	                                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr013.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr013.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[12] = pnr013;                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("var strpnr013 = '<h3>Park & Ride</h3><b>Route 20A and 219</b><br />5920 Big Tree Road Orchard Park, NY 14127<br /><br />Services Route: 72';	                                                                                 " & Environment.NewLine)
        script.Append("var infowindowpnr013 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr013                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr013, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr013.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr013.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 13 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 14 =========================                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var pnr014 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.956417, -78.819752),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'University Station' ,                                                                                                                                                                                                " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});	                                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr014.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr014.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[13] = pnr014;                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("var strpnr014 = '<h3>Park & Ride</h3><b>University Station</b><br />3435 Main Street Buffalo, NY 14214<br /><br />Services Metro Rail and Routes: 5, 8, 12, 13 19, 34, 44, 47, 48, and 49';	                                                     " & Environment.NewLine)
        script.Append("var infowindowpnr014 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr014                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr014, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr014.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr014.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 14 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 15 =========================                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var pnr015 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.639096, -79.027259),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'Village of Angola' ,                                                                                                                                                                                                 " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});	                                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr015.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr015.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[14] = pnr015;                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("var strpnr015 = '<h3>Park & Ride</h3><b>Village of Angola</b><br />Main street  Angola, NY 14006<br /><br />Services Route: 76';	                                                                                             " & Environment.NewLine)
        script.Append("var infowindowpnr015 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr015                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr015, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr015.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr015.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 15 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("                                                                                                                                                                                                                              " & Environment.NewLine)
        script.Append("// ===============  PARK 16 =========================                                                                                                                                                                         " & Environment.NewLine)
        script.Append("var pnr016 = new google.maps.Marker({                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("	position: new google.maps.LatLng(42.842891, -78.789955),                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	title: 'West Seneca Municipal Parking' ,                                                                                                                                                                                     " & Environment.NewLine)
        script.Append("	icon: 'http://metro.nfta.com/img/gmap-park.png',                                                                                                                                                                             " & Environment.NewLine)
        script.Append("	animation: google.maps.Animation.DROP                                                                                                                                                                                       " & Environment.NewLine)
        script.Append("});	                                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr016.setMap(map);                                                                                                                                                                                                         " & Environment.NewLine)
        script.Append("  pnr016.setVisible(true);                                                                                                                                                                                                   " & Environment.NewLine)
        script.Append("  arrPNR[15] = pnr016;                                                                                                                                                                                                        " & Environment.NewLine)
        script.Append("var strpnr016 = '<h3>Park & Ride</h3><b>West Seneca Municipal Parking</b><br />2789 Seneca Street West Seneca, NY 14224<br /><br />Services Routes: 15 and 75';	                                                             " & Environment.NewLine)
        script.Append("var infowindowpnr016 = new google.maps.InfoWindow({                                                                                                                                                                           " & Environment.NewLine)
        script.Append("content: strpnr016                                                                                                                                                                                                            " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("  google.maps.event.addListener(pnr016, ""click"", function(event) {	                                                                                                                                                         " & Environment.NewLine)
        script.Append("	infowindowpnr016.setPosition(event.latLng);                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("	infowindowpnr016.open(map);                                                                                                                                                                                                  " & Environment.NewLine)
        script.Append("});                                                                                                                                                                                                                           " & Environment.NewLine)
        script.Append("// ===============  End PARK 16 =========================                                                                                                                                                                     " & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  1 ========================= " & Environment.NewLine)
        script.Append("var carshare001 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.899462, -78.87656)," & Environment.NewLine)
        script.Append("title: 'Quaker Bonnet at Allen/Elmwood' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare001.setMap(map);" & Environment.NewLine)
        script.Append("carshare001.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[0] = carshare001;" & Environment.NewLine)
        script.Append("var strcarshare001 = '<h3>Quaker Bonnet at Allen/Elmwood</h3>The vehicle is parked at 175 Allen Street behind Quaker Bonnet Eatery, which is across from Towne Restaurant.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare001 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare001" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare001, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare001.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare001.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 1 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  2 ========================= " & Environment.NewLine)
        script.Append("var carshare002 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.89956, -78.86877)," & Environment.NewLine)
        script.Append("title: 'Allen/Medical Station' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare002.setMap(map);" & Environment.NewLine)
        script.Append("carshare002.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[1] = carshare002;" & Environment.NewLine)
        script.Append("var strcarshare002 = '<h3>Allen/Medical Station</h3>The vehicle is parked in the Buffalo Niagara Medical Campus parking lot on Washington (between Carlton and High), across the street from the Allen-Medical NFTA Train Station.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare002 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare002" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare002, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare002.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare002.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 2 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  3 ========================= " & Environment.NewLine)
        script.Append("var carshare003 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.8994, -78.870592)," & Environment.NewLine)
        script.Append("title: 'BCS Office' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare003.setMap(map);" & Environment.NewLine)
        script.Append("carshare003.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[2] = carshare003;" & Environment.NewLine)
        script.Append("var strcarshare003 = '<h3>BCS Office</h3>In front of our office at 14 Allen St.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare003 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare003" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare003, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare003.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare003.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 3 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  4 ========================= " & Environment.NewLine)
        script.Append("var carshare004 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.907083, -78.87713)," & Environment.NewLine)
        script.Append("title: 'Brent Manor at 366 Elmwood' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare004.setMap(map);" & Environment.NewLine)
        script.Append("carshare004.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[3] = carshare004;" & Environment.NewLine)
        script.Append("var strcarshare004 = '<h3>Brent Manor at 366 Elmwood</h3>The vehicle is parked just in front of Brent Manor apartments at 366 Elmwood between Summer and Bryant.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare004 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare004" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare004, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare004.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare004.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 4 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  5 ========================= " & Environment.NewLine)
        script.Append("var carshare005 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.951866, -78.823289)," & Environment.NewLine)
        script.Append("title: 'St. Joes/University Station' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare005.setMap(map);" & Environment.NewLine)
        script.Append("carshare005.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[4] = carshare005;" & Environment.NewLine)
        script.Append("var strcarshare005 = '<h3>St. Joes/University Station</h3>The vehicle is parked in the St. Joseph University Parish parking lot at 3269 Main Street. If you are coming from University station, walk towards downtown when exiting the station. St. Joseph University Parish is located on the same side as UB across from Shango Restaurant.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare005 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare005" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare005, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare005.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare005.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 5 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  6 ========================= " & Environment.NewLine)
        script.Append("var carshare006 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.921429, -78.889708)," & Environment.NewLine)
        script.Append("title: '271 Grant/Delavan' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare006.setMap(map);" & Environment.NewLine)
        script.Append("carshare006.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[5] = carshare006;" & Environment.NewLine)
        script.Append("var strcarshare006 = '<h3>271 Grant/Delavan</h3>The vechicle is parked behind the former library, which is now PUSH Buffalo (271 Grant Street).<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare006 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare006" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare006, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare006.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare006.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 6 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  7 ========================= " & Environment.NewLine)
        script.Append("var carshare007 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.923049, -78.854176)," & Environment.NewLine)
        script.Append("title: 'Delavan/Jefferson Parking Ramp' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare007.setMap(map);" & Environment.NewLine)
        script.Append("carshare007.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[6] = carshare007;" & Environment.NewLine)
        script.Append("var strcarshare007 = '<h3>Delavan/Jefferson Parking Ramp</h3>The vehicle is parked in the blue parking ramp on Delavan and Jefferson (across from Hedley Place). The vehicle is near the parking attendant on the first floor.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare007 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare007" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare007, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare007.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare007.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 7 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  8 ========================= " & Environment.NewLine)
        script.Append("var carshare008 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.918713, -78.868961)," & Environment.NewLine)
        script.Append("title: '1272 Delaware/Auburn' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare008.setMap(map);" & Environment.NewLine)
        script.Append("carshare008.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[7] = carshare008;" & Environment.NewLine)
        script.Append("var strcarshare008 = '<h3>1272 Delaware/Auburn</h3>The vehicle is parked south of Gates Circle in the parking lot directly behind the Network of Religious Communities (1272 Delaware Avenue).<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare008 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare008" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare008, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare008.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare008.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 8 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  9 ========================= " & Environment.NewLine)
        script.Append("var carshare009 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.940367, -78.879006)," & Environment.NewLine)
        script.Append("title: 'Joes Service at Elmwood/Amherst' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare009.setMap(map);" & Environment.NewLine)
        script.Append("carshare009.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[8] = carshare009;" & Environment.NewLine)
        script.Append("var strcarshare009 = '<h3>Joes Service at Elmwood/Amherst</h3>The vehicle is parked in the corner parking spot of Joes Service Center at Elmwood and Amherst.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare009 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare009" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare009, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare009.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare009.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 9 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  10 ========================= " & Environment.NewLine)
        script.Append("var carshare010 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.933582, -78.889038)," & Environment.NewLine)
        script.Append("title: 'Buffalo State Lot M-2 at Grant/Letchworth' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare010.setMap(map);" & Environment.NewLine)
        script.Append("carshare010.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[9] = carshare010;" & Environment.NewLine)
        script.Append("var strcarshare010 = '<h3>Buffalo State Lot M-2 at Grant/Letchworth</h3>The vehicle is parked near the entrance of Lot M-2 (Grant/Letchworth) across from the Student Apartment Complex.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare010 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare010" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare010, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare010.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare010.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 10 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  11 ========================= " & Environment.NewLine)
        script.Append("var carshare011 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.932728, -78.880634)," & Environment.NewLine)
        script.Append("title: 'Buffalo State Lot C' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare011.setMap(map);" & Environment.NewLine)
        script.Append("carshare011.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[10] = carshare011;" & Environment.NewLine)
        script.Append("var strcarshare011 = '<h3>Buffalo State Lot C</h3>The vehicle is parked in Lot C in front of the Donald Savage Theater and Communications Building. To get to Lot C take Rockwell Rd. to Cleveland Cr.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare011 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare011" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare011, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare011.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare011.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 11 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  12 ========================= " & Environment.NewLine)
        script.Append("var carshare012 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.895696, -78.87451)," & Environment.NewLine)
        script.Append("title: 'The Mansion at 414 Delaware/Edward' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare012.setMap(map);" & Environment.NewLine)
        script.Append("carshare012.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[11] = carshare012;" & Environment.NewLine)
        script.Append("var strcarshare012 = '<h3>The Mansion at 414 Delaware/Edward</h3>The vehicle is located in a parking lot across the street from The Mansion on Delaware (414 Delaware Avenue). Exit out the gate on Edward St., and make a Left (Edward St. is a one-way). Come back in off of Delaware Ave. and use the parking pass inside the envelope located in the passenger glove box.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare012 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare012" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare012, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare012.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare012.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 12 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  13 ========================= " & Environment.NewLine)
        script.Append("var carshare013 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.895764, -78.868087)," & Environment.NewLine)
        script.Append("title: 'EV - Innovation Center at 640 Ellicott/Goodell' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare013.setMap(map);" & Environment.NewLine)
        script.Append("carshare013.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[12] = carshare013;" & Environment.NewLine)
        script.Append("var strcarshare013 = '<h3>EV - Innovation Center at 640 Ellicott/Goodell</h3>The Electric Vehicle is parked in the parking lot across the street from the Innovation Center (640 Ellicott Street) on the Buffalo Niagara Medical Campus. It is located in the middle of the lot by the electric charging stations.NOTE: This is an EV. Do not reserve this vehicle if you plan on going more than 50 miles during your trip. Please read the user guide located inside the vehicle if this is your first time driving the EV.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare013 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare013" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare013, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare013.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare013.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 13 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  14 ========================= " & Environment.NewLine)
        script.Append("var carshare014 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.89956, -78.86877)," & Environment.NewLine)
        script.Append("title: 'Electric Vehicle (Allen/Medical) No Gas 50 mi. max' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare014.setMap(map);" & Environment.NewLine)
        script.Append("carshare014.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[13] = carshare014;" & Environment.NewLine)
        script.Append("var strcarshare014 = '<h3>Electric Vehicle (Allen/Medical) No Gas 50 mi. max</h3>The Electric Vehicle is parked at the ChargePoint Charging Station in the Buffalo Niagara Medical Campus parking lot. The lot is located on Washington St. (between Carlton and High), across the street from the Allen-Medical NFTA Train Station.Please read the instructions on using an Electric Vehicle before you drive the vehicle. Be sure to plug the charging plug back into the car when you bring it back. Failure to do so will result in a $25 fine. You must place the ChargePoint keychain card on the ChargePoint station to remove the charging plug.<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare014 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare014" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare014, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare014.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare014.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 14 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("// ===============  Carshare  15 ========================= " & Environment.NewLine)
        script.Append("var carshare015 = new google.maps.Marker({" & Environment.NewLine)
        script.Append("position: new google.maps.LatLng(42.912412, -78.883806)," & Environment.NewLine)
        script.Append("title: '490 Rhode Island St.' ," & Environment.NewLine)
        script.Append("icon: 'http://metro.nfta.com/img/carshare.png'," & Environment.NewLine)
        script.Append("animation: google.maps.Animation.DROP" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("carshare015.setMap(map);" & Environment.NewLine)
        script.Append("carshare015.setVisible(true);" & Environment.NewLine)
        script.Append("arrCARSHARE[14] = carshare015;" & Environment.NewLine)
        script.Append("var strcarshare015 = '<h3>490 Rhode Island St.</h3>The vehicle is located in the parking lot directly across the street from Providence Social (490 Rhode Island St.).<br /><br /><a href=""http://www.buffalocarshare.org/"" target=""_car"">Buffalo CarShare</a>';" & Environment.NewLine)
        script.Append("var infowindowcarshare015 = new google.maps.InfoWindow({" & Environment.NewLine)
        script.Append("content: strcarshare015" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("google.maps.event.addListener(carshare015, ""click"", function(event) {" & Environment.NewLine)
        script.Append("infowindowcarshare015.setPosition(event.latLng);" & Environment.NewLine)
        script.Append("infowindowcarshare015.open(map);" & Environment.NewLine)
        script.Append("});" & Environment.NewLine)
        script.Append("// =============== End Carshare 15 =========================" & Environment.NewLine)
        script.Append("" & Environment.NewLine)
        script.Append("" & Environment.NewLine)

    End Sub
    Public ReadOnly Property Script()
        Get
            Return _script
        End Get

    End Property

    Public ReadOnly Property Markup()
        Get
            Return _markup
        End Get
    End Property

    Public ReadOnly Property TrainMarkup()
        Get
            Return _trainMarkup
        End Get
    End Property

    Public ReadOnly Property TrainIndex()
        Get
            Return _trainIndex
        End Get
    End Property

End Class
