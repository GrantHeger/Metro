<%@ Page Title="Niagara Frontier Transportation Authority (NFTA) - Metro - Schedules" Language="vb" AutoEventWireup="false" MasterPageFile="~/Metro.Master" 
    CodeBehind="Routes.aspx.vb" Inherits="NFTA_Metro.Routes" %>
<%@ Import Namespace="NFTA_Metro.nftaRoute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phHead" runat="server">
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Game Day Express" />
    
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="phContent" runat="server">
    <h2 class="newsheader"><em>Schedules</em></h2>    	
    <style type="text/css">
    #effDates {font-size:.8em; font-weight:900; line-height:1.4em;}
		.eff {font-style:italic; font-weight:300;} 
    </style>
    <div id="main" style="width:919px">
        <div>
                
                <div style="text-align:center">
				
				
				<img src="/img/keep.jpg" width="900" height="341" alt="Keep Your Schedule" /><br />
				
				
			
				<table id="effDates" width="900" cellspacing="10" align="left" >
					<tr>
						<td valign="top" align="left"> 
							Metro Rail <span class="eff">effective 10/14/13</span><br />
							1 - William <span class="eff">effective 9/1/13</span><br />
							2 - Clinton <span class="eff">effective 9/1/13</span><br />
							3 - Grant <span class="eff">effective 9/1/13</span><br />
							4 - Broadway <span class="eff">effective 9/1/13</span><br />
							5 - Niagara <span class="eff">effective 9/1/13</span><br />
							6 - Sycamore <span class="eff">effective 9/1/13</span><br />
							7 - Baynes - Richmond <span class="eff">effective 9/1/13</span><br />
							8 - Main <span class="eff">effective 9/1/13</span><br />
							11 - Colvin <span class="eff">effective 9/1/13</span><br />
							12 - Utica <span class="eff">effective 9/1/13</span><br />
							13 - Kensington <span class="eff">effective 9/1/13</span><br />
							14 - Abbott <span class="eff">effective 9/1/13</span><br />
							15 - Seneca <span class="eff">effective 9/1/13</span><br />
							16 - South Park <span class="eff">effective 9/1/13</span><br />
							18 - Jefferson <span class="eff">effective 9/1/13</span><br />
							19 - Bailey <span class="eff">effective 9/1/13</span><br />
							
						</td>
						<td valign="top" align="left">
							
							20 - Elmwood <span class="eff">effective 10/18/12</span><br />
							22 -  Porter - Best <span class="eff">effective 9/1/13</span><br />
							23 - Fillmore/Hert. <span class="eff">effective 9/1/13</span><br />
							24 - Genesee <span class="eff">effective 9/1/13</span><br />
							25 - Delaware <span class="eff">effective 9/1/13</span><br />
							26 - Delavan <span class="eff">effective 9/1/13</span><br />
							27 - Wende <span class="eff">effective 9/1/13</span><br />
							29  - Wohlers <span class="eff">effective 6/17/12</span><br />
							32 - Amherst <span class="eff">effective 9/1/13</span><br />
							34  - N. Falls Blvd. <span class="eff">effective 7/22/13</span><br />
							35 - Sheridan <span class="eff">effective 3/10/13</span><br />
							36 - Hamburg <span class="eff">effective 9/1/13</span><br />
							40 - Buff.-Niag. <span class="eff">effective 9/1/13</span><br />
							42 - Lackawanna <span class="eff">effective 3/10/13</span><br />
							44 - Lockport <span class="eff">effective 7/22/13</span><br />
							46 - Lancaster <span class="eff">effective 3/10/13</span><br />
							47 - Youngs Road <span class="eff">effective 9/1/13</span><br />
							
							
					</td>
					<td valign="top" align="left">
							
							48 - Williamsville <span class="eff">effective 9/1/13</span><br />
							49 - Millard Subruban <span class="eff">effective 7/22/13</span><br />
							50 - University <span class="eff">effective 9/1/13</span><br />
							52 - Hyde Park <span class="eff">effective 9/1/13</span><br />
							54 - Military <span class="eff">effective 9/1/13</span><br />
							55 - Pine Avenue <span class="eff">effective 9/1/13</span><br />
							57 - Tonawandas <span class="eff">effective 9/1/13</span><br />
							60 - Niagara Falls <span class="eff">effective 9/2/12</span><br />
							61 - N. Tonawanda <span class="eff">effective 6/17/12</span><br />
							64 - Lockport <span class="eff">effective 9/2/12</span><br />
							66 - Williamsville <span class="eff">effective 6/17/12</span><br />
							67 - Cleveland Hill <span class="eff">effective 6/17/12</span><br />
							68 - George Urban <span class="eff">effective 6/17/12</span><br />
							69 - Alden <span class="eff">effective 6/16/13</span><br />
							70 - East Aurora <span class="eff">effective 6/16/13</span><br />
							72 - Orchard Park <span class="eff">effective 12/2/12</span><br />
							74 - Hamburg <span class="eff">effective 12/2/12</span><br />
							
							
							
						</td>
						<td valign="top" align="left">
							75 - West Seneca <span class="eff">effective 6/17/12</span><br />
							76 - Lotus Bay <span class="eff">effective 9/1/13</span><br />
							79 - Tonawanda <span class="eff">effective 6/17/12</span><br />
							81 - East Side <span class="eff">effective 6/17/12</span><br />
							101 - No.-South <span class="eff">effective 9/1/13</span><br />
							102 - Bailey <span class="eff">effective 9/1/13</span><br />
							103 - E.-Suburban <span class="eff">effective 9/1/13</span><br />
							104 - So. Central <span class="eff">effective 9/1/13</span><br />
							106 - So.-Suburban <span class="eff">effective 9/1/13</span><br />
							110 - West-North <span class="eff">effective 9/1/13</span><br />
							111 - So.-Michigan <span class="eff">effective 9/1/13</span><br />
							112 - Grant-North <span class="eff">effective 9/1/132</span><br />
							204 - Airport <span class="eff">effective 9/1/13</span><br />
							206 - Bflo. State <span class="eff">effective 9/1/13</span><br />
							211 - ECC/Transit <span class="eff">effective 9/1/13</span><br />
						</td>
					</tr>
				</table>
				
				<!--
				<p style="text=align:center;"><a href="routechanges.aspx"><img src="/img/change20121202.jpg" width="400" height="85" alt="schedule changes" /></a></p>
			-->
				
				
			</div>

        
                           
        </div>
        <div style="clear:both; width:740px; margin:0 auto; text-align:center">
        
            <table width="740" cellspacing="0" cellpadding="0" border="0" style="margin:0 auto; text-align:center; border-collapse:collapse;">
               
                <asp:Repeater ID="rptCategories" runat="server">
                    <ItemTemplate>
                        <tr>
	                        <td width="740">
	                            <div style="padding-top:30px; text-align:left">
                                    <h2 class="routeCat"><%#Eval("CategoryName")%></h2>
                                </div>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="rptRoutes" runat="server">
                                    <HeaderTemplate><table width="740" cellpadding="0" cellspacing="0"></HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>  
                                            <td style="width:240px; border-bottom:4px double #ccc; border-top:4px double #ccc; text-align:left;">
                                                <%#Eval("RouteNumber")%>
                                                <br /><strong><%#Eval("RouteName")%></strong>
                                            <td style="width:220px; border-bottom:4px double #ccc; border-top:4px double #ccc; text-align:left;">
                                                 <%#Eval("ToolTip")%> <%#Eval("Alert")%><%#Eval("Rev")%>
                                            </td>
                                            <td style="width:280px; border-bottom:4px double #ccc; border-top:4px double #ccc; text-align:left;"><%#Eval("Icons")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate></table></FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>		        
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="phSideBar" runat="server">
</asp:Content>
