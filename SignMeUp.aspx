<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SignMeUp.aspx.vb" Inherits="Phazzer.SignMeUp" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
   <title>Phazzer Training - Create Account</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="css/styles.css"  rel="stylesheet" type="text/css" /> 

    <style type="text/css">
        .lilBlueButton{
        color:Blue;
        font-weight:normal;
        font-size: 13px;
        padding-left:5px;
        }
        .auto-style1 {
            width: 442px;
        }
        .auto-style2 {
            height: 52px;

        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        	<telerik:RadScriptManager ID="RadScriptManager1" runat="server"/>
<div class="row topBanner"><%-- top banner--%>
    <div class="col">
<h4>PhaZZer Training</h4>
    </div>
    <div class="col text-right text-sm-right" style="padding-right:50px;" ><br />
        <asp:linkbutton ID="LnkLogin" runat="server" Text="Home" OnClientClick="javascript:window.location='default.aspx';" /> &nbsp; &nbsp; <asp:LoginName ID="LoginName1" runat="server" />
    </div>
</div><%-- end top banner--%>
<br />

<center><h5>Welcome to<br /> PhazzerTraining.com</h5>
<%--	<asp:Login ID="bpLogIn" runat="server" DisplayRememberMe="true" RememberMeSet="true">
		<LayoutTemplate>--%>

    <div class="d-flex align-items-center flex-column justify-content-center h-100" id="header">
<br />
<asp:Panel ID="pnlForm" runat="server" >

        <div class="form-group">
            <div class="container">
       <div class="row h-100 justify-content-center">
<div class="col" id="ProfileForm" >
<table>
    <tr>
        <td>First Name:</td>
        <td><telerik:RadTextBox ID="txtFirstName" EmptyMessage="First Name" runat="server" /></td>
    </tr>
    <tr>
        <td>Last Name:</td>
        <td><telerik:RadTextBox ID="txtLastName" EmptyMessage="Last Name" runat="server" /></td>
    </tr>
    <tr>
        <td>Company Name:</td>
        <td><telerik:RadTextBox ID="txtCompany" EmptyMessage="Optional" runat="server" /></td>
    </tr>
    <tr>
        <td>eMail Address:</td>
        <td><telerik:RadTextBox ID="txtEmail" EmptyMessage="eMail Address" Width="225px" runat="server" /></td>
    </tr>
    <tr>
        <td>Password:</td>
        <td>
            <telerik:RadTextBox ID="txtPassword" EmptyMessage="Password" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Phone Number:</td>
        <td><telerik:RadMaskedTextBox ID="txtPhone" Width="130px" runat="server" EmptyMessage="Phone Number" Mask="(###) ###-####" /></td>
    </tr>
    <tr>
        <td>Carrier, for txt msgs:</td>
        <td>
            <telerik:RadComboBox ID="cbCellCarrier" Width="200px" runat="server" AllowCustomText="True" DataSourceID="CellCarrierDataSource1" DataTextField="CarrierName" Font-Size="11" EmptyMessage="if Cell, choose carrier" DataValueField="CarrierID" MarkFirstMatch="True"></telerik:RadComboBox>        <br />
            <asp:SqlDataSource ID="CellCarrierDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PhazzerConnectionString %>" SelectCommand="SELECT [CarrierID], [CarrierName] FROM [CellCarrierSMS] ORDER BY [CarrierName]"></asp:SqlDataSource>
        </td>
    </tr>
</table>   
    <telerik:RadButton ID="btnSubmit" runat="server" text="Sign Me Up" />

</div>
       </div>
   </div>
        </div>
    <asp:Label ID="lblFormError" ForeColor="red" runat="server" />
<%--            <br />
            <telerik:RadTextBox ID="txtUserName" runat="server" /><br />--%>
     </asp:Panel>
</div>

<asp:Panel ID="pnlsuccess" runat="server" Visible="false">

    <table border="0" style="border-collapse:collapse;">
    <tr>
        <td style="padding:7px;">
            <table border="0" style="border:1px solid black;">
                <tr>
                    <td align="center" class="LoginHeader">
                        <asp:Label ID="Label1" Font-Bold="true" Text="Thank you" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="padding:17px 10px 17px 10px; text-align:center">
                        Check your eMail for the link <br />that will let you log in to:
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;padding-bottom:14px;">
                        <strong>Phazzer Training</strong> <br />
                    </td>
                </tr>

            </table>
        </td>
    </tr>
</table>
</asp:Panel>           



    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>
</html>
