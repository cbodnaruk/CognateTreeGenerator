Imports System.Text
Module Simulator

    Public CognateWeighting
    Public CurrentNode As Integer
    Public CurrentMaxNodeCreated As Integer
    Public NodeArray(3, 0) As Integer 'parentID, change rate, location x,y
    Public CognateArray(My.Settings.NumberOfCognates, 0)
    Public NumberOfAlternates(My.Settings.NumberOfCognates) As Integer
    Public CurrentLayerSize As Integer
    Public NumberOfCognates As Integer
    Public Function GenerateCognateWeighting()
        Dim InitialValue(NumberOfCognates) As Integer
        Dim CumValue(NumberOfCognates) As Integer
        MainDialogue.ProgressText.Text = "Generating Cognate Weights..."
        For i = 0 To NumberOfCognates
            InitialValue(i) = RandInt(10)
            Console.WriteLine(InitialValue(i))
        Next

        For i = 0 To NumberOfCognates
            If i = 0 Then
                CumValue(i) = InitialValue(i)
            Else
                CumValue(i) = InitialValue(i) + CumValue(i - 1)
            End If
            Console.WriteLine(CumValue(i))
        Next
        Return CumValue
    End Function
    Public Function SelectCognate(WeightPoints) As Integer
        Dim val = RandInt(WeightPoints(NumberOfCognates))
        Dim iter = 0
        MainDialogue.ProgressText.Text = "Selecting Random Cognate..."
        If val = WeightPoints(NumberOfCognates) Then
            iter = NumberOfCognates
        Else
            Do Until WeightPoints(iter) > val
                iter += 1
            Loop
        End If

        Return iter
    End Function

    Public Sub GenerateTree(TotalDaughterGoal)
        Dim PreviousLayerEnd As Integer
        Dim CognateChangeIndex As Integer
        Dim BifCounter As Integer = 0
        Dim LayerCounter As Integer = 1
        CurrentNode = 0
        CurrentMaxNodeCreated = 0
        CurrentLayerSize = 1
        MainDialogue.ProgressText.Text = "Generating Tree..."
        MainDialogue.ProgressText.Refresh()
        'Runs this for the origin node
        For i = 0 To NumberOfCognates
            CognateArray(i, 0) = 1
            NumberOfAlternates(i) = 1
        Next
        NodeArray(1, 0) = MainDialogue.TextBox2.Text

        'mark origin node
        NodeArray(2, 0) = GridChart.GridSize / 2
        NodeArray(3, 0) = GridChart.GridSize / 2
        WritePointVis(NodeArray(2, 0), NodeArray(3, 0), "o")


        CreateDaughter(CurrentNode, False)
        'Bifurcate
        CreateDaughter(CurrentNode, True)
        PreviousLayerEnd = 0
        'Now loops subsequent nodes
        Do Until CurrentLayerSize > TotalDaughterGoal
            'Layer Iteration
            CurrentLayerSize = CurrentMaxNodeCreated - PreviousLayerEnd
            MainDialogue.Label10.Text = CurrentLayerSize
            MainDialogue.Label10.Refresh()
            For i = 0 To (CurrentLayerSize - 1)
                'Node Iteration

                CurrentNode += 1
                For j = 0 To (NodeArray(1, CurrentNode) - 1)
                    CognateChangeIndex = SelectCognate(CognateWeighting)
                    CognateChange(CognateChangeIndex, CurrentNode)
                Next

                CreateDaughter(CurrentNode, False)
                If RandInt(100) <= MainDialogue.TextBox3.Text Then
                    'Bifurcate
                    CreateDaughter(CurrentNode, True)
                    BifCounter += 1
                    MainDialogue.Label6.Text = BifCounter
                    MainDialogue.Label6.Refresh()
                End If

                MainDialogue.Label4.Text = CurrentMaxNodeCreated
                MainDialogue.Label4.Refresh()
                MainDialogue.Label8.Text = CurrentNode
                MainDialogue.Label8.Refresh()

            Next
            LayerCounter += 1
            MainDialogue.Label5.Text = LayerCounter
            MainDialogue.Label5.Refresh()
            PreviousLayerEnd = CurrentNode
            CurrentLayerSize = CurrentMaxNodeCreated - PreviousLayerEnd
        Loop
        CurrentLayerSize = CurrentMaxNodeCreated - PreviousLayerEnd
        MainDialogue.Label10.Text = CurrentLayerSize
        MainDialogue.Label10.Refresh()
        'End Matter
        SaveArrays()



    End Sub
    Public Sub CognateChange(ChangeIndex, Node)
        NumberOfAlternates(ChangeIndex) += 1
        CognateArray(ChangeIndex, Node) = NumberOfAlternates(ChangeIndex)
    End Sub
    Public Sub SaveArrays()
        Dim CognateSetString As String = ""
        Dim file As System.IO.StreamWriter
        MainDialogue.ProgressText.Text = "Saving results to file..."
        MainDialogue.ProgressText.Refresh()
        System.IO.File.WriteAllText("CognateSets.txt", "") 'clears file
        file = My.Computer.FileSystem.OpenTextFileWriter("CognateSets.txt", True)
        For i = 0 To CurrentMaxNodeCreated
            For j = 0 To NumberOfCognates
                CognateSetString += CognateArray(j, i).ToString
                CognateSetString += ","
            Next
            file.WriteLine(i & "," & NodeArray(0, i) & "," & CognateSetString)
            CognateSetString = ""
        Next
        file.Close()
    End Sub
    Public Sub IsolateTerminals()
        Dim CognateSetString As String = ""
        Dim DecodeCognateAlternatives(NumberOfAlternates.Max - 1, NumberOfAlternates.Max - 1)
        Dim HighestNumAlternatives = NumberOfAlternates.Max
        Dim BlankBinarySet, CountedBinarySet As New StringBuilder(HighestNumAlternatives)
        Dim CumPos As Integer = 1
        Dim TotalCogs = 0
        'generate binaries. x/i is length, y/j is value
        For i = 0 To HighestNumAlternatives - 1
            BlankBinarySet.Append("0"c, 1)
            For j = 0 To i
                CountedBinarySet.Insert(0, BlankBinarySet.ToString)
                CountedBinarySet.Insert(j, "1").Remove(j + 1, 1)
                DecodeCognateAlternatives(i, j) = CountedBinarySet.ToString
                CountedBinarySet.Clear()
            Next
        Next
        For i = 0 To My.Settings.NumberOfCognates
            TotalCogs += NumberOfAlternates(i)
        Next
        Dim file As System.IO.StreamWriter
        MainDialogue.ProgressText.Text = "Saving Terminal Descendents to file..."
        MainDialogue.ProgressText.Refresh()
        System.IO.File.WriteAllText("TerminalCognateSets.nex", "") 'clears file
        file = My.Computer.FileSystem.OpenTextFileWriter("TerminalCognateSets.nex", True)
        file.WriteLine("#NEXUS")
        file.WriteLine("")
        file.WriteLine("begin data;")
        file.WriteLine("  dimensions ntax=" & CurrentLayerSize & " nchar=" & TotalCogs & ";")
        file.WriteLine("FORMAT DATATYPE=STANDARD MISSING=? GAP=-  SYMBOLS=""01"";")
        file.WriteLine("CHARSTATELABELS")
        'build charstatelabel table
        For i = 0 To NumberOfCognates
            If i = NumberOfCognates Then
                For j = 1 To NumberOfAlternates(i)
                    If j = NumberOfAlternates(i) Then
                        file.WriteLine(vbTab & vbTab & CumPos & " cog" & i & "_" & j & ";")
                        CumPos += 1
                    Else
                        file.WriteLine(vbTab & vbTab & CumPos & " cog" & i & "_" & j & ",")
                        CumPos += 1
                    End If

                Next
            Else
                For j = 1 To NumberOfAlternates(i)
                    file.WriteLine(vbTab & vbTab & CumPos & " cog" & i & "_" & j & ",")
                    CumPos += 1
                Next
            End If

        Next
        CumPos = 1

        file.WriteLine("MATRIX")
        For i = (CurrentMaxNodeCreated - CurrentLayerSize + 1) To CurrentMaxNodeCreated
            For j = 0 To NumberOfCognates
                CognateSetString += DecodeCognateAlternatives(NumberOfAlternates(j) - 1, CognateArray(j, i) - 1)
            Next
            file.WriteLine(i & "      " & CognateSetString)
            CognateSetString = ""
        Next
        file.WriteLine(";")
        file.WriteLine("end;")
        'list out groups of cognates
        file.WriteLine("begin assumptions;")
        For i = 0 To NumberOfCognates
            file.WriteLine(vbTab & "charset cog" & i & " = " & CumPos & "-" & CumPos + NumberOfAlternates(i) - 1 & ";")
            CumPos += NumberOfAlternates(i)
        Next
        file.WriteLine("end;")
        file.Close()
    End Sub
    Public Sub CreateNewick()
        Dim NewickArray(CurrentMaxNodeCreated) As String
        Dim CurrentParent
        Dim SplitText
        Dim InsertText
        Dim ReconnectedText = ""
        Dim NewickBound
        Dim str As String
        MainDialogue.ProgressText.Text = "Encoding Newick Tree..."
        MainDialogue.ProgressText.Refresh()

        For i = CurrentMaxNodeCreated To 1 Step -1
            CurrentParent = NodeArray(0, i)

            If NewickArray(CurrentParent) = "" Then
                If NewickArray(i) = "" Then
                    NewickArray(CurrentParent) = "(" & i & ")" & CurrentParent
                Else
                    NewickArray(CurrentParent) = "(" & NewickArray(i) & ")" & CurrentParent
                End If
            Else
                'SplitText = Split(NewickArray(CurrentParent), ")")
                If NewickArray(i) = "" Then
                    InsertText = "," & i
                Else
                    InsertText = "," & NewickArray(i)
                End If
                'SplitText(UBound(SplitText) - 1) = SplitText(UBound(SplitText) - 1) & InsertText
                'For j = 0 To UBound(SplitText) - 1
                '    ReconnectedText += SplitText(j) & ")"
                'Next
                'ReconnectedText += SplitText(UBound(SplitText))
                str = NewickArray(CurrentParent)
                SplitText = str.Substring(0, str.Length - (CurrentParent.ToString.Length + 1))
                ReconnectedText = SplitText & InsertText & ")" & CurrentParent
                NewickArray(CurrentParent) = ReconnectedText
            End If
            MainDialogue.Label8.Text = i
            MainDialogue.Label8.Refresh()
            NewickBound = UBound(NewickArray) - 1
            ReDim Preserve NewickArray(NewickBound)
        Next
        Dim file As System.IO.StreamWriter
        MainDialogue.ProgressText.Text = "Saving results to file..."
        MainDialogue.ProgressText.Refresh()
        System.IO.File.WriteAllText("Solution.tree", "") 'clears file
        file = My.Computer.FileSystem.OpenTextFileWriter("Solution.tree", True)
        file.WriteLine(NewickArray(0))
        file.Close()
        '(A,B,(C,D)E)F;
        'each parent hsa the format (C)P or (C,C)P
        'Grandparent will appear ((C)P)G
    End Sub
    Public Sub CreateDaughter(parent, bi)
        'ReDim
        Dim DaughterId As Integer = CurrentMaxNodeCreated + 1
        Dim distance, BorrowId As Integer
        ReDim Preserve NodeArray(3, DaughterId)
        ReDim Preserve CognateArray(NumberOfCognates, DaughterId)

        'load cognates from parent
        For i = 0 To NumberOfCognates
            CognateArray(i, DaughterId) = CognateArray(i, parent)
        Next

        'check distance from other living nodes
        For i = (parent + 1) To DaughterId - 1
            distance = Rnd(Math.Sqrt((Math.Abs(NodeArray(2, DaughterId) - NodeArray(2, i)) ^ 2) + (Math.Abs(NodeArray(2, DaughterId) - NodeArray(2, i)) ^ 2)))
            If distance < 6 Then
                For j = distance To 6
                    BorrowId = SelectCognate(CognateWeighting)
                    CognateArray(BorrowId, DaughterId) = CognateArray(BorrowId, i)
                Next
            End If
        Next
        'borrow forms if close enough

        'write metadata
        NodeArray(0, DaughterId) = parent
        NodeArray(1, DaughterId) = ChangeDevRate(parent)
        NodeArray(2, DaughterId) = NodeArray(2, parent)
        NodeArray(3, DaughterId) = NodeArray(3, parent)
        CurrentMaxNodeCreated = DaughterId
        If bi = True Then
            WalkNode(DaughterId, False)
        Else
            If RandInt(10) = 1 Then
                WalkNode(DaughterId, True)
            End If
        End If

    End Sub
    Public Function ChangeDevRate(parent)
        If RandInt(20) < 4 Then
            If NodeArray(1, parent) = 2 Then
                Return (NodeArray(1, parent) + 1)
            ElseIf NodeArray(1, parent) = 20 Then
                Return (NodeArray(1, parent) - 1)
            Else
                If RandInt(2) = 1 Then
                    Return (NodeArray(1, parent) + 1)
                Else
                    Return (NodeArray(1, parent) - 1)
                End If
            End If
        Else
            Return NodeArray(1, parent)
        End If
    End Function
    Public Function RandInt(max)
        Randomize()
        Return Int((max) * Rnd() + 1)
    End Function
    Public Sub WritePointVis(x As Integer, y As Integer, cont As String)
        Dim PointIndex = (((x - 1) * GridChart.GridSize) + y) - 1
        With GridChart.LabelList(PointIndex)
            If cont = "@" Then
                If .Text = cont Then
                    .Text = cont & "|"
                    .Refresh()
                ElseIf .Text = "@|" Then
                    .Text = "@||"
                    .Refresh()
                ElseIf .Text = "@||" Then
                    .Text = "@|||"
                    .Refresh()
                Else
                    .Text = cont
                    .Refresh()
                End If

            ElseIf cont = "-" And .Text = "@|" Then
                .Text = "@"
                .Refresh()
            Else
                .Text = cont
                .Refresh()
            End If
        End With
    End Sub
    Public Sub WalkNode(NodeId, RemainingNode)
        Dim ParentNode = NodeArray(0, NodeId)
        'select direction
        Dim dir = RandInt(4)
        Dim dist = RandInt(4) 'distance stepped each time

        'write new node
        If dir = 1 Then
            If NodeArray(3, NodeId) - dist > 1 Then
                NodeArray(3, NodeId) -= dist
            End If
        ElseIf dir = 2 Then
            If NodeArray(2, NodeId) + dist < GridChart.GridSize Then
                NodeArray(2, NodeId) += dist
            End If
        ElseIf dir = 3 Then
            If NodeArray(3, NodeId) + dist < GridChart.GridSize Then
                NodeArray(3, NodeId) += dist
            End If
        ElseIf dir = 4 Then
            If NodeArray(2, NodeId) - dist > 1 Then
                NodeArray(2, NodeId) -= dist
            End If
        End If

        WritePointVis(NodeArray(2, NodeId), NodeArray(3, NodeId), "@")
        'erase parent (write +)
        If RemainingNode = True Then
            WritePointVis(NodeArray(2, ParentNode), NodeArray(3, ParentNode), "-")
        End If
    End Sub

End Module
