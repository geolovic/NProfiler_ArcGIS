Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class DisplayForm
  Dim version As String = "20150828"

  Private Sub DisplayForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'Llena las combos con los perfiles
    Dim i As Integer
    For i = 0 To profiles.GetUpperBound(0)
      ProfileCombo.Items.Add(profiles(i).Name)
    Next
    ProfileCombo.SelectedIndex = 0
    canvas.Invalidate()
  End Sub

  Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    pinta_escala(e.Graphics, New Point(60, 440), 400, 400, 5)
  End Sub

  Private Sub canvas_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles canvas.Paint
    'Paint the Grid
    pinta_grid(e.Graphics, canvas.Width, canvas.Height, New Point(0, 0))

    'Paint the selected river from the combo
    If IsNumeric(suaviza_x.Text) = False Then
      suaviza_x.Text = 0
    ElseIf CInt(suaviza_x.Text) > 10 Then
      Dim mess As String = "Please select a factor between 0 and 10"
      MsgBox(mess, vbInformation)
      suaviza_x.Text = 10
    End If


    If CInt(suaviza_x.Text) > 0 Then
      profiles(ProfileCombo.SelectedIndex).SmoothProfile(CInt(suaviza_x.Text))
    Else
      profiles(ProfileCombo.SelectedIndex).Restore()
    End If
    profiles(ProfileCombo.SelectedIndex).Draw(e.Graphics, canvas.Width, canvas.Height)
    'profiles(0).Draw(e.Graphics, canvas.Width, canvas.Height)
  End Sub

  Private Sub legend_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles legend.Paint
    pinta_parametros(e.Graphics, New Point(0, 0), profiles(ProfileCombo.SelectedIndex))
  End Sub

  Private Sub SaveImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveImage.Click

    Dim FileName As String
    Dim extension As Integer
    Try
      SaveFileDialog1.Filter = "BMP (*.bmp)|*.bmp|TIFF (*.tif)|*.tif|EMF (*.emf)|*.emf"
      SaveFileDialog1.ShowDialog()
      FileName = SaveFileDialog1.FileName
      extension = SaveFileDialog1.FilterIndex '1 BMP, 2 TIFF, 3 EMF

      If extension = 1 Or extension = 2 Then
        save_image_file(FileName)
      ElseIf extension = 3 Then
        save_vector_file(FileName)
      Else
        MsgBox("Problem saving the file, File not saved", vbCritical, "Normalized profiles")
        Exit Sub
      End If

      MsgBox("Image saved", vbInformation)
    Catch ex As Exception
      MsgBox(ex.Message, vbCritical, "N. Profiler")
    End Try

  End Sub

  Private Sub save_image_file(ByVal filename As String)
    Try
      Dim BMap As Bitmap = New Bitmap(590, 470)

      Dim Graph As Graphics = Graphics.FromImage(BMap)
      'Código para mejorar la imagen
      Graph.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
      Graph.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
      Graph.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

      Paint_to_File(Graph)

      If SaveFileDialog1.FilterIndex = 1 Then
        BMap.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp)
      ElseIf SaveFileDialog1.FilterIndex = 2 Then
        BMap.Save(filename, System.Drawing.Imaging.ImageFormat.Tiff)
      End If

      'Clean up
      BMap.Dispose()
      Graph.Dispose()
    Catch ex As Exception
      MsgBox(ex.Message, vbCritical, "Normalized Profiles")
    End Try
  End Sub

  Private Sub save_vector_file(ByVal FileName As String)
    Try
      Dim gr As Graphics = Me.CreateGraphics()

      ' Get the Graphics object's hDC.
      Dim hdc As IntPtr = gr.GetHdc()

      ' Make a metafile that can work with the hDC.
      Dim mf As New Metafile(FileName, hdc, New Rectangle(0, 0, 590, 470), MetafileFrameUnit.Pixel)

      ' Make a Graphics object to work with the metafile.
      Dim mf_gr As Graphics = Graphics.FromImage(mf)

      Paint_to_File(mf_gr)

      ' Clean up.
      'mf.Dispose()
      mf_gr.Dispose()
      gr.ReleaseHdc(hdc)
      gr.Dispose()

    Catch ex As Exception
      MsgBox(ex.Message, vbCritical, "Normalized Profiles")
    End Try
  End Sub

  Private Sub Paint_to_File(ByVal oGraph As Graphics)
    oGraph.Clear(System.Drawing.Color.White)
    Dim pos As Point = New Point(60, 10)
    pinta_grid(oGraph, 400, 400, pos)
    pos.X = 60
    pos.Y = 410
    pinta_escala(oGraph, pos, 400, 400, 5)
    profiles(ProfileCombo.SelectedIndex).Draw(oGraph, 400, 400, 60, 10)
    pos.X = 470
    pos.Y = 10
    pinta_parametros(oGraph, pos, profiles(ProfileCombo.SelectedIndex))
  End Sub

  Private Sub ExportAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportAll.Click
    Dim FileName As String
    Dim sw As System.IO.StreamWriter
    Dim Linea As String
    Try
      'Open the OpenfileDialog to select the file to export
      SaveFileDialog1.Filter = "Text file (*.txt)|*.txt"
      SaveFileDialog1.ShowDialog()
      'Get the file name and open a StreamWriter to write the file
      FileName = SaveFileDialog1.FileName
      sw = New System.IO.StreamWriter(FileName)
      sw.WriteLine("Name; Concavity; MaxC; dL")

      For i = 0 To profiles.GetUpperBound(0)
        Linea = profiles(i).Name & ";"
        Linea = Linea & FormatNumber(profiles(i).GetConcavity() * 100, 2) & ";"
        Linea = Linea & FormatNumber(profiles(i).GetMaxConcavity, 3) & ";"
        Linea = Linea & FormatNumber(profiles(i).GetMaxLength, 3)
        sw.WriteLine(Linea)
      Next
      sw.Close()
      MsgBox("Saved", vbInformation, "Normalized profiles")
    Catch ex As Exception
      Dim mess As String = "Error saving the file"
      MsgBox(mess, vbCritical, "Normalized profiles")
    End Try
  End Sub

  Private Sub ProfileCombo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfileCombo.SelectedIndexChanged
    canvas.Invalidate()
    legend.Invalidate()
  End Sub

  Private Sub suaviza_x_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles suaviza_x.KeyPress
    If Char.IsNumber(e.KeyChar) Or e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Or e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Back) Then
      If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
        e.Handled = True
        canvas.Invalidate()
      End If
    Else
      e.Handled = True
    End If


  End Sub

  Private Sub DisplayForm_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
    If e.X > Me.Width - 25 And e.Y > Me.Height - 45 Then
      MsgBox(version)
    End If
  End Sub
End Class

