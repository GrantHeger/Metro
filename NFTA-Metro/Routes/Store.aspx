<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Store.aspx.vb" Inherits="NFTA_Metro._Store"  MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Metro Store</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />

    <style type="text/css">
    
    .ProductNoSale{
    border:4px solid #0e5195;
    border-radius:25px;
    -moz-border-radius:25px; /* Firefox 3.6 and earlier */
    background:#eeeeee;
    text-align:center;
    width:214px;
    margin:5px;
    min-height:380px;
    }
    
    .ProductSale{
    border:4px solid #0e5195;
    border-radius:25px;
    -moz-border-radius:25px; /* Firefox 3.6 and earlier */
    background:#eeeeee;
    text-align:center;
    width:214px;
    margin:5px;
    min-height:640px;
    }
    
    .ProductInfo {   padding:10px; }
    
    .ProdTitle {background:#0e5195; font-weight:900; font-size:1.1em; padding:10px 5px; color:#fff; margin:0; border:4px solid #0e5195; border-radius:18px 18px 0 0;  -moz-border-radius:18px 18px 0 0; /* Firefox 3.6 and earlier */}
 
    .Price {  font-size:1.2em; padding:10px 0;}
    .Green { font-weight:900;color:#698d1d; font-size:2.2em;}
    .Reduced {padding:0 0 10px 0;}
    .Reduced a { color:red; text-decoration:none; text-decoration:underline;}
    .Price a:hover {text-decoration:underline; color:blue;}
 
    .TxtSmall{font-size:11px;}
 
    .info {position:relative; top:8px; left:5px;}
 
 
    .Auto a {font-size:small; color:blue; text-decoration:none; line-height:40px; }
    .Auto a:hover {text-decoration:underline; color:#000;}
 
 
    .pImage {border:2px solid #0e5195;}

    .TableFares Table {
	    border-width: 1px;
	    border-spacing: 5px;
	    border-style: solid;
	    border-color: #000;
	    border-collapse: collapse;
	    background-color: #ccc;
    }
    .TableFares Table th, .TableFares Table td {
	    border-width: 2px;
	    color:#fff;
	    padding: 5px;
	    border-style: solid;
	    border-color: white;
	    background-color: #004890;
	    -moz-border-radius: 6px;
    }
    .TableFares Table td {
	    color:#000;
	    vertical-align:middle;
	    background-color: #eaeaea;
    }
    
   
    
    .TableFares Table {text-align:center;}
    .TableFares Table td.white {background-color:#fff; }
    .TableFares Table td.na {color:#999; font-weight:900;}
    .TableFares Table td a {font-size:small; color:blue; text-decoration:none; line-height:40px; }
    .TableFares Table td a:hover {text-decoration:underline; color:#000;}
    .more { color:#000; background:#fff; padding:2px; }
    a.tipLink { color:#fff;}
    
     /* tooltip styling. by default the element to be styled is .tooltip  */
      .tooltip {
        display:none;
        background:transparent url(/img/black_arrow-lg.png);
        font-size:13px;
        height:120px;
        width:340px;
        padding:25px;
        color:#eee;
        position:relative; z-index:1000;
      }
        /* style the trigger elements */
      
      
      #Fares { }
      #Fares img {
        border:0;
        cursor:pointer;
        margin:0 1px;
      }
  
        
        .fareSmall 
        {
        line-height:1.0em;
        font-size:.85em;
        font-weight:300;
        color:#fff;
        }	
        
        .blue { color:#004990;}
        
      
   
 
       .panel 
       {
       	margin:10px 0;
       	padding:0 10px 10px 10px;
        background:#eee;
        border:1px solid #999;
        -moz-border-radius: 10px;
        border-radius: 10px;
       }     
         
       .buy 
       {
       text-align:center;
       margin:10px 0;
       	padding:10px;
        background:#ccc;
        border:1px solid #999;
        -moz-border-radius: 10px;
        border-radius: 10px;
       }     
    </style>
    
    <!--[if lt IE 7]>
    <style>
      .tooltip {
        background-image:url(/img/black_arrow.gif);
      }
    </style>
    <![endif]-->
    
</asp:Content> 

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Metro Store</h1>
    
   
     <h2 class="newsheader">Metro Passes</h2>
     
     
 
    <a name="monthly" id="monthly"></a>
    <div class="panel"> 
        <p><img src="/img/fare-month.png" width="238" height="168" alt="Monthly Pass" align="right" /><b>Monthly Pass</b><br />
        Valid for unlimited rides on Metro Buses and Metro Rail during the month of issue.</p>
        <div class="Price">Pass <span class="Green">$75</span> &nbsp; Reduced fare $37.50</div>
        <p clear="all" class="fareSmall blue">Can be purchased <a href="Fares.aspx#online">online</a>, from <a href="Fares.aspx#machine">ticket vending machines</a>, or from <a href="Fares.aspx#vendors">participating retail outlets</a>.</p>
        <div class="buy">
            Pass Type:
            <select name="os0-MP">
                <option value="Regular">Regular Fare $75</option>
                <option value="Reduced">Reduced Fare $37.50</option>
            </select>
            &nbsp;&nbsp;
            Month:
            <select name="os1-MP">
                <option value="<% = Today.ToString("MMMM") %> (Current Month)"><% = Today.ToString("MMMM") %> (Current Month)</option>
                <option value="<% = DateTime.Now.AddMonths(+1).ToString("MMMM") %> (Next Month)" selected="selected"><% = DateTime.Now.AddMonths(+1).ToString("MMMM") %> (Next Month)</option>
            </select>
            <br /><br />
                                   
            <asp:ImageButton runat="server" ImageUrl="/img/buynow.gif" ID="ImageButton1" OnClick="btnMP_Click"></asp:ImageButton>
             <br /><br />                     
            <span class="fareSmall"><a href="AutoPassPayments.aspx">&#187;set up auto payments</a></span>
        </div>
    </div>
    
    <a name="pal" id="pal"></a>
    <div class="panel"> 
        <p><img src="/img/fare-pal.png" width="238" height="168" alt="PAL Pass" align="right" /><b>PAL Pass</b><br />
        Curb to curb, lift equipped van service called Paratransit Access Line (PAL) is available for <a href="/Paratransit/Paratransit.aspx">qualifying individuals</a>. PAL's service area extends three-quarters of a mile on either side of or from the end of Metro's bus and rail fixed route service. This does not apply to commuter or express route service.</p>
        <div class="Price">10 Trips <span class="Green">$35</span> &nbsp; 20 Trips <span class="Green">$70</span></div>
        <p clear="all" class="fareSmall blue">Can be purchased at <a href="Fares.aspx#machine">ticket vending machines</a> or <a href="Fares.aspx#online">online</a>.</p>
       <div class="buy">
            Select Trips:
                 <select name="os0-PAL">
                    <option value="10Trips">10 Trips $35</option>
                    <option value="20Trips">20 Trips $70</option>
                </select>                                 
                <br /><br />
                <asp:ImageButton runat="server" ImageUrl="/img/buynow.gif" ID="ImageButton2" OnClick="btnPAL_Click"></asp:ImageButton>
            <br /><br />                              
            <span class="fareSmall"><a href="AutoPassPayments.aspx">&#187;set up auto payments</a></span>
        </div>
    </div>

    <a name="summer" id="summer"></a>
    <div class="panel"> 
        <p><img src="/img/fare-sgp.png" width="238" height="168" alt="Summer Go Pass" align="right" /><b>Summer Go Pass</b><br />
        Valid individuals 17 years of age and under for rides on Metro Buses and Metro Rail during the summer months.</p>
        <div class="Price">Pass <span class="Green">$60</span></div>
        <p clear="all" class="fareSmall blue">Can be purchased <a href="Fares.aspx#online">online</a>, or from <a href="Fares.aspx#vendors">participating retail outlets</a>.</p>
        <div class="buy">
              <!--<img src="/img/buynowx.gif" width="140" alt="Not Available at this time" />
                <br /><br />
              
                <span class="fareSmall blue">Not Available at this time</span>
               -->
                <br />
                  
                <asp:ImageButton Visible="True" runat="server" ImageUrl="/img/buynow.gif" ID="btnSYP" OnClick="btnSYP_Click"></asp:ImageButton>
            <br /><br /> 
        </div>
    </div>


       <p><b><em>Online Shopping Policy</em></b> - We are not responsible for lost or stolen passes. All sales are final. Please allow 3-5 business days to ensure delivery of your pass by the first day of the following month.</p>
        
        <p><b><em>Shipping and Delivery</em></b> - Passes for the following month will be shipped after the 15th day of the current month. Please allow 3-5 business days to ensure delivery of your pass by the first day of the following month.</p>
        <p>Passes will not be refunded or exchanged.</p>
	<p>On-line purchases are handled through PayPal. Questions regarding recurring or one-time payments should be directed to (888)-221-1161. </p>
        
        
        <p>To purchase a pass with a <b>Commuter Check MasterCard</b> please call our Cash Office monday-friday at 716.855.7200.</p>  
                                          
		
		<input type="hidden" name="currency_code" value="USD">
		<img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1">
		
	
</asp:Content>
