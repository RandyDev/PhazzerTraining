<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UpdateExam.aspx.vb" Inherits="Phazzer.UpdateExam" %>

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
<style type="text/css"> 

    .viewWrap {
    padding: 15px;
    background-color:beige;
    -moz-box-shadow: inset 0 0px 5px rgba(41,51,58,0.5);
    -webkit-box-shadow: inset 0 0px 5px rgba(41,51,58,0.5);
    box-shadow: inset 0 0px 5px rgba(41,51,58,0.5);
}
    div.RadGrid .rgAltRow
{
   background: red;
   color: #000;
}
 .MyGridClass .rgDataDiv
{
height : 600px !important ;
padding:0px;
}

.RadGrid tr.Row50 
{ 
    padding-top:0; 
    padding-bottom:0; 
    height:15px; 
    vertical-align:middle; 
    background:orange; 
} 
 
.RadGrid .Row50 td 
{ 
    height:15px; 
    padding-top:0; 
    padding-bottom:0; 
    height:18px; 
    vertical-align:middle; 
} 
 
.RadGrid tr.Row1 
{ 
    height:15px; 
    background:green 
} 
 
.RadGrid .Row1 td 
{ 
    height:15px; 
    padding-top:0; 
    padding-bottom:0; 
    font:12px/12px sans-serif; 
} 
 form{
   height: 100%;
   margin: 0px;
   padding: 0px;
}
.element {
  position: relative;
  top: 50%;
  transform: translateY(-50%);
}
      .EditedItem, .EditedItem TABLE TR
     {
      background-color: #ffffe1;
      background-image: none;
     }
     .EditRow TD
     {
      border-bottom: 1px solid #d9d6cf;
     }
</style> 
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableScriptCombine="False"></telerik:RadScriptManager>  
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbExamlist">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="cbCourseList" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="lbtnSavePreReq">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbCourseList" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="lblSaved" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cbCourseList">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="lbtnSavePreReq" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="lblSaved" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cbDepartment">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbExamlist" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="cbCourseList" UpdatePanelCssClass="" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                        <telerik:AjaxUpdatedControl ControlID="RadGrid2" />
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
    <main id="content" runat="server" style="padding-top:40px;padding-left:12px;">

<table>
    <tr>
        <td>Company/Department<br /><telerik:RadComboBox  ID="cbDepartment" AutoPostBack="True"  EmptyMessage="Select Company/Department" runat="server" Width="227px" Filter="Contains" />
        </td>
            <td>Edit Exam:<br />
<telerik:RadComboBox ID="cbExamlist" EmptyMessage="Select Exam to Edit" AutoPostBack="true" runat="server" Width="479px" />

            </td>
        <td>
            <table><tr><td>
            CoursePreReq:<br /><telerik:RadComboBox AutoPostBack="true" ID="cbCourseList" EmptyMessage="Select Prerequisite Course Work" runat="server" Width="247px" Filter="Contains" MarkFirstMatch="True" />

                       </td>
                <td style="vertical-align:baseline;">
            &nbsp;&nbsp;<asp:LinkButton ID="lbtnSavePreReq" runat="server" ForeColor="Blue" Font-Size="12px" Text="Save Change" Visible="false" /><asp:Label ID="lblSaved" ForeColor="Red" Font-Size="12px" Text="Saved!" runat="server" Visible="false" />

                </td>

                   </tr></table>

        </td>
    </tr>
    </table>
<div id="divgrid" runat="server" class="MyGridClass" style="height:400px">

    
        <table style=" width:90%;height:400px">
    <tr>
<td colspan="2" style="align-content:center">      <asp:Label ID="Label1" ForeColor="Red" Text="Edits to this exam will cause the version number to change" runat="server" /><br /></td>
</tr><tr>
        <td style="width:50%; vertical-align:top;">
<telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None"
    AllowAutomaticInserts="True" AutoGenerateColumns="False" >
<HeaderContextMenu EnableAutoScroll="true"></HeaderContextMenu>
    <MasterTableView DataKeyNames="QuestionID" CommandItemDisplay="Top"  BackColor="#FFC080" ForeColor="Black" GridLines="None">
      <HeaderStyle BackColor="#ff9933" />
      <AlternatingItemStyle BackColor="#ffc080" />

<EditFormSettings>
<EditColumn UniqueName="EditCommandColumn1" FilterControlAltText="Filter EditCommandColumn1 column"></EditColumn>
</EditFormSettings>

      <ItemStyle BackColor="#ffe0c0" />
      <NoRecordsTemplate>
          No Questions Found
      </NoRecordsTemplate>
      <CommandItemSettings AddNewRecordText="Add Question" ShowRefreshButton="False"></CommandItemSettings>
        <Columns>
         <telerik:GridEditCommandColumn ButtonType="ImageButton" ItemStyle-Width="55px" HeaderStyle-Width="55px" >
<HeaderStyle Width="55px"></HeaderStyle>

<ItemStyle Width="55px"></ItemStyle>
            </telerik:GridEditCommandColumn>
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
                    <telerik:RadComboBox ID="cbQuestionTypes" Runat="server" />
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
                      ItemStyle-Width="55px" HeaderStyle-Width="55px"   ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" >
<HeaderStyle Width="55px"></HeaderStyle>

<ItemStyle Width="55px"></ItemStyle>
            </telerik:GridButtonColumn>
        </Columns>
        
        <CommandItemStyle BackColor="#FFFFC0" />
    </MasterTableView>
<GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

      <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" >
                <Selecting AllowRowSelect="true" />
                <scrolling allowscroll="True" usestaticheaders="True" />
            </ClientSettings>
</telerik:RadGrid>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PhazzerConnectionString %>" SelectCommand="SELECT [QuestionTypeID], [QuestionType] FROM [ExamQuestionTypes]"></asp:SqlDataSource>
        </td>
        <td style="width:50%;">

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

</div>
 </main>
    </form>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>

</body>
</html>

