<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="editProfile.aspx.vb" Inherits="Phazzer.editProfile" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="css/styles.css" rel="stylesheet" type="text/css" /> 
    <style>
        .botborder{
        border-bottom:1px solid #aaaaaa;
        }
        .phoneNumber{
            font-size:14px;nn
        }
        .lblErr{
            color:Red;
            font-size:11px;
            font-weight:bold;
        }
        body{
            font-family:Arial;
        }
        .lilBlueButton{
            font-size:11px;
        }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
    <telerik:RadStyleSheetManager runat="server" ID="RadStyleSheet1" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" EnableEmbeddedSkins="true" Skin="Default" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnUpdateProfile">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnUpdateProfile" 
                        LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="errProfileMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnChangePassword">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnChangePassword" 
                        LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelHeight="" />
                    <telerik:AjaxUpdatedControl ControlID="errMsg" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
<div class="row topBanner"><%-- top banner--%>
    <div class="col">
        <img alt="PHazzer Training" class="logoHeight" src="images/PhazzerTrainingLogo1.png" />
    </div>
    <div class="col text-right text-sm-right" style="padding-right:50px;" ><br />
        <asp:LoginStatus ID="LoginStatus1" runat="server" /> &nbsp; &nbsp; <asp:LoginName ID="LoginName1" runat="server" />
    </div>
</div><%-- end top banner--%>

			<telerik:RadMenu RenderMode="Auto" ID="RadMenu1" runat="server" EnableRoundedCorners="true" ShowToggleHandle="true"
            EnableShadows="true" EnableTextHTMLEncoding="true" DataValueField="Text" Style="position:absolute;z-index:3000" 
                DataSourceID="XmlDataSource1" DataNavigateUrlField="Url" DataTextField="Text" 
                Width="100%" />
            
            <asp:XmlDataSource ID="XmlDataSource0" runat="server" DataFile="~/menus/adminMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/menus/clerkMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource2" runat="server" DataFile="~/menus/Manager.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource3" runat="server" DataFile="~/menus/InstructorMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource4" runat="server" DataFile="~/menus/StudentMenu.xml" XPath="/Items/Item" />
            <asp:XmlDataSource ID="XmlDataSource5" runat="server" DataFile="~/menus/ClientMenu.xml" XPath="/Items/Item" />
    <main id="content" runat="server" style="padding-top:40px;padding-left:12px;">
<div class="container">
    
<div class="pageHeader"><div class="col h4">My Profile Info</div></div>
<main id="Main1" runat="server" style="padding-left:12px;">
    <div class="d-flex align-items-center flex-column justify-content-center h-100" id="header">
<br />
        <div class="form-group">
            <div class="container">
       <div class="row h-100 justify-content-center">
<div class="col" id="ProfileForm" >
<table>
    <tr>
        <td>First Name:</td>
        <td><telerik:RadTextBox ID="txtFirstName" runat="server" /></td>
    </tr>
    <tr>
        <td>Last Name:</td>
        <td><telerik:RadTextBox ID="txtLastName" runat="server" /></td>
    </tr>
    <tr>
        <td>Company Name:</td>
        <td><telerik:RadTextBox ID="txtCompany" EmptyMessage="Optional" runat="server" /></td>
    </tr>
    <tr>
        <td>eMail Address:</td>
        <td><telerik:RadTextBox ID="txtEmail" Width="225px" runat="server" /></td>
    </tr>
    <tr>
        <td>Phone Number:</td>
        <td><telerik:RadMaskedTextBox ID="txtPhone" Width="130px" runat="server" EmptyMessage="" Mask="(###) ###-####" /></td>
    </tr>
    <tr>
        <td>Carrier, for txt msgs:</td>
        <td>                <telerik:RadComboBox ID="cbCellCarrier" runat="server" AllowCustomText="True" DataSourceID="CellCarrierDataSource1" DataTextField="CarrierName" Font-Size="11" EmptyMessage="if Cell, choose carrier" DataValueField="CarrierID" MarkFirstMatch="True"></telerik:RadComboBox>        <br />
            <asp:SqlDataSource ID="CellCarrierDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PhazzerConnectionString %>" SelectCommand="SELECT [CarrierID], [CarrierName] FROM [CellCarrierSMS] ORDER BY [CarrierName]"></asp:SqlDataSource>
</td>
    </tr>
    <tr>
        <td></td>
        <td>
            Change Password
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <telerik:RadTextBox ID="txtnewPassword" EmptyMessage="NewPassword" runat="server" />
        </td>
    </tr>

</table>   
    <telerik:RadButton ID="btnSaveProfile" runat="server" text="Save Changes" />
    <br /><asp:Label ID="lblerr" runat="server" />
</div>
       </div>
   </div>
        </div>

<%--            <br />
            <telerik:RadTextBox ID="txtUserName" runat="server" /><br />--%>
</div>

</main>
  </div>
  </div>

    </main>
    </form>
</body>
</html>
