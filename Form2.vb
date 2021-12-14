Public Class Form2
    Public pictureBox1 As New PictureBox()
    Public xLength, yLength As Decimal
    Public CurrentX, CurrentY As Decimal
    Public BoundingRect As New Rectangle(0, 0, 0, 0)
    Public EachEll As New RectangleF(0, 0, 4, 4)
    Public GridSize = 5
    Public CurrentBrush As Brush
    Public boxes As New List(Of RectangleF)
    Private fnt As New Font("Arial", 10)
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pictureBox1.Dock = DockStyle.Fill
        pictureBox1.BackColor = Color.White

        'BoundingRect = New Rectangle(pictureBox1.Left + 15, pictureBox1.Top + 15, pictureBox1.Width - 30, pictureBox1.Height - 30)

        With BoundingRect
            .X = (pictureBox1.Left + 15)
            .Y = pictureBox1.Top + 15
            .Width = pictureBox1.Width - 30
            .Height = pictureBox1.Height - 30

        End With

        AddHandler pictureBox1.Paint, AddressOf Me.PictureBox1_PaintInit



        Me.Controls.Add(pictureBox1)

        Generate_Grid()

    End Sub
    Public Sub Generate_Grid()
        With Me
            .xLength = .BoundingRect.Width / (.GridSize + 1)
            .yLength = .BoundingRect.Height / (.GridSize + 1)
            .CurrentBrush = Brushes.Black
            For i = 0 To (.GridSize - 1)
                .CurrentX = (.xLength * (i + 1)) + 15
                For j = 0 To (.GridSize - 1)
                    .CurrentY = (.yLength * (j + 1)) + 15
                    .EachEll.X = .CurrentX - 2
                    .EachEll.Y = .CurrentY - 2
                    .boxes.Add(.EachEll)

                Next
            Next
            .pictureBox1.Refresh()
        End With
    End Sub
    Private Sub PictureBox1_PaintInit(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)

        ' Dim xLength, yLength As Decimal
        'Dim CurrentX, CurrentY As Decimal
        'Dim BoundingRect As New Rectangle(pictureBox1.Left + 15, pictureBox1.Top + 15, pictureBox1.Width - 30, pictureBox1.Height - 30)
        '    Dim EachEll As New RectangleF(0, 0, 4, 4)
        '   Dim GridSize = 50
        '  g.DrawRectangle(System.Drawing.Pens.Black, BoundingRect)
        ' xLength = BoundingRect.Width / (GridSize + 1)
        '       yLength = BoundingRect.Height / (GridSize + 1)
        '      For i = 0 To (GridSize - 1)
        '     CurrentX = (xLength * (i + 1)) + 15
        '    For j = 0 To (GridSize - 1)
        '   CurrentY = (yLength * (j + 1)) + 15
        '  EachEll.X = CurrentX - 2
        '     EachEll.Y = CurrentY - 2
        With e.Graphics
            For Each box As RectangleF In boxes
                .FillEllipse(CurrentBrush, box)
            Next
        End With
        '   Next
        '  Next
    End Sub
    Public Sub PictureBox1_HighlightPoint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim RandX = RandInt(50)
        Dim RandY = RandInt(50)
        Dim g As Graphics = e.Graphics
        Dim CurrentX, CurrentY As Decimal
        Dim xLength, yLength As Decimal
        Dim BoundingRect As New Rectangle(pictureBox1.Left + 15, pictureBox1.Top + 15, pictureBox1.Width - 30, pictureBox1.Height - 30)
        Dim EachEll As New RectangleF(0, 0, 4, 4)
        Dim GridSize = 50
        g.DrawRectangle(System.Drawing.Pens.Black, BoundingRect)
        xLength = BoundingRect.Width / (GridSize + 1)
        yLength = BoundingRect.Height / (GridSize + 1)
        CurrentX = (xLength * (RandX + 1)) + 15
        CurrentY = (yLength * (RandY + 1)) + 15
        EachEll.X = CurrentX - 2
                EachEll.Y = CurrentY - 2
        g.FillEllipse(Brushes.Red, EachEll)
    End Sub
    Private Sub Form2_Resized(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        Generate_Grid()
    End Sub
End Class