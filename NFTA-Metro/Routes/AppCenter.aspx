<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppCenter.aspx.vb" Inherits="NFTA_Metro._App" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | App Center</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />
</asp:Content> 

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Metro App Center</h1>
     <h3>Transit tools for the web and mobile devices</h3>
     <p>Below are some of the free and commercial applications that are available from the NFTA and third-party developers using Metro's open data.</p>
     <p>Developers can visit out <a href="/Contact/Developers.aspx">developer tools</a>. To suggest or submitt an app send and email to <a href="mailto:pr@nfta.com?Subject=Suggest%20App">pr@nfta.com</a>.</p>
     <style>
     .app {padding:20px 0px; clear:both;}    
     .title {
        color:#004990;
        font-weight:900;
        margin:0;
     }
     .small 
     {
       font-size:small;
       color:#666;
       font-weight:300;
     }
     .app p{
        padding:1px 0 0 0;
        margin:0;
     }
     
     .app img {float: left;}
     .for {color:#666; font-weight:900; font-size:small; font-style:italic;}
     
     a.btn {background:#bcd6f0; color:#fff; border:0; padding:3px; margin:0 4px; -moz-border-radius: 4px; border-radius:4px; text-decoration:none; font-weight:900; font-size:small;}

     </style>
     <div class="app">
        <a href="https://itunes.apple.com/us/app/nfta-metro/id908771775?mt=8&ign-mpt=uo%3D4" target="_blank"><img src="/img/app-nfta.png" width="157" height="166" alt="NFTA-Metro App"/></a>
        <p class="title"><br />NFTA-Metro <span class="small">by NFTA</span></p>
        <p>Transportation Information for Buffalo and Niagara Falls.</p>
        <p class="for">For iPhone, iPod Touch, iPad</p>
        <a class="btn" href="https://itunes.apple.com/us/app/nfta-metro/id908771775?mt=8&ign-mpt=uo%3D4" target="_blank">download</a>
    </div>
    

    <div class="app">
        <a href="http://wmb.nfta.com" target="_blank"><img src="/img/app-wmb.png" width="157" height="166" alt="Wheres My Bus"/></a>
        <p class="title"><br />Where's My Bus <span class="small">by NFTA & IBI</span></p>
        <p>Track where your bus is at that moment using real-time technology.</p>
        <p class="for">For Web and mobile devices</p>
        <a class="btn" href="http://wmb.nfta.com" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="http://smarttraveler.nfta.com" target="_blank"><img src="/img/app-stp.png" width="157" height="166" alt="Smart Traveler"/></a>
        <p class="title"><br />Smart Traveler <span class="small">by NFTA and ACS</span></p>
        <p>Real-Time Map Schedule information.</p>
        <p class="for">For Web and mobile Devices</p>
        <a class="btn" href="http://smarttraveler.nfta.com" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="http://metro.nfta.com/Routes/Systemmap.aspx" target="_blank"><img src="/img/app-systemmap.png" width="157" height="166" alt="System Map"/></a>
        <p class="title"><br />System Map <span class="small">by NFTA</span></p>
        <p>NFTA Metro interactive system map with routes, park and rides, and transit centers.</p>
        <p class="for">For Web and mobile devices</p>
        <a class="btn" href="http://metro.nfta.com/Routes/Systemmap.aspx" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="http://metro.nfta.com/Trip/Trip_Planner.asp" target="_blank"><img src="/img/app-tripplanner.png" width="157" height="166" alt="Trip Planner"/></a>
        <p class="title"><br />Trip Planner <span class="small">by NFTA and ATIS</span></p>
        <p>Text based trip planner.</p>
        <p class="for">For Web and mobile devices</p>
        <a class="btn" href="http://metro.nfta.com/Trip/Trip_Planner.asp" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="https://www.google.com/maps/dir///@42.87697,-78.845959,17709m/data=!3m1!1e3!4m4!4m3!1m0!1m0!3e3" target="_blank"><img src="/img/app-googletransit.png" width="157" height="166" alt="Google Transit"/></a>
        <p class="title"><br />Google Transit <span class="small">by Google Maps</span></p>
        <p>Public transportation planning tool that combines the latest agency data with the power of Google Maps.</p>
        <p class="for">For Web and mobile devices</p>
        <a class="btn" href="https://www.google.com/maps/dir///@42.87697,-78.845959,17709m/data=!3m1!1e3!4m4!4m3!1m0!1m0!3e3" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="http://www.nittec.org/travel_resources/nittec_mobile_app/index.html" target="_blank"><img src="/img/app-nittec.png" width="157" height="166" alt="NITTEC"/></a>
        <p class="title"><br />NITTEC App <span class="small">by NITTEC</span></p>
        <p>Be In the Know While You’re On the Go</p>
        <p class="for">For Android, iPhone, iPod Touch, iPad, Windows</p>
        <a class="btn" href="http://www.nittec.org/travel_resources/nittec_mobile_app/index.html" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="https://itunes.apple.com/us/app/transit-tracker-buffalo-nfta/id807300359?mt=8" target="_blank"><img src="/img/app-transittracker.png" width="157" height="166" alt="Transit Tracker"/></a>
        <p class="title"><br />Transit Tracker <span class="small">by Raging Coders</span></p>
        <p>You know how to get there; we’ll help make sure you get there on time!</p>
        <p class="for">For iPhone, iPod Touch, iPad</p>
        <a class="btn" href="https://itunes.apple.com/us/app/transit-tracker-buffalo-nfta/id807300359?mt=8" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="https://play.google.com/store/apps/details?id=com.swiftkaytech.nfta_metro_buffalo" target="_blank"><img src="/img/app-swift.png" width="157" height="166" alt="Transit Times"/></a>
        <p class="title"><br />NFTA-Metro-Buffalo <span class="small">by SwiftKay Development</span></p>
        <p>This app provides you with all the route listings and stop times for Buffalo's Bus and Rail lines</p>
        <p class="for">For Android</p>
        <a class="btn" href="https://play.google.com/store/apps/details?id=com.swiftkaytech.nfta_metro_buffalo" target="_blank">download</a>
    </div>
    
    
    <div class="app">
        <a href="http://transittimesapp.com/buffalo-ny-nfta-iphone-app.html" target="_blank"><img src="/img/app-transittimes.png" width="157" height="166" alt="Transit Times"/></a>
        <p class="title"><br />Transit Times <span class="small">by Zervaas Enterprises</span></p>
        <p>NFTA Public Transit Timetables, Maps & Trip Planning For Buffalo, NY, USA</p>
        <p class="for">For Android, iPhone, iPod Touch, iPad</p>
        <a class="btn" href="http://transittimesapp.com/buffalo-ny-nfta-iphone-app.html" target="_blank">download</a>
    </div>
    
    <div class="app">
        <a href="https://itunes.apple.com/app/hopstop/id495230948?mt=8" target="_blank"><img src="/img/app-hopstop.png" width="157" height="166" alt="Hop Stop"/></a>
        <p class="title"><br />HopStop Transit Directions <span class="small">by HopStop</span></p>
        <p>Get detailed subway, bus, train, taxi, walking & biking directions along with official transit maps.</p>
        <p class="for">For iPhone, iPod Touch, iPad</p>
        <a class="btn" href="https://itunes.apple.com/app/hopstop/id495230948?mt=8" target="_blank">download</a>
    </div>
    

    
     
</asp:Content>
