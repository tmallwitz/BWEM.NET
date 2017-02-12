Imports System.Runtime.CompilerServices
Imports BroodWar.Api

Namespace BWEM
  Module Extensions

    <Extension()>
    Public Function TopLeft(ByVal wp As WalkPosition) As Position
      Return Position.Rescale(wp)
    End Function

    <Extension()>
    Public Function BottomRight(ByVal wp As WalkPosition) As Position
      Return Position.Rescale(wp + New WalkPosition(1, 1))
    End Function



  End Module
End Namespace