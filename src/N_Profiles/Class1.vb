Imports System.Drawing

Public Class NormalizedProfile

  Private profile_points() As PointF
  Private profile_smooth_points() As PointF
  Private profile_concavity As Double
  Private profile_max_concavity As Double
  Private profile_max_length As Double
  Private profile_max_con_points(1) As PointF
  Private profile_name As String
  Private line_width As Single = 1.5
  Private smoothed As Boolean
  Private parameters As Boolean = True

  'Constructores [COMPLETO]
  Public Sub New(ByVal puntos As PointF(), ByVal name As String)
    create_values(puntos)
    calculate_parameters()
    If name.Length > 15 Then
      name = name.Substring(0, 15)
    End If
    profile_name = name
  End Sub

  Public Sub New(ByVal puntos As PointF())
    create_values(puntos)
    calculate_parameters()
    profile_name = "no name"
  End Sub

  'Métodos de dibujo [COMPLETO]
  Public Sub Draw(ByVal graph As System.Drawing.Graphics, ByVal width As Integer, ByVal height As Integer)
    If smoothed Then
      draw_points(profile_smooth_points, graph, width, height, 0, 0)
    Else
      draw_points(profile_points, graph, width, height, 0, 0)
    End If
  End Sub

  Public Sub Draw(ByVal graph As System.Drawing.Graphics, ByVal width As Integer, ByVal height As Integer, ByVal offsetX As Integer)
    If smoothed Then
      draw_points(profile_smooth_points, graph, width, height, offsetX, 0)
    Else
      draw_points(profile_points, graph, width, height, offsetX, 0)
    End If
  End Sub

  Public Sub Draw(ByVal graph As System.Drawing.Graphics, ByVal width As Integer, ByVal height As Integer, ByVal offsetX As Integer, ByVal offsetY As Integer)
    If smoothed Then
      draw_points(profile_smooth_points, graph, width, height, offsetX, offsetY)
    Else
      draw_points(profile_points, graph, width, height, offsetX, offsetY)
    End If
  End Sub

  'Métodos
  Public Sub SmoothProfile(ByVal n As Integer)
    n = CInt(n * 2.56)
    Dim i As Integer, j As Integer, k As Integer
    Dim Mean_value As Double
    Dim accum As Double
    Dim n_values As Integer = 0
    For i = 0 To profile_points.GetUpperBound(0)
      If i < n / 2 Then
        For j = 0 To 2 * i
          accum += profile_points(j).Y
          n_values += 1
        Next j
      ElseIf i + n / 2 > profile_points.GetUpperBound(0) Then
        For j = i - n / 2 To profile_points.GetUpperBound(0)
          accum += profile_points(j).Y
          n_values += 1
        Next j
      Else
        For j = (i - n \ 2) To (i + n \ 2)
          accum += profile_points(j).Y
          n_values += 1
        Next j
      End If

      Mean_value = accum / n_values
      ReDim Preserve profile_smooth_points(k)
      profile_smooth_points(k).X = profile_points(i).X
      profile_smooth_points(k).Y = Mean_value
      k += 1
      accum = 0
      n_values = 0
    Next i
    smoothed = True
  End Sub

  Public Sub Restore()
    smoothed = False
  End Sub

  'Propiedades
  Public Property show_parameters() As Boolean
    Set(ByVal value As Boolean)
      parameters = value
    End Set
    Get
      Return parameters
    End Get
  End Property

  Public ReadOnly Property GetConcavity() As Double
    Get
      Return profile_concavity
    End Get
  End Property

  Public ReadOnly Property GetMaxConcavity() As Double
    Get
      Return profile_max_concavity
    End Get
  End Property

  Public ReadOnly Property GetMaxLength() As Double
    Get
      Return profile_max_length
    End Get
  End Property

  Public Property Name() As String
    Set(ByVal value As String)
      If value.Length > 15 Then
        value = value.Substring(0, 15)
      End If
      profile_name = value
    End Set
    Get
      Return profile_name
    End Get
  End Property

  'Métodos y funciones internos
  Private Sub create_values(ByVal puntos As PointF())
    ReDim Preserve profile_points(puntos.GetUpperBound(0))
    Dim i As Integer
    Dim Xmin As Double = 999999999
    Dim Xmax As Double = -999999999
    Dim Ymin As Double = 999999999
    Dim Ymax As Double = -999999999

    'Get values of maximun / minimun x and y
    For i = 0 To puntos.GetUpperBound(0)
      If puntos(i).X > Xmax Then Xmax = puntos(i).X
      If puntos(i).X < Xmin Then Xmin = puntos(i).X
      If puntos(i).Y > Ymax Then Ymax = puntos(i).Y
      If puntos(i).Y < Ymin Then Ymin = puntos(i).Y
    Next
    For i = 0 To puntos.GetUpperBound(0)
      profile_points(i).X = (puntos(i).X - Xmin) / (Xmax - Xmin)
      profile_points(i).Y = (puntos(i).Y - Ymin) / (Ymax - Ymin)
    Next
  End Sub

  Private Sub calculate_parameters()
    Dim MaxC As Double = 0.0
    Dim MaxL As Double = 0.0
    Dim ProfileArea As Double = 0
    Dim NormalizedArea As Double = 0
    Dim x1 As Double, x2 As Double
    Dim y1 As Double, y2 As Double
    For i = 0 To profile_points.GetUpperBound(0) - 1
      ProfileArea += Get_area(profile_points(i), profile_points(i + 1))
      x1 = profile_points(i).X
      x2 = profile_points(i + 1).X
      y1 = 1 - x1
      y2 = 1 - x2
      NormalizedArea += Get_area(New PointF(x1, y1), New PointF(x2, y2))
      If y1 - profile_points(i).Y > MaxC Then
        MaxC = y1 - profile_points(i).Y
        MaxL = profile_points(i).X
        profile_max_con_points(0) = New PointF(x1, y1)
        profile_max_con_points(1) = New PointF(profile_points(i).X, profile_points(i).Y)
      End If
    Next
    profile_concavity = (NormalizedArea - ProfileArea) / NormalizedArea
    profile_max_concavity = MaxC
    profile_max_length = MaxL

  End Sub

  Private Function Get_area(ByVal pto1 As PointF, ByVal pto2 As PointF) As Double
    Dim Area As Double
    Dim base As Double = pto2.X - pto1.X
    Dim h As Double = pto2.Y
    Dim dh As Double = pto1.Y - pto2.Y
    If pto1.Y < pto2.Y Then
      h = pto1.Y
      dh = pto2.Y - pto1.Y
    End If
    Area = (base * h) + (base * (dh / 2))
    Return Area
  End Function

  Private Sub draw_points(ByVal points() As PointF, ByVal graph As System.Drawing.Graphics, ByVal width As Integer, ByVal height As Integer, ByVal offsetX As Integer, ByVal offsetY As Integer)
    Dim my_pen As Pen = New System.Drawing.Pen(Color.Black, line_width)
    Dim punto1 As Point, punto2 As Point
    Dim dx As Integer
    Dim dy As Integer
    dx = offsetX
    dy = offsetY
    'width = width * 0.9
    'height = height * 0.9

    For i = 0 To points.GetUpperBound(0) - 1
      punto1 = New Point(points(i).X * width + dx, height - points(i).Y * height + dy)
      punto2 = New Point(points(i + 1).X * width + dx, height - points(i + 1).Y * height + dy)
      graph.DrawLine(my_pen, punto1, punto2)
    Next

    If parameters Then
      punto1 = New Point(0 + dx, 0 + dy)
      punto2 = New Point(width + dx, height + dy)
      graph.DrawLine(New System.Drawing.Pen(Color.Red, 1.5), punto1, punto2)
      If profile_concavity > 0 Then
        punto1 = New Point(profile_max_con_points(0).X * width + dx, height - profile_max_con_points(0).Y * height + dy)
        punto2 = New Point(profile_max_con_points(1).X * width + dx, height - profile_max_con_points(1).Y * height + dy)
        graph.DrawLine(New System.Drawing.Pen(Color.Blue, 1.5), punto1, punto2)
        punto1.Y = punto1.Y + (punto2.Y - punto1.Y) / 2
        punto1.X = punto1.X + 4
        graph.DrawString("MaxC", New System.Drawing.Font("Arial", 10), Brushes.Blue, punto1)
      End If

    End If
  End Sub

End Class
