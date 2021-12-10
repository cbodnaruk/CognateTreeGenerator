Public Class Form1
    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        CognateWeighting = GenerateCognateWeighting()
        MsgBox(CognateWeighting(NumberOfCognates))
        ProgressText.Text = "Done!"
    End Sub

    Private Sub SelectButton_Click(sender As Object, e As EventArgs) Handles SelectButton.Click
        Dim CognateIndex = SelectCognate(CognateWeighting)
        MsgBox(CognateIndex)
        ProgressText.Text = "Done!"
    End Sub

    Private Sub StartGeneration_Click(sender As Object, e As EventArgs) Handles StartGeneration.Click
        GenerateTree(TextBox1.Text)
        ProgressText.Text = "Done!"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumberOfCognates = 499
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

End Class
