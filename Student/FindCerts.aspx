<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="FindCerts.aspx.vb" Inherits="Phazzer.FindCerts" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>PT-Find Certs</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="../css/styles.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            flex-basis: 0;
            -webkit-box-flex: 1;
            flex-grow: 1;
            max-width: 100%;
            left: 253px;
            top: 257px;
            padding-left: 15px;
            padding-right: 15px;
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
    <main id="content" runat="server" style="padding-top:40px;padding-left:12px;">


        <div class="row">
            <div class="col">
           <telerik:RadComboBox Filter="Contains" AllowCustomText="true" CausesValidation="true"
               RenderMode="Lightweight" AutoPostBack="true" ID="cbuser" runat="server" Width="300px" Height="150px" 
                EmptyMessage="Select a User"  Label="Search Student Name" />
                  

            </div>
            <table>
                <tr> 
                    <td>
            <asp:Label Font-Bold="true" runat="server" id="lbluserName" />&nbsp;Certificate(s)
                    </td>
                </tr>
                <tr>
                    <td>
<telerik:RadGrid id="radgrid1" runat="server" Width="855px" CellSpacing="-1" GridLines="Both" >
            <ClientSettings Resizing-AllowResizeToFit="true">
        <Selecting AllowRowSelect="True" />
    </ClientSettings>
    <MasterTableView DataKeyNames="studentID,studentExamID" GridLines="Both" CommandItemDisplay="top"  AutoGenerateColumns="False">
                <CommandItemSettings ShowRefreshButton="true" ShowAddNewRecordButton="false" /> 
        <Columns>
            <telerik:GridBoundColumn DataField="ExamName" DataType="System.String" HeaderText="Exam" HeaderStyle-Width="150px" UniqueName="ExamName" ReadOnly="True" Visible="true"  />
            <telerik:GridBoundColumn DataField="Pass" DataType="System.Boolean" HeaderText="Pass" UniqueName="Pass" HeaderStyle-Width="55px" ReadOnly="True" Visible="false"  />
            <telerik:GridBoundColumn DataField="examDate" DataFormatString="{0:dddd<br />dd-MMM-yy<br /> hh:mm tt}" DataType="System.DateTime" HeaderText="examDate" HeaderStyle-Width="30px" SortExpression="examDate" UniqueName="examDate" />
            <telerik:GridBoundColumn DataField="CertificateName" DataType="System.String" HeaderText="Certificate Name" HeaderStyle-Width="200px" UniqueName="CertificateName" ReadOnly="True" Visible="true"  />
            <telerik:GridBoundColumn DataField="studentExamID" DataType="System.Guid"  HeaderText="Student Exam ID" UniqueName="studentExamID" ReadOnly="True" Visible="false"  />
            <telerik:GridBoundColumn DataField="examID" DataType="System.Guid"  HeaderText="Exam ID" UniqueName="examID" ReadOnly="True" Visible="false"  />
            <telerik:GridBoundColumn DataField="CertificateID" DataType="System.Guid"  HeaderText="Certificate ID" UniqueName="CertificateID" ReadOnly="True" Visible="false"  />
            <telerik:GridBoundColumn DataField="studentID" DataType="System.Guid" HeaderText="StudentID" Visible="false" />
            <telerik:GridTemplateColumn HeaderStyle-Width="130px">
                <ItemTemplate>
                   <asp:HyperLink runat="server" ID="hlView" Text="View" /> /&nbsp;  
                   <asp:HyperLink runat="server" ID="hlDownload" Text="Download" /> /&nbsp;  
                   <asp:HyperLink runat="server" ID="hlEmail" Text="eMail" /> 

                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
                        <telerik:RadTextBox Visible="false" ID="txtResult" runat="server" ForeColor="Green"></telerik:RadTextBox>

                    </td>
                </tr>
            </table>


            <div class="auto-style1">

            </div>
        </div>
</main>

    </form>
</body>
</html>
