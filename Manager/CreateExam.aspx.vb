Imports Telerik.Web.UI

Public Class CreateExam
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
            LoadDepartment()

            Dim userDAL As userDAL = New userDAL()
            Dim userByName As ssUser = userDAL.GetUserByName(Me.User.Identity.Name)
            userByName.userID.ToString()
            lblcreatedBy.Text = String.Concat(userByName.FirstName, " ", userByName.LastName)
            lblversion.Text = "pending ..."
            lblExamID.Text = "pending ..."
            RadGrid1.Visible = False
            RadGrid2.Visible = False
        End If
        lblsaveresponse.Visible = False
        Session("QuestionTypeID") = "0e2a3d55-25e0-4e44-8115-024696670041"
    End Sub



    Private Sub BtnexaminfoSubmit_Click(sender As Object, e As EventArgs) Handles btnexaminfoSubmit.Click
        Dim examDAL As ExamDAL = New ExamDAL()
        Dim examOBJ As ExamOBJ = New ExamOBJ With {
            .ExamID = Guid.NewGuid()
        }
        Dim examID As Guid = examOBJ.ExamID
        lblExamID.Text = examID.ToString()
        examOBJ.ExamName = txtExamName.Text.Trim()
        examID = New Guid(cbDepartment.SelectedValue.ToString())
        examOBJ.DepartmentID = examID
        If cbCourseList.SelectedValue > "" Then
            examID = New Guid(cbCourseList.SelectedValue.ToString())
            examOBJ.CoursePreReq = examID
        Else
            examOBJ.CoursePreReq = Utilities.zeroGuid()
        End If
        Dim examPrice As Nullable(Of Double) = numExamPrice.Value
        examOBJ.Price = IIf(Not examPrice.HasValue, 0, numExamPrice.Value)
        Dim cuserID As Guid = New Guid(Membership.GetUser().ProviderUserKey.ToString())
        examOBJ.CreatedBy = cuserID
        examOBJ.TimeStamp = Date.Now
        examOBJ.CurrentVersion = Format(examOBJ.TimeStamp, "yyMddhhmmss")
        RadGrid1.Visible = True
        If examDAL.insertExam(examOBJ) = "" Then
            lblsaveresponse.Text = "Exam Saved"
        End If
        lblversion.Text = examOBJ.CurrentVersion
        RadGrid1.Rebind()
        Dim userDAL As userDAL = New userDAL()
        Dim userByName As ssUser = userDAL.GetUserByName(User.Identity.Name)
        lblExamID.Text = String.Concat("ExamID: ", examID.ToString())
        lblversion1.Text = String.Concat("Version #: ", examOBJ.CurrentVersion)
        lblexamName.Text = String.Concat("(NEW) -", txtExamName.Text)
        lblcreatedBy1.Text = String.Concat("CreatedBy: ", userByName.FirstName, " ", userByName.LastName)
        tblexaminfo.Visible = False
        divtitle.Visible = False
    End Sub
    Private Sub CbDepartment_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbDepartment.SelectedIndexChanged
        Me.cbCourseList.Items.Clear()
        Me.cbCourseList.Text = String.Empty
        Me.LoadCourses()
    End Sub
    Protected Sub LoadCourses()
        Dim dba As DBAccess = New DBAccess("Phazzer") With {
            .CommandText = "Select CourseID, CourseName from Course Where DepartmentID = @DepartmentID order by CourseName asc"
        }
        dba.AddParameter("@DepartmentID", Me.cbDepartment.SelectedValue)
        Dim dt As DataTable = dba.ExecuteDataSet().Tables(0)
        Dim radComboBox As Telerik.Web.UI.RadComboBox = DirectCast(Me.FindControl("cbCourseList"), Telerik.Web.UI.RadComboBox)
        radComboBox.DataSource = dt
        radComboBox.DataTextField = "CourseName"
        radComboBox.DataValueField = "CourseID"
        radComboBox.DataBind()
    End Sub
    Protected Sub LoadDepartment()
        Dim dba As DBAccess = New DBAccess("Phazzer") With {
            .CommandText = "Select DepartmentID, DepartmentName from Department Where DepartmentName = 'PhaZZer Training' order by DepartmentName asc"
        }
        Dim dt As DataTable = dba.ExecuteDataSet().Tables(0)
        Me.cbDepartment.DataSource = dt
        Me.cbDepartment.DataTextField = "DepartmentName"
        Me.cbDepartment.DataValueField = "DepartmentID"
        Me.cbDepartment.DataBind()
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim str As String
        Dim dBAccess As DBAccess = New DBAccess("Phazzer")

        str = lblExamID.Text

        dBAccess.CommandText = "SELECT ExamQuestions.Question, ExamQuestions.QuestionID, ExamQuestions.QuestionTypeID, ExamQuestions.ExamID, ExamQuestionTypes.QuestionType, ExamQuestions.TimeStamp FROM ExamQuestions INNER JOIN ExamQuestionTypes ON ExamQuestions.QuestionTypeID = ExamQuestionTypes.QuestionTypeID WHERE ExamID = @ExamID ORDER BY ExamQuestions.TimeStamp DESC"
        dBAccess.AddParameter("@ExamID", str)
        Dim item As DataTable = dBAccess.ExecuteDataSet().Tables(0)
        Me.RadGrid1.DataSource = item
    End Sub

    Private Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim enumerator As IEnumerator = Nothing
        Try
            enumerator = Me.RadGrid1.MasterTableView.Items.GetEnumerator()
            While enumerator.MoveNext()
                Dim current As GridDataItem = DirectCast(enumerator.Current, GridDataItem)
                If (Me.Session("QuestionID") Is Nothing OrElse current.GetDataKeyValue("QuestionID").ToString() = Me.Session("QuestionID").ToString()) Then
                    Continue While
                End If
                current.Selected = True
                Me.RadGrid2.Rebind()
                Me.RadGrid2.Visible = True
            End While

        Finally
        End Try
    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If (e.Item.IsInEditMode) Then
            Dim item As RadComboBox = DirectCast((DirectCast(e.Item, GridEditableItem)).FindControl("cbQuestionTypes"), RadComboBox)
            If (item IsNot Nothing) Then
                Dim dba As DBAccess = New DBAccess("Phazzer") With {
                    .CommandText = "SELECT [QuestionTypeID], [QuestionType] FROM [ExamQuestionTypes] ORDER BY [QuestionType] DESC"
                }
                item.DataSource = dba.ExecuteDataSet().Tables(0)
                item.DataTextField = "QuestionType"
                item.DataValueField = "QuestionTypeID"
                item.DataBind()
                If (TypeOf e.Item Is IGridInsertItem) Then
                    item.SelectedValue = "0e2a3d55-25e0-4e44-8115-024696670041"
                Else
                    Dim radComboBoxItem As Telerik.Web.UI.RadComboBoxItem = New Telerik.Web.UI.RadComboBoxItem With {
                        .Value = (DirectCast(e.Item.DataItem, DataRowView))("QuestionTypeID").ToString()
                    }
                    item.SelectedValue = radComboBoxItem.Value
                End If
            End If
        End If
    End Sub

    Private Sub RadGrid1_InsertCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.InsertCommand
        Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        Dim examDAL As ExamDAL = New ExamDAL()
        Dim examquestion As Examquestion = New Examquestion With {
            .QuestionID = System.Guid.NewGuid()
        }
        Dim guid As System.Guid = New System.Guid(Strings.Right(Me.lblExamid1.Text, 36))
        examquestion.ExamID = guid
        examquestion.Version = examDAL.getExamVersionbyID(examquestion.ExamID)
        Dim userDAL As userDAL = New userDAL()
        Dim userByName As mUser = userDAL.GetUserByName(Me.User.Identity.Name)
        guid = userByName.userID
        Dim guid1 As System.Guid = New System.Guid(guid.ToString())
        examquestion.CreatedBy = guid1
        examquestion.TimeStamp = DateTime.Now
        Dim radEditor As Telerik.Web.UI.RadEditor = DirectCast(item.FindControl("txtQuestion"), Telerik.Web.UI.RadEditor)
        examquestion.Question = radEditor.Content.Trim()
        Dim radComboBox As Telerik.Web.UI.RadComboBox = DirectCast(item.FindControl("cbQuestionTypes"), Telerik.Web.UI.RadComboBox)
        guid1 = New System.Guid(radComboBox.SelectedValue.ToString())
        examquestion.QuestionTypeID = guid1
        examDAL.insertQuestion(examquestion)
        If radComboBox.Text.Trim() = "True/False" Then
            Dim examAnswer As ExamAnswer = New ExamAnswer With {
                .Answer = "True",
                .AnswerID = System.Guid.NewGuid(),
                .QuestionID = examquestion.QuestionID,
                .isCorrectAnswer = False
            }
            guid1 = userByName.userID
            guid = New System.Guid(guid1.ToString())
            examAnswer.createdBy = guid
            examAnswer.TimeStamp = DateTime.Now
            examDAL.insertAnswer(examAnswer)
            examAnswer.Answer = "False"
            examAnswer.AnswerID = System.Guid.NewGuid()
            examAnswer.QuestionID = examquestion.QuestionID
            examAnswer.isCorrectAnswer = False
            guid1 = userByName.userID
            guid = New System.Guid(guid1.ToString())
            examAnswer.createdBy = guid
            examAnswer.TimeStamp = DateTime.Now
            examDAL.insertAnswer(examAnswer)
        End If
        Me.RadGrid2.Visible = False
        Me.RadGrid1.Rebind()
        Me.RadGrid1.MasterTableView.ClearEditItems()
    End Sub

    Private Sub RadGrid1_DeleteCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.DeleteCommand
        Dim dataKeyValue As Guid = DirectCast((DirectCast(e.Item, GridDataItem)).GetDataKeyValue("QuestionID"), Guid)
        Dim str As String = dataKeyValue.ToString()
        If (str IsNot Nothing) Then
            Dim dba As DBAccess = New DBAccess("Phazzer") With {
                .CommandText = "Delete from ExamQuestions WHERE QuestionID=@QuestionID"
            }
            dba.AddParameter("@QuestionID", str)
            dba.ExecuteNonQuery()
            Me.RadGrid1.Rebind()
            dba.CommandText = "Delete from ExamAnswers WHERE QuestionID=@QuestionID"
            dba.AddParameter("@QuestionID", str)
            dba.ExecuteNonQuery()
            Me.RadGrid2.Rebind()
        End If
    End Sub

    Private Sub RadGrid1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadGrid1.SelectedIndexChanged
        Me.Session("QuestionID") = Me.RadGrid1.SelectedValue
        If (Me.RadGrid1.MasterTableView.IsItemInserted) Then
            Me.RadGrid1.MasterTableView.IsItemInserted = False
            Me.RadGrid1.Rebind()
        End If
        If (Me.RadGrid1.SelectedValue Is Nothing) Then
            Me.RadGrid2.Visible = False
        Else
            Me.RadGrid2.MasterTableView.IsItemInserted = False
            Me.RadGrid2.Visible = True
            Me.RadGrid2.Rebind()
        End If
    End Sub

    Private Sub RadGrid1_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.UpdateCommand
        Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        Dim examquestion As Examquestion = New Examquestion()
        Dim radEditor As Telerik.Web.UI.RadEditor = DirectCast(e.Item.FindControl("txtQuestion"), Telerik.Web.UI.RadEditor)
        examquestion.Question = radEditor.Content
        Dim radComboBox As Telerik.Web.UI.RadComboBox = DirectCast(e.Item.FindControl("cbQuestionTypes"), Telerik.Web.UI.RadComboBox)
        Dim str As String = item.GetDataKeyValue("QuestionID").ToString()
        Dim guid As System.Guid = New System.Guid(str)
        examquestion.QuestionID = guid
        guid = New System.Guid(radComboBox.SelectedValue.ToString())
        examquestion.QuestionTypeID = guid
        Dim dba As DBAccess = New DBAccess("Phazzer") With {
            .CommandText = "UPDATE ExamQuestions SET Question = @Question, QuestionTypeID = @QuestionTypeID where QuestionID = @QuestionID"
        }
        dba.AddParameter("@Question", examquestion.Question)
        dba.AddParameter("@QuestionID", examquestion.QuestionID)
        dba.AddParameter("@QuestionTypeID", examquestion.QuestionTypeID)
        dba.ExecuteNonQuery()
        Me.RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim guid As System.Guid = New System.Guid()
        Dim guid1 As System.Guid
        Dim selectedValue As Object = Me.RadGrid1.SelectedValue
        If (selectedValue IsNot Nothing) Then
            guid1 = DirectCast(selectedValue, System.Guid)
        Else
            guid1 = guid
        End If
        Dim guid2 As System.Guid = guid1
        Dim dba As DBAccess = New DBAccess("Phazzer") With {
            .CommandText = "Select * from ExamAnswers where questionid=@questionid"
        }
        dba.AddParameter("@questionid", guid2)
        Dim item As DataTable = dba.ExecuteDataSet().Tables(0)
        Me.RadGrid2.DataSource = item
    End Sub


    Private Sub RadGrid2_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If (Not e.Item.IsInEditMode) Then
            Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
            Dim checkBox As System.Web.UI.WebControls.CheckBox = DirectCast(item.FindControl("chkEditIsCorrect"), System.Web.UI.WebControls.CheckBox)
            Dim label As System.Web.UI.WebControls.Label = DirectCast(item.FindControl("lblCorrect"), System.Web.UI.WebControls.Label)
            If (checkBox.Checked) Then
                label.Text = "Correct"
            End If
        End If
    End Sub

    Private Sub RadGrid2_InsertCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid2.InsertCommand
        Dim examdal As New ExamDAL
        Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        If (TypeOf item Is IGridInsertItem) Then
            Dim parentItem As GridDataItem = e.Item.OwnerTableView.ParentItem
            Dim str As String = Me.RadGrid1.SelectedValue.ToString()
            Dim userDAL As userDAL = New userDAL()
            Dim examAnswer As ExamAnswer = New ExamAnswer With {
                .AnswerID = System.Guid.NewGuid()
            }
            Dim guid As System.Guid = New System.Guid(str)
            examAnswer.QuestionID = guid
            Dim radTextBox As Telerik.Web.UI.RadTextBox = DirectCast(item.FindControl("txtAnswer"), Telerik.Web.UI.RadTextBox)
            examAnswer.Answer = radTextBox.Text.Trim()
            examAnswer.isCorrectAnswer = (DirectCast(item.FindControl("chkEditIsCorrect"), CheckBox)).Checked
            Dim userByName As mUser = userDAL.GetUserByName(Me.User.Identity.Name)
            guid = userByName.userID
            examAnswer.createdBy = New System.Guid(guid.ToString())
            examAnswer.TimeStamp = DateTime.Now
            examdal.insertAnswer(examAnswer)
            Me.RadGrid2.Rebind()
        End If
    End Sub

    Private Sub RadGrid2_UpdateCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid2.UpdateCommand
        Dim guid As System.Guid = New System.Guid()
        Dim guid1 As System.Guid
        Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        Dim examAnswer As ExamAnswer = New ExamAnswer()
        Dim radTextBox As Telerik.Web.UI.RadTextBox = DirectCast(item.FindControl("txtAnswer"), Telerik.Web.UI.RadTextBox)
        Dim checkBox As System.Web.UI.WebControls.CheckBox = DirectCast(item.FindControl("chkEditIsCorrect"), System.Web.UI.WebControls.CheckBox)
        examAnswer.Answer = radTextBox.Text
        examAnswer.isCorrectAnswer = checkBox.Checked
        Dim examAnswer1 As ExamAnswer = examAnswer
        Dim dataKeyValue As Object = item.GetDataKeyValue("AnswerID")
        If (dataKeyValue IsNot Nothing) Then
            guid1 = DirectCast(dataKeyValue, System.Guid)
        Else
            guid1 = guid
        End If
        examAnswer1.AnswerID = guid1
        Dim dba As DBAccess = New DBAccess("Phazzer") With {
            .CommandText = "Update ExamAnswers set Answer=@Answer, isCorrectAnswer=@isCorrectAnswer where AnswerID=@AnswerID"
        }
        dba.AddParameter("@Answer", examAnswer.Answer)
        dba.AddParameter("@isCorrectAnswer", examAnswer.isCorrectAnswer)
        dba.AddParameter("@AnswerID", examAnswer.AnswerID)
        Try
            dba.ExecuteNonQuery()
        Catch exception As System.Exception
            Dim message As String = exception.Message
        End Try
        Me.RadGrid2.Rebind()
    End Sub

End Class