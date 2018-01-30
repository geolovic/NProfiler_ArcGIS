<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelDataForm
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
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.CmbR = New System.Windows.Forms.ComboBox()
    Me.CmbL = New System.Windows.Forms.ComboBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.CheckSelected = New System.Windows.Forms.CheckBox()
    Me.LoadData = New System.Windows.Forms.Button()
    Me.RemoveSpikes = New System.Windows.Forms.CheckBox()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.CmbR)
    Me.GroupBox1.Controls.Add(Me.CmbL)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Location = New System.Drawing.Point(12, 9)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(246, 82)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Input Data"
    '
    'CmbR
    '
    Me.CmbR.FormattingEnabled = True
    Me.CmbR.Location = New System.Drawing.Point(132, 49)
    Me.CmbR.Name = "CmbR"
    Me.CmbR.Size = New System.Drawing.Size(102, 21)
    Me.CmbR.TabIndex = 3
    '
    'CmbL
    '
    Me.CmbL.FormattingEnabled = True
    Me.CmbL.Location = New System.Drawing.Point(132, 19)
    Me.CmbL.Name = "CmbL"
    Me.CmbL.Size = New System.Drawing.Size(102, 21)
    Me.CmbL.TabIndex = 2
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(45, 52)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(81, 13)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Raster Surface:"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(28, 22)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(98, 13)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "River feature class:"
    '
    'CheckSelected
    '
    Me.CheckSelected.AutoSize = True
    Me.CheckSelected.Location = New System.Drawing.Point(12, 97)
    Me.CheckSelected.Name = "CheckSelected"
    Me.CheckSelected.Size = New System.Drawing.Size(90, 17)
    Me.CheckSelected.TabIndex = 1
    Me.CheckSelected.Text = "Only selected"
    Me.CheckSelected.UseVisualStyleBackColor = True
    '
    'LoadData
    '
    Me.LoadData.Location = New System.Drawing.Point(156, 111)
    Me.LoadData.Name = "LoadData"
    Me.LoadData.Size = New System.Drawing.Size(102, 26)
    Me.LoadData.TabIndex = 2
    Me.LoadData.Text = "Procces profiles"
    Me.LoadData.UseVisualStyleBackColor = True
    '
    'RemoveSpikes
    '
    Me.RemoveSpikes.AutoSize = True
    Me.RemoveSpikes.Location = New System.Drawing.Point(12, 120)
    Me.RemoveSpikes.Name = "RemoveSpikes"
    Me.RemoveSpikes.Size = New System.Drawing.Size(99, 17)
    Me.RemoveSpikes.TabIndex = 3
    Me.RemoveSpikes.Text = "Remove spikes"
    Me.RemoveSpikes.UseVisualStyleBackColor = True
    '
    'SelDataForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(270, 153)
    Me.Controls.Add(Me.RemoveSpikes)
    Me.Controls.Add(Me.LoadData)
    Me.Controls.Add(Me.CheckSelected)
    Me.Controls.Add(Me.GroupBox1)
    Me.Name = "SelDataForm"
    Me.Text = "Select data"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents CmbR As System.Windows.Forms.ComboBox
  Friend WithEvents CmbL As System.Windows.Forms.ComboBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents CheckSelected As System.Windows.Forms.CheckBox
  Friend WithEvents LoadData As System.Windows.Forms.Button
  Friend WithEvents RemoveSpikes As System.Windows.Forms.CheckBox
End Class
