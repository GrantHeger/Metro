// onblur function in email list sign up
function CheckEmail(element)
{
  var result = false
  var theStr = new String(element.value)
  var index = theStr.indexOf("@");

  if (theStr.length > 0) {

     if (index > 0)
     {
       var pindex = theStr.indexOf(".",index);
       if ((pindex > index+1) && (theStr.length > pindex+1))
   	result = true;
     }

    if (!result) {
   	alert("Please enter a valid email address");
	element.select();
	element.focus();
    }
  }
}

// validation in email sign up
function formCheck(formobj){
	var fieldRequired = Array("name", "homephone");
	var fieldDescription = Array("Name", "Home Phone");
	var alertMsg = "Please complete the following fields:\n";
	
	var l_Msg = alertMsg.length;
	
	for (var i = 0; i < fieldRequired.length; i++){
		var obj = formobj.elements[fieldRequired[i]];
		if (obj){
			switch(obj.type){
			case "select-one":
				if (obj.selectedIndex == -1 || obj.options[obj.selectedIndex].text == ""){
					alertMsg += " - " + fieldDescription[i] + "\n";
				}
				break;
			case "select-multiple":
				if (obj.selectedIndex == -1){
					alertMsg += " - " + fieldDescription[i] + "\n";
				}
				break;
			case "text":
			case "textarea":
				if (obj.value == "" || obj.value == null){
					alertMsg += " - " + fieldDescription[i] + "\n";
				}
				break;
			default:
				if (obj.value == "" || obj.value == null){
					alertMsg += " - " + fieldDescription[i] + "\n";
				}
			}
		}
	}

	if (alertMsg.length == l_Msg){
		return true;
	}else{
		alert(alertMsg);
		return false;
	}
}

