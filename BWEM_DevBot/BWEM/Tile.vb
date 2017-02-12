Namespace BWEM
  Public Class Tile
    Inherits Markable(Of Tile)

    Public ReadOnly Property Buildable As Boolean
      Get
        Return _buildable
      End Get
    End Property

    Public ReadOnly Property AreaId As Integer
      Get
        Return _areaID
      End Get
    End Property

    Public ReadOnly Property MinAltitude As Integer
      Get
        Return _minAltitude
      End Get
    End Property

    Public ReadOnly Property Walkable As Boolean
      Get
        Return Not _areaID.Equals(0)
      End Get
    End Property

    Public ReadOnly Property Terrain As Boolean
      Get
        Return Walkable
      End Get
    End Property

    Public ReadOnly Property GroundHeight As Integer
      Get
        Return _groundHeight
      End Get
    End Property

    Public ReadOnly Property Doodad As Boolean
      Get
        Return _doodad
      End Get
    End Property

    Public ReadOnly Property Neutral As Neutral
      Get
        Return _neutral
      End Get
    End Property

    Friend Sub SetBuildable()
      _buildable = True
    End Sub

    Friend Sub SetGroundHeight(h As Integer)
      Debug.Assert(0 <= h AndAlso h <= 2)
      _groundHeight = h
    End Sub

    Friend Sub SetDoodad()
      _doodad = True
    End Sub

    Friend Sub AddNeutral(pNeutral As Neutral)
      Debug.Assert(_neutral Is Nothing And pNeutral IsNot Nothing)
      _neutral = pNeutral
    End Sub

    Friend Sub SetAreaID(id As Integer)
      Debug.Assert(id = 1 OrElse (_areaID = 0 AndAlso id <> 0))
      _areaID = id
    End Sub

    Friend Sub ResetAreaId()
      _areaID = 0
    End Sub

    Friend Sub SetMinAltitude(a As Integer)
      Debug.Assert(a >= 0)
      _minAltitude = a
    End Sub

    Friend Sub RemoveNeutral(pNeutral As Neutral)
      Debug.Assert(pNeutral IsNot Nothing AndAlso pNeutral.Equals(_neutral))
      _neutral = Nothing
    End Sub

    Friend Function InternalData() As Integer
      Return _internalData
    End Function

    Friend Sub SetInternalData(data As Integer)
      _internalData = data
    End Sub

    Private _buildable As Boolean = False
    Private _groundHeight As Integer = 0
    Private _doodad As Boolean = False
    Private _minAltitude As Integer
    Private _areaID As Integer = 0
    Private _internalData As Integer = 0
    Private _neutral As Neutral
  End Class
End Namespace