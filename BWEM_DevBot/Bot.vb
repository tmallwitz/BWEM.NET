Imports System.Threading
Imports BroodWar.Api.Client
Imports BroodWar.Api
Imports BroodWar.Api.Enum
Imports BWEM_DevBot.BWEM

Class Bot
  Private ReadOnly _map As Map = Map.Instance

  Public Sub Run()
    Reconnect()
    While True
      While Not Game.IsInGame
        Client.Update()
        If Not Client.IsConnected Then
          Reconnect()
        End If
      End While
      Game.EnableFlag(Flag.UserInput)
      Game.EnableFlag(Flag.CompleteMapInformation)
      While Game.IsInGame


        For Each gameEvent In Game.Events
          Select Case gameEvent.Type
            Case EventType.MatchStart
              _map.Initialize()
              Dim drawer as new MapDrawerBMP(_map)
              drawer.Draw(new MapDrawerParams() With {.Sea = true,.Lake=true, .Buildable = True, .Walkable = true})
            Case EventType.MatchFrame
              'draw()
          End Select
        Next



        Client.Update()
      End While
    End While
  End Sub

  Private Sub draw()

    Dim c As Integer = 0
    For y = 0 To _map.WalkSize.Y - 1
      For x = 0 To _map.WalkSize.X - 1
        Dim t As New WalkPosition(x, y)
        Dim t1 As New WalkPosition(x + 1, y + 1)



        'If _map.GetMiniTile(t).Sea Then
        '  DrawHelper.DiagonalCrossMap(Position.Rescale(t), Position.Rescale(t1), Color.Blue)
        'End If
        If _map.GetMiniTile(t).Lake() Then
          c = c + 1
          If c > 5000 Then
            Exit For
          End If
          DrawHelper.DiagonalCrossMap(Position.Rescale(t), Position.Rescale(t1), Color.LightBlue)
        End If
      Next
    Next
  End Sub
  Private Sub Reconnect()
    While Not Client.Connect()
      Thread.Sleep(100)
    End While
  End Sub

End Class
