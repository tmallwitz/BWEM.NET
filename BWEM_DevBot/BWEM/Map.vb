Imports BroodWar.Api

Namespace BWEM
  Public Class Map
    Inherits MapBase

    'These constants control how to decide between Seas and Lakes.
    Const LakeMaxMiniTiles As Integer = 300
    Const LakeMaxWidthInMiniTiles As Integer = 8 * 4

    ' At least area_min_miniTiles connected MiniTiles are necessary for an Area to be created.
    Const AreaMinMiniTiles As Integer = 64

    Const MaxTilesBetweenCommandCenterAndRessources As Integer = 10
    Const MinTilesBetweenBases As Integer = 10

    Const MaxTilesBetweenStartingLocationAndItsAssignedBase As Integer = 3


    Public Shared ReadOnly Property Instance As Map = New Map()

    Private Sub New()

    End Sub
    Private ReadOnly Property StartingLocations As List(Of TilePosition) = New List(Of TilePosition)

    Public Sub Initialize()
      Size = New TilePosition(Game.MapWidth, Game.MapHeight)
      SizeInt = Size.X * Size.Y
      For i = 0 To SizeInt - 1
        Tiles.Add(New Tile)
      Next
      WalkSize = WalkPosition.Rescale(Size)
      WalkSizeInt = WalkSize.X * WalkSize.Y
      For i = 0 To WalkSizeInt - 1
        MiniTiles.Add(New MiniTile())
      Next
      Center = Position.Rescale(Size) / 2
      For Each t As TilePosition In Game.StartLocations
        StartingLocations.Add(New TilePosition(t))
      Next

      LoadData()
      DecideSeasOrLakes()
      'InitializeNeutrals()
      'ComputeAltitude()
      'ProcessBlockingNeutrals()
      'ComputeAreas()
      'GetGraph().CreateChokePoints()
      'GetGraph().ComputeChokePointDistanceMatrix()
      'GetGraph().CollectInformation()
      'GetGraph().CreateBases()
    End Sub

    Private Sub LoadData()
      ' Mark unwalkable minitiles (minitiles are walkable by default)
      For y = 0 To WalkSize.Y - 1
        For x = 0 To WalkSize.X - 1
          If Not Game.IsWalkable(x, y) Then
            For dy = -1 To 1
              For dx = -1 To 1
                Dim walkpos As New WalkPosition(x + dx, y + dy)
                If Valid(walkpos) Then
                  GetMiniTile(walkpos).SetWalkable(False)
                End If
              Next
            Next
          End If
        Next
      Next

      'Mark buildable tiles (tiles are unbuildable by default)
      For y = 0 To Size.Y - 1
        For x = 0 To Size.X - 1
          Dim t As New TilePosition(x, y)
          If Game.IsBuildable(t, False) Then
            GetTile(t).SetBuildable()
            For dy = -1 To 1
              For dx = -1 To 1
                Dim walkpos = New WalkPosition(dx, dy) + WalkPosition.Rescale(t)
                If Valid(walkpos) Then
                  GetMiniTile(walkpos).SetWalkable(True)
                End If
              Next
            Next
          End If
        Next
      Next
    End Sub

    Private Sub DecideSeasOrLakes()
      Dim _
        toSearchDelta As _
          New List(Of WalkPosition) _
          From {New WalkPosition(1, 0), New WalkPosition(0, 1), New WalkPosition(-1, 0), New WalkPosition(0, -1)}
      For y = 0 To WalkSize.Y - 1
        For x = 0 To WalkSize.X - 1
          Dim origin = New WalkPosition(x, y)
          Dim originTile = GetMiniTile(origin)
          If originTile.SeaOrLake() Then
            Dim toSearch As New Queue(Of WalkPosition)
            Dim seaExtent As New List(Of MiniTile)
            Dim topLeftX As Integer = origin.X
            Dim topLeftY As Integer = origin.Y
            Dim bottomRightX As Integer = origin.X
            Dim bottomRightY As Integer = origin.Y
            toSearch.Enqueue(origin)
            seaExtent.Add(originTile)
            originTile.SetSea()
            While Not toSearch.Any()
              Dim current As WalkPosition = toSearch.Dequeue()
              If (current.X < topLeftX) Then topLeftX = current.X
              If (current.Y < topLeftY) Then topLeftY = current.Y
              If (current.X > bottomRightX) Then bottomRightX = current.X
              If (current.Y > bottomRightY) Then bottomRightY = current.Y
              For Each delta In toSearchDelta
                Dim searchNext As WalkPosition = current + delta
                If Valid(searchNext) Then
                  Dim searchNextTile As MiniTile = GetMiniTile(searchNext)
                  If searchNextTile.SeaOrLake() Then
                    toSearch.Enqueue(searchNext)
                    If seaExtent.Count <= LakeMaxMiniTiles Then seaExtent.Add(searchNextTile)
                    searchNextTile.SetSea()
                  End If
                End If
              Next


            End While
            If seaExtent.Count <= LakeMaxMiniTiles _
               AndAlso bottomRightX - topLeftX <= LakeMaxWidthInMiniTiles _
               AndAlso bottomRightY - topLeftY <= LakeMaxWidthInMiniTiles _
               AndAlso topLeftX >= 2 _
               AndAlso topLeftY >= 2 _
               AndAlso bottomRightX < WalkSize.X - 2 _
               AndAlso bottomRightY < WalkSize.Y - 2 Then
              seaExtent.ForEach(Sub(tile) tile.SetLake())
            End If

          End If
        Next
      Next
    End Sub
  End Class
End Namespace


