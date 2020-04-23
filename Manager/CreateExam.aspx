<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreateExam.aspx.vb" Inherits="Phazzer.CreateExam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PhaZZer Training</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="../css/styles.css" rel="stylesheet" type="text/css" /> 

        <style >
        .red {
        color:red;
        font-size:14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" ></telerik:RadAjaxLoadingPanel>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="RadGrid2" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cbDepartment">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbCourseList" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>

        </telerik:RadAjaxManager>
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
        <main id="content" runat="server">
<div id="divtitle" runat="server" style="margin:auto; align-self:center; align-content:center;"> NEW EXAM</div><br />
            <table id="tblexaminfo" runat="server"><tr>
    <td>
<table>
    <tr>
        <td>Exam Title:</td><td  style="padding-left:4px;"><telerik:RadTextBox ID="txtExamName" EmptyMessage="Provide Name for this Exam" runat="server" Width="307px" /></td>
    </tr>
    <tr>
        <td>Company:</td><td  style="padding-left:4px;"><telerik:RadComboBox ID="cbDepartment" AutoPostBack="True"  EmptyMessage="Select Company/Department" runat="server" Width="307px" Filter="Contains" /></td>
    </tr>
    <tr>
        <td>CoursePreReq:</td>
        <td style="padding-left:4px;">
            <telerik:RadComboBox ID="cbCourseList" EmptyMessage="Select Prerequisite Course Work" runat="server" Width="307px" Filter="Contains" MarkFirstMatch="True" />
        </td>
    </tr>
    <tr>
    <td colspan="2">

        <table>
            <tr>
                <td> Price:</td>
                <td style="padding-left:4px;">
                    <telerik:RadNumericTextBox Type="Currency" EmptyMessage="$" ID="numExamPrice" NumberFormat-GroupSeparator="," NumberFormat-DecimalSeparator="." runat="server" Culture="en-US" DbValueFactor="1" LabelWidth="64px" style="text-align: left" Value="0" Width="160px" >
                        <NegativeStyle Resize="None"></NegativeStyle>
                        <NumberFormat DecimalSeparator="." GroupSeparator="," ZeroPattern="$n"></NumberFormat>
                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                        <FocusedStyle Resize="None"></FocusedStyle>
                        <DisabledStyle Resize="None"></DisabledStyle>
                        <InvalidStyle Resize="None"></InvalidStyle>
                        <HoveredStyle Resize="None"></HoveredStyle>
                        <EnabledStyle Resize="None"></EnabledStyle>
                    </telerik:RadNumericTextBox>
                </td>
                <td style="text-align:right;"><telerik:RadButton ID="btnexaminfoSubmit" runat="server" Text="Save Exam Info" /></td>
            </tr>
        </table>
    </td>
    </tr>
    </table>
    </td>
     <td style="padding-left:65px; vertical-align:top;">
         <br />
<table><tr>
    <td>Created By:</td><td style="padding-left:4px;"><asp:Label ID="lblcreatedBy" ForeColor= "DimGray" runat="server" /></td></tr>
   
<tr>        <td colspan="2">Version #:: &nbsp;<asp:Label CssClass="red" ID="lblversion" runat="server" /></td></tr>
<tr>        <td colspan="2">ExamID: &nbsp;<asp:Label CssClass="red" ID="lblExamID" runat="server" /></td></tr>
<tr>        <td colspan="2"><asp:label id="lblsaveresponse" ForeColor="Red" runat="server" /> </td></tr>
</table>
</td></tr></table><br />
<table style="width:90%;">
    <tr>
        <td>
        <asp:Label ID="lblexamName" runat="server" />
        </td>
        <td>
        <asp:Label ID="lblcreatedBy1" runat="server" />
        </td>
        <td>
        <asp:Label ID="lblversion1" runat="server" />
        </td>
        <td>
        <asp:Label ID="lblExamid1" runat="server" />
        </td>
    </tr>
</table>

<table style="width:90%;">
    <tr>
        <td style="width:50%; vertical-align:top;">

<telerik:RadGrid ID="RadGrid1" runat="server" 
     CellSpacing="3" GridLines="Both" AllowAutomaticInserts="True" AutoGenerateColumns="False" GroupPanelPosition="Top">
    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
        <Selecting AllowRowSelect="True" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="400px" />
    </ClientSettings>
    <MasterTableView  Name="Questions" DataKeyNames="QuestionID" CommandItemDisplay="Top" BackColor="#FFC080" ForeColor="Black" GridLines="Both" >
      <HeaderStyle BackColor="#ff9933" />
      <AlternatingItemStyle BackColor="#ffc080" />
      <ItemStyle BackColor="#ffe0c0" />
      <NoRecordsTemplate>
          No Questions Found
      </NoRecordsTemplate>
      <CommandItemSettings AddNewRecordText="Add Question" ShowRefreshButton="False"></CommandItemSettings>
        <Columns>
         <telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="55px" HeaderStyle-Width="55px" />
            <telerik:GridBoundColumn DataField="QuestionID"  DataType="System.Guid" ReadOnly="True" Visible="False" UniqueName="QuestionID" FilterControlAltText="Filter QuestionID column" HeaderText="QuestionID" SortExpression="QuestionID" />
            <telerik:GridBoundColumn DataField="ExamID"  DataType="System.Guid" ReadOnly="True" Visible="False" UniqueName="ExamID" FilterControlAltText="Filter ExamID column" HeaderText="ExamID" SortExpression="ExamID" />
            <telerik:GridTemplateColumn DataField="Question" HeaderText="Question" SortExpression="Question" UniqueName="Question">
<HeaderStyle width="400px" />
<ItemStyle Width="400px" />

                <EditItemTemplate>
                    <telerik:RadEditor ID="txtQuestion" runat="server" Content='<%# Bind("Question") %>' EditModes="Design" Height="65px" ToolbarMode="ShowOnFocus" Width="400px">
                        <Tools>
                            <telerik:EditorToolGroup>
                                <telerik:EditorTool Name="Bold" />
                                <telerik:EditorTool Name="Italic" />
                                <telerik:EditorTool Name="Underline" />
                            </telerik:EditorToolGroup>
                        </Tools>
                    </telerik:RadEditor>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <telerik:RadEditor ID="txtQuestion" runat="server" Content='<%# Bind("Question") %>' EditModes="Design" Height="65px" ToolbarMode="ShowOnFocus" Width="400px">
                        <Tools>
                            <telerik:EditorToolGroup>
                                <telerik:EditorTool Name="Bold" />
                                <telerik:EditorTool Name="Italic" />
                                <telerik:EditorTool Name="Underline" />
                            </telerik:EditorToolGroup>
                        </Tools>
                    </telerik:RadEditor>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="QuestionLabel" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn Groupable="true" DataField="QuestionTypeID" DataType="System.Guid" HeaderText="Question Type" SortExpression="QuestionTypeID" UniqueName="QuestionTypeID">
<HeaderStyle width="135px" />
<ItemStyle Width="135px" />
                <EditItemTemplate>
                    <telerik:RadComboBox ID="cbQuestionTypes" Runat="server" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <telerik:RadComboBox ID="cbQuestionTypes" Runat="server" >
                    </telerik:RadComboBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="QuestionTypeIDLabel" runat="server" Text='<%# Eval("QuestionType") %>'></asp:Label>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn DataField="Version" DataType="System.Decimal" ReadOnly="True" Visible="False" UniqueName="Version" FilterControlAltText="Filter Version column" HeaderText="Version" SortExpression="Version" />
            <telerik:GridBoundColumn DataField="CreatedBy" DataType="System.Guid" FilterControlAltText="Filter CreatedBy column" HeaderText="CreatedBy" ReadOnly="True" SortExpression="CreatedBy" UniqueName="CreatedBy" Visible="False">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="TimeStamp" DataType="System.DateTime" FilterControlAltText="Filter TimeStamp column" HeaderText="TimeStamp" ReadOnly="True" SortExpression="TimeStamp" UniqueName="TimeStamp" Visible="False">
            </telerik:GridBoundColumn>
            <telerik:GridButtonColumn ConfirmText="Delete this Question?" ConfirmDialogType="RadWindow"
                      ItemStyle-Width="55px" HeaderStyle-Width="55px"   ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" />
        </Columns>
        
        <CommandItemStyle BackColor="#FFFFC0" />
    </MasterTableView>
    <ClientSettings AllowGroupExpandCollapse="true" EnableRowHoverStyle="true"> </ClientSettings>
</telerik:RadGrid>
        </td>
        <td style="width:50%; vertical-align:top;">

<telerik:RadGrid ID="RadGrid2" runat="server" AllowAutomaticInserts="true"  AllowMultiRowEdit="True" 
   AutoGenerateColumns="False" CellSpacing="-1" GridLines="Both">
    <ClientSettings EnablePostBackOnRowClick="true"  EnableRowHoverStyle="true">
        <Selecting AllowRowSelect="True" />
        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="400px" />
    </ClientSettings>
    <%--MasterTableView--%>
    <MasterTableView CommandItemSettings-AddNewRecordText="Add Answer" Name="Answers" DataKeyNames="AnswerID" CommandItemDisplay="Top" BackColor="#ccff99" ForeColor="Black" GridLines="Both" IsFilterItemExpanded="False" TableLayout="Fixed" >
<HeaderStyle BackColor="#33cc33" />
<ItemStyle BackColor="#99ff33" />
            <AlternatingItemStyle BackColor="#99ff66" />
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn1">
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle Width="50px" CssClass="MyImageButton"></ItemStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="QuestionID" DataType="System.Guid" ReadOnly="true" Visible="false" UniqueName="QuestionID" />
                    <telerik:GridTemplateColumn DataField="Answer" HeaderText="Answers" UniqueName="Answer">
                        <ItemTemplate>
                            <asp:Label ID="lblAnswer" runat="server" Width="350px" Text='<%# Eval("Answer")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadTextbox ID="txtAnswer" TextMode="MultiLine" Rows="2" Width="350px" runat="server" Text='<%# Bind("Answer") %>' />
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridTemplateColumn  DataField="isCorrectAnswer" HeaderText="is Correct" UniqueName="isCorrect">
<HeaderStyle width="75px" />
<ItemStyle Width="75px" />
                        <EditItemTemplate>
                            <asp:Checkbox id="chkEditIsCorrect" runat="server" checked='<%# Bind("isCorrectAnswer") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCorrect" runat="server" />
                            <asp:Checkbox id="chkEditIsCorrect" runat="server" Enabled="false" checked='<%# Eval("isCorrectAnswer")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
    </MasterTableView>
        <CommandItemStyle BackColor="#FFFFC0" />

</telerik:RadGrid>



        </td>
    </tr>
</table>

        </main>
    </form>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>

</body>
</html>
