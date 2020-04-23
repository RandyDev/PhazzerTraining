Imports Telerik.Web.UI

Public Class ClassList
    Inherits System.Web.UI.Page
    Dim usr As New ssUser

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me.IsPostBack) Then
            Dim usrDAL As New userDAL()
            Dim usr As String = HttpContext.Current.User.Identity.Name
            Dim cuser As ssUser = usrDAL.GetUserByName(usr)
            lblFirstLast.Text = cuser.FirstName & " " & cuser.LastName & "'s"
            Utilities.setSessionVars(cuser)
            If User.IsInRole("Administrator") Then
                RadMenu1.DataSourceID = "XmlDataSource0"
            ElseIf User.IsInRole("Clerk") Then
                RadMenu1.DataSourceID = "XmlDataSource1"
            ElseIf User.IsInRole("Managerr") Then
                RadMenu1.DataSourceID = "XmlDataSource2"
            ElseIf User.IsInRole("Instructor") Then
                RadMenu1.DataSourceID = "XmlDataSource3"
            ElseIf User.IsInRole("Student") Then
                RadMenu1.DataSourceID = "XmlDataSource4"
            ElseIf User.IsInRole("Client") Then
                RadMenu1.DataSourceID = "XmlDataSource5"
            Else
                RadMenu1.Visible = False
            End If
            Me.RadGrid1.Visible = True
            Me.lblListTitle.Text = "Class List"
            Me.btnViewClasses.Visible = False
            Me.RadGrid2.Visible = False
        End If
        Dim userDAL As userDAL = New userDAL()
        usr = userDAL.GetUserByName(User.Identity.Name)
        HttpContext.Current.Session("userID") = usr.userID.ToString()
        Me.User.IsInRole("Manager")
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim eventRegDAL As EventRegDAL = New EventRegDAL()
        Dim dataTable As System.Data.DataTable = New System.Data.DataTable()
        dataTable = eventRegDAL.getMyEvents(Session("userID"))
        Me.RadGrid1.DataSource = dataTable
    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If (e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item) Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            If item("hasInstructor").Text = "False" Then
                item.CssClass = "noInstructor"
            End If
            Dim eventRegDAL As EventRegDAL = New EventRegDAL()
            Dim btnLoadAttendees As RadButton = DirectCast(item.FindControl("btnLoadAttendees"), RadButton)
            item("count").Text = eventRegDAL.countEventRegistrants(item("EventID").Text)
            btnLoadAttendees.Text = "Load Attendee(s)"
            btnLoadAttendees.CommandArgument = item("EventID").Text.ToString()
        End If
    End Sub

    Private Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Dim strEventID As String = e.CommandArgument
        Dim commandName As String = e.CommandName
        Dim radButton As Telerik.Web.UI.RadButton = DirectCast(e.Item.FindControl("btnLoadAttendees"), Telerik.Web.UI.RadButton)
        Dim utility As Utilities = New Utilities()
        Dim userDAL As userDAL = New userDAL()
        Session("eventID") = strEventID
        RadGrid2.Visible = True
        RadGrid2.Rebind()
        lblListTitle.Text = "Attendee List"
        Dim eventRegDAL As EventRegDAL = New EventRegDAL()
        Dim dt As DataTable = eventRegDAL.geteventbyID(CType(strEventID, Integer))
        lblCourseTitle.Text = dt.Rows(0).Item("eventName") & "<br />" & dt.Rows(0).Item("eventStartDate")
        btnViewClasses.Visible = True
        RadGrid1.Visible = False
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim eventRegDAL As EventRegDAL = New EventRegDAL()
        Dim eventID As Integer = Session("eventID")
        Dim eventRegistrants As List(Of ssUser) = eventRegDAL.GetEventRegistrants(eventID)
        RadGrid2.DataSource = eventRegistrants
        Session("eventRegistrants") = eventRegistrants
    End Sub

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If (e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item) Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim btnSendExam As RadButton = DirectCast(item.FindControl("btnSendExam"), RadButton)
            Dim btnSendROL As RadButton = DirectCast(item.FindControl("btnSendROL"), RadButton)
            Dim eventRegDAL As EventRegDAL = New EventRegDAL()
            btnSendROL.Visible = Not eventRegDAL.isROLcurrent(Session("eventID"), item("username").Text, item.GetDataKeyValue("BookingID"))
            btnSendROL.BackColor = System.Drawing.Color.Orange
            '*************************************************
            '*************************************************
            '*************************************************
            btnSendExam.CommandArgument = item("username").Text.ToString() & ":" & Session("examid") & ":" & Session("eventID")
            btnSendROL.CommandArgument = item("username").Text.ToString() & ":" & item.GetDataKeyValue("BookingID")
        End If
    End Sub


    Private Sub RadGrid2_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid2.UpdateCommand
        Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        Dim _ssuser As ssUser = New ssUser()
        Dim userDAL As userDAL = New userDAL()
        If (TypeOf e.Item Is Telerik.Web.UI.GridEditFormItem AndAlso e.Item.IsInEditMode) Then
            Dim gridEditFormItem As Telerik.Web.UI.GridEditFormItem = DirectCast(e.Item, Telerik.Web.UI.GridEditFormItem)
            Dim parentItem As GridDataItem = gridEditFormItem.ParentItem
            _ssuser.userName = item.GetDataKeyValue("UserName")
            _ssuser = userDAL.GetUserByName(_ssuser.userName)
            Dim radTextBox As Telerik.Web.UI.RadTextBox = DirectCast(gridEditFormItem.FindControl("txtAltEmail"), Telerik.Web.UI.RadTextBox)
            Dim dba As DBAccess = New DBAccess("Phazzer") With {
                .CommandText = "UPDATE UserProfile SET altEmail = @altEmail WHERE userID = @userID"
            }
            dba.AddParameter("@altEmail", radTextBox.Text)
            dba.AddParameter("@userID", _ssuser.userID)
            dba.ExecuteNonQuery()
        End If
        Me.RadGrid2.Rebind()
    End Sub

    Private Sub RadGrid2_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid2.ItemCommand
        Dim udal As New userDAL
        Dim commandName As String = e.CommandName
        If commandName = "SendExam" Then
            Dim str As String() = Split(e.CommandArgument, ":")
            Dim command As String = "SendExam"
            Dim eventRegDAL As EventRegDAL = New EventRegDAL()
            Dim btnSendExam As RadButton = DirectCast(e.Item.FindControl("btnSendExam"), RadButton)
            Dim utility As Utilities = New Utilities()
            Dim userByName As ssUser = (New userDAL()).GetUserByName(str(0))
            Dim examID As String = eventRegDAL.getexamid(Session("eventID"))
            utility.SendExam(userByName, examID, Session("eventID"))
            If (Not Utilities.isValidEmail(userByName.AltEmail)) Then
                btnSendExam.ForeColor = System.Drawing.Color.Black
                btnSendExam.Text = String.Concat("Exam Sent to: " & vbCrLf & userByName.FirstName & " " & userByName.LastName & " at" & vbCrLf & userByName.eMail)
            Else
                btnSendExam.ForeColor = System.Drawing.Color.Black
                btnSendExam.Text = String.Concat("Exam Sent to: " & vbCrLf & userByName.FirstName & " " & userByName.LastName & " at" & vbCrLf & userByName.AltEmail)
            End If
            btnSendExam.BackColor = System.Drawing.Color.YellowGreen
            btnSendExam.Enabled = False
        ElseIf commandName = "SendROL" Then
            Dim Carg() As String = Split(e.CommandArgument, ":")
            Dim userName As String = Carg(0)
            Dim BookingID As String = Carg(1)
            Dim commandName2 As String = e.CommandName
            Dim eventRegDAL1 As EventRegDAL = New EventRegDAL()
            Dim btnSendROL As RadButton = DirectCast(e.Item.FindControl("btnSendROL"), RadButton)
            Dim utility1 As Utilities = New Utilities()
            Dim _ssuser As ssUser = udal.GetUserByName(userName)
            Dim eventID As Integer = Session("eventID")
            utility1.SendROL(_ssuser, eventID, Carg(1))

            If (Not Utilities.isValidEmail(_ssuser.AltEmail)) Then
                btnSendROL.ForeColor = System.Drawing.Color.Black
                btnSendROL.Text = String.Concat("ROL Sent to: " & vbCrLf & "", _ssuser.eMail)
            Else
                btnSendROL.ForeColor = System.Drawing.Color.Black
                btnSendROL.Text = String.Concat("ROL Sent to: " & vbCrLf & "", _ssuser.AltEmail)
            End If

            btnSendROL.BackColor = System.Drawing.Color.YellowGreen
            btnSendROL.Enabled = False
        ElseIf commandName = "Edit" Then
        ElseIf commandName = "Update" Then
        End If
    End Sub
    Private Sub ShowCertList(ByVal sender As Object, ByVal e As CommandEventArgs)
    End Sub
    Private Sub BtnViewClasses_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewClasses.Click
        Me.RadGrid2.Visible = False
        Me.RadGrid1.Rebind()
        Me.RadGrid1.Visible = True
        Me.lblListTitle.Text = "Class List"
        lblCourseTitle.Text = Nothing
        Me.btnViewClasses.Visible = False
    End Sub
    Private Sub LoginStatus1_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginStatus1.LoggedOut
        FormsAuthentication.SignOut()
        Session.Abandon()
        Dim cookie1 = New HttpCookie(FormsAuthentication.FormsCookieName, "") With {
            .Expires = Date.Now.AddYears(-1)
        }
        Response.Cookies.Add(cookie1)
        Dim cookie2 As HttpCookie = New HttpCookie("ASP.NET_SessionId", "") With {
            .Expires = DateTime.Now.AddYears(-1)
        }
        Response.Cookies.Add(cookie2)
        Response.Redirect("~/")
    End Sub
End Class