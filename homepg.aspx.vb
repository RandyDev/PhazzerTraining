Public Class homepg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim usrDAL As New userDAL()
            Dim usr As String = HttpContext.Current.User.Identity.Name
            Dim cuser As ssUser = usrDAL.GetUserByName(usr)
            '           lblFirstLast.Text = cuser.FirstName


        End If
    End Sub

End Class