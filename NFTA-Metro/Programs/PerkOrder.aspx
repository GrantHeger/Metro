<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PerkOrder.aspx.vb" Inherits="NFTA_Metro._PerkOrder" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Metro Perk</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail" />

   
      
    
    <style type="text/css">
        .perkOnline {
            width:500px; text-align:center; margin:0 auto;
        }
        input, .ddown{   
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
        
        .ddown { width:222px;} 
      
        input:hover, input:focus, {   
            border-color: #C9C9C9;   
            -webkit-box-shadow: rgba(0, 0, 0, 0.15) 0px 0px 8px;  
        }  
        label {   
            width:120px;
            text-align:left;
            display:inline-block;
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
         .passType input { width:16px; }
        .passType .lbType { width:200px;}
        
        .passTotal {border-top:1px solid #000; padding-top:10px;}
                 
        .note {
            font-size:.9em; color: #999999; text-align:center;
        }
    </style>
 
</asp:Content>



<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Metro Perk</h1>
    <img src="/img/logos/perk.gif" width="222" height="60" alt="Metro Perk logo" />
    	
    	
    	<asp:PlaceHolder runat="server" ID="phForm">
    	
    	<p>Employers can order Metro Perk passes using one of the following three methods.</p>
    	
    	<table cellpadding="10" border="0">
    	    <tr>
    	        <td align="left" valign="middle"><img src="/img/perk1.gif" width="69" height="55" alt="Metro Perk Phone Order" /></td>
    	        <td align="left" valign="middle"><span style="color:#6eb43f; font-size:1.2em; font-weight:900;">Phone</span><br />  Call NFTA-Metro's Cash Management Office at 716.855.7202.</td>
    	    </tr>
    	    <tr>
    	        <td align="left" valign="middle"><img src="/img/perk2.gif" width="69" height="55" alt="Metro Perk Mail Order" /></td>
    	        <td align="left" valign="middle"><span style="color:#6eb43f; font-size:1.2em; font-weight:900;">Mail</span><br /><a href="/pdfs/Perk.pdf" target="_perk1" >Download this form</a>, print it, and mail or fax it back to:<br />
    	            <p style="padding-left:30px;">
                        NFTA-Metro Cash Management Office<br />
                        Attn: Metro Perk<br />
                        181 Ellicott Street Buffalo, NY 14203<br />
                        fax: 716.855.7200</p>
    	         </td>
    	    </tr>
    	    <tr>
    	        <td align="left" valign="middle"><img src="/img/perk3.gif" width="69" height="55" alt="Metro Perk Online Order" /></td>
    	        <td align="left" valign="middle"><span style="color:#6eb43f; font-size:1.2em; font-weight:900;">Online</span><br />  By filling out the form below and clicking the "Order" button.</td>
    	    </tr>
    	</table>
        
        <p style="border-top:5px solid #6eb43f; padding-top:10px;">&nbsp;</p>
        
        
        
            

        <div class="perkOnline">  
            <p class="date">  
                <label for="name">Date</label> 
                <asp:TextBox ID="tbDate" runat="server" />   
            </p> 
            <p class="company">  
                <label for="company">Company*</label>  
                <asp:TextBox ID="tbCompany" runat="server" /></asp:TextBox>
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbCompany" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" /> 
            </p>  
            <p class="address">  
                <label for="address">Street Address*</label>  
                <asp:TextBox ID="tbAddress" runat="server" />  
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbAddress" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" /> 
            </p>  
            <p class="city">  
                <label for="city">City*</label>  
                <asp:TextBox ID="tbCity" runat="server" />  
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbCity" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" /> 
            </p>
            <p class="state">  
                <label for="state">State*</label>  
                <asp:DropDownList ID="ddlState" CssClass="ddown" runat="server" />
                <asp:CompareValidator ID="CompareValidator2" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="ddlState" Display="Dynamic" ErrorMessage="Required" ForeColor="Red" runat="server" ValidationGroup="valSignup" />
            </p>
            <p class="zip">  
                <label for="zip">Postal Code*</label>  
                <asp:TextBox ID="tbZip" runat="server" />  
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbZip" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" /> 
            </p>
            <p class="contact">  
                <label for="tbContact">Contact Name*</label>  
                <asp:TextBox ID="tbContact" runat="server" />  
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbContact" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" />  
            </p>
            <p class="contactEmail">  
                <label for="contactEmail">Contact Email*</label>  
                <asp:TextBox ID="tbContactEmail" runat="server" />  
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbContactEmail" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" />  
            </p>  
            <p class="phone">  
                <label for="contactPhone">Contact Phone*</label>  
                <asp:TextBox ID="tbContactPhone" runat="server" />  
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbContactPhone" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" />  
            </p>
            
            
            <p class="note" style="border-top:1px solid #999999;  margin-top:22px; padding-top:10px;">Enter the number of passes requested for each pass type</p>
            
            
            <table cellpadding="10" align="center">
                <tr> 
                    <td class="product" align="left">Full Fare</td>
                    <td class="passType"><asp:TextBox ID="tbFull" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> @ </td>
                    <td class="price-item-1">$75 each </td> 
                </tr>
                <tr> 
                    <td class="product" align="left">Reduced Fare**</td> 
                    <td class="passType"><asp:TextBox ID="tbReduced" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> @ </td>
                    <td class="price-item-2">$37.50 each</td>
                </tr>
                <tr> 
                    <td class="product" align="left">10 trip PAL Pass***</td>
                    <td class="passType"><asp:TextBox ID="tbPal10" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> @ </td>
                    <td class="price-item-3">$35 each</td> 
                </tr>
                <tr> 
                    <td class="product" align="left">20 trip PAL Pass**</td>
                    <td class="passType"><asp:TextBox ID="tbPal20" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> @ </td>
                    <td class="price-item-4">$70 each</td>
                </tr>
           
              
            </table>
            
            
            <!-- 

            <table cellpadding="10" align="center">
                <tr> 
                    <td class="product" align="left">Full Fare</td>
                    <td class="passType"><asp:TextBox ID="XXXXXXXXtbFull" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> x </td>
                    <td class="price-item-1">$75</td> 
                    <td class="note"> = </td>
                    <td class="total-item-1">$0</td> 
                </tr>
                <tr> 
                    <td class="product" align="left">Reduced Fare**</td> 
                    <td class="passType"><asp:TextBox ID="XXXXXXXXXtbReduced" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> x </td>
                    <td class="price-item-2">$37.50</td>
                    <td class="note"> = </td> 
                    <td class="total-item-2">$0</td> 
                </tr>
                <tr> 
                    <td class="product" align="left">10 trip PAL Pass***</td>
                    <td class="passType"><asp:TextBox ID="XXXXXXXtbPal10" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> x </td>
                    <td class="price-item-3">$35</td> 
                    <td class="note"> = </td>
                    <td class="total-item-3">$0</td> 
                </tr>
                <tr> 
                    <td class="product" align="left">20 trip PAL Pass**</td>
                    <td class="passType"><asp:TextBox ID="XXXXXXXXtbPal20" runat="server" Text="0" CssClass="pass" /></td> 
                    <td class="note"> x </td>
                    <td class="price-item-4">$70</td> 
                    <td class="note"> = </td>
                    <td class="total-item-4">$0</td> 
                </tr>
                <tr > 
                    <td colspan="6" style="border-bottom:2px solid #000;"></td>
                <tr > 
                    <td colspan="5" align="right"><strong>Payment Total:</strong></td> 
                    <td id="grandtotal">$0</td> 
                </tr> 
            </table>
            -->
        

              <p class="note" style="border-top:1px solid #999999;  margin-top:22px; padding-top:10px;">Enter the month you would like the passes for</p>
            
            
            <p class="month">  
                <label for="month">Month*</label>  
                <asp:TextBox ID="tbMonth" runat="server" />  
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbMonth" Display="Dynamic" ErrorMessage=" required" SetFocusOnError="true" ValidationGroup="valMetroPerk" />  
            </p>
        
            <p class="submit"><asp:Button ID="btnOrder" runat="server" Text="Order" ValidationGroup="valMetroPerk"  /></p>  
        
            
        </div>     
           
                
        <p>This form and payment is due by the 10th of the month preceding the month in which the passes will be used.</p> 
        
        <p style="font-size:.9em;">* Denotes a required field. ** Reduced Fares: Qualified individuals are 65+, have a Medicare card or a disability.  (For information on qualifying disabilities, call (716) 855-7360 or visit nfta.com. To take advantage of reduced fares riders must present either a Medicare card (red, white and blue), a Senior card issued by Erie or Niagara County or a reduced fare ID card issued by NFTA-Metro when paying.<br /><br />
    	*** Metro PAL services are available to registered PAL customers only.  For qualifications, <a href="http://metro.nfta.com/Paratransit/Paratransit.aspx">click here</a>.
</p>
 
        </asp:PlaceHolder>
        
        
        <asp:PlaceHolder runat="server" ID="phSent" Visible="false">
            <p><br /><br />Thank you.  Your Metro Perk form has been sent.  Please contact the NFTA-Metro Cash Management Office to make payment arrangements.<br /><br /></p>
        </asp:PlaceHolder>
        
    
        

        
        
        <p style="border-top:5px solid #6eb43f; padding-top:10px;"><em>For more information contact:</em></p>
        <p style="padding-left:30px;">
        NFTA-Metro Cash Management Office<br />
        Attn: Metro Perk<br />
        181 Ellicott Street Buffalo, NY 14203<br /><br />
        phone: 716.855.7202 | fax: 716.855.7311 | TTY/Relay 711 or 800.662.1220
    	</p>
    	
    	<p align="center" style="font-size:.8em; color:#999;"><br /><br />Metro Perk - Formerly Metro Advantage</p>
    	
    	
    
</asp:Content>

