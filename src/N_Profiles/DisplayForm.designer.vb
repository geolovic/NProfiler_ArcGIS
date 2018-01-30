<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DisplayForm
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.canvas = New System.Windows.Forms.PictureBox()
    Me.suaviza_x = New System.Windows.Forms.TextBox()
    Me.ProfileCombo = New System.Windows.Forms.ComboBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Smoothlbl = New System.Windows.Forms.Label()
    Me.ExportAll = New System.Windows.Forms.Button()
    Me.legend = New System.Windows.Forms.PictureBox()
    Me.SaveImage = New System.Windows.Forms.Button()
    Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
    CType(Me.canvas, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.legend, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'canvas
    '
    Me.canvas.BackColor = System.Drawing.SystemColors.ControlLightLight
    Me.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.canvas.Location = New System.Drawing.Point(60, 40)
    Me.canvas.Name = "canvas"
    Me.canvas.Size = New System.Drawing.Size(400, 400)
    Me.canvas.TabIndex = 0
    Me.canvas.TabStop = False
    '
    'suaviza_x
    '
    Me.suaviza_x.Location = New System.Drawing.Point(417, 10)
    Me.suaviza_x.Name = "suaviza_x"
    Me.suaviza_x.Size = New System.Drawing.Size(43, 20)
    Me.suaviza_x.TabIndex = 3
    Me.suaviza_x.Text = "0"
    '
    'ProfileCombo
    '
    Me.ProfileCombo.FormattingEnabled = True
    Me.ProfileCombo.Location = New System.Drawing.Point(143, 9)
    Me.ProfileCombo.Name = "ProfileCombo"
    Me.ProfileCombo.Size = New System.Drawing.Size(142, 21)
    Me.ProfileCombo.TabIndex = 5
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label3.Location = New System.Drawing.Point(63, 10)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(78, 16)
    Me.Label3.TabIndex = 7
    Me.Label3.Text = "Select river:"
    '
    'Smoothlbl
    '
    Me.Smoothlbl.AutoSize = True
    Me.Smoothlbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Smoothlbl.Location = New System.Drawing.Point(322, 11)
    Me.Smoothlbl.Name = "Smoothlbl"
    Me.Smoothlbl.Size = New System.Drawing.Size(93, 16)
    Me.Smoothlbl.TabIndex = 8
    Me.Smoothlbl.Text = "Smooth factor:"
    '
    'ExportAll
    '
    Me.ExportAll.Location = New System.Drawing.Point(477, 418)
    Me.ExportAll.Name = "ExportAll"
    Me.ExportAll.Size = New System.Drawing.Size(102, 22)
    Me.ExportAll.TabIndex = 9
    Me.ExportAll.Text = "Export text"
    Me.ExportAll.UseVisualStyleBackColor = True
    '
    'legend
    '
    Me.legend.Location = New System.Drawing.Point(470, 41)
    Me.legend.Name = "legend"
    Me.legend.Size = New System.Drawing.Size(116, 174)
    Me.legend.TabIndex = 11
    Me.legend.TabStop = False
    '
    'SaveImage
    '
    Me.SaveImage.Location = New System.Drawing.Point(477, 389)
    Me.SaveImage.Name = "SaveImage"
    Me.SaveImage.Size = New System.Drawing.Size(102, 23)
    Me.SaveImage.TabIndex = 12
    Me.SaveImage.Text = "Save Image File"
    Me.SaveImage.UseVisualStyleBackColor = True
    '
    'DisplayForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(593, 492)
    Me.Controls.Add(Me.SaveImage)
    Me.Controls.Add(Me.legend)
    Me.Controls.Add(Me.ExportAll)
    Me.Controls.Add(Me.Smoothlbl)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.ProfileCombo)
    Me.Controls.Add(Me.suaviza_x)
    Me.Controls.Add(Me.canvas)
    Me.Name = "DisplayForm"
    Me.Text = "Normalized profiles"
    CType(Me.canvas, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.legend, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents canvas As System.Windows.Forms.PictureBox
    Friend WithEvents suaviza_x As System.Windows.Forms.TextBox
    Friend WithEvents ProfileCombo As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Smoothlbl As System.Windows.Forms.Label
    Friend WithEvents ExportAll As System.Windows.Forms.Button
    Friend WithEvents legend As System.Windows.Forms.PictureBox
    Friend WithEvents SaveImage As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog

End Class
