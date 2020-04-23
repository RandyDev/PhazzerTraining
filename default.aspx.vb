Imports Telerik.Web.UI

Public Class _default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim usrDAL As New userDAL()
            Dim usr As String = HttpContext.Current.User.Identity.Name
            Dim cuser As ssUser = usrDAL.GetUserByName(usr)
            litName.Text = cuser.FirstName
            Utilities.setSessionVars(cuser)
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

    End Sub
    Private Sub LoginStatus1_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginStatus1.LoggedOut
        FormsAuthentication.SignOut()
        Session.Abandon()
        Dim cookie1 = New HttpCookie(FormsAuthentication.FormsCookieName, "")
        cookie1.Expires = Date.Now.AddYears(-1)
        Response.Cookies.Add(cookie1)
        Dim cookie2 As HttpCookie = New HttpCookie("ASP.NET_SessionId", "")
        cookie2.Expires = DateTime.Now.AddYears(-1)
        Response.Cookies.Add(cookie2)
        Response.Redirect("~/")
    End Sub
End Class