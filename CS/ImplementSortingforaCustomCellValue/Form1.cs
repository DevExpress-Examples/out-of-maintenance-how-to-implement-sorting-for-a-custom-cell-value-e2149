using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using System.Collections;

namespace WindowsFormsApplication15 {
    public partial class Form1 : Form {
        List<object> yearValues;
        PivotData pivotData;
        SortInfo sortInfo;

        public Form1() {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'nwindDataSet.SalesPerson' table. You can move, or remove it, as needed.
            this.salesPersonTableAdapter.Fill(this.nwindDataSet.CustomerReports);

            this.pivotData = FillPivotData();
        }

        List<object> YearValues {
            get {
                if(yearValues == null) {
                    yearValues = new List<object>();
                    yearValues.AddRange(fieldOrderDate.GetUniqueValues());
                    yearValues.Sort();
                }
                return yearValues;
            }
        }

        private void pivotGridControl1_CustomCellValue(object sender, PivotCellValueEventArgs e) {
            if(e.DataField == fieldProductAmount1 && e.Value != null) {               
                List<object> rowValues = new List<object>();
                foreach(PivotGridField field in e.GetRowFields()) {
                    rowValues.Add(UpdateValue(field, e.RowIndex, e));
                }
                List<object> columnValues = new List<object>();
                foreach(PivotGridField field in e.GetColumnFields()) {
                    columnValues.Add(UpdateValue(field, e.ColumnIndex, e));
                }
                decimal previousValue = Convert.ToDecimal(e.GetCellValue(columnValues.ToArray(), rowValues.ToArray(), fieldProductAmount));
                decimal value = Convert.ToDecimal(e.GetCellValue(fieldProductAmount));
                if(previousValue == 0m || e.Value == null) {
                    e.Value = null;
                } else {
                    e.Value = ((decimal)e.Value / previousValue - 1);
                }
            }
        }

        private void pivotGridControl1_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e) {
            if(this.pivotData == null || sortInfo == null) {
                e.Handled = false;
                return;
            }
            int sortByColumnIndex = GetSortByColumnIndex(e);
            int value1RowIndex = GetValueIndex(false, e.Value1, e.Field, null);
            int value2RowIndex = GetValueIndex(false, e.Value2, e.Field, null);
            if(value1RowIndex < 0 || value2RowIndex < 0 || sortByColumnIndex < 0)
                return;
            object cellValue1 = pivotData.GetCellValue(sortByColumnIndex, value1RowIndex);
            object cellValue2 = pivotData.GetCellValue(sortByColumnIndex, value2RowIndex);
            e.Handled = true;
            if(object.Equals(cellValue1, cellValue2))
                e.Result = Comparer.Default.Compare(e.Value1, e.Value2);
            else
                e.Result = Comparer.Default.Compare(cellValue1, cellValue2);
            return;
        }

        object UpdateValue(PivotGridField field, int index, PivotCellValueEventArgs e) {
            object value = e.GetFieldValue(field, index);
            if(field == fieldOrderDate) {
                int currentPosition = YearValues.IndexOf(value);
                if(currentPosition > 0) {
                    value = YearValues[currentPosition - 1];
                } else {
                    value = "0000";
                }
            }
            return value;
        }

        PivotData FillPivotData() {
            int columnCount = pivotGridControl1.Cells.ColumnCount,
                rowCount = pivotGridControl1.Cells.RowCount;
            List<PivotGridField> columnFields = pivotGridControl1.GetFieldsByArea(PivotArea.ColumnArea),
                rowFields = pivotGridControl1.GetFieldsByArea(PivotArea.RowArea);

            PivotData data = new PivotData(columnCount, rowCount,
                columnFields.Count, rowFields.Count);

            FillFieldValues(true, columnCount, columnFields, data);
            FillFieldValues(false, rowCount, rowFields, data);

            for(int i = 0; i < columnCount; i++) {
                for(int j = 0; j < rowCount; j++) {
                    PivotCellEventArgs info = pivotGridControl1.Cells.GetCellInfo(i, j);
                    if(info != null) {
                        data.SetCellValue(i, j, info.Value);
                    }
                }
            }

            return data;
        }
        void FillFieldValues(bool isColumn, int count, List<PivotGridField> fields, PivotData data) {
            for(int i = 0; i < count; i++) {
                FieldValue value = data.GetFieldValue(isColumn, i);
                value.ValueType = pivotGridControl1.GetFieldValueType(isColumn, i);
                value.DataField = pivotGridControl1.Cells.GetCellInfo(isColumn ? i : 0, isColumn ? 0 : i).DataField;
                for(int j = 0; j < fields.Count; j++) {
                    value.SetValue(j, pivotGridControl1.GetFieldValue(fields[j], i), fields[j]);
                }                
            }
        }

        int GetSortByColumnIndex(PivotGridCustomFieldSortEventArgs e) {
            if(sortInfo.Conditions.Count == 0) {
                for(int i = pivotData.ColumnCount - 1; i >= 0; i--) {
                    FieldValue value = pivotData.GetFieldValue(true, i);
                    if(value.ValueType == PivotGridValueType.GrandTotal && value.DataField == sortInfo.DataField)
                        return i;
                }
            } else {
                for(int i = 0; i < pivotData.ColumnCount; i++) {
                    FieldValue value = pivotData.GetFieldValue(true, i);
                    if(IsValueFit(value, sortInfo.Conditions, sortInfo.DataField)) {
                        return i;
                    }
                }
            }
            return -1;
        }

        int GetValueIndex(bool isColumn, object value, PivotGridField field, PivotGridField dataField) {
            int count = isColumn ? pivotData.ColumnCount : pivotData.RowCount;
            for(int i = 0; i < count; i++) {
                FieldValue fieldValue = pivotData.GetFieldValue(isColumn, i);
                if(object.Equals(value, fieldValue[field]) && (dataField == null || fieldValue.DataField == dataField)) {
                    return i;
                }
            }
            return -1;
        }

        bool IsValueFit(FieldValue value, List<PivotGridFieldSortCondition> conds, PivotGridFieldBase field) {            
            for(int i = 0; i < conds.Count; i++) {
                PivotGridFieldSortCondition cond = conds[i];
                if(!(object.Equals(value[(PivotGridField)cond.Field], cond.Value) && field == value.DataField as PivotGridFieldBase))
                    return false;
            }
            return true;
        }
        private void pivotGridControl1_MouseClick(object sender, MouseEventArgs e) {
            PivotGridHitInfo hi = pivotGridControl1.CalcHitInfo(e.Location);
            if(hi.HitTest == PivotGridHitTest.Value && hi.ValueInfo.IsColumn) {
                ApplySortByValue(hi.ValueInfo);
            }
        }
        void ApplySortByValue(PivotFieldValueEventArgs valueInfo) {
            if(valueInfo.DataField != valueInfo.Field) return;
            SortInfo sortInfo = new SortInfo();
            sortInfo.DataField = valueInfo.DataField;
            PivotGridField[] fields = valueInfo.GetHigherLevelFields();
            for(int i = 0; i < fields.Length; i++) {
                sortInfo.Conditions.Add(new PivotGridFieldSortCondition(fields[i], valueInfo.GetFieldValue(fields[i], valueInfo.MinIndex)));
            }            
            this.sortInfo = sortInfo;
            pivotGridControl1.RefreshData();
        }

        private void pivotGridControl1_ShowMenu(object sender, PivotGridMenuEventArgs e) {
            if(e.MenuType == PivotGridMenuType.FieldValue)
                e.Allow = false;
        }
    }

    class PivotData {
        FieldValue[] columns, rows;
        object[,] cells;

        public PivotData(int columnCount, int rowCount, int columnLevelCount, int rowLevelCount) {
            this.cells = new object[columnCount, rowCount];
            InitFieldValues(ref this.columns, columnCount, columnLevelCount);
            InitFieldValues(ref this.rows, rowCount, rowLevelCount);
        }

        public int ColumnCount { get { return this.columns.Length; } }
        public int RowCount { get { return this.rows.Length; } }

        void InitFieldValues(ref FieldValue[] values, int length, int levelCount) {
            values = new FieldValue[length];
            for(int i = 0; i < length; i++) {
                values[i] = new FieldValue(levelCount);
            }
        }

        public object GetCellValue(int columnIndex, int rowIndex) {
            return this.cells[columnIndex, rowIndex];
        }
        public void SetCellValue(int columnIndex, int rowIndex, object value) {
            this.cells[columnIndex, rowIndex] = value;
        }
        public FieldValue GetFieldValue(bool isColumn, int index) {
            FieldValue[] array = isColumn ? this.columns : this.rows;
            return array[index];
        }

        
    }

    class FieldValue {
        object[] values;
        PivotGridField[] fields;
        Dictionary<PivotGridField, object> cache;

        public FieldValue(int levelCount) {
            this.values = new object[levelCount];
            this.fields = new PivotGridField[levelCount];
            this.cache = new Dictionary<PivotGridField, object>(levelCount);
        }

        public object this[int index] {
            get { return values[index]; }
        }
        public object this[PivotGridField field] {
            get {
                object res;
                if(cache.TryGetValue(field, out res))
                    return res;
                int index = Array.IndexOf<PivotGridField>(fields, field);
                if(index < 0) return null;
                res = values[index];
                cache.Add(field, res);
                return res;
            }
        }

        public void SetValue(int index, object value, PivotGridField field) {
            this.values[index] = value;
            this.fields[index] = field;
        }

        public PivotGridValueType ValueType;
        public PivotGridField DataField;
    }


    class SortInfo {        
        public PivotGridField DataField;
        public List<PivotGridFieldSortCondition> Conditions = new List<PivotGridFieldSortCondition>();
    }
}
