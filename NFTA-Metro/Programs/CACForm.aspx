<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CACForm.aspx.vb" Inherits="NFTA_Metro._cac" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>NFTA-Metro | Citizens Advisory Committee Application</title>
    <meta name="description" content="The Niagara Frontier Transportation Authority is a diversified and synergistic organization, energized by 1,500 dedicated men and women, serving the Niagara Region through cost-effective, quality transportation services everyday." />
    <meta name="keywords" content="NFTA, Niagara Frontier Tranportation Authority, NFTA Metro, Metro, Transit, Transit Authority, Transit Agency, Tansportation, Bus and Rail, Metro Bus, Metro Rail" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="/js/showhide.js"></script>
    <style type="text/css">
        legend {font-size:1.2em; color:#c60606; font-weight:900;}
        h2.expand {font-size:1.0em; height:30px; text-indent:45px;line-height:30px; margin:0;}
        h2.expand.inactive {font-size:1.0em; background:#ccc  url(../img/i-active.gif) left no-repeat; padding:5px;}
        h2.expand.active {font-size:1.0em; background:#ccc  url(../img/i-inactive.gif) left no-repeat; padding:5px;}
    </style>
</asp:Content>

<asp:Content ID="phMAIN" ContentPlaceHolderID="phMain" runat="server">
    <h1>Citizens Advisory Committee Application</h1>
   <asp:PlaceHolder ID="phResponse" runat="server" Visible="false" >
            <div>
                <p>Thank you for applying to the Citizens Advisory Committee.</p>
                
                <asp:PlaceHolder ID="phExistingUser" runat="server" Visible="false">
                    <p>Based on the e-mail you provided, we have found that you have an existing <a href="http://alerts.nfta.com" target="_iu">NFTA Instant Update</a> account. Your email has been added to the Citizens Advisory Committee subscription and you will receive future e-mail notifications related to the CAC. To manage your subscriptions you may login to the <a href="http://alerts.nfta.com" target="_iu">NFTA Instant Update System</a> at any time. If you forgot your password you may receive it by using our <a href="http://alerts.nfta.com/RecoverPassword.aspx" target="_iu">recover password</a> page.</p>
                </asp:PlaceHolder>
                
                <asp:PlaceHolder ID="phNewUser" runat="server" Visible="false">
                    <p>A NFTA Instant Update account has been successfully created for you. You have been added to the Citizens Advisory Committee subscription and will receive future e-mail notifications related to the CAC.</p>
                    <p>To login to the NFTA Instant Update System, use the information below:</p>
                    <div style="margin:5px auto 5px 30px">
                        Website: <a href="http://alerts.nfta.com" target="_iu">http://alerts.nfta.com</a><br />
                        UserId: <asp:Literal ID="litNewUserEmail" runat="server" /> (your email address)<br />
                        Password: <asp:Literal ID="litNewUserPassword" runat="server" />
                    </div>
                    <p>Please make note of this information for future logins to the NFTA Instant Update System. After logging in, you may change your password at any time. You may also unsubscribe at any time. You can receive instant updates on almost anything happening at the NFTA. Metro bus route changes, special events, news, job postings, airport parking and even boat harbor alerts. Updates are delivered to you by e-mail or text messages, or both. There's no charge for this service from the NFTA, however your cellular service provider may charge per text message.</p>
                    
                </asp:PlaceHolder>
            </div>
        </asp:PlaceHolder>
        
        <asp:PlaceHolder ID="phForm" runat="server" >    
    	    <p>To be considered for the Citizens Advisory Committee fill out and submit the application below.</p>
    	    
    	    <hr /> 
    	    
    	    <fieldset>
    	        <legend>1. Information</legend>
	            <table cellpadding="10">
	                <tr>
	                    <td align="right" valign="top">
	                        First Name:
	                    </td>
	                    <td align="left">
	                        <asp:TextBox ID="txtFirstName" MaxLength="50" runat="server" />
	                    </td>
	                    <td align="right" valign="top">
	                        Last Name:
	                    </td>
	                    <td align="left">
	                        <asp:TextBox ID="txtLastName" MaxLength="50" runat="server" />
	                    </td>
	                </tr>
	                <tr>
	                    <td align="right" valign="top">
	                        Address:
	                    </td>
	                    <td align="left" colspan="3">
	                        <asp:TextBox ID="txtAddress" MaxLength="100" Width="436" runat="server" />
	                    </td>
	                </tr>
	                <tr>
	                    <td align="right" valign="top">
	                        City:
	                    </td>
	                    <td align="left">
	                        <asp:TextBox ID="txtCity" MaxLength="50" runat="server" />
	                    </td>
	                    <td align="right" valign="top">
	                        State:
	                    </td>
	                    <td align="left">
	                        <asp:DropDownList ID="ddlState" DataValueField="stpAbbreviation" DataTextField="stpName" runat="server" />
	                    </td>
	                </tr>
	                <tr>
	                    <td align="right" valign="top">
	                        Zip Code:
	                    </td>
	                    <td align="left">
	                        <asp:TextBox ID="txtZip" MaxLength="10" runat="server" />
	                    </td>
	                    <td align="right" valign="top">
	                        County:
	                    </td>
	                    <td align="left">
	                        <asp:TextBox ID="txtCounty" MaxLength="50" runat="server" />
	                    </td>
	                </tr>
	                <tr>
	                    <td align="right" valign="top">
	                        Phone:
	                    </td>
	                    <td align="left">
	                        <asp:TextBox ID="txtPhone" MaxLength="50" runat="server" />
	                    </td>
	                    <td align="right" valign="top">
	                        Email:
	                    </td>
	                    <td align="left">
	                        <asp:TextBox ID="txtEmail" MaxLength="50" runat="server" />
	                    </td>
	                </tr>
	            </table>
    	    </fieldset>
        	    
    	    <br /><br />
        	
        	<fieldset>
    	        <legend>2. Services</legend>
    	        <table cellpadding="10">
                    <tr>
                        <td align="left" valign="top" colspan="2">
                            Indicate the services that you use (check all that apply).
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <asp:CheckBox ID="chkPal" Text=" PAL" runat="server" /><br />
    	                    <asp:CheckBox ID="chkFixed" Text=" Fixed Route" runat="server" /><br />
    	                    <asp:CheckBox ID="chkExpress" Text=" Express Service" runat="server" /><br />
                        </td>
                        <td align="left" valign="top">
                            <asp:CheckBox ID="chkMonthly" Text=" Monthly or 30 Day Pass Holder" runat="server" /><br />
    	                    <asp:CheckBox ID="chkPark" Text=" Park & Ride Lots" runat="server" /><br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            Other please Indicate (50 chars)
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtOther" TextMode="MultiLine" width="300" Rows="4" MaxLength="50" runat="server" /><br />
                        </td>
                    </tr>
                </table>
    	    </fieldset>
    	    
    	    <br /><br />
        	
        	<fieldset>
    	    <legend>3. Statement of Interest</legend>
            <table cellpadding="10">
                <tr>
                    <td align="left" valign="top">
                        In the space below, describe your reasons for wanting to be a member of the CAC. Your response is limited to 500 words. When developing your response, keep the following questions in mind. <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-What is your purpose in joining the CAC? <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-What will you contribute to the CAC? <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-What public transportation issues and concerns are important to you?<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-How will you communicate the information and ideas discussed at the CAC to your community?<br />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtStatement" TextMode="MultiLine" Rows="8" Width="540" MaxLength="3000" runat="server" />
                    </td>
                </tr>
            </table>
    	    
    	    </fieldset>
    	    
    	    <br /><br />
    	    
    	    <fieldset>
    	    <legend>4. Routes</legend> 	    
    	        <table cellpadding="10">
                    <tr>
                        <td align="left" valign="top">
                            Which Metro Route(s) do you ride frequently.  Click on the categories below to view the routes. (check all the apply)<br />
                            <asp:Repeater ID="rptCategories" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td width="740">
                                            <div style="padding-top:5px; text-align:left">
                                                <h2 class="routeCat expand"><%#Eval("CategoryName")%> (<%#Eval("Count")%>)</h2>
                                                <div class="info">
    	                                            <asp:GridView ID="gvRoutes" AutoGenerateColumns="false" DataKeyNames="RouteId" BorderStyle="None" GridLines="None" runat="server" >
    	                                                <Columns>
    	                                                    <asp:BoundField DataField="RouteId" />
    	                                                    <asp:TemplateField ItemStyle-VerticalAlign="Top"><ItemTemplate><div style="text-align:center"><asp:CheckBox ID="RowLevelCheckBox" runat="Server" /></div></ItemTemplate></asp:TemplateField>
    	                                                    <asp:TemplateField ItemStyle-VerticalAlign="Top"><ItemTemplate><div style="text-align:left; padding-left:10px"><b><%#If(Eval("RouteNumber").ToString = "45", "Metro Rail", "Route # " & Eval("RouteNumber"))%></b><br /><%#Eval("RouteName")%></div></ItemTemplate></asp:TemplateField>
    	                                                    <asp:TemplateField ItemStyle-VerticalAlign="Top"><ItemTemplate>
    	                                                        <div style="text-align:left"><b>How Often:</b><br />
    	                                                            <div style="text-align:left; padding-left:20px">
    	                                                                <asp:RadioButton ID="rbFrequent" Text=" Frequent (+2 times/week)" GroupName="GroupFrequency" runat="server" /><br />
    	                                                                <asp:RadioButton ID="rbSometimes" Text=" Sometimes (1-4 times/month)" GroupName="GroupFrequency" runat="server" /><br />
    	                                                                <asp:RadioButton ID="rbRarely" Text=" Rarely (1-12 time/year)" GroupName="GroupFrequency" runat="server" />
    	                                                            </div>
    	                                                        </div>
    	                                                    </ItemTemplate></asp:TemplateField>
    	                                                </Columns>
    	                                                <AlternatingRowStyle CssClass="gv1" Height="110" />
    	                                                <RowStyle CssClass="gv2" Height="110" />
    	                                            </asp:GridView>                                
                                                </div>
                                            </div>
                                        </td>                            
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                       </td>
                    </tr>
                </table>
            </fieldset>
        
            <br /><br />
                <span style="font-size:.9em;"><asp:CheckBox ID="cbAgree" runat="server" /><strong>I have read and agreed to the following terms and conditions:</strong><br />
    	            <blockquote>
    	            If you have an existing <a href="http://alerts.nfta.com" target="_iu">NFTA Instant Update System</a> account based on the e-mail you provided, your e-mail will be added to the Citizens Advisory Committee subscription and you will receive future e-mail notifications related to the CAC. If you do not have an <a href="http://alerts.nfta.com" target="_iu">NFTA Instant Update System</a> account one will be created for you and your e-mail will be added to the Citizens Advisory Committee subscription.  As such, you will receive future e-mail notifications related to the CAC. Your username will be your e-mail address and your password will be provided upon successful completion of this application. After logging in, you may change your password at any time. If you forgot your password you may receive it by using our <a href="http://alerts.nfta.com/RecoverPassword.aspx" target="_iu">recover password</a> page. You may also unsubscribe at any time. You can receive Instant Updates on almost anything happening at the NFTA. Metro bus route changes, special events, news, job postings, airport parking and even boat harbor alerts. Updates are delivered to you by e-mail or text messages, or both. There's no charge for this service from the NFTA, however your cellular service provider may charge per text message.
    	            </blockquote>
    	        </span>
    	     
    	        <br /><br />
    	        <asp:PlaceHolder ID="phErrors" runat="server" Visible="false" >
    	            <div class="red"><ul><asp:Literal ID="litErrors" runat="server" /></ul></div>
    	        </asp:PlaceHolder>
    	      <center>
    	        <asp:Button ID="btnSubmit" Text="Submit" ToolTip="Click here to submit your application" ValidationGroup="valForm" runat="server" />
    	    </center>
    	</asp:PlaceHolder>
</asp:Content>  