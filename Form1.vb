Public Class Form1
    Public PrevRandLoc As Integer = 0
    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        CognateWeighting = GenerateCognateWeighting()
        GenerateTree(TextBox1.Text)
        ProgressText.Text = "Done!"
    End Sub
    Private Sub StartGeneration_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumberOfCognates = 499
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
            ReDim CognateArray(499, 0)
            Label4.Text = "0"
            Label5.Text = "0"
            Label6.Text = "0"
            Label8.Text = "0"
            Label10.Text = "0"

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        GridChart.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub
End Class
