Imports BroodWar.Api
Imports System.Drawing

Namespace BWEM

  Public Class DrawHelper
    Public Shared Sub DiagonalCrossMap(topLeft As Position, bottomRight As Position, c As System.Drawing.Color)
      Try
        Game.DrawLine(topLeft, bottomRight, c)
        'Game.DrawLine(New Position(bottomRight.X, topLeft.Y), New Position(topLeft.X, bottomRight.Y), c)

      Catch ex As Exception

      End Try
    End Sub


  End Class
End Namespace