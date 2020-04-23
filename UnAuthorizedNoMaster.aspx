<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UnAuthorizedNoMaster.aspx.vb" Inherits="Phazzer.UnAuthorizedNoMaster" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>Phazzer Training - UNAUTHORIZED</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="/css/styles.css"  rel="stylesheet" type="text/css" /> 

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
<%--************************************************************************--%>
<%--************************************************************************--%>

		<telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" 
            DecoratedControls="Default,Zone" />
<div class="row topBanner"><%-- top banner--%>
    <div class="col">
        <img alt="PHazzer Training" class="logoHeight" src="../images/PhazzerTrainingLogo1.png" />
    </div>
    <div class="col text-right text-sm-right" style="padding-right:50px;" ><br />
        <asp:LoginStatus ID="LoginStatus1" runat="server" /> &nbsp; &nbsp; <asp:LoginName ID="LoginName1" runat="server" />
    </div>
</div><%-- end top banner --%>
<br /><br /><br />
        <table align="Center"><tr>
            <td style="text-align:center"><strong>Phazzer Training</strong></td></tr></table>
       <div style="width:525px;margin:auto;text-align:center;border:1px solid red; padding:0px 12px 0px 12px;">
         <h2 style="color:firebrick">Unauthorized Access</h2>
            <p>
              You have attempted to access a page that you are not authorized to view,<br />
                or is missing required parameters.
     </p>
     <p style="text-align:left">
          If you have any questions, please contact the site administrator.
     </p>

<p style="text-align:left">         Please provide the following information:
<ul style="text-align:left;">
          <li>Your current IP Address:<asp:Label ID="IEEPEE" runat="server">
          </asp:Label> <asp:Label ID="lblIP" runat="server"/></li>
          <li>Current date and time: <asp:Label ID="lblTime" runat="server" /> EST</li>
</ul></p>
    </div>

    </form>
</body>
</html>

