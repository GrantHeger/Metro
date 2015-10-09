<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Feedback.aspx.vb" Inherits="NFTA_Metro._feed" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Website Feedback</title>
      <style type="text/css">
        .mapfeedback {
            width:500px; text-align:left; margin:0 auto;
        }
        input{   
            padding: 9px;  
            border: solid 1px #E5E5E5;  
            outline: 0;  
            font: normal 13px/100% Verdana, Tahoma, sans-serif;  
            width: 200px;  
            background: #FFFFFF url('/img/form-bg.png') left top repeat-x;  
            background: -webkit-gradient(linear, left top, left 25, from(#FFFFFF), color-stop(4%, #EEEEEE), to(#FFFFFF));  
            background: -moz-linear-gradient(top, #FFFFFF, #EEEEEE 1px, #FFFFFF 25px);  
            box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;  
            -moz-box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;  
            -webkit-box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;  
        }   
      
        input:hover, input:focus, {   
            border-color: #C9C9C9;   
            -webkit-box-shadow: rgba(0, 0, 0, 0.15) 0px 0px 8px;  
        }  
         
        .submit input {  
            width: auto;  
            padding: 9px 15px;  
            background: #617798;  
            border: 0;  
            font-size: 14px;  
            color: #FFFFFF;  
            -moz-border-radius: 5px;  
            -webkit-border-radius: 5px;  
        }  
      
    </style>
   
</asp:Content>


<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Website Feedback</h1>
    <asp:PlaceHolder runat="server" ID="phForm">
    	
    	
        <p>To send suggestions and feedback related to the NFTA-Metro website submit the form below.</p>
      

        <div class="mapfeedback">  
            <p class="name">  
                <label for="name"><br />Name <em>(optional)</em>:</label><br />
                <asp:TextBox ID="tbName" runat="server" />   
            </p> 
            <p class="email">  
                <label for="email">Email <em>(optional)</em>:</label> <br />
                <asp:TextBox ID="tbEmail" runat="server" /></asp:TextBox>
            </p>  
            <p class="Comments">  
                <br /><label for="comments">Comments:</label> <br />
                <asp:TextBox TextMode="multiline" Columns="60" Rows="8" ID="tbComments" runat="server" />  
            </p>  
            
            <p class="submit"><asp:Button ID="btnSubmit" runat="server" Text="Submit" /></p>  
        
            
        </div>     
           
   
        </asp:PlaceHolder>
        
        
        <asp:PlaceHolder runat="server" ID="phSent" Visible="false">
            <p><br /><br />Thank you.  Your message has been sent.<br /><br /></p>
        </asp:PlaceHolder>
</asp:Content>