Imports PackageIO
Public Class Form1
    Dim filepath As String
    Dim fdialog As New Form2
    Private Sub NumericUpDown2_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown2.ValueChanged
        If NumericUpDown2.Value <= 99999 Then
            TextBox3.Text = "1"
            TextBox2.Text = "Fauché"
        ElseIf NumericUpDown2.Value >= 100000 And NumericUpDown2.Value <= 499999 Then
            TextBox3.Text = "2"
            TextBox2.Text = "Précaire"
        ElseIf NumericUpDown2.Value >= 500000 And NumericUpDown2.Value <= 1999999 Then
            TextBox3.Text = "3"
            TextBox2.Text = "Stable"
        ElseIf NumericUpDown2.Value >= 2000000 And NumericUpDown2.Value <= 4999999 Then
            TextBox3.Text = "4"
            TextBox2.Text = "Aisé"
        ElseIf NumericUpDown2.Value >= 5000000 And NumericUpDown2.Value <= 9999999 Then
            TextBox3.Text = "5"
            TextBox2.Text = "Riche"
        ElseIf NumericUpDown2.Value >= 10000000 And NumericUpDown2.Value <= 19999999 Then
            TextBox3.Text = "6"
            TextBox2.Text = " Très Riche"
        ElseIf NumericUpDown2.Value >= 20000000 Then
            TextBox3.Text = "7"
            TextBox2.Text = "Plein aux as"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim open As New OpenFileDialog
        fdialog.Label1.Text = "Open Data0, Data1 or Data2" & vbNewLine & "Backup your save before using" & vbNewLine & "Ironfall Invasion Save Editor"
        fdialog.ShowDialog()
        open.Title = "Open save Data0, Data1 or Data2"
        open.ShowDialog()
        filepath = open.FileName
        Readfile()
    End Sub
    Private Sub Readfile()
        Try
            Dim Reader As New PackageIO.Reader(filepath, PackageIO.Endian.Little)
            Reader.Position = &H4 'profile name
            TextBox1.Text = Reader.ReadUnicodeString(8)
            Reader.Position = &H26 'botgun
            NumericUpDown1.Value = Reader.ReadUInt16
            Reader.Position = &H2A 'pistolet
            NumericUpDown3.Value = Reader.ReadUInt16
            Reader.Position = &H2E 'f.d'assault
            NumericUpDown4.Value = Reader.ReadUInt16
            Reader.Position = &H32 'lance roquette
            NumericUpDown6.Value = Reader.ReadUInt16
            Reader.Position = &H36 'lance grenades
            NumericUpDown5.Value = Reader.ReadUInt16
            Reader.Position = &H3A 'fusil a pompe
            NumericUpDown7.Value = Reader.ReadUInt16
            Reader.Position = &H3E 'fusil de precision
            NumericUpDown8.Value = Reader.ReadUInt16
            Reader.Position = &H1558 'Credits
            NumericUpDown2.Value = Reader.ReadUInt32

            Reader.Position = &H24 'botgun unlock
            NumericUpDown11.Value = Reader.ReadUInt16
            Reader.Position = &H28 'pistolet unlock
            NumericUpDown12.Value = Reader.ReadUInt16
            Reader.Position = &H2C 'f.d'assault unlock
            NumericUpDown13.Value = Reader.ReadUInt16
            Reader.Position = &H30 'lance roquette unlock
            NumericUpDown15.Value = Reader.ReadUInt16
            Reader.Position = &H34 'lance grenades unlock
            NumericUpDown14.Value = Reader.ReadUInt16
            Reader.Position = &H38 'fusil a pompe unlock
            NumericUpDown16.Value = Reader.ReadUInt16
            Reader.Position = &H3C 'fusil de precision unlock
            NumericUpDown17.Value = Reader.ReadUInt16
        Catch ex As Exception
            fdialog.Label1.Text = "Data0, Data1 or Data2 not load" & vbNewLine & "Save file is corrupted or not a Ironfall Invasion save"
            fdialog.ShowDialog()
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If RadioButton1.Checked = True Then
                Dim Writer As New PackageIO.Writer(filepath, PackageIO.Endian.Little)
                For i As Integer = 0 To 10
                    Writer.Position = &H1540 + i
                    Writer.WriteInt8(0)
                Next
                fdialog.Label1.Text = "All challenges has been unlocked"
                fdialog.ShowDialog()
            End If
        Catch ex As Exception
            fdialog.Label1.Text = "An error has occured, load a save file first"
            fdialog.ShowDialog()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Writefile()
    End Sub
    Private Sub Writefile()
        Try
            Dim Writer As New PackageIO.Writer(filepath, PackageIO.Endian.Little)
            For i As Integer = 0 To 7 'delete some characters before write
                Writer.Position = &H4 + i
                Writer.WriteInt8(0)
            Next
            Writer.Position = &H4 'profile name
            Writer.WriteUnicodeString(TextBox1.Text)
            Writer.Position = &H26 'botgun
            Writer.WriteUInt16(NumericUpDown1.Value)
            Writer.Position = &H2A 'pistolet
            Writer.WriteUInt16(NumericUpDown3.Value)
            Writer.Position = &H2E 'f.d'assault
            Writer.WriteUInt16(NumericUpDown4.Value)
            Writer.Position = &H32 'lance roquette
            Writer.WriteUInt16(NumericUpDown6.Value)
            Writer.Position = &H36 'lance grenades
            Writer.WriteUInt16(NumericUpDown5.Value)
            Writer.Position = &H3A 'fusil a pompe
            Writer.WriteUInt16(NumericUpDown7.Value)
            Writer.Position = &H3E 'fusil de precision
            Writer.WriteUInt16(NumericUpDown8.Value)
            Writer.Position = &H1558 'Credits
            Writer.WriteUInt32(NumericUpDown2.Value)

            Writer.Position = &H24 'botgun unlock
            Writer.WriteUInt16(NumericUpDown11.Value)
            Writer.Position = &H28 'pistolet unlock
            Writer.WriteUInt16(NumericUpDown12.Value)
            Writer.Position = &H2C 'f.d'assault unlock
            Writer.WriteUInt16(NumericUpDown13.Value)
            Writer.Position = &H30 'lance roquette unlock
            Writer.WriteUInt16(NumericUpDown15.Value)
            Writer.Position = &H34 'lance grenades unlock
            Writer.WriteUInt16(NumericUpDown14.Value)
            Writer.Position = &H38 'fusil a pompe unlock
            Writer.WriteUInt16(NumericUpDown16.Value)
            Writer.Position = &H3C 'fusil de precision unlock
            Writer.WriteUInt16(NumericUpDown17.Value)
            fdialog.Label1.Text = "File save"
            fdialog.ShowDialog()
        Catch ex As Exception
            fdialog.Label1.Text = "An error has occured, load a save file first"
            fdialog.ShowDialog()
        End Try
    End Sub

    Private Sub NumericUpDown11_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown11.ValueChanged
        If NumericUpDown11.Value = 0 Then
            CheckBox1.Checked = False
        ElseIf NumericUpDown11.Value > 0 Then
            CheckBox1.Checked = True
        End If
    End Sub

    Private Sub NumericUpDown12_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown12.ValueChanged
        If NumericUpDown12.Value = 0 Then
            CheckBox2.Checked = False
        ElseIf NumericUpDown12.Value > 0 Then
            CheckBox2.Checked = True
        End If
    End Sub

    Private Sub NumericUpDown13_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown13.ValueChanged
        If NumericUpDown13.Value = 0 Then
            CheckBox3.Checked = False
        ElseIf NumericUpDown13.Value > 0 Then
            CheckBox3.Checked = True
        End If
    End Sub

    Private Sub NumericUpDown14_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown14.ValueChanged
        If NumericUpDown14.Value = 0 Then
            CheckBox4.Checked = False
        ElseIf NumericUpDown14.Value > 0 Then
            CheckBox4.Checked = True
        End If
    End Sub

    Private Sub NumericUpDown15_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown15.ValueChanged
        If NumericUpDown15.Value = 0 Then
            CheckBox5.Checked = False
        ElseIf NumericUpDown15.Value > 0 Then
            CheckBox5.Checked = True
        End If
    End Sub

    Private Sub NumericUpDown16_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown16.ValueChanged
        If NumericUpDown16.Value = 0 Then
            CheckBox6.Checked = False
        ElseIf NumericUpDown16.Value > 0 Then
            CheckBox6.Checked = True
        End If
    End Sub

    Private Sub NumericUpDown17_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown17.ValueChanged
        If NumericUpDown17.Value = 0 Then
            CheckBox7.Checked = False
        ElseIf NumericUpDown17.Value > 0 Then
            CheckBox7.Checked = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            NumericUpDown11.Value = 1
        ElseIf CheckBox1.Checked = False Then
            NumericUpDown11.Value = 0
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            NumericUpDown12.Value = 3073
        ElseIf CheckBox2.Checked = False Then
            NumericUpDown12.Value = 0
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            NumericUpDown13.Value = 6145
        ElseIf CheckBox3.Checked = False Then
            NumericUpDown13.Value = 0
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            NumericUpDown15.Value = 9217
        ElseIf CheckBox5.Checked = False Then
            NumericUpDown15.Value = 0
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            NumericUpDown14.Value = 12289
        ElseIf CheckBox4.Checked = False Then
            NumericUpDown14.Value = 0
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            NumericUpDown16.Value = 15361
        ElseIf CheckBox6.Checked = False Then
            NumericUpDown16.Value = 0
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = True Then
            NumericUpDown17.Value = 18433
        ElseIf CheckBox7.Checked = False Then
            NumericUpDown17.Value = 0
        End If
    End Sub
End Class
