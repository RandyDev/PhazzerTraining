Imports WebSupergoo.ABCpdf10
Imports PdfEnterpriseServices


Public Class Certificate
    Inherits System.Web.UI.Page
    Dim examdate As DateTime = New DateTime()
    Dim examid As Guid
    Dim certificateID As String = String.Empty
    Dim docFile = Nothing
    Dim docSubject = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request("id") Is Nothing) Then
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
        End If
        If (Not Utilities.IsValidGuid(Request("id"))) Then
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
        End If
        Dim StudentID As Guid = New Guid(Request("id"))
        Dim dba As DBAccess = New DBAccess("Phazzer")
        dba.CommandText = "SELECT studentExams.studentExamID, studentExams.studentID, studentExams.examID, studentExams.examDate, studentExams.Pass, ExamCertificate.CertificateID FROM studentExams INNER JOIN ExamCertificate ON studentExams.examID = ExamCertificate.ExamID WHERE (studentExams.Pass = 1) AND (studentExams.studentID = @studentID) ORDER BY studentExams.examDate DESC"
        dba.AddParameter("@studentID", StudentID)
        Dim dt As DataTable = dba.ExecuteDataSet().Tables(0)
        If (dt.Rows.Count <= 0) Then
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
        Else
            examid = dt.Rows(0)("examID")
            examdate = dt.Rows(0)("examDate")
            certificateID = dt.Rows(0)("CertificateID").ToString()
            CreatePDF(Me.Request("id"))
        End If
    End Sub

    Private Sub CreatePDF(Optional ByVal uid As String = "0")
        Dim userid As Guid
        If Utilities.IsValidGuid(uid) Then
            userid = New Guid(uid)
        End If
        Dim mUser As ssUser = New ssUser()
        Dim userDAL As userDAL = New userDAL()
        mUser = userDAL.GetUserByID(userid)
        If (mUser Is Nothing) Then
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
        End If
        Dim docname() As String = {"Certificate of Training_", mUser.LastName, "_", mUser.FirstName, ".pdf"}
        docFile = String.Concat(docname)
        docSubject = String.Concat("CertID: ", userid.ToString())
        Dim doc As WebSupergoo.ABCpdf10.Doc = New WebSupergoo.ABCpdf10.Doc()
        doc.Color.[String] = "0 0 0"
        doc.AddFont("Arial")
        doc.AddFont("Times New Roman")
        Dim Font1 = doc.AddFont("Verdana")
        Dim Font2 = doc.AddFont("Verdana Bold")
        Dim Font3 = doc.AddFont("Arial")
        Dim Font4 = doc.AddFont("Arial Bold")
        Dim Font5 = doc.AddFont("Times-Roman")
        Dim Font6 = doc.AddFont("Times-Bold")
        Dim filetype As String = Strings.Right("Enforcer Certificate of Training-Instructor.pdf", 3)
        Dim dataTable As DataTable = New DataTable()
        Dim certificate As CertificateObj = New CertificateObj()
        If filetype = "pdf" Or filetype = "xps" Then
            Dim dba As DBAccess = New DBAccess("Phazzer")
            dba.CommandText = "Select CertificateID FROM Exams WHERE ExamID = @examID"
            Dim thisExam As ExamOBJ = Session("Exam")
            dba.AddParameter("@examID", examid)
            Dim certid As Guid = dba.ExecuteScalar
            'GET CERTIFICATE
            dba.CommandText = "SELECT CertificateID, CertificateName,Certificate,CreatedBy,TimeStamp from Certificates WHERE CertificateID=@CertificateID"
            'Dim salescert As Guid = New Guid("de2b7c67-da85-4711-bfc4-58eb273568d1")
            'Dim usrcert As Guid = New Guid("76c90b66-b1e5-4c39-bea9-484bd4ce661b")
            'Dim instcert As Guid = New Guid("ca58fe33-3608-4628-8068-c53235547e1a")
            dba.AddParameter("@CertificateID", certid)
            dataTable = dba.ExecuteDataSet().Tables(0)
            Dim certificateObj As CertificateObj = New CertificateObj()
            Dim objectValue(0) As Object
            Dim item As DataRow = dataTable.Rows(0)
            Dim str1 As String = "Certificate"
            objectValue(0) = item(str1)
            Dim objArray As Object() = objectValue
            Dim flagArray() As Boolean = {True}
            Dim row As DataRow = dataTable.Rows(0)
            certificate.CertificateData = row.Item("Certificate")
            '            certificate.CertficateImage = row.Item("Certificate")
            certificate.CertificateID = row.Item("CertificateID")
            certificate.CertificateName = row.Item("CertificateName")
            certificate.TimeStamp = row.Item("TimeStamp")
        ElseIf filetype = "doc" Or filetype = "ocx" Then
            Dim pdfUtility As PdfUtilities = New PdfUtilities()
        End If
        doc.Read(certificate.CertificateData)
        Dim pageCount As Integer = doc.PageCount
        doc.Rect.Position(100, 315)
        doc.Rect.Right = 600
        doc.Rect.Top = 365
        doc.TextStyle.HPos = 0.5
        Dim uppername() As String = {"<font pid=", Font2, " >", mUser.FirstName.ToUpper(), " ", mUser.LastName.ToUpper(), "</font>"}
        Dim str2 As String = String.Concat(uppername)
        doc.FontSize = 40
        doc.AddHtml(str2)
        doc.Rect.Position(10, 80)
        doc.Rect.Right = 240
        doc.Rect.Top = 110
        doc.TextStyle.HPos = 0.5
        doc.TextStyle.VPos = 1
        Dim str3 As String = String.Concat("<font pid=", Font2, " >Brandon F. Womack</font>")
        doc.FontSize = 20
        doc.AddHtml(str3)

        doc.FontSize = 14.66
        doc.Rect.Position(10, 60)
        doc.Rect.Right = 240
        doc.Rect.Top = 75
        doc.TextStyle.HPos = 0.5
        Dim instructortitle As String = "MASTER INSTRUCTOR"
        Dim instTitle As String() = {"<font pid=", Font1, " >", instructortitle, "</font>"}
        Dim str5 As String = String.Concat(instTitle)
        doc.AddHtml(str5)
        doc.Rect.Position(480, 80)
        doc.Rect.Right = 700
        Dim [date] As DateTime = examdate.ToShortDateString()
        Dim strexamdate As String() = New String() {"<font pid=", Font2, " >", Format([date], "dd MMMM yyyy"), "</font"}
        doc.AddHtml(String.Concat(strexamdate))
        doc.FontSize = 12
        doc.Rect.Position(300, 10)
        doc.Rect.Right = 700
        doc.Rect.Top = 25
        doc.TextStyle.HPos = 1
        Dim validationmark As String() = New String() {"<font color=white pid=", Font1, " >validation id: ", Nothing, Nothing}
        validationmark(3) = mUser.userID.ToString()
        validationmark(4) = "</font>"
        doc.FontSize = 8
        'doc.AddGrid()
        doc.AddHtml(String.Concat(validationmark))
        Dim num As Integer = doc.AddObject("<< >>")
        doc.SetInfo(-1, "/Info:Ref", num.ToString())
        doc.SetInfo(-1, "/Info*/Title:Text", "PhaZZer Training Group Certificate of Training")
        doc.SetInfo(-1, "/Info*/Author:Text", "PhaZZer Training Group")
        doc.SetInfo(-1, "/Info*/Subject:Text", "Certificate of Training")
        doc.SetInfo(doc.Root, "/Metadata:Del", "")
        Dim data As Byte() = doc.GetData()
        Response.ContentType = "application/pdf"
        If Request("dld") = "yes" Then
            Response.AddHeader("content-disposition", String.Concat("inline; filename=", docFile))
        Else
            Response.AddHeader("Content-Disposition", String.Concat("attachment; filename=", docFile))
        End If
        Response.AddHeader("content-length", (CInt(data.Length)).ToString())
        Response.BinaryWrite(data)
        Response.[End]()
    End Sub
End Class