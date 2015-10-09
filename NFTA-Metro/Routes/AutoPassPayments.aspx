<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AutoPassPayments.aspx.vb" Inherits="NFTA_Metro._AutoPass" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Automatic Pass Sales</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail, Schedules, Routes, Fares" />
</asp:Content> 

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Automatic Pass Sales</h1>
    <p>To set up monthly automatic payments select a pass type below and click on the "Buy Now" button.  This will take you to a check out process.</p>
        
        <p>Questions regarding pass sales can be directed to:</p>
        
        <p style="font-size:smaller; padding-left:100px;">
            <b>NFTA/Metro System, Inc., Cash Management Office</b><br />
            2nd floor 181 Ellicott Street, Buffalo, NY 14203<br />
            Phone: (716) 855-7200 (TDD) 855-7650<br />
            Fax: (716) 855-7311<br />
            Email: <a href="&#x6d;&#x61;&#x69;&#108;&#x74;&#x6f;&#x3a;&#x70;&#97;&#115;&#115;&#115;&#x61;&#108;&#101;&#115;&#64;&#000110;&#000102;&#x74;&#97;&#x2e;&#00099;&#x6f;&#x6d;" style="" class="" id="">&#112;&#x61;&#000115;&#x73;&#x73;&#x61;&#x6c;&#000101;&#115;&#64;&#x6e;&#000102;&#000116;&#97;&#00046;&#x63;&#x6f;&#x6d;</a>

        </p>
        <hr />
        <br /><br />
        <div align="center">
            Select Pass Type:
            <select name="os0-Auto">
	            <option value="Monthly Pass">Monthly Pass : $75</option>
	            <option value="Monthly Pass - Reduced Fare">Monthly Pass - Reduced Fare : $37.50</option>
	            <option value="PAL Pass - 10 Trips">PAL Pass - 10 Trips : $35</option>
	            <option value="PAL Pass - 20 Trips">PAL Pass - 20 Trips : $70</option>
            </select>
            <img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1" />
            <br /><br />
                                                           
            <asp:ImageButton runat="server" ImageUrl="http://metro.nfta.com/img/buynow.gif" ID="btnAuto" OnClick="btnAuto_Click"></asp:ImageButton>
   	    </div>
</asp:Content>