Imports Telerik.Web.UI

Public Class ReleaseOfLiability
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            errOFF()
        End If
        Me.lblIPaddress.Text = Me.Request.ServerVariables("REMOTE_ADDR")
        Me.lblTimeStamp.Text = DateTime.Now.ToString()
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

    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        lblerr.Text = String.Empty
        lblerr.ForeColor = Drawing.Color.Red
        Dim retstr As String = String.Empty
        Dim dba As DBAccess = New DBAccess("EventReg")
        Dim str As String = Me.txtEmail.Text.Trim()
        Dim bookingid As Integer = 0
        If (Not Utilities.isValidEmail(Me.txtEmail.Text.Trim())) Then
            Me.lblerr.Text = "Invalid eMail address"
            errON()
            Return
        End If
        If IsNumeric(txtBookingID.Text.Trim()) Then
            bookingid = txtBookingID.Text.Trim()
            If txtBookingID.Text.Length <> 4 Then
                lblerr.Text &= "Invalid Booking ID"
                errON()
                Return
            End If
        Else 'not numeric
            lblerr.Text &= "Invalid Booking ID"
            errON()
            Return
        End If 'isnumeric
        If lblerr.Text = "" Then
            dba.CommandText = "SELECT tblNaDevEventsBookingGuest.bookingId, tblNaDevEventsBookingGuest.GuestId, tblNaDevEventsBookingGuest.addressId, " &
                "tblNaDevEventsUsersSignedUpAddress.emailAddress " &
                "From tblNaDevEventsBookingGuest INNER Join " &
                "tblNaDevEventsUsersSignedUpAddress On tblNaDevEventsBookingGuest.addressId = tblNaDevEventsUsersSignedUpAddress.addressId " &
                "Where emailaddress = @email And bookingId=@bid"
            dba.AddParameter("@email", str)
            dba.AddParameter("@bid", bookingid)
            Dim item As DataTable = dba.ExecuteDataSet().Tables(0)
            Dim userDAL As userDAL = New userDAL()
            Dim _mUser As ssUser = New ssUser()
            _mUser = userDAL.GetUserByName(str)
            dba = New DBAccess("Phazzer")
            If (item.Rows.Count <= 0) Then
                lblerr.ForeColor = Drawing.Color.Red
                lblerr.Text = "Attendee Not Found!<br>eMail address <em>and</em> Booking ID <u>MUST</u> match registration form"
                errON()
                Return
            Else
                dba.CommandText = "select count(userid) from ROLsignatures WHERE userid = @userid AND BookingID = @BookingID"
                dba.AddParameter("@userid", _mUser.userID)
                dba.AddParameter("@BookingID", bookingid)
                If dba.ExecuteScalar() > 0 Then
                    lblerr.ForeColor = Drawing.Color.Green
                    lblerr.Text = "Thank You Again ..."
                    errON(True)
                    Return
                Else
                    dba.CommandText = "INSERT INTO ROLsignatures (userid, BookingID, ipAddress, TimeStamp) VALUES (@userid,@BookingID,@ipAddress,@TimeStamp)"
                    dba.AddParameter("@userid", _mUser.userID)
                    dba.AddParameter("@BookingID", bookingid)
                    dba.AddParameter("@ipAddress", lblIPaddress.Text)
                    dba.AddParameter("@TimeStamp", lblTimeStamp.Text)
                    Try
                        dba.ExecuteNonQuery()
                        lblerr.ForeColor = Drawing.Color.Green
                        lblerr.Text = "Thank You!"
                        errON(True)
                        Return
                    Catch Exception As System.Exception
                        Dim message As String = Exception.Message
                        lblerr.Text = message
                    End Try
                End If
            End If
        End If
    End Sub
    Private Sub ErrON(Optional ByVal starttimer As Boolean = False)
        lblerr.Visible = True
        If starttimer Then
            timerRD.Enabled = True
            timerRD.Interval = 1000
            lblRdTimer.Text = "5"
            lblerr.Visible = True
            lblRdTimerText.Visible = timerRD.Enabled
            lblRdTimer.Visible = timerRD.Enabled
        End If
    End Sub
    Private Sub ErrOFF()
        timerRD.Enabled = False
        lblerr.Visible = False
        lblRdTimer.Visible = timerRD.Enabled
        lblRdTimerText.Visible = timerRD.Enabled
    End Sub
    Private Sub TimerRD_Tick(sender As Object, e As EventArgs) Handles timerRD.Tick

        Dim tdelapsed As Integer = Integer.Parse(lblRdTimer.Text)
        tdelapsed = tdelapsed - 1
        lblRdTimer.Text = tdelapsed.ToString
        If tdelapsed = 0 Then
            Response.Redirect("http://PhaZZerTraining.com")
        End If
    End Sub

End Class