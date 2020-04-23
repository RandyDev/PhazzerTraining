\<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClassList.aspx.vb" Inherits="Phazzer.ClassList" %>

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
<style>
.lblName{
font-size:18px;
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

         .noInstructor
  {
    background-color: aqua;
    font-size: 16px;
    font-family: Arial;
  }
 </style>
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
    <main id="content" runat="server" style="padding-top:50px;padding-left:12px;">

        <center>
<asp:Label CssClass="lblName" ID="lblFirstLast" runat="server" />
</center>
<div id="divclasslist" style="margin:0;width:auto;" runat="server">
    <center><asp:Label ID="lblListTitle" runat="server" /><br />
        <asp:Literal ID="lblCourseTitle" runat="server" />
    </center>
 <div style="margin:auto">   
     <table align="center">
         <tr>
             <td>
                 <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top">

                     <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

                     <ClientSettings>
                         <Selecting AllowRowSelect="True" />
                     </ClientSettings>

                     <MasterTableView DataKeyNames="EventId" AutoGenerateColumns="False">
                         <Columns>

                             <telerik:GridBoundColumn DataField="EventId" DataType="System.Int32" HeaderText="ID" UniqueName="EventId" FilterControlAltText="Filter eventId column" ReadOnly="True" Visible="true" SortExpression="eventId" />
                             <telerik:GridBoundColumn DataField="eventName" FilterControlAltText="Filter eventName column" HeaderText="Course" SortExpression="eventName" UniqueName="eventName" />
                             <telerik:GridBoundColumn DataField="eventStartDate" DataFormatString="{0:dddd dd/MMM/yy hh:mm tt}" DataType="System.DateTime" FilterControlAltText="Filter eventStartDate column" HeaderText="Date" SortExpression="eventStartDate" UniqueName="eventStartDate" />
                             <telerik:GridBoundColumn DataField="hasInstructor" UniqueName="hasInstructor" HeaderText="instructor" Visible="false" />
                             <telerik:GridBoundColumn DataField="count" DataType="System.Int32" HeaderText="#reg" ReadOnly="true" UniqueName="count" />

                             <telerik:GridTemplateColumn>
                                 <ItemTemplate>
                                     <telerik:RadButton ID="btnLoadAttendees" runat="server" Text="Load Attendees" CommandName="loadattendees" />
                                 </ItemTemplate>
                             </telerik:GridTemplateColumn>

                         </Columns>
                     </MasterTableView>
                 </telerik:RadGrid>
                 <br />
                 <telerik:RadGrid ID="RadGrid2" runat="server" Width="500px" AutoGenerateColumns="False"
                     AllowAutomaticUpdates="false" AllowMultiRowSelection="true">
                     <ClientSettings>
                         <Selecting AllowRowSelect="True" />
                     </ClientSettings>
                     <MasterTableView DataKeyNames="UserName,BookingID" CommandItemDisplay="Top">
                         <CommandItemSettings ShowRefreshButton="true" ShowAddNewRecordButton="false" />
                         <Columns>
                             <telerik:GridEditCommandColumn ButtonType="ImageButton" EditText="Add/Edit Alternate eMail address" UniqueName="EditCommandColumn">
                                 <ItemStyle CssClass="MyImageButton" />
                             </telerik:GridEditCommandColumn>
                             <telerik:GridBoundColumn ReadOnly ="true" headertext="BK#" DataField="BookingID" uniqueName="BookinID" visible="true" />

                             <telerik:GridTemplateColumn DataType="System.String" HeaderText="Name" UniqueName="Name">
                                 <ItemTemplate>
                                     <%# Eval("firstname") %> <%# Eval("lastname") %>
                                 </ItemTemplate>
                             </telerik:GridTemplateColumn>

                             <telerik:GridBoundColumn ReadOnly="true" DataField="UserName" DataType="System.String" HeaderText="eMail" UniqueName="UserName" />

                             <telerik:GridTemplateColumn DataField="altEmail" DataType="System.String" HeaderText="Alternate eMail" UniqueName="altEmail">
                                 <ItemTemplate>
                                     <%# Eval("altEmail")%>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <telerik:RadTextBox Text='<%# Eval("altEmail")%>' EmptyMessage="Exam link will be sent here" Width="250" runat="server" ID="txtAltEmail" /><br />
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAltEmail" runat="server" Text="invalid eMail address" ForeColor="red"
                                         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                 </EditItemTemplate>
                             </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn>
                                 <ItemTemplate>
                                     <telerik:RadButton ID="btnSendExam" runat="server" Text="Send Exam" CommandName="SendExam" />
                                 </ItemTemplate>
                             </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn>
                                 <ItemTemplate>
                                     <telerik:RadButton ID="btnSendROL" runat="server" Text="Send R.O.L." CommandName="SendROL" />
                                 </ItemTemplate>
                             </telerik:GridTemplateColumn>
                         </Columns>
                     </MasterTableView>
                 </telerik:RadGrid>

             </td>
         </tr>
         <tr>
             <td>&nbsp;</td>
         </tr>
         <tr>
             <td align="right">
                 <telerik:RadButton ID="btnViewClasses" runat="server" Text="Back to Class List" />
             </td>
         </tr>

     </table>

</div>
</div>


        <br />
</main>


    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>

</body>
</html>
