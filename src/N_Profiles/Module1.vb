Imports System.Drawing
Imports System.IO
Module Module1
    'Array with profiles
    Public profiles() As NormalizedProfile

  Public Function GetDistance(ByVal p1 As PointF, ByVal p2 As PointF) As Double
    Dim dist As Double
    dist = Math.Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)
    Return dist
  End Function

  Public Function rescale(ByVal x() As Double, ByVal y() As Double, ByVal n As Integer) As PointF()
    Dim nValues() As Integer
    Dim accValues() As Double
    Dim i As Integer, k As Integer
    Dim valX As Double, valY As Double
    Dim Ymin As Double = 999999
    Dim Ymax As Double = -999999
    Dim Xmin As Double = 999999
    Dim Xmax As Double = -999999
    Dim outPoints() As PointF

    ReDim nValues(n)
    ReDim accValues(n)
    ReDim outPoints(n)

    'Obtiene valores máximo y mínimo de la matriz con los valores de Y
    For i = 0 To y.GetUpperBound(0)
      If y(i) > Ymax Then Ymax = y(i)
      If y(i) < Ymin Then Ymin = y(i)
      If x(i) > Xmax Then Xmax = x(i)
      If x(i) < Xmin Then Xmin = x(i)
    Next

    For i = 0 To y.GetUpperBound(0)
      k = ((x(i) - Xmin) / (Xmax - Xmin)) * n
      nValues(k) += 1
      accValues(k) += y(i)
    Next

    For i = 0 To n
      If nValues(i) > 0 Then
        valY = accValues(i) / nValues(i)
        valX = i * (Xmax / n)
        outPoints(i) = New PointF(valX, valY)
      Else
        valY = -99999
        valX = i * (Xmax / n)
        outPoints(i) = New PointF(valX, valY)
      End If

    Next

    'Ensure that the array contains all the points
    Dim auxValY As Double, dy As Double
    Dim j As Integer
    For i = 0 To n
      If outPoints(i).Y = -99999 Then
        'busca siguiente elemento de la matriz con valor
        j = i + 1
        auxValY = outPoints(j).Y
        Do Until auxValY <> -99999
          j = j + 1
          auxValY = outPoints(j).Y
        Loop
        dy = (auxValY - outPoints(i - 1).Y) * ((outPoints(i).X - outPoints(i - 1).X) / (outPoints(j).X - outPoints(i - 1).X))
        outPoints(i).Y = outPoints(i - 1).Y + dy
      End If
    Next i

    Return outPoints
  End Function

  Public Sub pinta_grid(ByVal graph As System.Drawing.Graphics, ByVal width As Integer, ByVal height As Integer, ByVal upper_left_corner As Point)
    'pinta Grid (10 líneas)
    Dim mypen As System.Drawing.Pen
    Dim incX As Integer
    Dim incY As Integer
    Dim y As Integer, x As Integer
    Dim p1 As Point, p2 As Point, p3 As Point, p4 As Point
    upper_left_corner.X -= 1
    upper_left_corner.Y -= 1
    mypen = New System.Drawing.Pen(Color.LightGray, 1)
    mypen.DashPattern = New Single() {4.0F, 2.0F, 1.0F, 3.0F}
    incX = width \ 5
    incY = height \ 5
    x = upper_left_corner.X
    y = upper_left_corner.Y
    For i = 0 To 5
      p1 = New System.Drawing.Point(upper_left_corner.X, y)
      p2 = New System.Drawing.Point(upper_left_corner.X + width, y)
      graph.DrawLine(mypen, p1, p2)
      p1 = New System.Drawing.Point(x, upper_left_corner.Y)
      p2 = New System.Drawing.Point(x, upper_left_corner.Y + height)
      graph.DrawLine(mypen, p1, p2)
      x += incX
      y += incY
    Next
    p1 = New Point(upper_left_corner.X, upper_left_corner.Y)
    p2 = New Point(upper_left_corner.X + width, upper_left_corner.Y)
    p4 = New Point(upper_left_corner.X, upper_left_corner.Y + height)
    p3 = New Point(upper_left_corner.X + width, upper_left_corner.Y + height)
    mypen = New System.Drawing.Pen(Color.Black, 1)
    graph.DrawLine(mypen, p1, p2)
    graph.DrawLine(mypen, p2, p3)
    graph.DrawLine(mypen, p3, p4)
    graph.DrawLine(mypen, p4, p1)


  End Sub

  Public Sub pinta_escala(ByVal graph As System.Drawing.Graphics, ByVal lower_left_corner As Point, ByVal width As Integer, ByVal height As Integer, ByVal d_Tics As Integer)
    'Variables para los ticks verticales y horizontales
    Dim horizontal_tic_1 As PointF
    Dim horizontal_tic_2 As PointF
    Dim vertical_tic_1 As PointF
    Dim vertical_tic_2 As PointF

    'Variables para las etiquetas
    Dim drawFont As System.Drawing.Font = New Font("Arial Narrow", 10)
    Dim drawBrush As System.Drawing.Brush = New SolidBrush(Color.Black)
    Dim label As String = "0.0"

    Dim i As Integer

    Dim horizontal_label As PointF = New PointF(lower_left_corner.X - drawFont.GetHeight() / 2 - 2, lower_left_corner.Y + 7)
    Dim vertical_label As PointF = New PointF(lower_left_corner.X - 30, lower_left_corner.Y - drawFont.GetHeight() / 2 - 2)
    Dim valorX As Single = horizontal_label.X
    Dim valorY As Single = vertical_label.Y

    vertical_tic_1 = New PointF(lower_left_corner.X - d_Tics, lower_left_corner.Y - 1)
    vertical_tic_2 = New PointF(lower_left_corner.X, lower_left_corner.Y - 1)
    horizontal_tic_1 = New PointF(lower_left_corner.X, lower_left_corner.Y + d_Tics)
    horizontal_tic_2 = New PointF(lower_left_corner.X, lower_left_corner.Y)
    Dim auxX As Single = horizontal_tic_1.X
    Dim auxY As Single = vertical_tic_1.Y

    For i = 0 To 5
      label = CStr((i * 2) / 10)
      If label = "0" Then label = "0.0"
      If label = "1" Then label = "1.0"
      vertical_label.Y = valorY - (height / 5) * i
      horizontal_label.X = valorX + (width / 5) * i

      vertical_tic_1.Y = auxY - (height / 5) * i
      vertical_tic_2.Y = auxY - (height / 5) * i
      horizontal_tic_1.X = auxX + (height / 5) * i
      horizontal_tic_2.X = auxX + (height / 5) * i

      graph.DrawLine(Pens.Black, horizontal_tic_1, horizontal_tic_2)
      graph.DrawLine(Pens.Black, vertical_tic_1, vertical_tic_2)
      graph.DrawString(label, drawFont, drawBrush, vertical_label)
      graph.DrawString(label, drawFont, drawBrush, horizontal_label)
    Next

    drawFont = New System.Drawing.Font("Arial", 10, FontStyle.Bold)
    Dim StringSize As New SizeF
    StringSize = graph.MeasureString("Normalized distance", drawFont)

    Dim pos As PointF = New PointF(lower_left_corner.X + (width / 2 - StringSize.Width / 2), lower_left_corner.Y + 24)
    graph.DrawString("Normalized distance", drawFont, drawBrush, pos)

    StringSize = graph.MeasureString("Normalized elevation", drawFont)
    pos = New Point(lower_left_corner.X - 50, lower_left_corner.Y - height / 2 + StringSize.Width / 2)

    graph.TranslateTransform(pos.X, pos.Y)
    graph.RotateTransform(-90)
    graph.DrawString("Normalized elevation", drawFont, drawBrush, New Point(0, 0))
    graph.ResetTransform()

  End Sub

  Public Sub pinta_parametros(ByVal graph As System.Drawing.Graphics, ByVal upper_left_position As Point, ByVal perfil As NormalizedProfile)
    Dim cadena As String
    Dim drawFont As System.Drawing.Font = New System.Drawing.Font("Arial", 10, FontStyle.Bold)
    Dim drawBrush As System.Drawing.Brush = New SolidBrush(Color.Black)
    cadena = perfil.Name
    graph.DrawString(cadena, drawFont, drawBrush, upper_left_position)
    upper_left_position.Y = upper_left_position.Y + 20
    drawFont = New System.Drawing.Font("Arial", 10)
    cadena = "Concavity: " & FormatNumber(perfil.GetConcavity() * 100, 2) & "%"
    graph.DrawString(cadena, drawFont, drawBrush, upper_left_position)
    upper_left_position.Y = upper_left_position.Y + 20
    If perfil.GetConcavity > 0 Then
      cadena = "MaxC: " & FormatNumber(perfil.GetMaxConcavity, 3)
      graph.DrawString(cadena, drawFont, drawBrush, upper_left_position)
      upper_left_position.Y = upper_left_position.Y + 20
      cadena = "dL: " & FormatNumber(perfil.GetMaxLength, 3)
      graph.DrawString(cadena, drawFont, drawBrush, upper_left_position)
    Else
      cadena = "MaxC: n/a"
      graph.DrawString(cadena, drawFont, drawBrush, upper_left_position)
      upper_left_position.Y = upper_left_position.Y + 20
      cadena = "dL: n/a"
      graph.DrawString(cadena, drawFont, drawBrush, upper_left_position)
    End If

  End Sub

  ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
  ''@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
  ''Old Debugging stuff

  '  Public Sub cargaDatos()
  '      Dim X() As Double = {}
  '      Dim Y() As Double = {}
  '      Dim minX As Double, minY As Double
  '      Dim maxX As Double, maxY As Double
  '      Dim in_file As StreamReader
  '      Dim files() As String
  '      Dim names() As String
  '      Dim linea As String
  '      Dim valores() As String
  '      Dim i As Integer
  '      Dim k As Integer
  '      Dim nPerfiles As Integer
  '      Dim puntos() As PointF
  '      minX = 99999999
  '      minY = 99999999
  '      maxX = -999999999
  '      maxY = -999999999

  '      nPerfiles = CInt(InputBox("Introduce el numero de perfiles"))

  '      ReDim files(nPerfiles - 1)
  '      ReDim names(nPerfiles - 1)
  '      ReDim profiles(nPerfiles - 1)


  '      For i = 0 To nPerfiles - 1
  '          files(i) = "D:\TEMP\perfil" & CStr(i + 1) & ".txt"
  '          names(i) = "River nº " & CStr(i + 1)
  '      Next i

  '      For i = 0 To nPerfiles - 1
  '          k = 0
  '          in_file = New StreamReader(files(i))
  '          linea = in_file.ReadLine()
  '          Do Until linea Is Nothing
  '              valores = linea.Split(";")
  '              ReDim Preserve X(k)
  '              ReDim Preserve Y(k)
  '              ReDim Preserve puntos(k)
  '              X(k) = CType(valores(0), Double)
  '              Y(k) = CType(valores(1), Double)
  '              puntos(k) = New Point(X(k), Y(k))

  '              If X(k) > maxX Then maxX = X(k)
  '              If X(k) < minX Then minX = X(k)
  '              If Y(k) > maxY Then maxY = Y(k)
  '              If Y(k) < minY Then minY = Y(k)
  '              k += 1
  '              linea = in_file.ReadLine()
  '          Loop
  '          in_file.Close()
  '          If X.GetUpperBound(0) > 255 Then
  '              puntos = rescale(X, Y, 255)
  '          End If
  '          profiles(i) = New NormalizedProfile(puntos, names(i))
  '      Next

  '  End Sub

End Module
