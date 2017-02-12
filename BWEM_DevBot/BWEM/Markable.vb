Namespace BWEM
  Public Class Markable(Of T)
    Private _lastMark As Integer
    Private Shared Property CurrentMark As Integer = 0

    Public Function Marked() As Boolean
      Return _lastMark = CurrentMark
    End Function

    Public Sub SetMarked()
      _lastMark = CurrentMark
    End Sub

    Public Sub SetUnmarked()
      _lastMark = CurrentMark - 1
    End Sub

    Public Shared Sub UnmarkAll()
      CurrentMark += 1
    End Sub
  End Class
End Namespace
