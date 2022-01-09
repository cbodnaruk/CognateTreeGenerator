

Public Class GridChart
    Public GraphWidth, GraphHeight As Integer
    Public GridSize As Integer = 50
    Public LabelList As New List(Of Label)
    Public PointsInitialised As Boolean = False
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GridSize = My.Settings.GridSize
        Initialise_Points()


    End Sub
    Private Sub Initialise_Points()
        Dim PointCounter As Integer = 0
        Dim TotalPoints As Integer = GridSize ^ 2
        Dim ProgStep As Decimal = 50 / TotalPoints
        Dim ProgCounter As Decimal = 0
        Dim newLabel As Label

        For i = 1 To TotalPoints
                newLabel = New Label With {
                .Name = "Label" & i,
                .Text = "",
                .Size = New System.Drawing.Size(30, 14)
            }

            Me.Controls.Add(newLabel)

            LabelList.Add(newLabel)
            ProgCounter += ProgStep
            LoadingBar.ProgressBar1.Value = Int(ProgCounter)
        Next


        PointsInitialised = True
            LoadingBar.Hide()
            Draw_Grid()


    End Sub
    Private Sub Draw_Grid()
        LoadingBar.Show()
        Dim GridStep(1) As Integer
        Dim CurrentLocation(1) As Integer
        Dim CurrentOfTotal As Integer = 0
        Dim ProgStep As Decimal = 50 / (GridSize ^ 2)
        Dim ProgCounter As Decimal = 50
        If PointsInitialised = True Then
            GraphWidth = Me.Width - 60
            GraphHeight = Me.Height - 60
            GridStep(0) = GraphWidth / (GridSize + 1)
            GridStep(1) = GraphHeight / (GridSize + 1)
            For x = 1 To GridSize
                CurrentLocation(0) = 30 + (x * GridStep(0))
                For y = 1 To GridSize
                    CurrentLocation(1) = 30 + (y * GridStep(1))
                    LabelList(CurrentOfTotal).Location = New Point(CurrentLocation(0), CurrentLocation(1))
                    CurrentOfTotal += 1
                    ProgCounter += ProgStep
                    LoadingBar.ProgressBar1.Value = Int(ProgCounter)
                Next
            Next


        End If
        LoadingBar.Close()
    End Sub

    Private Sub Form2_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        Draw_Grid()

    End Sub
End Class