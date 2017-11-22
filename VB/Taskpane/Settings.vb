﻿'Option Strict On
Option Explicit On

Imports System.Windows.Forms
Imports System.Reflection
Imports ServerActions.Scripts

Namespace TaskPane

    Partial Public Class Settings
        Inherits UserControl

        Public Sub New()
            Try
                InitializeComponent()
                Me.pgdSettings.SelectedObject = My.Settings

            Catch ex As Exception
                ErrorHandler.DisplayMessage(ex)

            End Try

        End Sub

        Friend WithEvents pgdSettings As PropertyGrid

        Private Sub InitializeComponent()
            Me.pgdSettings = New System.Windows.Forms.PropertyGrid()
            Me.SuspendLayout()
            '
            'pgdSettings
            '
            Me.pgdSettings.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pgdSettings.Location = New System.Drawing.Point(0, 0)
            Me.pgdSettings.Name = "pgdSettings"
            Me.pgdSettings.Size = New System.Drawing.Size(650, 750)
            Me.pgdSettings.TabIndex = 0
            '
            'Settings
            '
            Me.Controls.Add(Me.pgdSettings)
            Me.Name = "Settings"
            Me.Size = New System.Drawing.Size(650, 750)
            Me.ResumeLayout(False)

        End Sub

        Public Shared Sub SetLabelColumnWidth(grid As PropertyGrid, width As Integer)
            Try
                If grid Is Nothing Then
                    Return
                End If

                Dim fi As FieldInfo = grid.[GetType]().GetField("gridView", BindingFlags.Instance Or BindingFlags.NonPublic)
                If fi Is Nothing Then
                    Return
                End If

                Dim view As Control = TryCast(fi.GetValue(grid), Control)
                If view Is Nothing Then
                    Return
                End If

                Dim mi As MethodInfo = view.[GetType]().GetMethod("MoveSplitterTo", BindingFlags.Instance Or BindingFlags.NonPublic)
                If mi Is Nothing Then
                    Return
                End If
                mi.Invoke(view, New Object() {width})

            Catch ex As Exception
                ErrorHandler.DisplayMessage(ex)

            End Try

        End Sub

        Private Sub pgdSettings_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs)
            Try
                Scripts.Ribbon.InvalidateRibbon()

            Catch ex As Exception
                ErrorHandler.DisplayMessage(ex)

            End Try

        End Sub

    End Class

End Namespace