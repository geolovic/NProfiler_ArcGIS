Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.GeoAnalyst
Imports ESRI.ArcGIS.DataSourcesRaster
Imports ESRI.ArcGIS.Analyst3D
Imports System.IO
Imports System.Drawing

Public Class SelDataForm
  Dim pMxDoc As IMxDocument
  Dim pMap As IMap
  Dim pEnumLayers As IEnumLayer
  Dim pLayer As ILayer
  Dim pFLayer As IFeatureLayer
  Dim pRLayer As IRasterLayer
  Dim pFClass As IFeatureClass
  Dim pRaster As IRaster

  Private Sub SelDataForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try

      'Reference variables
      pMxDoc = TryCast(My.ArcMap.Document, IMxDocument)
      pMap = TryCast(pMxDoc.FocusMap, IMap)

      If pMap.LayerCount <= 0 Then
        MsgBox("No layers in the map!")
        Me.Close()
      End If

      pEnumLayers = TryCast(pMap.Layers, IEnumLayer)
      pLayer = TryCast(pEnumLayers.Next, ILayer)

      'Fill CmbL with polygon layers and CmbR with raster layers
      Do Until pLayer Is Nothing
        If TypeOf pLayer Is IFeatureLayer Then
          pFLayer = TryCast(pLayer, IFeatureLayer)
          If pFLayer.FeatureClass.ShapeType = esriGeometryType.esriGeometryPolyline Then
            CmbL.Items.Add(pLayer.Name)
          End If
        ElseIf TypeOf pLayer Is IRasterLayer Then
          CmbR.Items.Add(pLayer.Name)
        End If
        pLayer = pEnumLayers.Next
      Loop
    Catch ex As Exception
      MsgBox(ex.Message, vbExclamation, "Profiler AddIn")
    End Try
  End Sub

  Private Sub LoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadData.Click
    Dim pFCursor As IFeatureCursor
    Dim pFeat As IFeature
    Dim pGeometry As IGeometry
    Dim RSurface As IRasterSurface
    Dim pSurface As ISurface
    Dim pPointColl As IPointCollection
    Dim pto_1 As PointF
    Dim pto_2 As PointF
    Dim puntos() As PointF
    Dim Xvalues() As Double
    Dim Yvalues() As Double
    Dim AccDist As Double
    Dim name As String
    Dim i As Integer, k As Integer

    Try
      Me.Cursor = Windows.Forms.Cursors.WaitCursor
      'Ensure that both combos have a layer selected
      If CmbR.Text = "" Or CmbL.Text = "" Then
        MsgBox("Please select valid layers")
        Exit Sub
      End If

      'Identify layers in combos and reference pRaster y pFClass
      pEnumLayers = TryCast(pMap.Layers, IEnumLayer)
      pLayer = TryCast(pEnumLayers.Next, ILayer)
      Do Until pLayer Is Nothing
        If pLayer.Name = CmbL.Text Then
          pFLayer = TryCast(pLayer, IFeatureLayer)
          pFClass = TryCast(pFLayer.FeatureClass, IFeatureClass)
        ElseIf pLayer.Name = CmbR.Text Then
          pRLayer = TryCast(pLayer, IRasterLayer)
          pRaster = TryCast(pRLayer.Raster, IRaster)
        End If
        pLayer = pEnumLayers.Next
      Loop

      'Create a QueryFilter is the option of "Only Selected" is checked
      'If the option is not checked, the filter is set to Nothing
      Dim qryString As String
      Dim pQF As IQueryFilter
      Dim pFeatSel As IFeatureSelection
      Dim pSelSet As ISelectionSet
      Dim pEnumIDs As IEnumIDs
      Dim pID As Long
      pQF = New QueryFilter

      If CheckSelected.Checked Then
        pFeatSel = pFLayer
        pSelSet = pFeatSel.SelectionSet
        pEnumIDs = pSelSet.IDs
        If pSelSet.Count = 0 Then
          MsgBox("There are not selected features in the basin feature class")
          Exit Sub
        End If
        pID = pEnumIDs.Next
        qryString = pFClass.OIDFieldName & Space(1) & "in" & Space(1) & "("
        Do Until pID = -1
          qryString = qryString & Str(pID) & Space(1) & ","
          pID = pEnumIDs.Next
        Loop
        qryString = qryString.Substring(0, Len(qryString) - 1) & ")"
        pQF.WhereClause = qryString
      Else
        pQF = Nothing
      End If

      'Adapt the size of profiles to store all the lines
      ReDim profiles(pFClass.FeatureCount(pQF) - 1)

      'Store profile names (if present)
      Dim nameInd As Integer
      nameInd = pFClass.FindField("Name")

      'Set the Surface for the operations
      RSurface = New RasterSurface
      RSurface.PutRaster(pRaster, 0)
      pSurface = TryCast(RSurface, ISurface)

      ' Start the iteration over the rivers
      pFCursor = pFClass.Update(pQF, False)
      pFeat = pFCursor.NextFeature
      k = 0
      Do Until pFeat Is Nothing

        If nameInd > 0 Then
          name = pFeat.Value(nameInd)
        Else
          name = "River " & CStr(k + 1)
        End If

        pGeometry = pFeat.ShapeCopy
        pSurface.GetProfile(pGeometry, pGeometry)
        pPointColl = TryCast(pGeometry, IPointCollection)

        AccDist = 0
        ReDim Xvalues(pPointColl.PointCount - 1)
        ReDim Yvalues(pPointColl.PointCount - 1)
        Xvalues(0) = AccDist
        Yvalues(0) = pPointColl.Point(0).Z

        For i = 1 To pPointColl.PointCount - 1
          pto_1 = New PointF(CSng(pPointColl.Point(i - 1).X), CSng(pPointColl.Point(i - 1).Y))
          pto_2 = New PointF(CSng(pPointColl.Point(i).X), CSng(pPointColl.Point(i).Y))
          AccDist = AccDist + get_distance(pto_1, pto_2)
          Xvalues(i) = AccDist
          Yvalues(i) = pPointColl.Point(i).Z
        Next

        If RemoveSpikes.Checked = True Then
          For i = 0 To Xvalues.GetUpperBound(0) - 1
            If Yvalues(i + 1) > Yvalues(i) Then Yvalues(i + 1) = Yvalues(i)
          Next
        End If

        puntos = rescale(Xvalues, Yvalues, 255)
        profiles(k) = New NormalizedProfile(puntos, name)
        k += 1
        pFeat = pFCursor.NextFeature

      Loop
      Me.Cursor = Windows.Forms.Cursors.Default
      Dim DispForm As DisplayForm = New DisplayForm
      DispForm.Show()
      Me.Close()

    Catch ex As Exception
      MsgBox(ex.Message, vbCritical, "Profiler")
    End Try
  End Sub

  Public Function get_distance(ByVal p1 As PointF, ByVal p2 As PointF) As Double
    Return Math.Sqrt((p2.X - p1.X) ^ 2 + (p2.Y - p1.Y) ^ 2)
  End Function


  Private Sub RemoveSpikes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveSpikes.CheckedChanged

  End Sub
End Class