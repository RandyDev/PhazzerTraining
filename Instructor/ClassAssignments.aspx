<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="ClassAssignments.aspx.vb" Inherits="Phazzer.ClassAssignments" %>

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

</head>
<body>
    <form id="form1" runat="server">
  <telerik:RadScriptManager ID="RadScriptManager1" runat="server"  AsyncPostBackTimeOut="600"/>
  <telerik:RadStyleSheetManager runat="server" ID="RadStyleSheet1" />

<div class="row topBanner"><%-- top banner--%>
    <div class="col">
        <img alt="PHazzer Training" class="logoHeight" src="../images/PhazzerTrainingLogo1.png" />
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



<div id="wrapper" style="text-align:center;">
    <div id="tables" style="display:inline-block;">
<table>
    <tr>
        <td>Available Classes</td>
        <td>MY Classes</td>
    </tr>
    <tr>
        <td style="vertical-align:top;padding-right:10px;">

<telerik:RadGrid ID="RadGrid1" runat="server" Width="500px" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top">
    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
    <ClientSettings>
        <Selecting AllowRowSelect="True" />
    </ClientSettings>
    <MasterTableView DataKeyNames="EventId" AutoGenerateColumns="False">
        <Columns>
            <telerik:GridBoundColumn DataField="EventId" DataType="System.Int32" HeaderText="ID" UniqueName="EventId" FilterControlAltText="Filter eventId column" ReadOnly="True" Visible="true" SortExpression="eventId" />
            <telerik:GridBoundColumn DataField="eventName" FilterControlAltText="Filter eventName column" HeaderText="Course" SortExpression="eventName" UniqueName="eventName" />
            <telerik:GridBoundColumn DataField="eventStartDate" DataFormatString="{0:dddd dd/MMM/yy hh:mm tt}" DataType="System.DateTime" FilterControlAltText="Filter eventStartDate column" HeaderText="Date" SortExpression="eventStartDate" UniqueName="eventStartDate" />
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <telerik:RadButton ID="RadButton1" runat="server" Text="Accept Class" CommandName="AddInstructor" /> 
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
    
        </td>
        <td style="vertical-align:top;padding-left:10px;">

<telerik:RadGrid ID="RadGrid2" runat="server" Width="500px" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top">
    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
    <ClientSettings>
        <Selecting AllowRowSelect="True" />
    </ClientSettings>
    <MasterTableView DataKeyNames="EventId" AutoGenerateColumns="False">
        <Columns>
            <telerik:GridBoundColumn DataField="EventId" HeaderText="ID" UniqueName="EventId" ReadOnly="True" Visible="true"  />
            <telerik:GridBoundColumn DataField="eventName" HeaderText="Class Name" UniqueName="eventName" />
            <telerik:GridBoundColumn DataField="eventStartDate" DataFormatString="{0:dddd dd/MMM/yy hh:mm tt}" DataType="System.DateTime" HeaderText="Date" SortExpression="eventStartDate" UniqueName="eventStartDate" />
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <telerik:RadButton ID="RadButton1" runat="server" Text="Request Drop" CommandName="DropClass" /> 
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
            <br /><br />

<div id="divDropList" runat="server">
<asp:Label ID="lblDropList" Text="Pending Drop Requests" runat="server" />
<telerik:RadGrid ID="RadGrid3" runat="server" Width="500px" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top">
    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
    <ClientSettings>
        <Selecting AllowRowSelect="True" />
    </ClientSettings>
    <MasterTableView DataKeyNames="EventId" AutoGenerateColumns="False">
        <Columns>
            <telerik:GridBoundColumn DataField="UserID" HeaderText="ID" UniqueName="EventId" ReadOnly="True" Visible="false"  />
            <telerik:GridBoundColumn DataField="EventId" HeaderText="ID" UniqueName="EventId" ReadOnly="True" Visible="true"  />
            <telerik:GridBoundColumn DataField="Instructor" HeaderText="Instructor" UniqueName="Instructor" />
            <telerik:GridBoundColumn DataField="eventName" HeaderText="Class Name" UniqueName="eventName" >
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="eventStartDate" DataFormatString="{0:dddd dd/MMM/yy hh:mm tt}" DataType="System.DateTime" HeaderText="Date" SortExpression="eventStartDate" UniqueName="eventStartDate">
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <telerik:RadButton ID="RadButton1" runat="server" Text="Approve Drop" CommandName="ApproveDrop" /> 
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
</div>

        </td>
    </tr>
</table>
</div>
</div>
</main>


    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>

</body>
</html>

