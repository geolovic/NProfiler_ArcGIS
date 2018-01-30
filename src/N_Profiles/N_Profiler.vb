Public Class N_Profiler
  Inherits ESRI.ArcGIS.Desktop.AddIns.Button

  Public Sub New()

  End Sub

  Protected Overrides Sub OnClick()
    '
    '  TODO: Sample code showing how to access button host
    '
    My.ArcMap.Application.CurrentTool = Nothing
    Dim mainForm As SelDataForm = New SelDataForm
    mainForm.Show()
  End Sub

  Protected Overrides Sub OnUpdate()
    Enabled = My.ArcMap.Application IsNot Nothing
  End Sub
End Class
