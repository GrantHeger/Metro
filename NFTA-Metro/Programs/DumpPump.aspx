<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DumpPump.aspx.vb" Inherits="NFTA_Metro._dp" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Dump the Pump</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail" />
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
         
         .submit {text-align:center;}
         
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
   
    <p align="center"><img src="/img/dumppump.jpg" width="412" height="139" alt="Dump the Pump" /></p>
    <asp:PlaceHolder runat="server" ID="phSent" Visible="false">
        <p align="center"><br /><br /><b>Thank you.  Your testimonial has been sent.</b><br /><br /></p>
    </asp:PlaceHolder>
        
    <asp:PlaceHolder runat="server" ID="phForm">
    	<br />
    	
    	
    	<div id="countdown" style="height:117px; background-image:url('http://www.apta.com/members/memberprogramsandservices/advocacyandoutreachtools/dumpthepump/PublishingImages/dtp-count-2014-bg-large.gif');background-repeat:no-repeat;background-position: center 5px; font-family:Arial,sans-serif;background-color: #fff; color: #d63227; font-size: 30px;letter-spacing:-2px;border: 3px solid #D63227; margin: auto; text-align: left; width: 255px; font-weight:bold;">
            <div style="margin-left:2px;width:190px;margin-top:20px;margin-bottom:0;">
                <div id="countbox">
                    <script type="text/javascript">
                        dateFuture = new Date(2014,5,19,00,00,01);
                        dateNow = new Date();
	                    amount = dateFuture.getTime() - dateNow.getTime();
	                    delete dateNow;
	                    // time is already past
	                    if(amount < 0){
		                    document.write("Today is");
	                    }
	                    // date is still good
	                    else{
		                    days=0;hours=0;mins=0;secs=0;out="";
		                    amount = Math.floor(amount/1000);
		                    days=Math.floor((amount/86400)+1);
		                    amount=amount%86400;
		                    if(days != 0){out += days +" Day"+((days!=1)?"s":"")+" Until";}
                            document.write(out);
                        } 
                    </script>
                </div>
            </div>
        </div>
    	<p>NFTA Metro is preparing for a local ad campaign that will use testimonials from current riders affirming their personal benefits to using public transit.</p>
    	
    	<p>The registation period has ended.  Thank you to everyone who submitted testimonials.  Winners will be posted here.  Check back soon.</p>
    	
    	
    	<p align="center">
    	<img src="/img/dp1.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp2.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp3.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp4.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp5.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp6.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp7.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp8.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	<img src="/img/dp9.jpg" width="180" height="624" alt="Dump the Pump Testimonial" />
    	
    	
    	</p>
    	
    	<!--
    	<p>Tell us in 40 words or less how using Metro Bus and/or Rail benefits you and you could be selected for NFTA Metro's Dump the Pump Campaign. Your photo may be reqested.</p>
    	<p>Upon receipt of your testimonial, your name will be entered into a random drawing to WIN one of five Metro Prizes (which may include a free monthly pass). The winners of prizes may not necessarily be chosen to participate in the ad campaign.  Limit one entry per person.  </p>

        <div class="mapfeedback">  
            <p class="name">  
                <label for="name"><br />Name:</label><br />
                <asp:TextBox ID="tbName" runat="server" />   
            </p> 
            <p class="company">  
                <label for="company"><br />Company/Association:</label><br />
                <asp:TextBox ID="tbCompany" runat="server" />   
            </p> 
            <p class="phone">  
                <label for="phone"><br />Phone:</label><br />
                <asp:TextBox ID="tbPhone" runat="server" />   
            </p> 
            <p class="email">  
                <label for="email">Email:</label> <br />
                <asp:TextBox ID="tbEmail" runat="server" /></asp:TextBox>
            </p>  
            <p class="Comments">  
                <br /><label for="comments">Using Metro benefits me because:</label> <br />
                <asp:TextBox TextMode="multiline" Columns="60" Rows="12" ID="tbComments" runat="server" />  
            </p>  
            
            <p class="submit"><asp:Button ID="btnSubmit" runat="server" Text="Submit" /></p>  
        
            <br /><br />
        </div>     
         
        <p><b>All submissions must be submitted no later than May 20, 2014 </b></p>  
        <p class="txtSmall">No purchase necessary. The submission of this form provides Niagara Frontier Transportation Authority and Niagara Frontier Transit Metro System, Inc. and its representatives, employees, agents and assigns, the irrevocable and unrestricted right to use reproduce and publish these comments for editorial, trade, advertising or any other purpose or medium; to edit the same without restriction; and to copyright the same. Submission of this form releases NFTA and NFT Metro from all claims, actions and liability relating to its use of said comments and participation in the 2014 Dump the Pump Campaign.  Individuals providing testimonials must use their Metro transit pass at a minimum of three days per week.  If selected, participants must sign a release for use of testimonial/photo as part of ad campaign(s).  Employees/family members of employees of NFTA/Metro and Advertising Partners are ineligible to participate.</p> 
        -->
   
        </asp:PlaceHolder>
        
        
        
    
    	
</asp:Content>
