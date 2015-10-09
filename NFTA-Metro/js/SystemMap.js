        //var arrRoutes = [];
		var arrPNR = [];
		var arrTransit = [];
		var arrCARSHARE = [];
		var arrRouteCenterPoints = [];
		var arrTrains = [];
	    var map;
	    var infoWindow;
		
        function setMinMaxCoords(coords, routeIndex, isTrain) {
            // loop through the coords that are passed
            // in and add to arrRouteCenterPoints
            /*
                accepted  Find the max and min for both lat and long and then center on (max-min)/2 for each.
                That should be (max + min) / 2, the average.
            */    
            var maxLong;
            var minLong;
            
            var maxLat;
            var minLat;
            
            var x;
            var y;
            
            var i = 0;
            
            for(i = 0; i < coords.length; i++) {
                if (i == 0) {
                    maxLong = coords[i].lng();
                    minLong = coords[i].lng();
                    
                    maxLat = coords[i].lat();
                    minLat = coords[i].lat();
                } else {
                    maxLong = coords[i].lng() > maxLong ? coords[i].lng() : maxLong;
                    minLong = coords[i].lng() < minLong ? coords[i].lng() : minLong;
                    
                    maxLat = coords[i].lat() > maxLat ? coords[i].lat() : maxLat;
                    minLat = coords[i].lat() < minLat ? coords[i].lat() : minLat;
                }
            
            }
            
            x = (minLat + maxLat) / 2;
            y = (minLong + maxLong) / 2;
            
            if (isTrain == 'False') {
                arrRouteCenterPoints[routeIndex] =  new google.maps.LatLng ( x, y );
            
            } else {
                arrTrainCenterPoints[0] = new google.maps.LatLng ( x, y );
            }
                   
        }

	    function show(category) {
	        if (category == "routes") {
	            for (var i = 0; i < arrInboundRoutes.length; i++) {
	                arrInboundRoutes[i].setVisible(true);
	                //arrOutboundRoutes[i].setVisible(true);
	                arrRouteMarkers[i].setVisible(true);
	            }
	            for (var i = 0; i < arrOutboundRoutes.length; i++) {
	                //arrInboundRoutes[i].setVisible(true);
	                arrOutboundRoutes[i].setVisible(true);
	                arrRouteMarkers[i].setVisible(true);
	            }
	        }
	        if (category == "trains") {
	          
	            // var info = "";
	            for (var i = 0; i < arrTrainRouteMarkers.length; i++) {
	                //info += "arrTrainRouteMarkers[" + i.toString() + "] \n";
	                arrTrainRouteMarkers[i].setVisible(true);
	            }
	            
	            //alert(info);
	            
	        }
			if (category == "pnr") {
			    arrPNR[0].setVisible(true);
	            for (var i = 0; i < arrPNR.length; i++) {
	                arrPNR[i].setVisible(true);
	            }
	        }
			if (category == "transit") {
	            for (var i = 0; i < arrTransit.length; i++) {
	                arrTransit[i].setVisible(true);
	            }
	        }
	        if (category == "carshare") {
	            for (var i = 0; i < arrCARSHARE.length; i++) {
	                arrCARSHARE[i].setVisible(true);
	            }
	        }
	    }

	    function hide(category) {
	        if (category == "routes") {
	            for (var i = 0; i < arrInboundRoutes.length; i++) {
	                arrInboundRoutes[i].setVisible(false);
	                //arrOutboundRoutes[i].setVisible(false);
	                
	                arrRouteMarkers[i].setVisible(false);
	            }
	            for (var i = 0; i < arrOutboundRoutes.length; i++) {
	                //arrInboundRoutes[i].setVisible(false);
	                arrOutboundRoutes[i].setVisible(false);
	                
	                arrRouteMarkers[i].setVisible(false);
	            }
	        }
	        if (category == "trains") {
	            for(var i = 0; i < arrTrainRouteMarkers.length; i++) {
	                arrTrainRouteMarkers[i].setVisible(false);
	            }
	            
	            arrTrains[0].setVisible(false);
	            
	        }

			if (category == "pnr") {
	            for (var i = 0; i < arrPNR.length; i++) {
	                arrPNR[i].setVisible(false);
	            }
	        }
			if (category == "transit") {
	            for (var i = 0; i < arrTransit.length; i++) {
	                arrTransit[i].setVisible(false);
	            }
	        }
	        if (category == "carshare") {
	            for (var i = 0; i < arrCARSHARE.length; i++) {
	                arrCARSHARE[i].setVisible(false);
	            }
	        }
	    }

	    function catClick(box, category) {	        
	        var checkbox = $(box);
	
	        var itemsdiv = category != "trains" ? $(checkbox).parent().parent().find(".itemsDiv") : $(checkbox).parent().parent().find(".itemsTrainsDiv") ;
	        if ($(checkbox).is(":checked") == true) {	            
	            $(checkbox).prop("checked", true);
	            // select child items                
	            $(itemsdiv).find(".itemCheckbox").each(function () {
	                $(this).prop("checked", true);	                
	            });
	            show(category);         
	
	        } else {
                hide(category);
	            $(checkbox).removeAttr("checked");
	            // deselect child items
	            $(itemsdiv).find(".itemCheckbox").each(function () {
	                $(this).removeAttr("checked");
	            });	
	        }
	    }
	    
	    function getHighestZIndex() {

            // if we haven't previously got the highest zIndex
            // save it as no need to do it multiple times
            if (highestZIndex==0) {
                if (markers.length>0) {
                    for (var i=0; i<markers.length; i++) {
                        tempZIndex = markers[i].getZIndex();
                        if (tempZIndex>highestZIndex) {
                            highestZIndex = tempZIndex;
                        }
                    }
                }
            }
            return highestZIndex;

        }
	    
	    function routeClick(box, rt, routeIndex) {
	        var checkbox = $(box);
	        
	        //alert(routeIndex);	       
	        
	        if ($(checkbox).is(":checked") == true) {
	       
	            if (routeIndex == -1) {
	                arrTrains[0].setVisible(true);
	                for(var i= 0; i < arrTrainRouteMarkers.length; i++) {
	                    arrTrainRouteMarkers[i].setVisible(true);
	                }
	                
	            } else {
	                arrInboundRoutes[routeIndex].setVisible(true);	 
	                arrOutboundRoutes[routeIndex].setVisible(true);
	                arrRouteMarkers[routeIndex].setVisible(true); 
	            }
	            //zoomToRoute(routeIndex);
	            //document.getElementById(rt + "Box").checked == true	            
	                      
	            
	            // zoom to selection
	            if (routeIndex == -1) {
	                map.setCenter( arrTrainCenterPoints[0], 18);
	                
	            } else {  
	                
	                map.setCenter( arrRouteCenterPoints[routeIndex], 18);
	            }
	            
	            
	            
	        } else {
	            if (routeIndex == -1) {
	            	                //document.getElementById(rt + "Box").checked == false
	                arrTrains[0].setVisible(false);
	                for(var i = 0; i < arrTrainRouteMarkers.length; i++) {
	                    arrTrainRouteMarkers[i].setVisible(false);
	                } 
	                
	            }
	            else {
	                //document.getElementById(rt + "Box").checked == false
	                arrInboundRoutes[routeIndex].setVisible(false);
	                arrOutboundRoutes[routeIndex].setVisible(false);
	                
	                arrRouteMarkers[routeIndex].setVisible(false);	            
	            }

	        }
	    }
		
		function pnrClick(box, pnr, pnrIndex) {
	        if (document.getElementById(pnr + "Box").checked == true) {
	            document.getElementById(pnr + "Box").checked == true
	            arrPNR[pnrIndex].setVisible(true);
	            map.setCenter( arrPNR[pnrIndex].position, 18);			            
	            			
	        } else {
	            document.getElementById(pnr + "Box").checked == false
				arrPNR[pnrIndex].setVisible(false);
	        }
	    }

	    	    	    
		function transitClick(box, transit, transitIndex) {
	        if (document.getElementById(transit + "Box").checked == true) {
	            document.getElementById(transit + "Box").checked == true
	            arrTransit[transitIndex].setVisible(true);	
	            map.setCenter( arrTransit[transitIndex].position, 18);		
	        } else {
	            document.getElementById(transit + "Box").checked == false
				arrTransit[transitIndex].setVisible(false);
	        }
	    }
	    
	    function carshareClick(box, carshare, carshareIndex) {
	        if (document.getElementById(carshare + "Box").checked == true) {
	            document.getElementById(carshare + "Box").checked == true
	            arrCARSHARE[carshareIndex].setVisible(true);	
	            map.setCenter( arrCARSHARE[carshareIndex].position, 18);		
	        } else {
	            document.getElementById(carshare + "Box").checked == false
				arrCARSHARE[carshareIndex].setVisible(false);
	        }
	    }


  