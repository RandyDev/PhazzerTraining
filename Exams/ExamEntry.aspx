<%@ Page Language="vb" EnableSessionState="true" AutoEventWireup="false" CodeBehind="ExamEntry.aspx.vb" Inherits="Phazzer.ExamEntry" %>

<!DOCTYPE html>

<html lang="en">
<head>
	<telerik:RadStyleSheetManager runat="server" ID="RadStyleSheet1" />
    <link href="/styles/styles.css" rel="stylesheet" type="text/css"  />
    <title>PhaZZer Training</title>
    <link rel="SHORTCUT ICON" href="../Images/favicon.ico"/>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="h`ttps://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <link href="../css/styles.css" rel="stylesheet" type="text/css" /> 

    <style type="text/css">
        .fixedElement {
            background-color: #c0c0c0;
            position:fixed;
            top:66px;
            width:100%;
            z-index:100;
        }

        .margtop {
            padding-top:30px;
        }

        .Padding {
            padding-left:15px;
            padding-right:15px;
        }
        .PadImg {

            padding-right:115px;

        }
        .ScrollTop {
            color: blue;
font-weight:bold;
        }
        .auto-style1 {
            width: 142px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server" >
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="pagetimer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblTimeRemaining" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"  AsyncPostBackTimeOut="600" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="None" />
<telerik:RadSplitter Skin="Simple" runat="Server" ID="RadSplitter1" Width="100%" BorderSize="0"
    BorderStyle="None" PanesBorderSize="0"  Height="100%" Orientation="Horizontal">
	<telerik:RadPane ID="topPane" Scrolling="None" runat="server" Height="60px" MinHeight="5" MaxHeight="80" Index="0">
    <div class="row topBanner"><%-- top banner--%>
        <div class="col">
            <img alt="PHazzer Training" class="logoHeight" src="../images/PhazzerTrainingLogo1.png" />
       </div>
   </div><%-- end top banner--%>
</telerik:RadPane>
<telerik:RadSplitBar ID="RadSplitBar1"  runat="server" />


<telerik:RadPane MinWidth="1000" runat="server"  Scrolling="Y" ID="cPane">
    <div class="fixedElement mt-0">
              <table style="align-self:center;width:100%;">
                <tr>
                    <td style="text-align:center;">
                        <asp:Label ID="lblExamTitle" Font-Size="16px" Font-Bold="true" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    <div id="wrapper" style="text-align:center">
                <div id="divwelcome" runat="server" style="display:inline-block;width:100%;margin:auto;text-align:center;">
<br /><br /><br />
                                    <table align="center" style="width:410px;text-align:left; border: 1px solid black;">
                    <tr>
                        <td class="p-4">
                            <asp:Label ID="lblgreeting" runat="server" />,<br />
                            Welcome back to PhaZZer Training's online exam center.<br />
                            You took the <asp:Label ID="lblexamname" runat="server" /> on<br />
                            <asp:Label id="lblExamDate" runat="server" />  where you <asp:Label ID="lblPassFailret" runat="server" /><br /><br/>
                            For security purposes, we have your current IPV4 address as: <asp:label id="lblip" runat="server" />.<br /> The current time is: <asp:Label ID="lbltimestamp" runat="server" /><br />
                            By clicking the button below, you affirm that you are <em><asp:Label ID="lblfullname" runat="server" /></em>.<br /> If not, leave this page and report yourself immediately<br />
                            
                        </td>
                    </tr>
                </table><br />
                    <asp:Label ID="lblChoose" runat="server" Text="For a copy of your Certificate, Please select a Delivery Method" /><br />
<asp:Button ID="btnView" runat="server" Text="View" />&nbsp; <asp:Button ID="btnDownload" runat="server" Text="Download" />&nbsp;<asp:Button ID="btneMail" runat="server" Text="eMail" />
        <asp:Button ID="btnStart" BackColor="BlanchedAlmond" Width="325px" text="Click to Begin" runat="server" />
                   
                </div>
        </div>
        <div  style="position:relative;top:52px;margin:auto;width:100%;">

            <asp:PlaceHolder ID="phExam" runat="server" />


        <div id="divsubmit" runat="server" style="margin:auto;text-align:center">
            <span class="ScrollTop">
                Awesome! ... but <u>BEFORE</u> you submit your Exam for scoring we HIGHLY recommend you scroll to the top of the page and check that ALL questions have been answered.
            </span><br /><br />
         <telerik:RadButton ID="btnSubmit" BackColor="BlanchedAlmond" Width="325px" Text="Submit" runat="server" />
        <asp:label runat="server" ID="lblexam" Visible="false" />
            <br /><br /> <br />&nbsp;
        </div>

        </div>

        <div id="divscore" style="position:relative;top:52px;margin:auto;width:100%;" runat="server" visible="false">
            <center>

<b>Your Score</b><br />
            <table border="1"><tr><td style="text-align:left;" class="auto-style1">
                <asp:Label ID="lblPassFail" runat="server" />
                <br /> <asp:label ID="lblcorrectanswers" runat="server" />
                <br /> <asp:label ID="lblscore" runat="server" />
                <asp:label ID="lblmonkey" runat="server" />
                <br /> <asp:label ID="lblfinish" runat="server" />
            </td></tr></table>

            </center>
        </div>


<%--                        <div class="fixedElement" visible="false">
            <table style="align-self:center;width:100%;" >
                <tr>
                    <td style="width:20%;">
                        &nbsp;
                    </td>
                    <td style="width:60%;text-align:center;">
                        <asp:Label ID="Label1" Font-Size="16px" Font-Bold="true" runat="server" />
                    </td>
                    <td style="width:20%;padding-right:35px;text-align:right;">
                    </td>
                </tr>
            </table>
        </div>--%>
    

    </telerik:RadPane>
</telerik:RadSplitter>
<%--************************************************************************--%>
<%--************************************************************************--%>

    </form>
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>

</body>
</html>
