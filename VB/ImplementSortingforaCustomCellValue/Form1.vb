Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports System.Collections

Namespace WindowsFormsApplication15
	Partial Public Class Form1
		Inherits Form
		Private yearValues_Renamed As List(Of Object)
		Private pivotData As PivotData
		Private sortInfo As SortInfo

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            'TODO: This line of code loads data into the 'NwindDataSet.CustomerReports' table. You can move, or remove it, as needed.
            Me.CustomerReportsTableAdapter.Fill(Me.NwindDataSet.CustomerReports)
            Me.pivotData = FillPivotData()
		End Sub

		Private ReadOnly Property YearValues() As List(Of Object)
			Get
				If yearValues_Renamed Is Nothing Then
					yearValues_Renamed = New List(Of Object)()
					yearValues_Renamed.AddRange(fieldOrderDate.GetUniqueValues())
					yearValues_Renamed.Sort()
				End If
				Return yearValues_Renamed
			End Get
		End Property

		Private Sub pivotGridControl1_CustomCellValue(ByVal sender As Object, ByVal e As PivotCellValueEventArgs) Handles pivotGridControl1.CustomCellValue
			If e.DataField Is fieldProductAmount1 AndAlso e.Value IsNot Nothing Then
				Dim rowValues As New List(Of Object)()
				For Each field As PivotGridField In e.GetRowFields()
					rowValues.Add(UpdateValue(field, e.RowIndex, e))
				Next field
				Dim columnValues As New List(Of Object)()
				For Each field As PivotGridField In e.GetColumnFields()
					columnValues.Add(UpdateValue(field, e.ColumnIndex, e))
				Next field
				Dim previousValue As Decimal = Convert.ToDecimal(e.GetCellValue(columnValues.ToArray(), rowValues.ToArray(), fieldProductAmount))
				Dim value As Decimal = Convert.ToDecimal(e.GetCellValue(fieldProductAmount))
				If previousValue = 0D OrElse e.Value Is Nothing Then
					e.Value = Nothing
				Else
					e.Value = (CDec(e.Value) / previousValue - 1)
				End If
			End If
		End Sub

		Private Sub pivotGridControl1_CustomFieldSort(ByVal sender As Object, ByVal e As PivotGridCustomFieldSortEventArgs) Handles pivotGridControl1.CustomFieldSort
			If Me.pivotData Is Nothing OrElse sortInfo Is Nothing Then
				e.Handled = False
				Return
			End If
			Dim sortByColumnIndex As Integer = GetSortByColumnIndex(e)
			Dim value1RowIndex As Integer = GetValueIndex(False, e.Value1, e.Field, Nothing)
			Dim value2RowIndex As Integer = GetValueIndex(False, e.Value2, e.Field, Nothing)
			If value1RowIndex < 0 OrElse value2RowIndex < 0 OrElse sortByColumnIndex < 0 Then
				Return
			End If
			Dim cellValue1 As Object = pivotData.GetCellValue(sortByColumnIndex, value1RowIndex)
			Dim cellValue2 As Object = pivotData.GetCellValue(sortByColumnIndex, value2RowIndex)
			e.Handled = True
			If Object.Equals(cellValue1, cellValue2) Then
				e.Result = Comparer.Default.Compare(e.Value1, e.Value2)
			Else
				e.Result = Comparer.Default.Compare(cellValue1, cellValue2)
			End If
			Return
		End Sub

		Private Function UpdateValue(ByVal field As PivotGridField, ByVal index As Integer, ByVal e As PivotCellValueEventArgs) As Object
			Dim value As Object = e.GetFieldValue(field, index)
			If field Is fieldOrderDate Then
				Dim currentPosition As Integer = YearValues.IndexOf(value)
				If currentPosition > 0 Then
					value = YearValues(currentPosition - 1)
				Else
					value = "0000"
				End If
			End If
			Return value
		End Function

		Private Function FillPivotData() As PivotData
			Dim columnCount As Integer = pivotGridControl1.Cells.ColumnCount, rowCount As Integer = pivotGridControl1.Cells.RowCount
			Dim columnFields As List(Of PivotGridField) = pivotGridControl1.GetFieldsByArea(PivotArea.ColumnArea), rowFields As List(Of PivotGridField) = pivotGridControl1.GetFieldsByArea(PivotArea.RowArea)

			Dim data As New PivotData(columnCount, rowCount, columnFields.Count, rowFields.Count)

			FillFieldValues(True, columnCount, columnFields, data)
			FillFieldValues(False, rowCount, rowFields, data)

			For i As Integer = 0 To columnCount - 1
				For j As Integer = 0 To rowCount - 1
					Dim info As PivotCellEventArgs = pivotGridControl1.Cells.GetCellInfo(i, j)
					If info IsNot Nothing Then
						data.SetCellValue(i, j, info.Value)
					End If
				Next j
			Next i

			Return data
		End Function
		Private Sub FillFieldValues(ByVal isColumn As Boolean, ByVal count As Integer, ByVal fields As List(Of PivotGridField), ByVal data As PivotData)
			For i As Integer = 0 To count - 1
				Dim value As FieldValue = data.GetFieldValue(isColumn, i)
				value.ValueType = pivotGridControl1.GetFieldValueType(isColumn, i)
				value.DataField = pivotGridControl1.Cells.GetCellInfo(If(isColumn, i, 0),If(isColumn, 0, i)).DataField
				For j As Integer = 0 To fields.Count - 1
					value.SetValue(j, pivotGridControl1.GetFieldValue(fields(j), i), fields(j))
				Next j
			Next i
		End Sub

		Private Function GetSortByColumnIndex(ByVal e As PivotGridCustomFieldSortEventArgs) As Integer
			If sortInfo.Conditions.Count = 0 Then
				For i As Integer = pivotData.ColumnCount - 1 To 0 Step -1
					Dim value As FieldValue = pivotData.GetFieldValue(True, i)
					If value.ValueType = PivotGridValueType.GrandTotal AndAlso value.DataField Is sortInfo.DataField Then
						Return i
					End If
				Next i
			Else
				For i As Integer = 0 To pivotData.ColumnCount - 1
					Dim value As FieldValue = pivotData.GetFieldValue(True, i)
					If IsValueFit(value, sortInfo.Conditions, sortInfo.DataField) Then
						Return i
					End If
				Next i
			End If
			Return -1
		End Function

		Private Function GetValueIndex(ByVal isColumn As Boolean, ByVal value As Object, ByVal field As PivotGridField, ByVal dataField As PivotGridField) As Integer
			Dim count As Integer = If(isColumn, pivotData.ColumnCount, pivotData.RowCount)
			For i As Integer = 0 To count - 1
				Dim fieldValue As FieldValue = pivotData.GetFieldValue(isColumn, i)
				If Object.Equals(value, fieldValue(field)) AndAlso (dataField Is Nothing OrElse fieldValue.DataField Is dataField) Then
					Return i
				End If
			Next i
			Return -1
		End Function

		Private Function IsValueFit(ByVal value As FieldValue, ByVal conds As List(Of PivotGridFieldSortCondition), ByVal field As PivotGridFieldBase) As Boolean
			For i As Integer = 0 To conds.Count - 1
				Dim cond As PivotGridFieldSortCondition = conds(i)
				If Not(Object.Equals(value(CType(cond.Field, PivotGridField)), cond.Value) AndAlso field Is TryCast(value.DataField, PivotGridFieldBase)) Then
					Return False
				End If
			Next i
			Return True
		End Function
		Private Sub pivotGridControl1_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pivotGridControl1.MouseClick
			Dim hi As PivotGridHitInfo = pivotGridControl1.CalcHitInfo(e.Location)
			If hi.HitTest = PivotGridHitTest.Value AndAlso hi.ValueInfo.IsColumn Then
				ApplySortByValue(hi.ValueInfo)
			End If
		End Sub
		Private Sub ApplySortByValue(ByVal valueInfo As PivotFieldValueEventArgs)
			If valueInfo.DataField IsNot valueInfo.Field Then
				Return
			End If
			Dim sortInfo As New SortInfo()
			sortInfo.DataField = valueInfo.DataField
			Dim fields() As PivotGridField = valueInfo.GetHigherLevelFields()
			For i As Integer = 0 To fields.Length - 1
				sortInfo.Conditions.Add(New PivotGridFieldSortCondition(fields(i), valueInfo.GetFieldValue(fields(i), valueInfo.MinIndex)))
			Next i
			Me.sortInfo = sortInfo
			pivotGridControl1.RefreshData()
		End Sub


		Private Sub pivotGridControl1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PopupMenuShowingEventArgs)
			If e.MenuType = PivotGridMenuType.FieldValue Then
				e.Allow = False
			End If
		End Sub
	End Class

	Friend Class PivotData
		Private columns(), rows() As FieldValue
		Private cells(,) As Object

		Public Sub New(ByVal columnCount As Integer, ByVal rowCount As Integer, ByVal columnLevelCount As Integer, ByVal rowLevelCount As Integer)
			Me.cells = New Object(columnCount - 1, rowCount - 1){}
			InitFieldValues(Me.columns, columnCount, columnLevelCount)
			InitFieldValues(Me.rows, rowCount, rowLevelCount)
		End Sub

		Public ReadOnly Property ColumnCount() As Integer
			Get
				Return Me.columns.Length
			End Get
		End Property
		Public ReadOnly Property RowCount() As Integer
			Get
				Return Me.rows.Length
			End Get
		End Property

		Private Sub InitFieldValues(ByRef values() As FieldValue, ByVal length As Integer, ByVal levelCount As Integer)
			values = New FieldValue(length - 1){}
			For i As Integer = 0 To length - 1
				values(i) = New FieldValue(levelCount)
			Next i
		End Sub

		Public Function GetCellValue(ByVal columnIndex As Integer, ByVal rowIndex As Integer) As Object
			Return Me.cells(columnIndex, rowIndex)
		End Function
		Public Sub SetCellValue(ByVal columnIndex As Integer, ByVal rowIndex As Integer, ByVal value As Object)
			Me.cells(columnIndex, rowIndex) = value
		End Sub
		Public Function GetFieldValue(ByVal isColumn As Boolean, ByVal index As Integer) As FieldValue
			Dim array() As FieldValue = If(isColumn, Me.columns, Me.rows)
			Return array(index)
		End Function


	End Class

	Friend Class FieldValue
		Private values() As Object
		Private fields() As PivotGridField
		Private cache As Dictionary(Of PivotGridField, Object)

		Public Sub New(ByVal levelCount As Integer)
			Me.values = New Object(levelCount - 1){}
			Me.fields = New PivotGridField(levelCount - 1){}
			Me.cache = New Dictionary(Of PivotGridField, Object)(levelCount)
		End Sub

		Default Public ReadOnly Property Item(ByVal index As Integer) As Object
			Get
				Return values(index)
			End Get
		End Property
		Default Public ReadOnly Property Item(ByVal field As PivotGridField) As Object
			Get
				Dim res As Object
				If cache.TryGetValue(field, res) Then
					Return res
				End If
				Dim index As Integer = Array.IndexOf(Of PivotGridField)(fields, field)
				If index < 0 Then
					Return Nothing
				End If
				res = values(index)
				cache.Add(field, res)
				Return res
			End Get
		End Property

		Public Sub SetValue(ByVal index As Integer, ByVal value As Object, ByVal field As PivotGridField)
			Me.values(index) = value
			Me.fields(index) = field
		End Sub

		Public ValueType As PivotGridValueType
		Public DataField As PivotGridField
	End Class


	Friend Class SortInfo
		Public DataField As PivotGridField
		Public Conditions As New List(Of PivotGridFieldSortCondition)()
	End Class
End Namespace
