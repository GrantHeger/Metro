<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TitleVI.aspx.vb" Inherits="NFTA_Metro._TVI" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Title VI</title>
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
    <h1>Title VI Major Service Change and Fare Policy Amendment</h1>
    
    <asp:PlaceHolder runat="server" ID="phSent" Visible="false">
        <p align="center"><br /><br /><b>Thank you.  Your comments have been sent.</b><br /><br /></p>
    </asp:PlaceHolder>
        
    <asp:PlaceHolder runat="server" ID="phForm">
    	<br />
    	
    	<h4>Proposed NFTA Policies in Accordance with Title VI Major Service Changes and Fare Policy Amendment</h4>
    	
        <p><em>Posted on 4.16.14</em><br /><br />
        
        In response to recent changes made by the Federal Transportation Administration on the Title VI Requirements and Guidelines for Federal Transit Administration Recipient, NFTA seeks public comment on the following proposed authority policies regarding major service changes and the corresponding impacts: <br />
        <ul>
            <li>Title VI Disparate Impact Policy</li>
            <li>Disproportionate Burden Policy</li>
        </ul>
        </p>
        <h4>Title VI Major Service Changes and Fare Equity Policy</h4>
        
        <p>Major Service Change and Fare Equity Policy was adopted by the NFTA Board of Commissioners on March 12, 2012 in compliance with applicable federal requirements (Title VI of the Civil rights Act 1964, 49 CFR Section 21 and FTA Circular 4702.1B).</p>

        <p>The Federal Transit Administration (FTA) requires that recipients of FTA funding prepare and submit service and fare equity analyses for proposed major service and fare changes. The purpose of this policy is to establish a threshold that defines a major service change and a recipient’s definition of an adverse effect caused by a major service change or fare change.</p>  

        <p>A major service change is defined as a 25% reduction in the service hours or miles of any route.   All major service or fare changes will be subject to an equity analysis which includes an analysis of adverse effects. An adverse effect is defined as a geographical or temporal reduction in service which includes but is not limited to: elimination of a route, shortening a route, rerouting an existing route and an increase in headways or a change in fares. NFTA shall consider the degree of adverse effects, and analyze those effects, when planning major service changes or changes in fares.</p>
        
        <h4>Title VI Disparate Impact Policy</h4>
        
        <p>NFTA propose to establish this Disparate Impact Policy in compliance with applicable federal requirements (Title VI of the Civil rights Act 1964, 49 CFR Section 21 and FTA Circular 4702.1B).</p>

        <p>The FTA Circular 4702.1B requires that recipients of Federal Transit Administration funding prepare and submit service and fare equity analyses for proposed major service or fare changes (defined in NFTA’s Major Service Change Policy). The purpose of this policy is to establish a threshold which identifies when the adverse of a major service change or fare increase are borne disproportionately by minority populations. The Disparate Impact threshold is described as follows: A twenty percent (20%) threshold above which an impact will be deemed “a statistically significant disparity.” If the percentage difference between the minority population affected by the service or fare change is more than twenty percent (20%) above the minority population of the overall service area, a disparate impact exists.</p>

        <p>Should a proposed major service or fare change result in a disparate impact, NFTA will consider modifying the proposed change to avoid, minimize, or mitigate the disparate impact of the change. If NFTA finds potential disparate impacts and then modifies the proposed changes to avoid, minimize, or mitigate potential disparate impacts, NFTA will reanalyze the proposed changes to determine whether the modifications actually removed the potential disparate impacts of the changes.</p> 

        <h4>Disproportionate Burden Policy</h4>
        
        <p>NFTA proposes to establish this Disproportionate Burden Policy in compliance with applicable federal requirements (Executive Order 12898 and FTA Circular 4702.1B).

        <p>The FTA Circular 4702.1B requires that recipients of Federal Transit Administration funding prepare and submit service and fare equity analyses for proposed major service or fare changes (defined in NFTA’s Major Service Change Policy). The purpose of this policy is to establish a threshold which identifies when the adverse effects of a major service change or fare increase are borne disproportionately by low-income populations. For purposes of this policy, “low-income population” is defined as follows: a low-income population is any readily identifiable group of households who live in geographic proximity and whose median household income is at or below the Department of Health and Human Services Poverty Guidelines.</p>

        <p>The disproportionate burden threshold is described as follows: A twenty percent (20%) threshold above which an impact will be deemed “a statistically significant disparity.” If the percent difference between low-income population affected by the service or fare change is more than twenty percent (20%) above the low-income population of the overall service area, a disparate impact exists.</p>

        <p>Should a proposed major service or fare change result in a disproportionate burden, NFTA will consider modifying the proposed change to avoid, minimize, or mitigate the disproportionate burden of the change. If NFTA finds a potential disproportionate burden and then modifies the proposed changes to avoid, minimize, or mitigate potential disproportionate burdens, NFTA will reanalyze the proposed changes to determine whether the modifications actually removed the potential disproportionate burden of the changes.</p>
        
        
        <h4>Submit your comments</h4>
            	
        <p>To send comments related to the Title VI Major Service Change and Fare Policy Amendment fill out and submit the form below.  You may also call 716.855.7211 or email Service Planning at planning@nfta.com.</p>
      

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
                <asp:TextBox TextMode="multiline" Columns="60" Rows="12" ID="tbComments" runat="server" />  
            </p>  
            
            <p class="submit"><asp:Button ID="btnSubmit" runat="server" Text="Submit" /></p>  
        
            <br /><br />
        </div>     
           
   
        </asp:PlaceHolder>
        
        
        
    
    	
</asp:Content>
