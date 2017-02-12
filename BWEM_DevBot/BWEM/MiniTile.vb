Namespace BWEM
  Public Class MiniTile
    Public ReadOnly Property Walkable As Boolean
      Get
        Return Not _areaId.Equals(0)
      End Get
    End Property

    Public ReadOnly Property Altitude As Integer
      Get
        Return _altitude
      End Get
    End Property

    Public ReadOnly Property Sea As Boolean
      Get
        Return _altitude = 0
      End Get
    End Property

    Public ReadOnly Property Lake As Boolean
      Get
        Return _altitude <> 0 AndAlso Not Walkable
      End Get
    End Property

    Public ReadOnly Property Terrain As Boolean
      Get
        Return Walkable
      End Get
    End Property

    Public ReadOnly Property AreaID As Integer
      Get
        Return _areaId
      End Get
    End Property

    Friend Sub SetWalkable(isWalkable As Boolean)
      _areaId = CInt(IIf(isWalkable, -1, 0))
      _altitude = CInt(IIf(isWalkable, -1, 1))
    End Sub

    Friend Function SeaOrLake() As Boolean
      Return _altitude = 1
    End Function

    Friend Sub SetSea()
      Debug.Assert(Not Walkable AndAlso SeaOrLake())
      _altitude = 0
    End Sub

    Friend Sub SetLake()
      Debug.Assert(Not Walkable AndAlso Sea())
      _altitude = -1
    End Sub

    Friend Function AltitudeMissing() As Boolean
      Return _altitude = -1
    End Function

    Friend Sub SetAltitude(a As Integer)
      Debug.Assert(AltitudeMissing() AndAlso a > 0)
      _altitude = a
    End Sub

    Friend Function AreaIdMissing() As Boolean
      Return _areaId = -1
    End Function

    Friend Sub SetAreaId(id As Integer)
      Debug.Assert(AreaIdMissing() AndAlso id >= 1)
      _areaId = id
    End Sub

    Friend Sub ReplaceAreaId(id As Integer)
      Debug.Assert((_areaId > 0 AndAlso id >= 1) OrElse (id <= -2 AndAlso id <> _areaId))
      _areaId = id
    End Sub

    Friend Sub SetBlocked()
      Debug.Assert(AreaIdMissing())
      _areaId = blockingCP
    End Sub

    Friend Function Blocked() As Boolean
      Return _areaId = blockingCP
    End Function

    Friend Sub ReplaceBlockedAreaId(id As Integer)
      Debug.Assert(_areaId = blockingCP AndAlso id >= 1)
      _areaId = id
    End Sub


    Private _areaId As Integer = -1
    Private _altitude As Integer = -1
    Private ReadOnly blockingCP As Integer = Integer.MinValue
  End Class
End NameSpace