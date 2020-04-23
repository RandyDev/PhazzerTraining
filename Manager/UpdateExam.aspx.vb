Imports Telerik.Web.UI

Public Class UpdateExam
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Me.IsPostBack) Then
            Me.LoadDepartments()
            Me.RadGrid1.Visible = False
            Me.RadGrid2.Visible = False
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

    Private Sub cbCourseList_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbCourseList.SelectedIndexChanged
        Me.lbtnSavePreReq.Visible = True
        Me.lblSaved.Visible = False
    End Sub

    Private Sub cbDepartment_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbDepartment.SelectedIndexChanged
        cbCourseList.Items.Clear()
        cbCourseList.Text = String.Empty
        LoadCourses()
        cbCourseList.SelectedIndex = -1
        cbCourseList.Enabled = False
        loadcbExams()
    End Sub

    Private Sub cbExamlist_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbExamlist.SelectedIndexChanged
        Dim dba As New DBAccess("Phazzer")
        dba.CommandText = "SELECT Course.CourseName + ' : ' + Course.CourseSKU AS Course " &
            "From Exams INNER Join " &
            "Course On Exams.CoursePreReq = Course.CourseID " &
            "Where (Exams.ExamID = @examID)"
        dba.AddParameter("@examid", cbExamlist.SelectedValue)
        Me.cbCourseList.Enabled = True
        Me.cbCourseList.SelectedValue = dba.ExecuteScalar().ToString()
        Me.RadGrid1.Visible = True
        Me.RadGrid1.Rebind()
    End Sub

    Private Sub lbtnSavePreReq_Click(sender As Object, e As EventArgs) Handles lbtnSavePreReq.Click
        Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
{
    .CommandText = "Update Exams Set CoursePreReq=@CoursePreReq where examid=@examid"
}
        dBAccess.AddParameter("@CoursePreReq", Me.cbCourseList.SelectedValue.ToString())
        dBAccess.AddParameter("@examid", Me.cbExamlist.SelectedValue.ToString())
        dBAccess.ExecuteNonQuery()
        Me.lblSaved.Visible = True
        Me.lbtnSavePreReq.Visible = False
    End Sub

    Protected Sub loadcbExams()
        cbExamlist.Items.Clear()
        Dim i As Integer = Nothing
        Dim deptID As Guid = New Guid(cbDepartment.SelectedValue)
        Dim examList As List(Of ExamOBJ) = New List(Of ExamOBJ)
        Dim vexam As ExamOBJ = New ExamOBJ
        Dim dba As DBAccess = New DBAccess("Phazzer")
        'dba.CommandText = "Select distinct(ExamName) from Exams Where DepartmentID = @DepartmentID"
        'dba.AddParameter("@DepartmentID", deptID)
        'Dim dtexams As DataTable = dba.ExecuteDataSet().Tables(0)
        examList = New List(Of ExamOBJ)()
        'Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
        dba.CommandText = "SELECT ExamID, ExamName, CurrentVersion from Exams where DepartmentID=@DepartmentID order by currentversion desc"
        dba.AddParameter("@DepartmentID", deptID)
        Dim dtexams As DataTable = dba.ExecuteDataSet().Tables(0)
        cbExamlist.DataSource = dtexams
        cbExamlist.DataTextField = "ExamName"
        cbExamlist.DataValueField = "ExamID"
        cbExamlist.DataBind()
    End Sub

    Protected Sub LoadCourses()
        Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
            {
                .CommandText = "Select CourseID, CourseName from Course Where DepartmentID = @DepartmentID order by CourseName asc"
            }
        dBAccess.AddParameter("@DepartmentID", Me.cbDepartment.SelectedValue)
        Dim item As DataTable = dBAccess.ExecuteDataSet().Tables(0)
        Me.cbCourseList.DataSource = item
        Me.cbCourseList.DataTextField = "CourseName"
        Me.cbCourseList.DataValueField = "CourseID"
        Me.cbCourseList.DataBind()
    End Sub

    Protected Sub LoadDepartments()
        Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
            {
                .CommandText = "Select DepartmentName, DepartmentID from Department where DepartmentName= 'PhaZZer Training' and isInActive=0 Order by DepartmentName"
            }
        Dim item As DataTable = dBAccess.ExecuteDataSet().Tables(0)
        Me.cbDepartment.DataSource = item
        Me.cbDepartment.DataTextField = "DepartmentName"
        Me.cbDepartment.DataValueField = "DepartmentID"
        Me.cbDepartment.DataBind()
    End Sub

    Private Sub RadGrid1_DeleteCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.DeleteCommand

        Dim dataKeyValue As Guid = DirectCast((DirectCast(e.Item, GridDataItem)).GetDataKeyValue("QuestionID"), Guid)
        Dim str As String = dataKeyValue.ToString()
        If (str IsNot Nothing) Then
            Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
                {
                    .CommandText = "Delete from ExamQuestions WHERE QuestionID=@QuestionID"
                }
            dBAccess.AddParameter("@QuestionID", str)
            dBAccess.ExecuteNonQuery()
            Me.RadGrid1.Rebind()
            dBAccess.CommandText = "Delete from ExamAnswers WHERE QuestionID=@QuestionID"
            dBAccess.AddParameter("@QuestionID", str)
            dBAccess.ExecuteNonQuery()
            Me.RadGrid2.Rebind()
        End If
    End Sub

    Private Sub RadGrid1_EditCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.EditCommand
        If (e.Item.IsInEditMode) Then
            Dim item As RadComboBox = DirectCast((DirectCast(e.Item, GridEditableItem)).FindControl("cbQuestionTypes"), RadComboBox)
            If (item IsNot Nothing) Then
                Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
                    {
                        .CommandText = "SELECT [QuestionTypeID], [QuestionType] FROM [ExamQuestionTypes] ORDER BY [QuestionType] DESC"
                    }
                item.DataSource = dBAccess.ExecuteDataSet().Tables(0)
                item.DataTextField = "QuestionType"
                item.DataValueField = "QuestionTypeID"
                item.DataBind()
                If (TypeOf e.Item Is IGridInsertItem) Then
                    item.SelectedValue = "0e2a3d55-25e0-4e44-8115-024696670041"
                Else
                    Dim radComboBoxItem As Telerik.Web.UI.RadComboBoxItem = New Telerik.Web.UI.RadComboBoxItem() With
                        {
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
        Dim examquestion As Examquestion = New Examquestion() With
        {
            .QuestionID = System.Guid.NewGuid()
        }
        Dim guid As System.Guid = New System.Guid(Strings.Right(Me.cbExamlist.SelectedValue, 36))
        examquestion.ExamID = guid
        examquestion.Version = examDAL.getExamVersionbyID(examquestion.ExamID)
        Dim userDAL As userDAL = New userDAL()
        Dim userByName As ssUser = userDAL.GetUserByName(Me.User.Identity.Name)
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
            Dim examAnswer As ExamAnswer = New ExamAnswer() With
            {
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
            Dim now As ExamAnswer = New ExamAnswer() With
            {
                .Answer = "False",
                .AnswerID = System.Guid.NewGuid(),
                .QuestionID = examquestion.QuestionID,
                .isCorrectAnswer = False
            }
            guid1 = userByName.userID
            guid = New System.Guid(guid1.ToString())
            now.createdBy = guid
            now.TimeStamp = DateTime.Now
            examDAL.insertAnswer(now)
        End If
        Me.RadGrid2.Visible = False
        Me.RadGrid1.Rebind()
        Me.RadGrid1.MasterTableView.ClearEditItems()
    End Sub

    Private Sub RadGrid1_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "InitInsert" Then
            Me.Session("QuestionID") = Nothing
            Me.RadGrid2.Visible = False
        End If
    End Sub

    Private Sub RadGrid1_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If (e.Item.IsInEditMode) Then
            Dim cbQuestionTypes As RadComboBox = DirectCast((DirectCast(e.Item, GridEditableItem)).FindControl("cbQuestionTypes"), RadComboBox)

            If (cbQuestionTypes IsNot Nothing) Then
                Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
                    {
                        .CommandText = "SELECT [QuestionTypeID], [QuestionType] FROM [ExamQuestionTypes] ORDER BY [QuestionType] DESC"
                    }
                cbQuestionTypes.DataSource = dBAccess.ExecuteDataSet().Tables(0)
                cbQuestionTypes.DataTextField = "QuestionType"
                cbQuestionTypes.DataValueField = "QuestionTypeID"
                cbQuestionTypes.DataBind()
                If (TypeOf e.Item Is IGridInsertItem) Then
                    cbQuestionTypes.SelectedValue = "0e2a3d55-25e0-4e44-8115-024696670041"
                Else
                    Dim radComboBoxItem As Telerik.Web.UI.RadComboBoxItem = New Telerik.Web.UI.RadComboBoxItem() With
                        {
                            .Value = (DirectCast(e.Item.DataItem, DataRowView))("QuestionTypeID").ToString()
                        }
                    cbQuestionTypes.SelectedValue = radComboBoxItem.Value
                End If
            End If
        End If
    End Sub

    Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim str As String
        Dim dBAccess As DBAccess = New DBAccess("Phazzer")
        str = If(Me.cbExamlist.SelectedIndex <= -1, Utilities.zeroGuid().ToString(), Me.cbExamlist.SelectedValue)
        dBAccess.CommandText = "SELECT ExamQuestions.Question, ExamQuestions.QuestionID, ExamQuestions.QuestionTypeID, ExamQuestions.ExamID, ExamQuestionTypes.QuestionType, ExamQuestions.TimeStamp FROM ExamQuestions INNER JOIN ExamQuestionTypes ON ExamQuestions.QuestionTypeID = ExamQuestionTypes.QuestionTypeID WHERE ExamID = @ExamID ORDER BY ExamQuestions.TimeStamp DESC"
        dBAccess.AddParameter("@ExamID", str)
        Dim item As DataTable = dBAccess.ExecuteDataSet().Tables(0)
        Me.RadGrid1.DataSource = item
    End Sub


    Private Sub RadGrid1_PreRender(sender As Object, e As EventArgs) Handles RadGrid1.PreRender
        Dim enumerator As IEnumerator = Nothing
        Try
            enumerator = Me.RadGrid1.MasterTableView.Items.GetEnumerator()
            While enumerator.MoveNext()
                Dim current As GridDataItem = DirectCast(enumerator.Current, GridDataItem)
                If Session("QuestionID") Is Nothing OrElse current.GetDataKeyValue("QuestionID").ToString() = Session("QuestionID").ToString() Then
                    Continue While
                End If
                current.Selected = True
                Me.RadGrid2.Rebind()
                Me.RadGrid2.Visible = True
            End While
        Finally
        End Try
    End Sub

    Private Sub RadGrid1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadGrid1.SelectedIndexChanged
        Me.Session("QuestionID") = RadGrid1.SelectedValue
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
        Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
            {
                .CommandText = "UPDATE ExamQuestions SET Question = @Question, QuestionTypeID = @QuestionTypeID where QuestionID = @QuestionID"
            }
        dBAccess.AddParameter("@Question", examquestion.Question)
        dBAccess.AddParameter("@QuestionID", examquestion.QuestionID)
        dBAccess.AddParameter("@QuestionTypeID", examquestion.QuestionTypeID)
        dBAccess.ExecuteNonQuery()
        Me.RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid2_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles RadGrid2.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then

            If (Not e.Item.IsInEditMode) Then
                Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
                Dim checkBox As System.Web.UI.WebControls.CheckBox = DirectCast(item.FindControl("chkEditIsCorrect"), System.Web.UI.WebControls.CheckBox)
                Dim label As System.Web.UI.WebControls.Label = DirectCast(item.FindControl("lblCorrect"), System.Web.UI.WebControls.Label)
                If (checkBox.Checked) Then
                    label.Text = "Correct"
                End If
            End If
        End If

    End Sub

    Private Sub RadGrid2_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid2.NeedDataSource
        Dim questionID As Guid = RadGrid1.SelectedValue
        Dim dba As New DBAccess("Phazzer")
        dba.CommandText = "Select * from ExamAnswers where questionid=@questionid"
        dba.AddParameter("@questionid", questionID)
        Dim dt As DataTable = dba.ExecuteDataSet().Tables(0)
        Me.RadGrid2.DataSource = dt
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
        Dim dBAccess As DBAccess = New DBAccess("Phazzer") With
            {
                .CommandText = "Update ExamAnswers set Answer=@Answer, isCorrectAnswer=@isCorrectAnswer where AnswerID=@AnswerID"
            }
        dBAccess.AddParameter("@Answer", examAnswer.Answer)
        dBAccess.AddParameter("@isCorrectAnswer", examAnswer.isCorrectAnswer)
        dBAccess.AddParameter("@AnswerID", examAnswer.AnswerID)
        Try
            dBAccess.ExecuteNonQuery()
        Catch exception As System.Exception
            Dim message As String = exception.Message
        End Try
        Me.RadGrid2.Rebind()
    End Sub
End Class