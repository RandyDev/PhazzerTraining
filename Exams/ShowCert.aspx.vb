Imports WebSupergoo.ABCpdf10
Imports PdfEnterpriseServices
Imports System.IO
Imports System.Net.Mail

Public Class ShowCert
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sxamid As String = Request("sxamid")
        CreatePDF(sxamid)
        'todo handle failed stExamID
    End Sub
    Private Sub CreatePDF(Optional ByVal studentExamid As String = "0")
        '        Dim userid As Guid
        Dim edal As New ExamDAL
        Dim userDAL As userDAL = New userDAL()
        Dim mUser As ssUser = New ssUser()
        Dim cert As CertificateObj = New CertificateObj
        If Utilities.IsValidGuid(studentExamid) Then
            Dim sxamid As Guid = New Guid(studentExamid)
            cert = New CertificateObj
            cert = edal.getCertByStudentExamID(sxamid)
            mUser = userDAL.GetUserByID(cert.StudentID)
        Else
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
        End If
        If (mUser Is Nothing) Then
            Response.Redirect("~/UnauthorizedNoMaster.aspx")
        End If


        Dim docname As String = Left(cert.CertificateName, Len(cert.CertificateName) - 4) & "_" & mUser.LastName & "_" & mUser.FirstName & ".pdf"
        Dim docSubject As String = String.Concat("CertID: ", cert.StudentExamID.ToString())
        Dim doc As WebSupergoo.ABCpdf10.Doc = New WebSupergoo.ABCpdf10.Doc()
        doc.Color.[String] = "0 0 0"
        doc.AddFont("Arial")
        doc.AddFont("Times New Roman")
        Dim Font1 = doc.EmbedFont("Verdana")
        Dim Font2 = doc.EmbedFont("Verdana Bold")
        Dim Font3 = doc.EmbedFont("Arial")
        Dim Font4 = doc.EmbedFont("Arial Bold")
        Dim Font5 = doc.EmbedFont("Times-Roman")
        Dim Font6 = doc.EmbedFont("Times-Bold")
        Dim filetype As String = Strings.Right(docname, 3)
        Dim dataTable As DataTable = New DataTable()
        If filetype = "pdf" Or filetype = "xps" Then
            Dim certid As Guid = cert.CertificateID
            Dim objectValue(0) As Object
            objectValue(0) = cert.CertificateData
            Dim objArray As Object() = objectValue
            '            Dim flagArray() As Boolean = {True}
        ElseIf filetype = "doc" Or filetype = "ocx" Then
            '            Dim pdfUtility As PdfUtilities = New PdfUtilities()
        End If
        'read blank certificate into new document
        doc.Read(cert.CertificateData)
        'set page counter
        Dim pageCount As Integer = doc.PageCount
        'get student name to put on certificate
        Dim studentName As String = "<font pid=" & Font2 & " >" & mUser.FirstName() & " " & mUser.LastName() & "</font>"
        'position the cursor/rectangle
        doc.Rect.Position(160, 320)
        doc.Rect.Top = 360
        doc.Rect.Right = 560
        'set the vertical at the base line and the horizontal to center
        doc.TextStyle.VPos = 0
        doc.TextStyle.HPos = 0.5
        'set the font size
        doc.FontSize = 35
        'add the students name at this postion
        doc.AddHtml(studentName)
        'get the instructor
        'first, get the eventID
        Dim eventid As Integer = edal.GetEventIDByStudenExamID(New Guid(studentExamid))
        'then get the event's instructor
        Dim instructor As ssUser = edal.GetInstructorByEventID(eventid)
        'create the instructorName
        Dim instructorName As String = String.Empty
        If Not instructor Is Nothing And Not instructor.FirstName = "" Then
            instructorName = "<font pid=" & Font2 & " >" & instructor.FirstName & " " & instructor.LastName & "</font>"
        Else
            instructor = edal.GetInstructorByEventID(1)
            instructorName = "<font pid=" & Font2 & " >" & instructor.FirstName & " " & instructor.LastName & "</font>"
        End If
        'todo find instructor COND

        'position the cursor/rectangle
        doc.Rect.Position(15, 80)
        doc.Rect.Top = 110
        doc.Rect.Right = 240
        'set the vertical at the base line and the horizontal to center
        doc.TextStyle.VPos = 1
        doc.TextStyle.HPos = 0.5
        'set the font size
        doc.FontSize = 18
        'add the Instructor's name name at this postion
        doc.AddHtml(instructorName)
        'create instrucotor title
        Dim instructorTitle As String = String.Empty
        'todo check for empty role, stuff with Master Instructor
        'check the intructor's role(s) to see if he is is in an instructor role
        For Each role As String In instructor.myRoles
            If role.Contains("Instructor") Then
                'if he is, get his title
                instructorTitle = role
            End If
        Next
        'format the title
        Dim instTitle As String = "<font pid=" & Font2 & " >" & instructorTitle & "</font>"
        'set font size
        doc.FontSize = 14.66
        'set cursor position
        doc.Rect.Position(10, 60)
        doc.Rect.Right = 240
        doc.Rect.Top = 75
        'set horizontal to center
        doc.TextStyle.HPos = 0.5
        'add the title
        doc.AddHtml(instTitle)

        'add the exam date to certificate
        Dim examdate As String = "<font pid=" & Font2 & " >" & Format(cert.ExamDate, "dd MMMM yyyy") & "</font"
        doc.Rect.Position(475, 80)
        doc.TextStyle.HPos = 0.5
        doc.Rect.Right = 720
        doc.AddHtml(examdate)

        'add the validationMark in bottom righ corner
        'to do change to sans serif fong
        Dim validationmark As String = "<font pid=" & Font3 & " >validation id: " & cert.StudentExamID.ToString() & "</font>"
        doc.Rect.Position(400, 10)
        doc.Rect.Top = 45
        doc.Rect.Right = 710
        doc.TextStyle.HPos = 1
        doc.FontSize = 11
        doc.Color.[String] = "255 255 255"
        doc.AddHtml(validationmark)
        '************************************
        '************************* end doc creation
        '************************************
        If Request("dld") = "yes" Then
            '            Dim docStream As Stream = doc.GetStream()
            '           docStream.Position = 0
            Dim theData() As Byte = doc.GetData
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & docname)
            Response.AddHeader("content-length", theData.Length.ToString())
            Response.BinaryWrite(theData)
            Response.End()
        End If
        If Request("eMail") = "yes" Then
            Dim pdf As Stream = New MemoryStream
            doc.Save(pdf)
            doc.Clear()
            pdf.Position = 0
            Dim msg As RdMailer = New RdMailer
            msg.To = mUser.eMail.ToString
            msg.From = "PhaZZer Training Group <no-reply@PhaZZerTraining.com>"
            msg.Subject = "Certificate of Training"
            msg.Body = "Your Certificate of Training is attached as a pdf document"
            Dim attachment As New System.Net.Mail.Attachment(pdf, docname, "application/pdf")
            msg.Attachment = attachment
            RdMailer.SendMail(msg, "Randy@RandyDev.com")
            Response.Write("email sent, use Alt-left arrow to return to page")
        End If
        If Request("view") = "yes" Then
            Dim docStream As Stream = doc.GetStream()
            docStream.Position = 0
            Dim theData() As Byte = doc.GetData
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "inline; filename=" & docname)
            Response.AddHeader("content-length", theData.Length.ToString())
            Response.BinaryWrite(theData)
            Response.End()
        End If
    End Sub
End Class