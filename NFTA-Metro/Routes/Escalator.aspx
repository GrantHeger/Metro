<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Escalator.aspx.vb" Inherits="NFTA_Metro._Escalator" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>Niagara Frontier Transportation Authority (NFTA) - Escalator Status</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail" />
    <link rel="stylesheet" media="all" type="text/css" href="http://www.nfta.com/css/accordian.css" />
</asp:Content>

<asp:Content ID="ContentContent" ContentPlaceHolderID="phContent" runat="server">
    <h2 class="newsheader"><em>Metro Escalator and Elevator Status</em></h2>    	
    <div id="main">
    	
        <asp:Repeater ID="rptEscalator" runat="server">
                <HeaderTemplate>
                    <table cellpadding="10" border="0">
                        <!--<tr>
                            <th>Station</th>
                            <th>Direction</th>
                            <th>Status</th>
                            <th>Date Modified</th>
                        </tr>   -->                 
                </HeaderTemplate>
                <ItemTemplate>
                        <tr>
                            <td><b><%#Eval("Station")%></b><br /><span class="small"><%#Eval("Combo")%></span></td>
                            <td class="small" align="center"><%#Eval("Icon")%></td>
                            <td class="small" align="center"><%#Eval("Status")%></td>
                            <td class="small"><em>last modified: </em><%#Eval("LastModified")%></td>
                            
                        </tr>                    
  		        </ItemTemplate>
  		        <FooterTemplate>
  		            </table>
  		        </FooterTemplate>
            </asp:Repeater>

   	</div>
</asp:Content>
