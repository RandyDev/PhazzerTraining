Public Class ExamEntry
    Inherits System.Web.UI.Page
    Dim timer As Integer = 0
    Dim timercount As Integer = 60
    Dim prevquestionid As String = String.Empty
    Dim questionnumber As Integer = 0
    Dim examid As String = String.Empty
    Dim examName As String = String.Empty
    Dim examType As String = String.Empty
    Dim studentid As String = String.Empty
    Dim studentExamID As Guid = Guid.NewGuid
    Public sealist = New List(Of StudentExamAnswer)()
    Dim eventid As String = String.Empty
    Dim _mUser As ssUser = New ssUser()
    Dim isExpired As Boolean = True ' default to expired
    Dim theyTookExam As Boolean = False
    Dim theyPassed As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("exam") = "" Then
                Response.Redirect("~/UnauthorizedNoMaster.aspx")
                Exit Sub
            ElseIf Request("exam").Length < 72 Then
                Response.Redirect("~/UnauthorizedNoMaster.aspx")
                Exit Sub
            Else 'Request("exam") is pressent and length > 72
                'extract the info in Request("exam")
                Dim strRequest As String() = Split(Request("exam"), ":")
                If strRequest.Length = 3 Then
                    studentid = strRequest(0)
                    eventid = strRequest(2)
                    Session("studentid") = studentid.ToString

                    If (Not Utilities.IsValidGuid(studentid)) Then
                        Response.Redirect("~/UnauthorizedNoMaster.aspx")
                        Exit Sub
                    Else 'store studentID to Session variables
                        Session("studenID") = studentid.ToString
                    End If ' StudentID is valid Guid
                    examid = strRequest(1)
                    If (Not Utilities.IsValidGuid(examid)) Then
                        Response.Redirect("~/UnauthorizedNoMaster.aspx")
                        Exit Sub
                    Else 'store esxamID to session variables
                        Session("examid") = studentid.ToString
                    End If 'examID is valid guid
                Else 'request variables count <> 3
                    Response.Redirect("~/UnauthorizedNoMaster.aspx")
                    Exit Sub
                End If 'request variables count = 3
            End If 'Request("exam") > ""       
            '**********************************************************************
            '********************we have valid variables to use********************
            '**************studentid and examid are stored to Session**************
            '**********************************************************************
            'have they already taken the exam?
            'go get student exam
            Dim exDAL As New ExamDAL
            Dim studentExam As StudentExam = exDAL.getStudentExam(New Guid(studentid), New Guid(examid))
            Session("studentExam") = studentExam
            theyTookExam = studentExam.StudentExamID <> Utilities.ZeroGuid
            If theyTookExam = True Then
                lblExamDate.Text = studentExam.ExamDate.ToString("MMMM dd, yyyy")
                Dim expiryDate As Date = Nothing
                Dim dba As New DBAccess
                'did they pass?
                theyPassed = studentExam.Pass

                If theyPassed Then
                    lblcorrectanswers.Visible = True
                    btnView.Visible = True

                    btnStart.Visible = False
                    'is this a user ore instructor exam?
                    'get examtype examName and set expiry date
                    Dim examID As String = studentExam.ExamID.ToString
                    dba.CommandText = "Select ExamName FROM Exams WHERE ExamID = @ExamID"
                    dba.AddParameter("@ExamID", studentExam.ExamID.ToString)
                    examName = dba.ExecuteScalar
                    If examName > "" Then 'Determine examtype and exiry date
                        If examName.Contains("Instructor") Then
                            examType = "Instructor"
                            expiryDate = DateAdd(DateInterval.Year, 2, studentExam.ExamDate)
                        ElseIf examName.Contains("User") Then
                            examType = "User"
                            expiryDate = DateAdd(DateInterval.Year, 1, studentExam.ExamDate)
                        End If 'determine cert type : user or instructor
                        isExpired = expiryDate < Date.Now
                        If Not isExpired Then 'showcert
                            lblPassFailret.Text = "<em>Passed</em> the exam.<br/> Your certification doesn't expire until " & expiryDate.ToString("MMMM dd,yyyy")
                        End If
                    End If ' ExamName > ""
                Else
                    lblPassFailret.Text = " <em>almost</em> Passed the exam. ;)"
                    btnStart.Visible = True
                    btnStart.Text = "Re-Take Exam"
                    lblChoose.Visible = False
                    btnView.Visible = False
                    btnDownload.Visible = False
                    btneMail.Visible = False
                End If
            Else 'not theyTookExam
                Response.Redirect("~/Exams/Exam.aspx?exam=" & studentid & ":" & examid & ":" & eventid)
            End If 'theyTookExam

            lblip.Text = Request.ServerVariables("REMOTE_ADDR")
            Dim label As System.Web.UI.WebControls.Label = lbltimestamp
            Dim now As DateTime = DateTime.Now
            label.Text = String.Concat("approximately ", now.ToString())
            lblexam.Text = examid
            divsubmit.Visible = False
            If (Utilities.IsValidGuid(studentid)) Then
                Dim userDAL As UserDAL = New UserDAL()
                _mUser = userDAL.GetUserByID(New Guid(studentid))
                lblgreeting.Text = String.Concat("Hello ", _mUser.FirstName)
                Dim label1 As System.Web.UI.WebControls.Label = lblfullname
                Dim firstName() As String = {_mUser.FirstName, " ", _mUser.LastName, " at ", _mUser.eMail}
                label1.Text = String.Concat(firstName)
                Dim dba As DBAccess = New DBAccess("Phazzer")
                dba.CommandText = "Select ExamName from Exams WHERE ExamID=@ExamID"
                dba.AddParameter("@ExamID", examid)
                lblexamname.Text = dba.ExecuteScalar()
                lblExamTitle.Text = lblexamname.Text
                Session("ExamTitle") = lblexamname.Text
                Session("studentExamID") = studentExam.StudentExamID
            End If
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        'todo pass 2nd attempt back to exams
        Dim redirectToExam As String = "~/Exams/Exam.aspx?exam=" & Request("exam")
        Response.Redirect(redirectToExam)
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Response.Redirect("~/Exams/ShowCert.aspx?view=yes&sxamid=" & Session("studentExamID").ToString)
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Response.Redirect("~/Exams/ShowCert.aspx?dld=yes&sxamid=" & Session("studentExamID").ToString)
    End Sub

    Private Sub btneMail_Click(sender As Object, e As EventArgs) Handles btneMail.Click
        Response.Redirect("~/Exams/ShowCert.aspx?eMail=yes&sxamid=" & Session("studentExamID").ToString)
    End Sub
End Class