Imports BroodWar.Api

Namespace BWEM

  Public MustInherit Class MapBase
    ' Returns the size of the Map  
    Public Property Size() As TilePosition
    Protected Property SizeInt() As Integer

    'Returns the size of the Map  as int
    Public Property WalkSize() As WalkPosition
    Protected Property WalkSizeInt() As Integer

    Public ReadOnly Property Tiles As List(Of Tile) = New List(Of Tile)
    Public ReadOnly Property MiniTiles As List(Of MiniTile) = New List(Of MiniTile)

    Public Property Center As Position

    Public Overloads Function Valid(p As TilePosition) As Boolean
      Return 0 <= p.X AndAlso p.X < Size.X AndAlso 0 <= p.Y AndAlso p.Y < Size.Y
    End Function
    Public Overloads Function Valid(p As WalkPosition) As Boolean
      Return 0 <= p.X AndAlso p.X < WalkSize.X AndAlso 0 <= p.Y AndAlso p.Y < WalkSize.Y
    End Function
    Public Overloads Function Valid(p As Position) As Boolean
      Return Valid(TilePosition.Rescale(p))
    End Function

    Public Function GetTile(p As TilePosition, Optional check As CheckMode = CheckMode.NoCheck) As Tile
      Debug.Assert(check = CheckMode.NoCheck OrElse Valid(p), "GetTile: Tilepos not valid")
      Return Tiles(Size.X * p.Y + p.X)
    End Function

    Public Function GetMiniTile(p As WalkPosition, Optional check As CheckMode = CheckMode.NoCheck) As MiniTile
      Debug.Assert(check = CheckMode.NoCheck OrElse Valid(p), "GetTile: Tilepos not valid")
      Return MiniTiles(WalkSize.X * p.Y + p.X)
    End Function



  End Class


End Namespace
