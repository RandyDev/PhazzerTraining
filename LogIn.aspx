<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LogIn.aspx.vb" Inherits="Phazzer.LogIn" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Phazzer Training - Login</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
  	<link href="styles.css"  rel="stylesheet" type="text/css" />
    <style type="text/css">
        .logoHeight {
        height:55px;
        }
.topBanner {
    background-color: aliceblue;
    padding-left: 55px;
    height: 65px;
    margin: 0,auto;
    border-bottom:1px solid black;
    /*background-image: url('../images/header-colds.png');
    background-position:  0 0px;*/
    /* 0 98  196  294  392 coldonly> 490 */
    /* red blue gray dkgray blue coldonly> bluegreen */
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    	<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
      
<div class="row topBanner"><%-- top banner--%>
    <div class="col">
        <img alt="PHazzer Training" class="logoHeight" src="../images/PhazzerTrainingLogo1.png" />
    </div>
</div><%-- end top banner--%>

<main id="content" runat="server">
<%--************************************************************************--%>
<%--************************************************************************--%>

<br /><br /><br />
<center><h5>Welcome to<br />Phazzer Training
<%--	<asp:Login ID="bpLogIn" runat="server" DisplayRememberMe="true" RememberMeSet="true">
		<LayoutTemplate>--%>

<asp:Panel ID="pnlLogin" runat="server" DefaultButton="LoginButton">
<table border="0" name="tblLoginForm" style="border-collapse:collapse;">
    <tr>
        <td style="padding:7px;">
            <table border="1" id="tblLoginForm">
                <tr>
                    <td colspan="2" class="LoginHeader text-center">
                        <asp:Label ID="lblHeader" Text="Please Log In" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height:4px; font-size:11px;"><asp:Label ID="lblQuestion" runat="server" /></td>
                </tr>
                 <tr>
                    <td align="right" style="padding-top:8px;">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                        <asp:Label ID="lblUserName" Text="Login ID: " runat="server" /> </asp:Label>&nbsp; </td><td style="padding-top:8px;"><asp:TextBox ID="UserName" CssClass="form-control" runat="server" Font-Size="0.8em" /> 
                         <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                             ControlToValidate="UserName" ErrorMessage="User Name is required." 
                             ToolTip="User Name is required." ValidationGroup="Login1" Text="*" />
                     </td>
                 </tr>
                 <tr>
                    <td align="right" style="padding-top:12px;">
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="Password: "></asp:Label></td><td style="padding-top:12px;">
                         <asp:TextBox ID="Password" CssClass="form-control" runat="server" Font-Size="0.8em" TextMode="Password" />
                             <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                 ControlToValidate="Password" ErrorMessage="Password is required." 
                                 ToolTip="Password is required." ValidationGroup="Login1" text="*" />
                     </td>
                 </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellpadding="0" width="100%" cellspacing="0">
                            <tr>
                                <td align="left">
                                    <asp:LinkButton ID="lbtnForgotPassword" Text="forgot password" CommandName="forgotPassword" runat="server" ValidationGroup="Login2"/>
                                </td>
                                <td align="right">
                                    <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" 
                                        BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" Text="Log In" 
                                        ValidationGroup="Login1" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
<tr><td style="text-align:center;"><a class="lilbluebutton" href="SignMeUP.aspx"> I don&#39;t have an account! </a></td></tr></table><asp:Label ID="rpInstructions" runat="server" Visible="false">
            <br />
            Enter the correct Case SensitivE response to your security question above and click the <u>Show my password</u> link.<br />
Or<br />Use the button below to have your password eMailed to you.<br />
        </asp:Label><span style="color:Red;"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></span><br /><asp:Button ID="btnSendNewPassword" runat="server" Text="Yes" Visible="false" />&nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnNoPass" runat="server" Text=" No " Visible="false" /> 


</asp:Panel>
	
<%--		</LayoutTemplate>
	</asp:Login>--%>
</center>

</main>
    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>
</html>

