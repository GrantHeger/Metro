<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MetroNowBoard1.aspx.vb" Inherits="NFTA_Metro._NowBoard1" MasterPageFile="~/Stripped.Master" %>

<asp:Content ID="phHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Metro Now</title>
    <META HTTP-EQUIV="REFRESH" CONTENT="5" />
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />
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
        
        <style>
        #statusAlertList{margin:1em; color:#fff;}
         .color2 { background:#b50000; } /* Red */
        .color1 { background:#f9ff00; color:#000; } /* Yellow */
        .color0 { background:#006b03; } /* Green */

        #Emergency {text-align:center; padding:5px 0; margin:0; background:#fff; color:#890000; border-bottom:5px solid #890000; font-size:16px; font-weight:900; width:100%;}
     
    </style>
</asp:Content>
<asp:Content ID="EmergencyMessagePH" ContentPlaceHolderID="EmergencyMessagePH" runat="server">
    <div id="Emergency" <asp:Literal runat="server" ID="EmergencyStatus" />>
        <center><span style="color:#ccc; font-style:italic;">Emergency Message: </span><asp:Literal runat="server" ID="EmergencyText" /></center>
	</div>				
</asp:Content>

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
   <div id="Left">
        <center>
            <img src='/img/metro-now.png' width='250' height='53' alt='Metro Now' />
            <br /><b><em>Here's the status of your bus and rail system</em></b><br /><br />
        </center>
        <div id="routeStatusBox">
            <asp:Panel id="Panel1" runat="server">
                <asp:Repeater ID="rptRoutes" runat="server">
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
        </div>
        <center>
            <img src='/img/now-key.png' width='205' height='67' alt='Metro Now' />
        </center>
    </div>
    <div id="Right">
        <div id='statusAlertList'>
            <asp:Repeater ID="rptAlerts" runat="server" >
                <ItemTemplate>
                    <div class='theAlertDetails color<%#Eval("StatusColor")%>'>
                        <div class='AlertTitle'>
                            <span style='font-weight:100;'><strong><%#Eval("RouteDisplayNumber")%></strong><%#Eval("RouteName")%></span>
                        </div>
                        <div class="AlertStamp color<%#Eval("StatusColor")%>">
                            <em>posted: </em><%#Eval("DateAdded")%>
                        </div>
                        <br clear="all" />
                        <%#Eval("MessageEmail")%>
                    </div>            
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <br clear="all" />
    
    <!-- Latest compiled and minified JavaScript -->
    <script type='text/javascript' src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <!--<script type='text/javascript' src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>-->
    
</asp:Content>

