Imports System.Net

Public Class Exam
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
    Dim studentExam As StudentExam = Nothing
    Public sealist = New List(Of StudentExamAnswer)()
    Dim eventid As String = String.Empty
    Dim _mUser As ssUser = New ssUser()
    Dim isExpired As Boolean = True ' default to expired
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'VALIDATE params
        If Request("exam") = "" Then 'gotta have one
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
            Exit Sub
        ElseIf Request("exam").Length < 74 Then 'two guids and three colons 
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
            Exit Sub
        Else 'Request("exam") is pressent and length > 74
            'extract the info in Request("exam")
            Dim strRequest As String() = Split(Request("exam"), ":")
            If strRequest.Length = 3 Then
                studentid = strRequest(0)
                eventid = strRequest(2)
                If (Not Utilities.IsValidGuid(studentid)) Then
                    Response.Redirect("~/UnauthorizedNoMaster.aspx")
                    Exit Sub
                Else 'store studentID to Session variables
                    Session("studenID") = studentid
                End If ' StudentID is valid Guid
                examid = strRequest(1)
                If (Not Utilities.IsValidGuid(examid)) Then
                    Response.Redirect("~/UnauthorizedNoMaster.aspx")
                    Exit Sub
                Else 'store esxamID to session variables
                    Session("examid") = examid
                End If 'examID is valid guid
            Else 'request variables count <> 3
                Response.Redirect("~/UnauthorizedNoMaster.aspx")
                Exit Sub
            End If 'request variables count = 3
        End If 'Request("exam") > ""       
        lblemailconfirm.Text = ""
        '**********************************************************************
        '********************we have valid variables to use********************
        '**************studentid and examid are stored to Session**************
        'todo create delegate 
        '**********************************************************************
        If IsPostBack Then
            divwelcome.Visible = False
            examid = lblexam.Text
            If (Utilities.IsValidGuid(examid)) Then
                divwelcome.Visible = False
                phExam.Visible = True
                examid = lblexam.Text
                renderExam(examid)
            End If 'is valid examid

        Else 'Not a PostBack
            'have they taken the exam?
            Dim exDAL As New ExamDAL
            '        get the studentExam for student using studentid and examid(could be two records?
            studentExam = exDAL.getStudentExam(New Guid(studentid), New Guid(examid))
            'store exam in session variable
            Session("studentExam") = studentExam
            Dim theyTookExam As Boolean = studentExam.StudentExamID <> Utilities.ZeroGuid
            'if theyTookExam, did they pass
            Dim theyPassed As Boolean = exDAL.theyPassed(studentExam.StudentExamID.ToString)

            If theyTookExam = True And theyPassed Then
                Response.Redirect("~/Exams/ExamEntry.aspx?exam=" & studentid & ":" & examid & ":" & eventid)
                'todo catch redirect FROM ExamEntry.aspx
            ElseIf theyTookExam And Not theyPassed Then
                'if referrer is entry, retake exam
                btnStart.Text = " Re-Take Exam "
            Else ' didn't take exam
                'Take Exam
            End If 'theyTookExam

        End If
        Dim label As System.Web.UI.WebControls.Label = lbltimestamp
        Dim now As DateTime = DateTime.Now
        label.Text = String.Concat("approximately ", now.ToString())
        lblexam.Text = examid
        lblTimeRemaining.Visible = False
        lbltimer.Visible = False
        pagetimer.Interval = 5500
        pagetimer.Enabled = False
        lbltimer.Text = "00:30:00"
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
        End If 'isvalid(studenti)

    End Sub 'page_load

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        'todo, have they already taken the exam?
        Dim dba As DBAccess = New DBAccess("Phazzer")
        Dim edal As New ExamDAL
        Dim sxam As StudentExam = Session("studentExam")

        If (sxam.Attempts + 1) > edal.getAllowedAttemptsByExamId(sxam.ExamID) Then
            'abort
        Else sxam.Attempts += 1 'increment attempts
            dba.CommandText = "Update studentexams SET Attempts=@Attempts WHERE studentExamID=@studentExamID"
            dba.AddParameter("Attempts", sxam.Attempts)
            dba.AddParameter("studentExamID", sxam.StudentExamID)
            dba.ExecuteNonQuery()
            'how many vs allowed
            Dim exam As Exam = Session("Exam")
            'abort exam
        End If
        Session("studentExamID") = Guid.NewGuid().ToString()
        studentExamID = New Guid(Me.Session("studentExamID").ToString())
        divwelcome.Visible = False
        phExam.Visible = True
        pagetimer.Enabled = True
        lblExamTitle.Visible = True
        lbltimer.Visible = True
        lblTimeRemaining.Visible = True
        lblTimeRemaining.Visible = True
        lbltimestamp.Visible = True
        lblexamname.Visible = True
        divsubmit.Visible = True
        dba.CommandText = "select CurrentVersion from Exams WHERE examID= @examid"
        dba.AddParameter("@ExamID", examid)
        Dim str As String = dba.ExecuteScalar()
        If btnStart.Text = " Re-Take Exam " Then 'update attempts

            dba.CommandText = "Update StudentExams Set Attempts=@Attemps"
            dba.AddParameter("Attempts", 1)
            'add an attempt
        Else
            'Create StudentExam record
            dba.CommandText = "insert into studentExams (studentExamID,studentID,eventID,ExamID,examDate,ipAddress,version,Attempts) VALUES (@studentExamID,@studentID,@eventID,@ExamID,@examDate,@ipAddress,@version,@Attempts)"
            dba.AddParameter("@studentExamID", studentExamID)
            dba.AddParameter("@studentID", studentid)
            dba.AddParameter("@eventID", eventid)
            dba.AddParameter("@ExamID", examid)
            dba.AddParameter("@examDate", Now)
            dba.AddParameter("@ipAddress", lblip.Text)
            dba.AddParameter("@version", str)
            dba.AddParameter("@Attempts", 1)
            dba.ExecuteNonQuery()
        End If
    End Sub

    Private Sub pagetimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles pagetimer.Tick
        Dim str As String()
        Dim text As String = lbltimer.Text
        Dim strArrays As String() = text.Split(New Char() {":"c})
        Dim num As Integer = Integer.Parse(strArrays(2))
        Dim num1 As Integer = Integer.Parse(strArrays(1))
        Dim num2 As Integer = Integer.Parse(strArrays(0))
        lbltimer.Text = timercount.ToString()
        If (Not (num = 0 And num1 = 0 And num2 = 0)) Then
            num = num - 10
        End If
        If (num < 0) Then
            num = 50
            num1 = num1 - 1
        End If
        If (num1 < 0) Then
            num1 = 50
            num2 = num2 - 1
        End If
        Dim str1 As String = "0"
        If (num <> 0) Then
            Dim label As System.Web.UI.WebControls.Label = lbltimer
            str = New String() {num2, ":", num1, ":", num}
            label.Text = String.Concat(str)
        Else
            Dim label1 As System.Web.UI.WebControls.Label = lbltimer
            str = New String() {num2, ":", num1, ":", str1, num}
            label1.Text = String.Concat(str)
        End If
    End Sub
#Region "Render Exam"
    Protected Sub renderExam(ByVal examid As String)
        Dim exam As ExamOBJ = New ExamOBJ
        If Session("exam") Is Nothing Then
            exam = (New ExamDAL()).getExambyID(examid)
            Session("exam") = exam
        Else
            exam = Session("exam")
        End If

        lblExamTitle.Text = exam.ExamName
        Dim table As Table = New Table()
        table.Width = New Unit("20%")
        table.HorizontalAlign = HorizontalAlign.Center
        table.BorderStyle = BorderStyle.Solid
        table.BorderWidth = 1
        For Each question As Examquestion In exam.Questions
            renderquestion(table, question)
        Next
        phExam.Controls.Add(table)
        btnSubmit.CommandArgument = questionnumber.ToString()
    End Sub
    Private Function renderImageQuestion(ByVal q As Guid) As TableRow
        Dim guid As System.Guid = New System.Guid()
        Dim enumerator As IEnumerator = Nothing
        Dim enumerator1 As List(Of ExamImageQuestion).Enumerator = New List(Of ExamImageQuestion).Enumerator()
        'get exam image by questionid
        Dim dba As DBAccess = New DBAccess("Phazzer")
        dba.CommandText = "Select ImageID,image,Question,isAlpha,isCountAsOne FROM ExamImages where QuestionID=@questionID "
        dba.AddParameter("@questionID", q)
        Dim dt As DataTable = dba.ExecuteDataSet().Tables(0)
        Dim examImage As ExamImageObj = New ExamImageObj()
        examImage.QuestionID = q
        examImage.ImageID = dt.Rows(0)("ImageID")
        examImage.Image = DirectCast(dt.Rows(0)("image"), Byte())
        examImage.Question = dt.Rows(0)("Question")
        examImage.isAlpha = dt.Rows(0)("isAlpha")
        examImage.isCountAsOne = dt.Rows(0)("isCountAsOne")
        ' get the questions and answers for this image
        dba.CommandText = "SELECT QuestionID,Question,Answer FROM ExamImageQuestions Where ImageID=@imageid"
        dba.AddParameter("@imageid", examImage.ImageID)
        dt = New DataTable()
        dt = dba.ExecuteDataSet().Tables(0)
        Dim examImageQuestion As ExamImageQuestion = New ExamImageQuestion()
        Dim examImageQuestions As List(Of ExamImageQuestion) = New List(Of ExamImageQuestion)()
        For Each row As DataRow In dt.Rows
            examImageQuestion = New ExamImageQuestion()
            Dim examImageQuestion1 As ExamImageQuestion = examImageQuestion
            examImageQuestion1.QuestionID = row.Item("QuestionID")
            examImageQuestion.ImageID = examImage.ImageID
            examImageQuestion.Question = row.Item("Question")
            examImageQuestion.Answer = row.Item("Answer")
            examImageQuestions.Add(examImageQuestion)
        Next

        examImage.questionList = examImageQuestions

        Dim tableRow As System.Web.UI.WebControls.TableRow = New System.Web.UI.WebControls.TableRow()
        tableRow = New System.Web.UI.WebControls.TableRow()
        tableRow.BackColor = Drawing.Color.Honeydew
        Dim tableCell As System.Web.UI.WebControls.TableCell = New System.Web.UI.WebControls.TableCell()
        Dim table As System.Web.UI.WebControls.Table = New System.Web.UI.WebControls.Table()
        table = New System.Web.UI.WebControls.Table()
        Dim flag As Boolean = True
        For Each eiquestion As ExamImageQuestion In examImageQuestions
            Dim tableRow1 As TableRow = New TableRow()
            Dim tableCell1 As TableCell = New TableCell()
            tableCell1.BackColor = Drawing.Color.BlanchedAlmond
            Dim label As Label = New Label()
            questionnumber = questionnumber + 1
            label.Text = String.Concat("<b>", questionnumber.ToString(), ": &nbsp;</b>", eiquestion.Question)
            Dim questionID As System.Guid = eiquestion.QuestionID
            label.ID = String.Concat("lblQuestion-", questionID.ToString())
            tableCell1.Controls.Add(label)
            tableRow1.Cells.Add(tableCell1)
            Dim tableCell2 As TableCell = New TableCell
            tableCell2.BackColor = Drawing.Color.Honeydew
            Dim radTextBox As Telerik.Web.UI.RadTextBox = New Telerik.Web.UI.RadTextBox()
            questionID = eiquestion.QuestionID
            radTextBox.ID = String.Concat("imgAnswer : ", eiquestion.Answer.ToString())
            radTextBox.ValidationGroup = questionID.ToString
            radTextBox.Width = Unit.Pixel(45)
            radTextBox.MaxLength = IIf(examImage.isAlpha, 1, 2)
            tableCell2.CssClass = "PadImg"
            tableCell2.Controls.Add(radTextBox)
            tableRow1.Cells.Add(tableCell2)
            If (flag) Then
                Dim count As System.Web.UI.WebControls.TableCell = New System.Web.UI.WebControls.TableCell() With
                    {
                        .CssClass = "Center"
                    }
                Dim radBinaryImage As Telerik.Web.UI.RadBinaryImage = New Telerik.Web.UI.RadBinaryImage()
                radBinaryImage.ID = String.Concat(Me.questionnumber.ToString(), "imgMugShot")
                radBinaryImage.ResizeMode = Telerik.Web.UI.BinaryImageResizeMode.Fit
                radBinaryImage.DataValue = examImage.Image
                radBinaryImage.Width = Unit.Pixel(400)
                count.Controls.Add(radBinaryImage)
                count.RowSpan = examImage.questionList.Count
                tableRow1.Cells.Add(count)
                flag = False
            End If
            table.Rows.Add(tableRow1)
        Next
        tableCell.Controls.Add(table)
        tableRow.Cells.Add(tableCell)
        Return tableRow
    End Function
    Protected Sub renderquestion(ByRef mtbl As Table, ByVal q As Examquestion)
            Dim questionID As Guid = q.QuestionID
            Dim examAnswerList As List(Of ExamAnswer) = New List(Of ExamAnswer)
            '       Dim enumerator1 As List(Of ExamAnswer).Enumerator = New List(Of ExamAnswer).Enumerator()
            Dim table As System.Web.UI.WebControls.Table = New System.Web.UI.WebControls.Table()
            Dim tableRow As System.Web.UI.WebControls.TableRow = New System.Web.UI.WebControls.TableRow()
            Dim tableRow1 As System.Web.UI.WebControls.TableRow = New System.Web.UI.WebControls.TableRow()
            Dim tableRow2 As System.Web.UI.WebControls.TableRow = New System.Web.UI.WebControls.TableRow()
            Dim tableCell As System.Web.UI.WebControls.TableCell = New System.Web.UI.WebControls.TableCell()
            Dim tableCell1 As System.Web.UI.WebControls.TableCell = New System.Web.UI.WebControls.TableCell()
            Dim tableCell2 As System.Web.UI.WebControls.TableCell = New System.Web.UI.WebControls.TableCell()
            Dim tableCell3 As System.Web.UI.WebControls.TableCell = New System.Web.UI.WebControls.TableCell()
            Dim questionType As String = q.QuestionType
            If (questionType = "True/False") Then
                Dim qlabel As Label = New Label()
                'increment question number
                questionnumber = questionnumber + 1
                qlabel.Text = String.Concat("<b>", questionnumber.ToString(), ": &nbsp;</b>", q.Question)
                questionID = q.QuestionID
                qlabel.ID = String.Concat("lblQuestion-", questionID.ToString())
                tableCell1 = New TableCell()
                tableCell1.CssClass = "Padding"
                tableCell1.Controls.Add(qlabel)
                tableRow = New TableRow() 'question row
                tableRow.BackColor = Drawing.Color.BlanchedAlmond
                tableRow.CssClass = "margtop"
                tableRow.Cells.Add(tableCell1) 'add question cell to question row
                mtbl.Rows.Add(tableRow) 'add question row to mtbl
                tableRow1 = New TableRow() 'answer row
                tableRow1.BackColor = Drawing.Color.Honeydew
                tableRow1.CssClass = "margbott"
                tableCell2 = New TableCell()
                tableCell2.CssClass = "Padding"
                table = New System.Web.UI.WebControls.Table() With
                    {
                        .Width = New Unit("275")
                    }
                tableRow2 = New System.Web.UI.WebControls.TableRow()
                Dim str As String = "`"
                For Each ans As ExamAnswer In q.answers
                    str = Strings.Chr(Strings.Asc(str) + 1)
                    Dim RadButton As Telerik.Web.UI.RadButton = New Telerik.Web.UI.RadButton()
                    RadButton.AutoPostBack = False
                    RadButton.ValidationGroup = q.QuestionID.ToString()
                    RadButton.Text = ans.Answer
                    questionID = ans.QuestionID
                    RadButton.Value = String.Concat(str, "-", ans.AnswerID.ToString())
                    RadButton.GroupName = q.QuestionID.ToString()

                    questionID = ans.QuestionID

                    RadButton.ID = String.Concat("btnAnswer-", str, "-", questionID.ToString())

                    RadButton.ToggleType = Telerik.Web.UI.ButtonToggleType.Radio
                    Dim radButtonToggleState As Telerik.Web.UI.RadButtonToggleState = New Telerik.Web.UI.RadButtonToggleState()
                    radButtonToggleState.Text = ans.Answer
                    radButtonToggleState.PrimaryIconCssClass = "rbToggleRadio"
                    RadButton.ToggleStates.Add(radButtonToggleState)
                    tableCell3 = New TableCell()
                    tableCell3.CssClass = "Padding"
                    tableCell3.Controls.Add(RadButton)
                    tableRow2.Cells.Add(tableCell3)
                Next
                table.Rows.Add(tableRow2)
            tableCell2.Controls.Add(table)
            tableRow1.Cells.Add(tableCell2)
                mtbl.Rows.Add(tableRow1)
            ElseIf questionType = "Multiple Choice" Then
                Dim label1 As Label = New Label()
                'increment question count
                questionnumber = questionnumber + 1
                'stuff questionnumber and question into label
                label1.Text = String.Concat("<b>", questionnumber.ToString(), ": &nbsp;</b>", q.Question)
                questionID = q.QuestionID
                label1.ID = String.Concat("lblQuestion-", questionID.ToString())
                tableCell1 = New TableCell() 'question cell
                tableCell1.CssClass = "Padding"
                tableCell1.Controls.Add(label1) 'add questionlabel to cell
                tableRow = New TableRow() 'question row
                tableRow.BackColor = Drawing.Color.BlanchedAlmond
                tableRow.CssClass = "margtop"
                tableRow.Cells.Add(tableCell1) 'and question cell to question row
                mtbl.Rows.Add(tableRow) 'add question row to mtbl
                tableRow1 = New TableRow() 'answer row
                tableRow1.BackColor = Drawing.Color.Honeydew
                tableCell2 = New System.Web.UI.WebControls.TableCell() 'answer cell
                tableCell2.CssClass = "Padding"
                table = New System.Web.UI.WebControls.Table()
                Dim str1 As String = "`"

                For Each examAnswer As ExamAnswer In q.answers

                    str1 = Strings.Chr(Strings.Asc(str1) + 1)
                    label1 = New Label()
                    label1.Text = String.Concat(str1.ToUpper(), ": ")
                    Dim radButton1 As Telerik.Web.UI.RadButton = New Telerik.Web.UI.RadButton()
                    radButton1.AutoPostBack = False
                    radButton1.ValidationGroup = q.QuestionID.ToString()
                    radButton1.GroupName = q.QuestionID.ToString()
                    questionID = examAnswer.AnswerID
                    radButton1.Value = String.Concat(str1, "-", questionID.ToString())
                    questionID = examAnswer.QuestionID
                    radButton1.ID = String.Concat("btnAnswer-", str1, "-", questionID.ToString())
                    radButton1.Text = examAnswer.Answer
                    radButton1.ToggleType = Telerik.Web.UI.ButtonToggleType.Radio
                    Dim radButtonToggleState1 As Telerik.Web.UI.RadButtonToggleState = New Telerik.Web.UI.RadButtonToggleState()
                    radButtonToggleState1.Text = examAnswer.Answer
                    radButtonToggleState1.PrimaryIconCssClass = "rbToggleRadio"
                    radButton1.ToggleStates.Add(radButtonToggleState1)
                    tableCell3 = New TableCell()
                    tableCell3.CssClass = "Padding"
                    tableCell3.VerticalAlign = VerticalAlign.Bottom
                    tableCell3.Controls.Add(label1)
                    tableCell3.Controls.Add(radButton1)
                    tableRow2 = New System.Web.UI.WebControls.TableRow()
                    tableRow2.Cells.Add(tableCell3)
                    table.Rows.Add(tableRow2)
                Next
                tableCell2.Controls.Add(table)
                tableRow1.Cells.Add(tableCell2)
                mtbl.Rows.Add(tableRow1)
            ElseIf questionType = "Image" Then
                Dim label2 As System.Web.UI.WebControls.Label = New System.Web.UI.WebControls.Label() With
                    {
                        .Text = String.Concat("<b>", q.Question, "</b> &nbsp CAPS Lock Recommended - Use Tab key to advance")
                    }
                questionID = q.QuestionID
                label2.ID = String.Concat("lblQuestion-", questionID.ToString())
                tableCell1 = New System.Web.UI.WebControls.TableCell() With
                    {
                        .CssClass = "Padding"
                    }
                tableCell1.Controls.Add(label2)
                tableRow = New System.Web.UI.WebControls.TableRow() With
                    {
                        .BackColor = Drawing.Color.BlanchedAlmond,
                        .CssClass = "margtop"
                    }
                tableRow.Cells.Add(tableCell1)
                mtbl.Rows.Add(tableRow)
                mtbl.Rows.Add(Me.renderImageQuestion(q.QuestionID))
            End If
        End Sub
#End Region
#Region "Grade Test"
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim sealist = New List(Of StudentExamAnswer)
        Dim studentExamID As Guid
        Dim exam As ExamOBJ = Session("exam")
        Dim radButton As Telerik.Web.UI.RadButton = DirectCast(sender, Telerik.Web.UI.RadButton)

        Dim empty As String = String.Empty
        Dim numQuestions As Integer = radButton.CommandArgument
        sealist = getanswers(phExam.Controls) 'fill sealist with page controls
        'TODO if sealist.count < numQuestions, there are unanswered questions ....  what to do.
        Dim numCorrect As Integer = 0
        For Each sea As StudentExamAnswer In sealist
            Dim dba As DBAccess = New DBAccess("Phazzer")
            dba.CommandText = "INSERT INTO StudentExamAnswers (studentExamID,answerid,QuestionID,ImageAnswer) VALUES(@studentExamID,@answerid,@QuestionID,@ImageAnswer)"
            dba.AddParameter("@studentExamID", sea.StudentExamID)
            dba.AddParameter("@answerid", sea.AnswerID)
            dba.AddParameter("@QuestionID", sea.QuestionID)
            dba.AddParameter("@ImageAnswer", IIf(sea.ImageAnswer Is Nothing, "", sea.ImageAnswer))
            Try
                dba.ExecuteNonQuery()
            Catch exception As System.Exception
                Dim message As String = exception.Message
            End Try
            ' answer recorded, now check answer
            If (sea.AnswerID <> Utilities.ZeroGuid()) Then 'this is a button
                'check button answer
                dba.CommandText = "SELECT isCorrectAnswer FROM ExamAnswers WHERE AnswerID=@AnswerID"
                dba.AddParameter("@AnswerID", sea.AnswerID)
                If dba.ExecuteScalar() Then
                    numCorrect = numCorrect + 1
                Else
                    Dim w As String = "wrong"
                End If
            Else
                'check image answer
                dba.CommandText = "Select Answer FROM ExamImageQuestions WHERE QuestionID = @QuestionID"
                dba.AddParameter("@QuestionID", sea.QuestionID)
                If dba.ExecuteScalar().ToUpper() = sea.ImageAnswer.ToUpper() Then
                    numCorrect = numCorrect + 1
                Else
                    Dim w As String = "wrong"
                End If
            End If
        Next
        'calculate grade
        Dim num1 As Decimal = New Decimal(CDbl(numCorrect) / CDbl(numQuestions))
        Dim str As String = Strings.FormatPercent(num1, 0, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault)
        If (Convert.ToDouble(num1) <= 0.8) Then 'they failed
            studentExamID = sealist(0).StudentExamID
            Dim str1 As String = studentExamID.ToString()
            'store failing grade
            Dim dba As DBAccess = New DBAccess("Phazzer") With
                {
                    .CommandText = "Update studentExams set Pass=0 where studentExamID=@studentExamID"
                }
            dba.AddParameter("@studentExamID", str1)
            dba.ExecuteNonQuery()
            'tell user they failed
            lblPassFail.Text = "Oooophf!"
            lblcorrectanswers.Text = String.Concat("You missed ", CDbl(numQuestions) - numCorrect.ToString(), " out of ", numQuestions.ToString())
            lblscore.Text = String.Concat("You failed with a score of only ", str)
            'generate survymonkey link
            lblfinish.Text = "Please contact your instructor to re-take the exam"
        Else 'they passed
            'tell user they passed
            lblPassFail.Text = "Congratulations,&nbsp;you&nbsp;passed!<br/>"
            lblcorrectanswers.Text = String.Concat("You only missed&nbsp;&nbsp;", CDbl(numQuestions - numCorrect.ToString) & "&nbsp;out of&nbsp;" & numQuestions.ToString())
            lblscore.Text = String.Concat("You Passed with a score of ", str)

            studentExamID = sealist(0).StudentExamID
            Dim str2 As String = studentExamID.ToString()
            'store passing grade
            Dim dba As DBAccess = New DBAccess("Phazzer")
            dba.CommandText = "Update studentExams set Pass=1 where studentExamID=@studentExamID"
            dba.AddParameter("@studentExamID", studentExamID)
            dba.ExecuteNonQuery()

            dba.CommandText = "SELECT studentID from studentExams where studentExamID=@studentExamID"
            dba.AddParameter("@studentExamID", studentExamID)
            Dim studentID As String = dba.ExecuteScalar().ToString()
            Dim userDAL As UserDAL = New UserDAL()
            Dim gstudentID As Guid = New Guid(studentID)
            Dim userByID As ssUser = userDAL.GetUserByID(gstudentID)

            lblfinish.Text = "<strong><a href ='ShowCert.aspx?sxamid="
            lblfinish.Text &= studentExamID.ToString
            lblfinish.Text &= "&view=yes'"
            lblfinish.Text &= "><br/> Click Here</a></strong> To View your Certification"
            lblfinish.Text &= "<br/> or choose your delivery method below <br/>"

            Dim downloadLink As String = "<strong><a href ='http://exams.PhaZZerTraining.com/Exams/ShowCert.aspx?sxamid=" & studentExamID.ToString & "&dld=yes'" & "> Download</a></strong> To Download your Certification or "
            Dim emailLink As String = "<strong><a href ='http://exams.PhaZZerTraining.com/Exams/ShowCert.aspx?sxamid=" & studentExamID.ToString & "&email=yes'" & "> eMail</a></strong> To recieve your Certficate via eMail"
            lblfinish.Text &= downloadLink & " /  "
            lblfinish.Text &= emailLink

            Dim phazzerMail As RdMailer = New RdMailer()
            phazzerMail.[To] = userByID.eMail
            phazzerMail.Subject = "Your " & Session("ExamTitle") & " Certificate"
            'todo put greeting in email
            phazzerMail.Body = lblfinish.Text

            RdMailer.SendMail(phazzerMail)
        End If ' did they pass
        divscore.Visible = True
        divsubmit.Visible = False
        phExam.Visible = False
    End Sub
#End Region 'Grade Test"
    Public Function getanswers(ByVal controls As ControlCollection) As List(Of StudentExamAnswer)
        'should arrive with one control ... a table
        Dim studentExamAnswer As StudentExamAnswer = New StudentExamAnswer()
        Dim radButton As Telerik.Web.UI.RadButton = New Telerik.Web.UI.RadButton()
        Dim radTextBox As Telerik.Web.UI.RadTextBox = New Telerik.Web.UI.RadTextBox()
        Dim empty As String = String.Empty
        Dim num As Integer = Session("Exam").questions.count
        '        Conversions.ToInteger(Me.btnSubmit.CommandArgument)
        '        Dim dba As DBAccess = New DBAccess("Phazzer")
        For Each control As Control In controls
            If (TypeOf control Is Telerik.Web.UI.RadButton) Then
                radButton = DirectCast(control, Telerik.Web.UI.RadButton)
                If Left(radButton.ID, 9) = "btnAnswer" Then
                    If (radButton.Checked) Then
                        '                        num = num + 1 'useless? reference Session("Exam").questions.count
                        studentExamAnswer = New StudentExamAnswer()
                        studentExamAnswer.QuestionID = New Guid(radButton.ValidationGroup)
                        studentExamAnswer.StudentExamID = New Guid(Session("StudentExamID").ToString)
                        studentExamAnswer.AnswerID = New Guid(Right(radButton.Value.ToString, 36))
                        sealist.Add(studentExamAnswer)
                    End If
                End If
            ElseIf (TypeOf control Is Telerik.Web.UI.RadTextBox) Then
                radTextBox = DirectCast(control, Telerik.Web.UI.RadTextBox)
                If Left(radTextBox.ID, 9) = "imgAnswer" Then
                    '                    If radTextBox.Text > "" Then ' did they put an answer in?
                    studentExamAnswer = New StudentExamAnswer()
                    studentExamAnswer.ImageAnswer = Request.Form(radTextBox.ID)
                    If studentExamAnswer.ImageAnswer > "" Then

                        '                        studentExamAnswer.ImageAnswer = radTextBox.Text
                        studentExamAnswer.StudentExamID = New System.Guid(Session("studentExamID").ToString)
                        studentExamAnswer.AnswerID = Utilities.ZeroGuid()
                        studentExamAnswer.QuestionID = New Guid(radTextBox.ValidationGroup)
                        sealist.Add(studentExamAnswer)
                    End If
                End If
            End If
            getanswers(control.Controls)
        Next
        Return sealist

    End Function

    'Public Sub getquestions(ByVal controls As ControlCollection)
    '    Dim enumerator As IEnumerator = Nothing
    '    Dim label As System.Web.UI.WebControls.Label = New System.Web.UI.WebControls.Label()
    '    Dim num As Integer = 0
    '    Try
    '        enumerator = controls.GetEnumerator()
    '        While enumerator.MoveNext()
    '            Dim current As Control = DirectCast(enumerator.Current, Control)
    '            If TypeOf current Is Label AndAlso Left(current.ID, 11) = "lblQuestion" Then
    '                num = num + 1
    '            End If
    '            If (current.Controls.Count <= 0) Then
    '                Continue While
    '            End If
    '            getquestions(current.Controls)
    '        End While
    '    Finally
    '    End Try
    '    If btnSubmit.CommandArgument > num Then

    '    End If
    'End Sub
End Class