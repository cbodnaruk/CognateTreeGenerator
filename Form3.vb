Public Class Form3
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        AddHandler Form2.pictureBox1.Paint, AddressOf Form2.PictureBox1_HighlightPoint
        Form2.pictureBox1.Refresh()
     
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With Form2
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
End Class