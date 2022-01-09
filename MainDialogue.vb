Public Class MainDialogue
    Public PrevRandLoc As Integer = 0
    Public PrevGridSize
    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        CognateWeighting = GenerateCognateWeighting()
        GenerateTree(TextBox1.Text)
        ProgressText.Text = "Done!"
    End Sub
    Private Sub StartGeneration_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumberOfCognates = My.Settings.NumberOfCognates
        TextBox1.Text = My.Settings.NoTerminalNodes
        TextBox2.Text = My.Settings.CognateLoss
        TextBox3.Text = My.Settings.BifurChance
        TextBox4.Text = My.Settings.NumberOfCognates
        TextBox5.Text = My.Settings.GridSize
        TextBox6.Text = My.Settings.PercentBorrowingChance
        GridChart.Show()
    End Sub

    Private Sub IsolateButton_Click(sender As Object, e As EventArgs) Handles IsolateButton.Click
        IsolateTerminals()
        ProgressText.Text = "Done!"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CreateNewick()
        ProgressText.Text = "Done!"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Reset generator?", vbYesNo) = vbYes Then
            ReDim NodeArray(3, 0)
            ReDim CognateArray(My.Settings.NumberOfCognates, 0)
            Label4.Text = "0"
            Label5.Text = "0"
            Label6.Text = "0"
            Label8.Text = "0"
            Label10.Text = "0"
            If Areal = True Then
                GridChart.Close()
                GridChart.Show()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        GridChart.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            My.Settings.NoTerminalNodes = TextBox1.Text
        Catch exc As InvalidCastException
            If TextBox1.Text = "" Then
            Else
                TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length - 1, 1)
            End If
        End Try

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Try
            My.Settings.CognateLoss = TextBox2.Text
        Catch exc As InvalidCastException
            If TextBox2.Text = "" Then
            Else
                TextBox2.Text = TextBox2.Text.Remove(TextBox2.Text.Length - 1, 1)
            End If
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Try
            My.Settings.BifurChance = TextBox3.Text
        Catch exc As InvalidCastException
            If TextBox3.Text = "" Then
        Else
            TextBox3.Text = TextBox3.Text.Remove(TextBox3.Text.Length - 1, 1)
        End If
        End Try
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Try
            NumberOfCognates = TextBox4.Text
            My.Settings.NumberOfCognates = TextBox4.Text
        Catch exc As InvalidCastException
            If TextBox4.Text = "" Then
            Else
                TextBox4.Text = TextBox4.Text.Remove(TextBox4.Text.Length - 1, 1)
            End If
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.Save()
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        Try
            GridChart.GridSize = TextBox5.Text
            My.Settings.GridSize = TextBox5.Text
        Catch exc As InvalidCastException
            If TextBox5.Text = "" Then
            Else
                TextBox5.Text = TextBox5.Text.Remove(TextBox5.Text.Length - 1, 1)
            End If
        End Try
    End Sub

    Private Sub TextBox5_Leave(sender As Object, e As EventArgs) Handles TextBox5.Leave
        If TextBox5.Text = "" Then
            MsgBox("Please Enter a Number", vbExclamation)
        End If
        If MsgBox("Reload Grid Chart?", vbYesNo) = vbYes Then
                GridChart.Close()
            GridChart.Show()
        End If

    End Sub

    Private Sub TextBox5_Enter(sender As Object, e As EventArgs) Handles TextBox5.Enter
        PrevGridSize = TextBox5.Text
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Try
            My.Settings.PercentBorrowingChance = TextBox6.Text
        Catch exc As InvalidCastException
            If TextBox6.Text = "" Then
            Else
                TextBox6.Text = TextBox6.Text.Remove(TextBox6.Text.Length - 1, 1)
            End If
        End Try

    End Sub

    Private Sub ArealCheck_CheckedChanged(sender As Object, e As EventArgs) Handles ArealCheck.CheckedChanged
        If ArealCheck.Checked = True Then
            Areal = True
            GridChart.Show()
            TextBox5.Enabled = True
            TextBox6.Enabled = True
        Else
            Areal = False
            GridChart.Close()
            TextBox5.Enabled = False
            TextBox6.Enabled = False
        End If
    End Sub
End Class
