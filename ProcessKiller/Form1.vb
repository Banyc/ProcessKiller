Imports System.ComponentModel

Public Class Form1
    Dim db As SqliteManager

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        db = New SqliteManager()
        Reflesh()
        RefleshRuleSet()
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        db.Dispose()
    End Sub

#Region "Page 1st"
    'refleshes listbox1 with new process info
    Private Sub Reflesh()
        lbActiveProcesses.Items.Clear()
        lbActiveProcesses.Sorted = True
        Dim proc As Process
        For Each proc In Process.GetProcesses()
            lbActiveProcesses.Items.Add(proc.ProcessName)
        Next
    End Sub
    'kills the process called ```procName``` and returns a state whether the killing process is a success
    Private Function Kill(procName As String)
        Dim IsKilled As Boolean
        Dim procs As Process()
        procs = Process.GetProcessesByName(procName)
        If procs.Length Then
            If MessageBox.Show("Sure to kill ALL processes called " & Chr(34) & procs(0).ProcessName & Chr(34) & "?", "Warning", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                For Each proc In procs
                    Try
                        proc.Kill()
                        IsKilled = True
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString())
                        IsKilled = False
                    End Try
                Next
            Else
                IsKilled = False
            End If
            If IsKilled Then
                While Process.GetProcessesByName(procName).Length
                    'wait until Process is updated
                End While
                Reflesh()
            End If
        Else
            MessageBox.Show("No such a process")
            IsKilled = False
        End If
        Return IsKilled
    End Function

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Reflesh()
    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lbActiveProcesses.MouseDoubleClick
        lbPendingProcesses.Items.Add(lbActiveProcesses.SelectedItem)
    End Sub

    Private Sub btnKill_Click(sender As Object, e As EventArgs) Handles btnKill.Click

        Dim killedNames As New List(Of String)

        For Each procName In lbPendingProcesses.Items
            If Kill(procName) Then
                killedNames.Add(procName)
            End If
        Next

        For Each procName In killedNames
            lbPendingProcesses.Items.Remove(procName)
        Next

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lbPendingProcesses.Items.Clear()
    End Sub

    Private Sub ListBox2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lbPendingProcesses.MouseDoubleClick
        lbPendingProcesses.Items.RemoveAt(lbPendingProcesses.SelectedIndex)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' margin is 10

        lbActiveProcesses.Width = (TabControl1.Width - btnKill.Width - 40) / 2
        lbActiveProcesses.Height = TabControl1.Height - 30
        btnKill.Left = lbActiveProcesses.Width + 20
        btnClear.Left = lbActiveProcesses.Width + 20
        btnRefresh.Left = lbActiveProcesses.Width + 20

        btnKill.Top = (TabControl1.Height - btnKill.Height) / 2 + TabControl1.Top
        btnClear.Top = (TabControl1.Height - btnKill.Height) / 2 + TabControl1.Top - btnClear.Height - 10
        btnRefresh.Top = (TabControl1.Height - btnKill.Height) / 2 + TabControl1.Top + btnRefresh.Height + 10

        lbPendingProcesses.Left = (TabControl1.Width - btnKill.Width - 40) / 2 + 30 + btnKill.Width
        lbPendingProcesses.Width = (TabControl1.Width - btnKill.Width - 40) / 2
        lbPendingProcesses.Height = TabControl1.Height - 30
    End Sub

    Private Sub btnMigrate_Click(sender As Object, e As EventArgs) Handles btnMigrate.Click
        If Not lbRuleSets.SelectedItem Is Nothing Then
            Dim currentSelectedIndex = lbRuleSets.SelectedIndex
            Dim currentSelectedKey = lbRuleSets.SelectedItem.Key
            For Each processName In lbPendingProcesses.Items
                db.AddProcess(currentSelectedKey, processName)
            Next
            lbRuleSets.SelectedIndex = currentSelectedIndex
            RefleshProcessNames()
        End If
    End Sub
#End Region

#Region "Page 2nd"
    ' refleshes the listbox displaying rulesets through quering database
    'WARNING: side-effect: calling lbRuleSets_SelectedIndexChanged for three times
    Private Sub RefleshRuleSet()
        lbRuleSets.DataSource = Nothing  ' clears the prior elements in the listbox
        Dim ruleSets = db.ReadRuleSets()
        If ruleSets.Count Then  ' if dict ruleSets is not NULL
            ' https://stackoverflow.com/questions/1506987/how-to-bind-dictionary-to-listbox-in-winforms
            ' Warning: lines below will respectively call event lbRuleSets_SelectedIndexChanged
            lbRuleSets.DataSource = New BindingSource(ruleSets, Nothing)
            lbRuleSets.DisplayMember = "Value"  ' What is the point???
            lbRuleSets.ValueMember = "Key"
        End If
    End Sub

    Private Sub RefleshProcessNames()
        lbProcessNames.DataSource = Nothing
        If Not lbRuleSets.SelectedItem Is Nothing Then
            Dim displayProcNames = db.ReadProcessNames(lbRuleSets.SelectedItem.Key)
            If displayProcNames.Count Then
                lbProcessNames.DataSource = New BindingSource(displayProcNames, Nothing)
                lbProcessNames.ValueMember = "Key"
                lbProcessNames.DisplayMember = "Value"

            End If
        End If
    End Sub
    ' Reflesh process names in lbProcessNames
    Private Sub lbRuleSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbRuleSets.SelectedIndexChanged
        RefleshProcessNames()
        If Not lbRuleSets.SelectedItem Is Nothing Then
            txtRuleSet.Text = lbRuleSets.SelectedItem.Value
        End If
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        If txtRuleSet.Text.Count Then
            db.AddRuleSet(txtRuleSet.Text)
        Else
            db.AddRuleSet("New Rule Set")
        End If

        RefleshRuleSet()
        lbRuleSets.SelectedIndex = lbRuleSets.Items.Count - 1
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        db.DeleteRuleSet(lbRuleSets.SelectedItem.Key)

        Dim currentSelectedIndex = lbRuleSets.SelectedIndex
        RefleshRuleSet()
        If Not lbRuleSets.SelectedItem Is Nothing Then
            lbRuleSets.SelectedIndex = Math.Max(currentSelectedIndex - 1, 0)
        End If
    End Sub

    Private Sub btnSetName_Click(sender As Object, e As EventArgs) Handles btnSetName.Click
        db.RenameRuleSet(lbRuleSets.SelectedItem.Key, txtRuleSet.Text)

        Dim currentSelectedIndex = lbRuleSets.SelectedIndex
        RefleshRuleSet()
        lbRuleSets.SelectedIndex = currentSelectedIndex
    End Sub
    ' Deletes selected Process
    Private Sub lbProcessNames_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lbProcessNames.MouseDoubleClick
        If Not lbProcessNames.SelectedItem Is Nothing Then
            db.DeleteProcess(lbRuleSets.SelectedItem.Key, lbProcessNames.SelectedItem.Key)
            RefleshProcessNames()  ' reflesh lbProcessNames
        End If
    End Sub

    Private Sub btnAddProcess_Click(sender As Object, e As EventArgs) Handles btnAddProcess.Click
        If txtRuleSet.Text.Count And Not lbRuleSets.SelectedItem Is Nothing Then
            db.AddProcess(lbRuleSets.SelectedItem.Key, txtNewProcess.Text)
            RefleshProcessNames()
        End If
    End Sub
    ' Executes the selected rule by transferring process names to page first
    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        lbPendingProcesses.Items.Clear()
        TabControl1.SelectedIndex = 0
        For Each processNamePair In lbProcessNames.Items
            lbPendingProcesses.Items.Add(processNamePair.Value)
        Next

    End Sub
#End Region


End Class
