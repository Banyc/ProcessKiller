<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbActiveProcesses = New System.Windows.Forms.ListBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.MainPage = New System.Windows.Forms.TabPage()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnKill = New System.Windows.Forms.Button()
        Me.lbPendingProcesses = New System.Windows.Forms.ListBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.RulesPage = New System.Windows.Forms.TabPage()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.txtRuleSet = New System.Windows.Forms.TextBox()
        Me.btnSetName = New System.Windows.Forms.Button()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.btnAddProcess = New System.Windows.Forms.Button()
        Me.lbProcessNames = New System.Windows.Forms.ListBox()
        Me.lbRuleSets = New System.Windows.Forms.ListBox()
        Me.txtNewProcess = New System.Windows.Forms.TextBox()
        Me.btnMigrate = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.MainPage.SuspendLayout()
        Me.RulesPage.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbActiveProcesses
        '
        Me.lbActiveProcesses.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbActiveProcesses.FormattingEnabled = True
        Me.lbActiveProcesses.ItemHeight = 23
        Me.lbActiveProcesses.Location = New System.Drawing.Point(6, 6)
        Me.lbActiveProcesses.Name = "lbActiveProcesses"
        Me.lbActiveProcesses.Size = New System.Drawing.Size(325, 372)
        Me.lbActiveProcesses.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.MainPage)
        Me.TabControl1.Controls.Add(Me.RulesPage)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(776, 426)
        Me.TabControl1.TabIndex = 1
        '
        'MainPage
        '
        Me.MainPage.Controls.Add(Me.btnMigrate)
        Me.MainPage.Controls.Add(Me.btnClear)
        Me.MainPage.Controls.Add(Me.btnKill)
        Me.MainPage.Controls.Add(Me.lbPendingProcesses)
        Me.MainPage.Controls.Add(Me.btnRefresh)
        Me.MainPage.Controls.Add(Me.lbActiveProcesses)
        Me.MainPage.Location = New System.Drawing.Point(4, 25)
        Me.MainPage.Name = "MainPage"
        Me.MainPage.Padding = New System.Windows.Forms.Padding(3)
        Me.MainPage.Size = New System.Drawing.Size(768, 397)
        Me.MainPage.TabIndex = 0
        Me.MainPage.Text = "MainPage"
        Me.MainPage.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(352, 304)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 4
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnKill
        '
        Me.btnKill.Location = New System.Drawing.Point(352, 333)
        Me.btnKill.Name = "btnKill"
        Me.btnKill.Size = New System.Drawing.Size(75, 23)
        Me.btnKill.TabIndex = 3
        Me.btnKill.Text = "Kill"
        Me.btnKill.UseVisualStyleBackColor = True
        '
        'lbPendingProcesses
        '
        Me.lbPendingProcesses.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbPendingProcesses.Font = New System.Drawing.Font("Consolas", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPendingProcesses.FormattingEnabled = True
        Me.lbPendingProcesses.ItemHeight = 32
        Me.lbPendingProcesses.Location = New System.Drawing.Point(449, 6)
        Me.lbPendingProcesses.Name = "lbPendingProcesses"
        Me.lbPendingProcesses.Size = New System.Drawing.Size(313, 356)
        Me.lbPendingProcesses.TabIndex = 2
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(352, 362)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'RulesPage
        '
        Me.RulesPage.Controls.Add(Me.btnExecute)
        Me.RulesPage.Controls.Add(Me.btnDelete)
        Me.RulesPage.Controls.Add(Me.txtRuleSet)
        Me.RulesPage.Controls.Add(Me.btnSetName)
        Me.RulesPage.Controls.Add(Me.btnCreate)
        Me.RulesPage.Controls.Add(Me.btnAddProcess)
        Me.RulesPage.Controls.Add(Me.lbProcessNames)
        Me.RulesPage.Controls.Add(Me.lbRuleSets)
        Me.RulesPage.Controls.Add(Me.txtNewProcess)
        Me.RulesPage.Location = New System.Drawing.Point(4, 25)
        Me.RulesPage.Name = "RulesPage"
        Me.RulesPage.Padding = New System.Windows.Forms.Padding(3)
        Me.RulesPage.Size = New System.Drawing.Size(768, 397)
        Me.RulesPage.TabIndex = 1
        Me.RulesPage.Text = "RulesPage"
        Me.RulesPage.UseVisualStyleBackColor = True
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(292, 342)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(75, 23)
        Me.btnExecute.TabIndex = 7
        Me.btnExecute.Text = "Execute"
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(87, 342)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'txtRuleSet
        '
        Me.txtRuleSet.Location = New System.Drawing.Point(6, 314)
        Me.txtRuleSet.Name = "txtRuleSet"
        Me.txtRuleSet.Size = New System.Drawing.Size(280, 25)
        Me.txtRuleSet.TabIndex = 5
        '
        'btnSetName
        '
        Me.btnSetName.Location = New System.Drawing.Point(6, 342)
        Me.btnSetName.Name = "btnSetName"
        Me.btnSetName.Size = New System.Drawing.Size(75, 23)
        Me.btnSetName.TabIndex = 4
        Me.btnSetName.Text = "SetName"
        Me.btnSetName.UseVisualStyleBackColor = True
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(292, 313)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(75, 23)
        Me.btnCreate.TabIndex = 3
        Me.btnCreate.Text = "Create"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'btnAddProcess
        '
        Me.btnAddProcess.Location = New System.Drawing.Point(687, 347)
        Me.btnAddProcess.Name = "btnAddProcess"
        Me.btnAddProcess.Size = New System.Drawing.Size(75, 23)
        Me.btnAddProcess.TabIndex = 2
        Me.btnAddProcess.Text = "Add"
        Me.btnAddProcess.UseVisualStyleBackColor = True
        '
        'lbProcessNames
        '
        Me.lbProcessNames.FormattingEnabled = True
        Me.lbProcessNames.ItemHeight = 15
        Me.lbProcessNames.Location = New System.Drawing.Point(401, 6)
        Me.lbProcessNames.Name = "lbProcessNames"
        Me.lbProcessNames.Size = New System.Drawing.Size(361, 304)
        Me.lbProcessNames.TabIndex = 2
        '
        'lbRuleSets
        '
        Me.lbRuleSets.FormattingEnabled = True
        Me.lbRuleSets.ItemHeight = 15
        Me.lbRuleSets.Location = New System.Drawing.Point(6, 6)
        Me.lbRuleSets.Name = "lbRuleSets"
        Me.lbRuleSets.Size = New System.Drawing.Size(361, 304)
        Me.lbRuleSets.TabIndex = 1
        '
        'txtNewProcess
        '
        Me.txtNewProcess.Location = New System.Drawing.Point(401, 316)
        Me.txtNewProcess.Name = "txtNewProcess"
        Me.txtNewProcess.Size = New System.Drawing.Size(361, 25)
        Me.txtNewProcess.TabIndex = 0
        '
        'btnMigrate
        '
        Me.btnMigrate.Location = New System.Drawing.Point(352, 275)
        Me.btnMigrate.Name = "btnMigrate"
        Me.btnMigrate.Size = New System.Drawing.Size(75, 23)
        Me.btnMigrate.TabIndex = 5
        Me.btnMigrate.Text = "Ad2Rule"
        Me.btnMigrate.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.MainPage.ResumeLayout(False)
        Me.RulesPage.ResumeLayout(False)
        Me.RulesPage.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbActiveProcesses As ListBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents MainPage As TabPage
    Friend WithEvents btnRefresh As Button
    Friend WithEvents RulesPage As TabPage
    Friend WithEvents lbPendingProcesses As ListBox
    Friend WithEvents btnKill As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents txtNewProcess As TextBox
    Friend WithEvents lbProcessNames As ListBox
    Friend WithEvents lbRuleSets As ListBox
    Friend WithEvents btnAddProcess As Button
    Friend WithEvents btnCreate As Button
    Friend WithEvents txtRuleSet As TextBox
    Friend WithEvents btnSetName As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnExecute As Button
    Friend WithEvents btnMigrate As Button
End Class
