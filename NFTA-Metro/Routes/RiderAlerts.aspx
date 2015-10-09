<%@ Page Title="Niagara Frontier Transportation Authority (NFTA) - Alerts" Language="vb" AutoEventWireup="false" CodeBehind="RiderAlerts.aspx.vb" Inherits="NFTA_Metro._RiderAlerts" 
    MasterPageFile="~/Metro.Master" %>
<%@ MasterType VirtualPath="~/Metro.Master" %>    


<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Rider Alerts</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />

    <style type="text/css">
        h2.acc_trigger {
	    padding: 0;	margin: 0 0 5px 0;
	    background: url(../img/h2_trigger_a.gif) no-repeat;
	    height: 46px;	line-height: 46px;
	    width: 500px;
	    font-family:Arial, Sans-Serif;
	    font-size: 1.2em;
	    font-weight: 900;
	    float: left;
        }
        h2.acc_trigger a {
	        color: #fff;
	        text-decoration: none;
	        display: block;
	        padding: 0 0 0 50px;
        }
        h2.acc_trigger a:hover {
	        color: #ccc;
        }
        h2.active {background-position: left bottom;}
        .acc_container {
	        
	        overflow: hidden;
	        font-size: 1.2em;
	        width: 500px;
	        clear: both;
	       
        }
        .acc_container .block {
	        padding: 20px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            //Set default open/close settings
            $('.acc_container').hide(); //Hide/close all containers
            $('.acc_trigger:first').addClass('active').next().show(); //Add "active" class to first trigger, then show/open the immediate next container

            //On Click
            $('.acc_trigger').click(function() {
                if ($(this).next().is(':hidden')) { //If immediate next container is closed...
                    $('.acc_container').hide("slow"); //Hide/close all containers
                    $('.acc_trigger').removeClass('active').next().slideUp(); //Remove all "active" state and slide up the immediate next container
                    $(this).toggleClass('active').next().slideDown("slow"); //Add "active" state to clicked trigger and slide down the immediate next container
                } else {
                    $('.acc_container').hide("slow"); //Hide/close all containers
                }
                return false; //Prevent the browser jump to the link anchor
            });
        });
    </script>
    
</asp:Content> 

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Rider Alerts</h1>
    <div id="alerts" style="padding-left:50px;">
		    
		    <asp:PlaceHolder ID="phNoAlerts" runat="server">
		        <p>There are no active rider alerts at this time.</p>
		    </asp:PlaceHolder>
		    
            <asp:Repeater ID="rptCategories" runat="server">
                <ItemTemplate>
                    <h2 class="acc_trigger"><a href="#"><%#Eval("CategoryName")%></a></h3>
                    <div class="acc_container">
                        <div class="block">
                            <asp:Repeater ID="rptAlerts" runat="server" >
                                <ItemTemplate>
                                    <div style="margin:5px 0px; padding:10px; background:#dadada">
                                        <div style="text-align:right; font-size:.8em; padding-bottom:8px;"><em>posted: </em><strong><%#Format(Eval("DateAdded"), "d")%> <%#Format(Eval("DateAdded"), "t")%></strong></div>
                                        <div><%#Eval("MessageEmail")%></div>
                                    </div> 
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
  		        </ItemTemplate>
            </asp:Repeater>
        </div>
</asp:Content>

