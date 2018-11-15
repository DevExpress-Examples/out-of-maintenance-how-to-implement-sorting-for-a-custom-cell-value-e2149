<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/ImplementSortingforaCustomCellValue/Form1.cs)
* [nwindDataSet.cs](./CS/ImplementSortingforaCustomCellValue/nwindDataSet.cs)
* [Program.cs](./CS/ImplementSortingforaCustomCellValue/Program.cs)
<!-- default file list end -->
# How To: Implement Sorting for a Custom Cell Value


<p>In the current version, PivotGridControl doesn't support sorting by summary values, replaced in the <strong>PivotGridControl.CustomCellValue</strong> event. It's because PivotGridControl first filters, groups and sorts cell values and then raises the <strong>PivotGridControl.CustomCellValue</strong> and <strong>PivotGridControl.CustomCellDisplayText</strong> events. This example demonstrates how to implement sorting for such values. Note that this solution will work in simple cases and depends on the layout. In many cases, when it's not required to get other cell values, this approach can be replaced with handling the <strong>PivotGridControl.CustomSummary</strong> event. </p><p>Some key points include:</p><p>- In the <strong>PivotGridControl.CustomFieldSort</strong> event, custom cell values can't be obtained because they are not formed by this moment. The FormLoad event is used to remember custom field values into a special variable for further calculation in the <strong>PivotGridControl.CustomFieldSort</strong> event.<br />
- As PivotGridControl sorts cell values by SortBySummaryInfo, to disable sorting by summary, <strong>PivotGridControl.ShowMenu event is handled to prevent fields sort menu from being shown. The PivotGridControl.MouseClick event is handled to calculate sort info when a user clicks PivotGridControl's column value.<br />
-</strong><strong> </strong>When you sort pivot rows or columns by summary values, the PivotGrid uses the sort order specified in row or column fields, and does not use sort settings from a data field used for sorting.<strong> PivotGridControl.CustomFieldSort event is handled for the PivotArea.RowArea field. This allows users to change the row values order by comparing remembered custom cell values in the cell area using sort info calculated on the PivotGridControl.MouseClick event.</strong><strong><br />
</strong></p>

<br/>


