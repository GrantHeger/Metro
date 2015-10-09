<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DefaultDb.aspx.vb" Inherits="NFTA_Metro._DefaultDb" MasterPageFile="~/Metro.Master" EnableEventValidation="false" %>

<asp:Content ID="phHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail" />
    <link href="css/cssTripPlanner.css" rel="stylesheet" type="text/css" />
    
    <!-- Removing The Default Zoom on Mobile -->
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
        
        <!-- Latest compiled and minified Bootstrap core CSS -->
        <link href="/bootstrap-3.2.0-dist/css/bootstrap.min.css" rel="stylesheet" /> 
        <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"> -->

        <!-- Optional Bootstrap theme -->
        <link href="/bootstrap-3.2.0-dist/css/bootstrap-theme.min.css" rel="stylesheet" /> 
        <!-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap-theme.min.css"> -->
        <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
    
    <style type="text/css">
        #Emergency {text-align:center; padding:5px 0; margin:0; background:#b91d25; color:#fff; font-size:16px; font-weight:900; width:966px;}

          #MessageBoard
          {
            background:#fff;
            margin:30px 4px;
            padding:10px 10px 10px 10px;
            border:0px solid black;
        	border-radius:8px; /*CSS3 border radius*/
            -moz-border-radius:8px;
            -webkit-border-radius:8px;	
            box-shadow:0 0 15px #959595; /*CSS3 shadow*/
            -moz-box-shadow:0 0 15px #959595;
            -webkit-box-shadow:0 0 15px #959595;
            }
            
          #MessageBoard a {color:#004990}
          
          #MessageBoard hr
          {
          	color: #fff;
            background-color: #fff;
            height: 1px;
            border:0;
            border-bottom:1px dashed #ccc;
          }
          
          #toggle 
          {
            font-size:.8em; 
            text-align:right;
            margin:10px 20px;	
          }	
    </style>
</asp:Content>

<asp:Content ID="EmergencyMessagePH" ContentPlaceHolderID="EmergencyMessagePH" runat="server">
    <div id="Emergency" <asp:Literal runat="server" ID="EmergencyStatus" />>
        <center><asp:Literal runat="server" ID="EmergencyText" /></center>
	</div>				
</asp:Content>

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
   
    <div id="routeStatusBox">
        <p style="padding:0; margin:0;"><img src='/img/SytemStatus.png' width='563' height='74' alt='Route Status' /></p>
        <div id="toggle">
            <asp:Button runat="server" ID="btnToggle" Text="Show Route Names" OnClick="btnToggle_OnClick" class="btn btn-primary btn-xs" />
        </div>
        

        <asp:Panel id="Panel1" runat="server">
            <asp:Repeater ID="rptRoutes1" runat="server">
                <HeaderTemplate>
                    <div id="routeStatus">
                        
                    
                </HeaderTemplate>
                <ItemTemplate>
                    <!--Route Status Message Modal Window-->
                    <div class="modal fade" id="route<%#Eval("RouteId")%>" tabindex="-1" role="dialog" aria-labelledby="Route #1" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header header<%#Eval("StatusColor")%>">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title"><%#Eval("RouteDisplayNumber")%> - <%#Eval("RouteName")%></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="posted">Posted: <%#Eval("DateAdded")%></div>
                                    <%#Eval("RouteStatusMessage")%>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    <asp:Button runat="server" CssClass="btn btn-primary" ID="Button1" Text="View Route Details" CommandArgument=<%#Eval("RouteNumber")%> CommandName="AddLink" OnClick="btnRouteDetails_OnClick"/>
                                </div>
                            </div>
                        </div>
                    </div>
              
                    <ul id="routeBtns">
                        <li><a data-toggle="modal" href="#route<%#Eval("RouteId")%>" class="btn btn-primary btn-lg color<%#Eval("StatusColor")%>"><asp:Label runat="server" ID="lblDisplay"></asp:Label><%#Eval("RouteDisplayNumber")%></a></li>
                    </ul>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
                
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel id="Panel2" runat="server" visible="false">
            <asp:Repeater ID="rptRoutes2" runat="server">
                <HeaderTemplate>
                    <div id="routeStatus">
                        
                    
                </HeaderTemplate>
                <ItemTemplate>
                    <!--Route Status Message Modal Window-->
                    <div class="modal fade" id="route<%#Eval("RouteId")%>" tabindex="-1" role="dialog" aria-labelledby="Route #1" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header header<%#Eval("StatusColor")%>">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title"><%#Eval("RouteDisplayNumber")%> - <%#Eval("RouteName")%></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="posted">Posted: <%#Eval("DateAdded")%></div>
                                    <%#Eval("RouteStatusMessage")%>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    <asp:Button runat="server" CssClass="btn btn-primary" ID="Button1" Text="View Route Details" CommandArgument=<%#Eval("RouteNumber")%> CommandName="AddLink" OnClick="btnRouteDetails_OnClick"/>
                                </div>
                            </div>
                        </div>
                    </div>
              
                    <ul id="routeBtns">
                        <li><a data-toggle="modal" href="#route<%#Eval("RouteId")%>" class="btn btn-primary btn-lg color<%#Eval("StatusColor")%>"><%#Eval("RouteNumberAndName")%></a></li>
                    </ul>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
                
            </asp:Repeater>   
        </asp:Panel>
        <div class='clearAll keepInformed'><br /><a class="InstantUpdates" href='http://alerts.nfta.com'><img src='/img/footer-iu.png' class='domroll /img/footer-iu-o.png' width='50' alt='Instant Updates'></a> Receive status alerts by text or email<br />or follow us on Twitter <a href='http://twitter.com/NFTAMetro' target='_new'><img src='/img/icon-twitter.png' class='domroll /img/icon-twitter-o.png' height='29' alt='Follow us on Twitter' /></a></div>
    </div>
        
    <!--<div id="homeAlerts">
        <p><img src='/img/riderAlerts.png' width='240' height='50' alt='Rider Alerts' /></p>
        <div id="homeAlertsItems">
            <asp:PlaceHolder ID="phNoAlerts" runat="server">
               There are no rider alerts at this time.<br /><br />
            </asp:PlaceHolder>
    	  	<asp:Literal ID="litAlertLinks" runat="server" />  
    	  	<br clear="all" />          
        </div>
    </div>  -->
            
    <asp:Literal runat="server" ID="MessageBoardText" />
    
    
    <!-- Latest compiled and minified JavaScript -->
    <script type='text/javascript' src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <!--<script type='text/javascript' src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>-->

</asp:Content>


				
