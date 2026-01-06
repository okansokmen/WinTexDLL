Option Explicit On

Imports System
Imports System.Data
Imports System.Web
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing

Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors.Repository

Public Class Grid

    Public oGrid As DevExpress.XtraGrid.GridControl

    Public Sub New(ByRef oDataGridView As DevExpress.XtraGrid.GridControl)

        oGrid = oDataGridView

        oGrid.UseEmbeddedNavigator = True

    End Sub

End Class
