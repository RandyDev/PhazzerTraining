Public Class editProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim usrDAL As New userDAL()
            Dim usr As String = HttpContext.Current.User.Identity.Name
            Dim cuser As ssUser = usrDAL.GetUserByName(usr)
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
            usr = HttpContext.Current.User.Identity.Name
            cuser = usrDAL.GetUserByName(usr)
            cbCellCarrier.DataBind()
            LoadProfile(cuser)
        End If

    End Sub


    Private Sub LoadProfile(ByVal usr As ssUser)

        txtFirstName.Text = usr.FirstName
        txtLastName.Text = usr.LastName
        txtCompany.Text = usr.Company
        txtEmail.Text = usr.eMail
        txtPhone.Text = usr.Phone
        For Each itm As Telerik.Web.UI.RadComboBoxItem In cbCellCarrier.Items
            If itm.Value = usr.cellCarrierID.ToString Then

                itm.Selected = True
            End If
        Next
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

    Private Sub btnSaveProfile_Click(sender As Object, e As EventArgs) Handles btnSaveProfile.Click
        Dim usr As New ssUser
        Dim dal As New userDAL
        Dim retStr As String = String.Empty
        Dim np As String = txtnewPassword.Text.Trim
        If np > "" Then
            If np.Length > 4 Then
                Dim usrInfo As MembershipUser = Membership.GetUser(HttpContext.Current.User.Identity.Name)
                retStr = dal.UpdateEmail(usrInfo.ProviderUserKey, np)
                usrInfo.ChangePassword(usrInfo.GetPassword, np)
            Else
                retStr = "Unable to change password, min 5 characters"

            End If
        End If


        usr = dal.GetUserByID(New Guid(Session("UserID").ToString))
        usr.FirstName = txtFirstName.Text
        usr.LastName = txtLastName.Text
        usr.Company = txtCompany.Text
        usr.eMail = txtEmail.Text
        usr.Phone = txtPhone.Text
        usr.cellCarrierID = New Guid(cbCellCarrier.SelectedValue.ToString)
        If retStr = String.Empty Then
            retStr = dal.UpdateUser(usr)

            Utilities.setSessionVars(usr)
        End If
        If retStr = String.Empty Then
            retStr = "Changes Saved"
            lblerr.ForeColor = System.Drawing.Color.Green
        Else
            lblerr.ForeColor = System.Drawing.Color.Red
        End If
        lblerr.Text = retStr
    End Sub
End Class