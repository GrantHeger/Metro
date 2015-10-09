<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="NFTA_Metro._Special"  MasterPageFile="~/Metro.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Special Services</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares, ADA, Special Services, " />
</asp:Content> 

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Special Services</h1>
    <div align="center">
        <p><a href="Paratransit.aspx"><img src="/img/page-Pal.gif" class="domroll /img/page-Pal-o.gif" alt="PAL and Paratransit" width="307" height="90" /></a><br />
         Curb to curb, lift equipped van service called<br />
         Paratransit Access Line (PAL) is available for qualifying individuals</p>

        <p><a href="BusRail.aspx"><img src="/img/page-Accessibility.gif" class="domroll /img/page-Accessibility-o.gif" alt="Accessibiltiy" width="307" height="90" /></a><br />
        Transportation Services for Individuals With Disabilities</p>
        
        <p><a href="BusRail.aspx#ACD"><img src="/img/page-ACD.gif" class="domroll /img/page-ACD-o.gif" alt="Advisory Committee on the Disabled" width="307" height="90" /></a><br />
        The NFTA Advisory Committee on the Disabled </p>      
        
        <p><a href="/Programs/Perk.aspx"><img src="/img/page-perk.gif" class="domroll /img/page-perk-o.gif" alt="Metro Perk" width="307" height="90" /></a><br />
        Buy Metro-PAL Passes through your employer using pre-tax dollars</p>
        
        <p><a href="FaresReduced.aspx"><img src="/img/page-Reduced.gif" class="domroll /img/page-Reduced-o.gif" alt="Reduced Fares" width="307" height="90" /></a><br />
         Program for Individuals with Disabilities and the Elderly</p>
    </div>
</asp:Content>
