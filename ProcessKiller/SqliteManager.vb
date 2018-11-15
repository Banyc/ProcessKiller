Public Class SqliteManager : Implements IDisposable
    'Database Structure
    '- RuleSets
    '  - ID
    '  - Text
    '- ProcessNames
    '  - ID
    '  - Text
    '- RuleLines
    '  - RuleSet_ID
    '  - ProcessName_ID

    'Private Const PATH As String = "./test.db"
    Private path As String
    Private conn As SQLite.SQLiteConnection

#Region "Initiation"
    Public Sub New(Optional intiPath As String = "./test.db")
        path = intiPath
        conn = New SQLite.SQLiteConnection(String.Format("Data Source={0}", path))
        conn.Open()  ' if PATH does NOT exist, it creates a empty PATH
        InitTables()
    End Sub

    Private Sub InitTables()
        Dim cmd As New SQLite.SQLiteCommand(conn)
        cmd.CommandText =
            "
                Create Table If Not Exists RuleSets
                (
                    ID integer Not Null Primary Key AUTOINCREMENT UNIQUE,
                    Text text Not Null
                );
                Create Table If Not Exists ProcessNames
                (
                    ID integer Not Null Primary Key AUTOINCREMENT UNIQUE,
                    Text text Not Null UNIQUE
                );
                Create Table If Not Exists RuleLines
                (
                    RuleSet_ID integer Not Null,
                    ProcessName_ID integer Not Null,
                        FOREIGN KEY (RuleSet_ID) REFERENCES RuleSets(ID),
                        FOREIGN KEY (ProcessName_ID) REFERENCES ProcessNames(ID)
                );
                PRAGMA foreign_keys = ON;
            "
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub
#End Region

#Region "ProcessNames Operations"
    Public Sub AddProcess(ruleSet_ID As Integer, newProcName As String)
        Dim cmd As New SQLite.SQLiteCommand(conn)
        Dim process_ID As Integer  ' of newProcName
        'tries to get the corresponding ID of newProcName
        cmd.CommandText = String.Format("Select ID From ProcessNames Where Text = '{0}';", newProcName)
        Dim readerProcessNames4ID = cmd.ExecuteReader()
        If readerProcessNames4ID.HasRows Then  ' the process has already existed in table ProcessNames
            ' does nothing
        Else  ' generates a new ID for newProcName
            readerProcessNames4ID.Close()
            cmd.CommandText = String.Format(
                "
                    Insert Into ProcessNames
                    (
                        Text
                    )
                    Values
                    (
                        '{0}'
                    );
                ",
                newProcName
            )
            cmd.ExecuteNonQuery()
            cmd.CommandText = String.Format("Select ID From ProcessNames Where Text = '{0}';", newProcName)
            readerProcessNames4ID = cmd.ExecuteReader()
        End If
        readerProcessNames4ID.Read()
        process_ID = readerProcessNames4ID("ID")  ' of newProcName
        readerProcessNames4ID.Close()

        ' checks if the process has already existed in relationship to ruleSet_ID in table RuleLines
        cmd.CommandText = String.Format("Select * From RuleLines Where RuleSet_ID = {0} And ProcessName_ID = {1};", ruleSet_ID, process_ID)
        Dim reader2 = cmd.ExecuteReader()
        If reader2.Read() Then  ' the process has already existed in relationship to ruleSet_ID in table RuleLines
            reader2.Close()
            ' Does nothing
        Else  ' creates a new relationship of the two IDs in table RuleLines
            reader2.Close()
            cmd.CommandText = String.Format(
                "
                    Insert Into RuleLines
                    (
                        RuleSet_ID,
                        ProcessName_ID
                    )
                    Values
                    (
                        {0},
                        {1}
                    );
                ",
                ruleSet_ID, process_ID
            )
            cmd.ExecuteNonQuery()
        End If
        cmd.Dispose()
    End Sub

    Public Sub DeleteProcess(ruleSet_ID As Integer, process_ID As Integer)
        Dim cmd = New SQLite.SQLiteCommand(conn)
        cmd.CommandText = String.Format("Delete From RuleLines Where RuleSet_ID = {0} And ProcessName_ID = {1};", ruleSet_ID, process_ID)
        cmd.ExecuteNonQuery()
        ' Checks if the process_ID should be deleted from table ProcessNames as well
        cmd.CommandText = "Select * From RuleLines Where ProcessName_ID = " & Str(process_ID) & ";"
        Dim reader = cmd.ExecuteReader()
        If reader.HasRows Then
            reader.Close()
            ' Cancel
        Else
            reader.Close()
            cmd.CommandText = "Delete From ProcessNames Where ID = " & Str(process_ID) & ";"
            cmd.ExecuteNonQuery()
        End If
        cmd.Dispose()
    End Sub

    Public Function ReadProcessNames(ruleSet_ID As Integer) As Dictionary(Of Integer, String)  ' dict(ID, Text)
        Dim includedProcessName_IDs As New List(Of Integer)
        Dim processNames As New Dictionary(Of Integer, String)
        Dim cmd As New SQLite.SQLiteCommand(conn)

        ' finds out ruleSet_ID's responding processName_IDs
        cmd.CommandText = String.Format("Select ProcessName_ID From RuleLines Where RuleSet_ID = {0}", ruleSet_ID)
        Dim reader1 = cmd.ExecuteReader()
        While (reader1.Read())
            Debug.WriteLine(reader1("ProcessName_ID"))
            includedProcessName_IDs.Add(reader1("ProcessName_ID"))
        End While
        reader1.Close()

        ' finds out processName_IDs' responding full names
        Dim reader2 As SQLite.SQLiteDataReader
        For Each processName_ID In includedProcessName_IDs
            cmd.CommandText = String.Format("Select Text From ProcessNames Where ID = {0}", processName_ID)
            reader2 = cmd.ExecuteReader()
            While (reader2.Read())
                processNames.Add(processName_ID, reader2("Text"))
            End While
            reader2.Close()
        Next

        cmd.Dispose()
        includedProcessName_IDs = Nothing

        Return processNames
    End Function

    'Public Sub UpdateProcessNames(ruleSet_ID As Integer, processPair As Dictionary(Of Integer, String))
    '    Dim cmd As New SQLite.SQLiteCommand(conn)
    '    cmd.CommandText = ""

    'End Sub
#End Region

#Region "RuleSets Operations"
    Public Sub AddRuleSet(ruleSetName As String)
        Dim cmd As New SQLite.SQLiteCommand(conn)
        cmd.CommandText = String.Format("Insert Into RuleSets (Text) Values ('{0}');", ruleSetName)
        cmd.ExecuteNonQuery()

        cmd.Dispose()
    End Sub

    Public Sub RenameRuleSet(ruleSet_ID As Integer, newName As String)
        Dim cmd As New SQLite.SQLiteCommand(conn)
        cmd.CommandText = String.Format("Update RuleSets Set Text = '{0}' Where ID = {1};", newName, ruleSet_ID)  ' syntax: Text = 'newName'
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Public Sub DeleteRuleSet(ruleSet_ID As Integer)
        Dim cmd As New SQLite.SQLiteCommand(conn)
        ' Finds out each relevant process which is to be deleted by calling function DeleteProcess()
        cmd.CommandText = String.Format("Select ProcessName_ID From RuleLines Where RuleSet_ID = {0};", ruleSet_ID)
        Dim reader = cmd.ExecuteReader()
        While reader.Read()
            DeleteProcess(ruleSet_ID, Int(reader("ProcessName_ID")))
        End While
        reader.Close()

        cmd.CommandText = String.Format("Delete From RuleSets Where ID = {0}", ruleSet_ID)
        cmd.ExecuteNonQuery()
        cmd.Dispose()
    End Sub

    Public Function ReadRuleSets() As Dictionary(Of Integer, String)
        Dim ruleSets As New Dictionary(Of Integer, String)
        Dim cmd As New SQLite.SQLiteCommand(conn)
        cmd.CommandText = "Select ID, Text From RuleSets"
        Dim reader As SQLite.SQLiteDataReader = cmd.ExecuteReader

        While (reader.Read())
            Debug.WriteLine(reader("ID") & vbTab & reader("Text"))
            ruleSets.Add(reader("ID"), reader("Text"))
        End While
        reader.Close()
        cmd.Dispose()

        Return ruleSets
    End Function
#End Region

#Region "IDisposable Support"  ' https://stackoverflow.com/questions/19097555/dispose-Class-vb-net
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
