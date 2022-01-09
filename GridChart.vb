

Public Class GridChart
    Public GraphWidth, GraphHeight As Integer
    Public GridSize As Integer = 50
    Public LabelList As New List(Of Label)
    Public LabelDataList As New List(Of Integer)
    Public PointsInitialised As Boolean = False
    Public GridStep(1) As Integer
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GridSize = My.Settings.GridSize
        Initialise_Points()


    End Sub
    Private Sub Initialise_Points()
        'Dim PointCounter As Integer = 0
        'Dim TotalPoints As Integer = GridSize ^ 2
        'Dim ProgStep As Decimal = 50 / TotalPoints
        'Dim ProgCounter As Decimal = 0
        'Dim newLabel As Label
        GraphWidth = Me.Width - 60
        GraphHeight = Me.Height - 60
        GridStep(0) = GraphWidth / (GridSize + 1)
        GridStep(1) = GraphHeight / (GridSize + 1)
        Draw_Label(1)

        'For i = 1 To TotalPoints
        '    newLabel = New Label With {
        '        .Name = "Label" & i,
        '        .Text = "",
        '        .Size = New System.Drawing.Size(30, 14)
        '    }

        '    Me.Controls.Add(newLabel)

        '    LabelList.Add(newLabel)
        '    ProgCounter += ProgStep
        '    LoadingBar.ProgressBar1.Value = Int(ProgCounter)
        'Next


        PointsInitialised = True
        'LoadingBar.Hide()
        'Draw_Grid()


    End Sub
    Public Function Draw_Label(stringID)
        Dim DecX, DecY As Integer
        Dim newLabel As Label
        DecX = stringID Mod GridSize
        DecY = (stringID / GridSize) + 1

        newLabel = New Label With {
    .Name = "Label" & stringID,
    .Text = "",
    .Size = New System.Drawing.Size(14, 14),
    .Font = New Font("Microsoft Sans Serif", 4),
    .Location = New Point(30 + (DecX * GridStep(0)), 30 + (DecY * GridStep(1)))
}

        Me.Controls.Add(newLabel)

        LabelList.Add(newLabel)
        LabelDataList.Add(stringID)
        Return LabelList.Count - 1
    End Function
    Private Sub Redraw_Grid()
        'LoadingBar.Show()
        'Dim ProgStep As Decimal = 100 / (LabelList.Count)
        'Dim ProgCounter As Decimal = 1
        Dim DecX, DecY As Integer
        Dim stringID As Integer
        GraphWidth = Me.Width - 60
        GraphHeight = Me.Height - 60
        GridStep(0) = GraphWidth / (GridSize + 1)
        GridStep(1) = GraphHeight / (GridSize + 1)
        For i = 0 To LabelList.Count - 1
            stringID = LabelDataList(i)
            DecX = stringID Mod GridSize
            DecY = (stringID / GridSize) + 1

            LabelList(i).Location = New Point(30 + (DecX * GridStep(0)), 30 + (DecY * GridStep(1)))

            'ProgCounter += ProgStep
            'LoadingBar.ProgressBar1.Value = Int(ProgCounter)

        Next

        'LoadingBar.Close()
    End Sub

    Private Sub Form2_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        GraphWidth = Me.Width - 60
        GraphHeight = Me.Height - 60
        GridStep(0) = GraphWidth / (GridSize + 1)
        GridStep(1) = GraphHeight / (GridSize + 1)
        Redraw_Grid()

    End Sub
End Class