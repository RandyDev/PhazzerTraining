Imports Telerik.Web.UI

Public Class ClassAssignments
    Inherits System.Web.UI.Page
    Dim usr As ssUser = New ssUser


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If (Not Me.IsPostBack) Then
            Me.RadGrid1.Visible = True
            If User.IsInRole("Administrator") Then
                RadMenu1.DataSourceID = "XmlDataSource0"
            ElseIf User.IsInRole("Clerk") Then
                RadMenu1.DataSourceID = "XmlDataSource1"
            ElseIf User.IsInRole("Manager") Or User.IsInRole("Master Instructor") Or User.IsInRole("Senior Instructor") Then
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
        End If


        Dim userDAL As userDAL = New userDAL()
        Me.usr = userDAL.GetUserByName(Me.User.Identity.Name)
        HttpContext.Current.Session("userID") = Me.usr.userID.ToString()
        Me.User.IsInRole("Instructor")
    End Sub

    Private Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "AddInstructor" Then
            Dim strArrays As String() = Strings.Split(e.CommandArgument, ":")
            '            strArrays(0)
            Dim str As String = strArrays(1)
            Dim eventRegDAL As EventRegDAL = New EventRegDAL()
            eventRegDAL.addinstructor(strArrays(0), strArrays(1))
        End If
        Dim radButton As Telerik.Web.UI.RadButton = DirectCast(e.Item.FindControl("RadButton1"), Telerik.Web.UI.RadButton)
        Dim utility As Utilities = New Utilities()
        Dim userDAL As userDAL = New userDAL()
        Dim examDAL As ExamDAL = New ExamDAL()
        Me.RadGrid1.Rebind()
        Me.RadGrid2.Rebind()
    End Sub


    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If (e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item) Then
            Dim radButton As Telerik.Web.UI.RadButton = DirectCast(e.Item.FindControl("RadButton1"), Telerik.Web.UI.RadButton)
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            radButton.CommandArgument = String.Concat(item("EventID").Text.ToString(), ":", Me.Session("userID").ToString())
            radButton.CommandName = "AddInstructor"
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim eventsWOInstructors As DataTable = (New EventRegDAL()).GetEventsWOInstructors()
        Me.RadGrid1.DataSource = eventsWOInstructors
    End Sub

    Private Sub RadGrid2_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid2.ItemCommand
        Dim eventRegDAL As EventRegDAL = New EventRegDAL()
        Dim strArrays As String() = Strings.Split(e.CommandArgument, ":")
        Dim [integer] As Integer = strArrays(0)
        Dim str As String = strArrays(1)
        Dim limeGreen As RadButton = DirectCast(e.Item.FindControl("RadButton1"), RadButton)
        limeGreen.Text = "Request Sent"
        limeGreen.BackColor = System.Drawing.Color.LimeGreen
        limeGreen.Enabled = False
        If e.CommandName = "DropClass" Then
            eventRegDAL.requestDrop(strArrays(0), strArrays(1))
        End If
        RadGrid3.Rebind()
        Me.Session("eventnum") = [integer]

    End Sub

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If (e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item) Then
            Dim str As RadButton = DirectCast(e.Item.FindControl("RadButton1"), RadButton)
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim dba As DBAccess = New DBAccess("Phazzer") With {
                .CommandText = "select count(eventid) from DropRequest WHERE eventID=@eventID and userID=@userID AND isClosed=0"
            }
            dba.AddParameter("@eventID", item("EventID").Text.ToString())
            dba.AddParameter("@userID", Me.Session("userID").ToString())
            Dim count As Integer = dba.ExecuteScalar
            If Not count > 0 Then
                str.CommandArgument = item("EventID").Text.ToString() & ":" & Me.Session("userID")
            Else
                str.Text = "Drop Pending"
                str.BackColor = System.Drawing.Color.LimeGreen
                str.Enabled = False

            End If
        End If
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim dataTable As System.Data.DataTable = New System.Data.DataTable()
        Dim eventRegDAL As EventRegDAL = New EventRegDAL()
        Dim guid As System.Guid = Me.usr.userID
        dataTable = eventRegDAL.getMyEvents(guid.ToString())
        Me.RadGrid2.DataSource = dataTable
    End Sub

    Private Sub RadGrid3_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid3.NeedDataSource
        Dim dal As New EventRegDAL
        Dim drlist As List(Of DropRequest) = dal.GetDropRequests
        RadGrid3.DataSource = drlist
    End Sub
    Private Sub RadGrid3_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid3.ItemDataBound
        If (e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item) Then
            Dim str As Telerik.Web.UI.RadButton = DirectCast(e.Item.FindControl("RadButton1"), Telerik.Web.UI.RadButton)
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            str.CommandArgument = item.Cells(3).Text.ToString() & ":" & Me.Session("userID")
            str.CommandName = "ApproveDrop"
        End If
    End Sub
    Private Sub RadGrid3_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid3.ItemCommand
        If e.CommandName = "ApproveDrop" Then
            Dim strArrays As String() = Strings.Split(e.CommandArgument, ":")
            '            strArrays(0)
            Dim str As String = strArrays(1)
            Dim eventRegDAL As EventRegDAL = New EventRegDAL()
            eventRegDAL.removeinstructor(strArrays(0))
            RadGrid1.Rebind()
            RadGrid2.Rebind()
            RadGrid3.Rebind()

        End If
    End Sub

    Private Sub LoginStatus1_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginStatus1.LoggedOut
        FormsAuthentication.SignOut()
        Session.Abandon()
        Dim cookie1 = New HttpCookie(FormsAuthentication.FormsCookieName, "") With {
            .Expires = Date.Now.AddYears(-1)
        }
        Response.Cookies.Add(cookie1)
        Dim cookie2 As HttpCookie = NewMethod()
        cookie2.Expires = DateTime.Now.AddYears(-1)
        Response.Cookies.Add(cookie2)
        Response.Redirect("~/")
    End Sub

    Private Shared Function NewMethod() As HttpCookie
        Return New HttpCookie("ASP.NET_SessionId", "")
    End Function
End Class