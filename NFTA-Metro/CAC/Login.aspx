<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="NFTA_Metro._CACLogin" MasterPageFile="~/Metro.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="phHead" runat="server">
    <title>Niagara Frontier Transportation Authority (NFTA) - CAC</title>
</asp:Content>

<asp:Content ID="ContentContent" ContentPlaceHolderID="phContent" runat="server">
    <h2 class="newsheader"><em>Citizens Advisory Committee Member Login</em></h2>  
    <div id="main">
         <center>
           <p>
            <asp:Label id="lblInvalid" runat="server" />
            </p>

            <div style="text-align:right; width:300px;"><br /><br />
	            Username: <asp:TextBox CssClass="txtbox" Width="200" id="txtUsername" runat="server" /><br /><br />
	            Password: <asp:TextBox CssClass="txtbox"  Width="200" id="txtPassword" TextMode="password" runat="server" /><br />
	            <br />
	            <asp:Button id="btnLogin" runat="server"
		            text="Login" OnClick="btnLogin_OnClick"
	            />
	        </div>
        </center>
    </div>
</asp:Content>