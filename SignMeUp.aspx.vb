Imports Phazzer.Utilities
Imports Telerik.Web.UI

Public Class SignMeUP
    Inherits System.Web.UI.Page
    Dim udal As New userDAL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            '            GetData()
        End If
    End Sub
    Protected Function isValidForm() As String
        Dim retString As String = String.Empty
        If txtFirstName.Text.Trim = "" Then
            retString = "First Name is required<br />"
        End If
        If txtLastName.Text.Trim = "" Then
            retString &= "Last Name is required<br />"
        End If
        If txtEmail.Text.Trim = "" Then
            retString &= "eMail Address is required<br />"
        End If
        If Not isValidEmail(txtEmail.Text.Trim) Then
            retString &= "eMail Address is not valid<br />"
        End If
        If txtPassword.Text.Trim = "" Then
            retString &= "Password is required<br />"
        End If
        Return retString
    End Function

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        lblFormError.Visible = False
        Dim strError As String = isValidForm()
        If strError > String.Empty Then
            lblFormError.Text = strError
            lblFormError.Visible = True
            Exit Sub
        End If

        Dim nUser As New ssUser
        nUser.FirstName = txtFirstName.Text.Trim()
        nUser.MI = ""
        nUser.LastName = txtLastName.Text.Trim()
        nUser.eMail = txtEmail.Text.Trim()
        nUser.userName = txtEmail.Text.Trim()
        nUser.Company = txtCompany.Text.Trim()
        nUser.Password = txtPassword.Text.Trim()
        nUser.Phone = txtPhone.Text.Trim()
        nUser.cellCarrierID = New Guid(cbCellCarrier.SelectedValue)
        nUser.IsApproved = False
        Dim rlist As New List(Of String)
        Dim rl As String = "Client"
        rlist.Add(rl)
        nUser.myRoles = rlist
        Dim udal As New userDAL
        Dim strResponse As String = udal.AddUser(nUser)
        If strResponse <> "The user account was successfully created!" Then
            lblFormError.Text = strResponse
            lblFormError.Visible = True
            Exit Sub
        End If
        Dim validator As Guid = Guid.NewGuid()
        'TODO send email after email validated or upon sign up
        udal.SeteMailValidator(nUser.userID, validator)
        Dim eml As New rdMailer
        eml.To = nUser.eMail
        ''todo        eml.cc = nUser.Phone & "@txt.att.net"
        eml.Subject = "Welcome to Phazzer Training"
        Dim strBody As String = "Welcome to Phazzer Training" & vbCrLf & "Please follow this link To validate your email address" & vbCrLf & "http://PhazzerTraining.com/eMailValidator.aspx?PhazzerTrainingValID=" & validator.ToString
        eml.Body = strBody
        rdMailer.SendMail(eml)
        pnlForm.Visible = False
        pnlsuccess.Visible = True
    End Sub



    'Protected Sub OnUpload(sender As [Object], e As EventArgs)
    '    Dim bytes As Byte() = Convert.FromBase64String(ImageVal.Value.Split(","c)(1))
    '    Using stream As New FileStream(Server.MapPath("~/Images/" + txtImageName.Text.Trim() + ".png"), FileMode.Create)
    '        stream.Write(bytes, 0, bytes.Length)
    '        stream.Flush()
    '    End Using

    '    Using con As New SqlConnection(contring)
    '        Using cmd As New SqlCommand("INSERT INTO [File](FileName) VALUES(@FileName)", con)
    '            cmd.Parameters.AddWithValue("@FileName", "Images/" + txtImageName.Text.Trim() + ".png")
    '            Using da As New SqlDataAdapter(cmd)
    '                Dim dt As New DataTable()
    '                da.Fill(dt)
    '            End Using
    '        End Using
    '    End Using
    '    txtImageName.Text = ""
    '    Me.GetData()
    'End Sub

    'Private Sub GetData()
    '    Using con As New SqlConnection(contring)
    '        Using cmd As New SqlCommand("SELECT [FileName] FROM [File]", con)
    '            Using sda As New SqlDataAdapter(cmd)
    '                Dim dt As New DataTable()
    '                sda.Fill(dt)
    '                GvImage.DataSource = dt
    '                GvImage.DataBind()
    '            End Using
    '        End Using
    '    End Using
    'End Sub


End Class