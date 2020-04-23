<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="homepg.aspx.vb" Inherits="PhazzerTraining.homepg" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>PhaZZer Training</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="../css/styles.css" rel="stylesheet" type="text/css" /> 

<style type="text/css">
.lblName{
font-size:15px;
font-weight:bold;
}
.btnMoveImports{
font-size: 16px;
    }
.lilBlueButton{
font-size:11px;
color:Blue;
font-weight:normal;
}

       .rgCollapse
       {
           display:none !important;
       }
       .rgExpand
       {
          display:none !important;
       }
       #tblCertReport
       {
           border:1px solid black;
           }
       .CertHeaderText
       {
           font-size:14px;
           }
       .CertHeaderCell
       {
           text-align:center;
        }

.loca
{
       padding:0 12px 0 12px;
 
    }
       .expired
       {
           padding:0 12px 0 12px;
           color:Red;
           }
       .expiring
       {
           padding:0 12px 0 12px;
           color:Orange;
           }
       .lnkbtn
       {
           padding:0 12px 0 12px;
           }
 </style>
 </head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
        <center><br />
        <br />
 Home Page for <br />
<asp:Label CssClass="lblName" ID="lblFirstLast" runat="server" />
</center>
<br />
    </form>
</body>
</html>
