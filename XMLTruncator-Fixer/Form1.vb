Option Explicit On
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports Microsoft.Win32

Public Class Form1
    Public Function DelFromLeft(ByVal sChars As String, _
           ByVal sLine As String) As String

        ' Removes unwanted characters from left of given string
        '  EXAMPLE
        '      MsgBox DelFromLeft("THIS", "THIS IS A TEST")
        '        displays  "IS A TEST"


        Dim iCount As Integer
        Dim sChar As String

        DelFromLeft = ""
        ' Remove unwanted characters to left of folder name
        If InStr(sLine, sChars) > 0 Then
            For iCount = 1 To Len(sChars)
                ' Retrieve character from start string to 
                'look for in folder string (sLine)
                sChar = Mid$(sChars, iCount, 1)
                ' Remove all characters to left of found string
                sLine = Mid$(sLine, InStr(sLine, sChar) + 1)

            Next iCount
        End If
        DelFromLeft = sLine
        Exit Function

    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            'Select target file with OpenFileDialogBox
            Dim OFD As New OpenFileDialog
            Dim filename As String
            With OFD
                .ShowDialog()
                filename = .FileName
                PathTb.Text = .FileName
            End With
            Dim sFile As String = PathTb.Text

            Dim sFileInfo As New FileInfo(sFile)
            Dim sFileName As String = sFileInfo.Name
            Dim xmlFixTruncatedsFileName As String = "truncated-fixed-" & sFileName
            Dim sFileDirName As String = sFileInfo.DirectoryName
            Dim xmlFixTruncatedsFileNameFullPath = sFileDirName & "\" & xmlFixTruncatedsFileName

            FileCopy(sFile, xmlFixTruncatedsFileNameFullPath)

            'Now the xml file is validated to discover
            'where the error is. The validator is a simple pp 
            'compiled Perl program that uses the XML::Parser module.
            Dim xmlValidate As New Process()
            Dim xmlValidateArguments As String = """" & xmlFixTruncatedsFileNameFullPath & """"
            xmlValidate.StartInfo.FileName = "xmlval.exe"
            xmlValidate.StartInfo.Arguments = xmlValidateArguments
            xmlValidate.StartInfo.UseShellExecute = False
            xmlValidate.StartInfo.RedirectStandardError = True
            xmlValidate.StartInfo.RedirectStandardOutput = True
            xmlValidate.StartInfo.CreateNoWindow = True
            xmlValidate.Start()

            Dim xmlValidateReader As StreamReader = xmlValidate.StandardOutput
            Dim xmlValidateCompOut As String = xmlValidateReader.ReadToEnd
            Dim xmlValidateErrorReader As StreamReader = xmlValidate.StandardError
            Dim xmlValidateErrorOut As String = xmlValidateErrorReader.ReadToEnd
            xmlValidate.WaitForExit()
            xmlValidate.Close()

            'The validator will register an error and indicate the byte location 
            'of the error if the document.xml file has an error. We isolate
            ' this byte location with a Regex and the DelFromleft function, 
            'changed the byte to an integer and subtract 50 more bytes to try
            ' to steer clear of any additional bad xml just before the error.
            Dim byteMatch As Match = Regex.Match(xmlValidateErrorOut, _
                "byte [0-9]+")
            Dim byteMatchString As String = byteMatch.ToString
            Dim byteErrorLocation As String = DelFromLeft("byte ", byteMatchString)
            Dim byteErrorLocationInteger As Integer
            Integer.TryParse(byteErrorLocation, byteErrorLocationInteger)
            Dim truncatedLength As Integer = byteErrorLocationInteger - NumericUpDown1.Value
            Dim truncatedLengthAsString As String = String.Empty
            truncatedLengthAsString = System.Convert.ToString(truncatedLength)
            Dim truncate As New Process()
            Dim truncateArguments As String = xmlValidateArguments & " " & truncatedLengthAsString

            'Now we will truncate the file at bad byte minus
            '50 bytes. Validators will often report errors a 
            'few characters away from where they actually started.
            truncate.StartInfo.FileName = "trunc.exe"
            truncate.StartInfo.Arguments = truncateArguments
            truncate.StartInfo.UseShellExecute = False
            truncate.StartInfo.RedirectStandardError = True
            truncate.StartInfo.RedirectStandardOutput = True
            truncate.StartInfo.CreateNoWindow = True
            truncate.Start()
            truncate.WaitForExit()
            truncate.Close()

            'Now we use xmllint to reconstruct the nice xml ending tags
            'to try to slip document.xml past Word's XML validator.
            Dim xmlRecover As New Process()
            Dim xmlRecoverArguments As String = "--recover " & xmlValidateArguments & " -o " & xmlValidateArguments
            xmlRecover.StartInfo.FileName = "xmllint.exe"
            xmlRecover.StartInfo.Arguments = xmlRecoverArguments
            xmlRecover.StartInfo.UseShellExecute = False
            xmlRecover.StartInfo.RedirectStandardError = True
            xmlRecover.StartInfo.RedirectStandardOutput = True
            xmlRecover.StartInfo.CreateNoWindow = True
            xmlRecover.Start()
            xmlRecover.WaitForExit()
            xmlRecover.Close()

            Dim xmlFixedStart As New Process()
            xmlFixedStart.StartInfo.UseShellExecute = True
            xmlFixedStart.StartInfo.FileName = xmlValidateArguments
            xmlFixedStart.StartInfo.CreateNoWindow = True
            xmlFixedStart.Start()
            xmlFixedStart.WaitForExit()
            xmlFixedStart.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown1.ValueChanged

    End Sub

    Private Sub AboutToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles AboutToolStripMenuItem1.Click
        AboutBox1.Show()
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Try
            'Select target file with OpenFileDialogBox
            Dim OFD As New OpenFileDialog
            Dim filename As String
            With OFD
                .ShowDialog()
                filename = .FileName
                PathTb.Text = .FileName
            End With
            Dim sFile As String = PathTb.Text

            Dim sFileInfo As New FileInfo(sFile)
            Dim sFileName As String = sFileInfo.Name
            Dim xmlFixTruncatedsFileName As String = "truncated-fixed-" & sFileName
            Dim sFileDirName As String = sFileInfo.DirectoryName
            Dim xmlFixTruncatedsFileNameFullPath = sFileDirName & "\" & xmlFixTruncatedsFileName

            FileCopy(sFile, xmlFixTruncatedsFileNameFullPath)

            'Now the xml file is validated to discover
            'where the error is. The validator is a simple pp 
            'compiled Perl program that uses the XML::Parser module.
            Dim xmlValidate As New Process()
            Dim xmlValidateArguments As String = """" & xmlFixTruncatedsFileNameFullPath & """"
            xmlValidate.StartInfo.FileName = "xmlval.exe"
            xmlValidate.StartInfo.Arguments = xmlValidateArguments
            xmlValidate.StartInfo.UseShellExecute = False
            xmlValidate.StartInfo.RedirectStandardError = True
            xmlValidate.StartInfo.RedirectStandardOutput = True
            xmlValidate.StartInfo.CreateNoWindow = True
            xmlValidate.Start()

            Dim xmlValidateReader As StreamReader = xmlValidate.StandardOutput
            Dim xmlValidateCompOut As String = xmlValidateReader.ReadToEnd
            Dim xmlValidateErrorReader As StreamReader = xmlValidate.StandardError
            Dim xmlValidateErrorOut As String = xmlValidateErrorReader.ReadToEnd
            xmlValidate.WaitForExit()
            xmlValidate.Close()

            'The validator will register an error and indicate the byte location 
            'of the error if the document.xml file has an error. We isolate
            ' this byte location with a Regex and the DelFromleft function, 
            'changed the byte to an integer and subtract 50 more bytes to try
            ' to steer clear of any additional bad xml just before the error.
            Dim byteMatch As Match = Regex.Match(xmlValidateErrorOut, _
                "byte [0-9]+")
            Dim byteMatchString As String = byteMatch.ToString
            Dim byteErrorLocation As String = DelFromLeft("byte ", byteMatchString)
            Dim byteErrorLocationInteger As Integer
            Integer.TryParse(byteErrorLocation, byteErrorLocationInteger)
            Dim truncatedLength As Integer = byteErrorLocationInteger - NumericUpDown1.Value
            Dim truncatedLengthAsString As String = String.Empty
            truncatedLengthAsString = System.Convert.ToString(truncatedLength)
            Dim truncate As New Process()
            Dim truncateArguments As String = xmlValidateArguments & " " & truncatedLengthAsString

            'Now we will truncate the file at bad byte minus
            '50 bytes. Validators will often report errors a 
            'few characters away from where they actually started.
            truncate.StartInfo.FileName = "trunc.exe"
            truncate.StartInfo.Arguments = truncateArguments
            truncate.StartInfo.UseShellExecute = False
            truncate.StartInfo.RedirectStandardError = True
            truncate.StartInfo.RedirectStandardOutput = True
            truncate.StartInfo.CreateNoWindow = True
            truncate.Start()
            truncate.WaitForExit()
            truncate.Close()

            'Now we use xmllint to reconstruct the nice xml ending tags
            'to try to slip document.xml past Word's XML validator.
            Dim xmlRecover As New Process()
            Dim xmlRecoverArguments As String = "--recover " & xmlValidateArguments & " -o " & xmlValidateArguments
            xmlRecover.StartInfo.FileName = "xmllint.exe"
            xmlRecover.StartInfo.Arguments = xmlRecoverArguments
            xmlRecover.StartInfo.UseShellExecute = False
            xmlRecover.StartInfo.RedirectStandardError = True
            xmlRecover.StartInfo.RedirectStandardOutput = True
            xmlRecover.StartInfo.CreateNoWindow = True
            xmlRecover.Start()
            xmlRecover.WaitForExit()
            xmlRecover.Close()

            Dim xmlFixedStart As New Process()
            xmlFixedStart.StartInfo.UseShellExecute = True
            xmlFixedStart.StartInfo.FileName = xmlValidateArguments
            xmlFixedStart.StartInfo.CreateNoWindow = True
            xmlFixedStart.Start()
            xmlFixedStart.WaitForExit()
            xmlFixedStart.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
