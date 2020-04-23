<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="Phazzer._default" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <title>PhaZZer Training</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="css/styles.css" rel="stylesheet" type="text/css" /> 
<style>

</style>
</head>
<body>
<form id="form1" runat="server" >
  <telerik:RadScriptManager ID="RadScriptManager1" runat="server"  AsyncPostBackTimeOut="600"/>
  <telerik:RadStyleSheetManager runat="server" ID="RadStyleSheet1" />
  <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" EnableEmbeddedSkins="true" Skin="Default" />

<div class="row topBanner"><%-- top banner--%>
    <div class="col">
        <img alt="PHazzer Training" class="logoHeight" src="../images/PhazzerTrainingLogo1.png" />
    </div>
    <div class="col text-right text-sm-right" style="padding-right:50px;" ><br />
        <asp:LoginStatus ID="LoginStatus1" runat="server" /> &nbsp; &nbsp; <asp:LoginName ID="LoginName1" runat="server" />
    </div>
</div><%-- end top banner --%>

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
    <div class="row justify-content-md-center" style="padding-top:20px;">
        <h5>Hey there <asp:Literal ID="litName" runat="server" />, Welcome Back!</h5> 
    </div>
<hr /><br />
    <div class="row">
        <div class="col-sm-4">
        </div>
        <div class="col-sm-4">
           <div class="container">
                <asp:Literal ID="litComments" runat="server" /><br />
                <asp:Literal ID="litQuestions" runat="server" />
            </div>
        </div>
        <div class="col-sm-4">
        </div>

    </div>
</div>

    </main>

</form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>
</html>
