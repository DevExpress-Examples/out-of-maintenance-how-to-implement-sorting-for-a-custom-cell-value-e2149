namespace WindowsFormsApplication15
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.salesPersonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nwindDataSet = new WindowsFormsApplication15.nwindDataSet();
            this.fieldProductAmount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldProductAmount1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldOrderDate = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldCompanyName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldProductName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.salesPersonTableAdapter = new WindowsFormsApplication15.nwindDataSetTableAdapters.CustomerReportsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesPersonBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nwindDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pivotGridControl1.DataSource = this.salesPersonBindingSource;
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldProductAmount,
            this.fieldProductAmount1,
            this.fieldOrderDate,
            this.fieldCompanyName,
            this.fieldProductName});
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(578, 394);
            this.pivotGridControl1.TabIndex = 0;
            this.pivotGridControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pivotGridControl1_MouseClick);
            this.pivotGridControl1.CustomFieldSort += new DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventHandler(this.pivotGridControl1_CustomFieldSort);
            this.pivotGridControl1.CustomCellValue += new System.EventHandler<DevExpress.XtraPivotGrid.PivotCellValueEventArgs>(this.pivotGridControl1_CustomCellValue);
            this.pivotGridControl1.ShowMenu += new DevExpress.XtraPivotGrid.PivotGridMenuEventHandler(this.pivotGridControl1_ShowMenu);
            // 
            // salesPersonBindingSource
            // 
            this.salesPersonBindingSource.DataMember = "CustomerReports";
            this.salesPersonBindingSource.DataSource = this.nwindDataSet;
            // 
            // nwindDataSet
            // 
            this.nwindDataSet.DataSetName = "nwindDataSet";
            this.nwindDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fieldProductAmount
            // 
            this.fieldProductAmount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldProductAmount.AreaIndex = 0;
            this.fieldProductAmount.FieldName = "ProductAmount";
            this.fieldProductAmount.Name = "fieldProductAmount";
            // 
            // fieldProductAmount1
            // 
            this.fieldProductAmount1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldProductAmount1.AreaIndex = 1;
            this.fieldProductAmount1.Caption = "Percent Variation";
            this.fieldProductAmount1.CellFormat.FormatString = "P";
            this.fieldProductAmount1.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.fieldProductAmount1.FieldName = "ProductAmount";
            this.fieldProductAmount1.Name = "fieldProductAmount1";
            // 
            // fieldOrderDate
            // 
            this.fieldOrderDate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldOrderDate.AreaIndex = 0;
            this.fieldOrderDate.FieldName = "OrderDate";
            this.fieldOrderDate.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
            this.fieldOrderDate.Name = "fieldOrderDate";
            this.fieldOrderDate.UnboundFieldName = "fieldOrderDate";
            // 
            // fieldCompanyName
            // 
            this.fieldCompanyName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldCompanyName.AreaIndex = 0;
            this.fieldCompanyName.FieldName = "CompanyName";
            this.fieldCompanyName.Name = "fieldCompanyName";
            this.fieldCompanyName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // fieldProductName
            // 
            this.fieldProductName.AreaIndex = 0;
            this.fieldProductName.FieldName = "ProductName";
            this.fieldProductName.Name = "fieldProductName";
            // 
            // salesPersonTableAdapter
            // 
            this.salesPersonTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 394);
            this.Controls.Add(this.pivotGridControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesPersonBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nwindDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private nwindDataSet nwindDataSet;
        private System.Windows.Forms.BindingSource salesPersonBindingSource;
        private DevExpress.XtraPivotGrid.PivotGridField fieldProductAmount;
        private DevExpress.XtraPivotGrid.PivotGridField fieldProductAmount1;
        private WindowsFormsApplication15.nwindDataSetTableAdapters.CustomerReportsTableAdapter salesPersonTableAdapter;
        private DevExpress.XtraPivotGrid.PivotGridField fieldOrderDate;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCompanyName;
        private DevExpress.XtraPivotGrid.PivotGridField fieldProductName;
    }
}

