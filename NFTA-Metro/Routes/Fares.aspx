<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Fares.aspx.vb" Inherits="NFTA_Metro._Fares"  MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Metro Fares</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />

    <script type="text/JavaScript" src="/js/jquery-1.5.2.min.js"></script>    
    
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
  
        .fareHeading 
        {
        	background:#004990;
        	font-weight:900;
        	text-align:center;
        	color:#edc93e;
        	line-height:1.4em;
        	font-size:1em;
        }
        
        .fareHeading a 
        {
            color:#edc93e;
            
        }
        
        .fareSmall 
        {
        line-height:1.0em;
        font-size:.85em;
        font-weight:300;
        color:#fff;
        }	
        
        .blue { color:#004990;}
        
        .fareName 
        {
         text-align:left;
         background:#e5e5e5;
         font-weight:900;
         color:#004990;
         line-height:1em;
         padding-left:10px;
         font-size:.9em;
        }
        .fareReg
        {
         text-align:center;
         background:#dfe3e8;
         font-weight:900;
        }
        .fareRed
        {
         text-align:center;
         background:#e5e5e5;
        }
        .fareBuy
        {
         text-align:center;
         background:#dfe3e8;
        }
        
        .fareNameAlt 
        {
         text-align:left;
         background:#eeeeee;
         font-weight:900;
         color:#004990;
         line-height:1em;
         padding-left:10px;
         font-size:.9em;
        }
        .fareRegAlt
        {
         text-align:center;
         background:#d0d7d6;
         font-weight:900;
        }
        .fareRedAlt
        {
         text-align:center;
         background:#eeeeee;
        }
        .fareBuyAlt
        {
         text-align:center;
         background:#d0d7d6;
        }
          
       .topLeft {
        -moz-border-radius-topleft: 10px;
        border-top-left-radius: 10px;
       }    
       
       .topRight {
        -moz-border-radius-topright: 10px;
        border-top-right-radius: 10px;
       } 
       
       .bottomLeft {
        -moz-border-radius-bottomleft: 10px;
        border-bottom-left-radius: 10px;
       } 
       
       .bottomRight {
        -moz-border-radius-bottomright: 10px;
        border-bottom-right-radius: 10px;
       } 
       
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
    <h1>Metro Fares</h1>
     <p>Metro tickets and passes are valid on Metro Buses and Metro Rail. Pass must be retained for duration of trip. Tokens are no longer sold, but are still accepted. </p>
     
     
     <table border="0" cellpadding="3" cellspacing="2" width="100%"  summary="This table lists  Metro fare prices effective April first, two thousand fourteen. Standard Fare is two dollars.  One day pass is five dollars.  Seven day pass is twenty five dollars.  Thirty day pass is seventy five dollars. Monthly pass is seventy five dollars, PAL Passes are thrity five dollars for ten trips, and seventy dollars for twenty trips.  Summer youth passes are sixty dollars.  Here are the reduced fares for Children five to eleven years old, Senior Citizens sixty five and older, Disabled & Medicare Recipients. Standard reduced fare is one dollar.  Reduced fare day pass is two dollars and fifty cents.  Reduced fare seven day pass is twelve dollars and fifty cents. Reduced fare thirty day pass is thirty seven dollars and fifty cents.  Reduced fare monthy pass is thirty seven dollars and fifty cents.">
        <tr>
            <td width="130" ></td>
            <td width="90" class="fareHeading topLeft" valign="top"><a href="#regular">Full Fare</a></td>
            <td class="fareHeading" valign="top"><a href="#reduced">Reduced Fare</a><br /><span class="fareSmall">Children 5-11, Seniors 65+, Disabled or Medicare</span></td>
            <td width="180" class="fareHeading topRight" valign="top"><a href="#Buy">Where to buy</a></td>
        </tr>
        <tr>
            <td class="fareName topLeft"><a href="#standard">Standard Fare</a></td> 
            <td class="fareReg">$2</td>
            <td class="fareRed">$1</td>
            <td class="fareBuy">
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#operator"><img src="/img/icon-operator.png" class="domroll /img/icon-operator-o.png" alt="Pay operator upon boarding"  width="35" height="35" /></a>
                <a href="#machine"><img src="/img/icon-machine.png" class="domroll /img/icon-machine-o.png" alt="Ticket vending machines" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />                
            </td>   
        </tr>   
        <tr>
            <td class="fareNameAlt"><a href="#day">Day Pass</a></td> 
            <td class="fareRegAlt">$5</td>
            <td class="fareRedAlt">$2.50</td> 
            <td class="fareBuyAlt">
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#operator"><img src="/img/icon-operator.png" class="domroll /img/icon-operator-o.png" alt="Pay operator upon boarding"  width="35" height="35" /></a>
                <a href="#machine"><img src="/img/icon-machine.png" class="domroll /img/icon-machine-o.png" alt="Ticket vending machines" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />
            </td>  
        </tr>
        <tr>
            <td class="fareName"><a href="#seven">7 Day Pass</a></td> 
            <td class="fareReg">$25</td>
            <td class="fareRed">$12.50</td>
            <td class="fareBuy">
                <img src="/img/s.png" width="35" height="35" alt="" />
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#machine"><img src="/img/icon-machine.png" class="domroll /img/icon-machine-o.png" alt="Ticket vending machines" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />                
            </td>   
        </tr>
        <tr>
            <td class="fareNameAlt"><a href="#thirty">30 Day Pass</a></td> 
            <td class="fareRegAlt">$75</td>
            <td class="fareRedAlt">$37.50</td>   
            <td class="fareBuyAlt">
                <img src="/img/s.png" width="35" height="35" alt="" />
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#machine"><img src="/img/icon-machine.png" class="domroll /img/icon-machine-o.png" alt="Ticket vending machines" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />                
            </td>
        </tr>
        <tr>
            <td class="fareName"><a href="#monthly">Monthly Pass</a></td> 
            <td class="fareReg">$75</td>
            <td class="fareRed">$37.50</td> 
            <td class="fareBuy">
                <a href="#online"><img src="/img/icon-online.png" class="domroll /img/icon-online-o.png" alt="Purchase online" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#machine"><img src="/img/icon-machine.png" class="domroll /img/icon-machine-o.png" alt="Ticket vending machines" width="35" height="35" /></a>
                <a href="#vendor"><img src="/img/icon-vendor.png" class="domroll /img/icon-vendor-o.png" alt="Available at retail outlets" width="35" height="35" /></a>                
            </td>    
        </tr> 
        <tr>
            <td class="fareNameAlt"><a href="#pal">PAL Pass</a><br /><span class="fareSmall blue">(10 Trips)</span></td> 
            <td class="fareRegAlt">$35</td>
            <td class="fareRedAlt">&#8212;</td>   
            <td class="fareBuyAlt">
                <a href="#online"><img src="/img/icon-online.png" class="domroll /img/icon-online-o.png" alt="Purchase online" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#machine"><img src="/img/icon-machine.png" class="domroll /img/icon-machine-o.png" alt="Ticket vending machines" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />                
            </td>
        </tr>
        <tr>
            <td class="fareName"><a href="#pal">PAL Pass</a><br /><span class="fareSmall blue">(20 Trips)</span></td> 
            <td class="fareReg">$70</td>
            <td class="fareRed">&#8212;</td>   
            <td class="fareBuy">
                <a href="#online"><img src="/img/icon-online.png" class="domroll /img/icon-online-o.png" alt="Purchase online" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#machine"><img src="/img/icon-machine.png" class="domroll /img/icon-machine-o.png" alt="Ticket vending machines" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />                
            </td>
        </tr>
        <tr>
            <td class="fareNameAlt bottomLeft"><a href="#summer">Summer Go Pass</a></td> 
            <td class="fareRegAlt">$60</td>
            <td class="fareRedAlt">&#8212;</td> 
            <td class="fareBuyAlt bottomRight">
                <a href="#online"><img src="/img/icon-online.png" class="domroll /img/icon-online-o.png" alt="Purchase online" width="35" height="35" /></a>
                <img src="/img/s.png" width="35" height="35" alt="" />
                <img src="/img/s.png" width="35" height="35" alt="" />
                <a href="#vendor"><img src="/img/icon-vendor.png" class="domroll /img/icon-vendor-o.png" alt="Available at retail outlets" width="35" height="35" /> </a>               
            </td>  
        </tr>     
     </table>
            
            
    <a name="Buy" id="Buy"></a>  

    <br />
    <p class="BlueBar">Where to buy fares</p>
    <p>You can buy Metro fares at the locations listed below. Please note: Not all tickets and passes are available at every location.</p> 
     
    <div class="panel">
        <a name="online" id="online"></a>
        <p><img src="/img/icon-online.png" alt="available online" width="35" height="35" align="left" hspace="5" vspace="5"  /><b>You can purchase Metro Passes online</b></p>
        <p align="center"><a href="Store.aspx"><img src="/img/metro-store.png" class="domroll /img/metro-store-o.png" widht="200" height="59" alt="Metro Store" /></a></p>
        
        <p><b><em>Online Shopping Policy</em></b> - We are not responsible for lost or stolen passes. All sales are final. Please allow 3-5 business days to ensure delivery of your pass by the first day of the following month.</p>
        
        <p><b><em>Shipping and Delivery</em></b> - Passes for the following month will be shipped after the 15th day of the current month. Please allow 3-5 business days to ensure delivery of your pass by the first day of the following month.</p>
	<p>Passes will not be refunded or exchanged.</p>
	<p>On-line purchases are handled through PayPal. Questions regarding recurring or one-time payments should be directed to (888)-221-1161. </p>
        
        <p>To purchase a pass with a <b>Commuter Check MasterCard</b> please call our Cash Office monday-friday at 716.855.7200.</p>    
    </div>
    
    <div class="panel">
        <a name="operator" id="operator"></a>
        <p><img src="/img/icon-operator.png" alt="available from your operator" width="35" height="35" align="left" hspace="5" vspace="5" /><b>Paid upon boarding.</b><br />Exact fare only. Operators do not give change.</p>
    </div>
    
    <div class="panel"> 
        <a name="machine" id="machine"></a>
        <p><img src="/img/icon-machine.png" alt="purchase through ticket vending machines" width="35" height="35" align="left" hspace="5" vspace="5"  /><b>Ticket vending machines</b><br />Ticket Vending Machines are located in all Metro Rail stations, Metropolitan Transportation Center, Portage Road Transportation Center and Niagara Falls Transportation Center. These machines accept Metro tokens, U.S. coins, $1, $5, $10 or $20 bills. For Metro Rail you may purchase a round trip ticket or a one-way ticket. Please note, one-way tickets are only valid in one direction and for one hour after purchase.</p>
    </div>
    
    <div class="panel">
        <a name="vendors" id="vendors"></a>
        <p><img src="/img/icon-vendor.png" alt="available at locations" width="35" height="35" align="left" hspace="5" vspace="5"  /><b>Available at the following retail outlets</b><br />Tops, Dash's Market, and Parkside Pharmacy<br />
        </p>
    </div>             
        
       
    <a name="types" id="types"></a>
     
     <br />      
    <p class="BlueBar">Types of fares</p>
   
    <div class="panel">
        <a name="regular" id="regular"></a>            
        <p><b>Full Fare</b> - Available for Metro riders ages 12-64. Children four and under ride free with and adult, limit three children per fare paying adult.</p>
    </div>
     
    <div class="panel">
        <a name="reduced" id="reduced"></a>
        <p><b><a href="/Paratransit/FaresReduced.aspx">Reduced Fare</a></b> - Available for youth ages five to 11 years old, Seniors ages 65 and older, disabled and Medicare with proper ID.<br />
        Proof of eligibility required. <a href="/Paratransit/FaresReduced.aspx">Learn more</a></p>
    </div>
    
    <div class="panel">
        <p><b><a href="/Paratransit/Paratransit.aspx">Paratransit Access Line (PAL) </a></b> - Includes PAL 10 and 20 trip passes for individuals with disabilities.  <a href="/Paratransit/Paratransit.aspx">Learn more</a></p>
    </div>   
    
    <a name="passes" id="passes"></a>
    
    <br />
    <p class="BlueBar">Fares and passes</p>
    
    <a name="standard" id="standard"></a>
    <div class="panel">
        <p><b>Standard Fare</b><br />
        Valid for a single ride on Metro Buses and Metro Rail.</p>
        <div class="Price">Fare <span class="Green">$2</span> &nbsp; Reduced fare $1</div>
        <p clear="all" class="fareSmall blue">Can be purchased <a href="#operator">upon boarding</a>, or from <a href="#machine">ticket vending machines</a>.</p>
    </div>
     
    <a name="day" id="day"></a>
    <div class="panel"> 
        <p><img src="/img/fare-day.png" width="238" height="168" alt="Day Pass" align="right" /><b>Day Pass</b><br />
        Valid on the service day of purchase (5 a.m. to 2 a.m.)for rides on Metro Buses and Metro Rail.</p>
        <div class="Price">Pass <span class="Green">$5</span> &nbsp; Reduced fare $2.50</div>
        <p clear="all" class="fareSmall blue">Can be purchased <a href="#operator">upon boarding</a>, or from <a href="#machine">ticket vending machines</a>.</p>
    </div>
    
    <a name="seven" id="seven"></a>
    <div class="panel"> 
        <p><img src="/img/fare-7Day.png" width="238" height="168" alt="Seven Day Pass" align="right" /><b>Seven Day Pass</b><br />
        Valid for seven calandar days beginning with the day of purchase for rides on Metro Buses and Metro Rail.</p>
        <div class="Price">Pass <span class="Green">$25</span> &nbsp; Reduced fare $12.50</div>
        <p clear="all" class="fareSmall blue">Can be purchased from <a href="#machine">ticket vending machines</a> only.</p>
    </div>

    <a name="thirty" id="thirty"></a>
    <div class="panel"> 
        <p><img src="/img/fare-30day.png" width="238" height="168" alt="30 Day Pass" align="right" /><b>30 Day Pass</b><br />
        Valid for 30 calendar days beginning with the day of purchase for rides on Metro Buses and Metro Rail.</p>
        <div class="Price">Pass <span class="Green">$75</span> &nbsp; Reduced fare $37.50</div>
        <p clear="all" class="fareSmall blue">Can be purchased from <a href="#machine">ticket vending machines</a> only.</p>
    </div>
    
    <a name="monthly" id="monthly"></a>
    <div class="panel"> 
        <p><img src="/img/fare-month.png" width="238" height="168" alt="Monthly Pass" align="right" /><b>Monthly Pass</b><br />
        Valid for unlimited rides on Metro Buses and Metro Rail during the month of issue.</p>
        <div class="Price">Pass <span class="Green">$75</span> &nbsp; Reduced fare $37.50</div>
        <p clear="all" class="fareSmall blue">Can be purchased <a href="#online">online</a>, from <a href="#machine">ticket vending machines</a>, or from <a href="#vendors">participating retail outlets</a>.</p>
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
        <p clear="all" class="fareSmall blue">Can be purchased at <a href="#machine">ticket vending machines</a> or <a href="#online">online</a>.</p>
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
        <p clear="all" class="fareSmall blue">Can be purchased <a href="#online">online</a>, or from <a href="#vendors">participating retail outlets</a>.</p>
        <div class="buy">
              <!--<img src="/img/buynowx.gif" width="140" alt="Not Available at this time" />
                
                <br /><br />
             
         <span class="fareSmall blue">Not Available at this time</span>-->
             
             <br />
                  
                <asp:ImageButton Visible="True" runat="server" ImageUrl="/img/buynow.gif" ID="btnSYP" OnClick="btnSYP_Click"></asp:ImageButton>
            <br /><br /> 
        </div>
    </div>


                                        
		
		<input type="hidden" name="currency_code" value="USD">
		<img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1">
		
	
</asp:Content>
