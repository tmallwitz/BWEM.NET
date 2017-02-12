Imports BroodWar.Api

Namespace BWEM
  Public Class MapDrawerParams
    Public Walkable As Boolean = False
    Public Buildable As Boolean = False
    Public Sea As Boolean = False
    Public Lake As Boolean = False
  End Class


  Public Class MapDrawerBMP
    Private ReadOnly _map As MapBase
    Const tilesize As integer = 8
    Const MiniTile2TileFactor As integer = 8

    Public Sub New(map As MapBase)
      _map = map
    End Sub

    Public Sub Draw(params As MapDrawerParams)
      Dim bmp As New Bitmap(_map.WalkSize.X *tilesize , _map.WalkSize.Y* tilesize)
      Dim gr As Graphics = Graphics.FromImage(bmp)

      Dim brushes as new Dictionary(Of String, Brush)
      brushes.Add("Sea",new SolidBrush(color.Blue))
      brushes.Add("Lake",new SolidBrush(color.Aquamarine))
      brushes.Add("Buildable",new SolidBrush(color.Coral))
      brushes.Add("Walkable",new SolidBrush(color.DarkGreen))

      For y = 0 To _map.WalkSize.Y - 1
        For x = 0 To _map.WalkSize.X - 1
          Dim tile = _map.GetMiniTile(New WalkPosition(x, y))
          if params.Walkable AndAlso tile.Walkable then gr.FillRectangle(brushes("Walkable"),x*tilesize,y*tilesize,tilesize,tilesize)
          if params.Sea AndAlso tile.Sea then gr.FillRectangle(brushes("Sea"),x*tilesize,y*tilesize,tilesize,tilesize)
          if params.Lake AndAlso tile.Lake then gr.FillRectangle(brushes("Lake"),x*tilesize,y*tilesize,tilesize,tilesize)
        Next
      Next

      'For y = 0 To _map.Size.Y - 1
      '  For x = 0 To _map.Size.X - 1
      '    Dim tile = _map.GetTile(New TilePosition(x, y))
      '    if params.Buildable AndAlso tile.Buildable then gr.FillRectangle(brushes("Buildable"), x * tilesize * MiniTile2TileFactor, y * tilesize * MiniTile2TileFactor, tilesize * MiniTile2TileFactor, tilesize * MiniTile2TileFactor)
      '  Next
      'Next


      bmp.Save("test.png")
    End Sub
  End Class
End Namespace