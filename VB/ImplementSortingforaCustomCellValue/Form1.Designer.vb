Imports Microsoft.VisualBasic
Imports System
Namespace WindowsFormsApplication15
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pivotGridControl1 = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.fieldProductAmount = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.fieldProductAmount1 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.fieldOrderDate = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.fieldCompanyName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.fieldProductName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.NwindDataSet = New nwindDataSet()
        Me.CustomerReportsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CustomerReportsTableAdapter = New nwindDataSetTableAdapters.CustomerReportsTableAdapter()
        CType(Me.pivotGridControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.NwindDataSet,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CustomerReportsBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'pivotGridControl1
        '
        Me.pivotGridControl1.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl1.DataSource = Me.CustomerReportsBindingSource
        Me.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pivotGridControl1.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.fieldProductAmount, Me.fieldProductAmount1, Me.fieldOrderDate, Me.fieldCompanyName, Me.fieldProductName})
        Me.pivotGridControl1.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControl1.Name = "pivotGridControl1"
        Me.pivotGridControl1.Size = New System.Drawing.Size(578, 394)
        Me.pivotGridControl1.TabIndex = 0
        '
        'fieldProductAmount
        '
        Me.fieldProductAmount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldProductAmount.AreaIndex = 0
        Me.fieldProductAmount.FieldName = "ProductAmount"
        Me.fieldProductAmount.Name = "fieldProductAmount"
        '
        'fieldProductAmount1
        '
        Me.fieldProductAmount1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldProductAmount1.AreaIndex = 1
        Me.fieldProductAmount1.Caption = "Percent Variation"
        Me.fieldProductAmount1.CellFormat.FormatString = "P"
        Me.fieldProductAmount1.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.fieldProductAmount1.FieldName = "ProductAmount"
        Me.fieldProductAmount1.Name = "fieldProductAmount1"
        '
        'fieldOrderDate
        '
        Me.fieldOrderDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.fieldOrderDate.AreaIndex = 0
        Me.fieldOrderDate.FieldName = "OrderDate"
        Me.fieldOrderDate.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
        Me.fieldOrderDate.Name = "fieldOrderDate"
        Me.fieldOrderDate.UnboundFieldName = "fieldOrderDate"
        '
        'fieldCompanyName
        '
        Me.fieldCompanyName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldCompanyName.AreaIndex = 0
        Me.fieldCompanyName.FieldName = "CompanyName"
        Me.fieldCompanyName.Name = "fieldCompanyName"
        Me.fieldCompanyName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom
        '
        'fieldProductName
        '
        Me.fieldProductName.AreaIndex = 0
        Me.fieldProductName.FieldName = "ProductName"
        Me.fieldProductName.Name = "fieldProductName"
        '
        'NwindDataSet
        '
        Me.NwindDataSet.DataSetName = "nwindDataSet"
        Me.NwindDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CustomerReportsBindingSource
        '
        Me.CustomerReportsBindingSource.DataMember = "CustomerReports"
        Me.CustomerReportsBindingSource.DataSource = Me.NwindDataSet
        '
        'CustomerReportsTableAdapter
        '
        Me.CustomerReportsTableAdapter.ClearBeforeFill = true
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(578, 394)
        Me.Controls.Add(Me.pivotGridControl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.pivotGridControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.NwindDataSet,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CustomerReportsBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub


		#End Region

		Private WithEvents pivotGridControl1 As DevExpress.XtraPivotGrid.PivotGridControl
		Private fieldProductAmount As DevExpress.XtraPivotGrid.PivotGridField
		Private fieldProductAmount1 As DevExpress.XtraPivotGrid.PivotGridField
		Private fieldOrderDate As DevExpress.XtraPivotGrid.PivotGridField
		Private fieldCompanyName As DevExpress.XtraPivotGrid.PivotGridField
		Private fieldProductName As DevExpress.XtraPivotGrid.PivotGridField
  Friend WithEvents NwindDataSet As nwindDataSet
  Friend WithEvents CustomerReportsBindingSource As System.Windows.Forms.BindingSource
  Friend WithEvents CustomerReportsTableAdapter As nwindDataSetTableAdapters.CustomerReportsTableAdapter
	End Class
End Namespace

