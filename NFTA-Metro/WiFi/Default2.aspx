<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default2.aspx.vb" Inherits="NFTA_Metro._Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>NFTA-Metro Enhanced Express Service WiFi</title>
    <style type="text/css">
    body { font-family:Aerial, Sans-Serif; font-size:13px; background:#004990; margin:0; padding:50px 0 0 0; color:#fff; }
    img {border:0;}
    h1 {color:#fff;  font-size:22px;}
    a {color:#fff;}
    </style>
 


    <script type = "text/javascript">
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=cbox.ClientID %>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <p><img src="wifi.png" width="883" height="447" alt="NFTA-Metro WiFi" /></p>
            <br /><br /> 
               
            <p style="padding:5px 0;">
           <asp:CheckBox runat="server" ID="cbox" /> <b>I agree to the <a href="terms2.aspx">terms of service</a></b><br />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="You must agree to the terms of service" ClientValidationFunction = "ValidateCheckBox"></asp:CustomValidator><br /><br />
            <asp:Button Id="btnEnter" runat="server" Text="ENTER" /><br /><br />
            NFTA-Metro Customer Care: <a href="mailto:info@nfta.com?subject=Express Service Wi-Fi" target="_new" >info@nfta.com</a> or (716) 855-7200
               </p>
         
        
        </center>
    </div>
    <script type ="text/javascript"  src="/js/domroll.js"></script>  
    </form>
</body>
</html>
