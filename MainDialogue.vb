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
            ReDim NodeArray(1, 0)
            ReDim CognateArray(My.Settings.NumberOfCognates, 0)
            Label4.Text = "0"
            Label5.Text = "0"
            Label6.Text = "0"
            Label8.Text = "0"
            Label10.Text = "0"
            GridChart.Close()
            GridChart.Show()
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
        My.Settings.NoTerminalNodes = TextBox1.Text

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        My.Settings.CognateLoss = TextBox2.Text

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        My.Settings.BifurChance = TextBox3.Text
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        NumberOfCognates = TextBox4.Text
        My.Settings.NumberOfCognates = TextBox4.Text
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
        If TextBox5.Text <> PrevGridSize And TextBox5.Text > 70 Then
            If MsgBox("Reload Grid Chart? Values higher then 70 may crash program.", vbYesNo) = vbYes Then
                GridChart.Close()
                GridChart.Show()
            End If
        Else

            If MsgBox("Reload Grid Chart?", vbYesNo) = vbYes Then
                GridChart.Close()
                GridChart.Show()
            End If
        End If
    End Sub

    Private Sub TextBox5_Enter(sender As Object, e As EventArgs) Handles TextBox5.Enter
        PrevGridSize = TextBox5.Text
    End Sub
End Class
