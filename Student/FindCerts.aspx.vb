Imports Telerik.Web.UI
Imports Phazzer.Utilities
Imports System.Web.Services
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
'Imports System.ConfigurationPublic
Class FindCerts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me.IsPostBack) Then
            Me.radgrid1.Visible = True
            If User.IsInRole("Administrator") Then
                cbuser.Visible = True
                RadMenu1.DataSourceID = "XmlDataSource0"
            ElseIf User.IsInRole("Clerk") Then
                cbuser.Visible = True
                RadMenu1.DataSourceID = "XmlDataSource1"
            ElseIf User.IsInRole("Manager") Then
                cbuser.Visible = True
                RadMenu1.DataSourceID = "XmlDataSource2"
            ElseIf User.IsInRole("Instructor") Then
                cbuser.Visible = True
                RadMenu1.DataSourceID = "XmlDataSource3"
            ElseIf User.IsInRole("Student") Then
                RadMenu1.DataSourceID = "XmlDataSource4"
                cbuser.Visible = False

            ElseIf User.IsInRole("Client") Then
                RadMenu1.DataSourceID = "XmlDataSource5"
                cbuser.Visible = False
            Else
                RadMenu1.Visible = False
                cbuser.Visible = False
            End If
            Load_cbUser()
        End If
        'If Phazzer.Utilities.IsValidGuid(Session("UserID").ToString) Then
        'txtUserName.Visible = False

        '        End If
    End Sub

    Private Sub Radgrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles radgrid1.NeedDataSource
        Dim edal As New ExamDAL
        Dim uid As Guid
        'todo should be admin YN then user then selected 
        If cbuser.SelectedIndex > -1 Then
            uid = New Guid(cbuser.SelectedValue)
            lbluserName.Text = cbuser.Text & "'s "
        ElseIf IsValidGuid(Session("UserID").ToString) Then
            uid = Session("UserID")
            lbluserName.Text = "MY "
        Else
            uid = zeroGuid()
        End If
        Dim dt As DataTable = edal.getCertsbyUserID(uid)
        For Each row In dt.Rows
            Dim xa As String = row.item("ExamName")
            Dim xb As Boolean = row.item("Pass")
            Dim xc As Date = row.item("examDate")
            Dim xd As String = row.item("CertificateName")
            Dim xe As Guid = row.item("studentExamID")
            Dim xf As Guid = row.item("examID")
            Dim xg As Guid = row.item("CertificateID")
            Dim xh As Guid = row.item("studentID")
        Next
        radgrid1.DataSource = dt
    End Sub
    Private Sub Radgrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles radgrid1.ItemCommand
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim itm As GridDataItem = DirectCast(e.Item, GridDataItem)
            If e.CommandName = "getcert" Then
                Dim uid As Guid = e.CommandArgument
                Dim studenExamID As Guid = New Guid(itm.OwnerTableView.DataKeyValues(itm.ItemIndex)("studentExamID").ToString)
                Dim a As String = String.Empty

            End If
        End If
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

    'Private Sub cbuser_ItemsRequested(sender As Object, e As RadComboBoxItemsRequestedEventArgs) Handles cbuser.ItemsRequested
    '    If cbuser.Text.Trim.Length > 2 Then
    '        Dim searchtext As String = cbuser.Text.Trim
    '        Dim dba As New DBAccess
    '        dba.CommandText = "SELECT distinct studentExams.studentID,UserProfile.FirstName + ' ' + UserProfile.LastName AS userName " &
    '            "FROM studentExams INNER JOIN " &
    '            " UserProfile ON studentExams.studentID = UserProfile.userID " &
    '            "Where (UserProfile.FirstName Like '%' + @searchtext + '%') OR (UserProfile.LastName Like '%' + @searchtext + '%')"
    '        dba.AddParameter("@searchtext", searchtext)
    '        Dim dt As DataTable = dba.ExecuteDataSet.Tables(0)
    '        cbuser.DataSource = dt
    '        cbuser.DataTextField = "userName"
    '        cbuser.DataValueField = "userID"
    '    End If
    'End Sub
    Private Sub Radgrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles radgrid1.ItemDataBound
        Dim edal As ExamDAL = New ExamDAL
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim sxamid As Guid = e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("studentExamID")
            Dim hlView As HyperLink = e.Item.FindControl("hlView")
            Dim hlDownload As HyperLink = e.Item.FindControl("hlDownload")
            Dim hlemail As HyperLink = e.Item.FindControl("hlEmail")
            hlView.NavigateUrl = "~/Exams/ShowCert.aspx?sxamID=" & sxamid.ToString & "&view=yes"
            hlDownload.NavigateUrl = "~/Exams/ShowCert.aspx?sxamID=" & sxamid.ToString & "&dld=yes"
            hlemail.NavigateUrl = "~/Exams/ShowCert.aspx?sxamID=" & sxamid.ToString & "&email=yes"

        End If

    End Sub
    Private Sub Load_cbUser()
        Dim dba As New DBAccess With {
        .CommandText = "SELECT distinct studentExams.studentID, " &
            "UserProfile.FirstName + ' ' +UserProfile.LastName  " &
            "as userName " &
            "From studentExams INNER Join " &
            " UserProfile ON studentExams.studentID = UserProfile.userID"}
        Dim dt As DataTable = New DataTable
        dt = dba.ExecuteDataSet.Tables(0)
        cbuser.DataSource = dt
        cbuser.DataTextField = "userName"
        cbuser.DataValueField = "studentID"
        cbuser.DataBind()
    End Sub

    Private Sub Cbuser_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbuser.SelectedIndexChanged
        radgrid1.Rebind()
    End Sub
End Class